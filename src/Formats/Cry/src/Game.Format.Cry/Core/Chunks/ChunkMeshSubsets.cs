using static Game.Core.Debug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkMeshSubsets : Chunk // cccc0017:  The different parts of a mesh.  Needed for obj exporting
    {
        public uint Flags; // probably the offset
        public uint NumMeshSubset; // number of mesh subsets
        public MeshSubset[] MeshSubsets;

        // For bone ID meshes? Not sure where this is used yet.
        public uint NumberOfBoneIDs;
        public ushort[] BoneIDs;

        public override void WriteChunk()
        {
            Log("*** START MESH SUBSET CHUNK ***");
            Log("    ChunkType:       {ChunkType}");
            Log("    Mesh SubSet ID:  {ID:X}");
            Log("    Number of Mesh Subsets: {NumMeshSubset}");
            for (var i = 0; i < NumMeshSubset; i++)
            {
                Log($"        ** Mesh Subset:          {i}");
                Log($"           First Index:          {MeshSubsets[i].FirstIndex}");
                Log($"           Number of Indices:    {MeshSubsets[i].NumIndices}");
                Log($"           First Vertex:         {MeshSubsets[i].FirstVertex}");
                Log($"           Number of Vertices:   {MeshSubsets[i].NumVertices}  (next will be {MeshSubsets[i].NumVertices + MeshSubsets[i].FirstVertex})");
                Log($"           Material ID:          {MeshSubsets[i].MatID}");
                Log($"           Radius:               {MeshSubsets[i].Radius}");
                Log($"           Center:   {MeshSubsets[i].Center.x},{MeshSubsets[i].Center.y},{MeshSubsets[i].Center.z}");
                Log($"        ** Mesh Subset {i} End");
            }
            Log("*** END MESH SUBSET CHUNK ***");
        }
    }
}