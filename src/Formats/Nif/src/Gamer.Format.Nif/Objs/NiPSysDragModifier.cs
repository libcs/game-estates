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

/*! Particle modifier that applies a linear drag force to particles. */
public class NiPSysDragModifier : NiPSysModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysDragModifier", NiPSysModifier.TYPE);
	/*! The object whose position and orientation are the basis of the force. */
	internal NiAVObject dragObject;
	/*! The local direction of the force. */
	internal Vector3 dragAxis;
	/*! The amount of drag to apply to particles. */
	internal float percentage;
	/*! The distance up to which particles are fully affected. */
	internal float range;
	/*! The distance at which particles cease to be affected. */
	internal float rangeFalloff;

	public NiPSysDragModifier() {
	dragObject = null;
	dragAxis = 1.0, 0.0, 0.0;
	percentage = 0.05f;
	range = 3.402823466e+38f;
	rangeFalloff = 3.402823466e+38f;
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
public static NiObject Create() => new NiPSysDragModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out dragAxis, s, info);
	Nif.NifStream(out percentage, s, info);
	Nif.NifStream(out range, s, info);
	Nif.NifStream(out rangeFalloff, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	WriteRef((NiObject)dragObject, s, info, link_map, missing_link_stack);
	Nif.NifStream(dragAxis, s, info);
	Nif.NifStream(percentage, s, info);
	Nif.NifStream(range, s, info);
	Nif.NifStream(rangeFalloff, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Drag Object:  {dragObject}");
	s.AppendLine($"  Drag Axis:  {dragAxis}");
	s.AppendLine($"  Percentage:  {percentage}");
	s.AppendLine($"  Range:  {range}");
	s.AppendLine($"  Range Falloff:  {rangeFalloff}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	dragObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (dragObject != null)
		ptrs.Add((NiObject)dragObject);
	return ptrs;
}


}

}