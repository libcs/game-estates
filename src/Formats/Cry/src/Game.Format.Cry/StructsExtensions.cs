using System.IO;
using static Game.Core.CoreDebug;

namespace Game.Format.Cry
{
    public static partial class StructsExtensions
    {
        public static void ReadVector3(this Vector3 s, BinaryReader r) { s.x = r.ReadSingle(); s.y = r.ReadSingle(); s.z = r.ReadSingle(); return; }

        public static void ReadMatrix3x3(this Matrix3x3 s, BinaryReader r)
        {
            s.m00 = r.ReadSingle();
            s.m01 = r.ReadSingle();
            s.m02 = r.ReadSingle();
            s.m10 = r.ReadSingle();
            s.m11 = r.ReadSingle();
            s.m12 = r.ReadSingle();
            s.m20 = r.ReadSingle();
            s.m21 = r.ReadSingle();
            s.m22 = r.ReadSingle();
        }

        public static void WriteVector3(this Vector3 s, string label = null)
        {
            Log($"*** WriteVector3 *** - {label}");
            Log($"{s.x:F7}  {s.y:F7}  {s.z:F7}");
            Log();
        }
        public static void WriteVector4(this Vector4 s)
        {
            Log("=============================================");
            Log($"x:{s.x:F7}  y:{s.y:F7}  z:{s.z:F7} w:{s.w:F7}");
        }

        public static void WriteMatrix3x3(this Matrix3x3 s, string label = null)
        {
            Log($"====== {label} ===========");
            Log($"{s.m00:F7}  {s.m01:F7}  {s.m02:F7}");
            Log($"{s.m10:F7}  {s.m11:F7}  {s.m12:F7}");
            Log($"{s.m20:F7}  {s.m21:F7}  {s.m22:F7}");
        }

        public static void WriteMatrix4s4(this Matrix4x4 s)
        {
            Log($"=============================================");
            Log($"{s.m00:F7}  {s.m01:F7}  {s.m02:F7}  {s.m03:F7}");
            Log($"{s.m10:F7}  {s.m11:F7}  {s.m12:F7}  {s.m13:F7}");
            Log($"{s.m20:F7}  {s.m21:F7}  {s.m22:F7}  {s.m23:F7}");
            Log($"{s.m30:F7}  {s.m31:F7}  {s.m32:F7}  {s.m33:F7}");
            Log();
        }
    }
}