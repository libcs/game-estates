namespace UnityEngine
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2Int : IEquatable<Vector2Int>
    {
        private int m_X;
        private int m_Y;
        private static readonly Vector2Int s_Zero;
        private static readonly Vector2Int s_One;
        private static readonly Vector2Int s_Up;
        private static readonly Vector2Int s_Down;
        private static readonly Vector2Int s_Left;
        private static readonly Vector2Int s_Right;
        public Vector2Int(int x, int y)
        {
            this.m_X = x;
            this.m_Y = y;
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
        public void Set(int x, int y)
        {
            this.m_X = x;
            this.m_Y = y;
        }

        public int this[int index]
        {
            get
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
                    }
                }
                else
                {
                    return this.x;
                }
                return this.y;
            }
            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
                    }
                }
                else
                {
                    this.x = value;
                    return;
                }
                this.y = value;
            }
        }
        public float magnitude
        {
            get
            {
                return Mathf.Sqrt((float)((this.x * this.x) + (this.y * this.y)));
            }
        }
        public int sqrMagnitude
        {
            get
            {
                return ((this.x * this.x) + (this.y * this.y));
            }
        }
        public static float Distance(Vector2Int a, Vector2Int b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            return (float)Math.Sqrt((double)((num * num) + (num2 * num2)));
        }

        public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
        }

        public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
        {
            return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        }

        public static Vector2Int Scale(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x * b.x, a.y * b.y);
        }

        public void Scale(Vector2Int scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        public void Clamp(Vector2Int min, Vector2Int max)
        {
            this.x = Math.Max(min.x, this.x);
            this.x = Math.Min(max.x, this.x);
            this.y = Math.Max(min.y, this.y);
            this.y = Math.Min(max.y, this.y);
        }

        public static implicit operator Vector2(Vector2Int v)
        {
            return new Vector2((float)v.x, (float)v.y);
        }

        public static explicit operator Vector3Int(Vector2Int v)
        {
            return new Vector3Int(v.x, v.y, 0);
        }

        public static Vector2Int FloorToInt(Vector2 v)
        {
            return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
        }

        public static Vector2Int CeilToInt(Vector2 v)
        {
            return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
        }

        public static Vector2Int RoundToInt(Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
        }

        public static Vector2Int operator +(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x + b.x, a.y + b.y);
        }

        public static Vector2Int operator -(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x - b.x, a.y - b.y);
        }

        public static Vector2Int operator *(Vector2Int a, Vector2Int b)
        {
            return new Vector2Int(a.x * b.x, a.y * b.y);
        }

        public static Vector2Int operator *(Vector2Int a, int b)
        {
            return new Vector2Int(a.x * b, a.y * b);
        }

        public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
        {
            return ((lhs.x == rhs.x) && (lhs.y == rhs.y));
        }

        public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object other)
        {
            return ((other is Vector2Int) && this.Equals((Vector2Int)other));
        }

        public bool Equals(Vector2Int other)
        {
            return (this.x.Equals(other.x) && this.y.Equals(other.y));
        }

        public override int GetHashCode()
        {
            return (this.x.GetHashCode() ^ (this.y.GetHashCode() << 2));
        }

        public override string ToString()
        {
            object[] args = new object[] { this.x, this.y };
            return UnityString.Format("({0}, {1})", args);
        }

        public static Vector2Int zero
        {
            get
            {
                return s_Zero;
            }
        }
        public static Vector2Int one
        {
            get
            {
                return s_One;
            }
        }
        public static Vector2Int up
        {
            get
            {
                return s_Up;
            }
        }
        public static Vector2Int down
        {
            get
            {
                return s_Down;
            }
        }
        public static Vector2Int left
        {
            get
            {
                return s_Left;
            }
        }
        public static Vector2Int right
        {
            get
            {
                return s_Right;
            }
        }
        static Vector2Int()
        {
            s_Zero = new Vector2Int(0, 0);
            s_One = new Vector2Int(1, 1);
            s_Up = new Vector2Int(0, 1);
            s_Down = new Vector2Int(0, -1);
            s_Left = new Vector2Int(-1, 0);
            s_Right = new Vector2Int(1, 0);
        }
    }
}

