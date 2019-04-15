namespace UnityEngine
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2 : IEquatable<Vector2>
    {
        public float x;
        public float y;
        private static readonly Vector2 zeroVector;
        private static readonly Vector2 oneVector;
        private static readonly Vector2 upVector;
        private static readonly Vector2 downVector;
        private static readonly Vector2 leftVector;
        private static readonly Vector2 rightVector;
        private static readonly Vector2 positiveInfinityVector;
        private static readonly Vector2 negativeInfinityVector;
        public const float kEpsilon = 1E-05f;
        public const float kEpsilonNormalSqrt = 1E-15f;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float this[int index]
        {
            get
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
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
        public void Set(float newX, float newY)
        {
            this.x = newX;
            this.y = newY;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector2(a.x + ((b.x - a.x) * t), a.y + ((b.y - a.y) * t));
        }

        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(a.x + ((b.x - a.x) * t), a.y + ((b.y - a.y) * t));
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = (num * num) + (num2 * num2);
            if ((num3 == 0f) || ((maxDistanceDelta >= 0f) && (num3 <= (maxDistanceDelta * maxDistanceDelta))))
            {
                return target;
            }
            float num4 = (float)Math.Sqrt((double)num3);
            return new Vector2(current.x + ((num / num4) * maxDistanceDelta), current.y + ((num2 / num4) * maxDistanceDelta));
        }

        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public void Scale(Vector2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        public void Normalize()
        {
            float magnitude = this.magnitude;
            if (magnitude > 1E-05f)
            {
                this = (Vector2)(this / magnitude);
            }
            else
            {
                this = zero;
            }
        }

        public Vector2 normalized
        {
            get
            {
                Vector2 vector = new Vector2(this.x, this.y);
                vector.Normalize();
                return vector;
            }
        }
        public override string ToString()
        {
            object[] args = new object[] { this.x, this.y };
            return UnityString.Format("({0:F1}, {1:F1})", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[] { this.x.ToString(format, CultureInfo.InvariantCulture.NumberFormat), this.y.ToString(format, CultureInfo.InvariantCulture.NumberFormat) };
            return UnityString.Format("({0}, {1})", args);
        }

        public override int GetHashCode()
        {
            return (this.x.GetHashCode() ^ (this.y.GetHashCode() << 2));
        }

        public override bool Equals(object other)
        {
            return ((other is Vector2) && this.Equals((Vector2)other));
        }

        public bool Equals(Vector2 other)
        {
            return ((this.x == other.x) && (this.y == other.y));
        }

        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            float num = -2f * Dot(inNormal, inDirection);
            return new Vector2((num * inNormal.x) + inDirection.x, (num * inNormal.y) + inDirection.y);
        }

        public static Vector2 Perpendicular(Vector2 inDirection)
        {
            return new Vector2(-inDirection.y, inDirection.x);
        }

        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return ((lhs.x * rhs.x) + (lhs.y * rhs.y));
        }

        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt((double)((this.x * this.x) + (this.y * this.y)));
            }
        }
        public float sqrMagnitude
        {
            get
            {
                return ((this.x * this.x) + (this.y * this.y));
            }
        }
        public static float Angle(Vector2 from, Vector2 to)
        {
            float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
            if (num < 1E-15f)
            {
                return 0f;
            }
            return (((float)Math.Acos((double)Mathf.Clamp((float)(Dot(from, to) / num), (float)-1f, (float)1f))) * 57.29578f);
        }

        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            float num = Angle(from, to);
            float num2 = Mathf.Sign((from.x * to.y) - (from.y * to.x));
            return (num * num2);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            return (float)Math.Sqrt((double)((num * num) + (num2 * num2)));
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            float sqrMagnitude = vector.sqrMagnitude;
            if (sqrMagnitude > (maxLength * maxLength))
            {
                float num2 = (float)Math.Sqrt((double)sqrMagnitude);
                float num3 = vector.x / num2;
                float num4 = vector.y / num2;
                return new Vector2(num3 * maxLength, num4 * maxLength);
            }
            return vector;
        }

        public static float SqrMagnitude(Vector2 a)
        {
            return ((a.x * a.x) + (a.y * a.y));
        }

        public float SqrMagnitude()
        {
            return ((this.x * this.x) + (this.y * this.y));
        }

        public static Vector2 Min(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
        }

        public static Vector2 Max(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
        }

        //public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
        //{
        //    float deltaTime = Time.deltaTime;
        //    return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        //}

        //public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
        //{
        //    float deltaTime = Time.deltaTime;
        //    float positiveInfinity = float.PositiveInfinity;
        //    return SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
        //}

        //public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
        //{
        //    smoothTime = Mathf.Max(0.0001f, smoothTime);
        //    float num = 2f / smoothTime;
        //    float num2 = num * deltaTime;
        //    float num3 = 1f / (((1f + num2) + ((0.48f * num2) * num2)) + (((0.235f * num2) * num2) * num2));
        //    float num4 = current.x - target.x;
        //    float num5 = current.y - target.y;
        //    Vector2 vector = target;
        //    float num6 = maxSpeed * smoothTime;
        //    float num7 = num6 * num6;
        //    float num8 = (num4 * num4) + (num5 * num5);
        //    if (num8 > num7)
        //    {
        //        float num9 = (float) Math.Sqrt((double) num8);
        //        num4 = (num4 / num9) * num6;
        //        num5 = (num5 / num9) * num6;
        //    }
        //    target.x = current.x - num4;
        //    target.y = current.y - num5;
        //    float num10 = (currentVelocity.x + (num * num4)) * deltaTime;
        //    float num11 = (currentVelocity.y + (num * num5)) * deltaTime;
        //    currentVelocity.x = (currentVelocity.x - (num * num10)) * num3;
        //    currentVelocity.y = (currentVelocity.y - (num * num11)) * num3;
        //    float x = target.x + ((num4 + num10) * num3);
        //    float y = target.y + ((num5 + num11) * num3);
        //    float num14 = vector.x - current.x;
        //    float num15 = vector.y - current.y;
        //    float num16 = x - vector.x;
        //    float num17 = y - vector.y;
        //    if (((num14 * num16) + (num15 * num17)) > 0f)
        //    {
        //        x = vector.x;
        //        y = vector.y;
        //        currentVelocity.x = (x - vector.x) / deltaTime;
        //        currentVelocity.y = (y - vector.y) / deltaTime;
        //    }
        //    return new Vector2(x, y);
        //}

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            return (((num * num) + (num2 * num2)) < 9.999999E-11f);
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0f);
        }

        public static Vector2 zero
        {
            get
            {
                return zeroVector;
            }
        }
        public static Vector2 one
        {
            get
            {
                return oneVector;
            }
        }
        public static Vector2 up
        {
            get
            {
                return upVector;
            }
        }
        public static Vector2 down
        {
            get
            {
                return downVector;
            }
        }
        public static Vector2 left
        {
            get
            {
                return leftVector;
            }
        }
        public static Vector2 right
        {
            get
            {
                return rightVector;
            }
        }
        public static Vector2 positiveInfinity
        {
            get
            {
                return positiveInfinityVector;
            }
        }
        public static Vector2 negativeInfinity
        {
            get
            {
                return negativeInfinityVector;
            }
        }
        static Vector2()
        {
            zeroVector = new Vector2(0f, 0f);
            oneVector = new Vector2(1f, 1f);
            upVector = new Vector2(0f, 1f);
            downVector = new Vector2(0f, -1f);
            leftVector = new Vector2(-1f, 0f);
            rightVector = new Vector2(1f, 0f);
            positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
        }
    }
}

