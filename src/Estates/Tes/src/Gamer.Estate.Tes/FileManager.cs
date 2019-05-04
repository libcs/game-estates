using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Gamer.Asset.Tes.FilePack;
using static Microsoft.Win32.Registry;

namespace Gamer.Asset.Tes
{
    public class FileManager
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

        public static bool Is64Bit => true; // IntPtr.Size == 8;

        static FileManager()
        {
            for (var i = 0; i < _knownRegkeys.Length; i += 2)
            {
                var exePath = GetExePath(Is64Bit ? $"Wow6432Node\\{(string)_knownRegkeys[i]}" : (string)_knownRegkeys[i]);
                if (exePath != null && Directory.Exists(exePath))
                {
                    var dataPath = Path.Combine(exePath, "Data");
                    var gameId = (TesGame)_knownRegkeys[i + 1];
                    if (Directory.Exists(dataPath))
                        _fileDirectories.Add(gameId, dataPath);
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

        public static string GetFilePath(string path, TesGame gameId) =>
            _fileDirectories.TryGetValue(gameId, out var fileDirectory)
                ? File.Exists(path = Path.Combine(fileDirectory, path)) ? path : null
                : null;

        public static string[] GetFilePaths(string searchPattern, TesGame gameId) =>
            _fileDirectories.TryGetValue(gameId, out var fileDirectory)
                ? Directory.GetFiles(fileDirectory, searchPattern)
                : null;
    }
}
