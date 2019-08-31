using System.Collections.Generic;
using System.IO;
using static Microsoft.Win32.Registry;

namespace Game.Estate.Tes
{
    internal class TesFileManager
    {
        public static bool IsDataPresent => _fileDirectories.Count != 0;

        static readonly object[] _knownRegkeys = {
            @"Bethesda Softworks\Oblivion", TesGame.Oblivion,
            @"Bethesda Softworks\Skyrim", TesGame.Skyrim,
            @"Bethesda Softworks\Fallout 3", TesGame.Fallout3,
            @"Bethesda Softworks\Fallout NV", TesGame.FalloutNV,
            @"Bethesda Softworks\Morrowind", TesGame.Morrowind,
            @"Bethesda Softworks\Fallout 4", TesGame.Fallout4,
            @"Bethesda Softworks\Skyrim SE", TesGame.SkyrimSE,
            @"Bethesda Softworks\Fallout 4 VR", TesGame.Fallout4VR,
            @"Bethesda Softworks\Skyrim VR", TesGame.SkyrimVR
        };

        static Dictionary<TesGame, string> _fileDirectories = new Dictionary<TesGame, string>();

        public static bool Is64Bit => true;

        static TesFileManager()
        {
            for (var i = 0; i < _knownRegkeys.Length; i += 2)
            {
                var exePath = GetExePath(Is64Bit ? $"Wow6432Node\\{(string)_knownRegkeys[i]}" : (string)_knownRegkeys[i]);
                if (exePath != null && Directory.Exists(exePath))
                {
                    var dataPath = Path.Combine(exePath, "Data");
                    var game = (TesGame)_knownRegkeys[i + 1];
                    if (Directory.Exists(dataPath))
                        _fileDirectories.Add(game, dataPath);
                }
            }
            HardAdds();
        }

        static void HardAdds()
        {
            var morrowind = @"C:\Program Files (x86)\Steam\steamapps\common\Morrowind";
            if (Directory.Exists(morrowind))
            {
                var dataPath = Path.Combine(morrowind, "Data Files");
                _fileDirectories.Add(TesGame.Morrowind, dataPath);
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
                var path = key.GetValue("Installed Path") as string;
                if (File.Exists(path))
                    path = Path.GetDirectoryName(path);
                return string.IsNullOrEmpty(path) || !Directory.Exists(path) ? null : path;
            }
            catch { return null; }
        }

        public static string[] GetFilePaths(bool many, string pathOrPattern, TesGame game) =>
            _fileDirectories.TryGetValue(game, out var fileDirectory)
                ? many ? Directory.GetFiles(fileDirectory, pathOrPattern) : File.Exists(pathOrPattern = Path.Combine(fileDirectory, pathOrPattern)) ? new[] { pathOrPattern } : null
                : null;
    }
}
