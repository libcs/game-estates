using Gamer.Proxy.Server;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public class ProxySinkServer : ProxySink
    {
        readonly Func<HttpResponse> _resFunc;

        public ProxySinkServer(Func<HttpResponse> resFunc) => _resFunc = resFunc;

        public override HashSet<string> GetContainsSet(Func<HashSet<string>> action)
        {
            var res = _resFunc();
            var r = action(); res.ContentBytes = ToBytes(r); return r;
        }

        public async override Task<byte[]> LoadFileDataAsync(string filePath, Func<Task<byte[]>> action)
        {
            var res = _resFunc();
            var r = await action(); res.ContentBytes = ToBytes(r); return r;
        }
    }
}
