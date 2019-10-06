using Game.Core;
using System;
using System.Collections.Generic;
using System.IO;
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
        struct FileMetadata
        {
            public int Position;
            public int Size;
        }

        public override string ToString() => $"{Path.GetFileName(FilePath)}";
        GenericReader _r;
        FileMetadata[] _files;
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
        public HashSet<string> GetContainsSet() => new HashSet<string>() { };

        /// <summary>
        /// Determines whether the archive contains a file.
        /// </summary>
        public bool ContainsFile(string filePath) => int.TryParse(filePath, out var file) && file < _files.Length && _files[file].Size != 0;

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        public Task<byte[]> LoadFileDataAsync(string filePath)
        {
            if (int.TryParse(filePath, out var file) && file < _files.Length)
                return LoadFileDataAsync(_files[file]);
            CoreDebug.Log($"LoadFileDataAsync: {filePath} @ {_files}");
            throw new FileNotFoundException(filePath);
        }

        /// <summary>
        /// Loads an archived file's data.
        /// </summary>
        Task<byte[]> LoadFileDataAsync(FileMetadata file)
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
            _files = _r.ReadTArray<FileMetadata>(fileCount * 8, fileCount);
        }


        #region Texture

        public class Bitmap
        {
            public BitmapHeader Header;
            public Texture2DInfo[] Frames;
        }

        public struct BitmapHeader
        {
            public enum TextureFormat : ushort { }
            public enum TextureCompression : ushort
            {
                Uncompressed = 0x00,
                Unknown = 0x01 // Used with some 8-bit textures.
            }
            public ushort Width; // Maximum width in pixels of all the frames.
            public TextureFormat Format;
            public ushort Height; // Maximum height in pixels of all the frames.
            public TextureCompression Compression;
            public ushort FrameCount; // The number of frames.
            public uint Unknown;
        }

        struct BitmapFrame
        {
            public ushort Unknown1;
            public ushort Unknown2; // Usually 0x6000.
            public uint width; // Width in pixels of the frame.
            public uint height; // Height in pixels of the frame.
            public uint Unknown3; // Almost always 0.
            public uint Unknown4; // Almost always 0.
        }

        struct BitmapFrameOffset
        {
            public uint Offset;
            public uint Size;
        }

        internal static Bitmap LoadRawBitmap(Stream inputStream)
        {
            using (var r = new BinaryFileReader(inputStream))
            {
                var header = r.ReadT<BitmapHeader>(0x10);
                var frameCount = header.FrameCount;
                var frameOffsets = r.ReadTArray<BitmapFrameOffset>(frameCount * 0x08, frameCount);
                var frames = new Texture2DInfo[frameCount];
                for (var i = 0; i < frameCount; i++)
                {
                    r.Position = frameOffsets[i].Offset;
                    var frame = r.ReadT<BitmapFrame>(0x14);
                    var frameHeight = (int)frame.height;
                    var sizeOfFrame = 0x14 + frameHeight * 0x04;
                    var frameDataSize = (int)(frameHeight * frame.width);
                    var bitsPerPixel = (int)((frameOffsets[i].Size - sizeOfFrame) / frameDataSize);
                    var offsets = r.ReadTArray<uint>(frameHeight * 0x04, frameHeight); // Offset to the data for each row relative to the start of the resource.
                    if (offsets[0] == 0xCDCDCDCD) //: unknownFrame
                        continue;
                    r.Position = frameOffsets[i].Offset;
                    var rawData = r.ReadBytes(frameDataSize * bitsPerPixel);
                    frames[i] = bitsPerPixel == 1
                        ? new Texture2DInfo((int)frame.width, (int)frame.height, UnityEngine.TextureFormat.BGRA32, false, rawData).From8BitPallet(GetGlobal8BitPallet(), UnityEngine.TextureFormat.BGRA32)
                        : new Texture2DInfo((int)frame.width, (int)frame.height, UnityEngine.TextureFormat.RGBA32, false, rawData).FromRGBA555();
                }
                return new Bitmap
                {
                    Header = header,
                    Frames = frames,
                };
            }
        }

        #endregion

        #region Minimap

        internal static Texture2DInfo LoadRawMinimapTile(Stream inputStream, int bitsPerPixel)
        {
            using (var r = new BinaryFileReader(inputStream))
            {
                var width = r.ReadInt32();
                var height = r.ReadInt32();
                r.Skip(8);
                var rawData = r.ReadBytes(width * height * bitsPerPixel);
                return bitsPerPixel == 1
                    ? new Texture2DInfo(width, height, UnityEngine.TextureFormat.BGRA32, false, rawData).From8BitPallet(GetGlobal8BitPallet(), UnityEngine.TextureFormat.BGRA32)
                    : new Texture2DInfo(width, height, UnityEngine.TextureFormat.RGBA32, false, rawData).FromRGBA555();
            }
        }

        internal static byte[][] _global8BitPallet;
        internal static byte[][] GetGlobal8BitPallet()
        {
            if (_global8BitPallet != null) return _global8BitPallet;
            var assembly = typeof(ResFile).Assembly;
            lock (assembly)
            {
                if (_global8BitPallet != null) return _global8BitPallet;
                var pallet8 = new byte[256][];
                using (var bs = assembly.GetManifestResourceStream("Game.Estate.UltimaIX.FilePack.ankh.pal"))
                using (var br = new BinaryReader(bs))
                    for (var i = 0; i < pallet8.Length; i++)
                        pallet8[i] = BitConverter.GetBytes(br.ReadUInt32());
                return _global8BitPallet = pallet8;
            }
        }

        #endregion
    }
}