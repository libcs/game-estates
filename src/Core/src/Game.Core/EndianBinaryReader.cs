using System;
using System.IO;
using System.Text;

namespace Game.Core
{
    /// <summary>
    /// Read data from stream with data of specified endianness
    /// </summary>
    public class EndianBinaryReader : BinaryReader
    {
        public static readonly Endian NativeEndianness = BitConverter.IsLittleEndian ? Endian.LittleEndian : Endian.BigEndian;

        /// <summary>
        /// Currently specified endianness
        /// </summary>
        public Endian Endianness { get; set; }

        /// <summary>
        /// Boolean representing if the currently specified endianness equal to the system's native endianness
        /// </summary>
        public bool IsNativeEndianness => NativeEndianness == Endianness;

        public EndianBinaryReader(Stream input) : this(input, Endian.LittleEndian) { }
        public EndianBinaryReader(Stream input, Encoding encoding) : this(input, encoding, Endian.LittleEndian) { }
        public EndianBinaryReader(Stream input, Endian endianness) : this(input, Encoding.UTF8, endianness) { }
        public EndianBinaryReader(Stream input, Encoding encoding, Endian endianness) : base(input, encoding) => Endianness = endianness;

        public string ReadString(int count) => Encoding.Default.GetString(base.ReadBytes(count));

        public override float ReadSingle() => ReadSingle(Endianness);
        public float ReadSingle(Endian endianness) => endianness == NativeEndianness ? base.ReadSingle() : BitConverter.ToSingle(BitConverter.GetBytes(UnsafeUtils.Reverse(base.ReadUInt32())), 0);

        public override double ReadDouble() => ReadDouble(Endianness);
        public double ReadDouble(Endian endianness) => endianness == NativeEndianness ? base.ReadDouble() : BitConverter.ToDouble(BitConverter.GetBytes(UnsafeUtils.Reverse(base.ReadUInt64())), 0);

        public override short ReadInt16() => ReadInt16(Endianness);
        public short ReadInt16(Endian endianness) => endianness == NativeEndianness ? base.ReadInt16() : UnsafeUtils.Reverse(base.ReadInt16());

        public override ushort ReadUInt16() => ReadUInt16(Endianness);
        public ushort ReadUInt16(Endian endianness) => endianness == NativeEndianness ? base.ReadUInt16() : UnsafeUtils.Reverse(base.ReadUInt16());

        public override int ReadInt32() => ReadInt32(Endianness);
        public int ReadInt32(Endian endianness) => endianness == NativeEndianness ? base.ReadInt32() : UnsafeUtils.Reverse(base.ReadInt32());

        public override uint ReadUInt32() => ReadUInt32(Endianness);
        public uint ReadUInt32(Endian endianness) => endianness == NativeEndianness ? base.ReadUInt32() : UnsafeUtils.Reverse(base.ReadUInt32());

        public override long ReadInt64() => ReadInt64(Endianness);
        public long ReadInt64(Endian endianness) => endianness == NativeEndianness ? base.ReadInt64() : UnsafeUtils.Reverse(base.ReadInt64());

        public override ulong ReadUInt64() => ReadUInt64(Endianness);
        public ulong ReadUInt64(Endian endianness) => endianness == NativeEndianness ? base.ReadUInt64() : UnsafeUtils.Reverse(base.ReadUInt64());
    }
}