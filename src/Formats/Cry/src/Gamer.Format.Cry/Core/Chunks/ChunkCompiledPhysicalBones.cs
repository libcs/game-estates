using System.Collections.Generic;
using System.Linq;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class ChunkCompiledPhysicalBones : Chunk     //  0xACDC0000:  Bones info
    {
        public char[] Reserved;             // 32 byte array
        public CompiledPhysicalBone RootPhysicalBone;       // First bone in the data structure.  Usually Bip01
        public int NumBones;                // Number of bones in the chunk
        public Dictionary<uint, CompiledPhysicalBone> PhysicalBoneDictionary = new Dictionary<uint, CompiledPhysicalBone>();  // Dictionary of all the CompiledBone objects based on bone name.
        public List<CompiledPhysicalBone> PhysicalBoneList = new List<CompiledPhysicalBone>();

        protected void AddChildIDToParent(CompiledPhysicalBone bone)
        {
            // Root bone parent ID will be zero.
            if (bone.parentID != 0)
                PhysicalBoneList.Where(a => a.ControllerID == bone.parentID).FirstOrDefault()?.childIDs.Add(bone.ControllerID); // Should only be one parent.
        }

        public List<CompiledPhysicalBone> GetAllChildBones(CompiledPhysicalBone bone) => bone.NumChildren > 0 ? PhysicalBoneList.Where(a => bone.childIDs.Contains(a.ControllerID)).ToList() : null;

        protected Matrix44 GetTransformFromParts(Vector3 localTranslation, Matrix33 localRotation) => new Matrix44
        {
            // Translation part
            m14 = localTranslation.x,
            m24 = localTranslation.y,
            m34 = localTranslation.z,
            // Rotation part
            m11 = localRotation.m11,
            m12 = localRotation.m12,
            m13 = localRotation.m13,
            m21 = localRotation.m21,
            m22 = localRotation.m22,
            m23 = localRotation.m23,
            m31 = localRotation.m31,
            m32 = localRotation.m32,
            m33 = localRotation.m33,
            // Set final row
            m41 = 0,
            m42 = 0,
            m43 = 0,
            m44 = 1
        };

        public override void WriteChunk()
        {
            Log($"*** START CompiledBone Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
        }
    }
}
