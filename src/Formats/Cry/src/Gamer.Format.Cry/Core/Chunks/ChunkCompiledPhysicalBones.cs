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

        protected Matrix4x4 GetTransformFromParts(Vector3 localTranslation, Matrix3x3 localRotation) => new Matrix4x4
        {
            // Translation part
            m03 = localTranslation.x,
            m13 = localTranslation.y,
            m23 = localTranslation.z,
            // Rotation part
            m00 = localRotation.m00,
            m01 = localRotation.m01,
            m02 = localRotation.m02,
            m10 = localRotation.m10,
            m11 = localRotation.m11,
            m12 = localRotation.m12,
            m20 = localRotation.m20,
            m21 = localRotation.m21,
            m22 = localRotation.m22,
            // Set final row
            m30 = 0,
            m31 = 0,
            m32 = 0,
            m33 = 1
        };

        public override void WriteChunk()
        {
            Log($"*** START CompiledBone Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
        }
    }
}
