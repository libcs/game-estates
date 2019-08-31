using System.IO;

namespace Game.Format.Cry.Core
{
    public interface IBinaryChunk
    {
        void Read(BinaryReader reader);
        void Write(BinaryWriter writer);
    }
}
