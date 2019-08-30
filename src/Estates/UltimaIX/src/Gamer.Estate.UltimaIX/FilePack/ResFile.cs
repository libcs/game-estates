using Gamer.Proxy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamer.Estate.UltimaIX.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly ProxySink _proxySink;
        readonly IFlxFile _flxFile;

        public ResFile(ProxySink proxySink, IFlxFile pakFile)
        {
            _proxySink = proxySink;
            if (proxySink is ProxySinkClient)
                return;
            _flxFile = pakFile;
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() => _flxFile?.Close();

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => _proxySink.GetContainsSet(() => _flxFile.GetContainsSet());

        /// <summary>
        /// Determines whether the PAK archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _proxySink.ContainsFile(filePath, () => _flxFile.ContainsFile(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _proxySink.LoadFileDataAsync(filePath, () => _flxFile.LoadFileDataAsync(filePath));
    }
}