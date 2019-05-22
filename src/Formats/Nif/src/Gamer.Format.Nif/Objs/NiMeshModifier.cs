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

/*! Base class for mesh modifiers. */
public class NiMeshModifier : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiMeshModifier", NiObject.TYPE);
	/*!  */
	internal uint numSubmitPoints;
	/*! The sync points supported by this mesh modifier for SubmitTasks. */
	internal IList<SyncPoint> submitPoints;
	/*!  */
	internal uint numCompletePoints;
	/*! The sync points supported by this mesh modifier for CompleteTasks. */
	internal IList<SyncPoint> completePoints;

	public NiMeshModifier() {
	numSubmitPoints = (uint)0;
	numCompletePoints = (uint)0;
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
public static NiObject Create() => new NiMeshModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numSubmitPoints, s, info);
	submitPoints = new SyncPoint[numSubmitPoints];
	for (var i1 = 0; i1 < submitPoints.Count; i1++) {
		Nif.NifStream(out submitPoints[i1], s, info);
	}
	Nif.NifStream(out numCompletePoints, s, info);
	completePoints = new SyncPoint[numCompletePoints];
	for (var i1 = 0; i1 < completePoints.Count; i1++) {
		Nif.NifStream(out completePoints[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numCompletePoints = (uint)completePoints.Count;
	numSubmitPoints = (uint)submitPoints.Count;
	Nif.NifStream(numSubmitPoints, s, info);
	for (var i1 = 0; i1 < submitPoints.Count; i1++) {
		Nif.NifStream(submitPoints[i1], s, info);
	}
	Nif.NifStream(numCompletePoints, s, info);
	for (var i1 = 0; i1 < completePoints.Count; i1++) {
		Nif.NifStream(completePoints[i1], s, info);
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
	numCompletePoints = (uint)completePoints.Count;
	numSubmitPoints = (uint)submitPoints.Count;
	s.AppendLine($"  Num Submit Points:  {numSubmitPoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < submitPoints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Submit Points[{i1}]:  {submitPoints[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Complete Points:  {numCompletePoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < completePoints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Complete Points[{i1}]:  {completePoints[i1]}");
		array_output_count++;
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