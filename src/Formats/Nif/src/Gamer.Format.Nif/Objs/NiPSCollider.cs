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

/*! Abstract base class for all particle colliders. */
public class NiPSCollider : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSCollider", NiObject.TYPE);
	/*!  */
	internal NiPSSpawner spawner;
	/*!  */
	internal ColliderType type;
	/*!  */
	internal bool active;
	/*!  */
	internal float bounce;
	/*!  */
	internal bool spawnOnCollide;
	/*!  */
	internal bool dieOnCollide;

	public NiPSCollider() {
	spawner = null;
	type = (ColliderType)0;
	active = false;
	bounce = 0.0f;
	spawnOnCollide = false;
	dieOnCollide = false;
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
public static NiObject Create() => new NiPSCollider();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out type, s, info);
	Nif.NifStream(out active, s, info);
	Nif.NifStream(out bounce, s, info);
	Nif.NifStream(out spawnOnCollide, s, info);
	Nif.NifStream(out dieOnCollide, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	WriteRef((NiObject)spawner, s, info, link_map, missing_link_stack);
	Nif.NifStream(type, s, info);
	Nif.NifStream(active, s, info);
	Nif.NifStream(bounce, s, info);
	Nif.NifStream(spawnOnCollide, s, info);
	Nif.NifStream(dieOnCollide, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Spawner:  {spawner}");
	s.AppendLine($"  Type:  {type}");
	s.AppendLine($"  Active:  {active}");
	s.AppendLine($"  Bounce:  {bounce}");
	s.AppendLine($"  Spawn on Collide:  {spawnOnCollide}");
	s.AppendLine($"  Die on Collide:  {dieOnCollide}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	spawner = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (spawner != null)
		refs.Add((NiObject)spawner);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}