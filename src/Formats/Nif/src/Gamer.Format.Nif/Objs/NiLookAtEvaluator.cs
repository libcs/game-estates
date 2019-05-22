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
public class NiLookAtEvaluator : NiEvaluator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiLookAtEvaluator", NiEvaluator.TYPE);
	/*!  */
	internal LookAtFlags flags;
	/*!  */
	internal IndexString lookAtName;
	/*!  */
	internal IndexString drivenName;
	/*!  */
	internal NiPoint3Interpolator interpolator_Translation;
	/*!  */
	internal NiFloatInterpolator interpolator_Roll;
	/*!  */
	internal NiFloatInterpolator interpolator_Scale;

	public NiLookAtEvaluator() {
	flags = (LookAtFlags)0;
	interpolator_Translation = null;
	interpolator_Roll = null;
	interpolator_Scale = null;
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
public static NiObject Create() => new NiLookAtEvaluator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out lookAtName, s, info);
	Nif.NifStream(out drivenName, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(flags, s, info);
	Nif.NifStream(lookAtName, s, info);
	Nif.NifStream(drivenName, s, info);
	WriteRef((NiObject)interpolator_Translation, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator_Roll, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)interpolator_Scale, s, info, link_map, missing_link_stack);

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
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Look At Name:  {lookAtName}");
	s.AppendLine($"  Driven Name:  {drivenName}");
	s.AppendLine($"  Interpolator: Translation:  {interpolator_Translation}");
	s.AppendLine($"  Interpolator: Roll:  {interpolator_Roll}");
	s.AppendLine($"  Interpolator: Scale:  {interpolator_Scale}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	interpolator_Translation = FixLink<NiPoint3Interpolator>(objects, link_stack, missing_link_stack, info);
	interpolator_Roll = FixLink<NiFloatInterpolator>(objects, link_stack, missing_link_stack, info);
	interpolator_Scale = FixLink<NiFloatInterpolator>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (interpolator_Translation != null)
		refs.Add((NiObject)interpolator_Translation);
	if (interpolator_Roll != null)
		refs.Add((NiObject)interpolator_Roll);
	if (interpolator_Scale != null)
		refs.Add((NiObject)interpolator_Scale);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}