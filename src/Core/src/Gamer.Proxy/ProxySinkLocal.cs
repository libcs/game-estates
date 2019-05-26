using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySinkLocal : ProxySink
    {
        public override HashSet<string> GetContainsSet(Func<HashSet<string>> action) => action();

        public override bool ContainsFile(string filePath, Func<bool> action) => action();

        public override Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action) => action();
    }
}
