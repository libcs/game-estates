using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX.FilePack
{
    //http://jfregnault.free.fr/Ultima9/Ultima9.htm
    //http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats#FLX_Format
    //https://sites.google.com/site/burtonradons/eee/platforms/single-games/ultima-ix/file-formats
    public partial class ResFile : IDisposable
    {
        readonly IFlxFile _flxFile;
        readonly IIdxFile _idxFile;
        public readonly string FilePath;
        readonly string _prefix;
        readonly int _prefixLength;

        public ResFile(string filePath)
        {
            if (filePath == null)
                return;
            FilePath = filePath;
            _prefix = GetPrefix(filePath);
            _prefixLength = _prefix.Length;
            _flxFile = new FlxFile(filePath);
            _idxFile = null;
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() { _flxFile?.Close(); _idxFile?.Close(); }

        static string GetPrefix(string filePath)
            => filePath.Contains("sappear.flx") ? "sappear/"
            : filePath.Contains("bitmap") ? "bitmap/"
            : filePath.Contains("texture") || filePath.Contains("Texture") ? "texture/"
            : throw new ArgumentOutOfRangeException(nameof(filePath), filePath);

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() =>
            _flxFile != null ? _flxFile.GetContainsSet()
            : _idxFile != null ? _idxFile.GetContainsSet()
            : throw new InvalidOperationException("Unknown type");

        /// <summary>
        /// Determines whether the archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => !filePath.StartsWith(_prefix) ? false
            : _flxFile != null ? _flxFile.ContainsFile(filePath.Substring(_prefixLength))
            : _idxFile != null ? _idxFile.ContainsFile(filePath.Substring(_prefixLength))
            : throw new InvalidOperationException("Unknown type");

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath) => !filePath.StartsWith(_prefix) ? Task.FromResult<byte[]>(null)
            : _flxFile != null ? _flxFile.LoadFileDataAsync(filePath.Substring(_prefixLength))
            : _idxFile != null ? _idxFile.LoadFileDataAsync(filePath.Substring(_prefixLength))
            : throw new InvalidOperationException("Unknown type");
    }
}