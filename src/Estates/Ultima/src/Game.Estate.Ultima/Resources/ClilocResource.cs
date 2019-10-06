using Game.Core;
using System;
using System.Collections;
using System.IO;
using System.Text;
using static Game.Core.CoreDebug;

namespace Game.Estate.Ultima.Resources
{
    public class ClilocResource
    {
        Hashtable _table;

        public readonly string Language;

        public ClilocResource(string language)
        {
            Language = language;
            LoadAllClilocs(language);
        }

        public string GetString(int index)
        {
            if (_table[index] == null)
            {
                Log($"Missing cliloc with index {index}. Client version is lower than expected by Server.");
                return $"Err: Cliloc Entry {index} not found.";
            }
            return _table[index].ToString();
        }

        void LoadAllClilocs(string language)
        {
            _table = new Hashtable();
            var mainClilocFile = $"Cliloc.{language}";
            LoadCliloc(mainClilocFile);
            // All the other Cliloc*.language files:
            /*var paths = FileManager.GetFilePaths($"cliloc*.{language}");
            foreach (string path in paths)
                if (path != mainClilocFile)
                    LoadCliloc(path);
            */
        }

        void LoadCliloc(string path)
        {
            path = UltimaFileManager.GetFilePath(path);
            if (path == null)
                return;
            byte[] buffer;
            using (var r = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                buffer = r.ReadBytes((int)r.BaseStream.Length);
                Metrics.ReportDataRead((int)r.BaseStream.Position);
            }
            var pos = 6;
            var count = buffer.Length;
            while (pos < count)
            {
                var number = BitConverter.ToInt32(buffer, pos);
                var length = BitConverter.ToInt16(buffer, pos + 5);
                var text = Encoding.UTF8.GetString(buffer, pos + 7, length);
                pos += length + 7;
                _table[number] = text; // auto replace with updates.
            }
        }

        class StringEntry
        {
            public int Number { get; }
            public string Text { get; }

            public StringEntry(int number, string text)
            {
                Number = number;
                Text = text;
            }
        }
    }
}