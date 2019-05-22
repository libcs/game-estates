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

/*! Particle system collider. */
public class NiPSysCollider : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysCollider", NiObject.TYPE);
	/*! Amount of bounce for the collider. */
	internal float bounce;
	/*! Spawn particles on impact? */
	internal bool spawnOnCollide;
	/*! Kill particles on impact? */
	internal bool dieOnCollide;
	/*! Spawner to use for the collider. */
	internal NiPSysSpawnModifier spawnModifier;
	/*! Link to parent. */
	internal NiPSysColliderManager parent;
	/*! The next collider. */
	internal NiPSysCollider nextCollider;
	/*! The object whose position and orientation are the basis of the collider. */
	internal NiAVObject colliderObject;

	public NiPSysCollider() {
	bounce = 1.0f;
	spawnOnCollide = false;
	dieOnCollide = false;
	spawnModifier = null;
	parent = null;
	nextCollider = null;
	colliderObject = null;
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
public static NiObject Create() => new NiPSysCollider();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out bounce, s, info);
	Nif.NifStream(out spawnOnCollide, s, info);
	Nif.NifStream(out dieOnCollide, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
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
	Nif.NifStream(bounce, s, info);
	Nif.NifStream(spawnOnCollide, s, info);
	Nif.NifStream(dieOnCollide, s, info);
	WriteRef((NiObject)spawnModifier, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)parent, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)nextCollider, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)colliderObject, s, info, link_map, missing_link_stack);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Bounce:  {bounce}");
	s.AppendLine($"  Spawn on Collide:  {spawnOnCollide}");
	s.AppendLine($"  Die on Collide:  {dieOnCollide}");
	s.AppendLine($"  Spawn Modifier:  {spawnModifier}");
	s.AppendLine($"  Parent:  {parent}");
	s.AppendLine($"  Next Collider:  {nextCollider}");
	s.AppendLine($"  Collider Object:  {colliderObject}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	spawnModifier = FixLink<NiPSysSpawnModifier>(objects, link_stack, missing_link_stack, info);
	parent = FixLink<NiPSysColliderManager>(objects, link_stack, missing_link_stack, info);
	nextCollider = FixLink<NiPSysCollider>(objects, link_stack, missing_link_stack, info);
	colliderObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (spawnModifier != null)
		refs.Add((NiObject)spawnModifier);
	if (nextCollider != null)
		refs.Add((NiObject)nextCollider);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (parent != null)
		ptrs.Add((NiObject)parent);
	if (colliderObject != null)
		ptrs.Add((NiObject)colliderObject);
	return ptrs;
}


}

}