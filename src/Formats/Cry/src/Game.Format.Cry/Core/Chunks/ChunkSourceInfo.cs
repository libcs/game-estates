using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkSourceInfo : Chunk  // cccc0013:  Source Info chunk.  Pretty useless overall
    {
        public string SourceFile;
        public string Date;
        public string Author;

        public override void WriteChunk()
        {
            Log($"*** SOURCE INFO CHUNK ***");
            Log($"    ID: {ID:X}");
            Log($"    Sourcefile: {SourceFile}.");
            Log($"    Date:       {Date}.");
            Log($"    Author:     {Author}.");
            Log($"*** END SOURCE INFO CHUNK ***");
        }
    }
}
