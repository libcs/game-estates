using Game.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX.FilePack
{
    public interface IFlxFile : IDisposable
    {
        void Close();
        HashSet<string> GetContainsSet();
        bool ContainsFile(string filePath);
        Task<byte[]> LoadFileDataAsync(string filePath);
    }

    public partial class FlxFile : IFlxFile
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

        public FlxFile(string filePath)
        {
            if (filePath == null)
                return;
            FilePath = filePath;
            _r = new BinaryFileReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
            ReadMetadata();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~FlxFile() => Close();

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
        /// Determines whether the PAK archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _filesByPath.Contains(filePath.Replace("/", "\\"));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath)
        {
            var files = _filesByPath[filePath.Replace("/", "\\")].ToArray();
            if (files.Length == 1)
                return LoadFileDataAsync(files[0]);
            Debug.Log($"LoadFileDataAsync: {filePath} @ {files.Length}");
            if (files.Length == 0)
                throw new FileNotFoundException(filePath);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        internal Task<byte[]> LoadFileDataAsync(FileMetadata file)
        {
            if (file.Size == 0)
                return Task.FromResult<byte[]>(null);
            var buf = new byte[file.Size];
            lock (_r)
            {
                _r.Position = file.Position;
                _r.Read(buf, 0, buf.Length);
            }
            return Task.FromResult(buf);
        }

        void ReadMetadata()
        {
            _r.BaseStream.Seek(0x50, SeekOrigin.Begin);
            var fileCount = _r.ReadInt32();

            _r.BaseStream.Seek(0x80, SeekOrigin.Begin);
            _files = new List<FileMetadata>();
            var chunk = new byte[8];
            
            // read in 16 bytes chunks
            for (var i = 0; i < fileCount; i++)
            {
                _r.Read(chunk, 0, 8);
                _files.Add(new FileMetadata
                {
                    Position = BitConverter.ToInt32(chunk, 0),
                    Path = $"{i}",
                    Size = BitConverter.ToInt32(chunk, 4),
                });
            }

            // files by path
            _filesByPath = _files.ToLookup(x => x.Path, StringComparer.OrdinalIgnoreCase);
        }
    }
}