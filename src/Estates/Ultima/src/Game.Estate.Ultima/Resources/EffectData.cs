using System;
using System.IO;

namespace Game.Estate.Ultima.Resources
{
    public struct EffectData
    {
        readonly byte _unknown;

        public readonly sbyte[] Frames;
        public readonly byte FrameCount;
        public readonly byte FrameInterval;
        public readonly byte StartInterval;

        public EffectData(BinaryReader r)
        {
            var data = r.ReadBytes(0x40);
            Frames = Array.ConvertAll(data, b => unchecked((sbyte)b));
            _unknown = r.ReadByte();
            FrameCount = r.ReadByte();
            if (FrameCount == 0)
            {
                FrameCount = 1;
                Frames[0] = 0;
            }
            FrameInterval = r.ReadByte();
            if (FrameInterval == 0)
                FrameInterval = 1;
            StartInterval = r.ReadByte();
        }
    }
}
