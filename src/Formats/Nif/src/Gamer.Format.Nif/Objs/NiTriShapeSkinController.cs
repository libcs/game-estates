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

/*! Old version of skinning instance. */
public class NiTriShapeSkinController : NiTimeController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiTriShapeSkinController", NiTimeController.TYPE);
	/*! The number of node bones referenced as influences. */
	internal uint numBones;
	/*! The number of vertex weights stored for each bone. */
	internal IList<uint> vertexCounts;
	/*! List of all armature bones. */
	internal IList<NiBone> bones;
	/*! Contains skin weight data for each node that this skin is influenced by. */
	internal IList<OldSkinData[]> boneData;

	public NiTriShapeSkinController() {
	numBones = (uint)0;
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
public static NiObject Create() => new NiTriShapeSkinController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numBones, s, info);
	vertexCounts = new uint[numBones];
	for (var i1 = 0; i1 < vertexCounts.Count; i1++) {
		Nif.NifStream(out vertexCounts[i1], s, info);
	}
	bones = new *[numBones];
	for (var i1 = 0; i1 < bones.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	boneData = new OldSkinData[numBones];
	for (var i1 = 0; i1 < boneData.Count; i1++) {
		boneData[i1].Resize(vertexCounts[i1]);
		for (var i2 = 0; i2 < vertexCounts[i1]; i2++) {
			Nif.NifStream(out boneData[i1][i2].vertexWeight, s, info);
			Nif.NifStream(out boneData[i1][i2].vertexIndex, s, info);
			Nif.NifStream(out boneData[i1][i2].unknownVector, s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	for (var i1 = 0; i1 < boneData.Count; i1++)
		vertexCounts[i1] = (uint)boneData[i1].Count;
	numBones = (uint)vertexCounts.Count;
	Nif.NifStream(numBones, s, info);
	for (var i1 = 0; i1 < vertexCounts.Count; i1++) {
		Nif.NifStream(vertexCounts[i1], s, info);
	}
	for (var i1 = 0; i1 < bones.Count; i1++) {
		WriteRef((NiObject)bones[i1], s, info, link_map, missing_link_stack);
	}
	for (var i1 = 0; i1 < boneData.Count; i1++) {
		for (var i2 = 0; i2 < vertexCounts[i1]; i2++) {
			Nif.NifStream(boneData[i1][i2].vertexWeight, s, info);
			Nif.NifStream(boneData[i1][i2].vertexIndex, s, info);
			Nif.NifStream(boneData[i1][i2].unknownVector, s, info);
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
	for (var i1 = 0; i1 < boneData.Count; i1++)
		vertexCounts[i1] = (uint)boneData[i1].Count;
	numBones = (uint)vertexCounts.Count;
	s.AppendLine($"  Num Bones:  {numBones}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vertexCounts.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Vertex Counts[{i1}]:  {vertexCounts[i1]}");
		array_output_count++;
	}
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
	array_output_count = 0;
	for (var i1 = 0; i1 < boneData.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < vertexCounts[i1]; i2++) {
			s.AppendLine($"      Vertex Weight:  {boneData[i1][i2].vertexWeight}");
			s.AppendLine($"      Vertex Index:  {boneData[i1][i2].vertexIndex}");
			s.AppendLine($"      Unknown Vector:  {boneData[i1][i2].unknownVector}");
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < bones.Count; i1++) {
		bones[i1] = FixLink<NiBone>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < bones.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < bones.Count; i1++) {
		if (bones[i1] != null)
			ptrs.Add((NiObject)bones[i1]);
	}
	return ptrs;
}


}

}