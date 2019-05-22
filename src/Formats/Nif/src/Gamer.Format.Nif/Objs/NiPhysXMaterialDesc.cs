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

/*! For serializing NxMaterialDesc objects. */
public class NiPhysXMaterialDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXMaterialDesc", NiObject.TYPE);
	/*!  */
	internal ushort index;
	/*!  */
	internal uint numStates;
	/*!  */
	internal IList<NxMaterialDesc> materialDescs;

	public NiPhysXMaterialDesc() {
	index = (ushort)0;
	numStates = (uint)0;
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
public static NiObject Create() => new NiPhysXMaterialDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out index, s, info);
	Nif.NifStream(out numStates, s, info);
	materialDescs = new NxMaterialDesc[numStates];
	for (var i1 = 0; i1 < materialDescs.Count; i1++) {
		Nif.NifStream(out materialDescs[i1].dynamicFriction, s, info);
		Nif.NifStream(out materialDescs[i1].staticFriction, s, info);
		Nif.NifStream(out materialDescs[i1].restitution, s, info);
		Nif.NifStream(out materialDescs[i1].dynamicFrictionV, s, info);
		Nif.NifStream(out materialDescs[i1].staticFrictionV, s, info);
		Nif.NifStream(out materialDescs[i1].directionOfAnisotropy, s, info);
		Nif.NifStream(out materialDescs[i1].flags, s, info);
		Nif.NifStream(out materialDescs[i1].frictionCombineMode, s, info);
		Nif.NifStream(out materialDescs[i1].restitutionCombineMode, s, info);
		if (info.version <= 0x14020300) {
			Nif.NifStream(out materialDescs[i1].hasSpring, s, info);
			if (materialDescs[i1].hasSpring) {
				Nif.NifStream(out materialDescs[i1].spring.spring, s, info);
				Nif.NifStream(out materialDescs[i1].spring.damper, s, info);
				Nif.NifStream(out materialDescs[i1].spring.targetValue, s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numStates = (uint)materialDescs.Count;
	Nif.NifStream(index, s, info);
	Nif.NifStream(numStates, s, info);
	for (var i1 = 0; i1 < materialDescs.Count; i1++) {
		Nif.NifStream(materialDescs[i1].dynamicFriction, s, info);
		Nif.NifStream(materialDescs[i1].staticFriction, s, info);
		Nif.NifStream(materialDescs[i1].restitution, s, info);
		Nif.NifStream(materialDescs[i1].dynamicFrictionV, s, info);
		Nif.NifStream(materialDescs[i1].staticFrictionV, s, info);
		Nif.NifStream(materialDescs[i1].directionOfAnisotropy, s, info);
		Nif.NifStream(materialDescs[i1].flags, s, info);
		Nif.NifStream(materialDescs[i1].frictionCombineMode, s, info);
		Nif.NifStream(materialDescs[i1].restitutionCombineMode, s, info);
		if (info.version <= 0x14020300) {
			Nif.NifStream(materialDescs[i1].hasSpring, s, info);
			if (materialDescs[i1].hasSpring) {
				Nif.NifStream(materialDescs[i1].spring.spring, s, info);
				Nif.NifStream(materialDescs[i1].spring.damper, s, info);
				Nif.NifStream(materialDescs[i1].spring.targetValue, s, info);
			}
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
	numStates = (uint)materialDescs.Count;
	s.AppendLine($"  Index:  {index}");
	s.AppendLine($"  Num States:  {numStates}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materialDescs.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Dynamic Friction:  {materialDescs[i1].dynamicFriction}");
		s.AppendLine($"    Static Friction:  {materialDescs[i1].staticFriction}");
		s.AppendLine($"    Restitution:  {materialDescs[i1].restitution}");
		s.AppendLine($"    Dynamic Friction V:  {materialDescs[i1].dynamicFrictionV}");
		s.AppendLine($"    Static Friction V:  {materialDescs[i1].staticFrictionV}");
		s.AppendLine($"    Direction of Anisotropy:  {materialDescs[i1].directionOfAnisotropy}");
		s.AppendLine($"    Flags:  {materialDescs[i1].flags}");
		s.AppendLine($"    Friction Combine Mode:  {materialDescs[i1].frictionCombineMode}");
		s.AppendLine($"    Restitution Combine Mode:  {materialDescs[i1].restitutionCombineMode}");
		s.AppendLine($"    Has Spring:  {materialDescs[i1].hasSpring}");
		if (materialDescs[i1].hasSpring) {
			s.AppendLine($"      Spring:  {materialDescs[i1].spring.spring}");
			s.AppendLine($"      Damper:  {materialDescs[i1].spring.damper}");
			s.AppendLine($"      Target Value:  {materialDescs[i1].spring.targetValue}");
		}
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