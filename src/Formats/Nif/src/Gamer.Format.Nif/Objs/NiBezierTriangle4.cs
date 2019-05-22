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
 * LEGACY (pre-10.1)
 *         Sub data of NiBezierMesh
 */
public class NiBezierTriangle4 : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBezierTriangle4", NiObject.TYPE);
	/*! unknown */
	internal Array6<uint> unknown1;
	/*! unknown */
	internal ushort unknown2;
	/*! unknown */
	internal Matrix33 matrix;
	/*! unknown */
	internal Vector3 vector1;
	/*! unknown */
	internal Vector3 vector2;
	/*! unknown */
	internal Array4<short> unknown3;
	/*! unknown */
	internal byte unknown4;
	/*! unknown */
	internal uint unknown5;
	/*! unknown */
	internal Array24<short> unknown6;

	public NiBezierTriangle4() {
	unknown2 = (ushort)0;
	unknown4 = (byte)0;
	unknown5 = (uint)0;
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
public static NiObject Create() => new NiBezierTriangle4();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	for (var i1 = 0; i1 < 6; i1++) {
		Nif.NifStream(out unknown1[i1], s, info);
	}
	Nif.NifStream(out unknown2, s, info);
	Nif.NifStream(out matrix, s, info);
	Nif.NifStream(out vector1, s, info);
	Nif.NifStream(out vector2, s, info);
	for (var i1 = 0; i1 < 4; i1++) {
		Nif.NifStream(out unknown3[i1], s, info);
	}
	Nif.NifStream(out unknown4, s, info);
	Nif.NifStream(out unknown5, s, info);
	for (var i1 = 0; i1 < 24; i1++) {
		Nif.NifStream(out unknown6[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	for (var i1 = 0; i1 < 6; i1++) {
		Nif.NifStream(unknown1[i1], s, info);
	}
	Nif.NifStream(unknown2, s, info);
	Nif.NifStream(matrix, s, info);
	Nif.NifStream(vector1, s, info);
	Nif.NifStream(vector2, s, info);
	for (var i1 = 0; i1 < 4; i1++) {
		Nif.NifStream(unknown3[i1], s, info);
	}
	Nif.NifStream(unknown4, s, info);
	Nif.NifStream(unknown5, s, info);
	for (var i1 = 0; i1 < 24; i1++) {
		Nif.NifStream(unknown6[i1], s, info);
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
	array_output_count = 0;
	for (var i1 = 0; i1 < 6; i1++) {
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
	s.AppendLine($"  Unknown 2:  {unknown2}");
	s.AppendLine($"  Matrix:  {matrix}");
	s.AppendLine($"  Vector 1:  {vector1}");
	s.AppendLine($"  Vector 2:  {vector2}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 4; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown 3[{i1}]:  {unknown3[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown 4:  {unknown4}");
	s.AppendLine($"  Unknown 5:  {unknown5}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 24; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown 6[{i1}]:  {unknown6[i1]}");
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