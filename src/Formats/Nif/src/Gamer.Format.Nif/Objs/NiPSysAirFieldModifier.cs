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
 * Particle system modifier, updates the particle velocity to simulate the effects
 * of air movements like wind, fans, or wake.
 */
public class NiPSysAirFieldModifier : NiPSysFieldModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysAirFieldModifier", NiPSysFieldModifier.TYPE);
	/*! Direction of the particle velocity */
	internal Vector3 direction;
	/*! How quickly particles will accelerate to the magnitude of the air field. */
	internal float airFriction;
	/*! How much of the air field velocity will be added to the particle velocity. */
	internal float inheritVelocity;
	/*!  */
	internal bool inheritRotation;
	/*!  */
	internal bool componentOnly;
	/*!  */
	internal bool enableSpread;
	/*! The angle of the air field cone if Enable Spread is true. */
	internal float spread;

	public NiPSysAirFieldModifier() {
	direction = -1.0, 0.0, 0.0;
	airFriction = 0.0f;
	inheritVelocity = 0.0f;
	inheritRotation = false;
	componentOnly = false;
	enableSpread = false;
	spread = 0.0f;
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
public static NiObject Create() => new NiPSysAirFieldModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out direction, s, info);
	Nif.NifStream(out airFriction, s, info);
	Nif.NifStream(out inheritVelocity, s, info);
	Nif.NifStream(out inheritRotation, s, info);
	Nif.NifStream(out componentOnly, s, info);
	Nif.NifStream(out enableSpread, s, info);
	Nif.NifStream(out spread, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(direction, s, info);
	Nif.NifStream(airFriction, s, info);
	Nif.NifStream(inheritVelocity, s, info);
	Nif.NifStream(inheritRotation, s, info);
	Nif.NifStream(componentOnly, s, info);
	Nif.NifStream(enableSpread, s, info);
	Nif.NifStream(spread, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Direction:  {direction}");
	s.AppendLine($"  Air Friction:  {airFriction}");
	s.AppendLine($"  Inherit Velocity:  {inheritVelocity}");
	s.AppendLine($"  Inherit Rotation:  {inheritRotation}");
	s.AppendLine($"  Component Only:  {componentOnly}");
	s.AppendLine($"  Enable Spread:  {enableSpread}");
	s.AppendLine($"  Spread:  {spread}");
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