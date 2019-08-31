using System.IO;

namespace Game.Format.Cry.Core
{
    public class ChunkCompiledMorphTargets_801 : ChunkCompiledMorphTargets
    {
        // TODO:  Implement this.
        public override void Read(BinaryReader r)
        {
            //base.Read(r);
            //NumberOfMorphTargets = r.ReadUInt32();
            //if (NumberOfMorphTargets > 0)
            //{
            //    MorphTargetVertices = new MeshMorphTargetVertex[NumberOfMorphTargets];
            //    for (var i = 0; i < NumberOfMorphTargets; i++)
            //        MorphTargetVertices[i] = MeshMorphTargetVertex.Read(r);
            //}
            //var skin = GetSkinningInfo();
            //skin.MorphTargets = MorphTargetVertices.ToList();
        }

        public override void WriteChunk() => base.WriteChunk();
    }
}
