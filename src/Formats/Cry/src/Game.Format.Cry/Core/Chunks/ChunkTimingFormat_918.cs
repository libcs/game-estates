﻿using System;
using System.IO;

namespace Game.Format.Cry.Core
{
    public class ChunkTimingFormat_918 : ChunkTimingFormat
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            SecsPerTick = r.ReadSingle();
            TicksPerFrame = r.ReadInt32();
            GlobalRange.Name = r.ReadFString(32); // Name is technically a String32, but F those structs
            GlobalRange.Start = r.ReadInt32();
            GlobalRange.End = r.ReadInt32();
        }
    }
}
