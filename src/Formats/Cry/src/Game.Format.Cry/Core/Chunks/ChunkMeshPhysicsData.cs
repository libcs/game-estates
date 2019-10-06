using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    class ChunkMeshPhysicsData : Chunk
    {
        // Collision mesh or something like that.  TODO
        public int PhysicsDataSize;             //Size of the physical data at the end of the chunk.
        public int Flags;
        public int TetrahedraDataSize;          // Bytes per data entry
        public int TetrahedraID;                // Chunk ID of the data stream
        public ChunkDataStream Tetrahedra;
        public uint Reserved1;
        public uint Reserved2;

        public PhysicsData physicsData { get; internal set; }  // if physicsdatasize != 0
        public byte[] TetrahedraData { get; internal set; } // Array length TetrahedraDataSize.  

        public override void WriteChunk()
        {
            Log($"*** START CompiledBone Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
            Log($"    Node ID:             {PhysicsDataSize:X}");
            Log($"    Node ID:             {TetrahedraDataSize:X}");
            Log($"    Node ID:             {TetrahedraID:X}");
            Log($"    Node ID:             {ID:X}");
        }
    }
}
