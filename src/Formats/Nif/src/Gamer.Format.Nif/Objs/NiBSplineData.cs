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
 * Contains one or more sets of control points for use in interpolation of open,
 * uniform B-Splines, stored as either float or compact.
 */
public class NiBSplineData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineData", NiObject.TYPE);
	/*!  */
	internal uint numFloatControlPoints;
	/*! Float values representing the control data. */
	internal IList<float> floatControlPoints;
	/*!  */
	internal uint numCompactControlPoints;
	/*! Signed shorts representing the data from 0 to 1 (scaled by SHRT_MAX). */
	internal IList<short> compactControlPoints;

	public NiBSplineData() {
	numFloatControlPoints = (uint)0;
	numCompactControlPoints = (uint)0;
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
public static NiObject Create() => new NiBSplineData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numFloatControlPoints, s, info);
	floatControlPoints = new float[numFloatControlPoints];
	for (var i1 = 0; i1 < floatControlPoints.Count; i1++) {
		Nif.NifStream(out floatControlPoints[i1], s, info);
	}
	Nif.NifStream(out numCompactControlPoints, s, info);
	compactControlPoints = new short[numCompactControlPoints];
	for (var i1 = 0; i1 < compactControlPoints.Count; i1++) {
		Nif.NifStream(out compactControlPoints[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numCompactControlPoints = (uint)compactControlPoints.Count;
	numFloatControlPoints = (uint)floatControlPoints.Count;
	Nif.NifStream(numFloatControlPoints, s, info);
	for (var i1 = 0; i1 < floatControlPoints.Count; i1++) {
		Nif.NifStream(floatControlPoints[i1], s, info);
	}
	Nif.NifStream(numCompactControlPoints, s, info);
	for (var i1 = 0; i1 < compactControlPoints.Count; i1++) {
		Nif.NifStream(compactControlPoints[i1], s, info);
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
	numCompactControlPoints = (uint)compactControlPoints.Count;
	numFloatControlPoints = (uint)floatControlPoints.Count;
	s.AppendLine($"  Num Float Control Points:  {numFloatControlPoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < floatControlPoints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Float Control Points[{i1}]:  {floatControlPoints[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Compact Control Points:  {numCompactControlPoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < compactControlPoints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Compact Control Points[{i1}]:  {compactControlPoints[i1]}");
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