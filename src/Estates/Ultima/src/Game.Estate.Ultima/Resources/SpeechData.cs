using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Game.Estate.Ultima.Resources
{
    public class SpeechEntry
    {
        public int Index { get; }
        public List<string> Strings { get; }
        public List<Regex> Regex { get; }

        public SpeechEntry(int index)
        {
            Index = index;
            Strings = new List<string>();
            Regex = new List<Regex>();
        }
    }

    public class SpeechEntrySorter : IComparer<SpeechEntry>
    {
        public int Compare(SpeechEntry x, SpeechEntry y) => x.Index.CompareTo(y.Index);
    }

    public class SpeechData
    {
        static List<Dictionary<int, SpeechEntry>> _table;

        public static void GetSpeechTriggers(string text, string lang, out int count, out int[] triggers)
        {
            if (_table == null)
                _table = LoadSpeechFile();
            var t = new List<int>();
            var speechTable = 0; // "ENU/0"
            foreach (var e in _table[speechTable])
                for (var i = 0; i < e.Value.Regex.Count; i++)
                    if (e.Value.Regex[i].IsMatch(text))
                        if (!t.Contains(e.Key))
                            t.Add(e.Key);
            count = t.Count;
            triggers = t.ToArray();
        }

        static List<Dictionary<int, SpeechEntry>> LoadSpeechFile()
        {
            var tables = new List<Dictionary<int, SpeechEntry>>();
            var lastIndex = -1;
            Dictionary<int, SpeechEntry> table = null;
            var path = UltimaFileManager.GetFilePath("speech.mul");
            using (var r = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                while (r.PeekChar() >= 0)
                {
                    var index = (r.ReadByte() << 8) | r.ReadByte();
                    var length = (r.ReadByte() << 8) | r.ReadByte();
                    var text = Encoding.UTF8.GetString(r.ReadBytes(length)).Trim();
                    if (text.Length == 0)
                        continue;
                    if (table == null || lastIndex > index)
                    {
                        if (index == 0 && text == "*withdraw*") tables.Insert(0, table = new Dictionary<int, SpeechEntry>());
                        else tables.Add(table = new Dictionary<int, SpeechEntry>());
                    }
                    lastIndex = index;
                    table.TryGetValue(index, out SpeechEntry entry);
                    if (entry == null)
                        table[index] = entry = new SpeechEntry(index);
                    entry.Strings.Add(text);
                    entry.Regex.Add(new Regex(text.Replace("*", @".*"), RegexOptions.IgnoreCase));
                }
                return tables;
            }
        }
    }
}
