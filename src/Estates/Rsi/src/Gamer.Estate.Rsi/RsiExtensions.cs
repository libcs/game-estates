using Gamer.Proxy;
using System;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions
    {
        public static RsiGame ToRsiGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths) => ProxyUtils.ToGame<RsiGame>(uri, func, out proxySink, out filePaths, UnityEngine.Application.platform.ToString(), "Rsi", (path, game) => RsiFileManager.GetFilePaths("Data.p4k", game));
    }
}