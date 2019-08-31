using System;
using System.IO;

namespace Game.Format.Cry.Core
{
    // cccc0014:  provides material name as used in the .mtl file
    public class ChunkMtlName_744 : ChunkMtlName
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            Name = r.ReadFString(128);
            NumChildren = r.ReadUInt32();
            PhysicsType = new MtlNamePhysicsType[NumChildren];
            MatType = NumChildren == 0 ? MtlNameTypeEnum.Single : MtlNameTypeEnum.Library;
            for (var i = 0; i < NumChildren; i++)
                PhysicsType[i] = (MtlNamePhysicsType)Enum.ToObject(typeof(MtlNamePhysicsType), r.ReadUInt32());
        }
    }
}
