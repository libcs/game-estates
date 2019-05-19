using Gamer.Core;
using Gamer.Estate.Rsi.FilePack;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions2
    {
        public static Task<IAssetUnityPack> GetRsiAssetPackAsync(this Uri uri, Func<HttpResponse> resFunc = null)
        {
            uri.ToRsiGame(resFunc, out var proxySink, out var filePath);
            var pakFile = new PakFile(filePath);
            return Task.FromResult((IAssetUnityPack)new RsiAssetPack(proxySink, pakFile));
        }
    }
}