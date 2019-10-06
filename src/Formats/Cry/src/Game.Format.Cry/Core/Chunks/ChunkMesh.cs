﻿using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkMesh : Chunk      //  cccc0000:  Object that points to the datastream chunk.
    {
        // public uint Version;  // 623 Far Cry, 744 Far Cry, Aion, 800 Crysis
        //public bool HasVertexWeights; // for 744
        //public bool HasVertexColors; // 744
        //public bool InWorldSpace; // 623
        //public byte Reserved1;  // padding byte, 744
        //public byte Reserved2;  // padding byte, 744
        public int Flags1;  // 800  Offset of this chunk. 
        public int Flags2;  // 801 and 802
        // public uint ID;  // 800  Chunk ID
        public int NumVertices; // 
        public int NumIndices;  // Number of indices (each triangle has 3 indices, so this is the number of triangles times 3).
        //public uint NumUVs; // 744
        //public uint NumFaces; // 744
        // Pointers to various Chunk types
        //public ChunkMtlName Material; // 623, Material Chunk, never encountered?
        public int NumVertSubsets; // 801, Number of vert subsets
        public int VertsAnimID;
        public int MeshSubsets; // 800  Reference of the mesh subsets
        // public ChunkVertAnim VertAnims; // 744.  not implemented
        //public Vertex[] Vertices; // 744.  not implemented
        //public Face[,] Faces; // 744.  Not implemented
        //public UV[] UVs; // 744 Not implemented
        //public UVFace[] UVFaces; // 744 not implemented
        // public VertexWeight[] VertexWeights; // 744 not implemented
        //public IRGB[] VertexColors; // 744 not implemented
        public int VerticesData; // 800, 801.  Need an array because some 801 files have NumVertSubsets
        public int NumBuffs;
        public int NormalsData; // 800
        public int UVsData; // 800
        public int ColorsData; // 800
        public int Colors2Data; // 800 
        public int IndicesData; // 800
        public int TangentsData; // 800
        public int ShCoeffsData; // 800
        public int ShapeDeformationData; //800
        public int BoneMapData; //800
        public int FaceMapData; // 800
        public int VertMatsData; // 800
        public int MeshPhysicsData; // 801
        public int VertsUVsData;    // 801
        public int[] PhysicsData = new int[4]; // 800
        public Vector3 MinBound; // 800 minimum coordinate values
        public Vector3 MaxBound; // 800 Max coord values

        /// <summary>
        /// The actual geometry info for this mesh.
        /// </summary>
        public GeometryInfo GeometryInfo { get; set; }

        //public ChunkMeshSubsets chunkMeshSubset; // pointer to the mesh subset that belongs to this mesh

        public override void WriteChunk()
        {
            Log($"*** START MESH CHUNK ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Chunk ID:            {ID:X}");
            Log($"    MeshSubSetID:        {MeshSubsets:X}");
            Log($"    Vertex Datastream:   {VerticesData:X}");
            Log($"    Normals Datastream:  {NormalsData:X}");
            Log($"    UVs Datastream:      {UVsData:X}");
            Log($"    Indices Datastream:  {IndicesData:X}");
            Log($"    Tangents Datastream: {TangentsData:X}");
            Log($"    Mesh Physics Data:   {MeshPhysicsData:X}");
            Log($"    VertUVs:             {VertsUVsData:X}");
            Log($"    MinBound:            {MinBound.x:F7}, {MinBound.y:F7}, {MinBound.z:F7}");
            Log($"    MaxBound:            {MaxBound.x:F7}, {MaxBound.y:F7}, {MaxBound.z:F7}");
            Log($"*** END MESH CHUNK ***");
        }
    }
}
