namespace UnityEngine
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion : IEquatable<Quaternion>
    {
        public float x;
        public float y;
        public float z;
        public float w;
        private static readonly Quaternion identityQuaternion;
        public const float kEpsilon = 1E-06f;
        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        //public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        //{
        //    Quaternion quaternion;
        //    FromToRotation_Injected(ref fromDirection, ref toDirection, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion Inverse(Quaternion rotation)
        //{
        //    Quaternion quaternion;
        //    Inverse_Injected(ref rotation, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        //{
        //    Quaternion quaternion;
        //    Slerp_Injected(ref a, ref b, t, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
        //{
        //    Quaternion quaternion;
        //    SlerpUnclamped_Injected(ref a, ref b, t, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        //{
        //    Quaternion quaternion;
        //    Lerp_Injected(ref a, ref b, t, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
        //{
        //    Quaternion quaternion;
        //    LerpUnclamped_Injected(ref a, ref b, t, out quaternion);
        //    return quaternion;
        //}

        //private static Quaternion Internal_FromEulerRad(Vector3 euler)
        //{
        //    Quaternion quaternion;
        //    Internal_FromEulerRad_Injected(ref euler, out quaternion);
        //    return quaternion;
        //}

        //private static Vector3 Internal_ToEulerRad(Quaternion rotation)
        //{
        //    Vector3 vector;
        //    Internal_ToEulerRad_Injected(ref rotation, out vector);
        //    return vector;
        //}

        //private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
        //{
        //    Internal_ToAxisAngleRad_Injected(ref q, out axis, out angle);
        //}

        //public static Quaternion AngleAxis(float angle, Vector3 axis)
        //{
        //    Quaternion quaternion;
        //    AngleAxis_Injected(angle, ref axis, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion LookRotation(Vector3 forward, Vector3 upwards)
        //{
        //    Quaternion quaternion;
        //    LookRotation_Injected(ref forward, ref upwards, out quaternion);
        //    return quaternion;
        //}

        //public static Quaternion LookRotation(Vector3 forward)
        //{
        //    return LookRotation(forward, Vector3.up);
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

                    case 3:
                        return this.w;
                }
                throw new IndexOutOfRangeException("Invalid Quaternion index!");
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

                    case 3:
                        this.w = value;
                        break;

                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }
        public void Set(float newX, float newY, float newZ, float newW)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
            this.w = newW;
        }

        public static Quaternion identity
        {
            get
            {
                return identityQuaternion;
            }
        }
        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion((((lhs.w * rhs.x) + (lhs.x * rhs.w)) + (lhs.y * rhs.z)) - (lhs.z * rhs.y), (((lhs.w * rhs.y) + (lhs.y * rhs.w)) + (lhs.z * rhs.x)) - (lhs.x * rhs.z), (((lhs.w * rhs.z) + (lhs.z * rhs.w)) + (lhs.x * rhs.y)) - (lhs.y * rhs.x), (((lhs.w * rhs.w) - (lhs.x * rhs.x)) - (lhs.y * rhs.y)) - (lhs.z * rhs.z));
        }

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            Vector3 vector;
            float num = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            vector.x = (((1f - (num5 + num6)) * point.x) + ((num7 - num12) * point.y)) + ((num8 + num11) * point.z);
            vector.y = (((num7 + num12) * point.x) + ((1f - (num4 + num6)) * point.y)) + ((num9 - num10) * point.z);
            vector.z = (((num8 - num11) * point.x) + ((num9 + num10) * point.y)) + ((1f - (num4 + num5)) * point.z);
            return vector;
        }

        private static bool IsEqualUsingDot(float dot)
        {
            return (dot > 0.999999f);
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return IsEqualUsingDot(Dot(lhs, rhs));
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return !(lhs == rhs);
        }

        public static float Dot(Quaternion a, Quaternion b)
        {
            return ((((a.x * b.x) + (a.y * b.y)) + (a.z * b.z)) + (a.w * b.w));
        }

        //public void SetLookRotation(Vector3 view)
        //{
        //    Vector3 up = Vector3.up;
        //    this.SetLookRotation(view, up);
        //}

        //public void SetLookRotation(Vector3 view, Vector3 up)
        //{
        //    this = LookRotation(view, up);
        //}

        public static float Angle(Quaternion a, Quaternion b)
        {
            float dot = Dot(a, b);
            return (!IsEqualUsingDot(dot) ? ((Mathf.Acos(Mathf.Min(Mathf.Abs(dot), 1f)) * 2f) * 57.29578f) : 0f);
        }

        private static Vector3 Internal_MakePositive(Vector3 euler)
        {
            float num = -0.005729578f;
            float num2 = 360f + num;
            if (euler.x < num)
            {
                euler.x += 360f;
            }
            else if (euler.x > num2)
            {
                euler.x -= 360f;
            }
            if (euler.y < num)
            {
                euler.y += 360f;
            }
            else if (euler.y > num2)
            {
                euler.y -= 360f;
            }
            if (euler.z < num)
            {
                euler.z += 360f;
                return euler;
            }
            if (euler.z > num2)
            {
                euler.z -= 360f;
            }
            return euler;
        }

        //public Vector3 eulerAngles
        //{
        //    get
        //    {
        //        return Internal_MakePositive((Vector3) (Internal_ToEulerRad(this) * 57.29578f));
        //    }
        //    set
        //    {
        //        this = Internal_FromEulerRad((Vector3) (value * 0.01745329f));
        //    }
        //}
        //public static Quaternion Euler(float x, float y, float z)
        //{
        //    return Internal_FromEulerRad((Vector3) (new Vector3(x, y, z) * 0.01745329f));
        //}

        //public static Quaternion Euler(Vector3 euler)
        //{
        //    return Internal_FromEulerRad((Vector3) (euler * 0.01745329f));
        //}

        //public void ToAngleAxis(out float angle, out Vector3 axis)
        //{
        //    Internal_ToAxisAngleRad(this, out axis, out angle);
        //    angle *= 57.29578f;
        //}

        //public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
        //{
        //    this = FromToRotation(fromDirection, toDirection);
        //}

        //public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
        //{
        //    float num = Angle(from, to);
        //    if (num == 0f)
        //    {
        //        return to;
        //    }
        //    return SlerpUnclamped(from, to, Mathf.Min((float) 1f, (float) (maxDegreesDelta / num)));
        //}

        public static Quaternion Normalize(Quaternion q)
        {
            float num = Mathf.Sqrt(Dot(q, q));
            if (num < Mathf.Epsilon)
            {
                return identity;
            }
            return new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        public Quaternion normalized
        {
            get
            {
                return Normalize(this);
            }
        }
        public override int GetHashCode()
        {
            return (((this.x.GetHashCode() ^ (this.y.GetHashCode() << 2)) ^ (this.z.GetHashCode() >> 2)) ^ (this.w.GetHashCode() >> 1));
        }

        public override bool Equals(object other)
        {
            return ((other is Quaternion) && this.Equals((Quaternion)other));
        }

        public bool Equals(Quaternion other)
        {
            return (((this.x.Equals(other.x) && this.y.Equals(other.y)) && this.z.Equals(other.z)) && this.w.Equals(other.w));
        }

        public override string ToString()
        {
            object[] args = new object[] { this.x, this.y, this.z, this.w };
            return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[] { this.x.ToString(format, CultureInfo.InvariantCulture.NumberFormat), this.y.ToString(format, CultureInfo.InvariantCulture.NumberFormat), this.z.ToString(format, CultureInfo.InvariantCulture.NumberFormat), this.w.ToString(format, CultureInfo.InvariantCulture.NumberFormat) };
            return UnityString.Format("({0}, {1}, {2}, {3})", args);
        }

        static Quaternion()
        {
            identityQuaternion = new Quaternion(0f, 0f, 0f, 1f);
        }

        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void FromToRotation_Injected(ref Vector3 fromDirection, ref Vector3 toDirection, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Inverse_Injected(ref Quaternion rotation, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Slerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void SlerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Lerp_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void LerpUnclamped_Injected(ref Quaternion a, ref Quaternion b, float t, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Internal_FromEulerRad_Injected(ref Vector3 euler, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Internal_ToEulerRad_Injected(ref Quaternion rotation, out Vector3 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Internal_ToAxisAngleRad_Injected(ref Quaternion q, out Vector3 axis, out float angle);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void AngleAxis_Injected(float angle, ref Vector3 axis, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void LookRotation_Injected(ref Vector3 forward, [DefaultValue("Vector3.up")] ref Vector3 upwards, out Quaternion ret);
    }
}

