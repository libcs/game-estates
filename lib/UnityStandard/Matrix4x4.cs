namespace UnityEngine
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        public float m00;
        public float m10;
        public float m20;
        public float m30;
        public float m01;
        public float m11;
        public float m21;
        public float m31;
        public float m02;
        public float m12;
        public float m22;
        public float m32;
        public float m03;
        public float m13;
        public float m23;
        public float m33;
        private static readonly Matrix4x4 zeroMatrix;
        private static readonly Matrix4x4 identityMatrix;
        public Matrix4x4(Vector4 column0, Vector4 column1, Vector4 column2, Vector4 column3)
        {
            this.m00 = column0.x;
            this.m01 = column1.x;
            this.m02 = column2.x;
            this.m03 = column3.x;
            this.m10 = column0.y;
            this.m11 = column1.y;
            this.m12 = column2.y;
            this.m13 = column3.y;
            this.m20 = column0.z;
            this.m21 = column1.z;
            this.m22 = column2.z;
            this.m23 = column3.z;
            this.m30 = column0.w;
            this.m31 = column1.w;
            this.m32 = column2.w;
            this.m33 = column3.w;
        }

        //private Quaternion GetRotation()
        //{
        //    Quaternion quaternion;
        //    GetRotation_Injected(ref this, out quaternion);
        //    return quaternion;
        //}

        //private Vector3 GetLossyScale()
        //{
        //    Vector3 vector;
        //    GetLossyScale_Injected(ref this, out vector);
        //    return vector;
        //}

        //private bool IsIdentity()
        //{
        //    return IsIdentity_Injected(ref this);
        //}

        //private float GetDeterminant()
        //{
        //    return GetDeterminant_Injected(ref this);
        //}

        //private FrustumPlanes DecomposeProjection()
        //{
        //    FrustumPlanes planes;
        //    DecomposeProjection_Injected(ref this, out planes);
        //    return planes;
        //}

        //public Quaternion rotation
        //{
        //    get
        //    {
        //        return this.GetRotation();
        //    }
        //}
        //public Vector3 lossyScale
        //{
        //    get
        //    {
        //        return this.GetLossyScale();
        //    }
        //}
        //public bool isIdentity
        //{
        //    get
        //    {
        //        return this.IsIdentity();
        //    }
        //}
        //public float determinant
        //{
        //    get
        //    {
        //        return this.GetDeterminant();
        //    }
        //}
        //public FrustumPlanes decomposeProjection
        //{
        //    get
        //    {
        //        return this.DecomposeProjection();
        //    }
        //}
        //public bool ValidTRS()
        //{
        //    return ValidTRS_Injected(ref this);
        //}

        //public static float Determinant(Matrix4x4 m)
        //{
        //    return m.determinant;
        //}

        //public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
        //{
        //    Matrix4x4 matrixx;
        //    TRS_Injected(ref pos, ref q, ref s, out matrixx);
        //    return matrixx;
        //}

        //public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
        //{
        //    this = TRS(pos, q, s);
        //}

        //public static Matrix4x4 Inverse(Matrix4x4 m)
        //{
        //    Matrix4x4 matrixx;
        //    Inverse_Injected(ref m, out matrixx);
        //    return matrixx;
        //}

        //public Matrix4x4 inverse
        //{
        //    get
        //    {
        //        return Inverse(this);
        //    }
        //}
        //public static Matrix4x4 Transpose(Matrix4x4 m)
        //{
        //    Matrix4x4 matrixx;
        //    Transpose_Injected(ref m, out matrixx);
        //    return matrixx;
        //}

        //public Matrix4x4 transpose
        //{
        //    get
        //    {
        //        return Transpose(this);
        //    }
        //}
        //public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar)
        //{
        //    Matrix4x4 matrixx;
        //    Ortho_Injected(left, right, bottom, top, zNear, zFar, out matrixx);
        //    return matrixx;
        //}

        //public static Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar)
        //{
        //    Matrix4x4 matrixx;
        //    Perspective_Injected(fov, aspect, zNear, zFar, out matrixx);
        //    return matrixx;
        //}

        //public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
        //{
        //    Matrix4x4 matrixx;
        //    LookAt_Injected(ref from, ref to, ref up, out matrixx);
        //    return matrixx;
        //}

        //public static Matrix4x4 Frustum(float left, float right, float bottom, float top, float zNear, float zFar)
        //{
        //    Matrix4x4 matrixx;
        //    Frustum_Injected(left, right, bottom, top, zNear, zFar, out matrixx);
        //    return matrixx;
        //}

        //public static Matrix4x4 Frustum(FrustumPlanes fp)
        //{
        //    return Frustum(fp.left, fp.right, fp.bottom, fp.top, fp.zNear, fp.zFar);
        //}

        public float this[int row, int column]
        {
            get
            {
                return this[row + (column * 4)];
            }
            set
            {
                this[row + (column * 4)] = value;
            }
        }
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.m00;

                    case 1:
                        return this.m10;

                    case 2:
                        return this.m20;

                    case 3:
                        return this.m30;

                    case 4:
                        return this.m01;

                    case 5:
                        return this.m11;

                    case 6:
                        return this.m21;

                    case 7:
                        return this.m31;

                    case 8:
                        return this.m02;

                    case 9:
                        return this.m12;

                    case 10:
                        return this.m22;

                    case 11:
                        return this.m32;

                    case 12:
                        return this.m03;

                    case 13:
                        return this.m13;

                    case 14:
                        return this.m23;

                    case 15:
                        return this.m33;
                }
                throw new IndexOutOfRangeException("Invalid matrix index!");
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.m00 = value;
                        break;

                    case 1:
                        this.m10 = value;
                        break;

                    case 2:
                        this.m20 = value;
                        break;

                    case 3:
                        this.m30 = value;
                        break;

                    case 4:
                        this.m01 = value;
                        break;

                    case 5:
                        this.m11 = value;
                        break;

                    case 6:
                        this.m21 = value;
                        break;

                    case 7:
                        this.m31 = value;
                        break;

                    case 8:
                        this.m02 = value;
                        break;

                    case 9:
                        this.m12 = value;
                        break;

                    case 10:
                        this.m22 = value;
                        break;

                    case 11:
                        this.m32 = value;
                        break;

                    case 12:
                        this.m03 = value;
                        break;

                    case 13:
                        this.m13 = value;
                        break;

                    case 14:
                        this.m23 = value;
                        break;

                    case 15:
                        this.m33 = value;
                        break;

                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }
        public override int GetHashCode()
        {
            return (((this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2)) ^ (this.GetColumn(2).GetHashCode() >> 2)) ^ (this.GetColumn(3).GetHashCode() >> 1));
        }

        public override bool Equals(object other)
        {
            return ((other is Matrix4x4) && this.Equals((Matrix4x4)other));
        }

        public bool Equals(Matrix4x4 other)
        {
            return (((this.GetColumn(0).Equals(other.GetColumn(0)) && this.GetColumn(1).Equals(other.GetColumn(1))) && this.GetColumn(2).Equals(other.GetColumn(2))) && this.GetColumn(3).Equals(other.GetColumn(3)));
        }

        public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            Matrix4x4 matrixx;
            matrixx.m00 = (((lhs.m00 * rhs.m00) + (lhs.m01 * rhs.m10)) + (lhs.m02 * rhs.m20)) + (lhs.m03 * rhs.m30);
            matrixx.m01 = (((lhs.m00 * rhs.m01) + (lhs.m01 * rhs.m11)) + (lhs.m02 * rhs.m21)) + (lhs.m03 * rhs.m31);
            matrixx.m02 = (((lhs.m00 * rhs.m02) + (lhs.m01 * rhs.m12)) + (lhs.m02 * rhs.m22)) + (lhs.m03 * rhs.m32);
            matrixx.m03 = (((lhs.m00 * rhs.m03) + (lhs.m01 * rhs.m13)) + (lhs.m02 * rhs.m23)) + (lhs.m03 * rhs.m33);
            matrixx.m10 = (((lhs.m10 * rhs.m00) + (lhs.m11 * rhs.m10)) + (lhs.m12 * rhs.m20)) + (lhs.m13 * rhs.m30);
            matrixx.m11 = (((lhs.m10 * rhs.m01) + (lhs.m11 * rhs.m11)) + (lhs.m12 * rhs.m21)) + (lhs.m13 * rhs.m31);
            matrixx.m12 = (((lhs.m10 * rhs.m02) + (lhs.m11 * rhs.m12)) + (lhs.m12 * rhs.m22)) + (lhs.m13 * rhs.m32);
            matrixx.m13 = (((lhs.m10 * rhs.m03) + (lhs.m11 * rhs.m13)) + (lhs.m12 * rhs.m23)) + (lhs.m13 * rhs.m33);
            matrixx.m20 = (((lhs.m20 * rhs.m00) + (lhs.m21 * rhs.m10)) + (lhs.m22 * rhs.m20)) + (lhs.m23 * rhs.m30);
            matrixx.m21 = (((lhs.m20 * rhs.m01) + (lhs.m21 * rhs.m11)) + (lhs.m22 * rhs.m21)) + (lhs.m23 * rhs.m31);
            matrixx.m22 = (((lhs.m20 * rhs.m02) + (lhs.m21 * rhs.m12)) + (lhs.m22 * rhs.m22)) + (lhs.m23 * rhs.m32);
            matrixx.m23 = (((lhs.m20 * rhs.m03) + (lhs.m21 * rhs.m13)) + (lhs.m22 * rhs.m23)) + (lhs.m23 * rhs.m33);
            matrixx.m30 = (((lhs.m30 * rhs.m00) + (lhs.m31 * rhs.m10)) + (lhs.m32 * rhs.m20)) + (lhs.m33 * rhs.m30);
            matrixx.m31 = (((lhs.m30 * rhs.m01) + (lhs.m31 * rhs.m11)) + (lhs.m32 * rhs.m21)) + (lhs.m33 * rhs.m31);
            matrixx.m32 = (((lhs.m30 * rhs.m02) + (lhs.m31 * rhs.m12)) + (lhs.m32 * rhs.m22)) + (lhs.m33 * rhs.m32);
            matrixx.m33 = (((lhs.m30 * rhs.m03) + (lhs.m31 * rhs.m13)) + (lhs.m32 * rhs.m23)) + (lhs.m33 * rhs.m33);
            return matrixx;
        }

        public static Vector4 operator *(Matrix4x4 lhs, Vector4 vector)
        {
            Vector4 vector2;
            vector2.x = (((lhs.m00 * vector.x) + (lhs.m01 * vector.y)) + (lhs.m02 * vector.z)) + (lhs.m03 * vector.w);
            vector2.y = (((lhs.m10 * vector.x) + (lhs.m11 * vector.y)) + (lhs.m12 * vector.z)) + (lhs.m13 * vector.w);
            vector2.z = (((lhs.m20 * vector.x) + (lhs.m21 * vector.y)) + (lhs.m22 * vector.z)) + (lhs.m23 * vector.w);
            vector2.w = (((lhs.m30 * vector.x) + (lhs.m31 * vector.y)) + (lhs.m32 * vector.z)) + (lhs.m33 * vector.w);
            return vector2;
        }

        public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return ((((lhs.GetColumn(0) == rhs.GetColumn(0)) && (lhs.GetColumn(1) == rhs.GetColumn(1))) && (lhs.GetColumn(2) == rhs.GetColumn(2))) && (lhs.GetColumn(3) == rhs.GetColumn(3)));
        }

        public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
        {
            return !(lhs == rhs);
        }

        public Vector4 GetColumn(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4(this.m00, this.m10, this.m20, this.m30);

                case 1:
                    return new Vector4(this.m01, this.m11, this.m21, this.m31);

                case 2:
                    return new Vector4(this.m02, this.m12, this.m22, this.m32);

                case 3:
                    return new Vector4(this.m03, this.m13, this.m23, this.m33);
            }
            throw new IndexOutOfRangeException("Invalid column index!");
        }

        public Vector4 GetRow(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4(this.m00, this.m01, this.m02, this.m03);

                case 1:
                    return new Vector4(this.m10, this.m11, this.m12, this.m13);

                case 2:
                    return new Vector4(this.m20, this.m21, this.m22, this.m23);

                case 3:
                    return new Vector4(this.m30, this.m31, this.m32, this.m33);
            }
            throw new IndexOutOfRangeException("Invalid row index!");
        }

        public void SetColumn(int index, Vector4 column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = column.w;
        }

        public void SetRow(int index, Vector4 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }

        public Vector3 MultiplyPoint(Vector3 point)
        {
            Vector3 vector;
            vector.x = (((this.m00 * point.x) + (this.m01 * point.y)) + (this.m02 * point.z)) + this.m03;
            vector.y = (((this.m10 * point.x) + (this.m11 * point.y)) + (this.m12 * point.z)) + this.m13;
            vector.z = (((this.m20 * point.x) + (this.m21 * point.y)) + (this.m22 * point.z)) + this.m23;
            float num = (((this.m30 * point.x) + (this.m31 * point.y)) + (this.m32 * point.z)) + this.m33;
            num = 1f / num;
            vector.x *= num;
            vector.y *= num;
            vector.z *= num;
            return vector;
        }

        public Vector3 MultiplyPoint3x4(Vector3 point)
        {
            Vector3 vector;
            vector.x = (((this.m00 * point.x) + (this.m01 * point.y)) + (this.m02 * point.z)) + this.m03;
            vector.y = (((this.m10 * point.x) + (this.m11 * point.y)) + (this.m12 * point.z)) + this.m13;
            vector.z = (((this.m20 * point.x) + (this.m21 * point.y)) + (this.m22 * point.z)) + this.m23;
            return vector;
        }

        public Vector3 MultiplyVector(Vector3 vector)
        {
            Vector3 vector2;
            vector2.x = ((this.m00 * vector.x) + (this.m01 * vector.y)) + (this.m02 * vector.z);
            vector2.y = ((this.m10 * vector.x) + (this.m11 * vector.y)) + (this.m12 * vector.z);
            vector2.z = ((this.m20 * vector.x) + (this.m21 * vector.y)) + (this.m22 * vector.z);
            return vector2;
        }

        //public Plane TransformPlane(Plane plane)
        //{
        //    Matrix4x4 inverse = this.inverse;
        //    float x = plane.normal.x;
        //    float y = plane.normal.y;
        //    float z = plane.normal.z;
        //    float distance = plane.distance;
        //    float num5 = (((inverse.m00 * x) + (inverse.m10 * y)) + (inverse.m20 * z)) + (inverse.m30 * distance);
        //    float num6 = (((inverse.m01 * x) + (inverse.m11 * y)) + (inverse.m21 * z)) + (inverse.m31 * distance);
        //    float num7 = (((inverse.m02 * x) + (inverse.m12 * y)) + (inverse.m22 * z)) + (inverse.m32 * distance);
        //    return new Plane(new Vector3(num5, num6, num7), (((inverse.m03 * x) + (inverse.m13 * y)) + (inverse.m23 * z)) + (inverse.m33 * distance));
        //}

        public static Matrix4x4 Scale(Vector3 vector)
        {
            Matrix4x4 matrixx;
            matrixx.m00 = vector.x;
            matrixx.m01 = 0f;
            matrixx.m02 = 0f;
            matrixx.m03 = 0f;
            matrixx.m10 = 0f;
            matrixx.m11 = vector.y;
            matrixx.m12 = 0f;
            matrixx.m13 = 0f;
            matrixx.m20 = 0f;
            matrixx.m21 = 0f;
            matrixx.m22 = vector.z;
            matrixx.m23 = 0f;
            matrixx.m30 = 0f;
            matrixx.m31 = 0f;
            matrixx.m32 = 0f;
            matrixx.m33 = 1f;
            return matrixx;
        }

        public static Matrix4x4 Translate(Vector3 vector)
        {
            Matrix4x4 matrixx;
            matrixx.m00 = 1f;
            matrixx.m01 = 0f;
            matrixx.m02 = 0f;
            matrixx.m03 = vector.x;
            matrixx.m10 = 0f;
            matrixx.m11 = 1f;
            matrixx.m12 = 0f;
            matrixx.m13 = vector.y;
            matrixx.m20 = 0f;
            matrixx.m21 = 0f;
            matrixx.m22 = 1f;
            matrixx.m23 = vector.z;
            matrixx.m30 = 0f;
            matrixx.m31 = 0f;
            matrixx.m32 = 0f;
            matrixx.m33 = 1f;
            return matrixx;
        }

        public static Matrix4x4 Rotate(Quaternion q)
        {
            Matrix4x4 matrixx;
            float num = q.x * 2f;
            float num2 = q.y * 2f;
            float num3 = q.z * 2f;
            float num4 = q.x * num;
            float num5 = q.y * num2;
            float num6 = q.z * num3;
            float num7 = q.x * num2;
            float num8 = q.x * num3;
            float num9 = q.y * num3;
            float num10 = q.w * num;
            float num11 = q.w * num2;
            float num12 = q.w * num3;
            matrixx.m00 = 1f - (num5 + num6);
            matrixx.m10 = num7 + num12;
            matrixx.m20 = num8 - num11;
            matrixx.m30 = 0f;
            matrixx.m01 = num7 - num12;
            matrixx.m11 = 1f - (num4 + num6);
            matrixx.m21 = num9 + num10;
            matrixx.m31 = 0f;
            matrixx.m02 = num8 + num11;
            matrixx.m12 = num9 - num10;
            matrixx.m22 = 1f - (num4 + num5);
            matrixx.m32 = 0f;
            matrixx.m03 = 0f;
            matrixx.m13 = 0f;
            matrixx.m23 = 0f;
            matrixx.m33 = 1f;
            return matrixx;
        }

        public static Matrix4x4 zero
        {
            get
            {
                return zeroMatrix;
            }
        }
        public static Matrix4x4 identity
        {
            get
            {
                return identityMatrix;
            }
        }
        public override string ToString()
        {
            object[] args = new object[] { this.m00, this.m01, this.m02, this.m03, this.m10, this.m11, this.m12, this.m13, this.m20, this.m21, this.m22, this.m23, this.m30, this.m31, this.m32, this.m33 };
            return UnityString.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", args);
        }

        public string ToString(string format)
        {
            object[] args = new object[] { this.ToInvariantString(format, this.m00), this.ToInvariantString(format, this.m01), this.ToInvariantString(format, this.m02), this.ToInvariantString(format, this.m03), this.ToInvariantString(format, this.m10), this.ToInvariantString(format, this.m11), this.ToInvariantString(format, this.m12), this.ToInvariantString(format, this.m13), this.ToInvariantString(format, this.m20), this.ToInvariantString(format, this.m21), this.ToInvariantString(format, this.m22), this.ToInvariantString(format, this.m23), this.ToInvariantString(format, this.m30), this.ToInvariantString(format, this.m31), this.ToInvariantString(format, this.m32), this.ToInvariantString(format, this.m33) };
            return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", args);
        }

        private string ToInvariantString(string format, float val)
        {
            return val.ToString(format, CultureInfo.InvariantCulture.NumberFormat);
        }

        static Matrix4x4()
        {
            zeroMatrix = new Matrix4x4(new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f), new Vector4(0f, 0f, 0f, 0f));
            identityMatrix = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f), new Vector4(0f, 1f, 0f, 0f), new Vector4(0f, 0f, 1f, 0f), new Vector4(0f, 0f, 0f, 1f));
        }

        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void GetRotation_Injected(ref Matrix4x4 _unity_self, out Quaternion ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void GetLossyScale_Injected(ref Matrix4x4 _unity_self, out Vector3 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern bool IsIdentity_Injected(ref Matrix4x4 _unity_self);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern float GetDeterminant_Injected(ref Matrix4x4 _unity_self);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void DecomposeProjection_Injected(ref Matrix4x4 _unity_self, out FrustumPlanes ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern bool ValidTRS_Injected(ref Matrix4x4 _unity_self);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void TRS_Injected(ref Vector3 pos, ref Quaternion q, ref Vector3 s, out Matrix4x4 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Inverse_Injected(ref Matrix4x4 m, out Matrix4x4 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Transpose_Injected(ref Matrix4x4 m, out Matrix4x4 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Ortho_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Perspective_Injected(float fov, float aspect, float zNear, float zFar, out Matrix4x4 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void LookAt_Injected(ref Vector3 from, ref Vector3 to, ref Vector3 up, out Matrix4x4 ret);
        //[MethodImpl(MethodImplOptions.InternalCall)]
        //private static extern void Frustum_Injected(float left, float right, float bottom, float top, float zNear, float zFar, out Matrix4x4 ret);
    }
}

