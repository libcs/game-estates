/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

using System;
using System.Collections.Generic;

namespace Niflib
{
    public struct HeaderString
    {
        public string header;
        public static implicit operator HeaderString(string r) => new HeaderString { header = r };
    }

    public struct ShortString
    {
        public string str;
        public static implicit operator ShortString(string r) => new ShortString { str = r };
    }

    public struct LineString
    {
        public string line;
        public static implicit operator LineString(string r) => new LineString { line = r };
    }

    public struct IndexString
    {
        internal string val;
        //public IndexString() { val = null; }
        public IndexString(IndexString r) { val = r.val; }
        public IndexString(string r) { val = r; }
        public static implicit operator string(IndexString r) => r.val;
        public static implicit operator IndexString(string r) => new IndexString(r);
    }

    public struct Char8String
    {
        internal string val;
        //public Char8String() { val = null; }
        public Char8String(Char8String r) { val = r.val; }
        public Char8String(string r) { val = r; }
        public static implicit operator Char8String(string r) => new Char8String(r);
    }

    /*!
     * Used to specify optional ways the NIF file is to be written or retrieve information about
     * the way an existing file was stored. 
     */
    public class NifInfo
    {
        public static NifInfo Empty = new NifInfo();
        public NifInfo() : this(Nif.VER_4_0_0_2, 0, 0) { }
        public NifInfo(uint version, uint userVersion = 0, uint userVersion2 = 0)
        {
            this.version = version;
            this.userVersion = userVersion;
            this.userVersion2 = userVersion2;
            endian = EndianType.ENDIAN_LITTLE;
            author = null;
            processScript = null;
            exportScript = null;
        }

        public uint version;
        public uint userVersion;
        public uint userVersion2;
        /*! Specifies which low-level number storage format to use. Should match the processor type for the target system. */
        public EndianType endian;
        /*! This is only supported in Oblivion.  It contains the name of the person who created the NIF file. */
        public string author;
        /*! This is only supported in Oblivion.  It seems to contain the type of script or program used to export the file. */
        public string processScript;
        /*! This is only supported in Oblivion.  It seems to contain the more specific script or options of the above. */
        public string exportScript;
    }

    /*! Used to enable static arrays to be members of vectors */
    public class Array1<T>
    {
        public T[] data = new T[1];
        public Array1(T t0 = default(T)) { data[0] = t0; }
    }
    public class Array2<T>
    {
        public T[] data = new T[2];
        public Array2(T t0 = default(T), T t1 = default(T)) { data[0] = t0; data[1] = t1; }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }
    public class Array3<T>
    {
        public T[] data = new T[3];
        public Array3(T t0 = default(T), T t1 = default(T), T t2 = default(T)) { data[0] = t0; data[1] = t1; data[2] = t2; }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }
    public class Array4<T>
    {
        public T[] data = new T[4];
        public Array4(T t0 = default(T), T t1 = default(T), T t2 = default(T), T t3 = default(T)) { data[0] = t0; data[1] = t1; data[2] = t2; data[3] = t3; }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }
    public class Array5<T>
    {
        public T[] data = new T[5];
        public Array5(T t0 = default(T), T t1 = default(T), T t2 = default(T), T t3 = default(T), T t4 = default(T)) { data[0] = t0; data[1] = t1; data[2] = t2; data[3] = t3; data[4] = t4; }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }
    public class Array6<T>
    {
        public T[] data = new T[6];
        public Array6(T t0 = default(T), T t1 = default(T), T t2 = default(T), T t3 = default(T), T t4 = default(T), T t5 = default(T)) { data[0] = t0; data[1] = t1; data[2] = t2; data[3] = t3; data[4] = t4; data[5] = t5; }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }
    public class Array7<T>
    {
        public T[] data = new T[7];
        public Array7(T t0 = default(T), T t1 = default(T), T t2 = default(T), T t3 = default(T), T t4 = default(T), T t5 = default(T), T t6 = default(T)) { data[0] = t0; data[1] = t1; data[2] = t2; data[3] = t3; data[4] = t4; data[5] = t5; data[6] = t6; }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }
    public class Array8<T>
    {
        public T[] data = new T[7];
        public Array8(T t0 = default(T), T t1 = default(T), T t2 = default(T), T t3 = default(T), T t4 = default(T), T t5 = default(T), T t6 = default(T)) { data[0] = t0; data[1] = t1; data[2] = t2; data[3] = t3; data[4] = t4; data[5] = t5; data[6] = t6; data[7] = default(T); }
        public T this[int x] { get => data[x]; set => data[x] = value; }
    }

    static partial class Nif
    {
        public static void Resize<T>(this IList<T> s, int newSize) { var v = (T[])s; Array.Resize(ref v, newSize); }

        //-- BitField Helper functions --//
        public static bool UnpackFlag(ushort src, int lshift)
        {
            //Generate mask
            var mask = (ushort)(1 << lshift);
            return ((src & mask) >> lshift) != 0;
        }

        public static void PackFlag(ushort dest, bool new_value, int lshift)
        {
            //Generate mask
            var mask = (ushort)(1 << lshift);
            //Clear current value of requested flag
            dest &= (ushort)~mask;
            //Pack in the new value
            dest |= (ushort)((new_value ? 1 : 0 << lshift) & mask);
        }

        public static ushort UnpackField(ushort src, int lshift, int num_bits)
        {
            //Generate mask
            ushort mask = 0;
            for (var i = lshift; i < num_bits + lshift; ++i)
                mask |= (ushort)(1 << i);
            return (ushort)((src & mask) >> lshift);
        }

        public static void PackField(ushort dest, ushort new_value, int lshift, int num_bits)
        {
            //Generate Mask
            ushort mask = 0;
            for (var i = lshift; i < num_bits + lshift; ++i)
                mask |= (ushort)(1 << i);
            //Clear current value of requested field
            dest &= (ushort)~mask;
            //Pack in the new value
            dest |= (ushort)((new_value << lshift) & mask);
        }


    }
}