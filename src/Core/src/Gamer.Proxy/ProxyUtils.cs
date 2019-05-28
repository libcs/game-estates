using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gamer.Proxy
{
    public static class ProxyUtils
    {
        public static T ToGame<T>(Uri uri, Func<object> func, out ProxySink proxySink, out string[] filePaths, string estateKey, Func<string, T, string[]> fileManager)
        {
            var platform = Core.Debug.Platform;
            // game
            var fragment = uri.Fragment?.Substring(uri.Fragment.Length != 0 ? 1 : 0);
            var gameName = Enum.GetNames(typeof(T)).FirstOrDefault(x => string.Equals(x, fragment, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentOutOfRangeException(nameof(uri), uri.ToString());
            var game = (T)Enum.Parse(typeof(T), gameName);
            // scheme
            if (uri.IsFile)
            {
                var path = uri.LocalPath;
                proxySink = new ProxySink();
                filePaths = path.Contains('*') ? Directory.GetFiles(Path.GetDirectoryName(path), Path.GetFileName(path)) : File.Exists(path) ? new[] { path } : null;
            }
            else if (string.IsNullOrEmpty(uri.Host) || uri.Host == "serv")
            {
                var path = uri.LocalPath.Substring(1);
                proxySink = uri.Host != "serv" ? new ProxySink() : new ProxySinkServer(func);
                filePaths = fileManager(path, game) ?? throw new InvalidOperationException($"{game} not available");
            }
            else
            {
                proxySink = new ProxySinkClient(uri, platform, estateKey);
                filePaths = new[] { uri.LocalPath };
            }
            return game;
        }

        public static byte[] ToBytes<T>(bool schemeGame, T data)
        {
            if (typeof(T) == typeof(byte[])) return (byte[])(object)data;
            else if (typeof(T) == typeof(HashSet<string>))
            {
                if (!schemeGame)
                    throw new NotSupportedException();
                // scheme - game
                var d = (HashSet<string>)(object)data;
                using (var s = new MemoryStream())
                using (var w = new BinaryWriter(s))
                {
                    w.Write(d.Count);
                    foreach (var x in d)
                        w.Write(x);
                    return s.ToArray();
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(T), typeof(T).ToString());
        }

        public static T FromBytes<T>(bool schemeGame, byte[] data)
        {
            if (typeof(T) == typeof(byte[])) return (T)(object)data;
            else if (typeof(T) == typeof(HashSet<string>))
            {
                var d = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                if (!schemeGame)
                {
                    // dir /s/b/a-d > .set
                    var lines = Encoding.ASCII.GetString(data)?.Split('\n');
                    if (lines?.Length >= 0)
                    {
                        var startIndex = Path.GetDirectoryName(lines[0].TrimEnd()).Length + 1;
                        foreach (var line in lines)
                            if (line.Length >= startIndex)
                                d.Add(line.Substring(startIndex).TrimEnd().Replace('\\', '/'));
                    }
                    return (T)(object)d;
                }
                // scheme - game
                using (var s = new MemoryStream(data))
                using (var w = new BinaryReader(s))
                {
                    var count = w.ReadInt32();
                    for (var i = 0; i < count; i++)
                        d.Add(w.ReadString());
                    return (T)(object)d;
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(T), typeof(T).ToString());
        }
    }
}
