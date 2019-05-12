using System;
using System.Linq;

namespace Gamer.Estate.Cry
{
    public static class CryExtensions
    {
        public static CryGame ToGame(this Uri uri, out string path)
        {
            path = "Data.p4k"; //path = uri.Scheme == "file" ? uri.LocalPath : uri.LocalPath.Substring(1);
            // host
            var host = uri.Host;
            if (host.StartsWith("#"))
                host = host.Substring(1);
            var game = Enum.GetNames(typeof(CryGame)).FirstOrDefault(x => string.Equals(x, host, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(host), host);
            return (CryGame)Enum.Parse(typeof(CryGame), game);
        }
    }
}