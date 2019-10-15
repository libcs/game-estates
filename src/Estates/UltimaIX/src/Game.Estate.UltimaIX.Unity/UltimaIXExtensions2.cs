using Game.Core;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX
{
    public static class UltimaIXExtensions2
    {
        public static Task<IAssetUnityPack> GetUltimaIXAssetPackAsync(this Uri uri, Func<object> func = null)
        {
            uri.ToUltimaIXGame(func, out var streamSink, out var filePaths);
            if (filePaths.Length == 1 && uri.AbsolutePath == "/")
                filePaths = new[] { "static/sappear.flx", "static/bitmap16.flx" }.Select(x => Path.Combine(filePaths[0], x)).ToArray();
            return Task.FromResult((IAssetUnityPack)new UltimaIXAssetPack(streamSink, filePaths));
        }
    }
}