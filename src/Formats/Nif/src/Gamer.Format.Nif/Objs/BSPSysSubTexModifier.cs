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
 * Similar to a Flip Controller, this handles particle texture animation on a
 * single texture atlas
 */
public class BSPSysSubTexModifier : NiPSysModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSPSysSubTexModifier", NiPSysModifier.TYPE);
	/*! Starting frame/position on atlas */
	internal uint startFrame;
	/*! Random chance to start on a different frame? */
	internal float startFrameFudge;
	/*! Ending frame/position on atlas */
	internal float endFrame;
	/*! Frame to start looping */
	internal float loopStartFrame;
	/*!  */
	internal float loopStartFrameFudge;
	/*!  */
	internal float frameCount;
	/*!  */
	internal float frameCountFudge;

	public BSPSysSubTexModifier() {
	startFrame = (uint)0;
	startFrameFudge = 0.0f;
	endFrame = 0.0f;
	loopStartFrame = 0.0f;
	loopStartFrameFudge = 0.0f;
	frameCount = 0.0f;
	frameCountFudge = 0.0f;
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
public static NiObject Create() => new BSPSysSubTexModifier();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out startFrame, s, info);
	Nif.NifStream(out startFrameFudge, s, info);
	Nif.NifStream(out endFrame, s, info);
	Nif.NifStream(out loopStartFrame, s, info);
	Nif.NifStream(out loopStartFrameFudge, s, info);
	Nif.NifStream(out frameCount, s, info);
	Nif.NifStream(out frameCountFudge, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(startFrame, s, info);
	Nif.NifStream(startFrameFudge, s, info);
	Nif.NifStream(endFrame, s, info);
	Nif.NifStream(loopStartFrame, s, info);
	Nif.NifStream(loopStartFrameFudge, s, info);
	Nif.NifStream(frameCount, s, info);
	Nif.NifStream(frameCountFudge, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Start Frame:  {startFrame}");
	s.AppendLine($"  Start Frame Fudge:  {startFrameFudge}");
	s.AppendLine($"  End Frame:  {endFrame}");
	s.AppendLine($"  Loop Start Frame:  {loopStartFrame}");
	s.AppendLine($"  Loop Start Frame Fudge:  {loopStartFrameFudge}");
	s.AppendLine($"  Frame Count:  {frameCount}");
	s.AppendLine($"  Frame Count Fudge:  {frameCountFudge}");
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