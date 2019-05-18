using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions2
    {
        public static Task<IAssetPack> GetUltimaAssetPackAsync(this Uri uri)
        {
            uri.ToUltimaGame(out var proxySink, out var filePaths);
            return Task.FromResult((IAssetPack)new UltimaAssetPack(proxySink));
        }
    }
}