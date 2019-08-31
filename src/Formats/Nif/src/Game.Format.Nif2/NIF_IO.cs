/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;
using System.IO;
using System.Text;

namespace Niflib
{
    public class BStream
    {
        public Stream B;
        public BStream(Stream stream) { B = stream; }
        public int Read(byte[] buffer, int offset, int count) => B.Read(buffer, offset, count);
        public int ReadByte() => B.ReadByte();
        public void Write(byte[] buffer, int offset, int count) => B.Write(buffer, offset, count);
        public void WriteByte(byte value) => B.WriteByte(value);
        public long Position
        {
            get => B.Position;
            set => B.Position = value;
        }
        public bool IsEof => B.Position == B.Length;
        public void Close() => B.Close();
    }

    public class IStream : BStream
    {
        public IStream(Stream stream) : base(stream) { }

        //public static TStream operator -(IStream s, hdrInfo val) => s.pword(hdrInfo::infoIdx) = (void*)val.info;
        //public static TStream operator -(IStream s, strInfo val) => s.pword(strInfo::infoIdx) = (void*)val.info;

        public string GetLine(byte[] buf, int size)
        {
            var i = 0;
            while (i < size)
            {
                var c = (byte)B.ReadByte();
                if (c == '\n') break;
                buf[i++] = c;
            }
            return Encoding.ASCII.GetString(buf, 0, i);
        }
    }

    public class OStream : BStream
    {
        public OStream() : base(new MemoryStream()) { }
        public OStream(Stream stream) : base(stream) { }
        public static OStream operator +(OStream s, string val) { var buf = Encoding.ASCII.GetBytes(val); s.B.Write(buf, 0, buf.Length); return s; }
        public static OStream operator +(OStream s, HeaderString val) => s + val.header;
        public static OStream operator +(OStream s, LineString val) => s + val.line;
        public static OStream operator +(OStream s, ShortString val) => s + val.str;
        public static OStream operator +(OStream s, byte val) { s.B.WriteByte(val); return s; }
        public static OStream operator +(OStream s, IndexString val) => s + val.val;
        public static OStream operator +(OStream s, Char8String val) => s + val.val;
        public static OStream operator +(OStream s, ByteColor4 val) => s + $"RGBA: {val.r} {val.g} {val.b} {val.a}";
        public static OStream operator +(OStream s, Nif.hdrInfo val) => throw new NotImplementedException();
        public static OStream operator +(OStream s, Nif.strInfo val) => throw new NotImplementedException();
    }

    public static partial class Nif
    {
        ////Constant that stores the detected endian storage type of the current system
        //const EndianType sys_endian = BitConverter.IsLittleEndian ? EndianType.ENDIAN_LITTLE : EndianType.ENDIAN_BIG;
        public const int MAXARRAYDUMP = 20;

        internal static byte[] Buf = new byte[256];
        internal static Exception NotImplementedException = new NotImplementedException();
        internal static Exception ConvertException = new InvalidOperationException("premature end of stream");
        internal static Exception ConfigureException = new InvalidOperationException("stream not properly configured");

        //--Read utility functions--//
        public static int ReadInt(IStream s) { if (s.Read(Buf, 0, 4) != 4) throw ConvertException; return BitConverter.ToInt32(Buf, 0); }
        public static uint ReadUInt(IStream s) { if (s.Read(Buf, 0, 4) != 4) throw ConvertException; return BitConverter.ToUInt32(Buf, 0); }
        public static ushort ReadUShort(IStream s) { if (s.Read(Buf, 0, 2) != 2) throw ConvertException; return BitConverter.ToUInt16(Buf, 0); }
        public static short ReadShort(IStream s) { if (s.Read(Buf, 0, 2) != 2) throw ConvertException; return BitConverter.ToInt16(Buf, 0); }
        public static byte ReadByte(IStream s) { if (s.Read(Buf, 0, 1) != 1) throw ConvertException; return Buf[0]; }
        public static float ReadFloat(IStream s) { if (s.Read(Buf, 0, 4) != 4) throw ConvertException; return BitConverter.ToSingle(Buf, 0); }
        public static string ReadString(IStream s)
        {
            var len = ReadUInt(s);
            if (len > 0x4000)
                throw new InvalidOperationException("String too long. Not a NIF file or unsupported format?");
            if (len > 0) { var buf = new byte[len]; if (s.Read(buf, 0, (int)len) != len) throw ConvertException; return Encoding.ASCII.GetString(buf); }
            return string.Empty;
        }
        //Bools are stored as integers before version 4.1.0.1 and as bytes from 4.1.0.1 on
        public static bool ReadBool(IStream s, uint version) => version <= 0x04010001 ? ReadUInt(s) != 0 : ReadByte(s) != 0;

