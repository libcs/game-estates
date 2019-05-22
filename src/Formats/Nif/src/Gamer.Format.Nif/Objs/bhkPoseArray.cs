/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*!
 * Found in Fallout 3 .psa files, extra ragdoll info for NPCs/creatures. (usually
 * idleanims\deathposes.psa)
 *         Defines different kill poses. The game selects the pose randomly and
 * applies it to a skeleton immediately upon ragdolling.
 *         Poses can be previewed in GECK Object Window-Actor Data-Ragdoll and
 * selecting Pose Matching tab.
 */
public class bhkPoseArray : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkPoseArray", NiObject.TYPE);
	/*!  */
	internal int numBones;
	/*!  */
	internal IList<IndexString> bones;
	/*!  */
	internal int numPoses;
	/*!  */
	internal IList<BonePose> poses;

	public bhkPoseArray() {
	numBones = (int)0;
	numPoses = (int)0;
}

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new bhkPoseArray();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numBones, s, info);
	bones = new IndexString[numBones];
	for (var i1 = 0; i1 < bones.Count; i1++) {
		Nif.NifStream(out bones[i1], s, info);
	}
	Nif.NifStream(out numPoses, s, info);
	poses = new BonePose[numPoses];
	for (var i1 = 0; i1 < poses.Count; i1++) {
		Nif.NifStream(out poses[i1].numTransforms, s, info);
		poses[i1].transforms = new BoneTransform[poses[i1].numTransforms];
		for (var i2 = 0; i2 < poses[i1].transforms.Count; i2++) {
			Nif.NifStream(out poses[i1].transforms[i2].translation, s, info);
			Nif.NifStream(out poses[i1].transforms[i2].rotation.x, s, info);
			Nif.NifStream(out poses[i1].transforms[i2].rotation.y, s, info);
			Nif.NifStream(out poses[i1].transforms[i2].rotation.z, s, info);
			Nif.NifStream(out poses[i1].transforms[i2].rotation.w, s, info);
			Nif.NifStream(out poses[i1].transforms[i2].scale, s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numPoses = (int)poses.Count;
	numBones = (int)bones.Count;
	Nif.NifStream(numBones, s, info);
	for (var i1 = 0; i1 < bones.Count; i1++) {
		Nif.NifStream(bones[i1], s, info);
	}
	Nif.NifStream(numPoses, s, info);
	for (var i1 = 0; i1 < poses.Count; i1++) {
		poses[i1].numTransforms = (uint)poses[i1].transforms.Count;
		Nif.NifStream(poses[i1].numTransforms, s, info);
		for (var i2 = 0; i2 < poses[i1].transforms.Count; i2++) {
			Nif.NifStream(poses[i1].transforms[i2].translation, s, info);
			Nif.NifStream(poses[i1].transforms[i2].rotation.x, s, info);
			Nif.NifStream(poses[i1].transforms[i2].rotation.y, s, info);
			Nif.NifStream(poses[i1].transforms[i2].rotation.z, s, info);
			Nif.NifStream(poses[i1].transforms[i2].rotation.w, s, info);
			Nif.NifStream(poses[i1].transforms[i2].scale, s, info);
		}
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	numPoses = (int)poses.Count;
	numBones = (int)bones.Count;
	s.AppendLine($"  Num Bones:  {numBones}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bones.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Bones[{i1}]:  {bones[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Poses:  {numPoses}");
	array_output_count = 0;
	for (var i1 = 0; i1 < poses.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		poses[i1].numTransforms = (uint)poses[i1].transforms.Count;
		s.AppendLine($"    Num Transforms:  {poses[i1].numTransforms}");
		array_output_count = 0;
		for (var i2 = 0; i2 < poses[i1].transforms.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      Translation:  {poses[i1].transforms[i2].translation}");
			s.AppendLine($"      x:  {poses[i1].transforms[i2].rotation.x}");
			s.AppendLine($"      y:  {poses[i1].transforms[i2].rotation.y}");
			s.AppendLine($"      z:  {poses[i1].transforms[i2].rotation.z}");
			s.AppendLine($"      w:  {poses[i1].transforms[i2].rotation.w}");
			s.AppendLine($"      Scale:  {poses[i1].transforms[i2].scale}");
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}