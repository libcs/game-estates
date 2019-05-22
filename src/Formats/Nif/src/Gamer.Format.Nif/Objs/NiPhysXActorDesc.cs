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

/*! For serializing NxActor objects. */
public class NiPhysXActorDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXActorDesc", NiObject.TYPE);
	/*!  */
	internal IndexString actorName;
	/*!  */
	internal uint numPoses;
	/*!  */
	internal IList<Matrix34> poses;
	/*!  */
	internal NiPhysXBodyDesc bodyDesc;
	/*!  */
	internal float density;
	/*!  */
	internal uint actorFlags;
	/*!  */
	internal ushort actorGroup;
	/*!  */
	internal ushort dominanceGroup;
	/*!  */
	internal uint contactReportFlags;
	/*!  */
	internal ushort forceFieldMaterial;
	/*!  */
	internal uint dummy;
	/*!  */
	internal uint numShapeDescs;
	/*!  */
	internal IList<NiPhysXShapeDesc> shapeDescriptions;
	/*!  */
	internal NiPhysXActorDesc actorParent;
	/*!  */
	internal NiPhysXRigidBodySrc source;
	/*!  */
	internal NiPhysXRigidBodyDest dest;

	public NiPhysXActorDesc() {
	numPoses = (uint)0;
	bodyDesc = null;
	density = 0.0f;
	actorFlags = (uint)0;
	actorGroup = (ushort)0;
	dominanceGroup = (ushort)0;
	contactReportFlags = (uint)0;
	forceFieldMaterial = (ushort)0;
	dummy = (uint)0;
	numShapeDescs = (uint)0;
	actorParent = null;
	source = null;
	dest = null;
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
public static NiObject Create() => new NiPhysXActorDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out actorName, s, info);
	Nif.NifStream(out numPoses, s, info);
	poses = new Matrix34[numPoses];
	for (var i1 = 0; i1 < poses.Count; i1++) {
		Nif.NifStream(out poses[i1], s, info);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out density, s, info);
	Nif.NifStream(out actorFlags, s, info);
	Nif.NifStream(out actorGroup, s, info);
	if (info.version >= 0x14040000) {
		Nif.NifStream(out dominanceGroup, s, info);
		Nif.NifStream(out contactReportFlags, s, info);
		Nif.NifStream(out forceFieldMaterial, s, info);
	}
	if ((info.version >= 0x14030001) && (info.version <= 0x14030005)) {
		Nif.NifStream(out dummy, s, info);
	}
	Nif.NifStream(out numShapeDescs, s, info);
	shapeDescriptions = new Ref[numShapeDescs];
	for (var i1 = 0; i1 < shapeDescriptions.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numShapeDescs = (uint)shapeDescriptions.Count;
	numPoses = (uint)poses.Count;
	Nif.NifStream(actorName, s, info);
	Nif.NifStream(numPoses, s, info);
	for (var i1 = 0; i1 < poses.Count; i1++) {
		Nif.NifStream(poses[i1], s, info);
	}
	WriteRef((NiObject)bodyDesc, s, info, link_map, missing_link_stack);
	Nif.NifStream(density, s, info);
	Nif.NifStream(actorFlags, s, info);
	Nif.NifStream(actorGroup, s, info);
	if (info.version >= 0x14040000) {
		Nif.NifStream(dominanceGroup, s, info);
		Nif.NifStream(contactReportFlags, s, info);
		Nif.NifStream(forceFieldMaterial, s, info);
	}
	if ((info.version >= 0x14030001) && (info.version <= 0x14030005)) {
		Nif.NifStream(dummy, s, info);
	}
	Nif.NifStream(numShapeDescs, s, info);
	for (var i1 = 0; i1 < shapeDescriptions.Count; i1++) {
		WriteRef((NiObject)shapeDescriptions[i1], s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)actorParent, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)source, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)dest, s, info, link_map, missing_link_stack);

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
	numShapeDescs = (uint)shapeDescriptions.Count;
	numPoses = (uint)poses.Count;
	s.AppendLine($"  Actor Name:  {actorName}");
	s.AppendLine($"  Num Poses:  {numPoses}");
	array_output_count = 0;
	for (var i1 = 0; i1 < poses.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Poses[{i1}]:  {poses[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Body Desc:  {bodyDesc}");
	s.AppendLine($"  Density:  {density}");
	s.AppendLine($"  Actor Flags:  {actorFlags}");
	s.AppendLine($"  Actor Group:  {actorGroup}");
	s.AppendLine($"  Dominance Group:  {dominanceGroup}");
	s.AppendLine($"  Contact Report Flags:  {contactReportFlags}");
	s.AppendLine($"  Force Field Material:  {forceFieldMaterial}");
	s.AppendLine($"  Dummy:  {dummy}");
	s.AppendLine($"  Num Shape Descs:  {numShapeDescs}");
	array_output_count = 0;
	for (var i1 = 0; i1 < shapeDescriptions.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Shape Descriptions[{i1}]:  {shapeDescriptions[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Actor Parent:  {actorParent}");
	s.AppendLine($"  Source:  {source}");
	s.AppendLine($"  Dest:  {dest}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	bodyDesc = FixLink<NiPhysXBodyDesc>(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < shapeDescriptions.Count; i1++) {
		shapeDescriptions[i1] = FixLink<NiPhysXShapeDesc>(objects, link_stack, missing_link_stack, info);
	}
	actorParent = FixLink<NiPhysXActorDesc>(objects, link_stack, missing_link_stack, info);
	source = FixLink<NiPhysXRigidBodySrc>(objects, link_stack, missing_link_stack, info);
	dest = FixLink<NiPhysXRigidBodyDest>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (bodyDesc != null)
		refs.Add((NiObject)bodyDesc);
	for (var i1 = 0; i1 < shapeDescriptions.Count; i1++) {
		if (shapeDescriptions[i1] != null)
			refs.Add((NiObject)shapeDescriptions[i1]);
	}
	if (actorParent != null)
		refs.Add((NiObject)actorParent);
	if (source != null)
		refs.Add((NiObject)source);
	if (dest != null)
		refs.Add((NiObject)dest);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < shapeDescriptions.Count; i1++) {
	}
	return ptrs;
}


}

}