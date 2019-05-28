using Gamer.Proxy;
using System;

namespace Gamer.Estate.Cry
{
    public static class CryExtensions
    {
        public static CryGame ToCryGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths) => ProxyUtils.ToGame<CryGame>(uri, func, out proxySink, out filePaths, "Cry", (path, game) => CryFileManager.GetFilePaths("Data.p4k", game));
    }
}