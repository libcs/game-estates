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

/*! Bethesda-specific Havok serializable. */
public class bhkLiquidAction : bhkSerializable {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkLiquidAction", bhkSerializable.TYPE);
	/*!  */
	internal uint userData;
	/*! Unknown */
	internal int unknownInt2;
	/*! Unknown */
	internal int unknownInt3;
	/*!  */
	internal float initialStickForce;
	/*!  */
	internal float stickStrength;
	/*!  */
	internal float neighborDistance;
	/*!  */
	internal float neighborStrength;

	public bhkLiquidAction() {
	userData = (uint)0;
	unknownInt2 = (int)0;
	unknownInt3 = (int)0;
	initialStickForce = 0.0f;
	stickStrength = 0.0f;
	neighborDistance = 0.0f;
	neighborStrength = 0.0f;
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
public static NiObject Create() => new bhkLiquidAction();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out userData, s, info);
	Nif.NifStream(out unknownInt2, s, info);
	Nif.NifStream(out unknownInt3, s, info);
	Nif.NifStream(out initialStickForce, s, info);
	Nif.NifStream(out stickStrength, s, info);
	Nif.NifStream(out neighborDistance, s, info);
	Nif.NifStream(out neighborStrength, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(userData, s, info);
	Nif.NifStream(unknownInt2, s, info);
	Nif.NifStream(unknownInt3, s, info);
	Nif.NifStream(initialStickForce, s, info);
	Nif.NifStream(stickStrength, s, info);
	Nif.NifStream(neighborDistance, s, info);
	Nif.NifStream(neighborStrength, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  User Data:  {userData}");
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
	s.AppendLine($"  Unknown Int 3:  {unknownInt3}");
	s.AppendLine($"  Initial Stick Force:  {initialStickForce}");
	s.AppendLine($"  Stick Strength:  {stickStrength}");
	s.AppendLine($"  Neighbor Distance:  {neighborDistance}");
	s.AppendLine($"  Neighbor Strength:  {neighborStrength}");
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