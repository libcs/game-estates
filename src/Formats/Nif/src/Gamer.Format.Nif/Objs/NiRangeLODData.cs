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
 * NiRangeLODData controls switching LOD levels based on Z depth from the camera to
 * the NiLODNode.
 */
public class NiRangeLODData : NiLODData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRangeLODData", NiLODData.TYPE);
	/*!  */
	internal Vector3 lodCenter;
	/*!  */
	internal uint numLodLevels;
	/*!  */
	internal IList<LODRange> lodLevels;

	public NiRangeLODData() {
	numLodLevels = (uint)0;
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
public static NiObject Create() => new NiRangeLODData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out lodCenter, s, info);
	Nif.NifStream(out numLodLevels, s, info);
	lodLevels = new LODRange[numLodLevels];
	for (var i1 = 0; i1 < lodLevels.Count; i1++) {
		Nif.NifStream(out lodLevels[i1].nearExtent, s, info);
		Nif.NifStream(out lodLevels[i1].farExtent, s, info);
		if (info.version <= 0x03010000) {
			for (var i3 = 0; i3 < 3; i3++) {
				Nif.NifStream(out lodLevels[i1].unknownInts[i3], s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numLodLevels = (uint)lodLevels.Count;
	Nif.NifStream(lodCenter, s, info);
	Nif.NifStream(numLodLevels, s, info);
	for (var i1 = 0; i1 < lodLevels.Count; i1++) {
		Nif.NifStream(lodLevels[i1].nearExtent, s, info);
		Nif.NifStream(lodLevels[i1].farExtent, s, info);
		if (info.version <= 0x03010000) {
			for (var i3 = 0; i3 < 3; i3++) {
				Nif.NifStream(lodLevels[i1].unknownInts[i3], s, info);
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
	numLodLevels = (uint)lodLevels.Count;
	s.AppendLine($"  LOD Center:  {lodCenter}");
	s.AppendLine($"  Num LOD Levels:  {numLodLevels}");
	array_output_count = 0;
	for (var i1 = 0; i1 < lodLevels.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Near Extent:  {lodLevels[i1].nearExtent}");
		s.AppendLine($"    Far Extent:  {lodLevels[i1].farExtent}");
		array_output_count = 0;
		for (var i2 = 0; i2 < 3; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Unknown Ints[{i2}]:  {lodLevels[i1].unknownInts[i2]}");
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