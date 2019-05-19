using Gamer.Core;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Proxy
{
    public interface IProxyHandler
    {
        string Key { get; }
        Func<Uri, Func<HttpResponse>, Task<IAssetPack>> AssetPackFunc { get; }
        Func<Uri, Func<HttpResponse>, Task<IDataPack>> DataPackFunc { get; }
    }
}
