using Game.Core;
using Game.Core.Netstream;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Estate.UltimaIX
{
    public static class UltimaIXExtensions
    {
        public static UltimaIXGame ToUltimaIXGame(this Uri uri, Func<object> func, out StreamSink streamSink, out string[] filePaths) => StreamUtils.ToGame<UltimaIXGame>(uri, func, out streamSink, out filePaths, "UltimaIX", (path, game) => UltimaIXFileManager.GetFilePaths(false, path, game));

        public static Task<IDataPack> GetUltimaIXDataPackAsync(this Uri uri, Func<object> func = null)
        {
            var game = uri.ToUltimaIXGame(func, out var streamSink, out var filePaths);
            return Task.FromResult((IDataPack)new UltimaIXDataPack(streamSink, filePaths.Single(), game));
        }
    }
}