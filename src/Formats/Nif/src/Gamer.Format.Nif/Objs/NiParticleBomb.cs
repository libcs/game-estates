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

/*! LEGACY (pre-10.1) particle modifier. */
public class NiParticleBomb : NiParticleModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiParticleBomb", NiParticleModifier.TYPE);
	/*!  */
	internal float decay;
	/*!  */
	internal float duration;
	/*!  */
	internal float deltav;
	/*!  */
	internal float start;
	/*!  */
	internal DecayType decayType;
	/*!  */
	internal SymmetryType symmetryType;
	/*! The position of the mass point relative to the particle system? */
	internal Vector3 position;
	/*! The direction of the applied acceleration? */
	internal Vector3 direction;

	public NiParticleBomb() {
	decay = 0.0f;
	duration = 0.0f;
	deltav = 0.0f;
	start = 0.0f;
	decayType = (DecayType)0;
	symmetryType = (SymmetryType)0;
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
public static NiObject Create() => new NiParticleBomb();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out decay, s, info);
	Nif.NifStream(out duration, s, info);
	Nif.NifStream(out deltav, s, info);
	Nif.NifStream(out start, s, info);
	Nif.NifStream(out decayType, s, info);
	if (info.version >= 0x0401000C) {
		Nif.NifStream(out symmetryType, s, info);
	}
	Nif.NifStream(out position, s, info);
	Nif.NifStream(out direction, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(decay, s, info);
	Nif.NifStream(duration, s, info);
	Nif.NifStream(deltav, s, info);
	Nif.NifStream(start, s, info);
	Nif.NifStream(decayType, s, info);
	if (info.version >= 0x0401000C) {
		Nif.NifStream(symmetryType, s, info);
	}
	Nif.NifStream(position, s, info);
	Nif.NifStream(direction, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Decay:  {decay}");
	s.AppendLine($"  Duration:  {duration}");
	s.AppendLine($"  DeltaV:  {deltav}");
	s.AppendLine($"  Start:  {start}");
	s.AppendLine($"  Decay Type:  {decayType}");
	s.AppendLine($"  Symmetry Type:  {symmetryType}");
	s.AppendLine($"  Position:  {position}");
	s.AppendLine($"  Direction:  {direction}");
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