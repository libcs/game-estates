using Gamer.Proxy;
using System;

namespace Gamer.Estate.UltimaIX.FilePack
{
    public partial class DatFile : RecordGroup, IDisposable
    {
        public DatFile(ProxySink proxySink, string filePath, UltimaIXGame game)
            : base(proxySink, filePath)
        {
            if (_proxySink is ProxySinkServer)
                return;
            if (_proxySink is ProxySinkClient)
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