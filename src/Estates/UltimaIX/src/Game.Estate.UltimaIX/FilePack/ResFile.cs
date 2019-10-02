using Game.Core.Netstream;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly StreamSink _streamSink;
        readonly IFlxFile _flxFile;
        readonly IIdxFile _idxFile;

        public ResFile(StreamSink streamSink, IFlxFile flxFile, IIdxFile idxFile)
        {
            _streamSink = streamSink;
            if (streamSink is StreamSinkClient)
                return;
            _flxFile = flxFile;
            _idxFile = idxFile;
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() { _flxFile?.Close(); _idxFile?.Close(); }

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => _streamSink.GetContainsSet(() => _flxFile != null
            ? _flxFile.GetContainsSet()
            : _idxFile.GetContainsSet());

        /// <summary>
        /// Determines whether the PAK archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _streamSink.ContainsFile(filePath, () => _flxFile != null
            ? _flxFile.ContainsFile(filePath)
            : _idxFile.ContainsFile(filePath));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath) => _streamSink.LoadFileDataAsync(filePath, () => _flxFile != null
            ? _flxFile.LoadFileDataAsync(filePath)
            : _idxFile.LoadFileDataAsync(filePath));
    }
}