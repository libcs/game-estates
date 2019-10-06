using Game.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX.FilePack
{
    public interface IIdxFile : IDisposable
    {
        void Close();
        HashSet<string> GetContainsSet();
        bool ContainsFile(string filePath);
        Task<byte[]> LoadFileDataAsync(string filePath);
    }

    //http://wiki.ultimacodex.com/wiki/Ultima_IX_Internal_Formats
    public partial class IdxFile : IIdxFile
    {
        public override string ToString() => $"{Path.GetFileName(FilePath)}";
        GenericReader _r;
        public string FilePath;
        int _size;
        int _files;

        public bool IsAtEof => _r.Position >= _r.BaseStream.Length;

        public IdxFile(string filePath, int size)
        {
            if (filePath == null)
                return;
            FilePath = filePath;
            _r = new BinaryFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
            ReadMetadata(size);
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~IdxFile() => Close();

        public void Close()
        {
            _r?.Close();
            _r = null;
        }

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => new HashSet<string>() { "*" };

        /// <summary>
        /// Determines whether the archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => int.TryParse(filePath, out var file) && file < _files;

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath)
        {
            if (int.TryParse(filePath, out var file) && file < _files)
                return LoadFileDataAsync(file);
            CoreDebug.Log($"LoadFileDataAsync: {filePath} @ {_files}");
            throw new FileNotFoundException(filePath);
        }

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        internal Task<byte[]> LoadFileDataAsync(int file)
        {
            var buf = new byte[_size];
            lock (_r)
            {
                _r.Position = _size * file;
                _r.Read(buf, 0, buf.Length);
            }
            return Task.FromResult(buf);
        }

        void ReadMetadata(int size)
        {
            _size = size;
            _files = (int)(_r.BaseStream.Length / _size);
        }
    }
}