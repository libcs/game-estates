using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry.FilePack
{
    public partial class SocPakFile : IPakFile
    {
        public override string ToString() => $"{Path.GetFileName(FilePath)}";
        ZipArchive _r;
        IDictionary<string, ZipArchiveEntry> _filesByPath;
        public string FilePath;

        public SocPakFile(string filePath, Stream stream = null)
        {
            if (stream == null && File.Exists(filePath))
                stream = new FileStream(filePath, FileMode.Open);
            _r = new ZipArchive(stream, ZipArchiveMode.Read);
            FilePath = filePath;
            ReadMetadata();
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~SocPakFile() => Close();

        public void Close()
        {
            _r?.Dispose();
            _r = null;
        }

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => new HashSet<string>(_r.Entries.Select(x => x.FullName));

        /// <summary>
        /// Determines whether the PAK archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _filesByPath.ContainsKey(filePath.Replace("/", "\\"));

        void ReadMetadata() => _filesByPath = _r.Entries.ToDictionary(x => x.FullName, StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public async Task<byte[]> LoadFileDataAsync(string filePath)
        {
            var file = _filesByPath[filePath.Replace("/", "\\")];
            var r = new MemoryStream();
            using (var s = file.Open())
                await s.CopyToAsync(r);
            return r.ToArray();
        }
    }
}