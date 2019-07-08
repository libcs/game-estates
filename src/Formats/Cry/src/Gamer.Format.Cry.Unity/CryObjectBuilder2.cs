using Gamer.Core;
using Gamer.Format.Cry.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Gamer.Core.Debug;
using UVector3 = UnityEngine.Vector3;

namespace Gamer.Format.Cry
{
    public class CryObjectBuilder2
    {
        const bool KinematicRigidbodies = true;
        const bool SkipShieldNodes = false;
        const bool SkipProxyNodes = false;

        readonly CryFile _file;
        readonly MaterialManager _materialManager;
        readonly int _markerLayer;

        public CryObjectBuilder2(CryFile file, MaterialManager materialManager, int markerLayer)
        {
            _file = file;
            _materialManager = materialManager;
            _markerLayer = markerLayer;
        }

        public GameObject BuildObject()
        {
            Assert(_file.Models.Count > 0);
            //
            var nullParents = _file.NodeMap.Values.Where(p => p.ParentNode == null).ToArray();
            if (nullParents.Length > 1)
                foreach (var node in nullParents)
                    Log($"Rendering node with null parent {node.Name}");
            //
            var gameObject = InstantiateRootObject();
            if (gameObject == null)
            {
                Log($"{_file.Name} resulted in a null GameObject when instantiated.");
                gameObject = new GameObject(_file.Name);
            }
            return gameObject;
            //{
            //    Log($"{_obj.Name} has multiple roots.");
            //    var gameObject = new GameObject(_obj.Name);
            //    foreach (var model in _obj.Models)
            //    {
            //        var child = InstantiateRootObject(model);
            //        if (child != null)
            //            child.transform.SetParent(gameObject.transform, false);
            //    }
            //    return gameObject;
            //}
        }

        GameObject InstantiateRootObject()
        {
            var gameObject = InstantiateObject();
            //ProcessExtraData(obj, out var shouldAddMissingColliders, out var isMarker);
            //if (_obj.Name != null && IsMarkerFileName(_obj.Name))
            //{
            //    shouldAddMissingColliders = false;
            //    isMarker = true;
            //}
            //// Add colliders to the object if it doesn't already contain one.
            //if (shouldAddMissingColliders && gameObject.GetComponentInChildren<Collider>() == null)
            //    GameObjectUtils.AddMissingMeshCollidersRecursively(gameObject);
            //if (isMarker)
            //    GameObjectUtils.SetLayerRecursively(gameObject, _markerLayer);
            return gameObject;
        }

        GameObject InstantiateObject()
        {
            GameObject r = null;
            foreach (var node in _file.NodeMap.Values)
            {
                // Don't render shields
                if (SkipShieldNodes && node.Name.StartsWith("$shield"))
                {
                    Log($"Skipped shields node {node.Name}");
                    continue;
                }
                // Don't render proxy
                if (SkipProxyNodes && node.Name.StartsWith("proxy"))
                {
                    Log($"Skipped proxy node {node.Name}");
                    continue;
                }
                if (node.ObjectChunk == null)
                {
                    Log($"Skipped node with missing Object {node.Name}");
                    continue;
                }
                switch (node.ObjectChunk.ChunkType)
                {
                    case ChunkTypeEnum.Mesh:
                        if (node.ParentNode != null && node.ParentNode.ChunkType != ChunkTypeEnum.Node)
                            Log($"Rendering {node.Name} to parent {node.ParentNode.Name}");
                        r = InstantiateNode(node, true, false);
                        break;
                    case ChunkTypeEnum.Helper: break; // Ignore Helpers nodes
                    // Warn us if we're skipping other nodes of interest
                    default: Log($"Skipped a {node.ObjectChunk.ChunkType} chunk"); break;
                }
            }
            return r;
        }

        GameObject InstantiateNode(ChunkNode chunkNode, bool visual, bool collidable)
        {
            Assert(visual || collidable);

            // The transform of a child has to add the transforms of ALL the parents.
            if (!(chunkNode.ObjectChunk is ChunkMesh meshData))
                return null;

            var meshs = InstantiateNodeMesh(chunkNode, meshData, visual, collidable).ToArray();
            if (meshs.Length == 1)
                return meshs[0];
            Log($"{chunkNode.Name} has multiple meshs.");
            var obj = new GameObject(chunkNode.Name);
            foreach (var mesh in meshs)
                mesh.transform.SetParent(obj.transform, false);
            return obj;
        }