        //-- Write utility functions--//
        public static void WriteInt(int val, OStream s) => s.Write(BitConverter.GetBytes(val), 0, 4);
        public static void WriteUInt(uint val, OStream s) => s.Write(BitConverter.GetBytes(val), 0, 4);
        public static void WritePtr32(IntPtr val, OStream s) => throw new NotImplementedException();
        public static void WriteUShort(ushort val, OStream s) => s.Write(BitConverter.GetBytes(val), 0, 2);
        public static void WriteShort(short val, OStream s) => s.Write(BitConverter.GetBytes(val), 0, 2);
        public static void WriteByte(byte val, OStream s) => s.Write(BitConverter.GetBytes(val), 0, 1);
        public static void WriteFloat(float val, OStream s) => s.Write(BitConverter.GetBytes(val), 0, 4);
        public static void WriteString(string val, OStream s) { var buf = Encoding.ASCII.GetBytes(val); WriteUInt((uint)buf.Length, s); s.Write(buf, 0, buf.Length); }
        //Bools are stored as integers before version 4.1.0.1 and as bytes from 4.1.0.1 on
        public static void WriteBool(bool val, OStream s, uint version)
        {
            if (version < 0x04010001) WriteUInt(val ? (uint)1 : 0, s);
            else WriteByte(val ? (byte)1 : (byte)0, s);
        }

        //-- NifStream And OStream Functions --//

        // The NifStream functions allow each built-in type to be streamed to and from a file.
        // The OStream functions are for writing out a debug string.

        //--Basic Types--//
        //int
        public static void NifStream(out int val, IStream s, NifInfo info) => val = ReadInt(s);
        public static void NifStream(int val, OStream s, NifInfo info) => WriteInt(val, s);

        //unsigned int
        public static void NifStream(out uint val, IStream s, NifInfo info) => val = ReadUInt(s);
        public static void NifStream(uint val, OStream s, NifInfo info) => WriteUInt(val, s);

        //unsigned short
        public static void NifStream(out ushort val, IStream s, NifInfo info) => val = ReadUShort(s);
        public static void NifStream(ushort val, OStream s, NifInfo info) => WriteUShort(val, s);

        //short
        public static void NifStream(out short val, IStream s, NifInfo info) => val = ReadShort(s);
        public static void NifStream(short val, OStream s, NifInfo info) => WriteShort(val, s);

        //byte
        public static void NifStream(out byte val, IStream s, NifInfo info) => val = ReadByte(s);
        public static void NifStream(byte val, OStream s, NifInfo info) => WriteByte(val, s);

        //bool
        public static void NifStream(out bool val, IStream s, NifInfo info) => val = ReadBool(s, info.version);
        public static void NifStream(bool val, OStream s, NifInfo info) => WriteBool(val, s, info.version);

        //float
        public static void NifStream(out float val, IStream s, NifInfo info) => val = ReadFloat(s);
        public static void NifStream(float val, OStream s, NifInfo info) => WriteFloat(val, s);

        //string
        public static void NifStream(out string val, IStream s, NifInfo info) => val = ReadString(s);
        public static void NifStream(string val, OStream s, NifInfo info) => WriteString(val, s);

