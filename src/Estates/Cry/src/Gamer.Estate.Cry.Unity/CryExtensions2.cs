using Gamer.Core;
using Gamer.Estate.Cry.FilePack;
using Gamer.Proxy.Server;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Cry
{
    public static class CryExtensions2
    {
        public static Task<IAssetUnityPack> GetCryAssetPackAsync(this Uri uri, Func<HttpResponse> resFunc = null)
        {
            uri.ToCryGame(resFunc, out var proxySink, out var filePath);
            var pakFile = new PakFile(filePath);
            return Task.FromResult((IAssetUnityPack)new CryAssetPack(proxySink, pakFile));
        }
    }
}