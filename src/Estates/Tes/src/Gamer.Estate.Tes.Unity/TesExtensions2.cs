using Gamer.Core;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public static class TesExtensions2
    {
        public static Task<IAssetUnityPack> GetTesAssetPackAsync(this Uri uri, Func<HttpResponse> resFunc = null)
        {
            uri.ToTesGame(resFunc, out var proxySink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new TesAssetPack(proxySink, filePaths));
        }
    }
}
