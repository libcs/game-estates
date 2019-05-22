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
public class BSPSysHavokUpdateModifier : NiPSysModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSPSysHavokUpdateModifier", NiPSysModifier.TYPE);
	/*!  */
	internal uint numNodes;
	/*!  */
	internal IList<NiNode> nodes;
	/*!  */
	internal NiPSysModifier modifier;

	public BSPSysHavokUpdateModifier() {
	numNodes = (uint)0;
	modifier = null;
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
public static NiObject Create() => new BSPSysHavokUpdateModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numNodes, s, info);
	nodes = new Ref[numNodes];
	for (var i1 = 0; i1 < nodes.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numNodes = (uint)nodes.Count;
	Nif.NifStream(numNodes, s, info);
	for (var i1 = 0; i1 < nodes.Count; i1++) {
		WriteRef((NiObject)nodes[i1], s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)modifier, s, info, link_map, missing_link_stack);

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
	numNodes = (uint)nodes.Count;
	s.AppendLine($"  Num Nodes:  {numNodes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < nodes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Nodes[{i1}]:  {nodes[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Modifier:  {modifier}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < nodes.Count; i1++) {
		nodes[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}
	modifier = FixLink<NiPSysModifier>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < nodes.Count; i1++) {
		if (nodes[i1] != null)
			refs.Add((NiObject)nodes[i1]);
	}
	if (modifier != null)
		refs.Add((NiObject)modifier);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < nodes.Count; i1++) {
	}
	return ptrs;
}


}

}