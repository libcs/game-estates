using System;
using System.IO;
using System.Linq;

namespace Gamer.Format.Cry.Core
{
    public class ChunkBoneNameList_745 : ChunkBoneNameList
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            BoneNames = r.ReadCString().Split(' ').ToList();
        }

        public override void WriteChunk() => base.WriteChunk();
    }
}
