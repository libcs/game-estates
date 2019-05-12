using System;
using static Gamer.Estate.Cry.FilePack.PakFile;

namespace Gamer.Estate.Cry.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly PakFile _pakFile;

        public ResFile(PakFile pakFile) => _pakFile = pakFile;

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() => _pakFile.Close();

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _pakFile.ContainsFile(filePath);

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public byte[] LoadFileData(string filePath) => _pakFile.LoadFileData(filePath);

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        internal byte[] LoadFileData(FileMetadata file) => _pakFile.LoadFileData(file);
    }
}