using Gamer.Proxy;
using System;
using System.Threading.Tasks;
using static Gamer.Estate.Rsi.FilePack.PakFile;

namespace Gamer.Estate.Rsi.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly ProxySink _proxySink;
        readonly PakFile _pakFile;

        public ResFile(ProxySink proxySink, PakFile pakFile)
        {
            _proxySink = proxySink;
            if (proxySink is ProxySinkClient)
                return;
            _pakFile = pakFile;
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() => _pakFile?.Close();

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _proxySink.ContainsFile(filePath, () => _pakFile.ContainsFile(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _proxySink.LoadFileDataAsync(filePath, () => _pakFile.LoadFileDataAsync(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        internal Task<byte[]> LoadFileDataAsync(FileMetadata file) => _pakFile.LoadFileDataAsync(file);
    }
}