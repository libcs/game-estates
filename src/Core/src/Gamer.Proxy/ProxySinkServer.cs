using System;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySinkServer : ProxySink
    {
        public override bool ContainsFile(string filePath, Func<bool> action) => action();

        public override Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) => action();
    }
}
