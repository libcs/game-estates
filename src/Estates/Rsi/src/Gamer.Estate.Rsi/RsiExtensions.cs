using System;
using System.Linq;

namespace Gamer.Estate.Rsi
{
    public static class RsiExtensions
    {
        public static RsiGame ToGame(this Uri uri, out string path)
        {
            path = "Data.p4k"; //path = uri.Scheme == "file" ? uri.LocalPath : uri.LocalPath.Substring(1);
            // host
            var host = uri.Host;
            if (host.StartsWith("#"))
                host = host.Substring(1);
            var game = Enum.GetNames(typeof(RsiGame)).FirstOrDefault(x => string.Equals(x, host, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(host), host);
            return (RsiGame)Enum.Parse(typeof(RsiGame), game);
        }
    }
}