using Gamer.Core;
using Gamer.Proxy;
using System;
using System.Threading.Tasks;

namespace Gamer.Estate.Ultima
{
    public static class UltimaExtensions
    {
        public static UltimaGame ToUltimaGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths) => ProxyUtils.ToGame<UltimaGame>(uri, func, out proxySink, out filePaths, UnityEngine.Application.platform.ToString(), "Ultima", (path, game) => new string[0]);

        public static Task<IDataPack> GetUltimaDataPackAsync(this Uri uri, Func<object> func = null)
        {
            var game = uri.ToUltimaGame(func, out var proxySink, out var filePaths);
            return Task.FromResult((IDataPack)new UltimaDataPack((uint)game));
        }
    }
}