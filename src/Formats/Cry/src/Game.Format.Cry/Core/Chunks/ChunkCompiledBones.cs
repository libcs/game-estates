using System.Collections.Generic;
using System.Linq;
using static Game.Core.CoreDebug;

namespace Game.Format.Cry.Core
{
    public abstract class ChunkCompiledBones : Chunk     //  0xACDC0000:  Bones info
    {
        public string RootBoneName;         // Controller ID?  Name?  Not sure yet.
        public CompiledBone RootBone;       // First bone in the data structure.  Usually Bip01
        public int NumBones;                // Number of bones in the chunk
        // Bones are a bit different than Node Chunks, since there is only one CompiledBones Chunk, and it contains all the bones in the model.
        public Dictionary<int, CompiledBone> BoneDictionary = new Dictionary<int, CompiledBone>();  // Dictionary of all the CompiledBone objects based on parent offset(?).
        public List<CompiledBone> BoneList = new List<CompiledBone>();

        public CompiledBone GetParentBone(CompiledBone bone, int boneIndex) => bone.offsetParent != 0 ? BoneDictionary[boneIndex + bone.offsetParent] : null; // Should only be one parent.

        public List<CompiledBone> GetAllChildBones(CompiledBone bone) => bone.numChildren > 0 ? BoneList.Where(a => bone.childIDs.Contains(a.ControllerID)).ToList() : null;

        public List<string> GetBoneNames() => BoneList.Select(a => a.boneName).ToList();  // May need to replace space in bone names with _.

        protected void AddChildIDToParent(CompiledBone bone)
        {
            // Root bone parent ID will be zero.
            if (bone.parentID != 0)
                BoneList.Where(a => a.ControllerID == bone.parentID).FirstOrDefault()?.childIDs.Add(bone.ControllerID); // Should only be one parent.
        }

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

        protected void SetRootBoneLocalTransformMatrix()
        {
            RootBone.LocalTransform.m00 = RootBone.boneToWorld.boneToWorld[0, 0];
            RootBone.LocalTransform.m01 = RootBone.boneToWorld.boneToWorld[0, 1];
            RootBone.LocalTransform.m02 = RootBone.boneToWorld.boneToWorld[0, 2];
            RootBone.LocalTransform.m03 = RootBone.boneToWorld.boneToWorld[0, 3];
            RootBone.LocalTransform.m10 = RootBone.boneToWorld.boneToWorld[1, 0];
            RootBone.LocalTransform.m11 = RootBone.boneToWorld.boneToWorld[1, 1];
            RootBone.LocalTransform.m12 = RootBone.boneToWorld.boneToWorld[1, 2];
            RootBone.LocalTransform.m13 = RootBone.boneToWorld.boneToWorld[1, 3];
            RootBone.LocalTransform.m20 = RootBone.boneToWorld.boneToWorld[2, 0];
            RootBone.LocalTransform.m21 = RootBone.boneToWorld.boneToWorld[2, 1];
            RootBone.LocalTransform.m22 = RootBone.boneToWorld.boneToWorld[2, 2];
            RootBone.LocalTransform.m23 = RootBone.boneToWorld.boneToWorld[2, 3];
            RootBone.LocalTransform.m30 = 0;
            RootBone.LocalTransform.m31 = 0;
            RootBone.LocalTransform.m32 = 0;
            RootBone.LocalTransform.m33 = 1;
        }

        public override void WriteChunk()
        {
            Log($"*** START CompiledBone Chunk ***");
            Log($"    ChunkType:           {ChunkType}");
            Log($"    Node ID:             {ID:X}");
        }
    }

}
