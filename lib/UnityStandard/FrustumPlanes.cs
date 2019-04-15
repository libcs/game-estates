namespace UnityEngine
{
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct FrustumPlanes
    {
        public float left;
        public float right;
        public float bottom;
        public float top;
        public float zNear;
        public float zFar;
    }
}
