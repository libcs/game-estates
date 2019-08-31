using System.IO;
using System.Linq;

namespace Game.Format.Cry.Core
{
    public class ChunkCompiledIntFaces_800 : ChunkCompiledIntFaces
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            NumIntFaces = DataSize / 6;        // This is an array of TFaces, which are 3 uint16.
            Faces = new TFace[NumIntFaces];
            for (var i = 0; i < NumIntFaces; i++)
            {
                Faces[i].i0 = r.ReadUInt16();
                Faces[i].i1 = r.ReadUInt16();
                Faces[i].i2 = r.ReadUInt16();
            }
            var skin = GetSkinningInfo();
            skin.IntFaces = Faces.ToList();
        }

        public override void WriteChunk() => base.WriteChunk();
    }
}
