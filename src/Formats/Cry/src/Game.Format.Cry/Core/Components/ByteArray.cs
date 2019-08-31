﻿using System.Runtime.InteropServices;

namespace Game.Format.Cry.Core.Components
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ByteArray
    {
        [FieldOffset(0)] public byte byte1;
        [FieldOffset(1)] public byte byte2;
        [FieldOffset(2)] public byte byte3;
        [FieldOffset(3)] public byte byte4;
        [FieldOffset(0)] public uint uint1;
        [FieldOffset(0)] public int int1;
        [FieldOffset(0)] public float float1;
    }
}
