namespace UnityEngine
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3Int : IEquatable<Vector3Int>
    {
        private int m_X;
        private int m_Y;
        private int m_Z;
        private static readonly Vector3Int s_Zero;
        private static readonly Vector3Int s_One;
        private static readonly Vector3Int s_Up;
        private static readonly Vector3Int s_Down;
        private static readonly Vector3Int s_Left;
        private static readonly Vector3Int s_Right;
        public Vector3Int(int x, int y, int z)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Z = z;
        }

        public int x
        {
            get
            {
                return this.m_X;
            }
            set
            {
                this.m_X = value;
            }
        }
        public int y
        {
            get
            {
                return this.m_Y;
            }
            set
            {
                this.m_Y = value;
            }
        }
        public int z
        {
            get
            {
                return this.m_Z;
            }
            set
            {
                this.m_Z = value;
            }
        }
        public void Set(int x, int y, int z)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Z = z;
        }

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;

                    case 1:
                        return this.y;

                    case 2:
                        return this.z;
                }
                object[] args = new object[] { index };
                throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", args));
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;

                    case 1:
                        this.y = value;
                        break;

                    case 2:
                        this.z = value;
                        break;

                    default:
                        {
                            object[] args = new object[] { index };
                            throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", args));
                        }
                }
            }
        }
        public float magnitude
        {
            get
            {
                return Mathf.Sqrt((float)(((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)));
            }
        }
        public int sqrMagnitude
        {
            get
            {
                return (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
            }
        }
        public static float Distance(Vector3Int a, Vector3Int b)
        {
            Vector3Int num = a - b;
            return num.magnitude;
        }

        public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
        {
            return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
        {
            return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }

        public static Vector3Int Scale(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void Scale(Vector3Int scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        public void Clamp(Vector3Int min, Vector3Int max)
        {
            this.x = Math.Max(min.x, this.x);
            this.x = Math.Min(max.x, this.x);
            this.y = Math.Max(min.y, this.y);
            this.y = Math.Min(max.y, this.y);
            this.z = Math.Max(min.z, this.z);
            this.z = Math.Min(max.z, this.z);
        }

        public static implicit operator Vector3(Vector3Int v)
        {
            return new Vector3((float)v.x, (float)v.y, (float)v.z);
        }

        public static explicit operator Vector2Int(Vector3Int v)
        {
            return new Vector2Int(v.x, v.y);
        }

        public static Vector3Int FloorToInt(Vector3 v)
        {
            return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
        }

        public static Vector3Int CeilToInt(Vector3 v)
        {
            return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
        }

        public static Vector3Int RoundToInt(Vector3 v)
        {
            return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        }

        public static Vector3Int operator +(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3Int operator -(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3Int operator *(Vector3Int a, Vector3Int b)
        {
            return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3Int operator *(Vector3Int a, int b)
        {
            return new Vector3Int(a.x * b, a.y * b, a.z * b);
        }

        public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
        {
            return (((lhs.x == rhs.x) && (lhs.y == rhs.y)) && (lhs.z == rhs.z));
        }

        public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object other)
        {
            return ((other is Vector3Int) && this.Equals((Vector3Int)other));
        }

        public bool Equals(Vector3Int other)
        {
            return (this == other);
        }

        public override int GetHashCode()
        {
            int hashCode = this.y.GetHashCode();
            int num3 = this.z.GetHashCode();
            return ((((this.x.GetHashCode() ^ (hashCode << 4)) ^ (hashCode >> 0x1c)) ^ (num3 >> 4)) ^ (num3 << 0x1c));
        }

        public override string ToString()
        {
            object[] args = new object[] { this.x, this.y, this.z };
            return UnityString.Format("({0}, {1}, {2})", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[] { this.x.ToString(format), this.y.ToString(format), this.z.ToString(format) };
            return UnityString.Format("({0}, {1}, {2})", args);
        }

        public static Vector3Int zero
        {
            get
            {
                return s_Zero;
            }
        }
        public static Vector3Int one
        {
            get
            {
                return s_One;
            }
        }
        public static Vector3Int up
        {
            get
            {
                return s_Up;
            }
        }
        public static Vector3Int down
        {
            get
            {
                return s_Down;
            }
        }
        public static Vector3Int left
        {
            get
            {
                return s_Left;
            }
        }
        public static Vector3Int right
        {
            get
            {
                return s_Right;
            }
        }
        static Vector3Int()
        {
            s_Zero = new Vector3Int(0, 0, 0);
            s_One = new Vector3Int(1, 1, 1);
            s_Up = new Vector3Int(0, 1, 0);
            s_Down = new Vector3Int(0, -1, 0);
            s_Left = new Vector3Int(-1, 0, 0);
            s_Right = new Vector3Int(1, 0, 0);
        }
    }
}

