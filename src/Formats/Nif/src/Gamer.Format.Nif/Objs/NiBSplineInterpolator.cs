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

/*! Abstract base class for interpolators storing data via a B-spline. */
public class NiBSplineInterpolator : NiInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineInterpolator", NiInterpolator.TYPE);
	/*! Animation start time. */
	internal float startTime;
	/*! Animation stop time. */
	internal float stopTime;
	/*!  */
	internal NiBSplineData splineData;
	/*!  */
	internal NiBSplineBasisData basisData;

	public NiBSplineInterpolator() {
	startTime = 3.402823466e+38f;
	stopTime = -3.402823466e+38f;
	splineData = null;
	basisData = null;
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
public static NiObject Create() => new NiBSplineInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out startTime, s, info);
	Nif.NifStream(out stopTime, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(startTime, s, info);
	Nif.NifStream(stopTime, s, info);
	WriteRef((NiObject)splineData, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)basisData, s, info, link_map, missing_link_stack);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Start Time:  {startTime}");
	s.AppendLine($"  Stop Time:  {stopTime}");
	s.AppendLine($"  Spline Data:  {splineData}");
	s.AppendLine($"  Basis Data:  {basisData}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	splineData = FixLink<NiBSplineData>(objects, link_stack, missing_link_stack, info);
	basisData = FixLink<NiBSplineBasisData>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (splineData != null)
		refs.Add((NiObject)splineData);
	if (basisData != null)
		refs.Add((NiObject)basisData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}