using System;

namespace Gamer.Proxy
{
    public class ProxySink
    {
        public virtual bool ContainsFile(string filePath, Func<bool> action) => action();

        public virtual byte[] LoadFileData(string filePath, Func<byte[]> action) => action();
    }
}
