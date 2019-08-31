using System.IO;

namespace Game.Format.Cry.Core
{
    /// <summary>
    /// Remapped to CryEngine_744 format for now
    /// </summary>
    public class ChunkMtlName_802 : ChunkMtlName_744
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            // Appears to have 4 more Bytes than ChunkMtlName_744
        }
    }
}
