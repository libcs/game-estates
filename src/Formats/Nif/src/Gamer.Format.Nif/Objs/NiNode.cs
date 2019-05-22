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

/*! Generic node object for grouping. */
public class NiNode : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiNode", NiAVObject.TYPE);
	/*! The number of child objects. */
	internal uint numChildren;
	/*! List of child node object indices. */
	internal IList<NiAVObject> children;
	/*! The number of references to effect objects that follow. */
	internal uint numEffects;
	/*! List of node effects. ADynamicEffect? */
	internal IList<NiDynamicEffect> effects;

	public NiNode() {
	numChildren = (uint)0;
	numEffects = (uint)0;
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
public static NiObject Create() => new NiNode();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numChildren, s, info);
	children = new Ref[numChildren];
	for (var i1 = 0; i1 < children.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.userVersion2 < 130)) {
		Nif.NifStream(out numEffects, s, info);
		effects = new Ref[numEffects];
		for (var i2 = 0; i2 < effects.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numEffects = (uint)effects.Count;
	numChildren = (uint)children.Count;
	Nif.NifStream(numChildren, s, info);
	for (var i1 = 0; i1 < children.Count; i1++) {
		WriteRef((NiObject)children[i1], s, info, link_map, missing_link_stack);
	}
	if ((info.userVersion2 < 130)) {
		Nif.NifStream(numEffects, s, info);
		for (var i2 = 0; i2 < effects.Count; i2++) {
			WriteRef((NiObject)effects[i2], s, info, link_map, missing_link_stack);
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
	numEffects = (uint)effects.Count;
	numChildren = (uint)children.Count;
	s.AppendLine($"  Num Children:  {numChildren}");
	array_output_count = 0;
	for (var i1 = 0; i1 < children.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Children[{i1}]:  {children[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Effects:  {numEffects}");
	array_output_count = 0;
	for (var i1 = 0; i1 < effects.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Effects[{i1}]:  {effects[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < children.Count; i1++) {
		children[i1] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.userVersion2 < 130)) {
		for (var i2 = 0; i2 < effects.Count; i2++) {
			effects[i2] = FixLink<NiDynamicEffect>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < children.Count; i1++) {
		if (children[i1] != null)
			refs.Add((NiObject)children[i1]);
	}
	for (var i1 = 0; i1 < effects.Count; i1++) {
		if (effects[i1] != null)
			refs.Add((NiObject)effects[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < children.Count; i1++) {
	}
	for (var i1 = 0; i1 < effects.Count; i1++) {
	}
	return ptrs;
}


}

}