        //--Structs--//
        //TexCoord
        public static void NifStream(out TexCoord val, IStream s, NifInfo info)
        {
            val = new TexCoord
            {
                u = ReadFloat(s),
                v = ReadFloat(s),
            };
        }
        public static void NifStream(TexCoord val, OStream s, NifInfo info)
        {
            WriteFloat(val.u, s);
            WriteFloat(val.v, s);
        }

        //Triangle
        public static void NifStream(out Triangle val, IStream s, NifInfo info)
        {
            val = new Triangle
            {
                v1 = ReadUShort(s),
                v2 = ReadUShort(s),
                v3 = ReadUShort(s),
            };
        }
        public static void NifStream(Triangle val, OStream s, NifInfo info)
        {
            WriteUShort(val.v1, s);
            WriteUShort(val.v2, s);
            WriteUShort(val.v3, s);
        }

        //Vector3
        public static void NifStream(out Vector3 val, IStream s, NifInfo info)
        {
            val = new Vector3
            {
                x = ReadFloat(s),
                y = ReadFloat(s),
                z = ReadFloat(s),
            };
        }
        public static void NifStream(Vector3 val, OStream s, NifInfo info)
        {
            WriteFloat(val.x, s);
            WriteFloat(val.y, s);
            WriteFloat(val.z, s);
        }

        //Vector4
        public static void NifStream(out Vector4 val, IStream s, NifInfo info)
        {
            val = new Vector4
            {
                x = ReadFloat(s),
                y = ReadFloat(s),
                z = ReadFloat(s),
                w = ReadFloat(s),
            };
        }
        public static void NifStream(Vector4 val, OStream s, NifInfo info)
        {
            WriteFloat(val.x, s);
            WriteFloat(val.y, s);
            WriteFloat(val.z, s);
            WriteFloat(val.w, s);
        }

        //Float2
        public static void NifStream(out Float2 val, IStream s, NifInfo info)
        {
            val = new Float2();
            val.data[0] = ReadFloat(s);
            val.data[1] = ReadFloat(s);
        }
        public static void NifStream(Float2 val, OStream s, NifInfo info)
        {
            WriteFloat(val.data[0], s);
            WriteFloat(val.data[1], s);
        }

        //Matrix22
        public static void NifStream(out Matrix22 val, IStream s, NifInfo info)
        {
            val = new Matrix22();
            for (var c = 0; c < 2; ++c)
                for (var r = 0; r < 2; ++r)
                    val[r][c] = ReadFloat(s);
        }
        public static void NifStream(Matrix22 val, OStream s, NifInfo info)
        {
            for (var c = 0; c < 2; ++c)
                for (var r = 0; r < 2; ++r)
                    WriteFloat(val[r][c], s);
        }

        //Float3
        public static void NifStream(out Float3 val, IStream s, NifInfo info)
        {
            val = new Float3();
            val.data[0] = ReadFloat(s);
            val.data[1] = ReadFloat(s);
            val.data[2] = ReadFloat(s);
        }
        public static void NifStream(Float3 val, OStream s, NifInfo info)
        {
            WriteFloat(val.data[0], s);
            WriteFloat(val.data[1], s);
            WriteFloat(val.data[2], s);
        }

        //Matrix33
        public static void NifStream(out Matrix33 val, IStream s, NifInfo info)
        {
            val = new Matrix33();
            for (var c = 0; c < 3; ++c)
                for (var r = 0; r < 3; ++r)
                    val[r][c] = ReadFloat(s);
        }
        public static void NifStream(Matrix33 val, OStream s, NifInfo info)
        {
            for (var c = 0; c < 3; ++c)
                for (var r = 0; r < 3; ++r)
                    WriteFloat(val[r][c], s);
        }

        //Float4
        public static void NifStream(out Float4 val, IStream s, NifInfo info)
        {
            val = new Float4();
            val.data[0] = ReadFloat(s);
            val.data[1] = ReadFloat(s);
            val.data[2] = ReadFloat(s);
            val.data[3] = ReadFloat(s);
        }
        public static void NifStream(Float4 val, OStream s, NifInfo info)
        {
            WriteFloat(val.data[0], s);
            WriteFloat(val.data[1], s);
            WriteFloat(val.data[2], s);
            WriteFloat(val.data[3], s);
        }

