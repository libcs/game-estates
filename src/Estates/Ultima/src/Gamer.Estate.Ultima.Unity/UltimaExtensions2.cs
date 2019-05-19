using Gamer.Core;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaAssetPackAsync(this Uri uri, Func<HttpResponse> resFunc = null)
        {
            uri.ToUltimaGame(resFunc, out var proxySink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new UltimaAssetPack(proxySink));
        }
    }
}