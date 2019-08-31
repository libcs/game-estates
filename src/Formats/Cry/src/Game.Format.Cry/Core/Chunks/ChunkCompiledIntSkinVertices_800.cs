using System.IO;
using System.Linq;

namespace Game.Format.Cry.Core
{
    public class ChunkCompiledIntSkinVertices_800 : ChunkCompiledIntSkinVertices
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            NumIntVertices = (int)((Size - 32) / 64);
            IntSkinVertices = new IntSkinVertex[NumIntVertices];
            SkipBytes(r, 32);          // Padding between the chunk header and the first IntVertex.
            // Size of the IntSkinVertex is 64 bytes
            for (var i = 0; i < NumIntVertices; i++)
            {
                IntSkinVertices[i].Obsolete0.ReadVector3(r);
                IntSkinVertices[i].Position.ReadVector3(r);
                IntSkinVertices[i].Obsolete2.ReadVector3(r);
                // Read 4 bone IDs
                IntSkinVertices[i].BoneIDs = new ushort[4];
                for (var j = 0; j < 4; j++)
                    IntSkinVertices[i].BoneIDs[j] = r.ReadUInt16();
                // Read the weights for those bone IDs
                IntSkinVertices[i].Weights = new float[4];
                for (var j = 0; j < 4; j++)
                    IntSkinVertices[i].Weights[j] = r.ReadSingle();
                // Read the color
                IntSkinVertices[i].Color.Read(r);
            }
            var skin = GetSkinningInfo();
            skin.IntVertices = IntSkinVertices.ToList();
        }

        public override void WriteChunk() => base.WriteChunk();
    }
}