        //Matrix44
        public static void NifStream(out Matrix44 val, IStream s, NifInfo info)
        {
            val = new Matrix44();
            for (var c = 0; c < 4; ++c)
                for (var r = 0; r < 4; ++r)
                    val[r][c] = ReadFloat(s);
        }
        public static void NifStream(Matrix44 val, OStream s, NifInfo info)
        {
            for (var c = 0; c < 4; ++c)
                for (var r = 0; r < 4; ++r)
                    WriteFloat(val[r][c], s);
        }

        //Color3
        public static void NifStream(out Color3 val, IStream s, NifInfo info)
        {
            val = new Color3
            {
                r = ReadFloat(s),
                g = ReadFloat(s),
                b = ReadFloat(s),
            };
        }
        public static void NifStream(Color3 val, OStream s, NifInfo info)
        {
            WriteFloat(val.r, s);
            WriteFloat(val.g, s);
            WriteFloat(val.b, s);
        }

        //Color4
        public static void NifStream(out Color4 val, IStream s, NifInfo info)
        {
            val = new Color4
            {
                r = ReadFloat(s),
                g = ReadFloat(s),
                b = ReadFloat(s),
                a = ReadFloat(s),
            };
        }
        public static void NifStream(Color4 val, OStream s, NifInfo info)
        {
            WriteFloat(val.r, s);
            WriteFloat(val.g, s);
            WriteFloat(val.b, s);
            WriteFloat(val.a, s);
        }

        //Quaternion
        public static void NifStream(out Quaternion val, IStream s, NifInfo info)
        {
            val = new Quaternion
            {
                w = ReadFloat(s),
                x = ReadFloat(s),
                y = ReadFloat(s),
                z = ReadFloat(s),
            };
        }
        public static void NifStream(Quaternion val, OStream s, NifInfo info)
        {
            WriteFloat(val.w, s);
            WriteFloat(val.x, s);
            WriteFloat(val.y, s);
            WriteFloat(val.z, s);
        }

        //HeaderString
        public static void NifStream(out HeaderString val, IStream s, NifInfo info)
        {
            val = new HeaderString
            {
                header = s.GetLine(Buf, 256)
            };
            // make sure this is a NIF file
            int ver_start = 0;
            if (val.header.Substring(0, 22) == "NetImmerse File Format") ver_start = 32;
            else if (val.header.Substring(0, 20) == "Gamebryo File Format") ver_start = 30;
            else if (val.header.Substring(0, 6) == "NDSNIF") ver_start = 30;
            else info.version = VER_INVALID; //Not a NIF file
            //Parse version string and return result.
            info.version = ParseVersionString(val.header.Substring(ver_start));
        }
        public static void NifStream(HeaderString val, OStream s, NifInfo info)
        {
            s += (info.version <= VER_10_0_1_0 ? "NetImmerse File Format, Version " : "Gamebryo File Format, Version ");
            s += FormatVersionString(info.version);
            s += "\n";
        }

        //LineString
        public static void NifStream(out LineString val, IStream s, NifInfo info) => val = new LineString { line = s.GetLine(Buf, 256) };
        public static void NifStream(LineString val, OStream s, NifInfo info) => s += val.line + "\n";

        //ShortString
        public static void NifStream(out ShortString val, IStream s, NifInfo info)
        {
            var buf = new byte[ReadByte(s)];
            if (s.Read(buf, 0, buf.Length) != buf.Length) throw ConvertException;
            val = new ShortString { str = Encoding.ASCII.GetString(buf) };
        }
        public static void NifStream(ShortString val, OStream s, NifInfo info)
        {
            var buf = Encoding.ASCII.GetBytes(val.str);
            WriteByte((byte)(buf.Length + 1), s);
            s.Write(buf, 0, buf.Length);
            WriteByte(0, s);
        }

