using System;
using System.IO;

namespace Game.Format.Cry.Core
{
    public class ChunkHeader_744 : ChunkHeader
    {
        public override void Read(BinaryReader r)
        {
            var headerType = r.ReadUInt32();
            ChunkType = (ChunkTypeEnum)headerType;
            Version = r.ReadUInt32();
            Offset = r.ReadUInt32();
            ID = r.ReadInt32();
            Size = 0; // TODO: Figure out how to return a size - postprocess header table maybe?
            //if (ChunkType == ChunkTypeEnum.Timing)
            //    ID += 0xFFFF0000;
        }

        public override void Write(BinaryWriter w) => throw new NotImplementedException();
    }
}
