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

/*! Bethesda-Specific Havok serializable. */
public class bhkOrientHingedBodyAction : bhkSerializable {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkOrientHingedBodyAction", bhkSerializable.TYPE);
	/*!  */
	internal bhkRigidBody body;
	/*! Unknown. */
	internal uint unknownInt1;
	/*! Unknown. */
	internal uint unknownInt2;
	/*!  */
	internal Array8<byte> unused1;
	/*!  */
	internal Vector4 hingeAxisLs;
	/*!  */
	internal Vector4 forwardLs;
	/*!  */
	internal float strength;
	/*!  */
	internal float damping;
	/*!  */
	internal Array8<byte> unused2;

	public bhkOrientHingedBodyAction() {
	body = null;
	unknownInt1 = (uint)0;
	unknownInt2 = (uint)0;
	strength = 0.0f;
	damping = 0.0f;
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
public static NiObject Create() => new bhkOrientHingedBodyAction();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out unknownInt1, s, info);
	Nif.NifStream(out unknownInt2, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(out unused1[i1], s, info);
	}
	Nif.NifStream(out hingeAxisLs, s, info);
	Nif.NifStream(out forwardLs, s, info);
	Nif.NifStream(out strength, s, info);
	Nif.NifStream(out damping, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(out unused2[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	WriteRef((NiObject)body, s, info, link_map, missing_link_stack);
	Nif.NifStream(unknownInt1, s, info);
	Nif.NifStream(unknownInt2, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(unused1[i1], s, info);
	}
	Nif.NifStream(hingeAxisLs, s, info);
	Nif.NifStream(forwardLs, s, info);
	Nif.NifStream(strength, s, info);
	Nif.NifStream(damping, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(unused2[i1], s, info);
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
	s.AppendLine($"  Body:  {body}");
	s.AppendLine($"  Unknown Int 1:  {unknownInt1}");
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 8; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused 1[{i1}]:  {unused1[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Hinge Axis LS:  {hingeAxisLs}");
	s.AppendLine($"  Forward LS:  {forwardLs}");
	s.AppendLine($"  Strength:  {strength}");
	s.AppendLine($"  Damping:  {damping}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 8; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused 2[{i1}]:  {unused2[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	body = FixLink<bhkRigidBody>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (body != null)
		ptrs.Add((NiObject)body);
	return ptrs;
}


}

}