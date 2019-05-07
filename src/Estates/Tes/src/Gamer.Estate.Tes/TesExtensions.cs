using Gamer.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Estate.Tes
{
    public static class TesExtensions
    {
        public static TesGame ToGame(this Uri uri, out string path)
        {
            path = uri.Scheme == "file" ? uri.LocalPath : uri.LocalPath.Substring(1);
            // host
            var host = uri.Host;
            if (host.StartsWith("#"))
                host = host.Substring(1);
            var game = Enum.GetNames(typeof(TesGame)).FirstOrDefault(x => string.Equals(x, host, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(host), host);
            return (TesGame)Enum.Parse(typeof(TesGame), game);
        }

        public static Task<IDataPack> GetDataPackAsync(this Uri uri)
        {
            var game = uri.ToGame(out var path);
            var filePath = FileManager.GetFilePath(path, game);
            if (filePath == null)
                throw new InvalidOperationException($"{game} not available");
            return Task.FromResult((IDataPack)new TesDataPack(filePath, game));
        }
    }
}