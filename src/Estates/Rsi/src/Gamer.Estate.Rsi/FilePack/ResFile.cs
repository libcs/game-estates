using System;

namespace Gamer.Estate.Rsi.FilePack
{
    public partial class ResFile : IDisposable
    {
        readonly PakFile _pakFile;

        public ResFile(PakFile pakFile) => _pakFile = pakFile;

        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
        ~ResFile() => Close();

        public void Close() { }
    }
}