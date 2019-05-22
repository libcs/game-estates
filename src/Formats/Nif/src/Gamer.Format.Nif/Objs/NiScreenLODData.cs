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
 * NiScreenLODData controls switching LOD levels based on proportion of the screen
 * that a bound would include.
 */
public class NiScreenLODData : NiLODData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiScreenLODData", NiLODData.TYPE);
	/*!  */
	internal NiBound bound;
	/*!  */
	internal NiBound worldBound;
	/*!  */
	internal uint numProportions;
	/*!  */
	internal IList<float> proportionLevels;

	public NiScreenLODData() {
	numProportions = (uint)0;
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
public static NiObject Create() => new NiScreenLODData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out bound.center, s, info);
	Nif.NifStream(out bound.radius, s, info);
	Nif.NifStream(out worldBound.center, s, info);
	Nif.NifStream(out worldBound.radius, s, info);
	Nif.NifStream(out numProportions, s, info);
	proportionLevels = new float[numProportions];
	for (var i1 = 0; i1 < proportionLevels.Count; i1++) {
		Nif.NifStream(out proportionLevels[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numProportions = (uint)proportionLevels.Count;
	Nif.NifStream(bound.center, s, info);
	Nif.NifStream(bound.radius, s, info);
	Nif.NifStream(worldBound.center, s, info);
	Nif.NifStream(worldBound.radius, s, info);
	Nif.NifStream(numProportions, s, info);
	for (var i1 = 0; i1 < proportionLevels.Count; i1++) {
		Nif.NifStream(proportionLevels[i1], s, info);
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
	numProportions = (uint)proportionLevels.Count;
	s.AppendLine($"  Center:  {bound.center}");
	s.AppendLine($"  Radius:  {bound.radius}");
	s.AppendLine($"  Center:  {worldBound.center}");
	s.AppendLine($"  Radius:  {worldBound.radius}");
	s.AppendLine($"  Num Proportions:  {numProportions}");
	array_output_count = 0;
	for (var i1 = 0; i1 < proportionLevels.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Proportion Levels[{i1}]:  {proportionLevels[i1]}");
		array_output_count++;
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