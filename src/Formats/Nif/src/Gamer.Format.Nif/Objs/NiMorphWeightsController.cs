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

/*! Manipulates a mesh with the semantic MORPHWEIGHTS using an NiMorphMeshModifier. */
public class NiMorphWeightsController : NiInterpController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiMorphWeightsController", NiInterpController.TYPE);
	/*!  */
	internal uint count;
	/*!  */
	internal uint numInterpolators;
	/*!  */
	internal IList<NiInterpolator> interpolators;
	/*!  */
	internal uint numTargets;
	/*!  */
	internal IList<IndexString> targetNames;

	public NiMorphWeightsController() {
	count = (uint)0;
	numInterpolators = (uint)0;
	numTargets = (uint)0;
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
public static NiObject Create() => new NiMorphWeightsController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out count, s, info);
	Nif.NifStream(out numInterpolators, s, info);
	interpolators = new Ref[numInterpolators];
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numTargets, s, info);
	targetNames = new IndexString[numTargets];
	for (var i1 = 0; i1 < targetNames.Count; i1++) {
		Nif.NifStream(out targetNames[i1], s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numTargets = (uint)targetNames.Count;
	numInterpolators = (uint)interpolators.Count;
	Nif.NifStream(count, s, info);
	Nif.NifStream(numInterpolators, s, info);
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		WriteRef((NiObject)interpolators[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numTargets, s, info);
	for (var i1 = 0; i1 < targetNames.Count; i1++) {
		Nif.NifStream(targetNames[i1], s, info);
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
	numTargets = (uint)targetNames.Count;
	numInterpolators = (uint)interpolators.Count;
	s.AppendLine($"  Count:  {count}");
	s.AppendLine($"  Num Interpolators:  {numInterpolators}");
	array_output_count = 0;
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Interpolators[{i1}]:  {interpolators[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Targets:  {numTargets}");
	array_output_count = 0;
	for (var i1 = 0; i1 < targetNames.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Target Names[{i1}]:  {targetNames[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		interpolators[i1] = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		if (interpolators[i1] != null)
			refs.Add((NiObject)interpolators[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
	}
	return ptrs;
}


}

}