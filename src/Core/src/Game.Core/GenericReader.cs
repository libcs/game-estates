using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static Game.Core.CoreDebug;

namespace Game.Core
{
    public enum ASCIIFormat { Raw, PossiblyNullTerminated, ZeroPadded }

    public abstract class GenericReader : BinaryReader
    {
        protected GenericReader(Stream input) : base(input, new UTF8Encoding(), false) { }

        public abstract long Position { get; set; }
        public abstract long Seek(long offset, SeekOrigin origin);
        public abstract void Skip(int count);

        public abstract byte[] ReadAbsoluteBytes(long position, int count);
        public abstract byte[] ReadRestOfBytes();
        public abstract void ReadRestOfBytes(byte[] buffer, int startIndex);
        public abstract string ReadASCIIString(int length, ASCIIFormat format = ASCIIFormat.Raw);
        public abstract string[] ReadASCIIMultiString(int length, int bufSize = 64);
        public abstract unsafe T ReadT<T>(int length);
        public abstract unsafe T[] ReadTArray<T>(int length, int count);

        public abstract bool ReadBool32();
        public abstract byte[] ReadLength32PrefixedBytes();
        public abstract string ReadLength32PrefixedASCIIString();
        public abstract Vector2 ReadVector2();
        public abstract Vector3 ReadVector3();
        public abstract Vector4 ReadVector4();
        public abstract Matrix4x4 ReadColumnMajorMatrix3x3();
        public abstract Matrix4x4 ReadRowMajorMatrix3x3();
        public abstract Matrix4x4 ReadColumnMajorMatrix4x4();
        public abstract Matrix4x4 ReadRowMajorMatrix4x4();
        public abstract Quaternion ReadQuaternionWFirst();
        public abstract Quaternion ReadLEQuaternionWLast();

        public abstract string ReadZString();
        public abstract DateTime ReadDateTime();
        public abstract DateTimeOffset ReadDateTimeOffset();
        public abstract TimeSpan ReadTimeSpan();
        public abstract DateTime ReadDeltaTime();
        public abstract int ReadEncodedInt();
        public abstract IPAddress ReadIPAddress();

        public abstract Point3D ReadPoint3D();
        public abstract Point2D ReadPoint2D();
        public abstract Rectangle2D ReadRect2D();
        public abstract Rectangle3D ReadRect3D();

        public abstract bool End();
    }

    public class BinaryFileReader : GenericReader
    {
        public BinaryFileReader(Stream input) : base(input) { }
        public override long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }
        public override long Seek(long offset, SeekOrigin origin) => BaseStream.Seek(offset, origin);
        public override void Skip(int count) => BaseStream.Position += count;

        public override byte[] ReadAbsoluteBytes(long position, int count)
        {
            var last = BaseStream.Position;
            BaseStream.Position = position;
            var r = ReadBytes(count);
            BaseStream.Position = last;
            return r;
        }
        public override byte[] ReadRestOfBytes()
        {
            var remainingByteCount = BaseStream.Length - BaseStream.Position;
            Assert(remainingByteCount <= int.MaxValue);
            return ReadBytes((int)remainingByteCount);
        }
        public override void ReadRestOfBytes(byte[] buffer, int startIndex)
        {
            var remainingByteCount = BaseStream.Length - BaseStream.Position;
            Assert(startIndex >= 0 && remainingByteCount <= int.MaxValue && startIndex + remainingByteCount <= buffer.Length);
            Read(buffer, startIndex, (int)remainingByteCount);
        }
        public override string ReadASCIIString(int length, ASCIIFormat format = ASCIIFormat.Raw)
        {
            Assert(length >= 0);
            var bytes = ReadBytes(length);
            switch (format)
            {
                case ASCIIFormat.Raw: return Encoding.ASCII.GetString(bytes);
                case ASCIIFormat.PossiblyNullTerminated:
                    var bytesIdx = bytes.Last() != 0 ? bytes.Length : bytes.Length - 1;
                    return Encoding.ASCII.GetString(bytes, 0, bytesIdx);
                case ASCIIFormat.ZeroPadded:
                    var zeroIdx = bytes.Length - 1; for (; zeroIdx >= 0 && bytes[zeroIdx] == 0; zeroIdx--) { }
                    return Encoding.ASCII.GetString(bytes, 0, zeroIdx + 1);
                default: throw new ArgumentOutOfRangeException(nameof(format), format.ToString());
            }
        }
        public override string[] ReadASCIIMultiString(int length, int bufSize = 64)
        {
            var list = new List<string>();
            var buf = new List<byte>(bufSize);
            while (length > 0)
            {
                buf.Clear();
                byte curCharAsByte; while (length-- > 0 && (curCharAsByte = ReadByte()) != 0)
                    buf.Add(curCharAsByte);
                list.Add(Encoding.ASCII.GetString(buf.ToArray()));
            }
            return list.ToArray();
        }
        public override T ReadT<T>(int length) => UnsafeUtils.MarshalT<T>(ReadBytes(length), length);
        public override T[] ReadTArray<T>(int length, int count) => UnsafeUtils.MarshalTArray<T>(ReadBytes(length), count);

        //

