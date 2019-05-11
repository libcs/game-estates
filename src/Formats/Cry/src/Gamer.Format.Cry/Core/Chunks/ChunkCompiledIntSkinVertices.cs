using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class ChunkCompiledIntSkinVertices : Chunk
    {
        public int Reserved;
        public IntSkinVertex[] IntSkinVertices;
        public int NumIntVertices { get; set; }                  // Calculate by size of data div by size of IntSkinVertex structure.

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
        }
    }
}
