using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.UltimaIX
{
    public static class UltimaIXExtensions
    {
        public static UltimaIXGame ToUltimaIXGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths) => ProxyUtils.ToGame<UltimaIXGame>(uri, func, out proxySink, out filePaths, "UltimaIX", (path, game) => UltimaIXFileManager.GetFilePaths(false, path, game));

        public static Task<IDataPack> GetUltimaIXDataPackAsync(this Uri uri, Func<object> func = null)
        {
            var game = uri.ToUltimaIXGame(func, out var proxySink, out var filePaths);
            return Task.FromResult((IDataPack)new UltimaIXDataPack(proxySink, filePaths.Single(), game));
        }
    }
}