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

/*! Bethesda-specific AV object. */
public class BSSegmentedTriShape : NiTriShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSSegmentedTriShape", NiTriShape.TYPE);
	/*! Number of segments in the square grid */
	internal int numSegments;
	/*! Configuration of each segment */
	internal IList<BSGeometrySegmentData> segment;

	public BSSegmentedTriShape() {
	numSegments = (int)0;
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
public static NiObject Create() => new BSSegmentedTriShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numSegments, s, info);
	segment = new BSGeometrySegmentData[numSegments];
	for (var i1 = 0; i1 < segment.Count; i1++) {
		if ((info.userVersion2 < 130)) {
			Nif.NifStream(out segment[i1].flags, s, info);
			Nif.NifStream(out segment[i1].index, s, info);
			Nif.NifStream(out segment[i1].numTrisInSegment, s, info);
		}
		if (info.userVersion2 == 130) {
			Nif.NifStream(out segment[i1].startIndex, s, info);
			Nif.NifStream(out segment[i1].numPrimitives, s, info);
			Nif.NifStream(out segment[i1].parentArrayIndex, s, info);
			Nif.NifStream(out segment[i1].numSubSegments, s, info);
			segment[i1].subSegment = new BSGeometrySubSegment[segment[i1].numSubSegments];
			for (var i3 = 0; i3 < segment[i1].subSegment.Count; i3++) {
				Nif.NifStream(out segment[i1].subSegment[i3].startIndex, s, info);
				Nif.NifStream(out segment[i1].subSegment[i3].numPrimitives, s, info);
				Nif.NifStream(out segment[i1].subSegment[i3].parentArrayIndex, s, info);
				Nif.NifStream(out segment[i1].subSegment[i3].unused, s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSegments = (int)segment.Count;
	Nif.NifStream(numSegments, s, info);
	for (var i1 = 0; i1 < segment.Count; i1++) {
		segment[i1].numSubSegments = (uint)segment[i1].subSegment.Count;
		if ((info.userVersion2 < 130)) {
			Nif.NifStream(segment[i1].flags, s, info);
			Nif.NifStream(segment[i1].index, s, info);
			Nif.NifStream(segment[i1].numTrisInSegment, s, info);
		}
		if (info.userVersion2 == 130) {
			Nif.NifStream(segment[i1].startIndex, s, info);
			Nif.NifStream(segment[i1].numPrimitives, s, info);
			Nif.NifStream(segment[i1].parentArrayIndex, s, info);
			Nif.NifStream(segment[i1].numSubSegments, s, info);
			for (var i3 = 0; i3 < segment[i1].subSegment.Count; i3++) {
				Nif.NifStream(segment[i1].subSegment[i3].startIndex, s, info);
				Nif.NifStream(segment[i1].subSegment[i3].numPrimitives, s, info);
				Nif.NifStream(segment[i1].subSegment[i3].parentArrayIndex, s, info);
				Nif.NifStream(segment[i1].subSegment[i3].unused, s, info);
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
	numSegments = (int)segment.Count;
	s.AppendLine($"  Num Segments:  {numSegments}");
	array_output_count = 0;
	for (var i1 = 0; i1 < segment.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		segment[i1].numSubSegments = (uint)segment[i1].subSegment.Count;
		s.AppendLine($"    Flags:  {segment[i1].flags}");
		s.AppendLine($"    Index:  {segment[i1].index}");
		s.AppendLine($"    Num Tris in Segment:  {segment[i1].numTrisInSegment}");
		s.AppendLine($"    Start Index:  {segment[i1].startIndex}");
		s.AppendLine($"    Num Primitives:  {segment[i1].numPrimitives}");
		s.AppendLine($"    Parent Array Index:  {segment[i1].parentArrayIndex}");
		s.AppendLine($"    Num Sub Segments:  {segment[i1].numSubSegments}");
		array_output_count = 0;
		for (var i2 = 0; i2 < segment[i1].subSegment.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      Start Index:  {segment[i1].subSegment[i2].startIndex}");
			s.AppendLine($"      Num Primitives:  {segment[i1].subSegment[i2].numPrimitives}");
			s.AppendLine($"      Parent Array Index:  {segment[i1].subSegment[i2].parentArrayIndex}");
			s.AppendLine($"      Unused:  {segment[i1].subSegment[i2].unused}");
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