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

/*! NiRoom objects represent cells in a cell-portal culling system. */
public class NiRoom : NiNode {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRoom", NiNode.TYPE);
	/*!  */
	internal int numWalls;
	/*!  */
	internal IList<NiPlane> wallPlanes;
	/*!  */
	internal uint numInPortals;
	/*! The portals which see into the room. */
	internal IList<NiPortal> inPortals;
	/*!  */
	internal uint numOutPortals;
	/*! The portals which see out of the room. */
	internal IList<NiPortal> outPortals;
	/*!  */
	internal uint numFixtures;
	/*! All geometry associated with the room. */
	internal IList<NiAVObject> fixtures;

	public NiRoom() {
	numWalls = (int)0;
	numInPortals = (uint)0;
	numOutPortals = (uint)0;
	numFixtures = (uint)0;
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
public static NiObject Create() => new NiRoom();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numWalls, s, info);
	wallPlanes = new NiPlane[numWalls];
	for (var i1 = 0; i1 < wallPlanes.Count; i1++) {
		Nif.NifStream(out wallPlanes[i1].normal, s, info);
		Nif.NifStream(out wallPlanes[i1].constant, s, info);
	}
	Nif.NifStream(out numInPortals, s, info);
	inPortals = new *[numInPortals];
	for (var i1 = 0; i1 < inPortals.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numOutPortals, s, info);
	outPortals = new *[numOutPortals];
	for (var i1 = 0; i1 < outPortals.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numFixtures, s, info);
	fixtures = new *[numFixtures];
	for (var i1 = 0; i1 < fixtures.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numFixtures = (uint)fixtures.Count;
	numOutPortals = (uint)outPortals.Count;
	numInPortals = (uint)inPortals.Count;
	numWalls = (int)wallPlanes.Count;
	Nif.NifStream(numWalls, s, info);
	for (var i1 = 0; i1 < wallPlanes.Count; i1++) {
		Nif.NifStream(wallPlanes[i1].normal, s, info);
		Nif.NifStream(wallPlanes[i1].constant, s, info);
	}
	Nif.NifStream(numInPortals, s, info);
	for (var i1 = 0; i1 < inPortals.Count; i1++) {
		WriteRef((NiObject)inPortals[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numOutPortals, s, info);
	for (var i1 = 0; i1 < outPortals.Count; i1++) {
		WriteRef((NiObject)outPortals[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numFixtures, s, info);
	for (var i1 = 0; i1 < fixtures.Count; i1++) {
		WriteRef((NiObject)fixtures[i1], s, info, link_map, missing_link_stack);
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
	numFixtures = (uint)fixtures.Count;
	numOutPortals = (uint)outPortals.Count;
	numInPortals = (uint)inPortals.Count;
	numWalls = (int)wallPlanes.Count;
	s.AppendLine($"  Num Walls:  {numWalls}");
	array_output_count = 0;
	for (var i1 = 0; i1 < wallPlanes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Normal:  {wallPlanes[i1].normal}");
		s.AppendLine($"    Constant:  {wallPlanes[i1].constant}");
	}
	s.AppendLine($"  Num In Portals:  {numInPortals}");
	array_output_count = 0;
	for (var i1 = 0; i1 < inPortals.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    In Portals[{i1}]:  {inPortals[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Out Portals:  {numOutPortals}");
	array_output_count = 0;
	for (var i1 = 0; i1 < outPortals.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Out Portals[{i1}]:  {outPortals[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Fixtures:  {numFixtures}");
	array_output_count = 0;
	for (var i1 = 0; i1 < fixtures.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Fixtures[{i1}]:  {fixtures[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < inPortals.Count; i1++) {
		inPortals[i1] = FixLink<NiPortal>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < outPortals.Count; i1++) {
		outPortals[i1] = FixLink<NiPortal>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < fixtures.Count; i1++) {
		fixtures[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < inPortals.Count; i1++) {
	}
	for (var i1 = 0; i1 < outPortals.Count; i1++) {
	}
	for (var i1 = 0; i1 < fixtures.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < inPortals.Count; i1++) {
		if (inPortals[i1] != null)
			ptrs.Add((NiObject)inPortals[i1]);
	}
	for (var i1 = 0; i1 < outPortals.Count; i1++) {
		if (outPortals[i1] != null)
			ptrs.Add((NiObject)outPortals[i1]);
	}
	for (var i1 = 0; i1 < fixtures.Count; i1++) {
		if (fixtures[i1] != null)
			ptrs.Add((NiObject)fixtures[i1]);
	}
	return ptrs;
}


}

}