/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.
//-----------------------------------NOTICE----------------------------------//
// Only add custom code in the designated areas to preserve between builds   //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*!
	 * Found in Fallout 3 .psa files, extra ragdoll info for NPCs/creatures. (usually idleanims\deathposes.psa)
	 *         Defines different kill poses. The game selects the pose randomly and applies it to a skeleton immediately upon ragdolling.
	 *         Poses can be previewed in GECK Object Window-Actor Data-Ragdoll and selecting Pose Matching tab.
	 */
	public class bhkPoseArray : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkPoseArray", NiObject.TYPE);

		/*! numBones */
		internal int numBones;
		/*! bones */
		internal IList<IndexString> bones;
		/*! numPoses */
		internal int numPoses;
		/*! poses */
		internal IList<BonePose> poses;
		public bhkPoseArray()
		{
			numBones = (int)0;
			numPoses = (int)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkPoseArray();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numBones, s, info);
			bones = new IndexString[numBones];
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				Nif.NifStream(out bones[i3], s, info);
			}
			Nif.NifStream(out numPoses, s, info);
			poses = new BonePose[numPoses];
			for (var i3 = 0; i3 < poses.Count; i3++)
			{
				Nif.NifStream(out poses[i3].numTransforms, s, info);
				poses[i3].transforms = new BoneTransform[poses[i3].numTransforms];
				for (var i4 = 0; i4 < poses[i3].transforms.Count; i4++)
				{
					Nif.NifStream(out poses[i3].transforms[i4].translation, s, info);
					Nif.NifStream(out poses[i3].transforms[i4].rotation.x, s, info);
					Nif.NifStream(out poses[i3].transforms[i4].rotation.y, s, info);
					Nif.NifStream(out poses[i3].transforms[i4].rotation.z, s, info);
					Nif.NifStream(out poses[i3].transforms[i4].rotation.w, s, info);
					Nif.NifStream(out poses[i3].transforms[i4].scale, s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numPoses = (int)poses.Count;
			numBones = (int)bones.Count;
			Nif.NifStream(numBones, s, info);
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				Nif.NifStream(bones[i3], s, info);
			}
			Nif.NifStream(numPoses, s, info);
			for (var i3 = 0; i3 < poses.Count; i3++)
			{
				poses[i3].numTransforms = (uint)poses[i3].transforms.Count;
				Nif.NifStream(poses[i3].numTransforms, s, info);
				for (var i4 = 0; i4 < poses[i3].transforms.Count; i4++)
				{
					Nif.NifStream(poses[i3].transforms[i4].translation, s, info);
					Nif.NifStream(poses[i3].transforms[i4].rotation.x, s, info);
					Nif.NifStream(poses[i3].transforms[i4].rotation.y, s, info);
					Nif.NifStream(poses[i3].transforms[i4].rotation.z, s, info);
					Nif.NifStream(poses[i3].transforms[i4].rotation.w, s, info);
					Nif.NifStream(poses[i3].transforms[i4].scale, s, info);
				}
			}
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			var array_output_count = 0U;
			s.Append(base.AsString());
			numPoses = (int)poses.Count;
			numBones = (int)bones.Count;
			s.AppendLine($"      Num Bones:  {numBones}");
			array_output_count = 0;
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Bones[{i3}]:  {bones[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Num Poses:  {numPoses}");
			array_output_count = 0;
			for (var i3 = 0; i3 < poses.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				poses[i3].numTransforms = (uint)poses[i3].transforms.Count;
				s.AppendLine($"        Num Transforms:  {poses[i3].numTransforms}");
				array_output_count = 0;
				for (var i4 = 0; i4 < poses[i3].transforms.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					s.AppendLine($"          Translation:  {poses[i3].transforms[i4].translation}");
					s.AppendLine($"          x:  {poses[i3].transforms[i4].rotation.x}");
					s.AppendLine($"          y:  {poses[i3].transforms[i4].rotation.y}");
					s.AppendLine($"          z:  {poses[i3].transforms[i4].rotation.z}");
					s.AppendLine($"          w:  {poses[i3].transforms[i4].rotation.w}");
					s.AppendLine($"          Scale:  {poses[i3].transforms[i4].scale}");
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			return ptrs;
		}
	}
}
