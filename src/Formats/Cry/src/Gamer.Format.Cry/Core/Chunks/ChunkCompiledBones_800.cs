using System.Collections.Generic;
using System.IO;

namespace Gamer.Format.Cry.Core
{
    public class ChunkCompiledBones_800 : ChunkCompiledBones
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            SkipBytes(r, 32);  // Padding between the chunk header and the first bone.
            Vector3 localTranslation;
            Matrix33 localRotation;
            //  Read the first bone with ReadCompiledBone, then recursively grab all the children for each bone you find.
            //  Each bone structure is 584 bytes, so will need to seek childOffset * 584 each time, and go back.
            NumBones = (int)((Size - 32) / 584);
            for (var i = 0; i < NumBones; i++)
            {
                var tempBone = new CompiledBone();
                tempBone.ReadCompiledBone(r);
                if (RootBone == null)  // First bone read is root bone
                    RootBone = tempBone;
                tempBone.LocalTranslation = tempBone.boneToWorld.GetBoneToWorldTranslationVector();       // World positions of the bone
                tempBone.LocalRotation = tempBone.boneToWorld.GetBoneToWorldRotationMatrix();            // World rotation of the bone.
                //tempBone.ParentBone = BoneMap[i + tempBone.offsetParent];
                tempBone.ParentBone = GetParentBone(tempBone, i);
                tempBone.parentID = tempBone.ParentBone != null ? tempBone.ParentBone.ControllerID : 0;
                if (tempBone.parentID != 0)
                {
                    localRotation = GetParentBone(tempBone, i).boneToWorld.GetBoneToWorldRotationMatrix().ConjugateTransposeThisAndMultiply(tempBone.boneToWorld.GetBoneToWorldRotationMatrix());
                    localTranslation = GetParentBone(tempBone, i).LocalRotation * (tempBone.LocalTranslation - GetParentBone(tempBone, i).boneToWorld.GetBoneToWorldTranslationVector());
                }
                else
                {
                    localTranslation = tempBone.boneToWorld.GetBoneToWorldTranslationVector();
                    localRotation = tempBone.boneToWorld.GetBoneToWorldRotationMatrix();
                }
                tempBone.LocalTransform = GetTransformFromParts(localTranslation, localRotation);
                BoneList.Add(tempBone);
                BoneDictionary[i] = tempBone;
            }
            // Add the ChildID to the parent bone.  This will help with navigation. Also set up the TransformSoFar
            foreach (var bone in BoneList)
                AddChildIDToParent(bone);
            var skin = GetSkinningInfo();
            skin.CompiledBones = new List<CompiledBone>();
            skin.HasSkinningInfo = true;
            skin.CompiledBones = BoneList;
        }

        /// <summary>
        /// Writes the results of common matrix math.  For testing purposes.
        /// </summary>
        /// <param name="localRotation">The matrix that the math functions will be applied to.</param>
        void WriteMatrices(Matrix33 localRotation)
        {
            localRotation.WriteMatrix33("Regular");
            localRotation.Inverse().WriteMatrix33("Inverse");
            localRotation.Conjugate().WriteMatrix33("Conjugate");
            localRotation.ConjugateTranspose().WriteMatrix33("Conjugate Transpose");
        }
    }
}
