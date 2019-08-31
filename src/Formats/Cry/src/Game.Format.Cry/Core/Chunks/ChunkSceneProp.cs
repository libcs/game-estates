using static Game.Core.Debug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkSceneProp : Chunk     // cccc0008 
    {
        // This chunk isn't really used, but contains some data probably necessary for the game.
        // Size for 0x744 type is always 0xBB4 (test this)
        public uint NumProps; // number of elements in the props array  (31 for type 0x744)
        public string[] PropKey;
        public string[] PropValue;

        public override void WriteChunk()
        {
            Log($"*** START SceneProp Chunk ***");
            Log($"    ChunkType:   {ChunkType}");
            Log($"    Version:     {Version:X}");
            Log($"    ID:          {ID:X}");
            for (var i = 0; i < NumProps; i++)
                Log($"{PropKey[i],30}{PropValue[i],20}");
            Log("*** END SceneProp Chunk ***");
        }
    }
}
