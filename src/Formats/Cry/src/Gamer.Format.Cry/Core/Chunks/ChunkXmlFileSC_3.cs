using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkXmlFileSC_3 : ChunkXmlFileSC
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            var xml = CryXmlSerializer.ReadStream(r.BaseStream);
        }
    }
}
