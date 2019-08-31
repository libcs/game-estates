﻿using System;
using System.IO;

namespace Game.Format.Cry.Core
{
    public class ChunkTimingFormat_919 : ChunkTimingFormat
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            // TODO:  This is copied from 918 but may not be entirely accurate.  Not tested.
            SecsPerTick = r.ReadSingle();
            TicksPerFrame = r.ReadInt32();
            GlobalRange.Name = r.ReadFString(32);  // Name is technically a String32, but F those structs
            GlobalRange.Start = r.ReadInt32();
            GlobalRange.End = r.ReadInt32();
        }
    }
}
