using System;
using static Game.Core.Debug;

namespace Game.Format.Cry.Core.Components
{
    public class Vector4
    {
        float _x;
        float _y;
        float _z;
        float _w;

        ByteArray xBA = new ByteArray();
        ByteArray yBA = new ByteArray();
        ByteArray zBA = new ByteArray();
        ByteArray wBA = new ByteArray();

        public float x { get => xBA.float1; set { _x = value; xBA.float1 = value; } }
        public float y { get => yBA.float1; set { _y = value; yBA.float1 = value; } }
        public float z { get => zBA.float1; set { _z = value; zBA.float1 = value; } }
        public float w { get => wBA.float1; set { _w = value; wBA.float1 = value; } }

        public float X { get => xBA.float1; set { _x = value; xBA.float1 = value; } }
        public float Y { get => yBA.float1; set { _y = value; yBA.float1 = value; } }
        public float Z { get => zBA.float1; set { _z = value; zBA.float1 = value; } }
        public float W { get => wBA.float1; set { _w = value; wBA.float1 = value; } }

        public int xint { get => xBA.int1; set => xBA.int1 = value; }
        public int yint { get => yBA.int1; set => yBA.int1 = value; }
        public int zint { get => zBA.int1; set => zBA.int1 = value; }
        public int wint { get => wBA.int1; set => wBA.int1 = value; }

        public uint xuint { get => xBA.uint1; set => xBA.uint1 = value; }
        public uint yuint { get => yBA.uint1; set => yBA.uint1 = value; }
        public uint zuint { get => zBA.uint1; set => zBA.uint1 = value; }
        public uint wuint { get => wBA.uint1; set => wBA.uint1 = value; }

        public Vector4(double x, double y, double z, double w) { _x = (float)x; _y = (float)y; _z = (float)z; _w = (float)w; }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return W;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices must run from 0 to 3!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: W = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices must run from 0 to 3!");
                }
            }
        }

        public Vector3 ToVector3()
        {
            var r = new Vector3();
            if (_w == 0) { r.x = _x; r.y = _y; r.z = _z; }
            else { r.x = _x / _w; r.y = _y / _w; r.z = _z / _w; }
            return r;
        }

        public void WriteVector4()
        {
            Log("=============================================");
            Log($"x:{_x:F7}  y:{_y:F7}  z:{_z:F7} w:{_w:F7}");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Z.GetHashCode();
                hash = hash * 23 + W.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj) => obj == null ? false : obj is Vector4 ? this == (Vector4)obj : false;
        public override string ToString() => $"{X},{Y},{Z},{W}";
        public static implicit operator Vector4(Vector3 vec3) => new Vector4(vec3.x, vec3.y, vec3.z, 0);
        public static bool operator ==(Vector4 left, Vector4 right) => right is null ? left is null : left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
        public static bool operator !=(Vector4 left, Vector4 right) => !(left == right);
        public bool IsZero(float epsilon = 0) => Math.Abs(x) <= epsilon && Math.Abs(y) <= epsilon && Math.Abs(z) <= epsilon && Math.Abs(w) <= epsilon;
        public float Dot(Vector4 v) => X * v.X + Y * v.Y + Z * v.Z + W * v.W;
    }
}
