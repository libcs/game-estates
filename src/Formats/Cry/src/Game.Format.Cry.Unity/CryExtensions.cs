namespace Game.Format.Cry
{
    public static partial class CryExtensions
    {
        public static UnityEngine.Matrix4x4 ToUnity(this Matrix4x4 s) => new UnityEngine.Matrix4x4(
            new UnityEngine.Vector4(s.m00, s.m01, s.m02, s.m03),
            new UnityEngine.Vector4(s.m10, s.m11, s.m12, s.m13),
            new UnityEngine.Vector4(s.m20, s.m21, s.m22, s.m23),
            new UnityEngine.Vector4(s.m20, s.m31, s.m32, s.m33)
        );

        public static UnityEngine.Vector3 ToUnity(this Vector3 s) => new UnityEngine.Vector3(s.x, s.y, s.z);

        public static UnityEngine.Quaternion ToUnity(this Quat s) => new UnityEngine.Quaternion(s.x, s.y, s.z, s.w);
    }
}
