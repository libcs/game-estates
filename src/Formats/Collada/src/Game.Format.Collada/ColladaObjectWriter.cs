using Game.Format.Cry;
using Game.Format.Cry.Core;
using grendgine_collada;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using static Game.Core.Debug;

namespace Game.Format.Collada
{
    /// <summary>
    /// export to .dae format (COLLADA)
    /// </summary>
    /// <seealso cref="Game.Format.Collada.BaseWriter" />
    public class ColladaObjectWriter : CryObjectWriter
    {
        public XmlSchema schema = new XmlSchema();
        public FileInfo daeOutputFile;

        public Grendgine_Collada daeObject = new Grendgine_Collada();       // This is the serializable class.
        XmlSerializer mySerializer = new XmlSerializer(typeof(Grendgine_Collada));

        public ColladaObjectWriter(CryFile cryFile) : base(cryFile) { }

        public override void Write(string outputDir = null, bool preservePath = true)
        {
            // The root of the functions to write Collada files
            // At this point, we should have a cryData.Asset object, fully populated.
            Log("*** Starting WriteCOLLADA() ***");

            Log($"Number of models: {CryFile.Models.Count}");
            for (var i = 0; i < CryFile.Models.Count; i++)
                Log($"\tNumber of nodes in model: {CryFile.Models[i].NodeMap.Count}");

            #region Output testing

            //foreach (ChunkDataStream stream in CryData.Models[1].ChunkMap.Values.Where(a => a.ChunkType == ChunkTypeEnum.DataStream))
            //    if (stream.DataStreamType == DataStreamTypeEnum.TANGENTS)
            //    {
            //        foreach (var vec in stream.Tangents)
            //            Console.WriteLine("Tangent: {0:F6} {1:F6} {2:F6}", vec.x/127.0, vec.y/127.0, vec.z/127.0);
            //        Console.WriteLine($"Max x: {stream.Normals.Max(a => a.x)}");
            //        Console.WriteLine($"Max y: {stream.Normals.Max(a => a.y)}");
            //        Console.WriteLine($"Max z: {stream.Normals.Max(a => a.z)}");
            //    }
            //foreach (ChunkNode node in CryData.Models[1].NodeMap.Values)
            //{
            //    Console.WriteLine($"Node Chunk {node.Name} in model {node._model.FileName}");
            //    node.WriteChunk();
            //    Console.ReadKey();
            //}
            //foreach (ChunkNode node in CryData.Models[1].NodeMap.Values.Where(a => a.Name.Contains("Belly_Wing_Right_Decal")))
            //{
            //    node.WriteChunk();
            //    node.ParentNode.WriteChunk();
            //    node.ParentNode.ParentNode.WriteChunk();
            //    node.ParentNode.ParentNode.ParentNode.WriteChunk();
            //    Console.ReadKey();
            //}
            //foreach (var result in CryData.Models[0].SkinningInfo.BoneMapping)        // To see if the bone index > than the number of bones and bone weights
            //{
            //    for (var i = 0; i < 4; i++)
            //        if (result.Weight[i] > 0)
            //            Console.WriteLine($"Bone Weight: {result.Weight[i]}");
            //}
            //Console.WriteLine($"{CryData.Models[0].SkinningInfo.BoneMapping.Count} bone weights found");

            #endregion

            // File name will be "object name.dae"
            daeOutputFile = new FileInfo(GetOutputFile("dae", outputDir, preservePath));
            GetSchema();                                                    // Loads the schema.  Needs error checking in case it's offline.
            WriteRootNode();
            WriteAsset();
            WriteLibrary_Images();
            WriteScene();
            WriteLibrary_Effects();
            WriteLibrary_Materials();
            WriteLibrary_Geometries();
            // If there is Skinning info, create the controller library and set up visual scene to refer to it.  Otherwise just write the Visual Scene
            if (CryFile.SkinningInfo.HasSkinningInfo)
            {
                WriteLibrary_Controllers();
                WriteLibrary_VisualScenesWithSkeleton();
            }
            else WriteLibrary_VisualScenes();
            //WriteLibrary_Controllers();
            //WriteLibrary_VisualScenes();
            //WriteIDs();
            if (!daeOutputFile.Directory.Exists)
                daeOutputFile.Directory.Create();
            var w = new StreamWriter(daeOutputFile.FullName); // Makes the Textwriter object for the output
            mySerializer.Serialize(w, daeObject); // Serializes the daeObject and writes to the writer
            // Validate that the Collada document is ok
            w.Close();
            //ValidateXml(); // validates against the schema
            //ValidateDoc(); // validates IDs and URLs
            Log("End of Write Collada.  Export complete.");
        }

        void WriteRootNode()
        {
            //daeObject.Collada_Version = "1.5.0";  // Blender doesn't like 1.5. :(
            daeObject.Collada_Version = "1.4.1";
        }

        public void GetSchema()
        {
            // Get the schema from kronos.org.  Needs error checking in case it's offline
            schema.ElementFormDefault = XmlSchemaForm.Qualified;
            schema.TargetNamespace = "https://www.khronos.org/files/collada_schema_1_5";
        }

        public void WriteAsset()
        {
            // Writes the Asset element in a Collada XML doc
            var fileCreated = DateTime.Now;
            var fileModified = DateTime.Now; // since this only creates, both times should be the same
            var asset = new Grendgine_Collada_Asset
            {
                Revision = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            };
            var contributors = new Grendgine_Collada_Asset_Contributor[2];
            contributors[0] = new Grendgine_Collada_Asset_Contributor
            {
                Author = "Author",
                Author_Website = "https://github.com",
                Author_Email = "mail@mail",
                Source_Data = CryFile.RootNode.Name // The cgf/cga/skin/whatever file we read
            };
            // Get the actual file creators from the Source Chunk
            contributors[1] = new Grendgine_Collada_Asset_Contributor();
            foreach (ChunkSourceInfo tmpSource in CryFile.Chunks.Where(a => a.ChunkType == ChunkTypeEnum.SourceInfo))
            {
                contributors[1].Author = tmpSource.Author;
                contributors[1].Source_Data = tmpSource.SourceFile;
            }
            asset.Created = fileCreated;
            asset.Modified = fileModified;
            asset.Up_Axis = "Z_UP";
            asset.Unit = new Grendgine_Collada_Asset_Unit()
            {
                Meter = 1.0,
                Name = "meter"
            };
            asset.Title = CryFile.RootNode.Name;
            daeObject.Asset = asset;
            daeObject.Asset.Contributor = contributors;
        }

