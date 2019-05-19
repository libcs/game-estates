using Gamer.Proxy;
using Gamer.Proxy.Server;
using System;
using System.Linq;

namespace Gamer.Estate.Cry
{
    public static class CryExtensions
    {
        public static CryGame ToCryGame(this Uri uri, Func<HttpResponse> resFunc, out ProxySink proxySink, out string filePath)
        {
            var path = "Data.p4k";
            // game
            var fragment = uri.Scheme == "game" || uri.Scheme == "serv" ? uri.Host : uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var gameName = Enum.GetNames(typeof(CryGame)).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(uri), uri.ToString());
            var game = (CryGame)Enum.Parse(typeof(CryGame), gameName);
            // scheme
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            {
                proxySink = new ProxySinkClient(uri, "Cry");
                filePath = null;
            }
            else
            {
                proxySink = uri.Scheme == "serv" ? new ProxySinkServer(resFunc) : new ProxySink();
                filePath = FileManager.GetFilePath(path, game) ?? throw new InvalidOperationException($"{game} not available");
            }
            return game;
        }
    }
}