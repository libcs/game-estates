using Game.Estate.Ultima.Resources.IO;
using System;
using System.IO;
using System.Text;
using static Game.Core.Debug;
using static Microsoft.Win32.Registry;

namespace Game.Estate.Ultima
{
    internal class UltimaFileManager
    {
        static readonly byte[] PatchVersion = ClientVersion.DefaultVersion;

        public static bool IsDataPresent => _fileDirectory != null;
        static string _fileDirectory;

        static readonly string[] _knownRegkeys = {
                @"Origin Worlds Online\Ultima Online\KR Legacy Beta",
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000",
                @"Origin Worlds Online\Ultima Online\1.0",
                @"Origin Worlds Online\Ultima Online Third Dawn\1.0",
                @"EA GAMES\Ultima Online Samurai Empire",
                @"EA Games\Ultima Online: Mondain's Legacy",
                @"EA GAMES\Ultima Online Samurai Empire\1.0",
                @"EA GAMES\Ultima Online Samurai Empire\1.00.0000",
                @"EA GAMES\Ultima Online: Samurai Empire\1.0",
                @"EA GAMES\Ultima Online: Samurai Empire\1.00.0000",
                @"EA Games\Ultima Online: Mondain's Legacy\1.0",
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000",
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\2d\1.0",
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\3d\1.0",
                @"Origin Worlds Online\Ultima Online Samurai Empire\2d\1.0",
                @"Origin Worlds Online\Ultima Online Samurai Empire\3d\1.0",
                @"Electronic Arts\EA Games\Ultima Online Stygian Abyss Classic",
                @"Electronic Arts\EA Games\Ultima Online Classic",
                @"Electronic Arts\EA Games\"
            };

        public static bool Is64Bit => IntPtr.Size == 8;
        public static int ItemIdMask => ClientVersion.InstallationIsUopFormat ? 0xffff : 0x3fff;

        static UltimaFileManager()
        {
            var b = new StringBuilder();
            b.AppendLine($"Looking for UO Installation: Is64Bit = {Is64Bit}");
            for (var i = 0; i < _knownRegkeys.Length; i++)
            {
                var exePath = GetExePath(Is64Bit ? $"Wow6432Node\\{_knownRegkeys[i]}" : _knownRegkeys[i]);
                if (exePath != null && Directory.Exists(exePath))
                {
                    if (IsClientIsCompatible(exePath))
                    {
                        b.AppendLine($"Compatible: {exePath}");
                        _fileDirectory = exePath;
                    }
                    else b.AppendLine($"Incompatible: {exePath}");
                }
            }
            if (_fileDirectory != null)
            {
                b.AppendLine($"Selected: {_fileDirectory}");
                var clientVersion = string.Join(".", ClientVersion.ClientExe);
                var patchVersion = string.Join(".", PatchVersion);
                b.AppendLine($"Client.Exe version: {clientVersion}; Patch version reported to server: {patchVersion}");
                if (!ClientVersion.EqualTo(PatchVersion, ClientVersion.DefaultVersion))
                    b.AppendLine($"Note from ZaneDubya: I will not support any code where the Patch version is not {string.Join(".", ClientVersion.DefaultVersion)}");
            }
            Log(b.ToString());
        }

        static bool IsClientIsCompatible(string path)
        {
            var files = Directory.EnumerateFiles(path);
            foreach (var filepath in files)
            {
                var extension = Path.GetExtension(filepath).ToLower();
                if (extension == ".uop")
                    return false;
            }
            return true;
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
                        return null;
                }
                var path = key.GetValue("ExePath") as string;
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = key.GetValue("Install Dir") as string;
                    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                    {
                        path = key.GetValue("InstallDir") as string;
                        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                            return null;
                    }
                }
                if (File.Exists(path))
                    path = Path.GetDirectoryName(path);
                return string.IsNullOrEmpty(path) || !Directory.Exists(path) ? null : path;
            }
            catch { return null; }
        }

        public static string GetFilePath(string path)
        {
            if (_fileDirectory != null)
            {
                path = Path.Combine(_fileDirectory, path);
                if (File.Exists(path))
                    return path;
            }
            return null;
        }
        public static string[] GetFilePaths(string searchPattern) => Directory.GetFiles(_fileDirectory, searchPattern);

        public static bool Exists(string name)
        {
            try { return File.Exists(Path.Combine(_fileDirectory, name)); }
            catch { return false; }
        }
        public static bool Exists(string name, int index, string type) => Exists($"{name}{index}.{type}");
        public static bool Exists(string name, string type) => Exists($"{name}.{type}");

        public static FileStream GetFile(string path)
        {
            try { return new FileStream(Path.Combine(_fileDirectory, path), FileMode.Open, FileAccess.Read, FileShare.ReadWrite); }
            catch { return null; }
        }

        public static FileStream GetFile(string name, uint index, string type) => GetFile($"{name}{index}.{type}");
        public static FileStream GetFile(string name, string type) => GetFile($"{name}.{type}");
        public static string GetPath(string name) => Path.Combine(_fileDirectory, name);
        public static AFileIndex CreateFileIndex(string uopFile, int length, bool hasExtra, string extension) => new UopFileIndex(GetPath(uopFile), length, hasExtra, extension);
        public static AFileIndex CreateFileIndex(string idxFile, string mulFile, int length, int patch_file) => new MulFileIndex(GetPath(idxFile), GetPath(mulFile), length, patch_file);
    }
}
