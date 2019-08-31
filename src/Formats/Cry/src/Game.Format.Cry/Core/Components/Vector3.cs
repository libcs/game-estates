﻿using System;

namespace Game.Format.Cry.Core.Components
{
    public class Vector3
    {
        float _x;
        float _y;
        float _z;

        ByteArray xBA = new ByteArray();
        ByteArray yBA = new ByteArray();
        ByteArray zBA = new ByteArray();

        public float x { get => xBA.float1; set { _x = value; xBA.float1 = value; } }
        public float y { get => yBA.float1; set { _y = value; yBA.float1 = value; } }
        public float z { get => zBA.float1; set { _z = value; zBA.float1 = value; } }

        public float X { get => xBA.float1; set { _x = value; xBA.float1 = value; } }
        public float Y { get => yBA.float1; set { _y = value; yBA.float1 = value; } }
        public float Z { get => zBA.float1; set { _z = value; zBA.float1 = value; } }

        public int xint { get => xBA.int1; set => xBA.int1 = value; }
        public int yint { get => yBA.int1; set => yBA.int1 = value; }
        public int zint { get => zBA.int1; set => zBA.int1 = value; }

        public uint xuint { get => xBA.uint1; set => xBA.uint1 = value; }
        public uint yuint { get => yBA.uint1; set => yBA.uint1 = value; }
        public uint zuint { get => zBA.uint1; set => zBA.uint1 = value; }

        public Vector3() { }
        public Vector3(double x, double y, double z) { _x = (float)x; _y = (float)y; _z = (float)z; }
        public Vector3(Vector3 vector) { _x = vector.x; _y = vector.y; _z = vector.z; }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices must run from 0 to 2!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index), "Indices must run from 0 to 2!");
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
                hash = hash * 23 + Z.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj) => obj == null ? false : obj is Vector3 ? this == (Vector3)obj : false;
        public override string ToString() => $"{X},{Y},{Z}";
        public static implicit operator Vector3(Vector4 vec4) => new Vector3(vec4.x, vec4.y, vec4.z);
        public static bool operator ==(Vector3 left, Vector3 right) => right is null ? left is null : left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        public static bool operator !=(Vector3 left, Vector3 right) => !(left == right);
        public bool IsZero(float epsilon = 0) => Math.Abs(x) <= epsilon && Math.Abs(y) <= epsilon && Math.Abs(z) <= epsilon;
        public float Dot(Vector3 v) => X * v.X + Y * v.Y + Z * v.Z;
    }
}
