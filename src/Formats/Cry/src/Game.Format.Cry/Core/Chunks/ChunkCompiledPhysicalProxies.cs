using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkCompiledPhysicalProxies : Chunk        // 0xACDC0003:  Hit boxes?
    {
        // Properties.  VERY similar to datastream, since it's essential vertex info.
        public uint Flags2;
        public uint NumPhysicalProxies; // Number of data entries
        public uint BytesPerElement; // Bytes per data entry
        //public uint Reserved1;
        //public uint Reserved2;
        public PhysicalStream[] PhysicalProxies;

        public override void WriteChunk()
        {
            Log($"*** START CompiledPhysicalProxies Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Number of Targets:   {NumPhysicalProxies:X}");
        }
    }
}
