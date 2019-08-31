using Game.Core.Netstream;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Game.Estate.Cry.FilePack.PakFile;

namespace Game.Estate.Cry.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly StreamSink _streamSink;
        readonly IPakFile _pakFile;

        public ResFile(StreamSink streamSink, IPakFile pakFile)
        {
            _streamSink = streamSink;
            if (streamSink is StreamSinkClient)
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
        public HashSet<string> GetContainsSet() => _streamSink.GetContainsSet(() => _pakFile.GetContainsSet());

        /// <summary>
        /// Determines whether the PAK archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _streamSink.ContainsFile(filePath, () => _pakFile.ContainsFile(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _streamSink.LoadFileDataAsync(filePath, () => _pakFile.LoadFileDataAsync(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public async Task<SocPakFile> LoadSocPackAsync(string filePath) => new SocPakFile(filePath, new MemoryStream(await _streamSink.LoadFileDataAsync(filePath, () => _pakFile.LoadFileDataAsync(filePath))));
    }
}