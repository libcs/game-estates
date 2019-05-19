using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaAssetPackAsync(this Uri uri)
        {
            uri.ToUltimaGame(out var proxySink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new UltimaAssetPack(proxySink));
        }
    }
}