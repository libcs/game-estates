using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class ChunkMtlName : Chunk  // cccc0014:  provides material name as used in the .mtl file
    {
        /// <summary>
        /// Type of Material associated with this name
        /// </summary>
        public MtlNameTypeEnum MatType { get; internal set; }
        /// <summary>
        /// Name of the Material
        /// </summary>
        public string Name { get; set; }
        public MtlNamePhysicsType[] PhysicsType { get; internal set; }
        /// <summary>
        /// Number of Materials in this name (Max: 66)
        /// </summary>
        public uint NumChildren { get; internal set; }
        public uint[] ChildIDs { get; internal set; }

        public override void WriteChunk()
        {
            Log("*** START MATERIAL NAMES ***");
            Log($"    ChunkType:           {ChunkType} ({ChunkType:X})");
            Log($"    Material Name:       {Name}");
            Log($"    Material ID:         {ID:X}");
            Log($"    Version:             {Version:X}");
            Log($"    Number of Children:  {NumChildren}");
            Log($"    Material Type:       {MatType} ({MatType:X})");
            foreach (var physicsType in PhysicsType)
                Log($"    Physics Type:        {physicsType} ({physicsType:X})");
            Log("*** END MATERIAL NAMES ***");
        }
    }
}
