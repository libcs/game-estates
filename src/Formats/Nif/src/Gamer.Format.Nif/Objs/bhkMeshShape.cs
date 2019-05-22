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

/*!  */
public class bhkMeshShape : bhkShape {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkMeshShape", bhkShape.TYPE);
	/*! Unknown. */
	internal Array2<uint> unknowns;
	/*!  */
	internal float radius;
	/*!  */
	internal Array8<byte> unused2;
	/*!  */
	internal Vector4 scale;
	/*!  */
	internal uint numShapeProperties;
	/*!  */
	internal IList<hkWorldObjCinfoProperty> shapeProperties;
	/*! Unknown. */
	internal Array3<int> unknown2;
	/*! The number of strips data objects referenced. */
	internal uint numStripsData;
	/*! Refers to a bunch of NiTriStripsData objects that make up this shape. */
	internal IList<NiTriStripsData> stripsData;

	public bhkMeshShape() {
	radius = 0.0f;
	numShapeProperties = (uint)0;
	numStripsData = (uint)0;
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
public static NiObject Create() => new bhkMeshShape();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	for (var i1 = 0; i1 < 2; i1++) {
		Nif.NifStream(out unknowns[i1], s, info);
	}
	Nif.NifStream(out radius, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(out unused2[i1], s, info);
	}
	Nif.NifStream(out scale, s, info);
	Nif.NifStream(out numShapeProperties, s, info);
	shapeProperties = new hkWorldObjCinfoProperty[numShapeProperties];
	for (var i1 = 0; i1 < shapeProperties.Count; i1++) {
		Nif.NifStream(out shapeProperties[i1].data, s, info);
		Nif.NifStream(out shapeProperties[i1].size, s, info);
		Nif.NifStream(out shapeProperties[i1].capacityAndFlags, s, info);
	}
	for (var i1 = 0; i1 < 3; i1++) {
		Nif.NifStream(out unknown2[i1], s, info);
	}
	if (info.version <= 0x0A000100) {
		Nif.NifStream(out numStripsData, s, info);
		stripsData = new Ref[numStripsData];
		for (var i2 = 0; i2 < stripsData.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numStripsData = (uint)stripsData.Count;
	numShapeProperties = (uint)shapeProperties.Count;
	for (var i1 = 0; i1 < 2; i1++) {
		Nif.NifStream(unknowns[i1], s, info);
	}
	Nif.NifStream(radius, s, info);
	for (var i1 = 0; i1 < 8; i1++) {
		Nif.NifStream(unused2[i1], s, info);
	}
	Nif.NifStream(scale, s, info);
	Nif.NifStream(numShapeProperties, s, info);
	for (var i1 = 0; i1 < shapeProperties.Count; i1++) {
		Nif.NifStream(shapeProperties[i1].data, s, info);
		Nif.NifStream(shapeProperties[i1].size, s, info);
		Nif.NifStream(shapeProperties[i1].capacityAndFlags, s, info);
	}
	for (var i1 = 0; i1 < 3; i1++) {
		Nif.NifStream(unknown2[i1], s, info);
	}
	if (info.version <= 0x0A000100) {
		Nif.NifStream(numStripsData, s, info);
		for (var i2 = 0; i2 < stripsData.Count; i2++) {
			WriteRef((NiObject)stripsData[i2], s, info, link_map, missing_link_stack);
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
	numStripsData = (uint)stripsData.Count;
	numShapeProperties = (uint)shapeProperties.Count;
	array_output_count = 0;
	for (var i1 = 0; i1 < 2; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknowns[{i1}]:  {unknowns[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Radius:  {radius}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 8; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unused 2[{i1}]:  {unused2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Scale:  {scale}");
	s.AppendLine($"  Num Shape Properties:  {numShapeProperties}");
	array_output_count = 0;
	for (var i1 = 0; i1 < shapeProperties.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Data:  {shapeProperties[i1].data}");
		s.AppendLine($"    Size:  {shapeProperties[i1].size}");
		s.AppendLine($"    Capacity and Flags:  {shapeProperties[i1].capacityAndFlags}");
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < 3; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown 2[{i1}]:  {unknown2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Strips Data:  {numStripsData}");
	array_output_count = 0;
	for (var i1 = 0; i1 < stripsData.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Strips Data[{i1}]:  {stripsData[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x0A000100) {
		for (var i2 = 0; i2 < stripsData.Count; i2++) {
			stripsData[i2] = FixLink<NiTriStripsData>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < stripsData.Count; i1++) {
		if (stripsData[i1] != null)
			refs.Add((NiObject)stripsData[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < stripsData.Count; i1++) {
	}
	return ptrs;
}


}

}