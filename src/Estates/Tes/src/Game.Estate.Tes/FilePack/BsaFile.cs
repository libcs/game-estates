﻿using Game.Core;
using Game.Core.Format;
using Game.Core.Netstream;
using ICSharpCode.SharpZipLib.Lzw;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Estate.Tes.FilePack
{
    public partial class BsaFile : IDisposable
    {
        #region Types

        // Default header data
        const uint MW_BSAHEADER_FILEID = 0x00000100; // Magic for Morrowind BSA
        const uint OB_BSAHEADER_FILEID = 0x00415342; // Magic for Oblivion BSA, the literal string "BSA\0".
        const uint F4_BSAHEADER_FILEID = 0x58445442; // Magic for Fallout 4 BA2, the literal string "BTDX".
        const uint OB_BSAHEADER_VERSION = 0x67; // Version number of an Oblivion BSA
        const uint F3_BSAHEADER_VERSION = 0x68; // Version number of a Fallout 3 BSA
        const uint SSE_BSAHEADER_VERSION = 0x69; // Version number of a Skyrim SE BSA
        const uint F4_BSAHEADER_VERSION = 0x01; // Version number of a Fallout 4 BA2

        // Archive flags
        const ushort OB_BSAARCHIVE_PATHNAMES = 0x0001; // Whether the BSA has names for paths
        const ushort OB_BSAARCHIVE_FILENAMES = 0x0002; // Whether the BSA has names for files
        const ushort OB_BSAARCHIVE_COMPRESSFILES = 0x0004; // Whether the files are compressed
        const ushort F3_BSAARCHIVE_PREFIXFULLFILENAMES = 0x0100; // Whether the name is prefixed to the data?

        // File flags
        const ushort OB_BSAFILE_NIF = 0x0001; // Set when the BSA contains NIF files
        const ushort OB_BSAFILE_DDS = 0x0002; // Set when the BSA contains DDS files
        const ushort OB_BSAFILE_XML = 0x0004; // Set when the BSA contains XML files
        const ushort OB_BSAFILE_WAV = 0x0008; // Set when the BSA contains WAV files
        const ushort OB_BSAFILE_MP3 = 0x0010; // Set when the BSA contains MP3 files
        const ushort OB_BSAFILE_TXT = 0x0020; // Set when the BSA contains TXT files
        const ushort OB_BSAFILE_HTML = 0x0020; // Set when the BSA contains HTML files
        const ushort OB_BSAFILE_BAT = 0x0020; // Set when the BSA contains BAT files
        const ushort OB_BSAFILE_SCC = 0x0020; // Set when the BSA contains SCC files
        const ushort OB_BSAFILE_SPT = 0x0040; // Set when the BSA contains SPT files
        const ushort OB_BSAFILE_TEX = 0x0080; // Set when the BSA contains TEX files
        const ushort OB_BSAFILE_FNT = 0x0080; // Set when the BSA contains FNT files
        const ushort OB_BSAFILE_CTL = 0x0100; // Set when the BSA contains CTL files

        // Bitmasks for the size field in the header
        const uint OB_BSAFILE_SIZEMASK = 0x3FFFFFFF; // Bit mask with OBBSAFileInfo::sizeFlags to get the size of the file

        // Record flags
        const uint OB_BSAFILE_FLAG_COMPRESS = 0xC0000000; // Bit mask with OBBSAFileInfo::sizeFlags to get the compression status

        public class FileMetadata
        {
            // Skyrim and earlier
            public uint SizeFlags; // The size of the file in the BSA
            // Fallout 4
            public uint PackedSize;
            public uint UnpackedSize;
            //
            public long Offset; // The offset of the file in the BSA
            public F4Tex Tex;
            //
            public string Path;
            public ulong PathHash;
            // The size of the file inside the BSA
            public uint Size => SizeFlags > 0 ?
                        // Skyrim and earlier
                        SizeFlags & OB_BSAFILE_SIZEMASK :
                        // TODO: Not correct for texture BA2s
                        PackedSize == 0 ? UnpackedSize : PackedSize;
            // Whether the file is compressed inside the BSA
            public bool Compressed => (SizeFlags & OB_BSAFILE_FLAG_COMPRESS) != 0;
        }

        public struct F4Tex
        {
            public ushort Height;
            public ushort Width;
            public byte NumMips;
            public DXGIFormat Format;
            public ushort Unk16;
            public F4TexChunk[] Chunks;
        }

        public struct F4TexChunk
        {
            public ulong Offset;
            public uint PackedSize;
            public uint UnpackedSize;
            public ushort StartMip;
            public ushort EndMip;
            public uint Unk14;
        }

        #endregion

        public override string ToString() => $"{Path.GetFileName(FilePath)}";
        GenericReader _r;
        public uint Magic; // 4 bytes
        public uint Version; // 4 bytes
        bool _compressToggle; // Whether the %BSA is compressed
        bool _hasNamePrefix; // Whether Fallout 3 names are prefixed with an extra string
        FileMetadata[] _files;
        ILookup<ulong, FileMetadata> _filesByHash;
        public string FilePath;

        public bool IsAtEof => _r.Position >= _r.BaseStream.Length;

        public BsaFile(string filePath)
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
        ~BsaFile() => Close();

        public void Close()
        {
            _r?.Close();
            _r = null;
        }

        /// <summary>
        /// Gets the contains set.
        /// </summary>
        /// <returns></returns>
        public HashSet<string> GetContainsSet() => new HashSet<string>(_files.Select(x => x.Path), StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Determines whether the BSA archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => _filesByHash.Contains(HashFilePath(filePath.Replace('\\', '/')));

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath)
        {
            filePath = filePath.Replace('\\', '/');
            var files = _filesByHash[HashFilePath(filePath)].ToArray();
            if (files.Length == 0)
                throw new FileNotFoundException($"Could not find file \"{filePath}\" in a BSA file.");
            if (files.Length == 1)
                return LoadFileDataAsync(files[0]);
            var file = files.FirstOrDefault(x => string.Equals(x.Path, filePath, StringComparison.OrdinalIgnoreCase));
            if (file != null)
                return LoadFileDataAsync(file);
            throw new FileNotFoundException($"Could not find file \"{filePath}\" in a BSA file.");
        }

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        Task<byte[]> LoadFileDataAsync(FileMetadata file)
        {
            var fileSize = (int)file.Size;
            byte[] fileData;
            bool bsaCompressed;
            int newFileSize;
            lock (_r)
            {
                _r.Position = file.Offset;
                if (_hasNamePrefix)
                {
                    var len = _r.ReadByte();
                    fileSize -= len + 1;
                    _r.Position = file.Offset + 1 + len;
                }
                fileData = _r.ReadBytes(fileSize);
                bsaCompressed = file.SizeFlags > 0 && file.Compressed ^ _compressToggle;
                newFileSize = Version == SSE_BSAHEADER_VERSION && bsaCompressed ? _r.ReadInt32() - 4 : fileSize;
            }
            // BSA
            if (bsaCompressed)
            {
                var newFileData = new byte[newFileSize];
                if (Version != SSE_BSAHEADER_VERSION)
                {
                    if (fileData.Length > 4)
                        using (var s = new MemoryStream(fileData, 4, fileSize - 4))
                        using (var gs = new InflaterInputStream(s))
                            gs.Read(newFileData, 0, newFileData.Length);
                    else newFileData = fileData;
                }
                else
                {
                    using (var s = new MemoryStream(fileData))
                    using (var gs = new Lzw​Input​Stream(s))
                        gs.Read(newFileData, 0, newFileData.Length);
                }
                fileData = newFileData;
            }
            // General BA2
            else if (file.PackedSize > 0 && file.Tex.Chunks == null)
            {
                var newFileData = new byte[file.UnpackedSize];
                using (var s = new MemoryStream(fileData))
                using (var gs = new InflaterInputStream(s))
                    gs.Read(newFileData, 0, newFileData.Length);
                fileData = newFileData;
            }
            // Fill DDS Header
            else if (file.Tex.Chunks != null)
            {
                // Fill DDS Header
                var ddsHeader = new DDSHeader
                {
                    dwFlags = DDSFlags.HEADER_FLAGS_TEXTURE | DDSFlags.HEADER_FLAGS_LINEARSIZE | DDSFlags.HEADER_FLAGS_MIPMAP,
                    dwHeight = file.Tex.Height,
                    dwWidth = file.Tex.Width,
                    dwMipMapCount = file.Tex.NumMips,
                    dwCaps = DDSCaps.SURFACE_FLAGS_TEXTURE | DDSCaps.SURFACE_FLAGS_MIPMAP,
                    dwCaps2 = file.Tex.Unk16 == 2049 ? DDSCaps2.CUBEMAP_ALLFACES : 0,
                };
                var dx10Header = new DDSHeader_DXT10();
                var dx10 = false;
                // map tex format
                switch (file.Tex.Format)
                {
                    case DXGIFormat.BC1_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DXT1");
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height / 2U; // 4bpp
                        break;
                    case DXGIFormat.BC2_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DXT3");
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height; // 8bpp
                        break;
                    case DXGIFormat.BC3_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DXT5");
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height; // 8bpp
                        break;
                    case DXGIFormat.BC5_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("ATI2");
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height; // 8bpp
                        break;
                    case DXGIFormat.BC7_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.FourCC;
                        ddsHeader.ddspf.dwFourCC = Encoding.ASCII.GetBytes("DX10");
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height; // 8bpp
                        dx10 = true;
                        dx10Header.dxgiFormat = DXGIFormat.BC7_UNORM;
                        break;
                    case DXGIFormat.DXGI_FORMAT_B8G8R8A8_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.RGB | DDSPixelFormats.AlphaPixels;
                        ddsHeader.ddspf.dwRGBBitCount = 32;
                        ddsHeader.ddspf.dwRBitMask = 0x00FF0000;
                        ddsHeader.ddspf.dwGBitMask = 0x0000FF00;
                        ddsHeader.ddspf.dwBBitMask = 0x000000FF;
                        ddsHeader.ddspf.dwABitMask = 0xFF000000;
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height * 4; // 32bpp
                        break;
                    case DXGIFormat.DXGI_FORMAT_R8_UNORM:
                        ddsHeader.ddspf.dwFlags = DDSPixelFormats.RGB;
                        ddsHeader.ddspf.dwRGBBitCount = 8;
                        ddsHeader.ddspf.dwRBitMask = 0xFF;
                        ddsHeader.dwPitchOrLinearSize = (uint)file.Tex.Width * file.Tex.Height; // 8bpp
                        break;
                    default: throw new InvalidOperationException("DDS FAILED");
                }
                if (dx10)
                {
                    dx10Header.resourceDimension = DDSDimension.Texture2D;
                    dx10Header.miscFlag = 0;
                    dx10Header.arraySize = 1;
                    dx10Header.miscFlags2 = 0;
                    dx10Header.Write(null);
                    //char dds2[sizeof(dx10Header)];
                    //memcpy(dds2, &dx10Header, sizeof(dx10Header));
                    //content.append(QByteArray::fromRawData(dds2, sizeof(dx10Header)));
                }
            }
            return Task.FromResult(fileData);
        }

        void ReadMetadata()
        {
            // Open
            Magic = _r.ReadUInt32();
            if (Magic == F4_BSAHEADER_FILEID)
            {
                Version = _r.ReadUInt32();
                if (Version != F4_BSAHEADER_VERSION)
                    throw new InvalidOperationException("BAD MAGIC");
                // Read the header
                var header_Type = _r.ReadASCIIString(4);            // 08 GNRL=General, DX10=Textures
                var header_NumFiles = _r.ReadUInt32();            // 0C
                var header_NameTableOffset = _r.ReadUInt64();     // 10 - relative to start of file
                // Create file metadatas
                _r.Position = (long)header_NameTableOffset;
                _files = new FileMetadata[header_NumFiles];
                for (var i = 0; i < header_NumFiles; i++)
                {
                    var length = _r.ReadUInt16();
                    var path = _r.ReadASCIIString(length).Replace('\\', '/');
                    _files[i] = new FileMetadata
                    {
                        Path = path,
                        PathHash = Tes4HashFilePath(path),
                    };
                }
                if (header_Type == "GNRL") // General BA2 Format
                {
                    _r.Position = 16 + 8; // sizeof(header) + 8
                    for (var i = 0; i < header_NumFiles; i++)
                    {
                        var info_NameHash = _r.ReadUInt32();      // 00
                        var info_Ext = _r.ReadASCIIString(4);     // 04 - extension
                        var info_DirHash = _r.ReadUInt32();       // 08
                        var info_Unk0C = _r.ReadUInt32();         // 0C - flags? 00100100
                        var info_Offset = _r.ReadUInt64();        // 10 - relative to start of file
                        var info_PackedSize = _r.ReadUInt32();    // 18 - packed length (zlib)
                        var info_UnpackedSize = _r.ReadUInt32();  // 1C - unpacked length
                        var info_Unk20 = _r.ReadUInt32();         // 20 - BAADF00D
                        _files[i].PackedSize = info_PackedSize;
                        _files[i].UnpackedSize = info_UnpackedSize;
                        _files[i].Offset = (long)info_Offset;
                    }
                }
                else if (header_Type == "DX10") // Texture BA2 Format
                {
                    _r.Position = 16 + 8; // sizeof(header) + 8
                    for (var i = 0; i < header_NumFiles; i++)
                    {
                        var fileMetadata = _files[i];
                        var info_NameHash = _r.ReadUInt32();      // 00
                        var info_Ext = _r.ReadASCIIString(4);       // 04
                        var info_DirHash = _r.ReadUInt32();       // 08
                        var info_Unk0C = _r.ReadByte();             // 0C
                        var info_NumChunks = _r.ReadByte();         // 0D
                        var info_ChunkHeaderSize = _r.ReadUInt16();// 0E - size of one chunk header
                        var info_Height = _r.ReadUInt16();        // 10
                        var info_Width = _r.ReadUInt16();         // 12
                        var info_NumMips = _r.ReadByte();           // 14
                        var info_Format = _r.ReadByte();            // 15 - DXGI_FORMAT
                        var info_Unk16 = _r.ReadUInt16();         // 16 - 0800
                        // read tex-chunks
                        var texChunks = new F4TexChunk[info_NumChunks];
                        for (var j = 0; j < info_NumChunks; j++)
                            texChunks[j] = new F4TexChunk
                            {
                                Offset = _r.ReadUInt64(),         // 00
                                PackedSize = _r.ReadUInt32(),     // 08
                                UnpackedSize = _r.ReadUInt32(),   // 0C
                                StartMip = _r.ReadUInt16(),       // 10
                                EndMip = _r.ReadUInt16(),         // 12
                                Unk14 = _r.ReadUInt32(),          // 14 - BAADFOOD
                            };
                        var firstChunk = texChunks[0];
                        _files[i].PackedSize = firstChunk.PackedSize;
                        _files[i].UnpackedSize = firstChunk.UnpackedSize;
                        _files[i].Offset = (long)firstChunk.Offset;
                        fileMetadata.Tex = new F4Tex
                        {
                            Height = info_Height,
                            Width = info_Width,
                            NumMips = info_NumMips,
                            Format = (DXGIFormat)info_Format,
                            Unk16 = info_Unk16,
                            Chunks = texChunks,
                        };
                    }
                }
            }
            else if (Magic == OB_BSAHEADER_FILEID)
            {
                Version = _r.ReadUInt32();
                if (Version != OB_BSAHEADER_VERSION && Version != F3_BSAHEADER_VERSION && Version != SSE_BSAHEADER_VERSION)
                    throw new InvalidOperationException("BAD MAGIC");
                // Read the header
                var header_FolderRecordOffset = _r.ReadUInt32(); // Offset of beginning of folder records
                var header_ArchiveFlags = _r.ReadUInt32(); // Archive flags
                var header_FolderCount = _r.ReadUInt32(); // Total number of folder records (OBBSAFolderInfo)
                var header_FileCount = _r.ReadUInt32(); // Total number of file records (OBBSAFileInfo)
                var header_FolderNameLength = _r.ReadUInt32(); // Total length of folder names
                var header_FileNameLength = _r.ReadUInt32(); // Total length of file names
                var header_FileFlags = _r.ReadUInt32(); // File flags

                // Calculate some useful values
                if ((header_ArchiveFlags & OB_BSAARCHIVE_PATHNAMES) == 0 || (header_ArchiveFlags & OB_BSAARCHIVE_FILENAMES) == 0)
                    throw new InvalidOperationException("HEADER FLAGS");
                _compressToggle = (header_ArchiveFlags & OB_BSAARCHIVE_COMPRESSFILES) != 0;
                if (Version == F3_BSAHEADER_VERSION || Version == SSE_BSAHEADER_VERSION)
                    _hasNamePrefix = (header_ArchiveFlags & F3_BSAARCHIVE_PREFIXFULLFILENAMES) != 0;
                var folderSize = Version != SSE_BSAHEADER_VERSION ? 16 : 24;

                // Create file metadatas
                _files = new FileMetadata[header_FileCount];
                var filenamesSectionStartPos = _r.Position = header_FolderRecordOffset + header_FolderNameLength + header_FolderCount * (folderSize + 1) + header_FileCount * 16;
                var buf = new List<byte>(64);
                for (var i = 0; i < header_FileCount; i++)
                {
                    buf.Clear();
                    byte curCharAsByte; while ((curCharAsByte = _r.ReadByte()) != 0)
                        buf.Add(curCharAsByte);
                    var path = Encoding.ASCII.GetString(buf.ToArray()).Replace('\\', '/');
                    _files[i] = new FileMetadata
                    {
                        Path = path,
                    };
                }
                if (_r.Position != filenamesSectionStartPos + header_FileNameLength)
                    throw new InvalidOperationException("HEADER FILENAMES");

                // read-all folders
                _r.Position = header_FolderRecordOffset;
                var foldersFiles = new uint[header_FolderCount];
                for (var i = 0; i < header_FolderCount; i++)
                {
                    var folder_Hash = _r.ReadUInt64(); // Hash of the folder name
                    var folder_FileCount = _r.ReadUInt32(); // Number of files in folder
                    var folder_Unk = 0U; var folder_Offset = 0UL;
                    if (Version == SSE_BSAHEADER_VERSION) { folder_Unk = _r.ReadUInt32(); folder_Offset = _r.ReadUInt64(); }
                    else folder_Offset = _r.ReadUInt32();
                    foldersFiles[i] = folder_FileCount;
                }

                // add file
                var fileNameIndex = 0U;
                for (var i = 0; i < header_FolderCount; i++)
                {
                    var folder_name = _r.ReadASCIIString(_r.ReadByte(), ASCIIFormat.PossiblyNullTerminated).Replace('\\', '/'); // BSAReadSizedString
                    var folderFiles = foldersFiles[i];
                    for (var j = 0; j < folderFiles; j++)
                    {
                        var file_Hash = _r.ReadUInt64(); // Hash of the filename
                        var file_SizeFlags = _r.ReadUInt32(); // Size of the data, possibly with OB_BSAFILE_FLAG_COMPRESS set
                        var file_Offset = _r.ReadUInt32(); // Offset to raw file data
                        var fileMetadata = _files[fileNameIndex++];
                        fileMetadata.SizeFlags = file_SizeFlags;
                        fileMetadata.Offset = file_Offset;
                        fileMetadata.Path = folder_name + "/" + fileMetadata.Path;
                        fileMetadata.PathHash = Tes4HashFilePath(fileMetadata.Path);
                    }
                }
            }
            else if (Magic == MW_BSAHEADER_FILEID)
            {
                // Read the header
                var header_HashOffset = _r.ReadUInt32(); // Offset of hash table minus header size (12)
                var header_FileCount = _r.ReadUInt32(); // Number of files in the archive

                // Calculate some useful values
                var headerSize = _r.Position;
                var hashTablePosition = headerSize + header_HashOffset;
                var fileDataSectionPostion = hashTablePosition + (8 * header_FileCount);

                // Create file metadatas
                _files = new FileMetadata[header_FileCount];
                for (var i = 0; i < header_FileCount; i++)
                    _files[i] = new FileMetadata
                    {
                        // Read file sizes/offsets
                        SizeFlags = _r.ReadUInt32(),
                        Offset = fileDataSectionPostion + _r.ReadUInt32(),
                    };

                // Read filename offsets
                var filenameOffsets = new uint[header_FileCount]; // relative offset in filenames section
                for (var i = 0; i < header_FileCount; i++)
                    filenameOffsets[i] = _r.ReadUInt32();

                // Read filenames
                var filenamesSectionStartPos = _r.Position;
                var buf = new List<byte>(64);
                for (var i = 0; i < header_FileCount; i++)
                {
                    _r.Position = filenamesSectionStartPos + filenameOffsets[i];
                    buf.Clear();
                    byte curCharAsByte; while ((curCharAsByte = _r.ReadByte()) != 0)
                        buf.Add(curCharAsByte);
                    _files[i].Path = Encoding.ASCII.GetString(buf.ToArray()).Replace('\\', '/');
                }

                // Read filename hashes
                _r.Position = hashTablePosition;
                for (var i = 0; i < header_FileCount; i++)
                    //_files[i].PathHash = _r.ReadUInt64();
                    _files[i].PathHash = Tes3HashFilePath(_files[i].Path);

            }
            else throw new InvalidOperationException("BAD MAGIC");

            // Create the file metadata hash table
            _filesByHash = _files.ToLookup(x => x.PathHash);
        }

        ulong HashFilePath(string filePath)
        {
            if (Magic == MW_BSAHEADER_FILEID) return Tes3HashFilePath(filePath);
            else return Tes4HashFilePath(filePath);
        }

        // http://en.uesp.net/wiki/Tes3Mod:BSA_File_Format
        static ulong Tes3HashFilePath(string filePath)
        {
            filePath = filePath.ToLowerInvariant();
            var len = (uint)filePath.Length;
            Console.WriteLine(len.ToString());
            //
            var l = len >> 1;
            int off, i;
            uint sum, temp, n;
            for (sum = 0, off = 0, i = 0; i < l; i++)
            {
                sum ^= (uint)filePath[i] << (off & 0x1F);
                off += 8;
            }
            var value1 = sum;
            for (sum = 0, off = 0; i < len; i++)
            {
                temp = (uint)filePath[i] << (off & 0x1F);
                sum ^= temp;
                n = temp & 0x1F;
                sum = (sum << (32 - (int)n)) | (sum >> (int)n);
                off += 8;
            }
            var value2 = sum;
            return ((ulong)value2 << 32) | value1;
        }

        // http://en.uesp.net/wiki/Tes4Mod:Hash_Calculation
        static ulong Tes4HashFilePath(string filePath)
        {
            filePath = filePath.ToLowerInvariant();
            return GenHash(Path.ChangeExtension(filePath, null), Path.GetExtension(filePath));

            ulong GenHash(string file, string ext)
            {
                var hash = 0UL;
                if (file.Length > 0)
                {
                    hash = (ulong)(
                        (((byte)file[file.Length - 1]) * 0x1) +
                        ((file.Length > 2 ? (byte)file[file.Length - 2] : 0) * 0x100) +
                        (file.Length * 0x10000) +
                        (((byte)file[0]) * 0x1000000)
                    );
                }
                if (file.Length > 3)
                    hash += (ulong)(GenHash2(file.Substring(1, file.Length - 3)) * 0x100000000);
                if (ext.Length > 0)
                {
                    hash += (ulong)(GenHash2(ext) * 0x100000000);
                    byte i = 0;
                    switch (ext)
                    {
                        case ".nif": i = 1; break;
                        case ".kf": i = 2; break;
                        case ".dds": i = 3; break;
                        case ".wav": i = 4; break;
                    }
                    if (i != 0)
                    {
                        var a = (byte)(((i & 0xfc) << 5) + (byte)((hash & 0xff000000) >> 24));
                        var b = (byte)(((i & 0xfe) << 6) + (byte)(hash & 0xff));
                        var c = (byte)((i << 7) + (byte)((hash & 0xff00) >> 8));
                        hash -= hash & 0xFF00FFFF;
                        hash += (uint)((a << 24) + b + (c << 8));
                    }
                }
                return hash;
            }

            uint GenHash2(string s)
            {
                var hash = 0U;
                for (var i = 0; i < s.Length; i++)
                {
                    hash *= 0x1003f;
                    hash += (byte)s[i];
                }
                return hash;
            }
        }
    }
}