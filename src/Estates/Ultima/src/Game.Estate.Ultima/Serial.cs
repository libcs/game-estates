using System;

namespace Game.Estate.Ultima
{
    public struct Serial : IComparable, IComparable<Serial>
    {
        public static Serial ProtectedAction = int.MinValue;

        public static Serial Null => 0;

        public readonly static Serial World = unchecked((int)0xFFFFFFFF);

        Serial(int serial) => Value = serial;

        public int Value { get; }

        public bool IsMobile => Value > 0 && Value < 0x40000000;

        public bool IsItem => Value >= 0x40000000;

        public bool IsValid => Value > 0;

        public bool IsDynamic => Value < 0;

        static int _nextDynamicSerial = -1;
        public static int NewDynamicSerial => _nextDynamicSerial--;

        public override int GetHashCode() => Value;

        public int CompareTo(Serial other) => Value.CompareTo(other.Value);

        public int CompareTo(object other)
        {
            if (other is Serial)
                return CompareTo((Serial)other);
            else if (other == null)
                return -1;
            throw new ArgumentException();
        }

        public override bool Equals(object o) => o == null || !(o is Serial) ? false : ((Serial)o).Value == Value;

        public static bool operator ==(Serial l, Serial r) => l.Value == r.Value;

        public static bool operator !=(Serial l, Serial r) => l.Value != r.Value;

        public static bool operator >(Serial l, Serial r) => l.Value > r.Value;

        public static bool operator <(Serial l, Serial r) => l.Value < r.Value;

        public static bool operator >=(Serial l, Serial r) => l.Value >= r.Value;

        public static bool operator <=(Serial l, Serial r) => l.Value <= r.Value;

        public override string ToString() => $"0x{Value:X8}";

        public static implicit operator int(Serial a) => a.Value;

        public static implicit operator Serial(int a) => new Serial(a);
    }
}
