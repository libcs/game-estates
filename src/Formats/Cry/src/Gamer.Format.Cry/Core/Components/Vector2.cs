using System;

namespace Gamer.Format.Cry.Core.Components
{
    public class Vector2
    {
        float _x;
        float _y;

        ByteArray xBA = new ByteArray();
        ByteArray yBA = new ByteArray();

        public float x { get => xBA.float1; set { _x = value; xBA.float1 = value; } }
        public float y { get => yBA.float1; set { _y = value; yBA.float1 = value; } }

        public float X { get => xBA.float1; set { _x = value; xBA.float1 = value; } }
        public float Y { get => yBA.float1; set { _y = value; yBA.float1 = value; } }

        public int xint { get => xBA.int1; set => xBA.int1 = value; }
        public int yint { get => yBA.int1; set => yBA.int1 = value; }

        public uint xuint { get => xBA.uint1; set => xBA.uint1 = value; }
        public uint yuint { get => yBA.uint1; set => yBA.uint1 = value; }

        public Vector2() { }
        public Vector2(double x, double y) { _x = (float)x; _y = (float)y; }
        public Vector2(Vector2 vector) { _x = vector.x; _y = vector.y; }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices must run from 0 to 1.");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices must run from 0 to 1.");
                }
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj) => obj == null ? false : obj is Vector2 ? this == (Vector2)obj : false;
        public override string ToString() => $"{X},{Y}";
        public static bool operator ==(Vector2 left, Vector2 right) => right is null ? left is null : left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Vector2 left, Vector2 right) => !(left == right);
        public bool IsZero(float epsilon = 0) => Math.Abs(x) <= epsilon && Math.Abs(y) <= epsilon;
        public float Dot(Vector2 v) => X * v.X + Y * v.Y;     
    }
}
