using System;
using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkMtlName_800 : ChunkMtlName
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            MatType = (MtlNameTypeEnum)r.ReadUInt32();
            // if 0x01, then material lib.  If 0x12, mat name.  This is actually a bitstruct.
            SkipBytes(r, 4);               // NFlags2
            Name = r.ReadFString(128);
            PhysicsType = new MtlNamePhysicsType[] { (MtlNamePhysicsType)r.ReadUInt32() };
            NumChildren = r.ReadUInt32();
            // Now we need to read the Children references.  2 parts; the number of children, and then 66 - numchildren padding
            ChildIDs = new uint[NumChildren];
            for (var i = 0; i < NumChildren; i++)
                ChildIDs[i] = r.ReadUInt32();
            SkipBytes(r, 32);
        }
    }
}
