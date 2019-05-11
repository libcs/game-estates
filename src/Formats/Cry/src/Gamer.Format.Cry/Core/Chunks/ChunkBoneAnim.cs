using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class ChunkBoneAnim : Chunk
    {
        public int NumBones;

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Number of Targets:   {NumBones:X}");
        }
    }
}