        static float Safe(float value) => value == float.NegativeInfinity ? float.MinValue : value == float.PositiveInfinity ? float.MaxValue : value == float.NaN ? 0 : value;

        IEnumerable<GameObject> InstantiateNodeMesh(ChunkNode chunkNode, ChunkMesh meshData, bool visual, bool collidable)
        {
            if (meshData.MeshSubsets == 0)   // This is probably wrong.  These may be parents with no geometry, but still have an offset
            {
                Log($"*******Found a Mesh chunk with no Submesh ID (ID: {meshData.ID:X}, Name: {chunkNode.Name}).  Skipping...");
                // data.WriteChunk();
                // Log($"Node Chunk: {chunkNode.Name}");
                // transform = cgfData.GetTransform(chunkNode, transform);
                yield break;
            }
            if (meshData.VerticesData == 0 && meshData.VertsUVsData == 0)  // This is probably wrong.  These may be parents with no geometry, but still have an offset
            {
                Log($"*******Found a Mesh chunk with no Vertex info (ID: {meshData.ID:X}, Name: {chunkNode.Name}).  Skipping...");
                //data.WriteChunk();
                //Log($"Node Chunk: {chunkNode.Name}");
                //transform = cgfData.GetTransform(chunkNode, transform);
                yield break;
            }

            // Going to assume that there is only one VerticesData datastream for now.  Need to watch for this.   
            // Some 801 types have vertices and not VertsUVs.
            var chunkMap = chunkNode._model.ChunkMap;
            var tmpMtlName = chunkMap.GetValue(chunkNode.MatID, null) as ChunkMtlName;
            var tmpMeshSubsets = chunkMap.GetValue(meshData.MeshSubsets, null) as ChunkMeshSubsets; // Listed as Object ID for the Node
            var tmpIndices = chunkMap.GetValue(meshData.IndicesData, null) as ChunkDataStream;
            var tmpVertices = chunkMap.GetValue(meshData.VerticesData, null) as ChunkDataStream;
            var tmpNormals = chunkMap.GetValue(meshData.NormalsData, null) as ChunkDataStream;
            var tmpUVs = chunkMap.GetValue(meshData.UVsData, null) as ChunkDataStream;
            var tmpVertsUVs = chunkMap.GetValue(meshData.VertsUVsData, null) as ChunkDataStream;

            foreach (var meshSubset in tmpMeshSubsets.MeshSubsets)
            {
                var mesh = DataToMesh(chunkNode, meshData, tmpIndices, tmpVertices, tmpNormals, tmpUVs, tmpVertsUVs, meshSubset);
                var obj = new GameObject(chunkNode.Name);
                if (visual)
                {
                    obj.AddComponent<MeshFilter>().mesh = mesh;

                    // MATERIAL
                    MaterialProps materialProps;
                    if (_file.Materials.Length > meshSubset.MatID)
                        materialProps = MeshPropertiesToMaterialProperties(_file.Materials[meshSubset.MatID]);
                    else
                    {
                        if (_file.Materials.Length > 0)
                            Log($"Missing Material {meshSubset.MatID}");
                        // The material file doesn't have any elements with the Name of the material.  Use the object name.
                        var mtl = _file.Materials.First(x => x.Name == $"{_file.RootNode.Name}_{meshSubset.MatID}");
                        materialProps = MeshPropertiesToMaterialProperties(mtl);
                    }
                    var meshRenderer = obj.AddComponent<MeshRenderer>();
                    meshRenderer.material = _materialManager.BuildMaterialFromProperties(materialProps);
                    //if (Utils.ContainsBitFlags(triShape.Flags, (int)NiAVObject.NiFlags.Hidden))
                    //    meshRenderer.enabled = false;
                    obj.isStatic = true;
                }
                if (collidable)
                {
                    obj.AddComponent<MeshCollider>().sharedMesh = mesh;
                    if (KinematicRigidbodies)
                        obj.AddComponent<Rigidbody>().isKinematic = true;
                }
                yield return obj;
            }
        }

