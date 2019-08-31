using Game.Core.Netstream;
using System;

namespace Game.Estate.UltimaIX.FilePack
{
    public partial class DatFile : RecordGroup, IDisposable
    {
        public DatFile(StreamSink streamSink, string filePath, UltimaIXGame game)
            : base(streamSink, filePath)
        {
            if (_streamSink is StreamSinkServer)
                return;
            if (_streamSink is StreamSinkClient)
            {
                Process();
                return;
            }
            if (filePath == null)
                return;
        }

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~DatFile() => Close();

        public void Close() { }
    }
}