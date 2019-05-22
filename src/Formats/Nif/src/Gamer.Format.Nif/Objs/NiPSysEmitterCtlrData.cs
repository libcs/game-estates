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

/*! DEPRECATED (10.2). Particle system emitter controller data. */
public class NiPSysEmitterCtlrData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysEmitterCtlrData", NiObject.TYPE);
	/*!  */
	internal KeyGroup<float> birthRateKeys;
	/*!  */
	internal uint numActiveKeys;
	/*!  */
	internal IList<Key<byte>> activeKeys;

	public NiPSysEmitterCtlrData() {
	numActiveKeys = (uint)0;
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
public static NiObject Create() => new NiPSysEmitterCtlrData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out birthRateKeys.numKeys, s, info);
	if ((birthRateKeys.numKeys != 0)) {
		Nif.NifStream(out birthRateKeys.interpolation, s, info);
	}
	birthRateKeys.keys = new Key[birthRateKeys.numKeys];
	for (var i1 = 0; i1 < birthRateKeys.keys.Count; i1++) {
		Nif.NifStream(out birthRateKeys.keys[i1], s, info, birthRateKeys.interpolation);
	}
	Nif.NifStream(out numActiveKeys, s, info);
	activeKeys = new Key[numActiveKeys];
	for (var i1 = 0; i1 < activeKeys.Count; i1++) {
		Nif.NifStream(out activeKeys[i1], s, info, 1);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numActiveKeys = (uint)activeKeys.Count;
	birthRateKeys.numKeys = (uint)birthRateKeys.keys.Count;
	Nif.NifStream(birthRateKeys.numKeys, s, info);
	if ((birthRateKeys.numKeys != 0)) {
		Nif.NifStream(birthRateKeys.interpolation, s, info);
	}
	for (var i1 = 0; i1 < birthRateKeys.keys.Count; i1++) {
		Nif.NifStream(birthRateKeys.keys[i1], s, info, birthRateKeys.interpolation);
	}
	Nif.NifStream(numActiveKeys, s, info);
	for (var i1 = 0; i1 < activeKeys.Count; i1++) {
		Nif.NifStream(activeKeys[i1], s, info, 1);
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
	numActiveKeys = (uint)activeKeys.Count;
	birthRateKeys.numKeys = (uint)birthRateKeys.keys.Count;
	s.AppendLine($"  Num Keys:  {birthRateKeys.numKeys}");
	if ((birthRateKeys.numKeys != 0)) {
		s.AppendLine($"    Interpolation:  {birthRateKeys.interpolation}");
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < birthRateKeys.keys.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Keys[{i1}]:  {birthRateKeys.keys[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Active Keys:  {numActiveKeys}");
	array_output_count = 0;
	for (var i1 = 0; i1 < activeKeys.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Active Keys[{i1}]:  {activeKeys[i1]}");
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