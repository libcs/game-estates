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
 * Defines the levels of detail for a given character and dictates the character's
 * current LOD.
 */
public class NiSkinningLODController : NiTimeController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSkinningLODController", NiTimeController.TYPE);
	/*!  */
	internal uint currentLod;
	/*!  */
	internal uint numBones;
	/*!  */
	internal IList<NiNode> bones;
	/*!  */
	internal uint numSkins;
	/*!  */
	internal IList<NiMesh> skins;
	/*!  */
	internal uint numLodLevels;
	/*!  */
	internal IList<LODInfo> lods;

	public NiSkinningLODController() {
	currentLod = (uint)0;
	numBones = (uint)0;
	numSkins = (uint)0;
	numLodLevels = (uint)0;
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
public static NiObject Create() => new NiSkinningLODController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out currentLod, s, info);
	Nif.NifStream(out numBones, s, info);
	bones = new Ref[numBones];
	for (var i1 = 0; i1 < bones.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numSkins, s, info);
	skins = new Ref[numSkins];
	for (var i1 = 0; i1 < skins.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numLodLevels, s, info);
	lods = new LODInfo[numLodLevels];
	for (var i1 = 0; i1 < lods.Count; i1++) {
		Nif.NifStream(out lods[i1].numBones, s, info);
		Nif.NifStream(out lods[i1].numActiveSkins, s, info);
		lods[i1].skinIndices = new uint[lods[i1].numActiveSkins];
		for (var i2 = 0; i2 < lods[i1].skinIndices.Count; i2++) {
			Nif.NifStream(out lods[i1].skinIndices[i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numLodLevels = (uint)lods.Count;
	numSkins = (uint)skins.Count;
	numBones = (uint)bones.Count;
	Nif.NifStream(currentLod, s, info);
	Nif.NifStream(numBones, s, info);
	for (var i1 = 0; i1 < bones.Count; i1++) {
		WriteRef((NiObject)bones[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numSkins, s, info);
	for (var i1 = 0; i1 < skins.Count; i1++) {
		WriteRef((NiObject)skins[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numLodLevels, s, info);
	for (var i1 = 0; i1 < lods.Count; i1++) {
		lods[i1].numActiveSkins = (uint)lods[i1].skinIndices.Count;
		Nif.NifStream(lods[i1].numBones, s, info);
		Nif.NifStream(lods[i1].numActiveSkins, s, info);
		for (var i2 = 0; i2 < lods[i1].skinIndices.Count; i2++) {
			Nif.NifStream(lods[i1].skinIndices[i2], s, info);
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
	numLodLevels = (uint)lods.Count;
	numSkins = (uint)skins.Count;
	numBones = (uint)bones.Count;
	s.AppendLine($"  Current LOD:  {currentLod}");
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
	s.AppendLine($"  Num Skins:  {numSkins}");
	array_output_count = 0;
	for (var i1 = 0; i1 < skins.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Skins[{i1}]:  {skins[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num LOD Levels:  {numLodLevels}");
	array_output_count = 0;
	for (var i1 = 0; i1 < lods.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		lods[i1].numActiveSkins = (uint)lods[i1].skinIndices.Count;
		s.AppendLine($"    Num Bones:  {lods[i1].numBones}");
		s.AppendLine($"    Num Active Skins:  {lods[i1].numActiveSkins}");
		array_output_count = 0;
		for (var i2 = 0; i2 < lods[i1].skinIndices.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Skin Indices[{i2}]:  {lods[i1].skinIndices[i2]}");
			array_output_count++;
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < bones.Count; i1++) {
		bones[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < skins.Count; i1++) {
		skins[i1] = FixLink<NiMesh>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < bones.Count; i1++) {
		if (bones[i1] != null)
			refs.Add((NiObject)bones[i1]);
	}
	for (var i1 = 0; i1 < skins.Count; i1++) {
		if (skins[i1] != null)
			refs.Add((NiObject)skins[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < bones.Count; i1++) {
	}
	for (var i1 = 0; i1 < skins.Count; i1++) {
	}
	return ptrs;
}


}

}