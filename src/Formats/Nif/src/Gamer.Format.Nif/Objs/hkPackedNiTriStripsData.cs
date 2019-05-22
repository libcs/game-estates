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

/*! NiTriStripsData for havok data? */
public class hkPackedNiTriStripsData : bhkShapeCollection {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("hkPackedNiTriStripsData", bhkShapeCollection.TYPE);
	/*!  */
	internal uint numTriangles;
	/*!  */
	internal IList<TriangleData> triangles;
	/*!  */
	internal uint numVertices;
	/*! Unknown. */
	internal byte unknownByte1;
	/*!  */
	internal IList<Vector3> vertices;
	/*! Number of subparts. */
	internal ushort numSubShapes;
	/*! The subparts. */
	internal IList<OblivionSubShape> subShapes;

	public hkPackedNiTriStripsData() {
	numTriangles = (uint)0;
	numVertices = (uint)0;
	unknownByte1 = (byte)0;
	numSubShapes = (ushort)0;
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
public static NiObject Create() => new hkPackedNiTriStripsData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numTriangles, s, info);
	triangles = new TriangleData[numTriangles];
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		Nif.NifStream(out triangles[i1].triangle, s, info);
		Nif.NifStream(out triangles[i1].weldingInfo, s, info);
		if (info.version <= 0x14000005) {
			Nif.NifStream(out triangles[i1].normal, s, info);
		}
	}
	Nif.NifStream(out numVertices, s, info);
	if (info.version >= 0x14020007) {
		Nif.NifStream(out unknownByte1, s, info);
	}
	vertices = new Vector3[numVertices];
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		Nif.NifStream(out vertices[i1], s, info);
	}
	if (info.version >= 0x14020007) {
		Nif.NifStream(out numSubShapes, s, info);
		subShapes = new OblivionSubShape[numSubShapes];
		for (var i2 = 0; i2 < subShapes.Count; i2++) {
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(out subShapes[i2].havokFilter.layer_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(out subShapes[i2].havokFilter.layer_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(out subShapes[i2].havokFilter.layer_sk, s, info);
			}
			Nif.NifStream(out subShapes[i2].havokFilter.flagsAndPartNumber, s, info);
			Nif.NifStream(out subShapes[i2].havokFilter.group, s, info);
			Nif.NifStream(out subShapes[i2].numVertices, s, info);
			if (info.version <= 0x0A000102) {
				Nif.NifStream(out subShapes[i2].material.unknownInt, s, info);
			}
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(out subShapes[i2].material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(out subShapes[i2].material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(out subShapes[i2].material.material_sk, s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSubShapes = (ushort)subShapes.Count;
	numVertices = (uint)vertices.Count;
	numTriangles = (uint)triangles.Count;
	Nif.NifStream(numTriangles, s, info);
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		Nif.NifStream(triangles[i1].triangle, s, info);
		Nif.NifStream(triangles[i1].weldingInfo, s, info);
		if (info.version <= 0x14000005) {
			Nif.NifStream(triangles[i1].normal, s, info);
		}
	}
	Nif.NifStream(numVertices, s, info);
	if (info.version >= 0x14020007) {
		Nif.NifStream(unknownByte1, s, info);
	}
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		Nif.NifStream(vertices[i1], s, info);
	}
	if (info.version >= 0x14020007) {
		Nif.NifStream(numSubShapes, s, info);
		for (var i2 = 0; i2 < subShapes.Count; i2++) {
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(subShapes[i2].havokFilter.layer_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(subShapes[i2].havokFilter.layer_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(subShapes[i2].havokFilter.layer_sk, s, info);
			}
			Nif.NifStream(subShapes[i2].havokFilter.flagsAndPartNumber, s, info);
			Nif.NifStream(subShapes[i2].havokFilter.group, s, info);
			Nif.NifStream(subShapes[i2].numVertices, s, info);
			if (info.version <= 0x0A000102) {
				Nif.NifStream(subShapes[i2].material.unknownInt, s, info);
			}
			if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
				Nif.NifStream(subShapes[i2].material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
				Nif.NifStream(subShapes[i2].material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
				Nif.NifStream(subShapes[i2].material.material_sk, s, info);
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
	numSubShapes = (ushort)subShapes.Count;
	numVertices = (uint)vertices.Count;
	numTriangles = (uint)triangles.Count;
	s.AppendLine($"  Num Triangles:  {numTriangles}");
	array_output_count = 0;
	for (var i1 = 0; i1 < triangles.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Triangle:  {triangles[i1].triangle}");
		s.AppendLine($"    Welding Info:  {triangles[i1].weldingInfo}");
		s.AppendLine($"    Normal:  {triangles[i1].normal}");
	}
	s.AppendLine($"  Num Vertices:  {numVertices}");
	s.AppendLine($"  Unknown Byte 1:  {unknownByte1}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Vertices[{i1}]:  {vertices[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Sub Shapes:  {numSubShapes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Layer:  {subShapes[i1].havokFilter.layer_ob}");
		s.AppendLine($"    Layer:  {subShapes[i1].havokFilter.layer_fo}");
		s.AppendLine($"    Layer:  {subShapes[i1].havokFilter.layer_sk}");
		s.AppendLine($"    Flags and Part Number:  {subShapes[i1].havokFilter.flagsAndPartNumber}");
		s.AppendLine($"    Group:  {subShapes[i1].havokFilter.group}");
		s.AppendLine($"    Num Vertices:  {subShapes[i1].numVertices}");
		s.AppendLine($"    Unknown Int:  {subShapes[i1].material.unknownInt}");
		s.AppendLine($"    Material:  {subShapes[i1].material.material_ob}");
		s.AppendLine($"    Material:  {subShapes[i1].material.material_fo}");
		s.AppendLine($"    Material:  {subShapes[i1].material.material_sk}");
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