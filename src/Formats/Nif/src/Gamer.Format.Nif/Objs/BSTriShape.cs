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

/*! Fallout 4 Tri Shape */
public class BSTriShape : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSTriShape", NiAVObject.TYPE);
	/*!  */
	internal NiBound boundingSphere;
	/*!  */
	internal NiObject skin;
	/*!  */
	internal BSShaderProperty shaderProperty;
	/*!  */
	internal NiAlphaProperty alphaProperty;
	/*!  */
	internal BSVertexDesc vertexDesc;
	/*!  */
	internal uint numTriangles;
	/*!  */
	internal ushort numVertices;
	/*!  */
	internal uint dataSize;
	/*!  */
	internal IList<BSVertexData> vertexData;
	/*!  */
	internal IList<Triangle> triangles;
	/*!  */
	internal uint particleDataSize;
	/*!  */
	internal IList<Vector3> vertices;
	/*!  */
	internal IList<Triangle> trianglesCopy;

	public BSTriShape() {
	skin = null;
	shaderProperty = null;
	alphaProperty = null;
	numTriangles = (uint)0;
	numVertices = (ushort)0;
	dataSize = (uint)0;
	particleDataSize = (uint)0;
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
public static NiObject Create() => new BSTriShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out boundingSphere.center, s, info);
	Nif.NifStream(out boundingSphere.radius, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out vertexDesc.vf1, s, info);
	Nif.NifStream(out vertexDesc.vf2, s, info);
	Nif.NifStream(out vertexDesc.vf3, s, info);
	Nif.NifStream(out vertexDesc.vf4, s, info);
	Nif.NifStream(out vertexDesc.vf5, s, info);
	Nif.NifStream(out vertexDesc.vertexAttributes, s, info);
	Nif.NifStream(out vertexDesc.vf8, s, info);
	if (info.userVersion2 == 130) {
		Nif.NifStream(out numTriangles, s, info);
	}
	if ((info.userVersion2 < 130)) {
		Nif.NifStream(out (ushort)numTriangles, s, info);
	}
	Nif.NifStream(out numVertices, s, info);
	Nif.NifStream(out dataSize, s, info);
	if (info.userVersion2 == 130) {
		if ((dataSize > 0)) {
			vertexData = new BSVertexData[numVertices];
			for (var i3 = 0; i3 < vertexData.Count; i3++) {
				Nif.NifStream(out vertexData[i3], s, info, vertexDesc.vertexAttributes);
			}
		}
	}
	if (info.userVersion2 == 100) {
		if ((dataSize > 0)) {
			vertexData = new BSVertexData[numVertices];
			for (var i3 = 0; i3 < vertexData.Count; i3++) {
				Nif.NifStream(out vertexData[i3], s, info, vertexDesc.vertexAttributes);
			}
		}
	}
	if ((dataSize > 0)) {
		triangles = new Triangle[numTriangles];
		for (var i2 = 0; i2 < triangles.Count; i2++) {
			Nif.NifStream(out triangles[i2], s, info);
		}
	}
	if (info.userVersion2 == 100) {
		Nif.NifStream(out particleDataSize, s, info);
		if ((particleDataSize > 0)) {
			vertices = new Vector3[numVertices];
			for (var i3 = 0; i3 < vertices.Count; i3++) {
				Nif.NifStream(out vertices[i3], s, info);
			}
			trianglesCopy = new Triangle[numTriangles];
			for (var i3 = 0; i3 < trianglesCopy.Count; i3++) {
				Nif.NifStream(out trianglesCopy[i3], s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numVertices = (ushort)vertexData.Count;
	numTriangles = (uint)triangles.Count;
	Nif.NifStream(boundingSphere.center, s, info);
	Nif.NifStream(boundingSphere.radius, s, info);
	WriteRef((NiObject)skin, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)shaderProperty, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)alphaProperty, s, info, link_map, missing_link_stack);
	Nif.NifStream(vertexDesc.vf1, s, info);
	Nif.NifStream(vertexDesc.vf2, s, info);
	Nif.NifStream(vertexDesc.vf3, s, info);
	Nif.NifStream(vertexDesc.vf4, s, info);
	Nif.NifStream(vertexDesc.vf5, s, info);
	Nif.NifStream(vertexDesc.vertexAttributes, s, info);
	Nif.NifStream(vertexDesc.vf8, s, info);
	if (info.userVersion2 == 130) {
		Nif.NifStream(numTriangles, s, info);
	}
	if ((info.userVersion2 < 130)) {
		Nif.NifStream((ushort)numTriangles, s, info);
	}
	Nif.NifStream(numVertices, s, info);
	Nif.NifStream(dataSize, s, info);
	if (info.userVersion2 == 130) {
		if ((dataSize > 0)) {
			for (var i3 = 0; i3 < vertexData.Count; i3++) {
				Nif.NifStream(vertexData[i3], s, info, vertexDesc.vertexAttributes);
			}
		}
	}
	if (info.userVersion2 == 100) {
		if ((dataSize > 0)) {
			for (var i3 = 0; i3 < vertexData.Count; i3++) {
				Nif.NifStream(vertexData[i3], s, info, vertexDesc.vertexAttributes);
			}
		}
	}
	if ((dataSize > 0)) {
		for (var i2 = 0; i2 < triangles.Count; i2++) {
			Nif.NifStream(triangles[i2], s, info);
		}
	}
	if (info.userVersion2 == 100) {
		Nif.NifStream(particleDataSize, s, info);
		if ((particleDataSize > 0)) {
			for (var i3 = 0; i3 < vertices.Count; i3++) {
				Nif.NifStream(vertices[i3], s, info);
			}
			for (var i3 = 0; i3 < trianglesCopy.Count; i3++) {
				Nif.NifStream(trianglesCopy[i3], s, info);
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
	numVertices = (ushort)vertexData.Count;
	numTriangles = (uint)triangles.Count;
	s.AppendLine($"  Center:  {boundingSphere.center}");
	s.AppendLine($"  Radius:  {boundingSphere.radius}");
	s.AppendLine($"  Skin:  {skin}");
	s.AppendLine($"  Shader Property:  {shaderProperty}");
	s.AppendLine($"  Alpha Property:  {alphaProperty}");
	s.AppendLine($"  VF1:  {vertexDesc.vf1}");
	s.AppendLine($"  VF2:  {vertexDesc.vf2}");
	s.AppendLine($"  VF3:  {vertexDesc.vf3}");
	s.AppendLine($"  VF4:  {vertexDesc.vf4}");
	s.AppendLine($"  VF5:  {vertexDesc.vf5}");
	s.AppendLine($"  Vertex Attributes:  {vertexDesc.vertexAttributes}");
	s.AppendLine($"  VF8:  {vertexDesc.vf8}");
	s.AppendLine($"  Num Triangles:  {numTriangles}");
	s.AppendLine($"  Num Vertices:  {numVertices}");
	s.AppendLine($"  Data Size:  {dataSize}");
	if ((dataSize > 0)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < vertexData.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Data[{i2}]:  {vertexData[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < triangles.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Triangles[{i2}]:  {triangles[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Particle Data Size:  {particleDataSize}");
	if ((particleDataSize > 0)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < vertices.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertices[{i2}]:  {vertices[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < trianglesCopy.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Triangles Copy[{i2}]:  {trianglesCopy[i2]}");
			array_output_count++;
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	skin = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	shaderProperty = FixLink<BSShaderProperty>(objects, link_stack, missing_link_stack, info);
	alphaProperty = FixLink<NiAlphaProperty>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (skin != null)
		refs.Add((NiObject)skin);
	if (shaderProperty != null)
		refs.Add((NiObject)shaderProperty);
	if (alphaProperty != null)
		refs.Add((NiObject)alphaProperty);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}