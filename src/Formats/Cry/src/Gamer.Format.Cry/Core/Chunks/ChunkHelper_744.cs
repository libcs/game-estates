using System;
using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkHelper_744 : ChunkHelper
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            HelperType = (HelperTypeEnum)Enum.ToObject(typeof(HelperTypeEnum), r.ReadUInt32());
            if (Version == 0x744)  // only has the Position.
            {
                Pos.x = r.ReadSingle();
                Pos.y = r.ReadSingle();
                Pos.z = r.ReadSingle();
            }
            else if (Version == 0x362)   // will probably never see these.
            {
                var tmpName = r.ReadChars(64);
                var stringLength = 0;
                for (int i = 0, j = tmpName.Length; i < j; i++)
                    if (tmpName[i] == 0)
                    {
                        stringLength = i;
                        break;
                    }
                Name = new string(tmpName, 0, stringLength);
                HelperType = (HelperTypeEnum)Enum.ToObject(typeof(HelperTypeEnum), r.ReadUInt32());
                Pos.x = r.ReadSingle();
                Pos.y = r.ReadSingle();
                Pos.z = r.ReadSingle();
            }
        }
    }
}