        public void WriteLibrary_Images()
        {
            // I think this is a  list of all the images used by the asset.
            var libraryImages = new Grendgine_Collada_Library_Images();
            daeObject.Library_Images = libraryImages;
            var images = new List<Grendgine_Collada_Image>();
            //Console.WriteLine($"Number of images { CryData.Materials.Length}");
            // We now have the image library set up.  start to populate.
            //foreach (CryEngine_Core.Material material in CryData.Materials)
            for (var k = 0; k < CryFile.Materials.Length; k++)
            {
                // each mat will have a number of texture files.  Need to create an <image> for each of them.
                //var numTextures = material.Textures.Length;
                var numTextures = CryFile.Materials[k].Textures.Length;
                for (var i = 0; i < numTextures; i++)
                {
                    // For each texture in the material, we make a new <image> object and add it to the list. 
                    var tmpImage = new Grendgine_Collada_Image
                    {
                        ID = CryFile.Materials[k].Name + "_" + CryFile.Materials[k].Textures[i].Map,
                        Name = CryFile.Materials[k].Name + "_" + CryFile.Materials[k].Textures[i].Map,
                        Init_From = new Grendgine_Collada_Init_From()
                    };
                    // Build the URI path to the file as a .dds, clean up the slashes.
                    StringBuilder b;
                    if (CryFile.Materials[k].Textures[i].File.Contains(@"/") || CryFile.Materials[k].Textures[i].File.Contains(@"\"))
                    {
                        // if Datadir is empty, need a clean name and can only search in the current directory.  If Datadir is provided, then look there.
                        if (DataDir == null)
                        {
                            b = new StringBuilder(CleanMtlFileName(CryFile.Materials[k].Textures[i].File) + ".dds");
                            if (TiffTextures) b.Replace(".dds", ".tif");
                        }
                        else b = new StringBuilder(@"/" + DataDir.FullName.Replace(" ", @"%20") + @"/" + CryFile.Materials[k].Textures[i].File);
                    }
                    else
                        b = new StringBuilder(CryFile.Materials[k].Textures[i].File);
                    if (!TiffTextures)
                    {
                        b.Replace(".tif", ".dds");
                        b.Replace(".TIF", ".dds");
                    }
                    else b.Replace(".dds", ".tif");
                    // if 1.4.1, use URI.  If 1.5.0, use Ref.
                    if (daeObject.Collada_Version == "1.4.1") tmpImage.Init_From.Uri = b.ToString();
                    else tmpImage.Init_From.Ref = b.ToString();
                    images.Add(tmpImage);
                }
            }
            // images is the array of image (Gredgine_Collada_Image) objects
            daeObject.Library_Images.Image = images.ToArray();
        }

        public void WriteLibrary_Materials()
        {
            // Create the list of materials used in this object
            // There is just one .mtl file we need to worry about.
            var libraryMaterials = new Grendgine_Collada_Library_Materials();
            // We have our top level.
            daeObject.Library_Materials = libraryMaterials;
            var numMaterials = CryFile.Materials.Length;
            // Now create a material for each material in the object
            Log($"Number of materials: {numMaterials}");
            var materials = new Grendgine_Collada_Material[numMaterials];
            for (var i = 0; i < numMaterials; i++)
            {
                var tmpMaterial = new Grendgine_Collada_Material
                {
                    Instance_Effect = new Grendgine_Collada_Instance_Effect()
                };
                // Name is blank if it's a material file with no submats.  Set to file name.
                // Need material ID here, so the meshes can reference it.  Use the chunk ID.
                if (CryFile.Materials[i].Name == null)
                {
                    tmpMaterial.Name = CryFile.RootNode.Name;
                    tmpMaterial.ID = CryFile.RootNode.Name;
                    tmpMaterial.Instance_Effect.URL = "#" + CryFile.RootNode.Name + "-effect";
                }
                else
                {
                    tmpMaterial.Name = CryFile.Materials[i].Name;
                    tmpMaterial.ID = CryFile.Materials[i].Name + "-material";          // this is the order the materials appear in the .mtl file.  Needed for geometries.
                    tmpMaterial.Instance_Effect.URL = "#" + CryFile.Materials[i].Name + "-effect";
                }
                // The # in front of tmpMaterial.name is needed to reference the effect in Library_effects.
                materials[i] = tmpMaterial;
            }
            libraryMaterials.Material = materials;
        }

        public void WriteLibrary_Effects()
        {
            // The Effects library.  This is actual material stuff, so... let's get into it!  First, let's make a library effects object
            var libraryEffects = new Grendgine_Collada_Library_Effects();
            // Like materials.  We will need one effect for each material.
            var numEffects = CryFile.Materials.Length;
            var effects = new Grendgine_Collada_Effect[numEffects];
            for (var i = 0; i < numEffects; i++)
            {
                var tmpEffect = effects[i] = new Grendgine_Collada_Effect
                {
                    //Name = CryData.Materials[i].Name,
                    ID = CryFile.Materials[i].Name + "-effect",
                    Name = CryFile.Materials[i].Name
                };
                // create the profile_common for the effect
                var profiles = new List<Grendgine_Collada_Profile_COMMON>();
                var profile = new Grendgine_Collada_Profile_COMMON();
                profiles.Add(profile);
                tmpEffect.Profile_COMMON = profiles.ToArray();

                // Create a list for the new_params
                var newparams = new List<Grendgine_Collada_New_Param>();

                // MATERIALS SAMPLER AND SURFACE
                #region MATERIALS SAMPLER AND SURFACE

                // Check to see if the texture exists, and if so make a sampler and surface.
                var numTextures = CryFile.Materials[i].Textures.Length;
                for (var j = 0; j < CryFile.Materials[i].Textures.Length; j++)
                {
                    // Add the Surface node
                    var texSurface = new Grendgine_Collada_New_Param
                    {
                        sID = CleanMtlFileName(CryFile.Materials[i].Textures[j].File) + "-surface",
                        Surface = new Grendgine_Collada_Surface
                        {
                            Type = "2D",
                            //Init_From = new Grendgine_Collada_Init_From { Uri = CleanName(texture.File) },
                            Init_From = new Grendgine_Collada_Init_From { Uri = CryFile.Materials[i].Name + "_" + CryFile.Materials[i].Textures[j].Map },
                        },
                    };
                    // Add the Sampler node
                    var texSampler = new Grendgine_Collada_New_Param
                    {
                        sID = CleanMtlFileName(CryFile.Materials[i].Textures[j].File) + "-sampler",
                        Sampler2D = new Grendgine_Collada_Sampler2D { Source = texSurface.sID }
                    };
                    newparams.Add(texSurface);
                    newparams.Add(texSampler);
                }

                #endregion

                // THE TECHNIQUE
                #region THE TECHNIQUE

                // Make the techniques for the profile
                var phong = new Grendgine_Collada_Phong
                {
                    Diffuse = new Grendgine_Collada_FX_Common_Color_Or_Texture_Type(),
                    Specular = new Grendgine_Collada_FX_Common_Color_Or_Texture_Type(),
                    Emission = new Grendgine_Collada_FX_Common_Color_Or_Texture_Type
                    {
                        Color = new Grendgine_Collada_Color { sID = "emission", Value_As_String = CryFile.Materials[i].__Emissive.Replace(",", " ") }
                    },
                    Shininess = new Grendgine_Collada_FX_Common_Float_Or_Param_Type
                    {
                        Float = new Grendgine_Collada_SID_Float { sID = "shininess", Value = (float)CryFile.Materials[i].Shininess }
                    },
                    Index_Of_Refraction = new Grendgine_Collada_FX_Common_Float_Or_Param_Type
                    {
                        Float = new Grendgine_Collada_SID_Float()
                    },
                    Transparent = new Grendgine_Collada_FX_Common_Color_Or_Texture_Type
                    {
                        Color = new Grendgine_Collada_Color { Value_As_String = (1 - CryFile.Materials[i].Opacity).ToString() }, // Subtract from 1 for proper value.
                        Opaque = new Grendgine_Collada_FX_Opaque_Channel()
                    }
                };
                var technique = profile.Technique = new Grendgine_Collada_Effect_Technique_COMMON { Phong = phong, sID = "common" };

                // Add all the emissive, etc features to the phong
                // Need to check if a texture exists.  If so, refer to the sampler.  Should be a <Texture Map="Diffuse" line if there is a map.
                bool diffound = false, specfound = false;
                foreach (var texture in CryFile.Materials[i].Textures)
                {
                    //Console.WriteLine("Processing material texture {0}", CleanName(texture.File));
                    if (texture.Map == Material.Texture.MapTypeEnum.Diffuse)
                    {
                        diffound = true;
                        // Texcoord is the ID of the UV source in geometries.  Not needed.
                        phong.Diffuse.Texture = new Grendgine_Collada_Texture { Texture = CleanMtlFileName(texture.File) + "-sampler", TexCoord = string.Empty };
                    }
                    if (texture.Map == Material.Texture.MapTypeEnum.Specular)
                    {
                        specfound = true;
                        phong.Specular.Texture = new Grendgine_Collada_Texture { Texture = CleanMtlFileName(texture.File) + "-sampler", TexCoord = string.Empty };
                    }
                    if (texture.Map == Material.Texture.MapTypeEnum.Bumpmap)
                        technique.Extra = new[] { new Grendgine_Collada_Extra
                        {
                            Technique = new[] { new Grendgine_Collada_Technique
                            {
                                profile = "FCOLLADA",
                                Data = new XmlElement[] { new Grendgine_Collada_BumpMap
                                {
                                    Textures = new[] { new Grendgine_Collada_Texture { Texture = CleanMtlFileName(texture.File) + "-sampler" } }
                                }}
                            }}
                        } };
                }
                if (!diffound)
                    phong.Diffuse.Color = new Grendgine_Collada_Color { sID = "diffuse", Value_As_String = CryFile.Materials[i].__Diffuse.Replace(",", " ") };
                if (!specfound)
                    phong.Specular.Color = new Grendgine_Collada_Color { sID = "specular", Value_As_String = CryFile.Materials[i].__Specular != null ? CryFile.Materials[i].__Specular.Replace(",", " ") : "1 1 1" };

                #endregion

                profile.New_Param = newparams.ToArray();
            }
            libraryEffects.Effect = effects;
            daeObject.Library_Effects = libraryEffects;
            // libraryEffects contains a number of effects objects.  One effects object for each material.
        }

        /// <summary>
        /// Write the Library_Geometries element.  These won't be instantiated except through the visual scene or controllers.
        /// </summary>
        public void WriteLibrary_Geometries()
        {
            // Make a list for all the geometries objects we will need. Will convert to array at end.  Define the array here as well
            // Unfortunately we have to define a Geometry for EACH meshsubset in the meshsubsets, since the mesh can contain multiple materials
            var geometryList = new List<Grendgine_Collada_Geometry>();

            // For each of the nodes, we need to write the geometry.
            // Use a foreach statement to get all the node chunks.  This will get us the meshes, which will contain the vertex, UV and normal info.
            foreach (ChunkNode nodeChunk in CryFile.Chunks.Where(a => a.ChunkType == ChunkTypeEnum.Node))
            {
                // Create a geometry object.  Use the chunk ID for the geometry ID
                // Will have to be careful with this, since with .cga/.cgam pairs will need to match by Name.
                // Now make the mesh object.  This will have 3 sources, 1 vertices, and 1 or more polylist (with material ID)
                // If the Object ID of Node chunk points to a Helper or a Controller though, place an empty.
                // Will have to figure out transforms here too.
                // need to make a list of the sources and triangles to add to tmpGeo.Mesh
                var sourceList = new List<Grendgine_Collada_Source>();
                //var triList = new List<Grendgine_Collada_Triangles>();  // Use PolyList over trilist
                var polylistList = new List<Grendgine_Collada_Polylist>();
                ChunkDataStream tmpNormals = null;
                ChunkDataStream tmpUVs = null;
                ChunkDataStream tmpVertices = null;
                ChunkDataStream tmpVertsUVs = null;
                ChunkDataStream tmpIndices = null;
                ChunkDataStream tmpColors = null;
                ChunkDataStream tmpTangents = null;
                //var geometryInfo = nodeChunk.ObjectChunk.

                // Don't render shields
                if (SkipShieldNodes && nodeChunk.Name.StartsWith("$shield"))
                {
                    Log($"Skipped shields node {nodeChunk.Name}");
                    continue;
                }
                // Don't render proxies
                if (SkipStreamNodes && nodeChunk.Name.StartsWith("stream"))
                {
                    Log($"Skipped stream node {nodeChunk.Name}");
                    continue;
                }
                if (nodeChunk.ObjectChunk == null)
                {
                    Log($"Skipped node with missing Object {nodeChunk.Name}");
                    continue;
                }
                if (nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID].ChunkType == ChunkTypeEnum.Mesh)
                {
                    // Get the mesh chunk and submesh chunk for this node.
                    var tmpMeshChunk = (ChunkMesh)nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID];
                    // Check to see if the Mesh points to a PhysicsData mesh.  Don't want to write these.
                    if (tmpMeshChunk.MeshPhysicsData != 0)
                    {
                        // TODO:  Implement this chunk
                    }
                    // For the SC files, you can have Mesh chunks with no Mesh Subset.  Need to skip these.  They are in the .cga file and contain no geometry.  Just stub info.
                    if (tmpMeshChunk.MeshSubsets != 0)
                    {
                        //Console.WriteLine($"tmpMeshChunk ID is {nodeChunk.ObjectNodeID:X}");
                        //tmpMeshChunk.WriteChunk();
                        //Console.WriteLine($"tmpmeshsubset ID is {tmpMeshChunk.MeshSubsets:X}");
                        var tmpMeshSubsets = (ChunkMeshSubsets)nodeChunk._model.ChunkMap[tmpMeshChunk.MeshSubsets];  // Listed as Object ID for the Node
                        if (tmpMeshChunk.MeshSubsets != 0) tmpMeshSubsets = (ChunkMeshSubsets)nodeChunk._model.ChunkMap[tmpMeshChunk.MeshSubsets];  // Listed as Object ID for the Node
                        // Get pointers to the vertices data
                        if (tmpMeshChunk.VerticesData != 0) tmpVertices = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.VerticesData];
                        if (tmpMeshChunk.NormalsData != 0) tmpNormals = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.NormalsData];
                        if (tmpMeshChunk.UVsData != 0) tmpUVs = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.UVsData];
                        // Star Citizen file.  That means VerticesData and UVsData will probably be empty.  Need to handle both cases.
                        if (tmpMeshChunk.VertsUVsData != 0) tmpVertsUVs = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.VertsUVsData];
                        if (tmpMeshChunk.IndicesData != 0) tmpIndices = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.IndicesData];
                        // Ignore Tangent and Color data for now.
                        if (tmpMeshChunk.ColorsData != 0) tmpColors = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.ColorsData];
                        if (tmpMeshChunk.TangentsData != 0) tmpTangents = (ChunkDataStream)nodeChunk._model.ChunkMap[tmpMeshChunk.TangentsData];
                        if (tmpVertices == null && tmpVertsUVs == null)
                            // There is no vertex data for this node.  Skip.
                            continue;

                        // tmpGeo is a Geometry object for each meshsubset.  Name will be "Nodechunk name_matID".  Hopefully there is only one matID used per submesh
                        var tmpGeo = new Grendgine_Collada_Geometry { Name = nodeChunk.Name, ID = nodeChunk.Name + "-mesh" };
                        var tmpMesh = new Grendgine_Collada_Mesh();
                        tmpGeo.Mesh = tmpMesh;

                        var source = new Grendgine_Collada_Source[3]; // 3 possible source types.
                        // need a collada_source for position, normal, UV, tangents and color, what the source is (verts), and the tri index
                        var posSource = source[0] = new Grendgine_Collada_Source { ID = nodeChunk.Name + "-mesh-pos", Name = nodeChunk.Name + "-pos" };
                        var normSource = source[1] = new Grendgine_Collada_Source { ID = nodeChunk.Name + "-mesh-norm", Name = nodeChunk.Name + "-norm" };
                        var uvSource = source[2] = new Grendgine_Collada_Source { Name = nodeChunk.Name + "-UV", ID = nodeChunk.Name + "-mesh-UV" };
                        //var tangentSource = source[3] = new Grendgine_Collada_Source { Name = nodeChunk.Name + "-tangent", ID = nodeChunk.Name + "-mesh-tangent" };
                        //
                        var posInput = new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.POSITION, source = "#" + posSource.ID };
                        var normInput = new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.NORMAL, source = "#" + normSource.ID };
                        var uvInput = new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.TEXCOORD, source = "#" + uvSource.ID };    // might need to replace TEXCOORD with UV
                        var colorInput = new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.COLOR };
                        //var tangentInput = new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.TANGENT, source = "#" + tangentSource.ID } ;

                        // Create vertices node.  For polylist will just have VERTEX.
                        var vertices = tmpGeo.Mesh.Vertices = new Grendgine_Collada_Vertices { ID = nodeChunk.Name + "-vertices", Input = new[] { posInput, null, null } };

                        // Create a float_array object to store all the data
                        Grendgine_Collada_Float_Array floatArrayVerts, floatArrayNormals, floatArrayUVs; //floatArrayColors, floatArrayTangents;
                        // Strings for vertices
                        StringBuilder vertString = new StringBuilder(), normString = new StringBuilder(), uvString = new StringBuilder(); //tangentString = new StringBuilder()
                        if (tmpVertices != null)  // Will be null if it's using VertsUVs.
                        {
                            floatArrayVerts = new Grendgine_Collada_Float_Array
                            {
                                ID = posSource.ID + "-array",
                                Digits = 6,
                                Magnitude = 38,
                                Count = (int)tmpVertices.NumElements * 3
                            };
                            floatArrayUVs = new Grendgine_Collada_Float_Array
                            {
                                ID = uvSource.ID + "-array",
                                Digits = 6,
                                Magnitude = 38,
                                Count = (int)tmpUVs.NumElements * 2
                            };
                            floatArrayNormals = new Grendgine_Collada_Float_Array
                            {
                                ID = normSource.ID + "-array",
                                Digits = 6,
                                Magnitude = 38
                            };
                            if (tmpNormals != null)
                                floatArrayNormals.Count = (int)tmpNormals.NumElements * 3;
                            // Create Vertices and normals string
                            for (var j = 0U; j < tmpMeshChunk.NumVertices; j++)
                            {
                                var vertex = tmpVertices.Vertices[j];
                                vertString.AppendFormat("{0:F6} {1:F6} {2:F6} ", vertex.x, vertex.y, vertex.z);
                                var normal = tmpNormals?.Normals[j] ?? new Vector3(0.0f, 0.0f, 0.0f);
                                normString.AppendFormat("{0:F6} {1:F6} {2:F6} ", safe(normal.x), safe(normal.y), safe(normal.z));
                            }
                            // Create UV string
                            for (var j = 0U; j < tmpUVs.NumElements; j++)
                                uvString.AppendFormat("{0:F6} {1:F6} ", safe(tmpUVs.UVs[j].U), 1 - safe(tmpUVs.UVs[j].V));
                        }
                        // VertsUV structure.  Pull out verts and UVs from tmpVertsUVs.
                        else
                        {
                            floatArrayVerts = new Grendgine_Collada_Float_Array
                            {
                                ID = posSource.ID + "-array",
                                Digits = 6,
                                Magnitude = 38,
                                Count = (int)tmpVertsUVs.NumElements * 3
                            };
                            floatArrayUVs = new Grendgine_Collada_Float_Array
                            {
                                ID = uvSource.ID + "-array",
                                Digits = 6,
                                Magnitude = 38,
                                Count = (int)tmpVertsUVs.NumElements * 2
                            };
                            floatArrayNormals = new Grendgine_Collada_Float_Array
                            {
                                ID = normSource.ID + "-array",
                                Digits = 6,
                                Magnitude = 38,
                                Count = (int)tmpVertsUVs.NumElements * 3
                            };
                            // Create Vertices and normals string
                            for (var j = 0U; j < tmpMeshChunk.NumVertices; j++)
                            {
                                // Rotate/translate the vertex
                                // Dymek's code to rescale by bounding box.  Only apply to geometry (cga or cgf), and not skin or chr objects.
                                if (!CryFile.InputFile.EndsWith("skin") && !CryFile.InputFile.EndsWith("chr"))
                                {
                                    var multiplerX = Math.Abs(tmpMeshChunk.MinBound.x - tmpMeshChunk.MaxBound.x) / 2f;
                                    var multiplerY = Math.Abs(tmpMeshChunk.MinBound.y - tmpMeshChunk.MaxBound.y) / 2f;
                                    var multiplerZ = Math.Abs(tmpMeshChunk.MinBound.z - tmpMeshChunk.MaxBound.z) / 2f;
                                    if (multiplerX < 1) multiplerX = 1;
                                    if (multiplerY < 1) multiplerY = 1;
                                    if (multiplerZ < 1) multiplerZ = 1;
                                    tmpVertsUVs.Vertices[j].x = tmpVertsUVs.Vertices[j].x * multiplerX + (tmpMeshChunk.MaxBound.x + tmpMeshChunk.MinBound.x) / 2;
                                    tmpVertsUVs.Vertices[j].y = tmpVertsUVs.Vertices[j].y * multiplerY + (tmpMeshChunk.MaxBound.y + tmpMeshChunk.MinBound.y) / 2;
                                    tmpVertsUVs.Vertices[j].z = tmpVertsUVs.Vertices[j].z * multiplerZ + (tmpMeshChunk.MaxBound.z + tmpMeshChunk.MinBound.z) / 2;
                                }

                                var vertex = tmpVertsUVs.Vertices[j];
                                vertString.AppendFormat("{0:F6} {1:F6} {2:F6} ", vertex.x, vertex.y, vertex.z);
                                var normal = new Vector3();
                                // Normals depend on the data size.  16 byte structures have the normals in the Tangents.  20 byte structures are in the VertsUV.
                                if (tmpVertsUVs.BytesPerElement == 20)
                                    normal = tmpVertsUVs.Normals[j];
                                else
                                {
                                    //normal = tmpTangents.Normals[j];
                                    //normal.x = normal.x / 32767.0;
                                    //normal.y = normal.y / 32767.0;
                                    //normal.z = normal.z / 32767.0;                                    
                                    normal = tmpVertsUVs.Normals[j];
                                }
                                normString.AppendFormat("{0:F6} {1:F6} {2:F6} ", safe(normal.x), safe(normal.y), safe(normal.z));
                            }
                            // Create UV string
                            for (var j = 0U; j < tmpVertsUVs.NumElements; j++)
                                uvString.AppendFormat("{0:F6} {1:F6} ", safe(tmpVertsUVs.UVs[j].U), safe(1 - tmpVertsUVs.UVs[j].V));
                        }
                        CleanNumbers(vertString);
                        CleanNumbers(normString);
                        CleanNumbers(uvString);

                        //floatArrayNormals = new Grendgine_Collada_Float_Array
                        //{
                        //    ID = tangentSource.ID + "-array",
                        //    Digits = 6,
                        //    Magnitude = 38,
                        //    Count = (int)tmpTangents.NumElements * 2,
                        //};
                        //var tangentString = new StringBuilder();
                        // Create Tangent string
                        //for (var j = 0U; j < tmpTangents.NumElements; j++)
                        //    tangentString.AppendFormat("{0:F6} {1:F6} {2:F6} {3:F6} {4:F6} {5:F6} {6:F6} {7:F6} ", 
                        //        tmpTangents.Tangents[j, 0].w / 32767, tmpTangents.Tangents[j, 0].x / 32767, tmpTangents.Tangents[j, 0].y / 32767, tmpTangents.Tangents[j, 0].z / 32767,
                        //        tmpTangents.Tangents[j, 1].w / 32767, tmpTangents.Tangents[j, 1].x / 32767, tmpTangents.Tangents[j, 1].y / 32767, tmpTangents.Tangents[j, 1].z / 32767);
                        //CleanNumbers(tangentString);

                        #region Create the polylist node.

                        var polylists = tmpGeo.Mesh.Polylist = new Grendgine_Collada_Polylist[tmpMeshSubsets.NumMeshSubset];
                        StringBuilder b0 = new StringBuilder(), b1 = new StringBuilder();
                        for (var j = 0U; j < tmpMeshSubsets.NumMeshSubset; j++) // Need to make a new Polylist entry for each submesh.
                        {
                            b0.Length = 0; b1.Length = 0;
                            // Create the vcount list.  All triangles, so the subset number of indices.
                            for (var k = tmpMeshSubsets.MeshSubsets[j].FirstIndex; k < (tmpMeshSubsets.MeshSubsets[j].FirstIndex + tmpMeshSubsets.MeshSubsets[j].NumIndices); k++)
                            {
                                b0.AppendFormat("3 ");
                                k += 2;
                            }
                            // Create the P node for the Polylist.
                            for (var k = tmpMeshSubsets.MeshSubsets[j].FirstIndex; k < (tmpMeshSubsets.MeshSubsets[j].FirstIndex + tmpMeshSubsets.MeshSubsets[j].NumIndices); k++)
                            {
                                b1.AppendFormat("{0} {0} {0} {1} {1} {1} {2} {2} {2} ", tmpIndices.Indices[k], tmpIndices.Indices[k + 1], tmpIndices.Indices[k + 2]);
                                k += 2;
                            }
                            polylists[j] = new Grendgine_Collada_Polylist
                            {
                                Count = (int)tmpMeshSubsets.MeshSubsets[j].NumIndices / 3,
                                Input = new[] { // Create the 4 inputs.  vertex, normal, texcoord, tangent
                                    new Grendgine_Collada_Input_Shared { Semantic = Grendgine_Collada_Input_Semantic.VERTEX, Offset = 0, source = "#" + vertices.ID },
                                    new Grendgine_Collada_Input_Shared { Semantic = Grendgine_Collada_Input_Semantic.NORMAL, Offset = 1, source = "#" + normSource.ID },
                                    new Grendgine_Collada_Input_Shared { Semantic = Grendgine_Collada_Input_Semantic.TEXCOORD, Offset = 2, source = "#" + uvSource.ID }
                                    //new Grendgine_Collada_Input_Shared { Semantic = Grendgine_Collada_Input_Semantic.TANGENT, Offset = 3, source = "#" + tangentSource.ID }
                                },
                                VCount = new Grendgine_Collada_Int_Array_String { Value_As_String = b0.ToString().TrimEnd() },
                                P = new Grendgine_Collada_Int_Array_String { Value_As_String = b1.ToString().TrimEnd() },
                                Material = CryFile.Materials.Count() != 0 ? CryFile.Materials[tmpMeshSubsets.MeshSubsets[j].MatID].Name + "-material" : null
                            };
                        }

                        #endregion

                        #region Create the source float_array nodes.  Vertex, normal, UV.  May need color as well.

                        floatArrayVerts.Value_As_String = vertString.ToString().TrimEnd();
                        floatArrayNormals.Value_As_String = normString.ToString().TrimEnd();
                        floatArrayUVs.Value_As_String = uvString.ToString().TrimEnd();
                        //floatArrayColors.Value_As_String = colorString.ToString();
                        //floatArrayTangents.Value_As_String = tangentString.ToString().TrimEnd();
                        //
                        source[0].Float_Array = floatArrayVerts;
                        source[1].Float_Array = floatArrayNormals;
                        source[2].Float_Array = floatArrayUVs;
                        //source[3].Float_Array = floatArrayColors;
                        //source[3].Float_Array = floatArrayTangents;
                        tmpGeo.Mesh.Source = source;

                        // create the technique_common for each of these
                        posSource.Technique_Common = new Grendgine_Collada_Technique_Common_Source
                        {
                            Accessor = new Grendgine_Collada_Accessor
                            {
                                Source = "#" + floatArrayVerts.ID,
                                Stride = 3,
                                Count = (uint)tmpMeshChunk.NumVertices,
                                Param = new[] {
                                    new Grendgine_Collada_Param { Name = "X", Type = "float" },
                                    new Grendgine_Collada_Param { Name = "Y", Type = "float" },
                                    new Grendgine_Collada_Param { Name = "Z", Type = "float" }
                                }
                            }
                        };
                        normSource.Technique_Common = new Grendgine_Collada_Technique_Common_Source
                        {
                            Accessor = new Grendgine_Collada_Accessor
                            {
                                Source = "#" + floatArrayNormals.ID,
                                Stride = 3,
                                Count = (uint)tmpMeshChunk.NumVertices,
                                Param = new[] {
                                    new Grendgine_Collada_Param { Name = "X", Type = "float" },
                                    new Grendgine_Collada_Param { Name = "Y", Type = "float" },
                                    new Grendgine_Collada_Param { Name = "Z", Type = "float" }
                                }
                            }
                        };
                        uvSource.Technique_Common = new Grendgine_Collada_Technique_Common_Source
                        {
                            Accessor = new Grendgine_Collada_Accessor
                            {
                                Source = "#" + floatArrayUVs.ID,
                                Stride = 2,
                                Count = tmpVertices != null ? tmpUVs.NumElements : tmpVertsUVs.NumElements,
                                Param = new[] {
                                    new Grendgine_Collada_Param { Name = "S", Type = "float" },
                                    new Grendgine_Collada_Param { Name = "T", Type = "float" }
                                }
                            }
                        };
                        //tangentSource.Technique_Common = new Grendgine_Collada_Technique_Common_Source
                        //{
                        //    Accessor = new Grendgine_Collada_Accessor
                        //    {
                        //        Source = "#" + floatArrayTangents.ID,
                        //        Stride = 8,
                        //        Count = tmpTangents.NumElements
                        //    }
                        //};
                        //if (tmpColors != null)
                        //    colorSource.Technique_Common = new Grendgine_Collada_Technique_Common_Source
                        //    {
                        //        Accessor = new Grendgine_Collada_Accessor
                        //        {
                        //            Source = "#" + floatArrayColors.ID,
                        //            Stride = 3,
                        //            Count = tmpColors.NumElements,
                        //            Param = new[] {
                        //              new Grendgine_Collada_Param { Name = "R", Type = "float"},
                        //              new Grendgine_Collada_Param { Name = "G", Type = "float"},
                        //              new Grendgine_Collada_Param { Name = "B", Type = "float"}},
                        //        }
                        //    };
                        geometryList.Add(tmpGeo);

                        #endregion
                    }
                }
                // There is no geometry for a helper or controller node.  Can skip the rest.
            }

            daeObject.Library_Geometries = new Grendgine_Collada_Library_Geometries
            {
                Geometry = geometryList.ToArray()
            };
        }

        public void WriteLibrary_Controllers()
        {
            // Create the skin object and assign to the controller
            var skin = new Grendgine_Collada_Skin
            {
                source = "#" + daeObject.Library_Geometries.Geometry[0].ID,
                Bind_Shape_Matrix = new Grendgine_Collada_Float_Array_String
                {
                    Value_As_String = CreateStringFromMatrix44(Matrix4x4.Identity()) // We will assume the BSM is the identity matrix for now
                },
                // Create the 3 sources for this controller:  joints, bind poses, and weights
                Source = new Grendgine_Collada_Source[3],
            };

            // Populate the data.
            // Need to map the exterior vertices (geometry) to the int vertices.  Or use the Bone Map datastream if it exists (check HasBoneMapDatastream).

            // JOINTS SOURCE
            var boneNames = new StringBuilder();
            for (var i = 0; i < CryFile.SkinningInfo.CompiledBones.Count; i++)
                boneNames.Append(CryFile.SkinningInfo.CompiledBones[i].boneName.Replace(' ', '_') + " ");
            skin.Source[0] = new Grendgine_Collada_Source
            {
                ID = "Controller-joints",
                Name_Array = new Grendgine_Collada_Name_Array()
                {
                    ID = "Controller-joints-array",
                    Count = CryFile.SkinningInfo.CompiledBones.Count,
                    Value_Pre_Parse = boneNames.ToString().TrimEnd()
                },
                Technique_Common = new Grendgine_Collada_Technique_Common_Source
                {
                    Accessor = new Grendgine_Collada_Accessor
                    {
                        Source = "#Controller-joints-array",
                        Count = (uint)CryFile.SkinningInfo.CompiledBones.Count,
                        Stride = 1
                    }
                }
            };

            // BIND POSE SOURCE
            skin.Source[1] = new Grendgine_Collada_Source
            {
                ID = "Controller-bind_poses",
                Float_Array = new Grendgine_Collada_Float_Array
                {
                    ID = "Controller-bind_poses-array",
                    Count = CryFile.SkinningInfo.CompiledBones.Count * 16,
                    Value_As_String = GetBindPoseArray(CryFile.SkinningInfo.CompiledBones)
                },
                Technique_Common = new Grendgine_Collada_Technique_Common_Source
                {
                    Accessor = new Grendgine_Collada_Accessor
                    {
                        Source = "#Controller-bind_poses-array",
                        Count = (uint)CryFile.SkinningInfo.CompiledBones.Count,
                        Stride = 16,
                        Param = new[] { new Grendgine_Collada_Param { Name = "TRANSFORM", Type = "float4x4" } }
                    }
                }
            };

            // WEIGHTS SOURCE
            var weights = new StringBuilder();
            var weightsCount = CryFile.SkinningInfo.IntVertices == null ? CryFile.SkinningInfo.BoneMapping.Count : CryFile.SkinningInfo.Ext2IntMap.Count;
            // This is a case where there are bones, and only Bone Mapping data from a datastream chunk.  Skin files.
            if (CryFile.SkinningInfo.IntVertices == null)
                for (var i = 0; i < CryFile.SkinningInfo.BoneMapping.Count; i++)
                    for (var j = 0; j < 4; j++)
                        weights.Append(((float)CryFile.SkinningInfo.BoneMapping[i].Weight[j] / 255).ToString() + " ");
            // Bones and int verts.  Will use int verts for weights, but this doesn't seem perfect either.
            else
                for (var i = 0; i < CryFile.SkinningInfo.Ext2IntMap.Count; i++)
                    for (var j = 0; j < 4; j++)
                        weights.Append(CryFile.SkinningInfo.IntVertices[CryFile.SkinningInfo.Ext2IntMap[i]].Weights[j] + " ");
            CleanNumbers(weights);
            skin.Source[2] = new Grendgine_Collada_Source()
            {
                ID = "Controller-weights",
                Float_Array = new Grendgine_Collada_Float_Array()
                {
                    ID = "Controller-weights-array",
                    Count = weightsCount,
                    Value_As_String = weights.ToString().TrimEnd(),
                },
                Technique_Common = new Grendgine_Collada_Technique_Common_Source
                {
                    Accessor = new Grendgine_Collada_Accessor
                    {
                        Source = "#Controller-weights-array",
                        Count = (uint)weightsCount * 4,
                        Stride = 1,
                        Param = new[] { new Grendgine_Collada_Param { Name = "WEIGHT", Type = "float" } }
                    }
                }
            };

            // JOINTS
            skin.Joints = new Grendgine_Collada_Joints
            {
                Input = new[] {
                    new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.JOINT, source = "#Controller-joints" },
                    new Grendgine_Collada_Input_Unshared { Semantic = Grendgine_Collada_Input_Semantic.INV_BIND_MATRIX, source = "#Controller-bind_poses" }
                }
            };

            // VERTEX WEIGHTS
            var vCount = new StringBuilder();
            for (var i = 0; i < CryFile.SkinningInfo.BoneMapping.Count; i++)
                vCount.Append("4 ");
            var vertices = new StringBuilder();
            var idx = 0;
            if (!CryFile.Models[0].SkinningInfo.HasIntToExtMapping)
                for (var i = 0; i < CryFile.SkinningInfo.BoneMapping.Count; i++)
                {
                    var wholePart = i / 4;
                    vertices.Append(CryFile.SkinningInfo.BoneMapping[i].BoneIndex[0] + " " + idx + " ");
                    vertices.Append(CryFile.SkinningInfo.BoneMapping[i].BoneIndex[1] + " " + (idx + 1) + " ");
                    vertices.Append(CryFile.SkinningInfo.BoneMapping[i].BoneIndex[2] + " " + (idx + 2) + " ");
                    vertices.Append(CryFile.SkinningInfo.BoneMapping[i].BoneIndex[3] + " " + (idx + 3) + " ");
                    idx = idx + 4;
                }
            else
                for (var i = 0; i < CryFile.SkinningInfo.Ext2IntMap.Count; i++)
                {
                    var wholePart = i / 4;
                    vertices.Append(CryFile.SkinningInfo.IntVertices[CryFile.SkinningInfo.Ext2IntMap[i]].BoneIDs[0] + " " + idx + " ");
                    vertices.Append(CryFile.SkinningInfo.IntVertices[CryFile.SkinningInfo.Ext2IntMap[i]].BoneIDs[1] + " " + (idx + 1) + " ");
                    vertices.Append(CryFile.SkinningInfo.IntVertices[CryFile.SkinningInfo.Ext2IntMap[i]].BoneIDs[2] + " " + (idx + 2) + " ");
                    vertices.Append(CryFile.SkinningInfo.IntVertices[CryFile.SkinningInfo.Ext2IntMap[i]].BoneIDs[3] + " " + (idx + 3) + " ");
                    idx = idx + 4;
                }
            skin.Vertex_Weights = new Grendgine_Collada_Vertex_Weights
            {
                Count = CryFile.SkinningInfo.BoneMapping.Count,
                VCount = new Grendgine_Collada_Int_Array_String
                {
                    Value_As_String = vCount.ToString().TrimEnd()
                },
                V = new Grendgine_Collada_Int_Array_String
                {
                    Value_As_String = vertices.ToString().TrimEnd()
                },
                Input = new[] {
                    new Grendgine_Collada_Input_Shared
                    {
                        Semantic = Grendgine_Collada_Input_Semantic.JOINT,
                        source = "#Controller-joints",
                        Offset = 0
                    },
                    new Grendgine_Collada_Input_Shared
                    {
                        Semantic = Grendgine_Collada_Input_Semantic.WEIGHT,
                        source = "#Controller-weights",
                        Offset = 1
                    }
                }
            };

            // FINISHED

            // Set up the controller library.
            daeObject.Library_Controllers = new Grendgine_Collada_Library_Controllers
            {
                // There can be multiple controllers in the controller library.  But for Cryengine files, there is only one rig.
                // So if a rig exists, make that the controller.  This applies mostly to .chr files, which will have a rig and geometry.
                Controller = new[] { new Grendgine_Collada_Controller { // just need the one.
                    ID = "Controller",
                    // create the extra element for the FCOLLADA profile
                    Extra = new[] { new Grendgine_Collada_Extra {
                        Technique = new[] { new Grendgine_Collada_Technique {
                            profile = "FCOLLADA",
                            UserProperties = "SkinController"
                        }}
                    }},
                    // Add the parts to their parents
                    Skin = skin
                }}
            };
        }

        /// <summary>
        /// Provides a library in which to place visual_scene elements. 
        /// </summary>
        public void WriteLibrary_VisualScenes()
        {
            // There can be multiple visual scenes.  Will just have one (World) for now.  All node chunks go under Nodes for that visual scene
            var nodes = new List<Grendgine_Collada_Node>();

            // Check to see if there is a CompiledBones chunk.  If so, add a Node.
            if (CryFile.Chunks.Any(a => a.ChunkType == ChunkTypeEnum.CompiledBones || a.ChunkType == ChunkTypeEnum.CompiledBonesSC))
                nodes.Add(CreateJointNode(CryFile.Bones.RootBone));

            // Geometry visual Scene.
            if (CryFile.Models.Count > 1) // Star Citizen model with .cga/.cgam pair.
            {
                // First model file (.cga or .cgf) will contain the main Root Node, along with all non geometry Node chunks (placeholders).
                // Second one will have all the datastreams, but needs to be tied to the RootNode of the first model.
                // THERE CAN BE MULTIPLE ROOT NODES IN EACH FILE!  Check to see if the parentnodeid ~0 and be sure to add a node for it.
                var positionNodes = new List<Grendgine_Collada_Node>();        // For SC files, these are the nodes in the .cga/.cgf files.
                foreach (var root in CryFile.Models[0].NodeMap.Values.Where(a => a.ParentNodeID == ~0).ToList())
                    positionNodes.Add(CreateNode(root));
                nodes.AddRange(positionNodes.ToArray());
            }
            else nodes.Add(CreateNode(CryFile.RootNode));

            // Set up the library
            daeObject.Library_Visual_Scene = new Grendgine_Collada_Library_Visual_Scenes
            {
                Visual_Scene = new[] { new Grendgine_Collada_Visual_Scene {
                    Node = nodes.ToArray(),
                    ID = "Scene"
                } }
            };
        }

        /// <summary>
        /// Provides a library in which to place visual_scene elements for chr files (rigs + geometry). 
        /// </summary>
        public void WriteLibrary_VisualScenesWithSkeleton()
        {
            // There can be multiple visual scenes.  Will just have one (World) for now.  All node chunks go under Nodes for that visual scene
            var nodes = new List<Grendgine_Collada_Node>();

            // Check to see if there is a CompiledBones chunk.  If so, add a Node.  
            if (CryFile.Chunks.Any(a => a.ChunkType == ChunkTypeEnum.CompiledBones || a.ChunkType == ChunkTypeEnum.CompiledBonesSC))
                nodes.Add(CreateJointNode(CryFile.Bones.RootBone));

            // This gets complicated.  We need to make one instance_material for each material used in this node chunk.  The mat IDs used in this
            // node chunk are stored in meshsubsets, so for each subset we need to grab the mat, get the target (id), and make an instance_material for it.
            var instanceMaterials = new List<Grendgine_Collada_Instance_Material_Geometry>();
            for (var i = 0; i < CryFile.Materials.Length; i++)
                // For each mesh subset, we want to create an instance material and add it to instanceMaterials list.
                instanceMaterials.Add(new Grendgine_Collada_Instance_Material_Geometry
                {
                    Target = "#" + CryFile.Materials[i].Name + "-material",
                    Symbol = CryFile.Materials[i].Name + "-material"
                });

            // Geometry visual Scene.
            nodes.Add(new Grendgine_Collada_Node
            {
                ID = CryFile.Models[0].FileName,
                Name = CryFile.Models[0].FileName,
                Type = Grendgine_Collada_Node_Type.NODE,
                Matrix = new[] { new Grendgine_Collada_Matrix
                {
                    Value_As_String = CreateStringFromMatrix44(Matrix4x4.Identity())
                }},
                Instance_Controller = new[] { new Grendgine_Collada_Instance_Controller {
                    URL = "#Controller",
                    Skeleton = new []{ new Grendgine_Collada_Skeleton { Value = "#Armature" } },
                    Bind_Material = new [] { new Grendgine_Collada_Bind_Material {
                        Technique_Common = new Grendgine_Collada_Technique_Common_Bind_Material {
                            Instance_Material = instanceMaterials.ToArray()
                        }
                    }},
                }}
            });

            // Set up the library
            daeObject.Library_Visual_Scene = new Grendgine_Collada_Library_Visual_Scenes
            {
                Visual_Scene = new[] { new Grendgine_Collada_Visual_Scene { Node = nodes.ToArray(), ID = "Scene" } }
            };
        }

        Grendgine_Collada_Node CreateNode(ChunkNode nodeChunk)
        {
            // This will be used recursively to create a node object and return it to WriteLibrary_VisualScenes
            Grendgine_Collada_Node tmpNode;
            // Check to see if there is a second model file, and if the mesh chunk is actually there.
            if (CryFile.Models.Count > 1)
            {
                // Star Citizen pair.  Get the Node and Mesh chunks from the geometry file, unless it's a Stream node.
                var nodeName = nodeChunk.Name;
                var nodeID = nodeChunk.ID;
                // make sure there is a geometry node in the geometry file
                if (CryFile.Models[1].NodeMap.ContainsKey(nodeID))
                {
                    var geometryNode = CryFile.Models[1].NodeMap.Values.Where(a => a.Name == nodeChunk.Name).First();
                    var geometryMesh = (ChunkMesh)CryFile.Models[1].ChunkMap[geometryNode.ObjectNodeID];
                    tmpNode = CreateGeometryNode(geometryNode, geometryMesh);
                }
                else tmpNode = CreateSimpleNode(nodeChunk);
            }
            else
            {
                // Regular Cryengine file.
                if (nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID].ChunkType == ChunkTypeEnum.Mesh)
                {
                    var tmpMeshChunk = (ChunkMesh)nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID];
                    // Can have a node with a mesh and meshsubset, but no vertices.  Write as simple node.
                    tmpNode = tmpMeshChunk.MeshSubsets == 0 || tmpMeshChunk.NumVertices == 0
                        ? CreateSimpleNode(nodeChunk)
                        : nodeChunk._model.ChunkMap[tmpMeshChunk.MeshSubsets].ID != 0
                            ? CreateGeometryNode(nodeChunk, (ChunkMesh)nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID])
                            : CreateSimpleNode(nodeChunk);
                }
                else tmpNode = CreateSimpleNode(nodeChunk);
            }
            // Add childnodes
            tmpNode.node = CreateChildNodes(nodeChunk);
            return tmpNode;
        }

        /// <summary>
        /// This will be used to make the Collada node element for Node chunks that point to Helper Chunks and MeshPhysics
        /// </summary>
        /// <param name="nodeChunk">The node chunk for this Collada Node.</param>
        /// <returns>Grendgine_Collada_Node for the node chunk</returns>
        Grendgine_Collada_Node CreateSimpleNode(ChunkNode nodeChunk)
        {
            var matrixString = new StringBuilder();
            CalculateTransform(nodeChunk);
            matrixString.AppendFormat("{0:F6} {1:F6} {2:F6} {3:F6} {4:F6} {5:F6} {6:F6} {7:F6} {8:F6} {9:F6} {10:F6} {11:F6} {12:F6} {13:F6} {14:F6} {15:F6}",
                nodeChunk.LocalTransform.m00, nodeChunk.LocalTransform.m01, nodeChunk.LocalTransform.m02, nodeChunk.LocalTransform.m03,
                nodeChunk.LocalTransform.m10, nodeChunk.LocalTransform.m11, nodeChunk.LocalTransform.m12, nodeChunk.LocalTransform.m13,
                nodeChunk.LocalTransform.m20, nodeChunk.LocalTransform.m21, nodeChunk.LocalTransform.m22, nodeChunk.LocalTransform.m23,
                nodeChunk.LocalTransform.m30, nodeChunk.LocalTransform.m31, nodeChunk.LocalTransform.m32, nodeChunk.LocalTransform.m33);
            // This will be used to make the Collada node element for Node chunks that point to Helper Chunks and MeshPhysics
            return new Grendgine_Collada_Node
            {
                Type = Grendgine_Collada_Node_Type.NODE,
                Name = nodeChunk.Name,
                ID = nodeChunk.Name,
                // we can have multiple matrices, but only need one since there is only one per Node chunk anyway
                Matrix = new[] { new Grendgine_Collada_Matrix { Value_As_String = matrixString.ToString(), sID = "transform" } },
                // Add childnodes
                node = CreateChildNodes(nodeChunk),
            };
        }

        /// <summary>
        /// Used by CreateNode and CreateSimpleNodes to create all the child nodes for the given node.
        /// </summary>
        /// <param name="nodeChunk">Node with children to add.</param>
        /// <returns>A node with all the children added.</returns>
        Grendgine_Collada_Node[] CreateChildNodes(ChunkNode nodeChunk)
        {
            if (nodeChunk.__NumChildren != 0)
            {
                var childNodes = new List<Grendgine_Collada_Node>();
                foreach (var childNodeChunk in nodeChunk.AllChildNodes.ToList())
                    childNodes.Add(CreateNode(childNodeChunk));
                return childNodes.ToArray();
            }
            return null;
        }

        Grendgine_Collada_Node CreateJointNode(CompiledBone bone)
        {
            // Populate the matrix.  This is based on the BONETOWORLD data in this bone.
            var matrixValues = new StringBuilder();
            matrixValues.AppendFormat("{0:F6} {1:F6} {2:F6} {3:F6} {4:F6} {5:F6} {6:F6} {7:F6} {8:F6} {9:F6} {10:F6} {11:F6} 0 0 0 1",
                bone.LocalTransform.m00,
                bone.LocalTransform.m01,
                bone.LocalTransform.m02,
                bone.LocalTransform.m03,
                bone.LocalTransform.m10,
                bone.LocalTransform.m11,
                bone.LocalTransform.m12,
                bone.LocalTransform.m13,
                bone.LocalTransform.m20,
                bone.LocalTransform.m21,
                bone.LocalTransform.m22,
                bone.LocalTransform.m23);
            CleanNumbers(matrixValues);

            // This will be used recursively to create a node object and return it to WriteLibrary_VisualScenes
            // If this is the root bone, set the node id to Armature.  Otherwise set to armature_<bonename>
            var tmpNode = new Grendgine_Collada_Node
            {
                ID = bone.parentID != 0 ? "Armature_" + bone.boneName.Replace(' ', '_') : "Armature",
                Name = bone.boneName.Replace(' ', '_'),
                sID = bone.boneName.Replace(' ', '_'),
                Type = Grendgine_Collada_Node_Type.JOINT,
                // we can have multiple matrices, but only need one since there is only one per Node chunk anyway
                Matrix = new[] { new Grendgine_Collada_Matrix { Value_As_String = matrixValues.ToString() } }
            };

            // Recursively call this for each of the child bones to this bone.
            if (bone.numChildren > 0)
            {
                var idx = 0;
                var childNodes = new Grendgine_Collada_Node[bone.numChildren];
                foreach (var childBone in CryFile.Bones.GetAllChildBones(bone))
                    childNodes[idx++] = CreateJointNode(childBone);
                tmpNode.node = childNodes;
            }
            return tmpNode;
        }

        Grendgine_Collada_Node CreateGeometryNode(ChunkNode nodeChunk, ChunkMesh tmpMeshChunk)
        {
            var matrixString = new StringBuilder();
            // matrixString might have to be an identity matrix, since GetTransform is applying the transform to all the vertices.
            // Use same principle as CreateJointNode.  The Transform matrix (Matrix44) is the world transform matrix.
            CalculateTransform(nodeChunk);
            matrixString.AppendFormat("{0:F6} {1:F6} {2:F6} {3:F6} {4:F6} {5:F6} {6:F6} {7:F6} {8:F6} {9:F6} {10:F6} {11:F6} {12:F6} {13:F6} {14:F6} {15:F6}",
                nodeChunk.LocalTransform.m00, nodeChunk.LocalTransform.m01, nodeChunk.LocalTransform.m02, nodeChunk.LocalTransform.m03,
                nodeChunk.LocalTransform.m10, nodeChunk.LocalTransform.m11, nodeChunk.LocalTransform.m12, nodeChunk.LocalTransform.m13,
                nodeChunk.LocalTransform.m20, nodeChunk.LocalTransform.m21, nodeChunk.LocalTransform.m22, nodeChunk.LocalTransform.m23,
                nodeChunk.LocalTransform.m30, nodeChunk.LocalTransform.m31, nodeChunk.LocalTransform.m32, nodeChunk.LocalTransform.m33);

            // This gets complicated.  We need to make one instance_material for each material used in this node chunk.  The mat IDs used in this
            // node chunk are stored in meshsubsets, so for each subset we need to grab the mat, get the target (id), and make an instance_material for it.
            var instanceMaterials = new List<Grendgine_Collada_Instance_Material_Geometry>();
            var tmpMeshSubsets = (ChunkMeshSubsets)nodeChunk._model.ChunkMap[tmpMeshChunk.MeshSubsets];  // Listed as Object ID for the Node
            for (var i = 0; i < tmpMeshSubsets.NumMeshSubset; i++)
                // For each mesh subset, we want to create an instance material and add it to instanceMaterials list.
                if (CryFile.Materials.Count() > 0)
                    instanceMaterials.Add(new Grendgine_Collada_Instance_Material_Geometry
                    {
                        Target = "#" + CryFile.Materials[tmpMeshSubsets.MeshSubsets[i].MatID].Name + "-material",
                        Symbol = CryFile.Materials[tmpMeshSubsets.MeshSubsets[i].MatID].Name + "-material"
                    });

            // we can have multiple matrices, but only need one since there is only one per Node chunk anyway
            return new Grendgine_Collada_Node
            {
                Type = Grendgine_Collada_Node_Type.NODE,
                Name = nodeChunk.Name,
                ID = nodeChunk.Name,
                Matrix = new[] { new Grendgine_Collada_Matrix { Value_As_String = matrixString.ToString(), sID = "transform" } },
                // Each node will have one instance geometry, although it could be a list.
                Instance_Geometry = new[] { new Grendgine_Collada_Instance_Geometry {
                    Name = nodeChunk.Name,
                    URL = "#" + nodeChunk.Name + "-mesh",  // this is the ID of the geometry.
                    Bind_Material = new[]{new Grendgine_Collada_Bind_Material {
                        Technique_Common = new Grendgine_Collada_Technique_Common_Bind_Material {
                            Instance_Material = instanceMaterials.ToArray()
                        }
                    }}
                }}
            };
        }

        /// <summary>
        /// Creates the Collada Source element for a given datastream).
        /// </summary>
        /// <param name="vertices">The vertices of the source datastream.  This can be position, normals, colors, tangents, etc.</param>
        /// <param name="nodeChunk">The Node chunk of the datastream.  Need this for names, mesh, and submesh info.</param>
        /// <returns>Grendgine_Collada_Source object with the source data.</returns>
        Grendgine_Collada_Source GetMeshSource(ChunkDataStream vertices, ChunkNode nodeChunk) => new Grendgine_Collada_Source
        {
            ID = nodeChunk.Name + "-mesh-pos",
            Name = nodeChunk.Name + "-pos"
        };

        /// <summary>
        /// Retrieves the worldtobone (bind pose matrix) for the bone.
        /// </summary>
        /// <param name="compiledBones">List of bones to get the BPM from.</param>
        /// <returns>The float_array that represents the BPM of all the bones, in order.</returns>
        string GetBindPoseArray(List<CompiledBone> compiledBones)
        {
            var b = new StringBuilder();
            for (var i = 0; i < compiledBones.Count; i++)
                b.Append(CreateStringFromMatrix44(compiledBones[i].worldToBone.GetMatrix44()) + " ");
            return b.ToString().TrimEnd();
        }

        string GetRootBoneName(ChunkCompiledBones bones) => bones.RootBone.boneName;

        /// <summary>
        /// Adds the scene element to the Collada document.
        /// </summary>
        void WriteScene() =>
            daeObject.Scene = new Grendgine_Collada_Scene
            {
                Visual_Scene = new Grendgine_Collada_Instance_Visual_Scene
                {
                    URL = "#Scene",
                    Name = "Scene"
                }
            };

        void CalculateTransform(ChunkNode node)
        {
            var localTranslation = node.Transform.GetScale();
            var localRotation = node.Transform.GetRotation();
            var localScale = node.Transform.GetTranslation();
            node.LocalTranslation = localTranslation;
            node.LocalScale = localScale;
            node.LocalRotation = localRotation;
            node.LocalTransform = node.LocalTransform.GetTransformFromParts(localScale, localRotation, localTranslation);
        }

        #region XML Validation

        void ValidateXml()  // For testing
        {
            try
            {
                var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
                settings.Schemas.Add(null, @".\COLLADA_1_5.xsd");
                var r = XmlReader.Create(daeOutputFile.FullName, settings);
                var doc = new XmlDocument();
                doc.Load(r);
                var eventHandler = new ValidationEventHandler(ValidationEventHandler);
                Console.WriteLine("Validating Schema...");
                // the following call to Validate succeeds.
                doc.Validate(eventHandler);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error: Console.WriteLine("Error: {0}", e.Message); break;
                case XmlSeverityType.Warning: Console.WriteLine("Warning {0}", e.Message); break;
            }
        }

        /// <summary>
        /// This method will check all the URLs used in the Collada object and see if any reference IDs that don't exist.  It will
        /// also check for duplicate IDs
        /// </summary>
        void ValidateDoc()  // NYI
        {
            // Check for duplicate IDs.  Populate the idList with all the IDs.
            var root = XElement.Load(daeOutputFile.FullName);
            var nodes = root.Descendants();
            foreach (var node in nodes)
                if (node.HasAttributes)
                    foreach (var attrib in nodes.Where(a => a.Name.Equals("adder_a_cockpit_standard-mesh-pos")))
                        Console.WriteLine("attrib: {0} == {1}", attrib.Name, attrib.Value);
            // Create a list of URLs and see if any reference an ID that doesn't exist.
        }

        #endregion

        string CreateStringFromMatrix44(Matrix4x4 matrix)
        {
            var b = new StringBuilder();
            b.AppendFormat("{0:F6} {1:F6} {2:F6} {3:F6} {4:F6} {5:F6} {6:F6} {7:F6} {8:F6} {9:F6} {10:F6} {11:F6} {12:F6} {13:F6} {14:F6} {15:F6}",
                matrix.m00,
                matrix.m01,
                matrix.m02,
                matrix.m03,
                matrix.m10,
                matrix.m11,
                matrix.m12,
                matrix.m13,
                matrix.m20,
                matrix.m21,
                matrix.m22,
                matrix.m23,
                matrix.m30,
                matrix.m31,
                matrix.m32,
                matrix.m33);
            CleanNumbers(b);
            return b.ToString();
        }

        /// <summary>Takes the Material file name and returns just the file name with no extension</summary>
        string CleanMtlFileName(string cleanMe)
        {
            var stringSeparators = new string[] { @"\", @"/" };
            string[] f;
            if (cleanMe.Contains(@"/") || cleanMe.Contains(@"\"))
            {
                // Take out path info
                f = cleanMe.Split(stringSeparators, StringSplitOptions.None);
                cleanMe = f[f.Length - 1];
            }
            // Check to see if extension is added, and if so strip it out. Look for "."
            if (cleanMe.Contains(@"."))
            {
                var periodSep = new string[] { @"." };
                f = cleanMe.Split(periodSep, StringSplitOptions.None);
                cleanMe = f[0];
                //Console.WriteLine("Cleanme is {0}", cleanMe);
            }
            return cleanMe;
        }

        double safe(double value) =>
            value == double.NegativeInfinity
                ? double.MinValue
                : value == double.PositiveInfinity ? double.MaxValue : value == double.NaN ? 0 : value;

        void CleanNumbers(StringBuilder b) =>
            b.Replace("0.000000", "0")
            .Replace("-0.000000", "0")
            .Replace("1.000000", "1")
            .Replace("-1.000000", "-1");
    }
}