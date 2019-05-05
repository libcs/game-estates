using Gamer.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gamer.Estate.Rsi.FilePack
{
    public partial class PakFile : IDisposable
    {
        public class FileMetadata
        {
            internal string Path;
            internal bool Compressed;
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

        void ReadMetadata()
        {
            _r.BaseStream.Seek(0, SeekOrigin.Begin);
            var buf = new byte[16];
            var header = new byte[HeaderMagic.Length];
            // read bytes from the file
            while (_r.Read(buf, 0, 16) != 0)
            {
                // Create a header byte array and copy the first 4 bytes from the buffer
                Array.Copy(buf, 0, header, 0, 4);
                if (!header.SequenceEqual(HeaderMagic))
                    continue;
                var compressed = BitConverter.ToInt16(buf, 8) == 0x64;
                _r.Read(buf, 0, 16);
                var fileNameSize = BitConverter.ToInt16(buf, 0xA);
                var extraFieldSize = BitConverter.ToInt16(buf, 0xC);

                //Get the file name
                var fileNameBytes = new byte[fileNameSize];
                Array.Copy(buf, 0xE, fileNameBytes, 0, 2);
                int nameChunks = (int)Math.Ceiling((decimal)(fileNameSize - 2) / 16), fileNameIndex = 2, lastCharIndex = 0;
                while (fileNameIndex < fileNameSize)
                {
                    _r.Read(buf, 0, 16);
                    for (var i = 0; i < buf.Length; i++)
                    {
                        if (fileNameIndex >= fileNameSize)
                        {
                            lastCharIndex = i;
                            break;
                        }
                        fileNameBytes[fileNameIndex] = buf[i];
                        fileNameIndex++;
                    }
                }
                var fileName = Encoding.ASCII.GetString(fileNameBytes);
                var extraFieldChunkStart = _r.Position;

                // Get the file size
                var fileSizeBytes = new byte[4];
                lastCharIndex--;
                if (lastCharIndex < 3 && lastCharIndex >= 0)
                {
                    Array.Copy(buf, lastCharIndex + 13, fileSizeBytes, 0, 3 - lastCharIndex);
                    _r.Read(buf, 0, 16);
                    Array.Copy(buf, 0, fileSizeBytes, 3 - lastCharIndex, lastCharIndex + 1);
                }
                else if (lastCharIndex < 15 && lastCharIndex >= 3)
                {
                    _r.Read(buf, 0, 16);
                    Array.Copy(buf, lastCharIndex - 3, fileSizeBytes, 0, 4);
                }
                else
                {
                    _r.Read(buf, 0, 16);
                    extraFieldChunkStart = _r.Position;
                    Array.Copy(buf, 12, fileSizeBytes, 0, 4);
                }
                var fileSize = BitConverter.ToInt32(fileSizeBytes, 0);

                // get file data
                var chunkOffset = _r.Position - extraFieldChunkStart;
                var garbageLength = ((int)Math.Ceiling((decimal)extraFieldSize / 16) - 1) * 16 - chunkOffset;
                _r.ReadBytes((int)garbageLength);

                // read file data
                var file = new byte[fileSize];
                _r.Read(file, 0, fileSize);
                _r.ReadBytes(16 - (fileSize % 16));
                Console.WriteLine(fileName);

                // add
                _files.Add(new FileMetadata
                {
                    Path = fileName,
                    Compressed = compressed,
                });
            }
        }
    }
}