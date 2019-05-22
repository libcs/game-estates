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

/*! Fallout 4 Item Slot Parent */
public class BSConnectPoint__Parents : NiExtraData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSConnectPoint::Parents", NiExtraData.TYPE);
	/*!  */
	internal uint numConnectPoints;
	/*!  */
	internal IList<BSConnectPoint> connectPoints;

	public BSConnectPoint__Parents() {
	numConnectPoints = (uint)0;
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
public static NiObject Create() => new BSConnectPoint__Parents();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numConnectPoints, s, info);
	connectPoints = new BSConnectPoint[numConnectPoints];
	for (var i1 = 0; i1 < connectPoints.Count; i1++) {
		Nif.NifStream(out connectPoints[i1].parent, s, info);
		Nif.NifStream(out connectPoints[i1].name, s, info);
		Nif.NifStream(out connectPoints[i1].rotation, s, info);
		Nif.NifStream(out connectPoints[i1].translation, s, info);
		Nif.NifStream(out connectPoints[i1].scale, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numConnectPoints = (uint)connectPoints.Count;
	Nif.NifStream(numConnectPoints, s, info);
	for (var i1 = 0; i1 < connectPoints.Count; i1++) {
		Nif.NifStream(connectPoints[i1].parent, s, info);
		Nif.NifStream(connectPoints[i1].name, s, info);
		Nif.NifStream(connectPoints[i1].rotation, s, info);
		Nif.NifStream(connectPoints[i1].translation, s, info);
		Nif.NifStream(connectPoints[i1].scale, s, info);
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
	numConnectPoints = (uint)connectPoints.Count;
	s.AppendLine($"  Num Connect Points:  {numConnectPoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < connectPoints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Parent:  {connectPoints[i1].parent}");
		s.AppendLine($"    Name:  {connectPoints[i1].name}");
		s.AppendLine($"    Rotation:  {connectPoints[i1].rotation}");
		s.AppendLine($"    Translation:  {connectPoints[i1].translation}");
		s.AppendLine($"    Scale:  {connectPoints[i1].scale}");
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