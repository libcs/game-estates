using UnityEngine;

namespace Game.Core
{
    public static class UnityExtensions
    {
        // NifUtils
        public static Vector3 ToUnityVector(this Vector3 source) { Utils.Swap(ref source.y, ref source.z); return source; }
        public static Vector3 ToUnityVector(this Vector3 source, float meterInUnits) => source.ToUnityVector() / meterInUnits;
        public static Matrix4x4 ToUnityRotationMatrix(this Matrix4x4 rotationMatrix) => new Matrix4x4
        {
            m00 = rotationMatrix.m00,
            m01 = rotationMatrix.m02,
            m02 = rotationMatrix.m01,
            m03 = 0,
            m10 = rotationMatrix.m20,
            m11 = rotationMatrix.m22,
            m12 = rotationMatrix.m21,
            m13 = 0,
            m20 = rotationMatrix.m10,
            m21 = rotationMatrix.m12,
            m22 = rotationMatrix.m11,
            m23 = 0,
            m30 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1
        };
        public static Quaternion ToUnityQuaternionAsRotationMatrix(this Matrix4x4 rotationMatrix) => ToQuaternionAsRotationMatrix(rotationMatrix.ToUnityRotationMatrix());
        public static Quaternion ToQuaternionAsRotationMatrix(this Matrix4x4 rotationMatrix) => Quaternion.LookRotation(rotationMatrix.GetColumn(2), rotationMatrix.GetColumn(1));
        public static Quaternion ToUnityQuaternionAsEulerAngles(this Vector3 eulerAngles)
        {
            eulerAngles = eulerAngles.ToUnityVector();
            var xRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles.x, Vector3.right);
            var yRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles.y, Vector3.up);
            var zRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles.z, Vector3.forward);
            return xRot * zRot * yRot;
        }

        // Other
        public static void FromMatrix(this Transform transform, Matrix4x4 matrix)
        {
            transform.localScale = matrix.ExtractScale();
            transform.rotation = matrix.ExtractRotation();
            transform.position = matrix.ExtractPosition();
        }

        public static Quaternion ExtractRotation(this Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);
        }

        public static Vector3 ExtractPosition(this Matrix4x4 matrix)
        {
            Vector3 position;
            position.x = matrix.m03;
            position.y = matrix.m13;
            position.z = matrix.m23;
            return position;
        }

        public static Vector3 ExtractScale(this Matrix4x4 matrix)
        {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
            return scale;
        }
    }
}