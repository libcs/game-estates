using Game.Core;
using Game.Format.Cry.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Game.Core.CoreDebug;
using UVector3 = UnityEngine.Vector3;

namespace Game.Format.Cry
{
    public class CryObjectBuilder
    {
        const bool KinematicRigidbodies = true;
        const bool SkipShieldNodes = false;
        const bool SkipStreamNodes = false;

        readonly CryFile _file;
        readonly MaterialManager _materialManager;
        readonly int _markerLayer;
        Dictionary<(int, uint), (Mesh, MaterialProps)> _geometries = new Dictionary<(int, uint), (Mesh, MaterialProps)>();

        public CryObjectBuilder(CryFile file, MaterialManager materialManager, int markerLayer)
        {
            _file = file;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
            SetGeometries();
        }

        public GameObject BuildObject()
        {
            //Log($"Number of models: {_file.Models.Count}");
            //for (var i = 0; i < _file.Models.Count; i++)
            //    Log($"- Number of nodes in model: {_file.Models[i].NodeMap.Count}");
            var nodes = new List<GameObject>();

            // If there is Skinning info, create the controller library
            if (_file.SkinningInfo.HasSkinningInfo)
                AddControllers();

            // Check to see if there is a CompiledBones chunk.  If so, add a Node.
            if (_file.Chunks.Any(a => a.ChunkType == ChunkTypeEnum.CompiledBones || a.ChunkType == ChunkTypeEnum.CompiledBonesSC))
                nodes.Add(CreateJointNode(_file.Bones.RootBone));

            // Geometry visual Scene.
            if (false && _file.SkinningInfo.HasSkinningInfo)
            {
                Log($"{_file.Name} has SkinningInfo.");
                var skinNode = new GameObject(_file.Models[0].FileName);
                GameObject childObj;
                for (var i = 0; i < _file.Materials.Length; i++)
                {
                    var material = _file.Materials[i];
                    var geometry = _geometries[(0, 0)];
                    if (i == 0) childObj = skinNode;
                    else { childObj = new GameObject(material.Name); childObj.transform.SetParent(skinNode.transform, false); }
                    childObj.AddComponent<MeshFilter>().mesh = geometry.Item1;
                    var meshRenderer = childObj.AddComponent<MeshRenderer>();
                    meshRenderer.material = _materialManager.BuildMaterialFromProperties(geometry.Item2);
                }
                nodes.Add(skinNode);
            }
            else
            {
                if (_file.Models.Count > 1) // Star Citizen model with .cga/.cgam pair.
                {
                    // First model file (.cga or .cgf) will contain the main Root Node, along with all non geometry Node chunks (placeholders).
                    // Second one will have all the datastreams, but needs to be tied to the RootNode of the first model.
                    // THERE CAN BE MULTIPLE ROOT NODES IN EACH FILE!  Check to see if the parentnodeid ~0 and be sure to add a node for it.
                    var positionNodes = new List<GameObject>();        // For SC files, these are the nodes in the .cga/.cgf files.
                    foreach (var root in _file.Models[0].NodeMap.Values.Where(a => a.ParentNodeID == ~0).ToList())
                        positionNodes.Add(CreateNode(root));
                    nodes.AddRange(positionNodes.ToArray());
                }
                else nodes.Add(CreateNode(_file.RootNode));
            }
            if (nodes.Count == 1)
                return nodes[0];
            Log($"{_file.Name} has multiple meshs.");
            var obj = new GameObject(_file.RootNode.Name);
            foreach (var node in nodes)
                node.transform.SetParent(obj.transform, false);
            return obj;
        }

