using Game.Core;
using System;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX
{
    public static class UltimaIXExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaIXAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToUltimaIXGame(func, out var streamSink, out var filePaths);
            return Task.FromResult((IAssetUnityPack)new UltimaIXAssetPack(streamSink, filePaths));
        }
    }
}