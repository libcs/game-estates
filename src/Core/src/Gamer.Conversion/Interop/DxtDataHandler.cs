using Nvidia.TextureTools;
using System;
using System.Runtime.InteropServices;

namespace Gamer.Conversion.Interop
{
    class DxtDataHandler : IDisposable
    {
        public byte[] dst;
        byte[] _result;
        byte[] _buf;
        int _off;

        GCHandle delegateHandleBeginImage;
        GCHandle delegateHandleWriteData;

        public OutputOptions.WriteDataDelegate WriteData { get; private set; }
        public OutputOptions.ImageDelegate BeginImage { get; private set; }

        public DxtDataHandler(byte[] result, OutputOptions outputOptions)
        {
            _result = result;
            WriteData = new OutputOptions.WriteDataDelegate(WriteDataInternal);
            BeginImage = new OutputOptions.ImageDelegate(BeginImageInternal);
            // Keep the delegate from being re-located or collected by the garbage collector.
            delegateHandleBeginImage = GCHandle.Alloc(BeginImage);
            delegateHandleWriteData = GCHandle.Alloc(WriteData);
            outputOptions.SetOutputHandler(BeginImage, WriteData);
        }

        ~DxtDataHandler() => Dispose(false);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                delegateHandleBeginImage.Free();
                delegateHandleWriteData.Free();
                _disposed = true;
            }
        }

        void BeginImageInternal(int size, int width, int height, int depth, int face, int miplevel)
        {
            _buf = new byte[size];
            _off = 0;
        }

        bool WriteDataInternal(IntPtr data, int length)
        {
            Marshal.Copy(data, _buf, _off, length);
            _off += length;
            if (_off == _buf.Length)
                dst = _buf;
            return true;
        }
    }
}
