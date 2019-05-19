using System;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySink
    {
        public virtual bool ContainsFile(string filePath, Func<bool> action) => action();

        public virtual Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) => action();
    }
}
