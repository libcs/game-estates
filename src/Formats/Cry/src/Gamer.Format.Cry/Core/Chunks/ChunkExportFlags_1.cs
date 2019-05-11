using System;
using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkExportFlags_1 : ChunkExportFlags
    {
        public override void Read(BinaryReader b)
        {
            base.Read(b);
            var tmpExportFlag = b.ReadUInt32();
            ChunkType = (ChunkTypeEnum)Enum.ToObject(typeof(ChunkTypeEnum), tmpExportFlag);
            Version = b.ReadUInt32();
            ChunkOffset = b.ReadUInt32();
            ID = b.ReadInt32();
            SkipBytes(b, 4);
            RCVersion = new uint[4];
            for (var count = 0; count < 4; count++)
                RCVersion[count] = b.ReadUInt32();
            RCVersionString = b.ReadFString(16);
            SkipBytes(b);
        }
    }
}
