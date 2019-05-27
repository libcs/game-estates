using System;
using System.IO;
using System.Text;

namespace Gamer.Core
{
    /// <summary>
    /// Read data from stream with data of specified endianness
    /// </summary>
    public class EndianBinaryWriter : BinaryWriter
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

        public EndianBinaryWriter(Stream input) : this(input, Endian.LittleEndian) { }
        public EndianBinaryWriter(Stream input, Encoding encoding) : this(input, encoding, Endian.LittleEndian) { }
        public EndianBinaryWriter(Stream input, Endian endianness) : this(input, Encoding.UTF8, endianness) { }
        public EndianBinaryWriter(Stream input, Encoding encoding, Endian endianness) : base(input, encoding) => Endianness = endianness;

        public void Write(string value, int count) => base.Write(Encoding.Default.GetBytes(value), 0, count);

        public override void Write(float value) => Write(value, Endianness);
        public void Write(float value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(BitConverter.ToUInt32(BitConverter.GetBytes(value), 0))); }

        public override void Write(double value) => Write(value, Endianness);
        public void Write(double value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(BitConverter.ToUInt64(BitConverter.GetBytes(value), 0))); }

        public override void Write(short value) => Write(value, Endianness);
        public void Write(short value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(value)); }

        public override void Write(ushort value) => Write(value, Endianness);
        public void Write(ushort value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(value)); }

        public override void Write(int value) => Write(value, Endianness);
        public void Write(int value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(value)); }

        public override void Write(uint value) => Write(value, Endianness);
        public void Write(uint value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(value)); }

        public override void Write(long value) => Write(value, Endianness);
        public void Write(long value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(value)); }

        public override void Write(ulong value) => Write(value, Endianness);
        public void Write(ulong value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(Reverse(value)); }

        short Reverse(short value) => (short)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);

        ushort Reverse(ushort value) => (ushort)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);

        int Reverse(int value) => (int)(
                (((uint)value & 0xFF000000) >> 24) << 0 |
                (((uint)value & 0x00FF0000) >> 16) << 8 |
                (((uint)value & 0x0000FF00) >> 8) << 16 |
                (((uint)value & 0x000000FF) >> 0) << 24);

        uint Reverse(uint value) => (uint)(
                ((value & 0xFF000000) >> 24) << 0 |
                ((value & 0x00FF0000) >> 16) << 8 |
                ((value & 0x0000FF00) >> 8) << 16 |
                ((value & 0x000000FF) >> 0) << 24);

        long Reverse(long value) => (long)(
                (((ulong)value & 0xFF00000000000000UL) >> 56) << 0 |
                (((ulong)value & 0x00FF000000000000UL) >> 48) << 8 |
                (((ulong)value & 0x0000FF0000000000UL) >> 40) << 16 |
                (((ulong)value & 0x000000FF00000000UL) >> 32) << 24 |
                (((ulong)value & 0x00000000FF000000UL) >> 24) << 32 |
                (((ulong)value & 0x0000000000FF0000UL) >> 16) << 40 |
                (((ulong)value & 0x000000000000FF00UL) >> 8) << 48 |
                (((ulong)value & 0x00000000000000FFUL) >> 0) << 56);

        ulong Reverse(ulong value) => (ulong)(
                ((value & 0xFF00000000000000UL) >> 56) << 0 |
                ((value & 0x00FF000000000000UL) >> 48) << 8 |
                ((value & 0x0000FF0000000000UL) >> 40) << 16 |
                ((value & 0x000000FF00000000UL) >> 32) << 24 |
                ((value & 0x00000000FF000000UL) >> 24) << 32 |
                ((value & 0x0000000000FF0000UL) >> 16) << 40 |
                ((value & 0x000000000000FF00UL) >> 8) << 48 |
                ((value & 0x00000000000000FFUL) >> 0) << 56);
    }
}