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

/*! For serializing NxShapeDesc objects */
public class NiPhysXShapeDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXShapeDesc", NiObject.TYPE);
	/*!  */
	internal NxShapeType shapeType;
	/*!  */
	internal Matrix34 localPose;
	/*!  */
	internal uint shapeFlags;
	/*!  */
	internal ushort collisionGroup;
	/*!  */
	internal ushort materialIndex;
	/*!  */
	internal float density;
	/*!  */
	internal float mass;
	/*!  */
	internal float skinWidth;
	/*!  */
	internal IndexString shapeName;
	/*!  */
	internal uint non_interactingCompartmentTypes;
	/*!  */
	internal Array4<uint> collisionBits;
	/*!  */
	internal NxPlane plane;
	/*!  */
	internal float sphereRadius;
	/*!  */
	internal Vector3 boxHalfExtents;
	/*!  */
	internal NxCapsule capsule;
	/*!  */
	internal NiPhysXMeshDesc mesh;

	public NiPhysXShapeDesc() {
	shapeType = (NxShapeType)0;
	shapeFlags = (uint)0;
	collisionGroup = (ushort)0;
	materialIndex = (ushort)0;
	density = 0.0f;
	mass = 0.0f;
	skinWidth = 0.0f;
	non_interactingCompartmentTypes = (uint)0;
	sphereRadius = 0.0f;
	mesh = null;
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
public static NiObject Create() => new NiPhysXShapeDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out shapeType, s, info);
	Nif.NifStream(out localPose, s, info);
	Nif.NifStream(out shapeFlags, s, info);
	Nif.NifStream(out collisionGroup, s, info);
	Nif.NifStream(out materialIndex, s, info);
	Nif.NifStream(out density, s, info);
	Nif.NifStream(out mass, s, info);
	Nif.NifStream(out skinWidth, s, info);
	Nif.NifStream(out shapeName, s, info);
	if (info.version >= 0x14040000) {
		Nif.NifStream(out non_interactingCompartmentTypes, s, info);
	}
	for (var i1 = 0; i1 < 4; i1++) {
		Nif.NifStream(out collisionBits[i1], s, info);
	}
	if ((shapeType == 0)) {
		Nif.NifStream(out plane.val1, s, info);
		Nif.NifStream(out plane.point1, s, info);
	}
	if ((shapeType == 1)) {
		Nif.NifStream(out sphereRadius, s, info);
	}
	if ((shapeType == 2)) {
		Nif.NifStream(out boxHalfExtents, s, info);
	}
	if ((shapeType == 3)) {
		Nif.NifStream(out capsule.val1, s, info);
		Nif.NifStream(out capsule.val2, s, info);
		Nif.NifStream(out capsule.capsuleFlags, s, info);
	}
	if (((shapeType == 5) || (shapeType == 6))) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(shapeType, s, info);
	Nif.NifStream(localPose, s, info);
	Nif.NifStream(shapeFlags, s, info);
	Nif.NifStream(collisionGroup, s, info);
	Nif.NifStream(materialIndex, s, info);
	Nif.NifStream(density, s, info);
	Nif.NifStream(mass, s, info);
	Nif.NifStream(skinWidth, s, info);
	Nif.NifStream(shapeName, s, info);
	if (info.version >= 0x14040000) {
		Nif.NifStream(non_interactingCompartmentTypes, s, info);
	}
	for (var i1 = 0; i1 < 4; i1++) {
		Nif.NifStream(collisionBits[i1], s, info);
	}
	if ((shapeType == 0)) {
		Nif.NifStream(plane.val1, s, info);
		Nif.NifStream(plane.point1, s, info);
	}
	if ((shapeType == 1)) {
		Nif.NifStream(sphereRadius, s, info);
	}
	if ((shapeType == 2)) {
		Nif.NifStream(boxHalfExtents, s, info);
	}
	if ((shapeType == 3)) {
		Nif.NifStream(capsule.val1, s, info);
		Nif.NifStream(capsule.val2, s, info);
		Nif.NifStream(capsule.capsuleFlags, s, info);
	}
	if (((shapeType == 5) || (shapeType == 6))) {
		WriteRef((NiObject)mesh, s, info, link_map, missing_link_stack);
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
	s.AppendLine($"  Shape Type:  {shapeType}");
	s.AppendLine($"  Local Pose:  {localPose}");
	s.AppendLine($"  Shape Flags:  {shapeFlags}");
	s.AppendLine($"  Collision Group:  {collisionGroup}");
	s.AppendLine($"  Material Index:  {materialIndex}");
	s.AppendLine($"  Density:  {density}");
	s.AppendLine($"  Mass:  {mass}");
	s.AppendLine($"  Skin Width:  {skinWidth}");
	s.AppendLine($"  Shape Name:  {shapeName}");
	s.AppendLine($"  Non-Interacting Compartment Types:  {non_interactingCompartmentTypes}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 4; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Collision Bits[{i1}]:  {collisionBits[i1]}");
		array_output_count++;
	}
	if ((shapeType == 0)) {
		s.AppendLine($"    Val 1:  {plane.val1}");
		s.AppendLine($"    Point 1:  {plane.point1}");
	}
	if ((shapeType == 1)) {
		s.AppendLine($"    Sphere Radius:  {sphereRadius}");
	}
	if ((shapeType == 2)) {
		s.AppendLine($"    Box Half Extents:  {boxHalfExtents}");
	}
	if ((shapeType == 3)) {
		s.AppendLine($"    Val 1:  {capsule.val1}");
		s.AppendLine($"    Val 2:  {capsule.val2}");
		s.AppendLine($"    Capsule Flags:  {capsule.capsuleFlags}");
	}
	if (((shapeType == 5) || (shapeType == 6))) {
		s.AppendLine($"    Mesh:  {mesh}");
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (((shapeType == 5) || (shapeType == 6))) {
		mesh = FixLink<NiPhysXMeshDesc>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (mesh != null)
		refs.Add((NiObject)mesh);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}