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

/*! Fallout 4 Sub-Index Tri Shape */
public class BSSubIndexTriShape : BSTriShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSSubIndexTriShape", BSTriShape.TYPE);
	/*!  */
	internal uint numPrimitives;
	/*!  */
	internal uint numSegments;
	/*!  */
	internal uint totalSegments;
	/*!  */
	internal IList<BSGeometrySegmentData> segment;
	/*!  */
	internal BSGeometrySegmentSharedData segmentData;

	public BSSubIndexTriShape() {
	numPrimitives = (uint)0;
	numSegments = (uint)0;
	totalSegments = (uint)0;
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
public static NiObject Create() => new BSSubIndexTriShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if (info.userVersion2 == 130) {
		if ((dataSize > 0)) {
			Nif.NifStream(out numPrimitives, s, info);
			Nif.NifStream(out numSegments, s, info);
			Nif.NifStream(out totalSegments, s, info);
			segment = new BSGeometrySegmentData[numSegments];
			for (var i3 = 0; i3 < segment.Count; i3++) {
				if ((info.userVersion2 < 130)) {
					Nif.NifStream(out segment[i3].flags, s, info);
					Nif.NifStream(out segment[i3].index, s, info);
					Nif.NifStream(out segment[i3].numTrisInSegment, s, info);
				}
				if (info.userVersion2 == 130) {
					Nif.NifStream(out segment[i3].startIndex, s, info);
					Nif.NifStream(out segment[i3].numPrimitives, s, info);
					Nif.NifStream(out segment[i3].parentArrayIndex, s, info);
					Nif.NifStream(out segment[i3].numSubSegments, s, info);
					segment[i3].subSegment = new BSGeometrySubSegment[segment[i3].numSubSegments];
					for (var i5 = 0; i5 < segment[i3].subSegment.Count; i5++) {
						Nif.NifStream(out segment[i3].subSegment[i5].startIndex, s, info);
						Nif.NifStream(out segment[i3].subSegment[i5].numPrimitives, s, info);
						Nif.NifStream(out segment[i3].subSegment[i5].parentArrayIndex, s, info);
						Nif.NifStream(out segment[i3].subSegment[i5].unused, s, info);
					}
				}
			}
		}
		if (((numSegments < totalSegments) && (dataSize > 0))) {
			Nif.NifStream(out segmentData.numSegments, s, info);
			Nif.NifStream(out segmentData.totalSegments, s, info);
			segmentData.segmentStarts = new uint[segmentData.numSegments];
			for (var i3 = 0; i3 < segmentData.segmentStarts.Count; i3++) {
				Nif.NifStream(out segmentData.segmentStarts[i3], s, info);
			}
			segmentData.perSegmentData = new BSGeometryPerSegmentSharedData[segmentData.totalSegments];
			for (var i3 = 0; i3 < segmentData.perSegmentData.Count; i3++) {
				Nif.NifStream(out segmentData.perSegmentData[i3].userIndex, s, info);
				Nif.NifStream(out segmentData.perSegmentData[i3].boneId, s, info);
				Nif.NifStream(out segmentData.perSegmentData[i3].numCutOffsets, s, info);
				segmentData.perSegmentData[i3].cutOffsets = new float[segmentData.perSegmentData[i3].numCutOffsets];
				for (var i4 = 0; i4 < segmentData.perSegmentData[i3].cutOffsets.Count; i4++) {
					Nif.NifStream(out segmentData.perSegmentData[i3].cutOffsets[i4], s, info);
				}
			}
			Nif.NifStream(out segmentData.ssfLength, s, info);
			segmentData.ssfFile = new byte[segmentData.ssfLength];
			for (var i3 = 0; i3 < segmentData.ssfFile.Count; i3++) {
				Nif.NifStream(out segmentData.ssfFile[i3], s, info);
			}
		}
	}
	if (info.userVersion2 == 100) {
		Nif.NifStream(out (uint)numSegments, s, info);
		segment = new BSGeometrySegmentData[numSegments];
		for (var i2 = 0; i2 < segment.Count; i2++) {
			if ((info.userVersion2 < 130)) {
				Nif.NifStream(out segment[i2].flags, s, info);
				Nif.NifStream(out segment[i2].index, s, info);
				Nif.NifStream(out segment[i2].numTrisInSegment, s, info);
			}
			if (info.userVersion2 == 130) {
				Nif.NifStream(out segment[i2].startIndex, s, info);
				Nif.NifStream(out segment[i2].numPrimitives, s, info);
				Nif.NifStream(out segment[i2].parentArrayIndex, s, info);
				Nif.NifStream(out segment[i2].numSubSegments, s, info);
				segment[i2].subSegment = new BSGeometrySubSegment[segment[i2].numSubSegments];
				for (var i4 = 0; i4 < segment[i2].subSegment.Count; i4++) {
					Nif.NifStream(out segment[i2].subSegment[i4].startIndex, s, info);
					Nif.NifStream(out segment[i2].subSegment[i4].numPrimitives, s, info);
					Nif.NifStream(out segment[i2].subSegment[i4].parentArrayIndex, s, info);
					Nif.NifStream(out segment[i2].subSegment[i4].unused, s, info);
				}
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSegments = (uint)segment.Count;
	if (info.userVersion2 == 130) {
		if ((dataSize > 0)) {
			Nif.NifStream(numPrimitives, s, info);
			Nif.NifStream(numSegments, s, info);
			Nif.NifStream(totalSegments, s, info);
			for (var i3 = 0; i3 < segment.Count; i3++) {
				segment[i3].numSubSegments = (uint)segment[i3].subSegment.Count;
				if ((info.userVersion2 < 130)) {
					Nif.NifStream(segment[i3].flags, s, info);
					Nif.NifStream(segment[i3].index, s, info);
					Nif.NifStream(segment[i3].numTrisInSegment, s, info);
				}
				if (info.userVersion2 == 130) {
					Nif.NifStream(segment[i3].startIndex, s, info);
					Nif.NifStream(segment[i3].numPrimitives, s, info);
					Nif.NifStream(segment[i3].parentArrayIndex, s, info);
					Nif.NifStream(segment[i3].numSubSegments, s, info);
					for (var i5 = 0; i5 < segment[i3].subSegment.Count; i5++) {
						Nif.NifStream(segment[i3].subSegment[i5].startIndex, s, info);
						Nif.NifStream(segment[i3].subSegment[i5].numPrimitives, s, info);
						Nif.NifStream(segment[i3].subSegment[i5].parentArrayIndex, s, info);
						Nif.NifStream(segment[i3].subSegment[i5].unused, s, info);
					}
				}
			}
		}
		if (((numSegments < totalSegments) && (dataSize > 0))) {
			segmentData.ssfLength = (ushort)segmentData.ssfFile.Count;
			segmentData.totalSegments = (uint)segmentData.perSegmentData.Count;
			segmentData.numSegments = (uint)segmentData.segmentStarts.Count;
			Nif.NifStream(segmentData.numSegments, s, info);
			Nif.NifStream(segmentData.totalSegments, s, info);
			for (var i3 = 0; i3 < segmentData.segmentStarts.Count; i3++) {
				Nif.NifStream(segmentData.segmentStarts[i3], s, info);
			}
			for (var i3 = 0; i3 < segmentData.perSegmentData.Count; i3++) {
				segmentData.perSegmentData[i3].numCutOffsets = (uint)segmentData.perSegmentData[i3].cutOffsets.Count;
				Nif.NifStream(segmentData.perSegmentData[i3].userIndex, s, info);
				Nif.NifStream(segmentData.perSegmentData[i3].boneId, s, info);
				Nif.NifStream(segmentData.perSegmentData[i3].numCutOffsets, s, info);
				for (var i4 = 0; i4 < segmentData.perSegmentData[i3].cutOffsets.Count; i4++) {
					Nif.NifStream(segmentData.perSegmentData[i3].cutOffsets[i4], s, info);
				}
			}
			Nif.NifStream(segmentData.ssfLength, s, info);
			for (var i3 = 0; i3 < segmentData.ssfFile.Count; i3++) {
				Nif.NifStream(segmentData.ssfFile[i3], s, info);
			}
		}
	}
	if (info.userVersion2 == 100) {
		Nif.NifStream((uint)numSegments, s, info);
		for (var i2 = 0; i2 < segment.Count; i2++) {
			segment[i2].numSubSegments = (uint)segment[i2].subSegment.Count;
			if ((info.userVersion2 < 130)) {
				Nif.NifStream(segment[i2].flags, s, info);
				Nif.NifStream(segment[i2].index, s, info);
				Nif.NifStream(segment[i2].numTrisInSegment, s, info);
			}
			if (info.userVersion2 == 130) {
				Nif.NifStream(segment[i2].startIndex, s, info);
				Nif.NifStream(segment[i2].numPrimitives, s, info);
				Nif.NifStream(segment[i2].parentArrayIndex, s, info);
				Nif.NifStream(segment[i2].numSubSegments, s, info);
				for (var i4 = 0; i4 < segment[i2].subSegment.Count; i4++) {
					Nif.NifStream(segment[i2].subSegment[i4].startIndex, s, info);
					Nif.NifStream(segment[i2].subSegment[i4].numPrimitives, s, info);
					Nif.NifStream(segment[i2].subSegment[i4].parentArrayIndex, s, info);
					Nif.NifStream(segment[i2].subSegment[i4].unused, s, info);
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
	numSegments = (uint)segment.Count;
	if ((dataSize > 0)) {
		s.AppendLine($"    Num Primitives:  {numPrimitives}");
		s.AppendLine($"    Num Segments:  {numSegments}");
		s.AppendLine($"    Total Segments:  {totalSegments}");
		array_output_count = 0;
		for (var i2 = 0; i2 < segment.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			segment[i2].numSubSegments = (uint)segment[i2].subSegment.Count;
			s.AppendLine($"      Flags:  {segment[i2].flags}");
			s.AppendLine($"      Index:  {segment[i2].index}");
			s.AppendLine($"      Num Tris in Segment:  {segment[i2].numTrisInSegment}");
			s.AppendLine($"      Start Index:  {segment[i2].startIndex}");
			s.AppendLine($"      Num Primitives:  {segment[i2].numPrimitives}");
			s.AppendLine($"      Parent Array Index:  {segment[i2].parentArrayIndex}");
			s.AppendLine($"      Num Sub Segments:  {segment[i2].numSubSegments}");
			array_output_count = 0;
			for (var i3 = 0; i3 < segment[i2].subSegment.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Start Index:  {segment[i2].subSegment[i3].startIndex}");
				s.AppendLine($"        Num Primitives:  {segment[i2].subSegment[i3].numPrimitives}");
				s.AppendLine($"        Parent Array Index:  {segment[i2].subSegment[i3].parentArrayIndex}");
				s.AppendLine($"        Unused:  {segment[i2].subSegment[i3].unused}");
			}
		}
	}
	if (((numSegments < totalSegments) && (dataSize > 0))) {
		segmentData.ssfLength = (ushort)segmentData.ssfFile.Count;
		segmentData.totalSegments = (uint)segmentData.perSegmentData.Count;
		segmentData.numSegments = (uint)segmentData.segmentStarts.Count;
		s.AppendLine($"    Num Segments:  {segmentData.numSegments}");
		s.AppendLine($"    Total Segments:  {segmentData.totalSegments}");
		array_output_count = 0;
		for (var i2 = 0; i2 < segmentData.segmentStarts.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Segment Starts[{i2}]:  {segmentData.segmentStarts[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < segmentData.perSegmentData.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			segmentData.perSegmentData[i2].numCutOffsets = (uint)segmentData.perSegmentData[i2].cutOffsets.Count;
			s.AppendLine($"      User Index:  {segmentData.perSegmentData[i2].userIndex}");
			s.AppendLine($"      Bone ID:  {segmentData.perSegmentData[i2].boneId}");
			s.AppendLine($"      Num Cut Offsets:  {segmentData.perSegmentData[i2].numCutOffsets}");
			array_output_count = 0;
			for (var i3 = 0; i3 < segmentData.perSegmentData[i2].cutOffsets.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Cut Offsets[{i3}]:  {segmentData.perSegmentData[i2].cutOffsets[i3]}");
				array_output_count++;
			}
		}
		s.AppendLine($"    SSF Length:  {segmentData.ssfLength}");
		array_output_count = 0;
		for (var i2 = 0; i2 < segmentData.ssfFile.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      SSF File[{i2}]:  {segmentData.ssfFile[i2]}");
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