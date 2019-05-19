using Gamer.Core;
using Gamer.Proxy;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public static class TesExtensions
    {
        public static TesGame ToTesGame(this Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths)
        {
            // game
            var fragment = uri.Scheme == "game" || uri.Scheme == "serv" ? uri.Host : uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var gameName = Enum.GetNames(typeof(TesGame)).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(uri), uri.ToString());
            var game = (TesGame)Enum.Parse(typeof(TesGame), gameName);
            // scheme
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            {
                proxySink = new ProxySinkClient(uri, "Tes");
                filePaths = null;
            }
            else
            {
                var path = uri.IsFile ? uri.LocalPath : uri.LocalPath.Substring(1);
                proxySink = uri.Scheme == "serv" ? new ProxySinkServer(func) : new ProxySink();
                var many = Path.GetExtension(path) == ".bsa" || Path.GetExtension(path) == ".ba2";
                filePaths = FileManager.GetFilePaths(many, path, game) ?? throw new InvalidOperationException($"{game} not available");
            }
            return game;
        }

        public static Task<IDataPack> GetTesDataPackAsync(this Uri uri, Func<object> func = null)
        {
            var game = uri.ToTesGame(func, out var client, out var filePaths);
            return Task.FromResult((IDataPack)new TesDataPack(client, filePaths.Single(), game));
        }
    }
}