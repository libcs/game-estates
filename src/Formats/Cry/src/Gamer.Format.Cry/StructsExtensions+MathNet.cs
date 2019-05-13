using MathNet.Numerics.LinearAlgebra;

namespace Gamer.Format.Cry
{
    public static partial class StructsExtensions
    {
        //public static Vector<float> ToMathVector3(this Vector3 s) { var r = Vector<float>.Build.Dense(3); r[0] = s.x; r[1] = s.y; r[2] = s.z; return r; }
        public static Vector3 GetVector3(this Vector3 s, Vector<float> vector) => new Vector3 { x = vector[0], y = vector[1], z = vector[2] };

        public static Matrix<float> ToMathMatrix(this Matrix3x3 s)
        {
            var r = Matrix<float>.Build.Dense(3, 3);
            r[0, 0] = s.m00;
            r[0, 1] = s.m01;
            r[0, 2] = s.m02;
            r[1, 0] = s.m10;
            r[1, 1] = s.m11;
            r[1, 2] = s.m12;
            r[2, 0] = s.m20;
            r[2, 1] = s.m21;
            r[2, 2] = s.m22;
            return r;
        }
        public static Matrix3x3 GetMatrix3x3(Matrix<float> matrix) => new Matrix3x3
        {
            m00 = matrix[0, 0],
            m01 = matrix[0, 1],
            m02 = matrix[0, 2],
            m10 = matrix[1, 0],
            m11 = matrix[1, 1],
            m12 = matrix[1, 2],
            m20 = matrix[2, 0],
            m21 = matrix[2, 1],
            m22 = matrix[2, 2]
        };

        public static Matrix<float> ToMathMatrix(this Matrix4x4 s)
        {
            var r = Matrix<float>.Build.Dense(4, 4);
            r[0, 0] = s.m00;
            r[0, 1] = s.m01;
            r[0, 2] = s.m02;
            r[0, 3] = s.m03;
            r[1, 0] = s.m10;
            r[1, 1] = s.m11;
            r[1, 2] = s.m12;
            r[1, 3] = s.m13;
            r[2, 0] = s.m20;
            r[2, 1] = s.m21;
            r[2, 2] = s.m22;
            r[2, 3] = s.m23;
            r[3, 0] = s.m30;
            r[3, 1] = s.m31;
            r[3, 2] = s.m32;
            r[3, 3] = s.m33;
            return r;
        }
        public static Matrix4x4 GetMatrix4x4(Matrix<float> matrix) => new Matrix4x4
        {
            m00 = matrix[0, 0],
            m01 = matrix[0, 1],
            m02 = matrix[0, 2],
            m03 = matrix[0, 3],
            m10 = matrix[1, 0],
            m11 = matrix[1, 1],
            m12 = matrix[1, 2],
            m13 = matrix[1, 3],
            m20 = matrix[2, 0],
            m21 = matrix[2, 1],
            m22 = matrix[2, 2],
            m23 = matrix[2, 3],
            m30 = matrix[3, 0],
            m31 = matrix[3, 1],
            m32 = matrix[3, 2],
            m33 = matrix[3, 3],
        };
    }
}