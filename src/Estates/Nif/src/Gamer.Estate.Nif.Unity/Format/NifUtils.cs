using Gamer.Core;
using UnityEngine;

namespace Gamer.Estate.Nif.Format
{
    public static class NifUtils
    {
        public static Vector3 NifVectorToUnityVector(Vector3 vector) { Utils.Swap(ref vector.y, ref vector.z); return vector; }

        public static Vector3 NifPointToUnityPoint(Vector3 point) => NifVectorToUnityVector(point) / ConvertUtils.MeterInUnits;

        public static Matrix4x4 NifRotationMatrixToUnityRotationMatrix(Matrix4x4 rotationMatrix) => new Matrix4x4
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

        public static Quaternion NifRotationMatrixToUnityQuaternion(Matrix4x4 rotationMatrix) => ConvertUtils.RotationMatrixToQuaternion(NifRotationMatrixToUnityRotationMatrix(rotationMatrix));

        public static Quaternion NifEulerAnglesToUnityQuaternion(Vector3 eulerAngles)
        {
            var eulerAngles2 = NifVectorToUnityVector(eulerAngles);
            var xRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles2.x, Vector3.right);
            var yRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles2.y, Vector3.up);
            var zRot = Quaternion.AngleAxis(Mathf.Rad2Deg * eulerAngles2.z, Vector3.forward);
            return xRot * zRot * yRot;
        }
    }
}