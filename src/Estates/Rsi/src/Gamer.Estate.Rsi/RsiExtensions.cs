using Gamer.Proxy;
using System;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions
    {
        public static RsiGame ToRsiGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths) => ProxySink.ToGame<RsiGame>(uri, func, out proxySink, out filePaths, "Rsi", (path, game) => RsiFileManager.GetFilePaths("Data.p4k", game));
    }
}