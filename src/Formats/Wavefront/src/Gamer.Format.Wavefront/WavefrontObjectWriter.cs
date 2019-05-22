using Gamer.Format.Cry;
using Gamer.Format.Cry.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static Gamer.Core.Debug;

namespace Gamer.Format.Wavefront
{
    public partial class WavefrontObjectWriter : CryObjectWriter
    {
        public WavefrontObjectWriter(CryFile cryFile) : base(cryFile) { }

        public FileInfo OutputFile_Model { get; internal set; }
        public FileInfo OutputFile_Material { get; internal set; }
        public uint CurrentVertexPosition { get; internal set; }
        public uint TempIndicesPosition { get; internal set; }
        public uint TempVertexPosition { get; internal set; }
        public uint CurrentIndicesPosition { get; internal set; }
        public string GroupOverride { get; internal set; }
        public int FaceIndex { get; internal set; }

        /// <summary>
        /// Renders an .obj file, and matching .mat file for the current model
        /// </summary>
        /// <param name="outputDir">Folder to write files to</param>
        /// <param name="preservePath">When using an <paramref name="outputDir"/>, preserve the original hierarchy</param>
        public override void Write(string outputDir = null, bool preservePath = true)
        {
            // We need to create the obj header, then for each submesh write the vertex, UV and normal data.
            // First, let's figure out the name of the output file.  Should be <object name>.obj

            // Each Mesh will have a mesh subset and a series of datastream objects.  Need temporary pointers to these
            // so we can manipulate

            // Get object name.  This is the Root Node chunk Name
            // Get the objOutputFile name

            // string outputFile = outputFile;

            OutputFile_Model = new FileInfo(GetOutputFile("obj", outputDir, preservePath));
            OutputFile_Material = new FileInfo(GetOutputFile("mtl", outputDir, preservePath));

            if (GroupMeshes)
                GroupOverride = Path.GetFileNameWithoutExtension(OutputFile_Model.Name);

            Console.WriteLine(@"Output file is {0}\...\{1}", outputDir, OutputFile_Model.Name);

            if (!OutputFile_Model.Directory.Exists)
                OutputFile_Model.Directory.Create();

            WriteMaterial(CryFile);

            using (var file = new StreamWriter(OutputFile_Model.FullName))
            {
                file.WriteLine($"# cgf-converter .obj export version {Assembly.GetExecutingAssembly().GetName().Version}");
                file.WriteLine("#");
                if (OutputFile_Material.Exists)
                    file.WriteLine($"mtllib {OutputFile_Material.Name}");

                FaceIndex = 1;
                var nullParents = CryFile.NodeMap.Values.Where(p => p.ParentNode == null).ToArray();
                if (nullParents.Length > 1)
                    foreach (var node in nullParents)
                        Log($"Rendering node with null parent {node.Name}");

                foreach (ChunkNode node in CryFile.NodeMap.Values)
                {
                    // Don't render shields
                    if (SkipShieldNodes && node.Name.StartsWith("$shield"))
                    {
                        Log($"Skipped shields node {node.Name}");
                        continue;
                    }
                    // Don't render shields
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
                            // Render Meshes
                            if (node.ParentNode != null && node.ParentNode.ChunkType != ChunkTypeEnum.Node)
                                Log($"Rendering {node.Name} to parent {node.ParentNode.Name}");
                            // TODO: Transform Root Nodes here?
                            file.WriteLine("o {0}", node.Name);
                            // Grab the mesh and process that.
                            WriteObjNode(file, node);
                            break;
                        case ChunkTypeEnum.Helper:
                            // Ignore Helpers nodes
                            // TODO: Investigate if there's something we should do here
                            break;
                        // Warn us if we're skipping other nodes of interest
                        default: Log($"Skipped a {node.ObjectChunk.ChunkType} chunk"); break;
                    }
                }

                // If this is a .chr file, just write out the hitbox info.  OBJ files can't do armatures.
                foreach (ChunkCompiledPhysicalProxies tmpProxy in CryFile.Chunks.Where(a => a.ChunkType == ChunkTypeEnum.CompiledPhysicalProxies))
                    WriteObjHitBox(file, tmpProxy); // TODO: align these properly
            }
        }

        public double safe(double value) => value == double.NegativeInfinity
                ? double.MinValue
                : value == double.PositiveInfinity ? double.MaxValue : value == double.NaN ? 0 : value;

