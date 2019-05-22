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

/*! Describes a physical constraint. */
public class bhkConstraint : bhkSerializable {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkConstraint", bhkSerializable.TYPE);
	/*! Number of bodies affected by this constraint. */
	internal uint numEntities;
	/*! The entities affected by this constraint. */
	internal IList<bhkEntity> entities;
	/*! Usually 1. Higher values indicate higher priority of this constraint? */
	internal uint priority;

	public bhkConstraint() {
	numEntities = (uint)0;
	priority = (uint)1;
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
public static NiObject Create() => new bhkConstraint();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numEntities, s, info);
	entities = new *[numEntities];
	for (var i1 = 0; i1 < entities.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out priority, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numEntities = (uint)entities.Count;
	Nif.NifStream(numEntities, s, info);
	for (var i1 = 0; i1 < entities.Count; i1++) {
		WriteRef((NiObject)entities[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(priority, s, info);

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
	numEntities = (uint)entities.Count;
	s.AppendLine($"  Num Entities:  {numEntities}");
	array_output_count = 0;
	for (var i1 = 0; i1 < entities.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Entities[{i1}]:  {entities[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Priority:  {priority}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < entities.Count; i1++) {
		entities[i1] = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < entities.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < entities.Count; i1++) {
		if (entities[i1] != null)
			ptrs.Add((NiObject)entities[i1]);
	}
	return ptrs;
}


}

}