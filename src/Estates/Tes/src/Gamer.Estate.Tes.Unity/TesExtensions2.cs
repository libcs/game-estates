using Gamer.Core;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public static class TesExtensions2
    {
        public static Task<IAssetUnityPack> GetTesAssetPackAsync(this Uri uri)
        {
            uri.ToTesGame(out var proxySink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new TesAssetPack(proxySink, filePaths));
        }
    }
}
