using Game.Core;
using UnityEngine;

namespace Game.Format.Cry
{
    public static class CryUtils
    {
        public static UnityEngine.Vector3 CryVectorToUnityVector(UnityEngine.Vector3 vector) { Utils.Swap(ref vector.y, ref vector.z); return vector; }

        public static UnityEngine.Vector3 CryPointToUnityPoint(Vector3 point) => CryVectorToUnityVector(point.ToUnity()); // / ConvertUtils.MeterInUnits;

        public static Matrix4x4 CryRotationMatrixToUnityRotationMatrix(Matrix4x4 rotationMatrix) => new Matrix4x4
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

        public static Quaternion CryRotationMatrixToUnityQuaternion(Matrix4x4 rotationMatrix) => ConvertUtils.RotationMatrixToQuaternion(CryRotationMatrixToUnityRotationMatrix(rotationMatrix));

        public static Quaternion CryEulerAnglesToUnityQuaternion(Vector3 eulerAngles)
        {
            var eulerAngles2 = CryVectorToUnityVector(eulerAngles.ToUnity());
            var xRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles2.x, UnityEngine.Vector3.right);
            var yRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles2.y, UnityEngine.Vector3.up);
            var zRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles2.z, UnityEngine.Vector3.forward);
            return xRot * zRot * yRot;
        }
    }
}