        public void WriteObjNode(StreamWriter w, ChunkNode chunkNode)  // Pass a node to this to have it write to the Stream
        {
            // Get the Transform here. It's the node chunk Transform.m(41/42/42) divided by 100, added to the parent transform.
            // The transform of a child has to add the transforms of ALL the parents.
            if (!(chunkNode.ObjectChunk is ChunkMesh tmpMesh))
                return;

            if (tmpMesh.MeshSubsets == 0)   // This is probably wrong.  These may be parents with no geometry, but still have an offset
            {
                Log($"*******Found a Mesh chunk with no Submesh ID (ID: {tmpMesh.ID:X}, Name: {chunkNode.Name}).  Skipping...");
                // tmpMesh.WriteChunk();
                // Log($"Node Chunk: {chunkNode.Name}");
                // transform = cgfData.GetTransform(chunkNode, transform);
                return;
            }
            if (tmpMesh.VerticesData == 0 && tmpMesh.VertsUVsData == 0)  // This is probably wrong.  These may be parents with no geometry, but still have an offset
            {
                Log($"*******Found a Mesh chunk with no Vertex info (ID: {tmpMesh.ID:X}, Name: {chunkNode.Name}).  Skipping...");
                //tmpMesh.WriteChunk();
                //Log($"Node Chunk: {chunkNode.Name}");
                //transform = cgfData.GetTransform(chunkNode, transform);
                return;
            }

            // Going to assume that there is only one VerticesData datastream for now.  Need to watch for this.   
            // Some 801 types have vertices and not VertsUVs.
            var tmpMtlName = chunkNode._model.ChunkMap.GetValue(chunkNode.MatID, null) as ChunkMtlName;
            var tmpMeshSubsets = tmpMesh._model.ChunkMap.GetValue(tmpMesh.MeshSubsets, null) as ChunkMeshSubsets; // Listed as Object ID for the Node
            var tmpIndices = tmpMesh._model.ChunkMap.GetValue(tmpMesh.IndicesData, null) as ChunkDataStream;
            var tmpVertices = tmpMesh._model.ChunkMap.GetValue(tmpMesh.VerticesData, null) as ChunkDataStream;
            var tmpNormals = tmpMesh._model.ChunkMap.GetValue(tmpMesh.NormalsData, null) as ChunkDataStream;
            var tmpUVs = tmpMesh._model.ChunkMap.GetValue(tmpMesh.UVsData, null) as ChunkDataStream;
            var tmpVertsUVs = tmpMesh._model.ChunkMap.GetValue(tmpMesh.VertsUVsData, null) as ChunkDataStream;

            // We only use 3 things in obj files:  vertices, normals and UVs.  No need to process the Tangents.

            var numChildren = chunkNode.__NumChildren;           // use in a for loop to print the mesh for each child

            var tempVertexPosition = CurrentVertexPosition;
            var tempIndicesPosition = CurrentIndicesPosition;

            foreach (var meshSubset in tmpMeshSubsets.MeshSubsets)
            {
                // Write vertices data for each MeshSubSet (v)
                w.WriteLine("g {0}", GroupOverride ?? chunkNode.Name);

                // WRITE VERTICES OUT (V, VT)
                if (tmpMesh.VerticesData == 0)
                {
                    // Probably using VertsUVs (3.7+).  Write those vertices out. Do UVs at same time.
                    for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                    {
                        // Let's try this using this node chunk's rotation matrix, and the transform is the sum of all the transforms.
                        // Get the transform.
                        // Dymek's code.  Scales the object by the bounding box.
                        var multiplerX = Math.Abs(tmpMesh.MinBound.x - tmpMesh.MaxBound.x) / 2f;
                        var multiplerY = Math.Abs(tmpMesh.MinBound.y - tmpMesh.MaxBound.y) / 2f;
                        var multiplerZ = Math.Abs(tmpMesh.MinBound.z - tmpMesh.MaxBound.z) / 2f;
                        if (multiplerX < 1) multiplerX = 1;
                        if (multiplerY < 1) multiplerY = 1;
                        if (multiplerZ < 1) multiplerZ = 1;
                        tmpVertsUVs.Vertices[j].x = tmpVertsUVs.Vertices[j].x * multiplerX + (tmpMesh.MaxBound.x + tmpMesh.MinBound.x) / 2f;
                        tmpVertsUVs.Vertices[j].y = tmpVertsUVs.Vertices[j].y * multiplerY + (tmpMesh.MaxBound.y + tmpMesh.MinBound.y) / 2f;
                        tmpVertsUVs.Vertices[j].z = tmpVertsUVs.Vertices[j].z * multiplerZ + (tmpMesh.MaxBound.z + tmpMesh.MinBound.z) / 2f;
                        var vertex = chunkNode.GetTransform(tmpVertsUVs.Vertices[j]);
                        w.WriteLine("v {0:F7} {1:F7} {2:F7}", safe(vertex.x), safe(vertex.y), safe(vertex.z));
                    }
                    w.WriteLine();
                    for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                        w.WriteLine("vt {0:F7} {1:F7} 0", safe(tmpVertsUVs.UVs[j].U), safe(1 - tmpVertsUVs.UVs[j].V));
                }
                else
                {
                    for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                        if (tmpVertices != null)
                        {
                            // Rotate/translate the vertex
                            var vertex = chunkNode.GetTransform(tmpVertices.Vertices[j]);
                            w.WriteLine("v {0:F7} {1:F7} {2:F7}", safe(vertex.x), safe(vertex.y), safe(vertex.z));
                        }
                        else Log($"Error rendering vertices for {chunkNode.Name:X}");
                    w.WriteLine();
                    for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                        w.WriteLine("vt {0:F7} {1:F7} 0", safe(tmpUVs.UVs[j].U), safe(1 - tmpUVs.UVs[j].V));
                }

                w.WriteLine();

                // WRITE NORMALS BLOCK (VN)
                if (tmpMesh.NormalsData != 0)
                    for (var j = meshSubset.FirstVertex; j < meshSubset.NumVertices + meshSubset.FirstVertex; j++)
                        w.WriteLine("vn {0:F7} {1:F7} {2:F7}",
                            tmpNormals.Normals[j].x,
                            tmpNormals.Normals[j].y,
                            tmpNormals.Normals[j].z);

                // WRITE GROUP (G)
                // w.WriteLine("g {0}", this.GroupOverride ?? chunkNode.Name);

                if (Smooth)
                    w.WriteLine("s {0}", FaceIndex++);

                // WRITE MATERIAL BLOCK (USEMTL)
                if (CryFile.Materials.Length > meshSubset.MatID)
                    w.WriteLine("usemtl {0}", CryFile.Materials[meshSubset.MatID].Name);
                else
                {
                    if (CryFile.Materials.Length > 0)
                        Log($"Missing Material {meshSubset.MatID}");
                    // The material file doesn't have any elements with the Name of the material.  Use the object name.
                    w.WriteLine("usemtl {0}_{1}", CryFile.RootNode.Name, meshSubset.MatID);
                }

                // Now write out the faces info based on the MtlName
                for (var j = meshSubset.FirstIndex; j < meshSubset.NumIndices + meshSubset.FirstIndex; j++)
                {
                    w.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}",    // Vertices, UVs, Normals
                        tmpIndices.Indices[j] + 1 + CurrentVertexPosition,
                        tmpIndices.Indices[j + 1] + 1 + CurrentVertexPosition,
                        tmpIndices.Indices[j + 2] + 1 + CurrentVertexPosition);
                    j += 2;
                }

