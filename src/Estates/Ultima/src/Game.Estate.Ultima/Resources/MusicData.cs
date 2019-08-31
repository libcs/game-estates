using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Estate.Ultima.Resources
{
    public class MusicData
    {
        const string _configFilePath = @"Music/Digital/Config.txt";
        static readonly char[] _configFileDelimiters = { ' ', ',', '\t' };

        static readonly Dictionary<int, (string, bool)> _musicData = new Dictionary<int, (string, bool)>();

        static MusicData()
        {
            // open UO's music Config.txt
            if (!UltimaFileManager.Exists(_configFilePath))
                return;
            // attempt to read out all the values from the file.
            string line;
            using (var r = new StreamReader(UltimaFileManager.GetFile(_configFilePath)))
                while ((line = r.ReadLine()) != null)
                    if (TryParseConfigLine(line, out var songData))
                        _musicData.Add(songData.Item1, (songData.Item2, songData.Item3));
        }

        /// <summary>
        /// Attempts to parse a line from UO's music Config.txt.
        /// </summary>
        /// <param name="line">A line from the file.</param>
        /// <param name="?">If successful, contains a tuple with these fields: int songIndex, string songName, bool doesLoop</param>
        /// <returns>true if line could be parsed, false otherwise.</returns>
        static bool TryParseConfigLine(string line, out Tuple<int, string, bool> songData)
        {
            songData = null;
            var splits = line.Split(_configFileDelimiters);
            if (splits.Length < 2 || splits.Length > 3)
                return false;
            var index = int.Parse(splits[0]);
            var name = splits[1].Trim();
            var doesLoop = splits.Length == 3 && splits[2] == "loop";
            songData = new Tuple<int, string, bool>(index, name, doesLoop);
            return true;
        }

        public static bool TryGetMusicData(int index, out string name, out bool doesLoop)
        {
            name = null;
            doesLoop = false;
            if (_musicData.ContainsKey(index))
            {
                name = _musicData[index].Item1;
                doesLoop = _musicData[index].Item2;
                return true;
            }
            return false;
        }
    }
}
