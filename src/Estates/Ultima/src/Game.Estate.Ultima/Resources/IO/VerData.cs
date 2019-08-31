using System.IO;

namespace Game.Estate.Ultima.Resources.IO
{
    public class VerData
    {
        public static Stream Stream { get; private set; }
        public static FileIndexEntry5D[] Patches { get; private set; }

        static VerData()
        {
            var path = UltimaFileManager.GetFilePath("verdata.mul");
            if (!File.Exists(path))
            {
                Patches = new FileIndexEntry5D[0];
                Stream = Stream.Null;
            }
            else
            {
                Stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var bin = new BinaryReader(Stream);
                Patches = new FileIndexEntry5D[bin.ReadInt32()];
                for (var i = 0; i < Patches.Length; ++i)
                {
                    Patches[i].file = bin.ReadInt32();
                    Patches[i].index = bin.ReadInt32();
                    Patches[i].lookup = bin.ReadInt32();
                    Patches[i].length = bin.ReadInt32();
                    Patches[i].extra = bin.ReadInt32();
                }
            }
        }
    }
}