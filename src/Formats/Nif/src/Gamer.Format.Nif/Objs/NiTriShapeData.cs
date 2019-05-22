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

/*! Holds mesh data using a list of singular triangles. */
public class NiTriShapeData : NiTriBasedGeomData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiTriShapeData", NiTriBasedGeomData.TYPE);
	/*! Num Triangles times 3. */
	internal uint numTrianglePoints;
	/*! Do we have triangle data? */
	internal bool hasTriangles;
	/*! Triangle data. */
	internal IList<Triangle> triangles;
	/*! Number of shared normals groups. */
	internal ushort numMatchGroups;
	/*! The shared normals. */
	internal IList<MatchGroup> matchGroups;

	public NiTriShapeData() {
	numTrianglePoints = (uint)0;
	hasTriangles = false;
	numMatchGroups = (ushort)0;
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
public static NiObject Create() => new NiTriShapeData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numTrianglePoints, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(out hasTriangles, s, info);
	}
	if (info.version <= 0x0A000102) {
		triangles = new Triangle[numTriangles];
		for (var i2 = 0; i2 < triangles.Count; i2++) {
			Nif.NifStream(out triangles[i2], s, info);
		}
	}
	if (info.version >= 0x0A000103) {
		if (hasTriangles) {
			triangles = new Triangle[numTriangles];
			for (var i3 = 0; i3 < triangles.Count; i3++) {
				Nif.NifStream(out (Triangle)triangles[i3], s, info);
			}
		}
	}
	if (info.version >= 0x03010000) {
		Nif.NifStream(out numMatchGroups, s, info);
		matchGroups = new MatchGroup[numMatchGroups];
		for (var i2 = 0; i2 < matchGroups.Count; i2++) {
			Nif.NifStream(out matchGroups[i2].numVertices, s, info);
			matchGroups[i2].vertexIndices = new ushort[matchGroups[i2].numVertices];
			for (var i3 = 0; i3 < matchGroups[i2].vertexIndices.Count; i3++) {
				Nif.NifStream(out matchGroups[i2].vertexIndices[i3], s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numMatchGroups = (ushort)matchGroups.Count;
	hasTriangles = hasTrianglesCalc(info);
	Nif.NifStream(numTrianglePoints, s, info);
	if (info.version >= 0x0A010000) {
		Nif.NifStream(hasTriangles, s, info);
	}
	if (info.version <= 0x0A000102) {
		for (var i2 = 0; i2 < triangles.Count; i2++) {
			Nif.NifStream(triangles[i2], s, info);
		}
	}
	if (info.version >= 0x0A000103) {
		if (hasTriangles) {
			for (var i3 = 0; i3 < triangles.Count; i3++) {
				Nif.NifStream((Triangle)triangles[i3], s, info);
			}
		}
	}
	if (info.version >= 0x03010000) {
		Nif.NifStream(numMatchGroups, s, info);
		for (var i2 = 0; i2 < matchGroups.Count; i2++) {
			matchGroups[i2].numVertices = (ushort)matchGroups[i2].vertexIndices.Count;
			Nif.NifStream(matchGroups[i2].numVertices, s, info);
			for (var i3 = 0; i3 < matchGroups[i2].vertexIndices.Count; i3++) {
				Nif.NifStream(matchGroups[i2].vertexIndices[i3], s, info);
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
	numMatchGroups = (ushort)matchGroups.Count;
	s.AppendLine($"  Num Triangle Points:  {numTrianglePoints}");
	s.AppendLine($"  Has Triangles:  {hasTriangles}");
	array_output_count = 0;
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Triangles[{i1}]:  {triangles[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Match Groups:  {numMatchGroups}");
	array_output_count = 0;
	for (var i1 = 0; i1 < matchGroups.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		matchGroups[i1].numVertices = (ushort)matchGroups[i1].vertexIndices.Count;
		s.AppendLine($"    Num Vertices:  {matchGroups[i1].numVertices}");
		array_output_count = 0;
		for (var i2 = 0; i2 < matchGroups[i1].vertexIndices.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Indices[{i2}]:  {matchGroups[i1].vertexIndices[i2]}");
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