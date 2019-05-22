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

/*!
 * Orientation marker for Skyrim's inventory view.
 *         How to show the nif in the player's inventory.
 *         Typically attached to the root node of the nif tree.
 *         If not present, then Skyrim will still show the nif in inventory,
 *         using the default values.
 *         Name should be 'INV' (without the quotes).
 *         For rotations, a short of "4712" appears as "4.712" but "959" appears as
 * "0.959"  meshes\weapons\daedric\daedricbowskinned.nif
 */
public class BSInvMarker : NiExtraData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSInvMarker", NiExtraData.TYPE);
	/*!  */
	internal ushort rotationX;
	/*!  */
	internal ushort rotationY;
	/*!  */
	internal ushort rotationZ;
	/*! Zoom factor. */
	internal float zoom;

	public BSInvMarker() {
	rotationX = (ushort)4712;
	rotationY = (ushort)6283;
	rotationZ = (ushort)0;
	zoom = 1.0f;
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
public static NiObject Create() => new BSInvMarker();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out rotationX, s, info);
	Nif.NifStream(out rotationY, s, info);
	Nif.NifStream(out rotationZ, s, info);
	Nif.NifStream(out zoom, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(rotationX, s, info);
	Nif.NifStream(rotationY, s, info);
	Nif.NifStream(rotationZ, s, info);
	Nif.NifStream(zoom, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Rotation X:  {rotationX}");
	s.AppendLine($"  Rotation Y:  {rotationY}");
	s.AppendLine($"  Rotation Z:  {rotationZ}");
	s.AppendLine($"  Zoom:  {zoom}");
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