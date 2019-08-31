using System;

namespace Game.Core
{
    public interface IPoint2D
    {
        int X { get; }
        int Y { get; }
    }

    [Parsable]
    public struct Point2D : IPoint2D, IComparable, IComparable<Point2D>
    {
        public static readonly Point2D Zero = new Point2D(0, 0);

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point2D(IPoint2D p)
            : this(p.X, p.Y) { }

        [CommandProperty(AccessLevel.Counselor)]
        public int X { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public int Y { get; set; }

        public override string ToString() => $"({X}, {Y})";

        public static Point2D Parse(string value)
        {
            var start = value.IndexOf('(');
            var end = value.IndexOf(',', start + 1);
            var param1 = value.Substring(start + 1, end - (start + 1)).Trim();
            start = end;
            end = value.IndexOf(')', start + 1);
            var param2 = value.Substring(start + 1, end - (start + 1)).Trim();
            return new Point2D(Convert.ToInt32(param1), Convert.ToInt32(param2));
        }

        public int CompareTo(Point2D other)
        {
            var v = X.CompareTo(other.X);
            if (v == 0)
                v = Y.CompareTo(other.Y);
            return v;
        }

        public int CompareTo(object other)
        {
            if (other is Point2D) return CompareTo((Point2D)other);
            else if (other == null) return -1;
            throw new ArgumentException();
        }

        public override bool Equals(object o)
        {
            if (o == null || !(o is IPoint2D)) return false;
            var p = (IPoint2D)o;
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode() => X ^ Y;

        public static bool operator ==(Point2D l, Point2D r) => l.X == r.X && l.Y == r.Y;
        public static bool operator !=(Point2D l, Point2D r) => l.X != r.X || l.Y != r.Y;
        public static bool operator ==(Point2D l, IPoint2D r) => ReferenceEquals(r, null) ? false : l.X == r.X && l.Y == r.Y;
        public static bool operator !=(Point2D l, IPoint2D r) => ReferenceEquals(r, null) ? false : l.X != r.X || l.Y != r.Y;
        public static bool operator >(Point2D l, Point2D r) => l.X > r.X && l.Y > r.Y;
        public static bool operator >(Point2D l, Point3D r) => l.X > r.X && l.Y > r.Y;
        public static bool operator >(Point2D l, IPoint2D r) => ReferenceEquals(r, null) ? false : l.X > r.X && l.Y > r.Y;
        public static bool operator <(Point2D l, Point2D r) => l.X < r.X && l.Y < r.Y;
        public static bool operator <(Point2D l, Point3D r) => l.X < r.X && l.Y < r.Y;
        public static bool operator <(Point2D l, IPoint2D r) => ReferenceEquals(r, null) ? false : l.X < r.X && l.Y < r.Y;
        public static bool operator >=(Point2D l, Point2D r) => l.X >= r.X && l.Y >= r.Y;
        public static bool operator >=(Point2D l, Point3D r) => l.X >= r.X && l.Y >= r.Y;
        public static bool operator >=(Point2D l, IPoint2D r) => ReferenceEquals(r, null) ? false : l.X >= r.X && l.Y >= r.Y;
        public static bool operator <=(Point2D l, Point2D r) => l.X <= r.X && l.Y <= r.Y;
        public static bool operator <=(Point2D l, Point3D r) => l.X <= r.X && l.Y <= r.Y;
        public static bool operator <=(Point2D l, IPoint2D r) => ReferenceEquals(r, null) ? false : l.X <= r.X && l.Y <= r.Y;
    }

    public interface IPoint3D : IPoint2D
    {
        int Z { get; }
    }

    [Parsable]
    public struct Point3D : IPoint3D, IComparable, IComparable<Point3D>
    {
        public static readonly Point3D Zero = new Point3D(0, 0, 0);

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(IPoint3D p)
            : this(p.X, p.Y, p.Z) { }
        public Point3D(IPoint2D p, int z)
            : this(p.X, p.Y, z) { }

        [CommandProperty(AccessLevel.Counselor)]
        public int X { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public int Y { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public int Z { get; set; }

        public override string ToString() => $"({X}, {Y}, {Z})";

        public override bool Equals(object o)
        {
            if (o == null || !(o is IPoint3D))
                return false;
            var p = (IPoint3D)o;
            return X == p.X && Y == p.Y && Z == p.Z;
        }

        public override int GetHashCode() => X ^ Y ^ Z;

        public static Point3D Parse(string value)
        {
            var start = value.IndexOf('(');
            var end = value.IndexOf(',', start + 1);
            var param1 = value.Substring(start + 1, end - (start + 1)).Trim();
            start = end;
            end = value.IndexOf(',', start + 1);
            var param2 = value.Substring(start + 1, end - (start + 1)).Trim();
            start = end;
            end = value.IndexOf(')', start + 1);
            var param3 = value.Substring(start + 1, end - (start + 1)).Trim();
            return new Point3D(Convert.ToInt32(param1), Convert.ToInt32(param2), Convert.ToInt32(param3));
        }

        public static bool operator ==(Point3D l, Point3D r) => l.X == r.X && l.Y == r.Y && l.Z == r.Z;
        public static bool operator !=(Point3D l, Point3D r) => l.X != r.X || l.Y != r.Y || l.Z != r.Z;
        public static bool operator ==(Point3D l, IPoint3D r) => ReferenceEquals(r, null) ? false : l.X == r.X && l.Y == r.Y && l.Z == r.Z;
        public static bool operator !=(Point3D l, IPoint3D r) => ReferenceEquals(r, null) ? false : l.X != r.X || l.Y != r.Y || l.Z != r.Z;

        public int CompareTo(Point3D other)
        {
            var v = X.CompareTo(other.X);
            if (v == 0)
            {
                v = Y.CompareTo(other.Y);
                if (v == 0)
                    v = Z.CompareTo(other.Z);
            }
            return v;
        }

        public int CompareTo(object other)
        {
            if (other is Point3D) return CompareTo((Point3D)other);
            else if (other == null) return -1;
            throw new ArgumentException();
        }
    }

    [NoSort, Parsable, PropertyObject]
    public struct Rectangle2D
    {
        Point2D _Start;
        Point2D _End;

        public Rectangle2D(IPoint2D start, IPoint2D end)
        {
            _Start = new Point2D(start);
            _End = new Point2D(end);
        }

        public Rectangle2D(int x, int y, int width, int height)
        {
            _Start = new Point2D(x, y);
            _End = new Point2D(x + width, y + height);
        }

        public void Set(int x, int y, int width, int height)
        {
            _Start = new Point2D(x, y);
            _End = new Point2D(x + width, y + height);
        }

        public static Rectangle2D Parse(string value)
        {
            var start = value.IndexOf('(');
            var end = value.IndexOf(',', start + 1);
            var param1 = value.Substring(start + 1, end - (start + 1)).Trim();
            start = end;
            end = value.IndexOf(',', start + 1);
            var param2 = value.Substring(start + 1, end - (start + 1)).Trim();
            start = end;
            end = value.IndexOf(',', start + 1);
            var param3 = value.Substring(start + 1, end - (start + 1)).Trim();
            start = end;
            end = value.IndexOf(')', start + 1);
            var param4 = value.Substring(start + 1, end - (start + 1)).Trim();
            return new Rectangle2D(Convert.ToInt32(param1), Convert.ToInt32(param2), Convert.ToInt32(param3), Convert.ToInt32(param4));
        }

        [CommandProperty(AccessLevel.Counselor)]
        public Point2D Start
        {
            get => _Start;
            set => _Start = value;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public Point2D End
        {
            get => _End;
            set => _End = value;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public int X
        {
            get => _Start.X;
            set => _Start.X = value;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public int Y
        {
            get => _Start.Y;
            set => _Start.Y = value;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public int Width
        {
            get => _End.X - _Start.X;
            set => _End.X = _Start.X + value;
        }

        [CommandProperty(AccessLevel.Counselor)]
        public int Height
        {
            get => _End.Y - _Start.Y;
            set => _End.Y = _Start.Y + value;
        }

        public void MakeHold(Rectangle2D r)
        {
            if (r._Start.X < _Start.X) _Start.X = r._Start.X;
            if (r._Start.Y < _Start.Y) _Start.Y = r._Start.Y;
            if (r._End.X > _End.X) _End.X = r._End.X;
            if (r._End.Y > _End.Y) _End.Y = r._End.Y;
        }

        public bool Contains(Point3D p) => _Start.X <= p.X && _Start.Y <= p.Y && _End.X > p.X && _End.Y > p.Y;
        public bool Contains(Point2D p) => _Start.X <= p.X && _Start.Y <= p.Y && _End.X > p.X && _End.Y > p.Y;
        public bool Contains(IPoint2D p) => _Start <= p && _End > p;

        public override string ToString() => $"({X}, {Y})+({Width}, {Height})";
    }

    [NoSort, PropertyObject]
    public struct Rectangle3D
    {
        public Rectangle3D(Point3D start, Point3D end)
        {
            Start = start;
            End = end;
        }

        public Rectangle3D(int x, int y, int z, int width, int height, int depth)
        {
            Start = new Point3D(x, y, z);
            End = new Point3D(x + width, y + height, z + depth);
        }

        [CommandProperty(AccessLevel.Counselor)]
        public Point3D Start { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public Point3D End { get; set; }

        [CommandProperty(AccessLevel.Counselor)]
        public int Width => End.X - Start.X;

        [CommandProperty(AccessLevel.Counselor)]
        public int Height => End.Y - Start.Y;

        [CommandProperty(AccessLevel.Counselor)]
        public int Depth => End.Z - Start.Z;

        public bool Contains(Point3D p) =>
            p.X >= Start.X && p.X < End.X &&
            p.Y >= Start.Y && p.Y < End.Y &&
            p.Z >= Start.Z && p.Z < End.Z;

        public bool Contains(IPoint3D p) =>
            p.X >= Start.X && p.X < End.X &&
            p.Y >= Start.Y && p.Y < End.Y &&
            p.Z >= Start.Z && p.Z < End.Z;
    }
}