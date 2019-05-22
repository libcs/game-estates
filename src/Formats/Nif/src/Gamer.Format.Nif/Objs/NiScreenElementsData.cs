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
 * DEPRECATED (20.5), functionality included in NiMeshScreenElements.
 *         Two dimensional screen elements.
 */
public class NiScreenElementsData : NiTriShapeData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiScreenElementsData", NiTriShapeData.TYPE);
	/*!  */
	internal ushort maxPolygons;
	/*!  */
	internal IList<Polygon> polygons;
	/*!  */
	internal IList<ushort> polygonIndices;
	/*!  */
	internal ushort polygonGrowBy;
	/*!  */
	internal ushort numPolygons;
	/*!  */
	internal ushort maxVertices;
	/*!  */
	internal ushort verticesGrowBy;
	/*!  */
	internal ushort maxIndices;
	/*!  */
	internal ushort indicesGrowBy;

	public NiScreenElementsData() {
	maxPolygons = (ushort)0;
	polygonGrowBy = (ushort)1;
	numPolygons = (ushort)0;
	maxVertices = (ushort)0;
	verticesGrowBy = (ushort)1;
	maxIndices = (ushort)0;
	indicesGrowBy = (ushort)1;
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
public static NiObject Create() => new NiScreenElementsData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out maxPolygons, s, info);
	polygons = new Polygon[maxPolygons];
	for (var i1 = 0; i1 < polygons.Count; i1++) {
		Nif.NifStream(out polygons[i1].numVertices, s, info);
		Nif.NifStream(out polygons[i1].vertexOffset, s, info);
		Nif.NifStream(out polygons[i1].numTriangles, s, info);
		Nif.NifStream(out polygons[i1].triangleOffset, s, info);
	}
	polygonIndices = new ushort[maxPolygons];
	for (var i1 = 0; i1 < polygonIndices.Count; i1++) {
		Nif.NifStream(out polygonIndices[i1], s, info);
	}
	Nif.NifStream(out polygonGrowBy, s, info);
	Nif.NifStream(out numPolygons, s, info);
	Nif.NifStream(out maxVertices, s, info);
	Nif.NifStream(out verticesGrowBy, s, info);
	Nif.NifStream(out maxIndices, s, info);
	Nif.NifStream(out indicesGrowBy, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	maxPolygons = (ushort)polygons.Count;
	Nif.NifStream(maxPolygons, s, info);
	for (var i1 = 0; i1 < polygons.Count; i1++) {
		Nif.NifStream(polygons[i1].numVertices, s, info);
		Nif.NifStream(polygons[i1].vertexOffset, s, info);
		Nif.NifStream(polygons[i1].numTriangles, s, info);
		Nif.NifStream(polygons[i1].triangleOffset, s, info);
	}
	for (var i1 = 0; i1 < polygonIndices.Count; i1++) {
		Nif.NifStream(polygonIndices[i1], s, info);
	}
	Nif.NifStream(polygonGrowBy, s, info);
	Nif.NifStream(numPolygons, s, info);
	Nif.NifStream(maxVertices, s, info);
	Nif.NifStream(verticesGrowBy, s, info);
	Nif.NifStream(maxIndices, s, info);
	Nif.NifStream(indicesGrowBy, s, info);

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
	maxPolygons = (ushort)polygons.Count;
	s.AppendLine($"  Max Polygons:  {maxPolygons}");
	array_output_count = 0;
	for (var i1 = 0; i1 < polygons.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Num Vertices:  {polygons[i1].numVertices}");
		s.AppendLine($"    Vertex Offset:  {polygons[i1].vertexOffset}");
		s.AppendLine($"    Num Triangles:  {polygons[i1].numTriangles}");
		s.AppendLine($"    Triangle Offset:  {polygons[i1].triangleOffset}");
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < polygonIndices.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Polygon Indices[{i1}]:  {polygonIndices[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Polygon Grow By:  {polygonGrowBy}");
	s.AppendLine($"  Num Polygons:  {numPolygons}");
	s.AppendLine($"  Max Vertices:  {maxVertices}");
	s.AppendLine($"  Vertices Grow By:  {verticesGrowBy}");
	s.AppendLine($"  Max Indices:  {maxIndices}");
	s.AppendLine($"  Indices Grow By:  {indicesGrowBy}");
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