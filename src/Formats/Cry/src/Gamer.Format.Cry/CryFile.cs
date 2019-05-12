using Gamer.Format.Cry.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry
{
    public partial class CryFile
    {
        /// <summary>
        /// File extensions processed by CryEngine
        /// </summary>
        static HashSet<string> _validExtensions = new HashSet<string>
        {
            ".cgf",
            ".cga",
            ".chr",
            ".skin",
            ".anim"
        };

        public CryFile(string fileName, string dataDir)
        {
            InputFile = fileName;
            var inputFile = new FileInfo(fileName);
            var inputFiles = new List<FileInfo> { inputFile };
            // Validate file extension - handles .cgam / skinm
            if (!_validExtensions.Contains(inputFile.Extension))
            {
                Log("Warning: Unsupported file extension - please use a cga, cgf, chr or skin file");
                throw new FileLoadException("Warning: Unsupported file extension - please use a cga, cgf, chr or skin file", fileName);
            }
            //
            var mFile = new FileInfo(Path.ChangeExtension(fileName, $"{inputFile.Extension}m"));
            if (mFile.Exists)
            {
                Log($"Found geometry file {mFile.Name}");
                inputFiles.Add(mFile); // Add to list of files to process
            }
            //
            Models = new List<Model> { };
            foreach (var file in inputFiles)
            {
                // Each file (.cga and .cgam if applicable) will have its own RootNode.  This can cause problems.  .cga files with a .cgam files won't have geometry for the one root node.
                var model = Model.FromFile(file.FullName);
                if (RootNode == null)
                    RootNode = model.RootNode; // This makes the assumption that we read the .cga file before the .cgam file.
                //RootNode = RootNode ?? model.RootNode;
                Bones = Bones ?? model.Bones;
                Models.Add(model);
            }
            SkinningInfo = ConsolidateSkinningInfo();
            // For eanch node with geometry info, populate that node's Mesh Chunk GeometryInfo with the geometry data.
            ConsolidateGeometryInfo();
            // Get the material file name
            foreach (ChunkMtlName mtlChunk in Models.SelectMany(a => a.ChunkMap.Values).Where(c => c.ChunkType == ChunkTypeEnum.MtlName))
            {
                // Don't process child or collision materials for now
                if (mtlChunk.MatType == MtlNameTypeEnum.Child || mtlChunk.MatType == MtlNameTypeEnum.Unknown1)
                    continue;
                // The Replace part is for SC files that point to a _core material file that doesn't exist.
                var cleanName = mtlChunk.Name.Replace("_core", string.Empty);
                //
                FileInfo materialFile;
                if (mtlChunk.Name.Contains("default_body"))
                {
                    // New MWO models for some crazy reason don't put the actual mtl file name in the mtlchunk.  They just have /objects/mechs/default_body
                    // have to assume that it's /objects/mechs/<mechname>/body/<mechname>_body.mtl.  There is also a <mechname>.mtl that contains mtl 
                    // info for hitboxes, but not needed.
                    // TODO:  This isn't right.  Fix it.
                    var charsToClean = cleanName.ToCharArray().Intersect(Path.GetInvalidFileNameChars()).ToArray();
                    if (charsToClean.Length > 0)
                        foreach (char character in charsToClean)
                            cleanName = cleanName.Replace(character.ToString(), string.Empty);
                    materialFile = new FileInfo(Path.Combine(Path.GetDirectoryName(fileName), cleanName));
                }
                else if (mtlChunk.Name.Contains(@"/") || mtlChunk.Name.Contains(@"\"))
                {
                    // The mtlname has a path.  Most likely starts at the Objects directory.
                    var stringSeparators = new[] { @"\", @"/" };
                    // if objectdir is provided, check objectdir + mtlchunk.name
                    if (dataDir != null)
                        materialFile = new FileInfo(Path.Combine(dataDir, mtlChunk.Name));
                    else // object dir not provided, but we have a path.  Just grab the last part of the name and check the dir of the cga file
                    {
                        var r = mtlChunk.Name.Split(stringSeparators, StringSplitOptions.None);
                        materialFile = new FileInfo(r[r.Length - 1]);
                    }
                }
                else
                {
                    var charsToClean = cleanName.ToCharArray().Intersect(Path.GetInvalidFileNameChars()).ToArray();
                    if (charsToClean.Length > 0)
                        foreach (var character in charsToClean)
                            cleanName = cleanName.Replace(character.ToString(), string.Empty);
                    materialFile = new FileInfo(Path.Combine(Path.GetDirectoryName(fileName), cleanName));
                }
                // First try relative to file being processed
                if (materialFile.Extension != ".mtl") materialFile = new FileInfo(Path.ChangeExtension(materialFile.FullName, "mtl"));
                // Then try just the last part of the chunk, relative to the file being processed
                if (!materialFile.Exists) materialFile = new FileInfo(Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileName(cleanName)));
                if (materialFile.Extension != ".mtl") materialFile = new FileInfo(Path.ChangeExtension(materialFile.FullName, "mtl"));
                // Then try relative to the ObjectDir
                if (!materialFile.Exists && dataDir != null) materialFile = new FileInfo(Path.Combine(dataDir, cleanName));
                if (materialFile.Extension != ".mtl") materialFile = new FileInfo(Path.ChangeExtension(materialFile.FullName, "mtl"));
                // Then try just the fileName.mtl
                if (!materialFile.Exists) materialFile = new FileInfo(fileName);
                if (materialFile.Extension != ".mtl") materialFile = new FileInfo(Path.ChangeExtension(materialFile.FullName, "mtl"));

                // TODO: Try more paths

                // Populate CryEngine_Core.Material
                var material = Material.FromFile(materialFile);
                if (material != null)
                {
                    Log($"Located material file {materialFile.Name}");
                    Materials = FlattenMaterials(material).Where(m => m.Textures != null).ToArray();
                    if (Materials.Length == 1)
                    {
                        // only one material, so it's a material file with no submaterials.  Check and set the name
                        //Console.WriteLine("Single material found.  setting name...");
                        Materials[0].Name = RootNode.Name;
                    }
                    // Early return - we have the material map
                    return;
                }
                else Log($"Unable to locate material file {mtlChunk.Name}.mtl");
            }
            Log("Unable to locate any material file");
            Materials = new Material[] { };
        }

        void ConsolidateGeometryInfo()
        {
            //foreach (Model model in Models)
            //{
            //    var nodes = model.ChunkNodes;
            //}
        }

        SkinningInfo ConsolidateSkinningInfo()
        {
            var skin = new SkinningInfo();
            foreach (var model in Models)
            {
                skin.HasSkinningInfo = Models.Any(a => a.SkinningInfo.HasSkinningInfo);
                skin.HasBoneMapDatastream = Models.Any(a => a.SkinningInfo.HasBoneMapDatastream);
                if (model.SkinningInfo.IntFaces != null) skin.IntFaces = model.SkinningInfo.IntFaces;
                if (model.SkinningInfo.IntVertices != null) skin.IntVertices = model.SkinningInfo.IntVertices;
                if (model.SkinningInfo.LookDirectionBlends != null) skin.LookDirectionBlends = model.SkinningInfo.LookDirectionBlends;
                if (model.SkinningInfo.MorphTargets != null) skin.MorphTargets = model.SkinningInfo.MorphTargets;
                if (model.SkinningInfo.PhysicalBoneMeshes != null) skin.PhysicalBoneMeshes = model.SkinningInfo.PhysicalBoneMeshes;
                if (model.SkinningInfo.BoneEntities != null) skin.BoneEntities = model.SkinningInfo.BoneEntities;
                if (model.SkinningInfo.BoneMapping != null) skin.BoneMapping = model.SkinningInfo.BoneMapping;
                if (model.SkinningInfo.Collisions != null) skin.Collisions = model.SkinningInfo.Collisions;
                if (model.SkinningInfo.CompiledBones != null) skin.CompiledBones = model.SkinningInfo.CompiledBones;
                if (model.SkinningInfo.Ext2IntMap != null) skin.Ext2IntMap = model.SkinningInfo.Ext2IntMap;
            }
            return skin;
        }

        /// <summary>
        /// There will be one Model for each model in this object.  
        /// </summary>
        public List<Model> Models { get; internal set; }
        public Material[] Materials { get; internal set; }
        public ChunkNode RootNode { get; internal set; }
        public ChunkCompiledBones Bones { get; internal set; }
        public SkinningInfo SkinningInfo { get; set; }
        public string InputFile { get; internal set; }

        Chunk[] _chunks;
        public Chunk[] Chunks
        {
            get
            {
                if (_chunks == null)
                    _chunks = Models.SelectMany(m => m.ChunkMap.Values).ToArray();
                return _chunks;
            }
        }

        // Cannot use the Node name for the key.  Across a couple files, you may have multiple nodes with same name.
        public Dictionary<string, ChunkNode> _nodeMap;
        public Dictionary<string, ChunkNode> NodeMap
        {
            get
            {
                if (_nodeMap == null)
                {
                    _nodeMap = new Dictionary<string, ChunkNode>(StringComparer.InvariantCultureIgnoreCase) { };
                    ChunkNode rootNode = null;
                    Log("Mapping Nodes");
                    foreach (var model in Models)
                    {
                        model.RootNode = rootNode = rootNode ?? model.RootNode; // Each model will have it's own rootnode.
                        foreach (var node in model.ChunkMap.Values.Where(c => c.ChunkType == ChunkTypeEnum.Node).Select(c => c as ChunkNode))
                        {
                            // Preserve existing parents
                            if (_nodeMap.ContainsKey(node.Name))
                            {
                                var parentNode = _nodeMap[node.Name].ParentNode;
                                if (parentNode != null)
                                    parentNode = _nodeMap[parentNode.Name];
                                node.ParentNode = parentNode;
                            }
                            _nodeMap[node.Name] = node;    // TODO:  fix this.  The node name can conflict.
                        }
                    }
                }
                return _nodeMap;
            }
        }

        /// <summary>
        /// Flatten all child materials into a one dimensional list
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public static IEnumerable<Material> FlattenMaterials(Material material)
        {
            if (material != null)
            {
                yield return material;
                if (material.SubMaterials != null)
                    foreach (var subMaterial in material.SubMaterials.SelectMany(m => FlattenMaterials(m)))
                        yield return subMaterial;
            }
        }

        public IEnumerable<string> GetTexturePaths()
        {
            foreach (var texture in Materials.SelectMany(x => x.Textures))
                if (!string.IsNullOrEmpty(texture.File))
                    yield return texture.File;
        }
    }
}