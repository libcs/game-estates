﻿using static Game.Core.Debug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkCompiledIntFaces : Chunk
    {
        public int Reserved;
        public uint NumIntFaces;
        public TFace[] Faces;

        public override void WriteChunk()
        {
            Log($"*** START MorphTargets Chunk ***");
            Log($"    ChunkType:           {ChunkType}" );
            Log($"    Node ID:             {ID:X}" );
        }
    }
}
