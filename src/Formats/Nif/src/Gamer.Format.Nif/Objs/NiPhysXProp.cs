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

/*! A PhysX prop which holds information about PhysX actors in a Gamebryo scene */
public class NiPhysXProp : NiObjectNET {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXProp", NiObjectNET.TYPE);
	/*!  */
	internal float physxToWorldScale;
	/*!  */
	internal uint numSources;
	/*!  */
	internal IList<NiObject> sources;
	/*!  */
	internal int numDests;
	/*!  */
	internal IList<NiPhysXDest> dests;
	/*!  */
	internal uint numModifiedMeshes;
	/*!  */
	internal IList<NiMesh> modifiedMeshes;
	/*!  */
	internal IndexString tempName;
	/*!  */
	internal bool keepMeshes;
	/*!  */
	internal NiPhysXPropDesc propDescription;

	public NiPhysXProp() {
	physxToWorldScale = 0.0f;
	numSources = (uint)0;
	numDests = (int)0;
	numModifiedMeshes = (uint)0;
	keepMeshes = false;
	propDescription = null;
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
public static NiObject Create() => new NiPhysXProp();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out physxToWorldScale, s, info);
	Nif.NifStream(out numSources, s, info);
	sources = new Ref[numSources];
	for (var i1 = 0; i1 < sources.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numDests, s, info);
	dests = new Ref[numDests];
	for (var i1 = 0; i1 < dests.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if (info.version >= 0x14040000) {
		Nif.NifStream(out numModifiedMeshes, s, info);
		modifiedMeshes = new Ref[numModifiedMeshes];
		for (var i2 = 0; i2 < modifiedMeshes.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x1E010002) && (info.version <= 0x1E020002)) {
		Nif.NifStream(out tempName, s, info);
	}
	Nif.NifStream(out keepMeshes, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numModifiedMeshes = (uint)modifiedMeshes.Count;
	numDests = (int)dests.Count;
	numSources = (uint)sources.Count;
	Nif.NifStream(physxToWorldScale, s, info);
	Nif.NifStream(numSources, s, info);
	for (var i1 = 0; i1 < sources.Count; i1++) {
		WriteRef((NiObject)sources[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numDests, s, info);
	for (var i1 = 0; i1 < dests.Count; i1++) {
		WriteRef((NiObject)dests[i1], s, info, link_map, missing_link_stack);
	}
	if (info.version >= 0x14040000) {
		Nif.NifStream(numModifiedMeshes, s, info);
		for (var i2 = 0; i2 < modifiedMeshes.Count; i2++) {
			WriteRef((NiObject)modifiedMeshes[i2], s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x1E010002) && (info.version <= 0x1E020002)) {
		Nif.NifStream(tempName, s, info);
	}
	Nif.NifStream(keepMeshes, s, info);
	WriteRef((NiObject)propDescription, s, info, link_map, missing_link_stack);

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
	numModifiedMeshes = (uint)modifiedMeshes.Count;
	numDests = (int)dests.Count;
	numSources = (uint)sources.Count;
	s.AppendLine($"  PhysX to World Scale:  {physxToWorldScale}");
	s.AppendLine($"  Num Sources:  {numSources}");
	array_output_count = 0;
	for (var i1 = 0; i1 < sources.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Sources[{i1}]:  {sources[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Dests:  {numDests}");
	array_output_count = 0;
	for (var i1 = 0; i1 < dests.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Dests[{i1}]:  {dests[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Modified Meshes:  {numModifiedMeshes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < modifiedMeshes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Modified Meshes[{i1}]:  {modifiedMeshes[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Temp Name:  {tempName}");
	s.AppendLine($"  Keep Meshes:  {keepMeshes}");
	s.AppendLine($"  Prop Description:  {propDescription}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < sources.Count; i1++) {
		sources[i1] = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < dests.Count; i1++) {
		dests[i1] = FixLink<NiPhysXDest>(objects, link_stack, missing_link_stack, info);
	}
	if (info.version >= 0x14040000) {
		for (var i2 = 0; i2 < modifiedMeshes.Count; i2++) {
			modifiedMeshes[i2] = FixLink<NiMesh>(objects, link_stack, missing_link_stack, info);
		}
	}
	propDescription = FixLink<NiPhysXPropDesc>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < sources.Count; i1++) {
		if (sources[i1] != null)
			refs.Add((NiObject)sources[i1]);
	}
	for (var i1 = 0; i1 < dests.Count; i1++) {
		if (dests[i1] != null)
			refs.Add((NiObject)dests[i1]);
	}
	for (var i1 = 0; i1 < modifiedMeshes.Count; i1++) {
		if (modifiedMeshes[i1] != null)
			refs.Add((NiObject)modifiedMeshes[i1]);
	}
	if (propDescription != null)
		refs.Add((NiObject)propDescription);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < sources.Count; i1++) {
	}
	for (var i1 = 0; i1 < dests.Count; i1++) {
	}
	for (var i1 = 0; i1 < modifiedMeshes.Count; i1++) {
	}
	return ptrs;
}


}

}