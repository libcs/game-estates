using static Game.Core.Debug;

namespace Game.Format.Cry.Core
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
