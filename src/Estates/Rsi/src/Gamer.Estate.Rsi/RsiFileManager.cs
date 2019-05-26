using System.IO;
using static Gamer.Core.Debug;
using static Microsoft.Win32.Registry;

namespace Gamer.Estate.Rsi
{
    public class RsiFileManager
    {
        public static bool IsDataPresent => _fileDirectory != null;
        static string _fileDirectory;

        static readonly object[] _knownRegkeys = {
            //@"Cloud Imperium Games", RsiGame.StarCitizen,
        };

        public static bool Is64Bit => true;

        static RsiFileManager()
        {
            Log("Looking for RSI Installation:");
            _fileDirectory = @"D:\Roberts Space Industries\StarCitizen\LIVE";
            //var x = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //for (var i = 0; i < _knownRegkeys.Length; i += 2)
            //{
            //    var exePath = GetExePath(Is64Bit ? $"Wow6432Node\\{(string)_knownRegkeys[i]}" : (string)_knownRegkeys[i]);
            //    if (exePath != null && Directory.Exists(exePath))
            //    {
            //        Log($"Compatible: {exePath}");
            //        _fileDirectory = exePath;
            //        break;
            //    }
            //}
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

        public static string[] GetFilePaths(string path, RsiGame game) => File.Exists(path = Path.Combine(_fileDirectory, path)) ? new[] { path } : null;
    }
}
