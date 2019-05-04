using Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Asset.Tes
{
    public static class TesExtensions
    {
        static TesGame ToGame(this Uri uri, out string path)
        {
            path = uri.Scheme == "file" ? uri.LocalPath : uri.LocalPath.Substring(1);
            // host
            var host = uri.Host;
            if (host.StartsWith("#"))
                host = host.Substring(1);
            var game = Enum.GetNames(typeof(TesGame)).FirstOrDefault(x => string.Equals(x, host, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(host), host);
            return (TesGame)Enum.Parse(typeof(TesGame), game);
        }

        public static Task<IAssetPack> GetAssetPack(this Uri uri)
        {
            var game = uri.ToGame(out var path);
            var filePaths = FileManager.GetFilePaths(path, game);
            if (filePaths == null)
                throw new InvalidOperationException($"{game} not available");
            return Task.FromResult((IAssetPack)new TesAssetPack(filePaths));
        }
    }
}