using System.Text;
using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkExportFlags : Chunk  // cccc0015:  Export Flags
    {
        public uint ChunkOffset;  // for some reason the offset of Export Flag chunk is stored here.
        public uint Flags;    // ExportFlags type technically, but it's just 1 value
        public uint[] RCVersion;  // 4 uints
        public string RCVersionString;  // Technically String16

        public override void WriteChunk()
        {
            Log($"*** START EXPORT FLAGS ***");
            Log($"    Export Chunk ID: {ID:X}");
            Log($"    ChunkType: {ChunkType}");
            Log($"    Version: {Version}");
            Log($"    Flags: {Flags}");
            var b = new StringBuilder("    RC Version: ");
            for (var i = 0; i < 4; i++)
                b.Append(RCVersion[i]);
            Log(b.ToString());
            Log();
            Log("    RCVersion String: {RCVersionString}");
            Log("*** END EXPORT FLAGS ***");
        }
    }
}
