using Gamer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Gamer.Core.Debug;

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
        List<FileMetadata> _files;
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
        public byte[] LoadFileData(FileMetadata file)
        {
            return null;
            //byte[] decompFile = null;
            ////Console.WriteLine(fileName);
            ////Console.WriteLine(fileSize + " Bytes");

            //if (BitConverter.ToInt16(compressionMethodBytes, 0) == 0x64)
            //{
            //    using (var decompressor = new Decompressor())
            //    {
            //        try
            //        {
            //            decompFile = decompressor.Unwrap(file);
            //            if (Path.GetDirectoryName(fileName) != "")
            //                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            //            using (FileStream fs2 = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            //            {
            //                fs2.Write(decompFile, 0, decompFile.Length);
            //            }
            //        }
            //        catch (ZstdException e)
            //        {
            //            Console.Write("\rSkipping the following file because it is broken. Size code: " + BitConverter.ToString(fileSizeBytes));
            //            Console.Write("\nLast char index: " + lastCharIndex);
            //            Console.Write("\nError: " + e.Message + "\n");
            //            //Console.ReadLine();
            //        }
            //    }
            //}
            //else
            //{
            //    decompFile = file;
            //    if (Path.GetDirectoryName(fileName) != "")
            //        Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            //    using (FileStream fs2 = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            //    {
            //        fs2.Write(decompFile, 0, decompFile.Length);
            //    }
            //}
        }

        static readonly byte[] HeaderMagic = { 0x50, 0x4B, 0x03, 0x14 };

//#define ROUND64_(x)			(((x)+15)&~15)
        void ReadMetadata()
        {
            _files = new List<FileMetadata>();
            _r.BaseStream.Seek(0, SeekOrigin.Begin);

            //
            var chunk = new byte[16];
            var buf = new byte[HeaderMagic.Length];
            var idx = 0;
            // read bytes from the file
            while (_r.Read(chunk, 0, 16) != 0)
            {
                // Create a header byte array and copy the first 4 bytes from the buffer
                Array.Copy(chunk, 0, buf, 0, 4);
                if (!buf.Take(4).SequenceEqual(HeaderMagic))
                    continue;
                var compressed = BitConverter.ToInt16(chunk, 8) == 0x64;
                _r.Read(chunk, 0, 16);
                var fileNameSize = BitConverter.ToInt16(chunk, 0xA);
                var extraFieldSize = BitConverter.ToInt16(chunk, 0xC);

                // file name
                if (fileNameSize > buf.Length) buf = new byte[fileNameSize];
                Array.Copy(chunk, 0xE, buf, 0, 2);
                int nameChunks = (int)Math.Ceiling((decimal)(fileNameSize - 2) / 16), lastCharIndex = 0;
                idx = 2;
                while (idx < fileNameSize)
                {
                    _r.Read(chunk, 0, 16);
                    for (var i = 0; i < chunk.Length; i++)
                    {
                        if (idx >= fileNameSize)
                        {
                            lastCharIndex = i;
                            break;
                        }
                        buf[idx] = chunk[i];
                        idx++;
                    }
                }
                var fileName = Encoding.ASCII.GetString(buf, 0, idx);
                var extraFieldChunkStart = _r.Position;

                // Get the file size
                lastCharIndex--;
                if (lastCharIndex < 3 && lastCharIndex >= 0)
                {
                    Array.Copy(chunk, lastCharIndex + 13, buf, 0, 3 - lastCharIndex);
                    _r.Read(chunk, 0, 16);
                    Array.Copy(chunk, 0, buf, 3 - lastCharIndex, lastCharIndex + 1);
                }
                else if (lastCharIndex < 15 && lastCharIndex >= 3)
                {
                    _r.Read(chunk, 0, 16);
                    Array.Copy(chunk, lastCharIndex - 3, buf, 0, 4);
                }
                else
                {
                    _r.Read(chunk, 0, 16);
                    extraFieldChunkStart = _r.Position;
                    Array.Copy(chunk, 12, buf, 0, 4);
                }
                var fileSize = BitConverter.ToInt32(buf, 0);

                // get file data
                var chunkOffset = _r.Position - extraFieldChunkStart;
                var garbageLength = ((int)Math.Ceiling((decimal)extraFieldSize / 16) - 1) * 16 - chunkOffset;
                _r.Position += garbageLength;

                // add
                _files.Add(new FileMetadata
                {
                    Position = _r.Position,
                    Compressed = compressed,
                    Path = fileName,
                    Size = fileSize,
                });
                Log($"{fileName} - {fileSize} Bytes");

                // skip file data
                _r.Position += fileSize + (16 - (fileSize % 16));

                //// read file data
                //var file = new byte[fileSize];
                //_r.Read(file, 0, fileSize);
                //_r.ReadBytes(16 - (fileSize % 16));
                //
            }
        }
    }
}