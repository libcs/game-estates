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
 * A mesh modifier that uses particle system data to generate aligned quads for
 * each particle.
 */
public class NiPSAlignedQuadGenerator : NiMeshModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSAlignedQuadGenerator", NiMeshModifier.TYPE);
	/*!  */
	internal float scaleAmountU;
	/*!  */
	internal float scaleLimitU;
	/*!  */
	internal float scaleRestU;
	/*!  */
	internal float scaleAmountV;
	/*!  */
	internal float scaleLimitV;
	/*!  */
	internal float scaleRestV;
	/*!  */
	internal float centerU;
	/*!  */
	internal float centerV;
	/*!  */
	internal bool uvScrolling;
	/*!  */
	internal ushort numFramesAcross;
	/*!  */
	internal ushort numFramesDown;
	/*!  */
	internal bool pingPong;
	/*!  */
	internal ushort initialFrame;
	/*!  */
	internal float initialFrameVariation;
	/*!  */
	internal ushort numFrames;
	/*!  */
	internal float numFramesVariation;
	/*!  */
	internal float initialTime;
	/*!  */
	internal float finalTime;

	public NiPSAlignedQuadGenerator() {
	scaleAmountU = 0.0f;
	scaleLimitU = 0.0f;
	scaleRestU = 0.0f;
	scaleAmountV = 0.0f;
	scaleLimitV = 0.0f;
	scaleRestV = 0.0f;
	centerU = 0.0f;
	centerV = 0.0f;
	uvScrolling = false;
	numFramesAcross = (ushort)0;
	numFramesDown = (ushort)0;
	pingPong = false;
	initialFrame = (ushort)0;
	initialFrameVariation = 0.0f;
	numFrames = (ushort)0;
	numFramesVariation = 0.0f;
	initialTime = 0.0f;
	finalTime = 0.0f;
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
public static NiObject Create() => new NiPSAlignedQuadGenerator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out scaleAmountU, s, info);
	Nif.NifStream(out scaleLimitU, s, info);
	Nif.NifStream(out scaleRestU, s, info);
	Nif.NifStream(out scaleAmountV, s, info);
	Nif.NifStream(out scaleLimitV, s, info);
	Nif.NifStream(out scaleRestV, s, info);
	Nif.NifStream(out centerU, s, info);
	Nif.NifStream(out centerV, s, info);
	Nif.NifStream(out uvScrolling, s, info);
	Nif.NifStream(out numFramesAcross, s, info);
	Nif.NifStream(out numFramesDown, s, info);
	Nif.NifStream(out pingPong, s, info);
	Nif.NifStream(out initialFrame, s, info);
	Nif.NifStream(out initialFrameVariation, s, info);
	Nif.NifStream(out numFrames, s, info);
	Nif.NifStream(out numFramesVariation, s, info);
	Nif.NifStream(out initialTime, s, info);
	Nif.NifStream(out finalTime, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(scaleAmountU, s, info);
	Nif.NifStream(scaleLimitU, s, info);
	Nif.NifStream(scaleRestU, s, info);
	Nif.NifStream(scaleAmountV, s, info);
	Nif.NifStream(scaleLimitV, s, info);
	Nif.NifStream(scaleRestV, s, info);
	Nif.NifStream(centerU, s, info);
	Nif.NifStream(centerV, s, info);
	Nif.NifStream(uvScrolling, s, info);
	Nif.NifStream(numFramesAcross, s, info);
	Nif.NifStream(numFramesDown, s, info);
	Nif.NifStream(pingPong, s, info);
	Nif.NifStream(initialFrame, s, info);
	Nif.NifStream(initialFrameVariation, s, info);
	Nif.NifStream(numFrames, s, info);
	Nif.NifStream(numFramesVariation, s, info);
	Nif.NifStream(initialTime, s, info);
	Nif.NifStream(finalTime, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Scale Amount U:  {scaleAmountU}");
	s.AppendLine($"  Scale Limit U:  {scaleLimitU}");
	s.AppendLine($"  Scale Rest U:  {scaleRestU}");
	s.AppendLine($"  Scale Amount V:  {scaleAmountV}");
	s.AppendLine($"  Scale Limit V:  {scaleLimitV}");
	s.AppendLine($"  Scale Rest V:  {scaleRestV}");
	s.AppendLine($"  Center U :  {centerU}");
	s.AppendLine($"  Center V:  {centerV}");
	s.AppendLine($"  UV Scrolling:  {uvScrolling}");
	s.AppendLine($"  Num Frames Across:  {numFramesAcross}");
	s.AppendLine($"  Num Frames Down:  {numFramesDown}");
	s.AppendLine($"  Ping Pong:  {pingPong}");
	s.AppendLine($"  Initial Frame:  {initialFrame}");
	s.AppendLine($"  Initial Frame Variation:  {initialFrameVariation}");
	s.AppendLine($"  Num Frames:  {numFrames}");
	s.AppendLine($"  Num Frames Variation:  {numFramesVariation}");
	s.AppendLine($"  Initial Time:  {initialTime}");
	s.AppendLine($"  Final Time:  {finalTime}");
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