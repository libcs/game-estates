using Gamer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZstdNet;

namespace Gamer.Estate.Rsi.FilePack
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
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _filesByPath.Contains(filePath);

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public byte[] LoadFileData(string filePath)
        {
            var files = _filesByPath[filePath].ToArray();
            if (files.Length == 0)
                throw new NotSupportedException();
            if (files.Length == 1)
                return LoadFileData(files[0]);
            throw new NotSupportedException();
        }

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        internal byte[] LoadFileData(FileMetadata file)
        {
            var buf = new byte[file.Size];
            lock (_r)
            {
                _r.Position = file.Position;
                _r.Read(buf, 0, buf.Length);
            }
            if (file.Compressed)
                using (var decompressor = new Decompressor())
                {
                    try { return decompressor.Unwrap(buf); }
                    catch (ZstdException e)
                    {
                        Console.WriteLine($"Skipping the following file because it is broken. Size: {file.Size}");
                        Console.WriteLine($"Error: {e.Message}");
                        return null;
                    }
                }
            return buf;
        }

        static readonly byte[] HeaderMagic = { 0x50, 0x4B, 0x03, 0x14 };

        void ReadMetadata()
        {
            _r.BaseStream.Seek(0, SeekOrigin.Begin);
            _files = new List<FileMetadata>();
            byte[] chunk = new byte[16], buf = new byte[100];

            // read in 16 bytes chunks
            while (_r.Read(chunk, 0, 16) != 0)
            {
                if (chunk[0] != HeaderMagic[0] || chunk[1] != HeaderMagic[1] || chunk[2] != HeaderMagic[2] || chunk[3] != HeaderMagic[3])
                    continue;
                // file found
                var compressed = BitConverter.ToInt16(chunk, 8) == 0x64;
                _r.Read(chunk, 0, 14); //: minus 2
                var fileNameSize = BitConverter.ToInt16(chunk, 0xA);
                var extraFieldSize = BitConverter.ToInt16(chunk, 0xC);

                // file name
                var fileNameRead = 2 + (((fileNameSize - 2) + 15) & ~15) + 16;
                if (fileNameRead > buf.Length) buf = new byte[fileNameRead];
                _r.Read(buf, 0, fileNameRead);
                var fileName = Encoding.ASCII.GetString(buf, 0, fileNameSize);
                var charIdx = (fileNameSize - 2) % 16;

                // file size
                var fileSize = BitConverter.ToInt32(buf, fileNameSize + 12);

                // skip extra
                var extraFieldRead = ((extraFieldSize + 15) & ~15) - (charIdx != 0 ? 32 : 16);
                _r.Skip(extraFieldRead); //: var extraField = new byte[extraFieldRead]; _r.Read(extraField, 0, extraField.Length);

                // add
                _files.Add(new FileMetadata
                {
                    Position = _r.Position,
                    Compressed = compressed,
                    Path = fileName,
                    Size = fileSize,
                });

                // file data
                _r.Skip(fileSize + (16 - (fileSize % 16))); //: var file = new byte[fileSize]; _r.Read(file, 0, fileSize); _r.Position += 16 - (fileSize % 16);
            }

            // files by path
            _filesByPath = _files.ToLookup(x => x.Path);
        }
    }
}