        static Mesh DataToMesh(ChunkNode chunkNode, ChunkMesh data, ChunkDataStream tmpIndices, ChunkDataStream tmpVertices, ChunkDataStream tmpNormals, ChunkDataStream tmpUVs, ChunkDataStream tmpVertsUVs, MeshSubset meshSubset)
        {
            // VERTICES/UVS
            var vertices = new List<UVector3>();
            var UVs = new List<Vector2>();
            if (data.VerticesData == 0)
            {
                // Probably using VertsUVs (3.7+).  Write those vertices out. Do UVs at same time.
                for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                {
                    // Get the transform. Dymek's code. Scales the object by the bounding box.
                    var multiplerX = Math.Abs(data.MinBound.x - data.MaxBound.x) / 2f;
                    var multiplerY = Math.Abs(data.MinBound.y - data.MaxBound.y) / 2f;
                    var multiplerZ = Math.Abs(data.MinBound.z - data.MaxBound.z) / 2f;
                    if (multiplerX < 1) multiplerX = 1;
                    if (multiplerY < 1) multiplerY = 1;
                    if (multiplerZ < 1) multiplerZ = 1;
                    tmpVertsUVs.Vertices[j].x = tmpVertsUVs.Vertices[j].x * multiplerX + (data.MaxBound.x + data.MinBound.x) / 2f;
                    tmpVertsUVs.Vertices[j].y = tmpVertsUVs.Vertices[j].y * multiplerY + (data.MaxBound.y + data.MinBound.y) / 2f;
                    tmpVertsUVs.Vertices[j].z = tmpVertsUVs.Vertices[j].z * multiplerZ + (data.MaxBound.z + data.MinBound.z) / 2f;
                    var vertex = tmpVertsUVs.Vertices[j];
                    vertices.Add(new UVector3(Safe(vertex.x), Safe(vertex.y), Safe(vertex.z)));
                }
                for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                    UVs.Add(new Vector2(Safe(tmpVertsUVs.UVs[j].U), Safe(1 - tmpVertsUVs.UVs[j].V)));
            }
            else
            {
                for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                    if (tmpVertices != null)
                    {
                        var vertex = tmpVertices.Vertices[j];
                        vertices.Add(new UVector3(Safe(vertex.x), Safe(vertex.y), Safe(vertex.z)));
                    }
                    else Log($"Error rendering vertices for {chunkNode.Name:X}");
                for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                    UVs.Add(new Vector2(Safe(tmpUVs.UVs[j].U), Safe(1 - tmpUVs.UVs[j].V)));
            }

            // NORMALS
            var normals = new List<UVector3>();
            if (data.NormalsData != 0)
                for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                    normals.Add(new UVector3(tmpNormals.Normals[j].x, tmpNormals.Normals[j].y, tmpNormals.Normals[j].z));

            // FACES
            var triangles = new List<int>();
            for (var j = meshSubset.FirstIndex; j < meshSubset.NumIndices + meshSubset.FirstIndex; j += 3)
            {
                triangles.Add((int)tmpIndices.Indices[j]);
                triangles.Add((int)tmpIndices.Indices[j + 1]);
                triangles.Add((int)tmpIndices.Indices[j + 2]);
            }

            // Create the mesh.
            var mesh = new Mesh
            {
                vertices = vertices.ToArray(),
                normals = normals.ToArray(),
                uv = UVs.ToArray(),
                triangles = triangles.ToArray()
            };
            if (normals.Count > 0)
                mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            return mesh;
        }

        MaterialProps MeshPropertiesToMaterialProperties(Core.Material mtl)
        {
            // Create the material properties.
            var mp = new MaterialProps();
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
                var filePath = $@"Data\{texture.File.Replace("/", "\\")}";
                Log($"{texture.Map} - {filePath}");
                switch (texture.Map)
                {
                    case Core.Material.Texture.MapTypeEnum.Diffuse: tp.MainFilePath = filePath; break;
                    //case Core.Material.Texture.MapTypeEnum.Bumpmap: tp.BumpFilePath = filePath; break;
                    case Core.Material.Texture.MapTypeEnum.Specular: tp.GlowFilePath = filePath; break;
                    default: Log($"Unk {texture.Map}: {filePath}"); break;
                }
            }
            return tp;
        }
    }
}