        //IndexString
        public static void NifStream(out IndexString val, IStream s, NifInfo info)
        {
            val = new IndexString();
            if (info.version >= VER_20_1_0_3) ToIndexString(ReadUInt(s), hdrInfo.getInfo(s), val);
            else val.val = ReadString(s);
        }
        static void ToIndexString(uint idx, Header header, IndexString value)
        {
            if (header == null)
                throw ConfigureException;
            if (idx == 0xffffffff) value.val = string.Empty;
            else if (idx >= 0 && idx <= header.strings.Length) value = header.strings[idx];
            else throw new InvalidOperationException("invalid string index");
        }
        public static void NifStream(IndexString val, OStream s, NifInfo info)
        {
            if (info.version >= VER_20_1_0_3)
            {
                uint idx = 0xffffffff;
                FromIndexString(val, hdrInfo.getInfo(s), idx);
                WriteUInt(idx, s);
            }
            else WriteString(val.val, s);
        }
        static void FromIndexString(out IndexString value, Header header, uint idx)
        {
            value = new IndexString();
            if (header == null)
                throw ConfigureException;
            if (value.empty())
                idx = 0xffffffff;
            else
            {
                int i = 0;
                for (; i < header.strings.Length; i++)
                    if (header.strings[i] == value)
                        break;
                if (i >= header.numStrings)
                    header.numStrings = i;
                int len = value.length();
                if (header.maxStringLength < len)
                    header.maxStringLength = len;
                header.strings.Add(value);
                idx = i;
            }
        }

        //Char8String
        public static void NifStream(out Char8String val, IStream s, NifInfo info)
        {
            val = new Char8String();
            for (int i = 0; i < 8; ++i)
                s.Read(Buf, i, 1);
            val.val = Encoding.ASCII.GetString(Buf, 0, 8);
        }
        public static void NifStream(Char8String val, OStream s, NifInfo info)
        {
            var buf = Encoding.ASCII.GetBytes(val.val);
            int i = 0, n = Math.Max(8, val.val.Length);
            for (i = 0; i < n; ++i)
                s.Write(buf, i, 1);
            for (; i < 8; ++i)
                s.WriteByte(0);
        }

        //InertiaMatrix
        public static void NifStream(out InertiaMatrix val, IStream s, NifInfo info)
        {
            val = new InertiaMatrix();
            for (int r = 0; r < 3; ++r)
                for (int c = 0; c < 4; ++c)
                    val[r][c] = ReadFloat(s);
        }
        public static void NifStream(InertiaMatrix val, OStream s, NifInfo info)
        {
            for (int r = 0; r < 3; ++r)
                for (int c = 0; c < 4; ++c)
                    WriteFloat(val[r][c], s);
        }

        //ByteColor4
        public static void NifStream(out ByteColor4 val, IStream s, NifInfo info)
        {
            val = new ByteColor4
            {
                r = ReadByte(s),
                g = ReadByte(s),
                b = ReadByte(s),
                a = ReadByte(s),
            };
        }
        public static void NifStream(ByteColor4 val, OStream s, NifInfo info)
        {
            WriteByte(val.r, s);
            WriteByte(val.g, s);
            WriteByte(val.b, s);
            WriteByte(val.a, s);
        }

        //--Templates--//
        public static void NifStream(out Key<Quaternion> key, IStream file, NifInfo info, KeyType type)
        {
            key = new Key<Quaternion>
            {
                time = ReadFloat(file)
            };
            //If key type is not 1, 2, or 3, throw an exception
            if ((int)type < 1 || (int)type > 3)
                type = KeyType.LINEAR_KEY;
            //Read data based on the type of key
            NifStream(out key.data, file, info);
            if (type == KeyType.TBC_KEY)
            {
                //Uses TBC interpolation
                key.tension = ReadFloat(file);
                key.bias = ReadFloat(file);
                key.continuity = ReadFloat(file);
            }
        }
        public static void NifStream(Key<Quaternion> key, OStream file, NifInfo info, KeyType type)
        {
            WriteFloat(key.time, file);
            //If key type is not 1, 2, or 3, throw an exception
            if ((int)type < 1 || (int)type > 3)
                type = KeyType.LINEAR_KEY;
            //Read data based on the type of key
            NifStream(key.data, file, info);
            if (type == KeyType.TBC_KEY)
            {
                //Uses TBC interpolation
                WriteFloat(key.tension, file);
                WriteFloat(key.bias, file);
                WriteFloat(key.continuity, file);
            }
        }