        public ushort[] ReadManyUInt16(int count)
        {
            var data = ReadBytes(count * 2);
            var data_out = new ushort[count];
            Buffer.BlockCopy(data, 0, data_out, 0, count * 2);
            return data_out;
        }

        public int[] ReadManyInt32(int count)
        {
            var data = ReadBytes(count * 4);
            var data_out = new int[count];
            Buffer.BlockCopy(data, 0, data_out, 0, count * 4);
            return data_out;
        }

        public uint[] ReadManyUInt32(int count)
        {
            var data = ReadBytes(count * 4);
            var data_out = new uint[count];
            Buffer.BlockCopy(data, 0, data_out, 0, count * 4);
            return data_out;
        }

        //

        public override bool ReadBool32() => ReadUInt32() != 0;
        public override byte[] ReadLength32PrefixedBytes() => ReadBytes((int)ReadUInt32());
        public override string ReadLength32PrefixedASCIIString() => Encoding.ASCII.GetString(ReadLength32PrefixedBytes());
        public override Vector2 ReadVector2() => new Vector2(ReadSingle(), ReadSingle());
        public override Vector3 ReadVector3() => new Vector3(ReadSingle(), ReadSingle(), ReadSingle());
        public override Vector4 ReadVector4() => new Vector4(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
        /// <summary>
        /// Reads a column-major 3x3 matrix but returns a functionally equivalent 4x4 matrix.
        /// </summary>
        public override Matrix4x4 ReadColumnMajorMatrix3x3()
        {
            var matrix = new Matrix4x4();
            for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                {
                    // If we're in the 3x3 part of the matrix, read values. Otherwise, use the identity matrix.
                    if (rowIndex <= 2 && columnIndex <= 2) matrix[rowIndex, columnIndex] = ReadSingle();
                    else matrix[rowIndex, columnIndex] = rowIndex == columnIndex ? 1 : 0;
                }
            return matrix;
        }
        /// <summary>
        /// Reads a row-major 3x3 matrix but returns a functionally equivalent 4x4 matrix.
        /// </summary>
        public override Matrix4x4 ReadRowMajorMatrix3x3()
        {
            var matrix = new Matrix4x4();
            for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                {
                    // If we're in the 3x3 part of the matrix, read values. Otherwise, use the identity matrix.
                    if (rowIndex <= 2 && columnIndex <= 2) matrix[rowIndex, columnIndex] = ReadSingle();
                    else matrix[rowIndex, columnIndex] = rowIndex == columnIndex ? 1 : 0;
                }
            return matrix;
        }
        public override Matrix4x4 ReadColumnMajorMatrix4x4()
        {
            var matrix = new Matrix4x4();
            for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                    matrix[rowIndex, columnIndex] = ReadSingle();
            return matrix;
        }
        public override Matrix4x4 ReadRowMajorMatrix4x4()
        {
            var matrix = new Matrix4x4();
            for (var rowIndex = 0; rowIndex < 4; rowIndex++)
                for (var columnIndex = 0; columnIndex < 4; columnIndex++)
                    matrix[rowIndex, columnIndex] = ReadSingle();
            return matrix;
        }
        public override Quaternion ReadQuaternionWFirst()
        {
            var w = ReadSingle();
            var x = ReadSingle();
            var y = ReadSingle();
            var z = ReadSingle();
            return new Quaternion(x, y, z, w);
        }
        public override Quaternion ReadLEQuaternionWLast()
        {
            var x = ReadSingle();
            var y = ReadSingle();
            var z = ReadSingle();
            var w = ReadSingle();
            return new Quaternion(x, y, z, w);
        }

        public override string ReadZString() => ReadByte() != 0 ? ReadString() : null;
        public override DateTime ReadDeltaTime()
        {
            var ticks = ReadInt64();
            var now = DateTime.UtcNow.Ticks;
            if (ticks > 0 && (ticks + now) < 0) return DateTime.MaxValue;
            else if (ticks < 0 && (ticks + now) < 0) return DateTime.MinValue;
            try { return new DateTime(now + ticks); }
            catch { return ticks > 0 ? DateTime.MaxValue : DateTime.MinValue; }
        }
        public override IPAddress ReadIPAddress() => new IPAddress(ReadInt64());
        public override int ReadEncodedInt()
        {
            int v = 0, shift = 0;
            byte b;
            do
            {
                b = ReadByte();
                v |= (b & 0x7F) << shift;
                shift += 7;
            } while (b >= 0x80);
            return v;
        }
        public override DateTime ReadDateTime() => new DateTime(ReadInt64());
        public override DateTimeOffset ReadDateTimeOffset()
        {
            var ticks = ReadInt64();
            var offset = new TimeSpan(ReadInt64());
            return new DateTimeOffset(ticks, offset);
        }
        public override TimeSpan ReadTimeSpan() => new TimeSpan(ReadInt64());

        public override Point3D ReadPoint3D() => new Point3D(ReadInt32(), ReadInt32(), ReadInt32());
        public override Point2D ReadPoint2D() => new Point2D(ReadInt32(), ReadInt32());
        public override Rectangle2D ReadRect2D() => new Rectangle2D(ReadPoint2D(), ReadPoint2D());
        public override Rectangle3D ReadRect3D() => new Rectangle3D(ReadPoint3D(), ReadPoint3D());

        public override bool End() => PeekChar() == -1;
    }
}