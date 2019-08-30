using Gamer.Core;
using Gamer.Estate.UltimaIX.FilePack;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.UltimaIX
{
    public static class UltimaIXExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaIXAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToUltimaIXGame(func, out var proxySink, out var filePaths);
            var flxFile = new FlxFile(filePaths[0]);
            return Task.FromResult((IAssetUnityPack)new UltimaIXAssetPack(proxySink, flxFile));
        }
    }
}