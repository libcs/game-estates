using System;

namespace Gamer.Proxy
{
    public class ProxySinkServer : ProxySink
    {
        public override bool ContainsFile(string filePath, Func<bool> action) => action();

        public override byte[] LoadFileData(string filePath, Func<byte[]> action) => action();
    }
}