        //Key<T>
        public static void NifStream<T>(out Key<T> key, IStream file, NifInfo info, KeyType type)
        {
            key = new Key<T>();
            key.time = ReadFloat(file);
            //If key type is not 1, 2, or 3, throw an exception
            if ((int)type < 1 || (int)type > 3)
            {
                type = KeyType.LINEAR_KEY;
                //throw runtime_error("Invalid key type.");
            }
            //Read data based on the type of key
            NifStream(out key.data, file, info);
            if (type == KeyType.QUADRATIC_KEY)
            {
                //Uses Quadratic interpolation
                NifStream(out key.forward_tangent, file, info);
                NifStream(out key.backward_tangent, file, info);
            }
            else if (type == KeyType.TBC_KEY)
            {
                //Uses TBC interpolation
                key.tension = ReadFloat(file);
                key.bias = ReadFloat(file);
                key.continuity = ReadFloat(file);
            }
        }
        public static void NifStream<T>(out Key<T> key, IStream file, NifInfo info, int type) => NifStream(out key, file, info, (KeyType)type);
        public static void NifStream<T>(Key<T> key, OStream file, NifInfo info, KeyType type)
        {
            WriteFloat(key.time, file);
            //If key type is not 1, 2, or 3, throw an exception
            if ((int)type < 1 || (int)type > 3)
            {
                type = KeyType.LINEAR_KEY;
                //throw runtime_error("Invalid key type.");
            }
            //Read data based on the type of key
            NifStream(out key.data, file, info);
            if (type == KeyType.QUADRATIC_KEY)
            {
                //Uses Quadratic interpolation
                NifStream(out key.forward_tangent, file, info);
                NifStream(out key.backward_tangent, file, info);
            }
            else if (type == KeyType.TBC_KEY)
            {
                //Uses TBC interpolation
                WriteFloat(key.tension, file);
                WriteFloat(key.bias, file);
                WriteFloat(key.continuity, file);
            }
        }
        public static void NifStream<T>(Key<T> key, OStream file, NifInfo info, int type) => NifStream(key, file, info, (KeyType)type);

        public static void NifStream(out Key<IndexString> key, IStream file, NifInfo info, KeyType type)
        {
            key = new Key<IndexString>();
            if (info.version >= Nif.VER_20_1_0_3)
            {
                Key<int> ikey;
                NifStream(out ikey, file, info, type);
                key.time = ikey.time;
                ToIndexString(ikey.data, hdrInfo.getInfo(file), key.data);
                key.tension = ikey.tension;
                key.bias = ikey.bias;
                key.continuity = ikey.continuity;
            }
            else
            {
                Key<string> skey;
                NifStream(out skey, file, info, type);
                key.time = skey.time;
                key.data = skey.data;
                key.tension = skey.tension;
                key.bias = skey.bias;
                key.continuity = skey.continuity;
            }
        }
        public static void NifStream(Key<IndexString> key, OStream file, NifInfo info, KeyType type)
        {
            if (info.version >= Nif.VER_20_1_0_3)
            {
                Key<uint> ikey;
                ikey.time = key.time;
                ikey.tension = key.tension;
                ikey.bias = key.bias;
                ikey.continuity = key.continuity;
                FromIndexString(key.data, hdrInfo.getInfo(file), ikey.data);
                NifStream(ikey, file, info, type);
            }
            else
            {
                Key<string> skey;
                skey.time = key.time;
                skey.data = key.data;
                skey.tension = key.tension;
                skey.bias = key.bias;
                skey.continuity = key.continuity;
                NifStream(skey, file, info, type);
            }
        }

