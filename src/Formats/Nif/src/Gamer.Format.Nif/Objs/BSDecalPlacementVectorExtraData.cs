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
 * Bethesda-specific extra data. Lists locations and normals on a mesh that are
 * appropriate for decal placement.
 */
public class BSDecalPlacementVectorExtraData : NiFloatExtraData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSDecalPlacementVectorExtraData", NiFloatExtraData.TYPE);
	/*!  */
	internal short numVectorBlocks;
	/*!  */
	internal IList<DecalVectorArray> vectorBlocks;

	public BSDecalPlacementVectorExtraData() {
	numVectorBlocks = (short)0;
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
public static NiObject Create() => new BSDecalPlacementVectorExtraData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numVectorBlocks, s, info);
	vectorBlocks = new DecalVectorArray[numVectorBlocks];
	for (var i1 = 0; i1 < vectorBlocks.Count; i1++) {
		Nif.NifStream(out vectorBlocks[i1].numVectors, s, info);
		vectorBlocks[i1].points = new Vector3[vectorBlocks[i1].numVectors];
		for (var i2 = 0; i2 < vectorBlocks[i1].points.Count; i2++) {
			Nif.NifStream(out vectorBlocks[i1].points[i2], s, info);
		}
		vectorBlocks[i1].normals = new Vector3[vectorBlocks[i1].numVectors];
		for (var i2 = 0; i2 < vectorBlocks[i1].normals.Count; i2++) {
			Nif.NifStream(out vectorBlocks[i1].normals[i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numVectorBlocks = (short)vectorBlocks.Count;
	Nif.NifStream(numVectorBlocks, s, info);
	for (var i1 = 0; i1 < vectorBlocks.Count; i1++) {
		vectorBlocks[i1].numVectors = (short)vectorBlocks[i1].points.Count;
		Nif.NifStream(vectorBlocks[i1].numVectors, s, info);
		for (var i2 = 0; i2 < vectorBlocks[i1].points.Count; i2++) {
			Nif.NifStream(vectorBlocks[i1].points[i2], s, info);
		}
		for (var i2 = 0; i2 < vectorBlocks[i1].normals.Count; i2++) {
			Nif.NifStream(vectorBlocks[i1].normals[i2], s, info);
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
	numVectorBlocks = (short)vectorBlocks.Count;
	s.AppendLine($"  Num Vector Blocks:  {numVectorBlocks}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vectorBlocks.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		vectorBlocks[i1].numVectors = (short)vectorBlocks[i1].points.Count;
		s.AppendLine($"    Num Vectors:  {vectorBlocks[i1].numVectors}");
		array_output_count = 0;
		for (var i2 = 0; i2 < vectorBlocks[i1].points.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Points[{i2}]:  {vectorBlocks[i1].points[i2]}");
			array_output_count++;
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < vectorBlocks[i1].normals.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Normals[{i2}]:  {vectorBlocks[i1].normals[i2]}");
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