                tempVertexPosition += meshSubset.NumVertices;  // add the number of vertices so future objects can start at the right place
                tempIndicesPosition += meshSubset.NumIndices;  // Not really used...
            }

            // Extend the current vertex, uv and normal positions by the length of those arrays.
            CurrentVertexPosition = tempVertexPosition;
            CurrentIndicesPosition = tempIndicesPosition;
        }

        public void WriteObjHitBox(StreamWriter w, ChunkCompiledPhysicalProxies chunkProx)  // Pass a bone proxy to write to the stream.  For .chr files (armatures)
        {
            // The chunkProx has the vertex and index info, so much like WriteObjNode just need to write it out.  Much simpler than WriteObjNode though in theory
            // Assume only one CompiledPhysicalProxies per .chr file (or any file for that matter).  May not be a safe bet.
            // Need the materials
            //
            // Log($"There are {chunkProx.NumBones} Bones");
            for (var i = 0; i < chunkProx.NumPhysicalProxies; i++)        // Write out all the bones
            {
                // write out this bones vertex info.
                // Need to find a way to get the material name associated with the bone, so we can link the hitbox to the body part.
                w.WriteLine("g");
                //Log("Num Vertices: {0} ", chunkProx.HitBoxes[i].NumVertices);
                for (var j = 0; j < chunkProx.PhysicalProxies[i].NumVertices; j++)
                {
                    //Log("{0} {1} {2}", chunkProx.HitBoxes[i].Vertices[j].x, chunkProx.HitBoxes[i].Vertices[j].y, chunkProx.HitBoxes[i].Vertices[j].z);
                    // Transform the vertex
                    //var vertex = chunkNode.GetTransform(tmpVertsUVs.Vertices[j]);
                    w.WriteLine("v {0:F7} {1:F7} {2:F7}",
                        chunkProx.PhysicalProxies[i].Vertices[j].x,
                        chunkProx.PhysicalProxies[i].Vertices[j].y,
                        chunkProx.PhysicalProxies[i].Vertices[j].z);
                }
                w.WriteLine();
                w.WriteLine("g {0}", i);

                // The material file doesn't have any elements with the Name of the material.  Use i
                w.WriteLine("usemtl {0}", i);
                for (var j = 0; j < chunkProx.PhysicalProxies[i].NumIndices; j++)
                {
                    w.WriteLine("f {0} {1} {2}",
                        chunkProx.PhysicalProxies[i].Indices[j] + 1 + CurrentVertexPosition,
                        chunkProx.PhysicalProxies[i].Indices[j + 1] + 1 + CurrentVertexPosition,
                        chunkProx.PhysicalProxies[i].Indices[j + 2] + 1 + CurrentVertexPosition);
                    j = j + 2;
                }
                CurrentVertexPosition += chunkProx.PhysicalProxies[i].NumVertices;
                CurrentIndicesPosition += chunkProx.PhysicalProxies[i].NumIndices;
                w.WriteLine();
            }
            w.WriteLine();
            //CurrentVertexPosition = TempVertexPosition;
            //CurrentIndicesPosition = TempIndicesPosition;
        }
    }
}