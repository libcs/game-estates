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
 * A havok shape.
 *         A list of convex shapes.
 * 
 *         Do not put a bhkPackedNiTriStripsShape in the Sub Shapes. Use a
 *         separate collision nodes without a list shape for those.
 * 
 *         Also, shapes collected in a bhkListShape may not have the correct
 *         walking noise, so only use it for non-walkable objects.
 */
public class bhkConvexListShape : bhkShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkConvexListShape", bhkShape.TYPE);
	/*!  */
	internal uint numSubShapes;
	/*! List of shapes. */
	internal IList<bhkConvexShape> subShapes;
	/*! The material of the shape. */
	internal HavokMaterial material;
	/*!  */
	internal float radius;
	/*! Unknown. */
	internal uint unknownInt1;
	/*! Unknown. */
	internal float unknownFloat1;
	/*!  */
	internal hkWorldObjCinfoProperty childShapeProperty;
	/*! Unknown. */
	internal byte unknownByte1;
	/*! Unknown. */
	internal float unknownFloat2;

	public bhkConvexListShape() {
	numSubShapes = (uint)0;
	radius = 0.0f;
	unknownInt1 = (uint)0;
	unknownFloat1 = 0.0f;
	unknownByte1 = (byte)0;
	unknownFloat2 = 0.0f;
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
public static NiObject Create() => new bhkConvexListShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numSubShapes, s, info);
	subShapes = new Ref[numSubShapes];
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if (info.version <= 0x0A000102) {
		Nif.NifStream(out material.unknownInt, s, info);
	}
	if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
		Nif.NifStream(out material.material_ob, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
		Nif.NifStream(out material.material_fo, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
		Nif.NifStream(out material.material_sk, s, info);
	}
	Nif.NifStream(out radius, s, info);
	Nif.NifStream(out unknownInt1, s, info);
	Nif.NifStream(out unknownFloat1, s, info);
	Nif.NifStream(out childShapeProperty.data, s, info);
	Nif.NifStream(out childShapeProperty.size, s, info);
	Nif.NifStream(out childShapeProperty.capacityAndFlags, s, info);
	Nif.NifStream(out unknownByte1, s, info);
	Nif.NifStream(out unknownFloat2, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSubShapes = (uint)subShapes.Count;
	Nif.NifStream(numSubShapes, s, info);
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
		WriteRef((NiObject)subShapes[i1], s, info, link_map, missing_link_stack);
	}
	if (info.version <= 0x0A000102) {
		Nif.NifStream(material.unknownInt, s, info);
	}
	if ((info.version <= 0x14000005) && ((info.userVersion2 < 16))) {
		Nif.NifStream(material.material_ob, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 <= 34))) {
		Nif.NifStream(material.material_fo, s, info);
	}
	if (((info.version == 0x14020007) && (info.userVersion2 > 34))) {
		Nif.NifStream(material.material_sk, s, info);
	}
	Nif.NifStream(radius, s, info);
	Nif.NifStream(unknownInt1, s, info);
	Nif.NifStream(unknownFloat1, s, info);
	Nif.NifStream(childShapeProperty.data, s, info);
	Nif.NifStream(childShapeProperty.size, s, info);
	Nif.NifStream(childShapeProperty.capacityAndFlags, s, info);
	Nif.NifStream(unknownByte1, s, info);
	Nif.NifStream(unknownFloat2, s, info);

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
	numSubShapes = (uint)subShapes.Count;
	s.AppendLine($"  Num Sub Shapes:  {numSubShapes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Sub Shapes[{i1}]:  {subShapes[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown Int:  {material.unknownInt}");
	s.AppendLine($"  Material:  {material.material_ob}");
	s.AppendLine($"  Material:  {material.material_fo}");
	s.AppendLine($"  Material:  {material.material_sk}");
	s.AppendLine($"  Radius:  {radius}");
	s.AppendLine($"  Unknown Int 1:  {unknownInt1}");
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	s.AppendLine($"  Data:  {childShapeProperty.data}");
	s.AppendLine($"  Size:  {childShapeProperty.size}");
	s.AppendLine($"  Capacity and Flags:  {childShapeProperty.capacityAndFlags}");
	s.AppendLine($"  Unknown Byte 1:  {unknownByte1}");
	s.AppendLine($"  Unknown Float 2:  {unknownFloat2}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
		subShapes[i1] = FixLink<bhkConvexShape>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
		if (subShapes[i1] != null)
			refs.Add((NiObject)subShapes[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < subShapes.Count; i1++) {
	}
	return ptrs;
}


}

}