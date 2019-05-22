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

/*! Performs linear-weighted blending between a set of target data streams. */
public class NiMorphMeshModifier : NiMeshModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiMorphMeshModifier", NiMeshModifier.TYPE);
	/*!
	 * FLAG_RELATIVETARGETS = 0x01
	 *             FLAG_UPDATENORMALS   = 0x02
	 *             FLAG_NEEDSUPDATE     = 0x04
	 *             FLAG_ALWAYSUPDATE    = 0x08
	 *             FLAG_NEEDSCOMPLETION = 0x10
	 *             FLAG_SKINNED         = 0x20
	 *             FLAG_SWSKINNED       = 0x40
	 */
	internal byte flags;
	/*! The number of morph targets. */
	internal ushort numTargets;
	/*! The number of morphing data stream elements. */
	internal uint numElements;
	/*! Semantics and normalization of the morphing data stream elements. */
	internal IList<ElementReference> elements;

	public NiMorphMeshModifier() {
	flags = (byte)0;
	numTargets = (ushort)0;
	numElements = (uint)0;
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
public static NiObject Create() => new NiMorphMeshModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out numTargets, s, info);
	Nif.NifStream(out numElements, s, info);
	elements = new ElementReference[numElements];
	for (var i1 = 0; i1 < elements.Count; i1++) {
		Nif.NifStream(out elements[i1].semantic.name, s, info);
		Nif.NifStream(out elements[i1].semantic.index, s, info);
		Nif.NifStream(out elements[i1].normalizeFlag, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numElements = (uint)elements.Count;
	Nif.NifStream(flags, s, info);
	Nif.NifStream(numTargets, s, info);
	Nif.NifStream(numElements, s, info);
	for (var i1 = 0; i1 < elements.Count; i1++) {
		Nif.NifStream(elements[i1].semantic.name, s, info);
		Nif.NifStream(elements[i1].semantic.index, s, info);
		Nif.NifStream(elements[i1].normalizeFlag, s, info);
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
	numElements = (uint)elements.Count;
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Num Targets:  {numTargets}");
	s.AppendLine($"  Num Elements:  {numElements}");
	array_output_count = 0;
	for (var i1 = 0; i1 < elements.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Name:  {elements[i1].semantic.name}");
		s.AppendLine($"    Index:  {elements[i1].semantic.index}");
		s.AppendLine($"    Normalize Flag:  {elements[i1].normalizeFlag}");
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