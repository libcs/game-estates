using System.IO;

namespace Game.Core
{
    public abstract class TransformStream : Stream
    {
        Stream _stream;
        MemoryStream _cacheStream = new MemoryStream(5000);

        public TransformStream(Stream stream)
        {
            _stream = stream;
        }

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        public override long Length => 0;
        public override long Position { get; set; }
        public override long Seek(long offset, SeekOrigin direction) => _stream.Seek(offset, direction);
        public override void SetLength(long length) => _stream.SetLength(length);
        public override void Close() => _stream.Close();

        public override void Flush()
        {
            if (_cacheStream.Length > 0)
            {
                _cacheStream.Position = 0;
                _cacheStream = Transform(_cacheStream);
                _stream.Write(_cacheStream.ToArray(), 0, (int)_cacheStream.Length);
                _cacheStream.SetLength(0);
            }
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count) => _stream.Read(buffer, offset, count);
        public override void Write(byte[] buffer, int offset, int count) => _cacheStream.Write(buffer, 0, count);

        protected abstract MemoryStream Transform(MemoryStream s);
    }
}
