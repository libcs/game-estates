using System.IO;

namespace Gamer.Format.Cry.Core
{
    public interface IBinaryChunk
    {
        void Read(BinaryReader reader);
        void Write(BinaryWriter writer);
    }
}
