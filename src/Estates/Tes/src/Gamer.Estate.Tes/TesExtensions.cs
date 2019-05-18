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
        public static TesGame ToTesGame(this Uri uri, out ProxySink proxySink, out string[] filePaths)
        {
            var path = uri.IsFile ? uri.LocalPath : uri.LocalPath.Substring(1);
            // game
            var host = uri.Host;
            if (host.StartsWith("#"))
                host = host.Substring(1);
            var gameName = Enum.GetNames(typeof(TesGame)).FirstOrDefault(x => string.Equals(x, host, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(host), host);
            var game = (TesGame)Enum.Parse(typeof(TesGame), gameName);
            // scheme
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            {
                proxySink = new ProxySinkClient(uri);
                filePaths = null;
            }
            else
            {
                proxySink = new ProxySink();
                var many = Path.GetExtension(path) == ".bsa" || Path.GetExtension(path) == ".ba2";
                filePaths = FileManager.GetFilePaths(many, path, game) ?? throw new InvalidOperationException($"{game} not available");
            }
            return game;
        }

        public static Task<IDataPack> GetTesDataPackAsync(this Uri uri)
        {
            var game = uri.ToTesGame(out var client, out var filePaths);
            return Task.FromResult((IDataPack)new TesDataPack(client, filePaths.Single(), game));
        }
    }
}