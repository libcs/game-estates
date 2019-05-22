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

/*! For serialization of PhysX objects and to attach them to the scene. */
public class NiPhysXPropDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXPropDesc", NiObject.TYPE);
	/*!  */
	internal int numActors;
	/*!  */
	internal IList<NiPhysXActorDesc> actors;
	/*!  */
	internal uint numJoints;
	/*!  */
	internal IList<NiPhysXJointDesc> joints;
	/*!  */
	internal uint numClothes;
	/*!  */
	internal IList<NiObject> clothes;
	/*!  */
	internal uint numMaterials;
	/*!  */
	internal IList<PhysXMaterialRef> materials;
	/*!  */
	internal uint numStates;
	/*!  */
	internal uint numStateNames;
	/*!  */
	internal IList<PhysXStateName> stateNames;
	/*!  */
	internal byte flags;

	public NiPhysXPropDesc() {
	numActors = (int)0;
	numJoints = (uint)0;
	numClothes = (uint)0;
	numMaterials = (uint)0;
	numStates = (uint)0;
	numStateNames = (uint)0;
	flags = (byte)0;
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
public static NiObject Create() => new NiPhysXPropDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numActors, s, info);
	actors = new Ref[numActors];
	for (var i1 = 0; i1 < actors.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numJoints, s, info);
	joints = new Ref[numJoints];
	for (var i1 = 0; i1 < joints.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if (info.version >= 0x14030005) {
		Nif.NifStream(out numClothes, s, info);
		clothes = new Ref[numClothes];
		for (var i2 = 0; i2 < clothes.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	Nif.NifStream(out numMaterials, s, info);
	materials = new PhysXMaterialRef[numMaterials];
	for (var i1 = 0; i1 < materials.Count; i1++) {
		Nif.NifStream(out materials[i1].key, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numStates, s, info);
	if (info.version >= 0x14040000) {
		Nif.NifStream(out numStateNames, s, info);
		stateNames = new PhysXStateName[numStateNames];
		for (var i2 = 0; i2 < stateNames.Count; i2++) {
			Nif.NifStream(out stateNames[i2].name, s, info);
			Nif.NifStream(out stateNames[i2].index, s, info);
		}
		Nif.NifStream(out flags, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numStateNames = (uint)stateNames.Count;
	numMaterials = (uint)materials.Count;
	numClothes = (uint)clothes.Count;
	numJoints = (uint)joints.Count;
	numActors = (int)actors.Count;
	Nif.NifStream(numActors, s, info);
	for (var i1 = 0; i1 < actors.Count; i1++) {
		WriteRef((NiObject)actors[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numJoints, s, info);
	for (var i1 = 0; i1 < joints.Count; i1++) {
		WriteRef((NiObject)joints[i1], s, info, link_map, missing_link_stack);
	}
	if (info.version >= 0x14030005) {
		Nif.NifStream(numClothes, s, info);
		for (var i2 = 0; i2 < clothes.Count; i2++) {
			WriteRef((NiObject)clothes[i2], s, info, link_map, missing_link_stack);
		}
	}
	Nif.NifStream(numMaterials, s, info);
	for (var i1 = 0; i1 < materials.Count; i1++) {
		Nif.NifStream(materials[i1].key, s, info);
		WriteRef((NiObject)materials[i1].materialDesc, s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numStates, s, info);
	if (info.version >= 0x14040000) {
		Nif.NifStream(numStateNames, s, info);
		for (var i2 = 0; i2 < stateNames.Count; i2++) {
			Nif.NifStream(stateNames[i2].name, s, info);
			Nif.NifStream(stateNames[i2].index, s, info);
		}
		Nif.NifStream(flags, s, info);
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
	numStateNames = (uint)stateNames.Count;
	numMaterials = (uint)materials.Count;
	numClothes = (uint)clothes.Count;
	numJoints = (uint)joints.Count;
	numActors = (int)actors.Count;
	s.AppendLine($"  Num Actors:  {numActors}");
	array_output_count = 0;
	for (var i1 = 0; i1 < actors.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Actors[{i1}]:  {actors[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Joints:  {numJoints}");
	array_output_count = 0;
	for (var i1 = 0; i1 < joints.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Joints[{i1}]:  {joints[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Clothes:  {numClothes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < clothes.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Clothes[{i1}]:  {clothes[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Materials:  {numMaterials}");
	array_output_count = 0;
	for (var i1 = 0; i1 < materials.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Key:  {materials[i1].key}");
		s.AppendLine($"    Material Desc:  {materials[i1].materialDesc}");
	}
	s.AppendLine($"  Num States:  {numStates}");
	s.AppendLine($"  Num State Names:  {numStateNames}");
	array_output_count = 0;
	for (var i1 = 0; i1 < stateNames.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Name:  {stateNames[i1].name}");
		s.AppendLine($"    Index:  {stateNames[i1].index}");
	}
	s.AppendLine($"  Flags:  {flags}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < actors.Count; i1++) {
		actors[i1] = FixLink<NiPhysXActorDesc>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < joints.Count; i1++) {
		joints[i1] = FixLink<NiPhysXJointDesc>(objects, link_stack, missing_link_stack, info);
	}
	if (info.version >= 0x14030005) {
		for (var i2 = 0; i2 < clothes.Count; i2++) {
			clothes[i2] = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
		}
	}
	for (var i1 = 0; i1 < materials.Count; i1++) {
		materials[i1].materialDesc = FixLink<NiPhysXMaterialDesc>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < actors.Count; i1++) {
		if (actors[i1] != null)
			refs.Add((NiObject)actors[i1]);
	}
	for (var i1 = 0; i1 < joints.Count; i1++) {
		if (joints[i1] != null)
			refs.Add((NiObject)joints[i1]);
	}
	for (var i1 = 0; i1 < clothes.Count; i1++) {
		if (clothes[i1] != null)
			refs.Add((NiObject)clothes[i1]);
	}
	for (var i1 = 0; i1 < materials.Count; i1++) {
		if (materials[i1].materialDesc != null)
			refs.Add((NiObject)materials[i1].materialDesc);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < actors.Count; i1++) {
	}
	for (var i1 = 0; i1 < joints.Count; i1++) {
	}
	for (var i1 = 0; i1 < clothes.Count; i1++) {
	}
	for (var i1 = 0; i1 < materials.Count; i1++) {
	}
	return ptrs;
}


}

}