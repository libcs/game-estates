using System.Collections.Generic;
using System.IO;
using static Microsoft.Win32.Registry;

namespace Game.Estate.UltimaIX
{
    public class UltimaIXFileManager
    {
        public static bool IsDataPresent => _fileDirectories.Count != 0;

        static readonly object[] _knownRegkeys = {
            @"GOG.com\GOGULTIMA9", UltimaIXGame.UltimaIX,
            @"GOG.com\Games\1207659093", UltimaIXGame.UltimaIX,
        };

        static Dictionary<UltimaIXGame, string> _fileDirectories = new Dictionary<UltimaIXGame, string>();

        public static bool Is64Bit => true;

        static UltimaIXFileManager()
        {
            for (var i = 0; i < _knownRegkeys.Length; i += 2)
            {
                var exePath = GetExePath(Is64Bit ? $"Wow6432Node\\{(string)_knownRegkeys[i]}" : (string)_knownRegkeys[i]);
                if (exePath != null && Directory.Exists(exePath))
                {
                    var game = (UltimaIXGame)_knownRegkeys[i + 1];
                    if (Directory.Exists(exePath))
                        _fileDirectories.Add(game, exePath);
                    break;
                }
            }
        }

        static string GetExePath(string subName)
        {
            try
            {
                var key = LocalMachine.OpenSubKey($"SOFTWARE\\{subName}");
                if (key == null)
                {
                    key = CurrentUser.OpenSubKey($"SOFTWARE\\{subName}");
                    if (key == null)
                    {
                        key = ClassesRoot.OpenSubKey($"VirtualStore\\MACHINE\\SOFTWARE\\{subName}");
                        if (key == null)
                            return null;
                    }
                }
                var path = key.GetValue("EXE") as string;
                if (File.Exists(path))
                    path = Path.GetDirectoryName(path);
                return string.IsNullOrEmpty(path) || !Directory.Exists(path) ? null : path;
            }
            catch { return null; }
        }

        public static string[] GetFilePaths(bool many, string pathOrPattern, UltimaIXGame game) =>
            _fileDirectories.TryGetValue(game, out var fileDirectory)
                ? string.IsNullOrEmpty(pathOrPattern) ? new[] { fileDirectory }
                : many ? Directory.GetFiles(fileDirectory, pathOrPattern) : File.Exists(pathOrPattern = Path.Combine(fileDirectory, pathOrPattern)) ? new[] { pathOrPattern } : null
                : null;
    }
}
