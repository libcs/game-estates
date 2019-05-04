using Gamer.Base.Core;
using System.IO;

namespace Gamer.Estate.Ultima.Resources.IO
{
    public abstract class AFileIndex
    {
        public FileIndexEntry3D[] Index { get; private set; }
        public Stream Stream { get; private set; }

        public string DataPath { get; private set; }
        public int Length { get; protected set; }

        protected abstract FileIndexEntry3D[] ReadEntries();

        protected AFileIndex(string dataPath)
        {
            DataPath = dataPath;
        }

        protected AFileIndex(string dataPath, int length)
        {
            Length = length;
            DataPath = dataPath;
        }

        public void Open()
        {
            Index = ReadEntries();
            Length = Index.Length;
        }

        public BinaryFileReader Seek(int index, out int length, out int extra, out bool patched)
        {
            if (Stream == null)
                Stream = new FileStream(DataPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (index < 0 || index >= Index.Length)
            {
                length = extra = 0;
                patched = false;
                return null;
            }
            var e = Index[index];
            if (e.Lookup < 0)
            {
                length = extra = 0;
                patched = false;
                return null;
            }
            length = e.Length & 0x7FFFFFFF;
            extra = e.Extra;
            if ((e.Length & 0xFF000000) != 0)
            {
                patched = true;
                VerData.Stream.Seek(e.Lookup, SeekOrigin.Begin);
                return new BinaryFileReader(VerData.Stream);
            }
            else if (Stream == null)
            {
                length = extra = 0;
                patched = false;
                return null;
            }
            patched = false;
            Stream.Position = e.Lookup;
            return new BinaryFileReader(Stream);
        }
    }
}
