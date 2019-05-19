using Gamer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry.FilePack
{
    public partial class PakFile : IDisposable
    {
        public class FileMetadata
        {
            public long Position;
            public bool Compressed;
            public string Path;
            public int Size;
        }

        public override string ToString() => $"{Path.GetFileName(FilePath)}";
        GenericReader _r;
        internal List<FileMetadata> _files;
        ILookup<string, FileMetadata> _filesByPath;
        public string FilePath;

        public bool IsAtEof => _r.Position >= _r.BaseStream.Length;

        public PakFile(string filePath)
        {
            if (filePath == null)
                return;
            FilePath = filePath;
            _r = new BinaryFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read));
            ReadMetadata();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~PakFile() => Close();

        public void Close()
        {
            _r?.Close();
            _r = null;
        }

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => new HashSet<string>(_files.Select(x => x.Path));

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _filesByPath.Contains(filePath);

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath)
        {
            var files = _filesByPath[filePath].ToArray();
            if (files.Length == 0)
                throw new NotSupportedException();
            if (files.Length == 1)
                return LoadFileDataAsync(files[0]);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        internal Task<byte[]> LoadFileDataAsync(FileMetadata file)
        {
            throw new NotSupportedException();
        }

        void ReadMetadata()
        {
        }
    }
}