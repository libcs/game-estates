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
        public void Write(float value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(BitConverter.ToUInt32(BitConverter.GetBytes(value), 0))); }

        public override void Write(double value) => Write(value, Endianness);
        public void Write(double value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(BitConverter.ToUInt64(BitConverter.GetBytes(value), 0))); }

        public override void Write(short value) => Write(value, Endianness);
        public void Write(short value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(value)); }

        public override void Write(ushort value) => Write(value, Endianness);
        public void Write(ushort value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(value)); }

        public override void Write(int value) => Write(value, Endianness);
        public void Write(int value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(value)); }

        public override void Write(uint value) => Write(value, Endianness);
        public void Write(uint value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(value)); }

        public override void Write(long value) => Write(value, Endianness);
        public void Write(long value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(value)); }

        public override void Write(ulong value) => Write(value, Endianness);
        public void Write(ulong value, Endian endianness) { if (endianness == NativeEndianness) base.Write(value); else base.Write(UnsafeUtils.Reverse(value)); }
    }
}