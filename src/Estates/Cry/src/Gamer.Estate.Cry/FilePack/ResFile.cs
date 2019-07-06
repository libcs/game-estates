using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Gamer.Estate.Cry.FilePack.PakFile;

namespace Gamer.Estate.Cry.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly ProxySink _proxySink;
        readonly IPakFile _pakFile;

        public ResFile(ProxySink proxySink, IPakFile pakFile)
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
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => _proxySink.GetContainsSet(() => _pakFile.GetContainsSet());

        /// <summary>
        /// Determines whether the PAK archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _proxySink.ContainsFile(filePath, () => _pakFile.ContainsFile(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _proxySink.LoadFileDataAsync(filePath, () => _pakFile.LoadFileDataAsync(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public async Task<SocPakFile> LoadSocPackAsync(string filePath) => new SocPakFile(filePath, new MemoryStream(await _proxySink.LoadFileDataAsync(filePath, () => _pakFile.LoadFileDataAsync(filePath))));
    }
}