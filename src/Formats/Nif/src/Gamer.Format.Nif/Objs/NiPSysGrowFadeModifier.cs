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

/*! Particle modifier that controls the time it takes to grow and shrink a particle. */
public class NiPSysGrowFadeModifier : NiPSysModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysGrowFadeModifier", NiPSysModifier.TYPE);
	/*! The time taken to grow from 0 to their specified size. */
	internal float growTime;
	/*!
	 * Specifies the particle generation to which the grow effect should be applied.
	 * This is usually generation 0, so that newly created particles will grow.
	 */
	internal ushort growGeneration;
	/*! The time taken to shrink from their specified size to 0. */
	internal float fadeTime;
	/*!
	 * Specifies the particle generation to which the shrink effect should be applied.
	 * This is usually the highest supported generation for the particle system.
	 */
	internal ushort fadeGeneration;
	/*! A multiplier on the base particle scale. */
	internal float baseScale;

	public NiPSysGrowFadeModifier() {
	growTime = 0.0f;
	growGeneration = (ushort)0;
	fadeTime = 0.0f;
	fadeGeneration = (ushort)0;
	baseScale = 0.0f;
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
public static NiObject Create() => new NiPSysGrowFadeModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out growTime, s, info);
	Nif.NifStream(out growGeneration, s, info);
	Nif.NifStream(out fadeTime, s, info);
	Nif.NifStream(out fadeGeneration, s, info);
	if ((info.userVersion2 >= 34)) {
		Nif.NifStream(out baseScale, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(growTime, s, info);
	Nif.NifStream(growGeneration, s, info);
	Nif.NifStream(fadeTime, s, info);
	Nif.NifStream(fadeGeneration, s, info);
	if ((info.userVersion2 >= 34)) {
		Nif.NifStream(baseScale, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Grow Time:  {growTime}");
	s.AppendLine($"  Grow Generation:  {growGeneration}");
	s.AppendLine($"  Fade Time:  {fadeTime}");
	s.AppendLine($"  Fade Generation:  {fadeGeneration}");
	s.AppendLine($"  Base Scale:  {baseScale}");
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