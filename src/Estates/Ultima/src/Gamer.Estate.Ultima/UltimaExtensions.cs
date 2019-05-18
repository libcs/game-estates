using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions
    {
        public static UltimaGame ToUltimaGame(this Uri uri, out ProxySink proxySink, out string[] filePaths)
        {
            filePaths = null;
            // game
            var game = UltimaGame.UltimaOnline;
            // scheme
            proxySink = uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps
                ? new ProxySinkClient(uri)
                : new ProxySink();
            return game;
        }

        public static Task<IDataPack> GetUltimaDataPackAsync(this Uri uri)
        {
            var game = uri.ToUltimaGame(out var proxySink, out var filePaths);
            return Task.FromResult((IDataPack)new UltimaDataPack((uint)game));
        }
    }
}