        //The HexString function creates a formatted hex display of the given data for use in printing
        //a debug string for information that is not understood
        public static string HexString(string src, uint len)
        {
            var b = new StringBuilder();
            return b.ToString();
            //Display Data in Hex form
            //       stringstream out;
            //out << hex << setfill('0');

            //       for (unsigned int i = 0; i<len; ++i)
            //       {
            //	out << uppercase << setw(2) << (unsigned int)(src[i]);
            //       if (i % 16 == 15 || i == len - 1)
            //		out << endl;
            //	else if (i % 16 == 7)
            //		out << "   ";
            //	else if (i % 8 == 3)
            //		out << "  ";
            //	else
            //		out << " ";
            //   }
            //return out.str();
        }

        // strInfo
        public class strInfo
        {
            NifInfo info;
            static NifInfo infoIdx;

            public strInfo(NifInfo value) { info = value; }
            public static void Set(BStream s, NifInfo value) { infoIdx = value; }
            public static NifInfo getInfo(BStream s) => infoIdx;
        }

        // hdrInfo
        public class hdrInfo
        {
            Header info;
            static Header infoIdx;

            public hdrInfo(Header value) { info = value; }
            public static void Set(BStream s, Header value) { infoIdx = value; }
            public static Header getInfo(BStream s) => infoIdx;
        }

        //std::streamsize NifStreamBuf::xsputn(const char_type* _Ptr, std::streamsize _Count)
        //{
        //pos += _Count;
        //if (size<pos) size = pos;
        //return _Count;
        //}

        //    std::streampos NifStreamBuf::seekoff(std::streamoff offset, std::ios_base::seekdir dir, std::ios_base::openmode mode)
        //    {   // change position by offset, according to way and mode
        //        switch (dir)
        //        {
        //            case std::ios_base::beg:
        //                pos = offset;
        //                return (pos >= 0 && pos < size) ? (streampos(-1)) : pos;
        //            case std::ios_base::cur:
        //                pos += offset;
        //                return (pos >= 0 && pos < size) ? (streampos(-1)) : pos;
        //            case std::ios_base::end:
        //                pos = size - offset;
        //                return (pos >= 0 && pos < size) ? (streampos(-1)) : pos;
        //            default:
        //                return streampos(-1);
        //        }
        //        return streampos(-1);
        //    }

        //    std::streampos NifStreamBuf::seekpos(std::streampos offset, std::ios_base::openmode mode)
        //    {   // change to specified position, according to mode
        //        pos = offset;
        //        return (pos >= 0 && pos < size) ? (streampos(-1)) : pos;
        //    }



        //        class NifStreamBuf : public std::streambuf {
        //	streamsize size;
        //        pos_type pos;
        //        public:
        //	NifStreamBuf() : size(0), pos(0) { }
        //        private:
        //	friend class NifSizeStream;
        //        char* pbase() const { return NULL; }
        //    char* pptr() const { return NULL; }
        //char* epptr() const { return NULL; }
        //    // disable spurious msvc warning about xsgetn
        //    #pragma warning (disable : 4996)
        //    virtual streamsize xsgetn(char_type* _Ptr, streamsize _Count) { return 0; }
        //virtual streamsize xsputn(const char_type* _Ptr, streamsize _Count);
        //virtual streampos seekoff(streamoff, ios_base::seekdir, ios_base::openmode = ios_base::in | ios_base::out);
        //virtual streampos seekpos(streampos, ios_base::openmode = ios_base::in | ios_base::out);

        //void reset() { size = 0; pos = 0; }
        //};

        //class NifSizeStream : public std::ostream {
        //	NifStreamBuf _buf;
        //public:
        //	NifSizeStream() : std::ostream( &_buf ) {}
        //	void reset() { _buf.reset(); }
        //};
    }
}