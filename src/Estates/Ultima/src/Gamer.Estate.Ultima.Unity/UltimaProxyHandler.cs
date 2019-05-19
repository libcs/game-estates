using Gamer.Core;
using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public class UltimaProxyHandler : IProxyHandler
    {
        public string Key => "Ultima";
        public Func<Uri, Func<HttpResponse>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await UltimaExtensions2.GetUltimaAssetPackAsync(a, b);
        public Func<Uri, Func<HttpResponse>, Task<IDataPack>> DataPackFunc => UltimaExtensions.GetUltimaDataPackAsync;
    }
}
