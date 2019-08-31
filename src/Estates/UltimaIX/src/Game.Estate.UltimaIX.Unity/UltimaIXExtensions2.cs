using Game.Core;
using Game.Estate.UltimaIX.FilePack;
using System;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX
{
    public static class UltimaIXExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaIXAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToUltimaIXGame(func, out var streamSink, out var filePaths);
            var flxFile = new FlxFile(filePaths[0]);
            return Task.FromResult((IAssetUnityPack)new UltimaIXAssetPack(streamSink, flxFile));
        }
    }
}