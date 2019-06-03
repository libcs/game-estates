using System;
using System.IO;
using System.Net;
using System.Text;
using static Gamer.Core.Debug;

namespace Gamer.Core
{
    public interface ISerializable
    {
        int TypeReference { get; }
        int SerialIdentity { get; }
        void Serialize(GenericWriter writer);
    }

    public abstract class GenericWriter : BinaryWriter
    {
        protected GenericWriter(Stream output) : base(output, new UTF8Encoding(), false) { }

        //public abstract long Position { get; }

        public abstract void WriteASCIIString(string value, int length, ASCIIFormat format = ASCIIFormat.Raw);
        public abstract void WriteASCIIMultiString(string[] value, int length, int bufSize = 64);
        public abstract unsafe void WriteT<T>(T value, int length);
        public abstract unsafe void WriteTArray<T>(T[] value, int length, int count);

        public abstract void Write(DateTime value);
        public abstract void Write(DateTimeOffset value);
        public abstract void Write(TimeSpan value);
        //public abstract void WriteEncodedInt(int value);
        public abstract void Write(IPAddress value);

        public abstract void WriteDeltaTime(DateTime value);

        public abstract void Write(Point3D value);
        public abstract void Write(Point2D value);
        public abstract void Write(Rectangle2D value);
        public abstract void Write(Rectangle3D value);
    }

    public class BinaryFileWriter : GenericWriter
    {
        //bool PrefixStrings;

        public BinaryFileWriter(Stream output) : base(output) { }

        //byte[] _buf;
        //int _idx;
        //Encoding _encoding;

        //public override void WriteEncodedInt(int value)
        //{
        //    var v = (uint)value;
        //    while (v >= 0x80)
        //    {
        //        if ((_idx + 1) > _buf.Length)
        //            Flush();
        //        _buf[_idx++] = (byte)(v | 0x80);
        //        v >>= 7;
        //    }
        //    if ((_idx + 1) > _buf.Length)
        //        Flush();
        //    _buf[_idx++] = (byte)v;
        //}

        //byte[] _characterBuffer;
        //int _maxBufferChars;
        //const int LargeByteBufferSize = 256;

        //internal void InternalWriteString(string value)
        //{
        //    var length = _encoding.GetByteCount(value);
        //    WriteEncodedInt(length);
        //    if (_characterBuffer == null)
        //    {
        //        _characterBuffer = new byte[LargeByteBufferSize];
        //        _maxBufferChars = LargeByteBufferSize / _encoding.GetMaxByteCount(1);
        //    }
        //    if (length > LargeByteBufferSize)
        //    {
        //        var current = 0;
        //        var charsLeft = value.Length;
        //        while (charsLeft > 0)
        //        {
        //            var charCount = (charsLeft > _maxBufferChars) ? _maxBufferChars : charsLeft;
        //            var byteLength = _encoding.GetBytes(value, current, charCount, _characterBuffer, 0);
        //            if ((_idx + byteLength) > _buf.Length)
        //                Flush();
        //            Buffer.BlockCopy(_characterBuffer, 0, _buf, _idx, byteLength);
        //            _idx += byteLength;
        //            current += charCount;
        //            charsLeft -= charCount;
        //        }
        //    }
        //    else
        //    {
        //        var byteLength = _encoding.GetBytes(value, 0, value.Length, _characterBuffer, 0);
        //        if ((_idx + byteLength) > _buf.Length)
        //            Flush();
        //        Buffer.BlockCopy(_characterBuffer, 0, _buf, _idx, byteLength);
        //        _idx += byteLength;
        //    }
        //}

        //public override void Write(string value)
        //{
        //    if (PrefixStrings)
        //    {
        //        if (value == null)
        //        {
        //            if ((_idx + 1) > _buf.Length)
        //                Flush();
        //            _buf[_idx++] = 0;
        //        }
        //        else
        //        {
        //            if ((_idx + 1) > _buf.Length)
        //                Flush();
        //            _buf[_idx++] = 1;
        //            InternalWriteString(value);
        //        }
        //    }
        //    else InternalWriteString(value);
        //}
        public override void Write(DateTime value) => Write(value.Ticks);
        public override void Write(DateTimeOffset value)
        {
            Write(value.Ticks);
            Write(value.Offset.Ticks);
        }
        public override void WriteDeltaTime(DateTime value)
        {
            var ticks = value.Ticks;
            var now = DateTime.UtcNow.Ticks;
            TimeSpan d;
            try { d = new TimeSpan(ticks - now); }
            catch { d = ticks < now ? TimeSpan.MaxValue : TimeSpan.MaxValue; }
            Write(d);
        }
#pragma warning disable 618
        public override void Write(IPAddress value) => Write((long)value.Address);
#pragma warning restore 618
        public override void Write(TimeSpan value) => Write(value.Ticks);
        public override void Write(Point3D value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }
        public override void Write(Point2D value)
        {
            Write(value.X);
            Write(value.Y);
        }
        public override void Write(Rectangle2D value)
        {
            Write(value.Start);
            Write(value.End);
        }
        public override void Write(Rectangle3D value)
        {
            Write(value.Start);
            Write(value.End);
        }

        public override void WriteASCIIString(string value, int length, ASCIIFormat format = ASCIIFormat.Raw)
        {
            Assert(length >= 0);
            byte[] bytes;
            switch (format)
            {
                case ASCIIFormat.Raw: bytes = Encoding.ASCII.GetBytes(value); Array.Resize(ref bytes, length); Write(bytes, 0, bytes.Length); break;
                case ASCIIFormat.PossiblyNullTerminated: bytes = Encoding.ASCII.GetBytes(value); Array.Resize(ref bytes, length); Write(bytes, 0, bytes.Length); break;
                //case ASCIIFormat.ZeroPadded:
                //    var zeroIdx = bytes.Length - 1; for (; zeroIdx >= 0 && bytes[zeroIdx] == 0; zeroIdx--) { }
                //    return Encoding.ASCII.GetString(bytes, 0, zeroIdx + 1);
                default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
            }
        }
        public override void WriteASCIIMultiString(string[] value, int length, int bufSize = 64) => throw new NotImplementedException();
        public override void WriteT<T>(T value, int length) => throw new NotImplementedException();
        public override void WriteTArray<T>(T[] value, int length, int count) => throw new NotImplementedException();
    }
}