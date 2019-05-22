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

/*! Holds mesh data using strips of triangles. */
public class NiTriStripsData : NiTriBasedGeomData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiTriStripsData", NiTriBasedGeomData.TYPE);
	/*! Number of OpenGL triangle strips that are present. */
	internal ushort numStrips;
	/*! The number of points in each triangle strip. */
	internal IList<ushort> stripLengths;
	/*! Do we have strip point data? */
	internal bool hasPoints;
	/*!
	 * The points in the Triangle strips.  Size is the sum of all entries in Strip
	 * Lengths.
	 */
	internal IList<ushort[]> points;

	public NiTriStripsData() {
	numStrips = (ushort)0;
	hasPoints = false;
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
public static NiObject Create() => new NiTriStripsData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numStrips, s, info);
	stripLengths = new ushort[numStrips];
	for (var i1 = 0; i1 < stripLengths.Count; i1++) {
		Nif.NifStream(out stripLengths[i1], s, info);
	}
	if (info.version >= 0x0A000103) {
		Nif.NifStream(out hasPoints, s, info);
	}
	if (info.version <= 0x0A000102) {
		points = new ushort[numStrips];
		for (var i2 = 0; i2 < points.Count; i2++) {
			points[i2].Resize(stripLengths[i2]);
			for (var i3 = 0; i3 < stripLengths[i2]; i3++) {
				Nif.NifStream(out points[i2][i3], s, info);
			}
		}
	}
	if (info.version >= 0x0A000103) {
		if (hasPoints) {
			points = new ushort[numStrips];
			for (var i3 = 0; i3 < points.Count; i3++) {
				points[i3].Resize(stripLengths[i3]);
				for (var i4 = 0; i4 < stripLengths[i3]; i4++) {
					Nif.NifStream(out (ushort)points[i3][i4], s, info);
				}
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	for (var i1 = 0; i1 < points.Count; i1++)
		stripLengths[i1] = (ushort)points[i1].Count;
	numStrips = (ushort)stripLengths.Count;
	Nif.NifStream(numStrips, s, info);
	for (var i1 = 0; i1 < stripLengths.Count; i1++) {
		Nif.NifStream(stripLengths[i1], s, info);
	}
	if (info.version >= 0x0A000103) {
		Nif.NifStream(hasPoints, s, info);
	}
	if (info.version <= 0x0A000102) {
		for (var i2 = 0; i2 < points.Count; i2++) {
			for (var i3 = 0; i3 < stripLengths[i2]; i3++) {
				Nif.NifStream(points[i2][i3], s, info);
			}
		}
	}
	if (info.version >= 0x0A000103) {
		if (hasPoints) {
			for (var i3 = 0; i3 < points.Count; i3++) {
				for (var i4 = 0; i4 < stripLengths[i3]; i4++) {
					Nif.NifStream((ushort)points[i3][i4], s, info);
				}
			}
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
	for (var i1 = 0; i1 < points.Count; i1++)
		stripLengths[i1] = (ushort)points[i1].Count;
	numStrips = (ushort)stripLengths.Count;
	s.AppendLine($"  Num Strips:  {numStrips}");
	array_output_count = 0;
	for (var i1 = 0; i1 < stripLengths.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Strip Lengths[{i1}]:  {stripLengths[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Has Points:  {hasPoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < points.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < stripLengths[i1]; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Points[{i2}]:  {points[i1][i2]}");
			array_output_count++;
		}
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