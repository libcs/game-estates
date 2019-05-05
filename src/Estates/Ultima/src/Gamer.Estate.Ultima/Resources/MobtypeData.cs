using Gamer.Core;
using System.Collections.Generic;
using System.IO;

namespace Gamer.Estate.Ultima.Resources
{
    public static class MobtypeData
    {
        static readonly Dictionary<int, MobtypeEntry> _entries = new Dictionary<int, MobtypeEntry>();

        static MobtypeData()
        {
            var path = FileManager.GetFilePath("mobtypes.txt");
            {
                var r = new StreamReader(path);
                while (!r.EndOfStream)
                {
                    var line = r.ReadLine();
                    Metrics.ReportDataRead(line.Length);
                    if ((line != string.Empty) && (line.Substring(0, 1) != "#"))
                    {
                        var data = line.Split('\t');
                        var bodyId = int.Parse(data[0]);
                        if (_entries.ContainsKey(bodyId))
                            _entries.Remove(bodyId);
                        _entries.Add(bodyId, new MobtypeEntry(data[1], data[2]));
                    }
                }
            }
        }

        public static MobType AnimationTypeXXX(int bodyId) => _entries[bodyId].AnimationType;
    }

    public struct MobtypeEntry
    {
        public string Flags;
        public MobType AnimationType;

        public MobtypeEntry(string type, string flags)
        {
            Flags = flags;
            switch (type)
            {
                case "MONSTER": AnimationType = MobType.Monster; break;
                case "ANIMAL": AnimationType = MobType.Animal; break;
                case "SEA_MONSTER": AnimationType = MobType.Monster; break;
                case "HUMAN": AnimationType = MobType.Humanoid; break;
                case "EQUIPMENT": AnimationType = MobType.Humanoid; break;
                default: AnimationType = MobType.Null; break;
            }
        }
    }

    public enum MobType
    {
        Null = -1,
        Monster = 0,
        Animal = 1,
        Humanoid = 2
    }
}
