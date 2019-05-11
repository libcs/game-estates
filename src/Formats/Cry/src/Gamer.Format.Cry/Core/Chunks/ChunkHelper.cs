using System;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    /// <summary>
    /// Helper chunk.  This is the top level, then nodes, then mesh, then mesh subsets
    /// CCCC0001  
    /// </summary>
    public abstract class ChunkHelper : Chunk
    {
        public string Name;
        public HelperTypeEnum HelperType;
        public Vector3 Pos;
        public Matrix44 Transform;

        public override void WriteChunk()
        {
            Log($"*** START Helper Chunk ***");
            Log($"    ChunkType:   {ChunkType}" );
            Log($"    Version:     {Version:X}" );
            Log($"    ID:          {ID:X}" );
            Log($"    HelperType:  {HelperType}" );
            Log($"    Position:    {Pos.x}, {Pos.y}, {Pos.z}");
            Log($"*** END Helper Chunk ***");
        }
    }
}
