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

/*! Unknown. */
public class Ni3dsAlphaAnimator : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("Ni3dsAlphaAnimator", NiObject.TYPE);
	/*! Unknown. */
	internal Array40<byte> unknown1;
	/*! The parent? */
	internal NiObject parent;
	/*! Unknown. */
	internal uint num1;
	/*! Unknown. */
	internal uint num2;
	/*! Unknown. */
	internal IList<uint[]> unknown2;

	public Ni3dsAlphaAnimator() {
	parent = null;
	num1 = (uint)0;
	num2 = (uint)0;
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
public static NiObject Create() => new Ni3dsAlphaAnimator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	for (var i1 = 0; i1 < 40; i1++) {
		Nif.NifStream(out unknown1[i1], s, info);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out num1, s, info);
	Nif.NifStream(out num2, s, info);
	unknown2 = new uint[num1];
	for (var i1 = 0; i1 < unknown2.Count; i1++) {
		unknown2[i1].Resize(num2);
		for (var i2 = 0; i2 < unknown2[i1].Count; i2++) {
			Nif.NifStream(out unknown2[i1][i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	num2 = (uint)((unknown2.Count > 0) ? unknown2[0].Count : 0);
	num1 = (uint)unknown2.Count;
	for (var i1 = 0; i1 < 40; i1++) {
		Nif.NifStream(unknown1[i1], s, info);
	}
	WriteRef((NiObject)parent, s, info, link_map, missing_link_stack);
	Nif.NifStream(num1, s, info);
	Nif.NifStream(num2, s, info);
	for (var i1 = 0; i1 < unknown2.Count; i1++) {
		for (var i2 = 0; i2 < unknown2[i1].Count; i2++) {
			Nif.NifStream(unknown2[i1][i2], s, info);
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
	num2 = (uint)((unknown2.Count > 0) ? unknown2[0].Count : 0);
	num1 = (uint)unknown2.Count;
	array_output_count = 0;
	for (var i1 = 0; i1 < 40; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown 1[{i1}]:  {unknown1[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Parent:  {parent}");
	s.AppendLine($"  Num 1:  {num1}");
	s.AppendLine($"  Num 2:  {num2}");
	array_output_count = 0;
	for (var i1 = 0; i1 < unknown2.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < unknown2[i1].Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Unknown 2[{i2}]:  {unknown2[i1][i2]}");
			array_output_count++;
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	parent = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (parent != null)
		refs.Add((NiObject)parent);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}