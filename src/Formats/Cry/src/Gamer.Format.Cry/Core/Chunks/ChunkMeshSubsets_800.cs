using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkMeshSubsets_800 : ChunkMeshSubsets
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            Flags = r.ReadUInt32();   // Might be a ref to this chunk
            NumMeshSubset = r.ReadUInt32();  // number of mesh subsets
            SkipBytes(r, 8);
            MeshSubsets = new MeshSubset[NumMeshSubset];
            for (var i = 0; i < NumMeshSubset; i++)
            {
                MeshSubsets[i].FirstIndex = r.ReadUInt32();
                MeshSubsets[i].NumIndices = r.ReadUInt32();
                MeshSubsets[i].FirstVertex = r.ReadUInt32();
                MeshSubsets[i].NumVertices = r.ReadUInt32();
                MeshSubsets[i].MatID = r.ReadUInt32();
                MeshSubsets[i].Radius = r.ReadSingle();
                MeshSubsets[i].Center.x = r.ReadSingle();
                MeshSubsets[i].Center.y = r.ReadSingle();
                MeshSubsets[i].Center.z = r.ReadSingle();
            }
        }
    }
}
