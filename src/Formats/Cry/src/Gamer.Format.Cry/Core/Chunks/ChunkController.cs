using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class ChunkController : Chunk    // cccc000d:  Controller chunk
    {
        public CtrlType ControllerType { get; internal set; }
        public uint NumKeys { get; internal set; }
        public uint ControllerFlags { get; internal set; }        // technically a bitstruct to identify a cycle or a loop.
        public uint ControllerID { get; internal set; }           // Unique id based on CRC32 of bone name.  Ver 827 only?
        public Key[] Keys { get; internal set; }                  // array length NumKeys.  Ver 827?

        public override void WriteChunk()
        {
            Log($"*** Controller Chunk ***");
            Log($"Version:                 {Version:X}");
            Log($"ID:                      {ID:X}");
            Log($"Number of Keys:          {NumKeys}");
            Log($"Controller Type:         {ControllerType}");
            Log($"Conttroller Flags:       {ControllerFlags}");
            Log($"Controller ID:           {ControllerID}");
            for (var i = 0; i < NumKeys; i++)
            {
                Log($"        Key {i}:       Time: {Keys[i].Time}");
                Log($"        AbsPos {i}:    {Keys[i].AbsPos.x:F7}, {Keys[i].AbsPos.y:F7}, {Keys[i].AbsPos.z:F7}");
                Log($"        RelPos {i}:    {Keys[i].RelPos.x:F7}, {Keys[i].RelPos.y:F7}, {Keys[i].RelPos.z:F7}");
            }
        }
    }
}
