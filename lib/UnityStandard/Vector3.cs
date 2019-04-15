namespace UnityEngine
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3 : IEquatable<Vector3>
    {
        public const float kEpsilon = 1E-05f;
        public const float kEpsilonNormalSqrt = 1E-15f;
        public float x;
        public float y;
        public float z;
        private static readonly Vector3 zeroVector;
        private static readonly Vector3 oneVector;
        private static readonly Vector3 upVector;
        private static readonly Vector3 downVector;
        private static readonly Vector3 leftVector;
        private static readonly Vector3 rightVector;
        private static readonly Vector3 forwardVector;
        private static readonly Vector3 backVector;
        private static readonly Vector3 positiveInfinityVector;
        private static readonly Vector3 negativeInfinityVector;
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
        }

        //public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
        //{
        //    Vector3 vector;
        //    Slerp_Injected(ref a, ref b, t, out vector);
        //    return vector;
        //}

        //public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
        //{
        //    Vector3 vector;
        //    SlerpUnclamped_Injected(ref a, ref b, t, out vector);
        //    return vector;
        //}

        //[MethodImpl(MethodImplOptions.InternalCall), FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
        //private static extern void OrthoNormalize2(ref Vector3 a, ref Vector3 b);
        //public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
        //{
        //    OrthoNormalize2(ref normal, ref tangent);
        //}

        //[MethodImpl(MethodImplOptions.InternalCall), FreeFunction("VectorScripting::OrthoNormalize", IsThreadSafe = true)]
        //private static extern void OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);
        //public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
        //{
        //    OrthoNormalize3(ref normal, ref tangent, ref binormal);
        //}

        //public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
        //{
        //    Vector3 vector;
        //    RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out vector);
        //    return vector;
        //}

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3(a.x + ((b.x - a.x) * t), a.y + ((b.y - a.y) * t), a.z + ((b.z - a.z) * t));
        }

        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.x + ((b.x - a.x) * t), a.y + ((b.y - a.y) * t), a.z + ((b.z - a.z) * t));
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = target.z - current.z;
            float num4 = ((num * num) + (num2 * num2)) + (num3 * num3);
            if ((num4 == 0f) || (num4 <= (maxDistanceDelta * maxDistanceDelta)))
            {
                return target;
            }
            float num5 = (float)Math.Sqrt((double)num4);
            return new Vector3(current.x + ((num / num5) * maxDistanceDelta), current.y + ((num2 / num5) * maxDistanceDelta), current.z + ((num3 / num5) * maxDistanceDelta));
        }

        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
        //{
        //    float deltaTime = Time.deltaTime;
        //    return SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
        //}

        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
        //{
        //    float deltaTime = Time.deltaTime;
        //    float positiveInfinity = float.PositiveInfinity;
        //    return SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
        //}

        //public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
        //{
        //    float x = 0f;
        //    float y = 0f;
        //    float z = 0f;
        //    smoothTime = Mathf.Max(0.0001f, smoothTime);
        //    float num4 = 2f / smoothTime;
        //    float num5 = num4 * deltaTime;
        //    float num6 = 1f / (((1f + num5) + ((0.48f * num5) * num5)) + (((0.235f * num5) * num5) * num5));
        //    float num7 = current.x - target.x;
        //    float num8 = current.y - target.y;
        //    float num9 = current.z - target.z;
        //    Vector3 vector = target;
        //    float num10 = maxSpeed * smoothTime;
        //    float num11 = num10 * num10;
        //    float num12 = ((num7 * num7) + (num8 * num8)) + (num9 * num9);
        //    if (num12 > num11)
        //    {
        //        float num13 = (float)Math.Sqrt((double)num12);
        //        num7 = (num7 / num13) * num10;
        //        num8 = (num8 / num13) * num10;
        //        num9 = (num9 / num13) * num10;
        //    }
        //    target.x = current.x - num7;
        //    target.y = current.y - num8;
        //    target.z = current.z - num9;
        //    float num14 = (currentVelocity.x + (num4 * num7)) * deltaTime;
        //    float num15 = (currentVelocity.y + (num4 * num8)) * deltaTime;
        //    float num16 = (currentVelocity.z + (num4 * num9)) * deltaTime;
        //    currentVelocity.x = (currentVelocity.x - (num4 * num14)) * num6;
        //    currentVelocity.y = (currentVelocity.y - (num4 * num15)) * num6;
        //    currentVelocity.z = (currentVelocity.z - (num4 * num16)) * num6;
        //    x = target.x + ((num7 + num14) * num6);
        //    y = target.y + ((num8 + num15) * num6);
        //    z = target.z + ((num9 + num16) * num6);
        //    float num17 = vector.x - current.x;
        //    float num18 = vector.y - current.y;
        //    float num19 = vector.z - current.z;
        //    float num20 = x - vector.x;
        //    float num21 = y - vector.y;
        //    float num22 = z - vector.z;
        //    if ((((num17 * num20) + (num18 * num21)) + (num19 * num22)) > 0f)
        //    {
        //        x = vector.x;
        //        y = vector.y;
        //        z = vector.z;
        //        currentVelocity.x = (x - vector.x) / deltaTime;
        //        currentVelocity.y = (y - vector.y) / deltaTime;
        //        currentVelocity.z = (z - vector.z) / deltaTime;
        //    }
        //    return new Vector3(x, y, z);
        //}

        public float this[int index]
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
                throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }
        public void Set(float newX, float newY, float newZ)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
        }

        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void Scale(Vector3 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3((lhs.y * rhs.z) - (lhs.z * rhs.y), (lhs.z * rhs.x) - (lhs.x * rhs.z), (lhs.x * rhs.y) - (lhs.y * rhs.x));
        }

        public override int GetHashCode()
        {
            return ((this.x.GetHashCode() ^ (this.y.GetHashCode() << 2)) ^ (this.z.GetHashCode() >> 2));
        }

        public override bool Equals(object other)
        {
            return ((other is Vector3) && this.Equals((Vector3)other));
        }

        public bool Equals(Vector3 other)
        {
            return (((this.x == other.x) && (this.y == other.y)) && (this.z == other.z));
        }

        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            float num = -2f * Dot(inNormal, inDirection);
            return new Vector3((num * inNormal.x) + inDirection.x, (num * inNormal.y) + inDirection.y, (num * inNormal.z) + inDirection.z);
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float num = Magnitude(value);
            if (num > 1E-05f)
            {
                return (Vector3)(value / num);
            }
            return zero;
        }

        public void Normalize()
        {
            float num = Magnitude(this);
            if (num > 1E-05f)
            {
                this = (Vector3)(this / num);
            }
            else
            {
                this = zero;
            }
        }

        public Vector3 normalized
        {
            get
            {
                return Normalize(this);
            }
        }
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (((lhs.x * rhs.x) + (lhs.y * rhs.y)) + (lhs.z * rhs.z));
        }

        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            float num = Dot(onNormal, onNormal);
            if (num < Mathf.Epsilon)
            {
                return zero;
            }
            float num2 = Dot(vector, onNormal);
            return new Vector3((onNormal.x * num2) / num, (onNormal.y * num2) / num, (onNormal.z * num2) / num);
        }

        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            float num = Dot(planeNormal, planeNormal);
            if (num < Mathf.Epsilon)
            {
                return vector;
            }
            float num2 = Dot(vector, planeNormal);
            return new Vector3(vector.x - ((planeNormal.x * num2) / num), vector.y - ((planeNormal.y * num2) / num), vector.z - ((planeNormal.z * num2) / num));
        }

        public static float Angle(Vector3 from, Vector3 to)
        {
            float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
            if (num < 1E-15f)
            {
                return 0f;
            }
            return (((float)Math.Acos((double)Mathf.Clamp((float)(Dot(from, to) / num), (float)-1f, (float)1f))) * 57.29578f);
        }

        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            float num = Angle(from, to);
            float num2 = (from.y * to.z) - (from.z * to.y);
            float num3 = (from.z * to.x) - (from.x * to.z);
            float num4 = (from.x * to.y) - (from.y * to.x);
            float num5 = Mathf.Sign(((axis.x * num2) + (axis.y * num3)) + (axis.z * num4));
            return (num * num5);
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            float num3 = a.z - b.z;
            return (float)Math.Sqrt((double)(((num * num) + (num2 * num2)) + (num3 * num3)));
        }

        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            float sqrMagnitude = vector.sqrMagnitude;
            if (sqrMagnitude > (maxLength * maxLength))
            {
                float num2 = (float)Math.Sqrt((double)sqrMagnitude);
                float num3 = vector.x / num2;
                float num4 = vector.y / num2;
                float num5 = vector.z / num2;
                return new Vector3(num3 * maxLength, num4 * maxLength, num5 * maxLength);
            }
            return vector;
        }

        public static float Magnitude(Vector3 vector)
        {
            return (float)Math.Sqrt((double)(((vector.x * vector.x) + (vector.y * vector.y)) + (vector.z * vector.z)));
        }

        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt((double)(((this.x * this.x) + (this.y * this.y)) + (this.z * this.z)));
            }
        }
        public static float SqrMagnitude(Vector3 vector)
        {
            return (((vector.x * vector.x) + (vector.y * vector.y)) + (vector.z * vector.z));
        }

        public float sqrMagnitude
        {
            get
            {
                return (((this.x * this.x) + (this.y * this.y)) + (this.z * this.z));
            }
        }
        public static Vector3 Min(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        public static Vector3 Max(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }

        public static Vector3 zero
        {
            get
            {
                return zeroVector;
            }
        }
        public static Vector3 one
        {
            get
            {
                return oneVector;
            }
        }
        public static Vector3 forward
        {
            get
            {
                return forwardVector;
            }
        }
        public static Vector3 back
        {
            get
            {
                return backVector;
            }
        }
        public static Vector3 up
        {
            get
            {
                return upVector;
            }
        }
        public static Vector3 down
        {
            get
            {
                return downVector;
            }
        }
        public static Vector3 left
        {
            get
            {
                return leftVector;
            }
        }
        public static Vector3 right
        {
            get
            {
                return rightVector;
            }
        }
        public static Vector3 positiveInfinity
        {
            get
            {
                return positiveInfinityVector;
            }
        }
        public static Vector3 negativeInfinity
        {
            get
            {
                return negativeInfinityVector;
            }
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            float num4 = ((num * num) + (num2 * num2)) + (num3 * num3);
            return (num4 < 9.999999E-11f);
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            object[] args = new object[] { this.x, this.y, this.z };
            return UnityString.Format("({0:F1}, {1:F1}, {2:F1})", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[] { this.x.ToString(format, CultureInfo.InvariantCulture.NumberFormat), this.y.ToString(format, CultureInfo.InvariantCulture.NumberFormat), this.z.ToString(format, CultureInfo.InvariantCulture.NumberFormat) };
            return UnityString.Format("({0}, {1}, {2})", args);
        }

        [Obsolete("Use Vector3.forward instead.")]
        public static Vector3 fwd
        {
            get
            {
                return new Vector3(0f, 0f, 1f);
            }
        }
        [Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
        public static float AngleBetween(Vector3 from, Vector3 to)
        {
            return (float)Math.Acos((double)Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f));
        }

        [Obsolete("Use Vector3.ProjectOnPlane instead.")]
        public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
        {
            return ProjectOnPlane(fromThat, excludeThis);
        }

        static Vector3()
        {
            zeroVector = new Vector3(0f, 0f, 0f);
            oneVector = new Vector3(1f, 1f, 1f);
            upVector = new Vector3(0f, 1f, 0f);
            downVector = new Vector3(0f, -1f, 0f);
            leftVector = new Vector3(-1f, 0f, 0f);
            rightVector = new Vector3(1f, 0f, 0f);
            forwardVector = new Vector3(0f, 0f, 1f);
            backVector = new Vector3(0f, 0f, -1f);
            positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        }

        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Slerp_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void SlerpUnclamped_Injected(ref Vector3 a, ref Vector3 b, float t, out Vector3 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void RotateTowards_Injected(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta, out Vector3 ret);
    }
}

