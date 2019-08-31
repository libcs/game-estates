using System.IO;
using System.Linq;

namespace Game.Format.Cry.Core
{
    public class ChunkCompiledExtToIntMap_800 : ChunkCompiledExtToIntMap
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            NumExtVertices = DataSize / sizeof(ushort);
            Source = new ushort[NumExtVertices];
            for (var i = 0; i < NumExtVertices; i++)
                Source[i] = r.ReadUInt16();
            // Add to SkinningInfo
            var skin = GetSkinningInfo();
            skin.Ext2IntMap = Source.ToList();
            skin.HasIntToExtMapping = true;
        }

        public override void WriteChunk() => base.WriteChunk();
    }
}
