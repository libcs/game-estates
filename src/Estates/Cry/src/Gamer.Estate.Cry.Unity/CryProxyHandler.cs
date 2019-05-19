using Gamer.Core;
using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public class CryProxyHandler : IProxyHandler
    {
        public string Key => "Cry";
        public Func<Uri, Func<HttpResponse>, Task<IAssetPack>> AssetPackFunc => async (a, b) => await CryExtensions2.GetCryAssetPackAsync(a, b);
        public Func<Uri, Func<HttpResponse>, Task<IDataPack>> DataPackFunc => null;
    }
}
