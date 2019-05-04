using System;
using System.Linq;

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
    }
}