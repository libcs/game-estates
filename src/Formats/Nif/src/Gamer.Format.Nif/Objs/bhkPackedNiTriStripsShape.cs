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

/*! A shape constructed from strips data. */
public class bhkPackedNiTriStripsShape : bhkShapeCollection {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkPackedNiTriStripsShape", bhkShapeCollection.TYPE);
	/*!  */
	internal ushort numSubShapes;
	/*!  */
	internal IList<OblivionSubShape> subShapes;
	/*!  */
	internal uint userData;
	/*! Looks like a memory pointer and may be garbage. */
	internal uint unused1;
	/*!  */
	internal float radius;
	/*! Looks like a memory pointer and may be garbage. */
	internal uint unused2;
	/*!  */
	internal Vector4 scale;
	/*! Same as radius */
	internal float radiusCopy;
	/*! Same as scale. */
	internal Vector4 scaleCopy;
	/*!  */
	internal hkPackedNiTriStripsData data;

	public bhkPackedNiTriStripsShape() {
	numSubShapes = (ushort)0;
	userData = (uint)0;
	unused1 = (uint)0;
	radius = 0.1f;
	unused2 = (uint)0;
	scale = 1.0, 1.0, 1.0, 0.0;
	radiusCopy = 0.1f;
	scaleCopy = 1.0, 1.0, 1.0, 0.0;
	data = null;
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
public static NiObject Create() => new bhkPackedNiTriStripsShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version <= 0x14000005) {
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
	Nif.NifStream(out userData, s, info);
	Nif.NifStream(out unused1, s, info);
	Nif.NifStream(out radius, s, info);
	Nif.NifStream(out unused2, s, info);
	Nif.NifStream(out scale, s, info);
	Nif.NifStream(out radiusCopy, s, info);
	Nif.NifStream(out scaleCopy, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSubShapes = (ushort)subShapes.Count;
	if (info.version <= 0x14000005) {
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
	Nif.NifStream(userData, s, info);
	Nif.NifStream(unused1, s, info);
	Nif.NifStream(radius, s, info);
	Nif.NifStream(unused2, s, info);
	Nif.NifStream(scale, s, info);
	Nif.NifStream(radiusCopy, s, info);
	Nif.NifStream(scaleCopy, s, info);
	WriteRef((NiObject)data, s, info, link_map, missing_link_stack);

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
	s.AppendLine($"  User Data:  {userData}");
	s.AppendLine($"  Unused 1:  {unused1}");
	s.AppendLine($"  Radius:  {radius}");
	s.AppendLine($"  Unused 2:  {unused2}");
	s.AppendLine($"  Scale:  {scale}");
	s.AppendLine($"  Radius Copy:  {radiusCopy}");
	s.AppendLine($"  Scale Copy:  {scaleCopy}");
	s.AppendLine($"  Data:  {data}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	data = FixLink<hkPackedNiTriStripsData>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (data != null)
		refs.Add((NiObject)data);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}