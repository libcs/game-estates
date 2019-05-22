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

/*! Unknown */
public class BSBoneLODExtraData : NiExtraData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSBoneLODExtraData", NiExtraData.TYPE);
	/*! Number of bone entries */
	internal uint bonelodCount;
	/*! Bone Entry */
	internal IList<BoneLOD> bonelodInfo;

	public BSBoneLODExtraData() {
	bonelodCount = (uint)0;
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
public static NiObject Create() => new BSBoneLODExtraData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out bonelodCount, s, info);
	bonelodInfo = new BoneLOD[bonelodCount];
	for (var i1 = 0; i1 < bonelodInfo.Count; i1++) {
		Nif.NifStream(out bonelodInfo[i1].distance, s, info);
		Nif.NifStream(out bonelodInfo[i1].boneName, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	bonelodCount = (uint)bonelodInfo.Count;
	Nif.NifStream(bonelodCount, s, info);
	for (var i1 = 0; i1 < bonelodInfo.Count; i1++) {
		Nif.NifStream(bonelodInfo[i1].distance, s, info);
		Nif.NifStream(bonelodInfo[i1].boneName, s, info);
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
	bonelodCount = (uint)bonelodInfo.Count;
	s.AppendLine($"  BoneLOD Count:  {bonelodCount}");
	array_output_count = 0;
	for (var i1 = 0; i1 < bonelodInfo.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Distance:  {bonelodInfo[i1].distance}");
		s.AppendLine($"    Bone Name:  {bonelodInfo[i1].boneName}");
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