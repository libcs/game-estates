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

/*! Used to make an object follow a predefined spline path. */
public class NiPathInterpolator : NiKeyBasedInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPathInterpolator", NiKeyBasedInterpolator.TYPE);
	/*!  */
	internal PathFlags flags;
	/*! -1 = Negative, 1 = Positive */
	internal int bankDir;
	/*! Max angle in radians. */
	internal float maxBankAngle;
	/*!  */
	internal float smoothing;
	/*! 0, 1, or 2 representing X, Y, or Z. */
	internal short followAxis;
	/*!  */
	internal NiPosData pathData;
	/*!  */
	internal NiFloatData percentData;

	public NiPathInterpolator() {
	flags = (PathFlags)3;
	bankDir = (int)1;
	maxBankAngle = 0.0f;
	smoothing = 0.0f;
	followAxis = (short)0;
	pathData = null;
	percentData = null;
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
public static NiObject Create() => new NiPathInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out bankDir, s, info);
	Nif.NifStream(out maxBankAngle, s, info);
	Nif.NifStream(out smoothing, s, info);
	Nif.NifStream(out followAxis, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(flags, s, info);
	Nif.NifStream(bankDir, s, info);
	Nif.NifStream(maxBankAngle, s, info);
	Nif.NifStream(smoothing, s, info);
	Nif.NifStream(followAxis, s, info);
	WriteRef((NiObject)pathData, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)percentData, s, info, link_map, missing_link_stack);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Bank Dir:  {bankDir}");
	s.AppendLine($"  Max Bank Angle:  {maxBankAngle}");
	s.AppendLine($"  Smoothing:  {smoothing}");
	s.AppendLine($"  Follow Axis:  {followAxis}");
	s.AppendLine($"  Path Data:  {pathData}");
	s.AppendLine($"  Percent Data:  {percentData}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	pathData = FixLink<NiPosData>(objects, link_stack, missing_link_stack, info);
	percentData = FixLink<NiFloatData>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (pathData != null)
		refs.Add((NiObject)pathData);
	if (percentData != null)
		refs.Add((NiObject)percentData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}