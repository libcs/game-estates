using System;
using System.IO;

namespace Game.Estate.Ultima.Data
{
    public enum BodyType : byte
    {
        Empty,
        Monster,
        Sea,
        Animal,
        Human,
        Equipment
    }

    public struct Body
    {
        static BodyType[] _types;

        static Body()
        {
            var path = @"data/bodytable.cfg";
            if (File.Exists(path))
                using (var ip = new StreamReader(path))
                {
                    _types = new BodyType[0x1000];
                    string line;
                    while ((line = ip.ReadLine()) != null)
                    {
                        if (line.Length == 0 || line.StartsWith("#"))
                            continue;
                        var split = line.Split('\t');
                        if (int.TryParse(split[0], out var bodyId) && Enum.TryParse(split[1], true, out BodyType type) && bodyId >= 0 && bodyId < _types.Length)
                            _types[bodyId] = type;
                        else
                        {
                            Console.WriteLine("Warning: Invalid bodyTable entry:");
                            Console.WriteLine(line);
                        }
                    }
                }
            else
            {
                Console.WriteLine("Warning: Data/bodyTable.cfg does not exist");
                _types = new BodyType[0];
            }
        }

        public Body(int bodyId) => BodyId = bodyId;

        public BodyType Type => BodyId >= 0 && BodyId < _types.Length ? _types[BodyId] : BodyType.Empty;

        public bool IsHumanoid =>
            BodyId >= 0
            && BodyId < _types.Length
            && _types[BodyId] == BodyType.Human
            && BodyId != 402
            && BodyId != 403
            && BodyId != 607
            && BodyId != 608
            && BodyId != 694
            && BodyId != 695
            && BodyId != 970;

        public bool IsGargoyle =>
            BodyId == 666
            || BodyId == 667
            || BodyId == 694
            || BodyId == 695;

        public bool IsMale =>
            BodyId == 183
            || BodyId == 185
            || BodyId == 400
            || BodyId == 402
            || BodyId == 605
            || BodyId == 607
            || BodyId == 666
            || BodyId == 694
            || BodyId == 750;

        public bool IsFemale =>
            BodyId == 184
            || BodyId == 186
            || BodyId == 401
            || BodyId == 403
            || BodyId == 606
            || BodyId == 608
            || BodyId == 667
            || BodyId == 695
            || BodyId == 751;

        public bool IsGhost =>
            BodyId == 402
            || BodyId == 403
            || BodyId == 607
            || BodyId == 608
            || BodyId == 694
            || BodyId == 695
            || BodyId == 970;

        public bool IsMonster => 
            BodyId >= 0
            && BodyId < _types.Length
            && _types[BodyId] == BodyType.Monster;

        public bool IsAnimal => 
            BodyId >= 0
            && BodyId < _types.Length
            && _types[BodyId] == BodyType.Animal;

        public bool IsEmpty => 
            BodyId >= 0
            && BodyId < _types.Length
            && _types[BodyId] == BodyType.Empty;

        public bool IsSea => 
            BodyId >= 0
            && BodyId < _types.Length
            && _types[BodyId] == BodyType.Sea;

        public bool IsEquipment => 
            BodyId >= 0
            && BodyId < _types.Length
            && _types[BodyId] == BodyType.Equipment;

        public int BodyId { get; }

        public static implicit operator int(Body a) => a.BodyId;

        public static implicit operator Body(int a) => new Body(a);

        public override string ToString() => $"0x{BodyId:X}";

        public override int GetHashCode() => BodyId;

        public override bool Equals(object o) => o == null || !(o is Body) ? false : ((Body)o).BodyId == BodyId;

        public static bool operator ==(Body l, Body r) => l.BodyId == r.BodyId;

        public static bool operator !=(Body l, Body r) => l.BodyId != r.BodyId;

        public static bool operator >(Body l, Body r) => l.BodyId > r.BodyId;

        public static bool operator >=(Body l, Body r) => l.BodyId >= r.BodyId;

        public static bool operator <(Body l, Body r) => l.BodyId < r.BodyId;

        public static bool operator <=(Body l, Body r) => l.BodyId <= r.BodyId;
    }
}