        /// <summary>
        /// Creates the node.
        /// This will be used recursively to create a node object and return it to WriteLibrary_VisualScenes
        /// </summary>
        /// <param name="nodeChunk">The node chunk.</param>
        /// <returns></returns>
        GameObject CreateNode(ChunkNode nodeChunk)
        {
            //Log($"CreateNode: for {nodeChunk.Name}");
            GameObject obj;
            // Check to see if there is a second model file, and if the mesh chunk is actually there.
            if (_file.Models.Count > 1)
            {
                // Star Citizen pair.  Get the Node and Mesh chunks from the geometry file, unless it's a Stream node.
                var nodeName = nodeChunk.Name;
                var nodeID = nodeChunk.ID;
                // make sure there is a geometry node in the geometry file
                if (true || _file.Models[1].NodeMap.ContainsKey(nodeID))
                {
                    var geometryNode = _file.Models[1].NodeMap.Values.Where(a => a.Name == nodeChunk.Name).First();
                    var geometryMesh = (ChunkMesh)_file.Models[1].ChunkMap[geometryNode.ObjectNodeID];
                    obj = CreateGeometryNode(geometryNode, geometryMesh);
                }
                else obj = CreateSimpleNode(nodeChunk);
            }
            else
            {
                // Regular Cryengine file.
                if (nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID].ChunkType == ChunkTypeEnum.Mesh)
                {
                    var geometryMesh = (ChunkMesh)nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID];
                    // Can have a node with a mesh and meshsubset, but no vertices.  Write as simple node.
                    obj = geometryMesh.MeshSubsets == 0 || geometryMesh.NumVertices == 0
                        ? CreateSimpleNode(nodeChunk)
                        : nodeChunk._model.ChunkMap[geometryMesh.MeshSubsets].ID != 0
                            ? CreateGeometryNode(nodeChunk, (ChunkMesh)nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID])
                            : CreateSimpleNode(nodeChunk);
                }
                else obj = CreateSimpleNode(nodeChunk);
            }
            // Add childnodes
            var childs = CreateChildNodes(nodeChunk);
            if (childs != null)
                foreach (var child in childs)
                    child.transform.SetParent(obj.transform, false);
            return obj;
        }

        GameObject CreateSimpleNode(ChunkNode nodeChunk)
        {
            //Log($"CreateSimpleNode: for {nodeChunk.Name}");
            // This will be used to make the node element for Node chunks that point to Helper Chunks and MeshPhysics
            var simpleNode = new GameObject(nodeChunk.Name);
            ApplyObject(nodeChunk, simpleNode);
            simpleNode.transform.FromMatrix(nodeChunk.LocalTransform.ToUnity());
            // Add childnodes
            var childs = CreateChildNodes(nodeChunk);
            if (childs != null)
                foreach (var child in childs)
                    child.transform.SetParent(simpleNode.transform, false);
            return simpleNode;
        }

        IEnumerable<GameObject> CreateChildNodes(ChunkNode nodeChunk)
        {
            if (nodeChunk.__NumChildren != 0)
            {
                var childNodes = new List<GameObject>();
                foreach (var childNodeChunk in nodeChunk.AllChildNodes)
                    childNodes.Add(CreateNode(childNodeChunk));
                return childNodes;
            }
            return null;
        }

        /// <summary>
        /// Creates the joint node.
        /// This will be used recursively to create a node object and return it to WriteLibrary_VisualScenes
        /// </summary>
        /// <param name="bone">The bone.</param>
        /// <returns></returns>
        GameObject CreateJointNode(CompiledBone bone)
        {
            //Log($"CreateJointNode: for {bone.boneName}");
            // If this is the root bone, set the node id to Armature.  Otherwise set to armature_<bonename>
            var jointNode = new GameObject(bone.boneName); //bone.parentID != 0 ? "Armature:" + bone.boneName : "Armature",
            jointNode.transform.FromMatrix(bone.LocalTransform.ToUnity()); // This is based on the BONETOWORLD data in this bone.
            // Recursively call this for each of the child bones to this bone.
            if (bone.numChildren > 0)
                foreach (var childBone in _file.Bones.GetAllChildBones(bone))
                    CreateJointNode(childBone).transform.SetParent(jointNode.transform, false);
            return jointNode;
        }

        void ApplyObject(ChunkNode node, GameObject obj)
        {
            var localTranslation = node.Transform.GetScale();
            var localRotation = node.Transform.GetRotation();
            var localScale = node.Transform.GetTranslation();
            node.LocalTranslation = localTranslation;
            node.LocalScale = localScale;
            node.LocalRotation = localRotation;
            node.LocalTransform = node.LocalTransform.GetTransformFromParts(localScale, localRotation, localTranslation);
            //
            obj.transform.FromMatrix(node.LocalTransform.ToUnity());
            //obj.transform.position = localTranslation.ToUnity(); // CryUtils.CryPointToUnityPoint(node.Translation);
            ////obj.transform.rotation = localRotation.ToUnity(); // CryUtils.CryRotationMatrixToUnityQuaternion(node.Rotation);
            //obj.transform.localScale = localScale.ToUnity(); // node.Scale.ToUnity(); // * UVector3.one;
        }

        GameObject CreateGeometryNode(ChunkNode nodeChunk, ChunkMesh tmpMeshChunk, bool visual = true, bool collidable = false)
        {
            //Log($"CreateGeometryNode: for {nodeChunk.Name} @ {_file.Materials.Length}");
            // we can have multiple matrices, but only need one since there is only one per Node chunk anyway
            var geometryNode = new GameObject(nodeChunk.Name);
            ApplyObject(nodeChunk, geometryNode);
            if (_file.Materials.Length == 0)
                return geometryNode;
            var tmpMeshSubsets = (ChunkMeshSubsets)nodeChunk._model.ChunkMap[tmpMeshChunk.MeshSubsets];
            GameObject childObj;
            for (var i = 0; i < tmpMeshSubsets.NumMeshSubset; i++)
            {
                var meshSubset = tmpMeshSubsets.MeshSubsets[i];
                var material = _file.Materials[meshSubset.MatID];
                if (!_geometries.TryGetValue((nodeChunk.ObjectNodeID, meshSubset.FirstIndex), out var geometry))
                    throw new InvalidOperationException("Missing");
                if (i == int.MaxValue) childObj = geometryNode;
                else { childObj = new GameObject(material.Name); childObj.transform.SetParent(geometryNode.transform, false); }
                if (visual)
                {
                    childObj.AddComponent<MeshFilter>().mesh = geometry.Item1;
                    var meshRenderer = childObj.AddComponent<MeshRenderer>();
                    meshRenderer.material = _materialManager.BuildMaterialFromProperties(geometry.Item2);
                    //if (Utils.ContainsBitFlags(triShape.Flags, (int)NiAVObject.NiFlags.Hidden))
                    //    meshRenderer.enabled = false;
                    childObj.isStatic = true;
                }
                if (collidable)
                {
                    childObj.AddComponent<MeshCollider>().sharedMesh = geometry.Item1;
                    if (KinematicRigidbodies)
                        childObj.AddComponent<Rigidbody>().isKinematic = true;
                }
            }
            return geometryNode;
        }

        void AddControllers() { }

        void SetGeometries()
        {
            var objs = new List<GameObject>();
            foreach (ChunkNode nodeChunk in _file.Chunks.Where(x => x.ChunkType == ChunkTypeEnum.Node))
            {
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
                var objectNode = nodeChunk._model.ChunkMap[nodeChunk.ObjectNodeID];
                if (objectNode.ChunkType != ChunkTypeEnum.Mesh)
                    continue;
                var meshChunk = (ChunkMesh)objectNode;

                // Check to see if the Mesh points to a PhysicsData mesh.  Don't want to write these.
                if (meshChunk.MeshPhysicsData != 0) { }
                // For the SC files, you can have Mesh chunks with no Mesh Subset.  Need to skip these.  They are in the .cga file and contain no geometry.  Just stub info.
                if (meshChunk.MeshSubsets == 0)
                    continue;

                ChunkDataStream tmpNormals = null;
                ChunkDataStream tmpUVs = null;
                ChunkDataStream tmpVertices = null;
                ChunkDataStream tmpVertsUVs = null;
                ChunkDataStream tmpIndices = null;
                ChunkDataStream tmpColors = null;
                ChunkDataStream tmpTangents = null;

                //Log($"tmpMeshChunk ID is {nodeChunk.ObjectNodeID:X}"); Log($"tmpmeshsubset ID is {meshChunk.MeshSubsets:X}");
                var tmpMeshSubsets = (ChunkMeshSubsets)nodeChunk._model.ChunkMap[meshChunk.MeshSubsets];
                if (meshChunk.VerticesData != 0) tmpVertices = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.VerticesData];
                if (meshChunk.NormalsData != 0) tmpNormals = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.NormalsData];
                if (meshChunk.UVsData != 0) tmpUVs = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.UVsData];
                // Star Citizen file.  That means VerticesData and UVsData will probably be empty.  Need to handle both cases.
                if (meshChunk.VertsUVsData != 0) tmpVertsUVs = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.VertsUVsData];
                if (meshChunk.IndicesData != 0) tmpIndices = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.IndicesData];
                // Ignore Tangent and Color data for now.
                if (meshChunk.ColorsData != 0) tmpColors = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.ColorsData];
                if (meshChunk.TangentsData != 0) tmpTangents = (ChunkDataStream)nodeChunk._model.ChunkMap[meshChunk.TangentsData];
                if (tmpVertices == null && tmpVertsUVs == null)
                    // There is no vertex data for this node.  Skip.
                    continue;

                UVector3[] vertices, normals; Vector2[] uvs;
                if (tmpVertices != null)  // Will be null if it's using VertsUVs.
                {
                    vertices = new UVector3[tmpVertices.NumElements];
                    normals = new UVector3[tmpNormals != null ? tmpNormals.NumElements : tmpVertices.NumElements];
                    uvs = new Vector2[tmpUVs.NumElements];
                    // Create Vertices and normals string
                    for (var j = 0U; j < meshChunk.NumVertices; j++)
                    {
                        var vertex = tmpVertices.Vertices[j]; vertices[j] = new UVector3(vertex.x, vertex.y, vertex.z);
                        var normal = tmpNormals?.Normals[j] ?? new Vector3(0.0f, 0.0f, 0.0f); normals[j] = new UVector3(Safe(normal.x), Safe(normal.y), Safe(normal.z));
                    }
                    // Create UV string
                    for (var j = 0U; j < tmpUVs.NumElements; j++)
                    {
                        var uv = tmpUVs.UVs[j]; uvs[j] = new Vector2(Safe(uv.U), Safe(1 - uv.V));
                    }
                }
                // VertsUV structure.  Pull out verts and UVs from tmpVertsUVs.
                else
                {
                    var inputFile = _file.InputFile;
                    vertices = new UVector3[tmpVertsUVs.NumElements];
                    normals = new UVector3[tmpVertsUVs.NumElements];
                    uvs = new Vector2[tmpVertsUVs.NumElements];
                    // Create Vertices and normals string
                    for (var j = 0U; j < meshChunk.NumVertices; j++)
                    {
                        // Rotate/translate the vertex
                        // Dymek's code to rescale by bounding box.  Only apply to geometry (cga or cgf), and not skin or chr objects.
                        if (!inputFile.EndsWith("skin") && !inputFile.EndsWith("chr"))
                        {
                            var multiplerX = Math.Abs(meshChunk.MinBound.x - meshChunk.MaxBound.x) / 2f;
                            var multiplerY = Math.Abs(meshChunk.MinBound.y - meshChunk.MaxBound.y) / 2f;
                            var multiplerZ = Math.Abs(meshChunk.MinBound.z - meshChunk.MaxBound.z) / 2f;
                            if (multiplerX < 1) multiplerX = 1;
                            if (multiplerY < 1) multiplerY = 1;
                            if (multiplerZ < 1) multiplerZ = 1;
                            tmpVertsUVs.Vertices[j].x = tmpVertsUVs.Vertices[j].x * multiplerX + (meshChunk.MaxBound.x + meshChunk.MinBound.x) / 2;
                            tmpVertsUVs.Vertices[j].y = tmpVertsUVs.Vertices[j].y * multiplerY + (meshChunk.MaxBound.y + meshChunk.MinBound.y) / 2;
                            tmpVertsUVs.Vertices[j].z = tmpVertsUVs.Vertices[j].z * multiplerZ + (meshChunk.MaxBound.z + meshChunk.MinBound.z) / 2;
                        }
                        var vertex = tmpVertsUVs.Vertices[j]; vertices[j] = new UVector3(vertex.x, vertex.y, vertex.z);
                        var normal = tmpVertsUVs.Normals[j]; normals[j] = new UVector3(Safe(normal.x), Safe(normal.y), Safe(normal.z));
                    }
                    // Create UV string
                    for (var j = 0U; j < tmpVertsUVs.NumElements; j++)
                    {
                        var uv = tmpVertsUVs.UVs[j]; uvs[j] = new Vector2(Safe(uv.U), Safe(1 - uv.V));
                    }
                }
                // Need to make a new Polylist entry for each submesh.
                for (var j = 0; j < tmpMeshSubsets.NumMeshSubset; j++)
                {
                    var triangles = new List<int>();
                    for (var k = tmpMeshSubsets.MeshSubsets[j].FirstIndex; k < (tmpMeshSubsets.MeshSubsets[j].FirstIndex + tmpMeshSubsets.MeshSubsets[j].NumIndices); k += 3)
                    {
                        triangles.Add((int)tmpIndices.Indices[k]);
                        triangles.Add((int)tmpIndices.Indices[k + 1]);
                        triangles.Add((int)tmpIndices.Indices[k + 2]);
                    }
                    // Create the mesh.
                    var mesh = new Mesh
                    {
                        vertices = vertices,
                        normals = normals,
                        uv = uvs,
                        triangles = triangles.ToArray()
                    };
                    if (normals.Length > 0)
                        mesh.RecalculateNormals();
                    mesh.RecalculateBounds();
                    //
                    var meshSubset = tmpMeshSubsets.MeshSubsets[j];
                    var material = _file.Materials.Length != 0 ? _file.Materials[meshSubset.MatID] : null;
                    if (material == null)
                    {
                        Log($"No material defined {nodeChunk.Name}");
                        continue;
                    }
                    var mtl = MeshPropertiesToMaterialProperties(material);
                    //Log($"Geometries: {nodeChunk.ObjectNodeID},{meshSubset.FirstIndex} for {nodeChunk.Name}");
                    _geometries.Add((nodeChunk.ObjectNodeID, meshSubset.FirstIndex), (mesh, mtl));
                }
            }
        }

        MaterialProps MeshPropertiesToMaterialProperties(Core.Material mtl)
        {
            var mp = new MaterialProps();
            if (mtl == null)
                return mp;
            // Create the material properties.
            //Log($"Mtl: {mtl.Name}");
            if (true)
            {
                if (mtl.AlphaTest != 0.0) // if flags contain the alpha test flag
                {
                    mp.AlphaTest = true;
                    mp.AlphaCutoff = (float)mtl.AlphaTest / 255F;
                }
            }
            else
            {
                mp.AlphaBlended = false;
                mp.AlphaTest = false;
            }
            // Apply textures.
            if (mtl.Textures != null) mp.Textures = ConfigureTextureProperties(mtl.Textures);
            return mp;
        }

        MaterialTextures ConfigureTextureProperties(Core.Material.Texture[] textures)
        {
            var tp = new MaterialTextures();
            foreach (var texture in textures.Where(x => x.TexType == Core.Material.Texture.TypeEnum.Default))
            {
                var filePath = $@"Data\{texture.File}";
                switch (texture.Map)
                {
                    //case Core.Material.Texture.MapTypeEnum.Unknown:
                    case Core.Material.Texture.MapTypeEnum.Diffuse: tp.MainFilePath = filePath; break;
                    //case Core.Material.Texture.MapTypeEnum.Bumpmap: tp.BumpFilePath = filePath; break;
                    case Core.Material.Texture.MapTypeEnum.Specular: tp.GlowFilePath = filePath; break;
                        //default: Log($"Unk {texture.Map}: {filePath}"); break;
                }
            }
            return tp;
        }

        static float Safe(float value) => value == float.NegativeInfinity ? float.MinValue : value == float.PositiveInfinity ? float.MaxValue : value == float.NaN ? 0 : value;
    }
}