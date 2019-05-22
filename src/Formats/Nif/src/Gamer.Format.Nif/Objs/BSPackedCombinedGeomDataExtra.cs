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
 * Fallout 4 Packed Combined Geometry Data.
 *         Geometry is baked into the file and given a list of transforms to
 * position each copy.
 */
public class BSPackedCombinedGeomDataExtra : NiExtraData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSPackedCombinedGeomDataExtra", NiExtraData.TYPE);
	/*!  */
	internal BSVertexDesc vertexDesc;
	/*!  */
	internal uint numVertices;
	/*!  */
	internal uint numTriangles;
	/*! Unknown. */
	internal uint unknownFlags1;
	/*! Unknown. */
	internal uint unknownFlags2;
	/*!  */
	internal uint numData;
	/*!  */
	internal IList<BSPackedGeomData> objectData;

	public BSPackedCombinedGeomDataExtra() {
	numVertices = (uint)0;
	numTriangles = (uint)0;
	unknownFlags1 = (uint)0;
	unknownFlags2 = (uint)0;
	numData = (uint)0;
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
public static NiObject Create() => new BSPackedCombinedGeomDataExtra();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out vertexDesc.vf1, s, info);
	Nif.NifStream(out vertexDesc.vf2, s, info);
	Nif.NifStream(out vertexDesc.vf3, s, info);
	Nif.NifStream(out vertexDesc.vf4, s, info);
	Nif.NifStream(out vertexDesc.vf5, s, info);
	Nif.NifStream(out vertexDesc.vertexAttributes, s, info);
	Nif.NifStream(out vertexDesc.vf8, s, info);
	Nif.NifStream(out numVertices, s, info);
	Nif.NifStream(out numTriangles, s, info);
	Nif.NifStream(out unknownFlags1, s, info);
	Nif.NifStream(out unknownFlags2, s, info);
	Nif.NifStream(out numData, s, info);
	objectData = new BSPackedGeomData[numData];
	for (var i1 = 0; i1 < objectData.Count; i1++) {
		Nif.NifStream(out objectData[i1].numVerts, s, info);
		Nif.NifStream(out objectData[i1].lodLevels, s, info);
		Nif.NifStream(out objectData[i1].triCountLod0, s, info);
		Nif.NifStream(out objectData[i1].triOffsetLod0, s, info);
		Nif.NifStream(out objectData[i1].triCountLod1, s, info);
		Nif.NifStream(out objectData[i1].triOffsetLod1, s, info);
		Nif.NifStream(out objectData[i1].triCountLod2, s, info);
		Nif.NifStream(out objectData[i1].triOffsetLod2, s, info);
		Nif.NifStream(out objectData[i1].numCombined, s, info);
		objectData[i1].combined = new BSPackedGeomDataCombined[objectData[i1].numCombined];
		for (var i2 = 0; i2 < objectData[i1].combined.Count; i2++) {
			Nif.NifStream(out objectData[i1].combined[i2].grayscaleToPaletteScale, s, info);
			Nif.NifStream(out objectData[i1].combined[i2].transform.rotation, s, info);
			Nif.NifStream(out objectData[i1].combined[i2].transform.translation, s, info);
			Nif.NifStream(out objectData[i1].combined[i2].transform.scale, s, info);
			Nif.NifStream(out objectData[i1].combined[i2].boundingSphere.center, s, info);
			Nif.NifStream(out objectData[i1].combined[i2].boundingSphere.radius, s, info);
		}
		Nif.NifStream(out objectData[i1].vertexDesc.vf1, s, info);
		Nif.NifStream(out objectData[i1].vertexDesc.vf2, s, info);
		Nif.NifStream(out objectData[i1].vertexDesc.vf3, s, info);
		Nif.NifStream(out objectData[i1].vertexDesc.vf4, s, info);
		Nif.NifStream(out objectData[i1].vertexDesc.vf5, s, info);
		Nif.NifStream(out objectData[i1].vertexDesc.vertexAttributes, s, info);
		Nif.NifStream(out objectData[i1].vertexDesc.vf8, s, info);
		objectData[i1].vertexData = new BSVertexData[objectData[i1].numVerts];
		for (var i2 = 0; i2 < objectData[i1].vertexData.Count; i2++) {
			Nif.NifStream(out objectData[i1].vertexData[i2], s, info, objectData[i1].vertexDesc.vertexAttributes);
		}
		objectData[i1].triangles = new Triangle[(objectData[i1].triCountLod0 + (objectData[i1].triCountLod1 + objectData[i1].triCountLod2))];
		for (var i2 = 0; i2 < objectData[i1].triangles.Count; i2++) {
			Nif.NifStream(out objectData[i1].triangles[i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numData = (uint)objectData.Count;
	Nif.NifStream(vertexDesc.vf1, s, info);
	Nif.NifStream(vertexDesc.vf2, s, info);
	Nif.NifStream(vertexDesc.vf3, s, info);
	Nif.NifStream(vertexDesc.vf4, s, info);
	Nif.NifStream(vertexDesc.vf5, s, info);
	Nif.NifStream(vertexDesc.vertexAttributes, s, info);
	Nif.NifStream(vertexDesc.vf8, s, info);
	Nif.NifStream(numVertices, s, info);
	Nif.NifStream(numTriangles, s, info);
	Nif.NifStream(unknownFlags1, s, info);
	Nif.NifStream(unknownFlags2, s, info);
	Nif.NifStream(numData, s, info);
	for (var i1 = 0; i1 < objectData.Count; i1++) {
		objectData[i1].numCombined = (uint)objectData[i1].combined.Count;
		objectData[i1].numVerts = (uint)objectData[i1].vertexData.Count;
		Nif.NifStream(objectData[i1].numVerts, s, info);
		Nif.NifStream(objectData[i1].lodLevels, s, info);
		Nif.NifStream(objectData[i1].triCountLod0, s, info);
		Nif.NifStream(objectData[i1].triOffsetLod0, s, info);
		Nif.NifStream(objectData[i1].triCountLod1, s, info);
		Nif.NifStream(objectData[i1].triOffsetLod1, s, info);
		Nif.NifStream(objectData[i1].triCountLod2, s, info);
		Nif.NifStream(objectData[i1].triOffsetLod2, s, info);
		Nif.NifStream(objectData[i1].numCombined, s, info);
		for (var i2 = 0; i2 < objectData[i1].combined.Count; i2++) {
			Nif.NifStream(objectData[i1].combined[i2].grayscaleToPaletteScale, s, info);
			Nif.NifStream(objectData[i1].combined[i2].transform.rotation, s, info);
			Nif.NifStream(objectData[i1].combined[i2].transform.translation, s, info);
			Nif.NifStream(objectData[i1].combined[i2].transform.scale, s, info);
			Nif.NifStream(objectData[i1].combined[i2].boundingSphere.center, s, info);
			Nif.NifStream(objectData[i1].combined[i2].boundingSphere.radius, s, info);
		}
		Nif.NifStream(objectData[i1].vertexDesc.vf1, s, info);
		Nif.NifStream(objectData[i1].vertexDesc.vf2, s, info);
		Nif.NifStream(objectData[i1].vertexDesc.vf3, s, info);
		Nif.NifStream(objectData[i1].vertexDesc.vf4, s, info);
		Nif.NifStream(objectData[i1].vertexDesc.vf5, s, info);
		Nif.NifStream(objectData[i1].vertexDesc.vertexAttributes, s, info);
		Nif.NifStream(objectData[i1].vertexDesc.vf8, s, info);
		for (var i2 = 0; i2 < objectData[i1].vertexData.Count; i2++) {
			Nif.NifStream(objectData[i1].vertexData[i2], s, info, objectData[i1].vertexDesc.vertexAttributes);
		}
		for (var i2 = 0; i2 < objectData[i1].triangles.Count; i2++) {
			Nif.NifStream(objectData[i1].triangles[i2], s, info);
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
	numData = (uint)objectData.Count;
	s.AppendLine($"  VF1:  {vertexDesc.vf1}");
	s.AppendLine($"  VF2:  {vertexDesc.vf2}");
	s.AppendLine($"  VF3:  {vertexDesc.vf3}");
	s.AppendLine($"  VF4:  {vertexDesc.vf4}");
	s.AppendLine($"  VF5:  {vertexDesc.vf5}");
	s.AppendLine($"  Vertex Attributes:  {vertexDesc.vertexAttributes}");
	s.AppendLine($"  VF8:  {vertexDesc.vf8}");
	s.AppendLine($"  Num Vertices:  {numVertices}");
	s.AppendLine($"  Num Triangles:  {numTriangles}");
	s.AppendLine($"  Unknown Flags 1:  {unknownFlags1}");
	s.AppendLine($"  Unknown Flags 2:  {unknownFlags2}");
	s.AppendLine($"  Num Data:  {numData}");
	array_output_count = 0;
	for (var i1 = 0; i1 < objectData.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		objectData[i1].numCombined = (uint)objectData[i1].combined.Count;
		objectData[i1].numVerts = (uint)objectData[i1].vertexData.Count;
		s.AppendLine($"    Num Verts:  {objectData[i1].numVerts}");
		s.AppendLine($"    LOD Levels:  {objectData[i1].lodLevels}");
		s.AppendLine($"    Tri Count LOD0:  {objectData[i1].triCountLod0}");
		s.AppendLine($"    Tri Offset LOD0:  {objectData[i1].triOffsetLod0}");
		s.AppendLine($"    Tri Count LOD1:  {objectData[i1].triCountLod1}");
		s.AppendLine($"    Tri Offset LOD1:  {objectData[i1].triOffsetLod1}");
		s.AppendLine($"    Tri Count LOD2:  {objectData[i1].triCountLod2}");
		s.AppendLine($"    Tri Offset LOD2:  {objectData[i1].triOffsetLod2}");
		s.AppendLine($"    Num Combined:  {objectData[i1].numCombined}");
		array_output_count = 0;
		for (var i2 = 0; i2 < objectData[i1].combined.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      Grayscale to Palette Scale:  {objectData[i1].combined[i2].grayscaleToPaletteScale}");
			s.AppendLine($"      Rotation:  {objectData[i1].combined[i2].transform.rotation}");
			s.AppendLine($"      Translation:  {objectData[i1].combined[i2].transform.translation}");
			s.AppendLine($"      Scale:  {objectData[i1].combined[i2].transform.scale}");
			s.AppendLine($"      Center:  {objectData[i1].combined[i2].boundingSphere.center}");
			s.AppendLine($"      Radius:  {objectData[i1].combined[i2].boundingSphere.radius}");
		}
		s.AppendLine($"    VF1:  {objectData[i1].vertexDesc.vf1}");
		s.AppendLine($"    VF2:  {objectData[i1].vertexDesc.vf2}");
		s.AppendLine($"    VF3:  {objectData[i1].vertexDesc.vf3}");
		s.AppendLine($"    VF4:  {objectData[i1].vertexDesc.vf4}");
		s.AppendLine($"    VF5:  {objectData[i1].vertexDesc.vf5}");
		s.AppendLine($"    Vertex Attributes:  {objectData[i1].vertexDesc.vertexAttributes}");
		s.AppendLine($"    VF8:  {objectData[i1].vertexDesc.vf8}");
		array_output_count = 0;
		for (var i2 = 0; i2 < objectData[i1].vertexData.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Data[{i2}]:  {objectData[i1].vertexData[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < objectData[i1].triangles.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Triangles[{i2}]:  {objectData[i1].triangles[i2]}");
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