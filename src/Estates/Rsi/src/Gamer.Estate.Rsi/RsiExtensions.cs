using Gamer.Proxy;
using System;
using System.Linq;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions
    {
        public static RsiGame ToRsiGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string filePath)
        {
            var path = "Data.p4k";
            // game
            var fragment = uri.Scheme == "game" || uri.Scheme == "serv" ? uri.Host : uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var gameName = Enum.GetNames(typeof(RsiGame)).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(uri), uri.ToString());
            var game = (RsiGame)Enum.Parse(typeof(RsiGame), gameName);
            // scheme
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            {
                proxySink = new ProxySinkClient(uri, "Rsi");
                filePath = null;
            }
            else
            {
                proxySink = uri.Scheme == "serv" ? new ProxySinkServer(func) : new ProxySink();
                filePath = FileManager.GetFilePath(path, game) ?? throw new InvalidOperationException($"{game} not available");
            }
            return game;
        }
    }
}