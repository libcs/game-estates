using Gamer.Core;
using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public class TesProxyHandler : IProxyHandler
    {
        public string Key => "Tes";
        public Func<Uri, Func<HttpResponse>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await TesExtensions2.GetTesAssetPackAsync(a, b);
        public Func<Uri, Func<HttpResponse>, Task<IDataPack>> DataPackFunc => TesExtensions.GetTesDataPackAsync;
    }
}
