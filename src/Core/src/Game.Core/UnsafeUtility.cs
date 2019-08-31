using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Unity.Collections.LowLevel.Unsafe;

namespace Game.Core
{
    public static class UnsafeUtils
    {
        public const string PlatformWindows = "Windows";
        public const string PlatformNone = "";
        public static readonly string Platform;
        public static readonly Func<IntPtr, IntPtr, uint, IntPtr> Memcpy;

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)] extern static unsafe IntPtr memcpy(IntPtr dest, IntPtr src, uint count);
        static unsafe UnsafeUtils()
        {
            var task = Task.Run(() => UnityEngine.Application.platform.ToString());
            try
            {
                Platform = task.Result;
                //Debug.Log(Platform);
                Memcpy = (dest, src, count) => { UnsafeUtility.MemCpy((void*)dest, (void*)src, count); return IntPtr.Zero; };
                Debug.AssertFunc = x => UnityEngine.Debug.Assert(x);
                Debug.LogFunc = a => UnityEngine.Debug.Log(a);
                Debug.LogFormatFunc = (a, b) => UnityEngine.Debug.LogFormat(a, b);
                return;
            }
            catch
            {
                Platform = string.Empty;
                Memcpy = memcpy;
                Debug.AssertFunc = x => System.Diagnostics.Debug.Assert(x);
                Debug.LogFunc = a => System.Diagnostics.Debug.Print(a);
                Debug.LogFormatFunc = (a, b) => System.Diagnostics.Debug.Print(a, b);
            }
        }

        [DllImport("Kernel32")] extern static unsafe int _lread(SafeFileHandle hFile, void* lpBuffer, int wBytes);
        public static unsafe void ReadBuffer(this FileStream stream, byte[] buf, int length)
        {
            if (Platform == PlatformWindows)
                fixed (byte* pbuf = buf)
                    _lread(stream.SafeFileHandle, pbuf, length);
            else
                stream.Read(buf, 0, length);
        }

        public static unsafe T MarshalT<T>(byte[] bytes, int length)
        {
            var size = Marshal.SizeOf(typeof(T));
            if (size > length)
                Array.Resize(ref bytes, size);
            fixed (byte* src = bytes)
                return (T)Marshal.PtrToStructure(new IntPtr(src), typeof(T));
            //fixed (byte* src = bytes)
            //{
            //    var r = default(T);
            //    var hr = GCHandle.Alloc(r, GCHandleType.Pinned);
            //    Memcpy(hr.AddrOfPinnedObject(), new IntPtr(src), (uint)bytes.Length);
            //    hr.Free();
            //    return r;
            //}
        }

        public static unsafe T[] MarshalTArray<T>(byte[] bytes, int length, int count)
        {
            fixed (byte* src = bytes)
            {
                var r = new T[count];
                var hr = GCHandle.Alloc(r, GCHandleType.Pinned);
                Memcpy(hr.AddrOfPinnedObject(), new IntPtr(src), (uint)bytes.Length);
                hr.Free();
                return r;
            }
        }

        public static short Reverse(short value) => (short)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);
        public static ushort Reverse(ushort value) => (ushort)(
                ((value & 0xFF00) >> 8) << 0 |
                ((value & 0x00FF) >> 0) << 8);
        public static int Reverse(int value) => (int)(
                (((uint)value & 0xFF000000) >> 24) << 0 |
                (((uint)value & 0x00FF0000) >> 16) << 8 |
                (((uint)value & 0x0000FF00) >> 8) << 16 |
                (((uint)value & 0x000000FF) >> 0) << 24);
        public static uint Reverse(uint value) => (uint)(
                ((value & 0xFF000000) >> 24) << 0 |
                ((value & 0x00FF0000) >> 16) << 8 |
                ((value & 0x0000FF00) >> 8) << 16 |
                ((value & 0x000000FF) >> 0) << 24);
        public static long Reverse(long value) => (long)(
                (((ulong)value & 0xFF00000000000000UL) >> 56) << 0 |
                (((ulong)value & 0x00FF000000000000UL) >> 48) << 8 |
                (((ulong)value & 0x0000FF0000000000UL) >> 40) << 16 |
                (((ulong)value & 0x000000FF00000000UL) >> 32) << 24 |
                (((ulong)value & 0x00000000FF000000UL) >> 24) << 32 |
                (((ulong)value & 0x0000000000FF0000UL) >> 16) << 40 |
                (((ulong)value & 0x000000000000FF00UL) >> 8) << 48 |
                (((ulong)value & 0x00000000000000FFUL) >> 0) << 56);
        public static ulong Reverse(ulong value) => (ulong)(
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