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

/*!  */
public class NiBSplineCompTransformEvaluator : NiBSplineTransformEvaluator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBSplineCompTransformEvaluator", NiBSplineTransformEvaluator.TYPE);
	/*!  */
	internal float translationOffset;
	/*!  */
	internal float translationHalfRange;
	/*!  */
	internal float rotationOffset;
	/*!  */
	internal float rotationHalfRange;
	/*!  */
	internal float scaleOffset;
	/*!  */
	internal float scaleHalfRange;

	public NiBSplineCompTransformEvaluator() {
	translationOffset = 3.402823466e+38f;
	translationHalfRange = 3.402823466e+38f;
	rotationOffset = 3.402823466e+38f;
	rotationHalfRange = 3.402823466e+38f;
	scaleOffset = 3.402823466e+38f;
	scaleHalfRange = 3.402823466e+38f;
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
public static NiObject Create() => new NiBSplineCompTransformEvaluator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out translationOffset, s, info);
	Nif.NifStream(out translationHalfRange, s, info);
	Nif.NifStream(out rotationOffset, s, info);
	Nif.NifStream(out rotationHalfRange, s, info);
	Nif.NifStream(out scaleOffset, s, info);
	Nif.NifStream(out scaleHalfRange, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(translationOffset, s, info);
	Nif.NifStream(translationHalfRange, s, info);
	Nif.NifStream(rotationOffset, s, info);
	Nif.NifStream(rotationHalfRange, s, info);
	Nif.NifStream(scaleOffset, s, info);
	Nif.NifStream(scaleHalfRange, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Translation Offset:  {translationOffset}");
	s.AppendLine($"  Translation Half Range:  {translationHalfRange}");
	s.AppendLine($"  Rotation Offset:  {rotationOffset}");
	s.AppendLine($"  Rotation Half Range:  {rotationHalfRange}");
	s.AppendLine($"  Scale Offset:  {scaleOffset}");
	s.AppendLine($"  Scale Half Range:  {scaleHalfRange}");
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