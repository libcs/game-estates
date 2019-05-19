using Gamer.Core;
using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public class RsiProxyHandler : IProxyHandler
    {
        public string Key => "Rsi";
        public Func<Uri, Func<HttpResponse>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await RsiExtensions2.GetRsiAssetPackAsync(a, b);
        public Func<Uri, Func<HttpResponse>, Task<IDataPack>> DataPackFunc => null;
    }
}
