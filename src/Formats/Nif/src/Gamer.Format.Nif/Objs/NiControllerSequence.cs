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

/*! Root node in Gamebryo .kf files (version 10.0.1.0 and up). */
public class NiControllerSequence : NiSequence {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiControllerSequence", NiSequence.TYPE);
	/*!
	 * The weight of a sequence describes how it blends with other sequences at the
	 * same priority.
	 */
	internal float weight;
	/*!  */
	internal NiTextKeyExtraData textKeys;
	/*!  */
	internal CycleType cycleType;
	/*!  */
	internal float frequency;
	/*!  */
	internal float phase;
	/*!  */
	internal float startTime;
	/*!  */
	internal float stopTime;
	/*!  */
	internal bool playBackwards;
	/*! The owner of this sequence. */
	internal NiControllerManager manager;
	/*!
	 * The name of the NiAVObject serving as the accumulation root. This is where all
	 * accumulated translations, scales, and rotations are applied.
	 */
	internal IndexString accumRootName;
	/*!  */
	internal AccumFlags accumFlags;
	/*!  */
	internal NiStringPalette stringPalette;
	/*!  */
	internal BSAnimNotes animNotes;
	/*!  */
	internal ushort numAnimNoteArrays;
	/*!  */
	internal IList<BSAnimNotes> animNoteArrays;

	public NiControllerSequence() {
	weight = 1.0f;
	textKeys = null;
	cycleType = (CycleType)0;
	frequency = 1.0f;
	phase = 0.0f;
	startTime = 3.402823466e+38f;
	stopTime = -3.402823466e+38f;
	playBackwards = false;
	manager = null;
	accumFlags = (AccumFlags)ACCUM_X_FRONT;
	stringPalette = null;
	animNotes = null;
	numAnimNoteArrays = (ushort)0;
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
public static NiObject Create() => new NiControllerSequence();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out weight, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out cycleType, s, info);
		Nif.NifStream(out frequency, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A040001)) {
		Nif.NifStream(out phase, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out startTime, s, info);
		Nif.NifStream(out stopTime, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A01006A)) {
		Nif.NifStream(out playBackwards, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out accumRootName, s, info);
	}
	if (info.version >= 0x14030008) {
		Nif.NifStream(out accumFlags, s, info);
	}
	if ((info.version >= 0x0A010071) && (info.version <= 0x14010000)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.version >= 0x14020007) && (((info.userVersion2 >= 24) && (info.userVersion2 <= 28)))) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.version >= 0x14020007) && ((info.userVersion2 > 28))) {
		Nif.NifStream(out numAnimNoteArrays, s, info);
		animNoteArrays = new Ref[numAnimNoteArrays];
		for (var i2 = 0; i2 < animNoteArrays.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numAnimNoteArrays = (ushort)animNoteArrays.Count;
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(weight, s, info);
		WriteRef((NiObject)textKeys, s, info, link_map, missing_link_stack);
		Nif.NifStream(cycleType, s, info);
		Nif.NifStream(frequency, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A040001)) {
		Nif.NifStream(phase, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(startTime, s, info);
		Nif.NifStream(stopTime, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x0A01006A)) {
		Nif.NifStream(playBackwards, s, info);
	}
	if (info.version >= 0x0A01006A) {
		WriteRef((NiObject)manager, s, info, link_map, missing_link_stack);
		Nif.NifStream(accumRootName, s, info);
	}
	if (info.version >= 0x14030008) {
		Nif.NifStream(accumFlags, s, info);
	}
	if ((info.version >= 0x0A010071) && (info.version <= 0x14010000)) {
		WriteRef((NiObject)stringPalette, s, info, link_map, missing_link_stack);
	}
	if ((info.version >= 0x14020007) && (((info.userVersion2 >= 24) && (info.userVersion2 <= 28)))) {
		WriteRef((NiObject)animNotes, s, info, link_map, missing_link_stack);
	}
	if ((info.version >= 0x14020007) && ((info.userVersion2 > 28))) {
		Nif.NifStream(numAnimNoteArrays, s, info);
		for (var i2 = 0; i2 < animNoteArrays.Count; i2++) {
			WriteRef((NiObject)animNoteArrays[i2], s, info, link_map, missing_link_stack);
		}
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
	numAnimNoteArrays = (ushort)animNoteArrays.Count;
	s.AppendLine($"  Weight:  {weight}");
	s.AppendLine($"  Text Keys:  {textKeys}");
	s.AppendLine($"  Cycle Type:  {cycleType}");
	s.AppendLine($"  Frequency:  {frequency}");
	s.AppendLine($"  Phase:  {phase}");
	s.AppendLine($"  Start Time:  {startTime}");
	s.AppendLine($"  Stop Time:  {stopTime}");
	s.AppendLine($"  Play Backwards:  {playBackwards}");
	s.AppendLine($"  Manager:  {manager}");
	s.AppendLine($"  Accum Root Name:  {accumRootName}");
	s.AppendLine($"  Accum Flags:  {accumFlags}");
	s.AppendLine($"  String Palette:  {stringPalette}");
	s.AppendLine($"  Anim Notes:  {animNotes}");
	s.AppendLine($"  Num Anim Note Arrays:  {numAnimNoteArrays}");
	array_output_count = 0;
	for (var i1 = 0; i1 < animNoteArrays.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Anim Note Arrays[{i1}]:  {animNoteArrays[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x0A01006A) {
		textKeys = FixLink<NiTextKeyExtraData>(objects, link_stack, missing_link_stack, info);
		manager = FixLink<NiControllerManager>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.version >= 0x0A010071) && (info.version <= 0x14010000)) {
		stringPalette = FixLink<NiStringPalette>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.version >= 0x14020007) && (((info.userVersion2 >= 24) && (info.userVersion2 <= 28)))) {
		animNotes = FixLink<BSAnimNotes>(objects, link_stack, missing_link_stack, info);
	}
	if ((info.version >= 0x14020007) && ((info.userVersion2 > 28))) {
		for (var i2 = 0; i2 < animNoteArrays.Count; i2++) {
			animNoteArrays[i2] = FixLink<BSAnimNotes>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (textKeys != null)
		refs.Add((NiObject)textKeys);
	if (stringPalette != null)
		refs.Add((NiObject)stringPalette);
	if (animNotes != null)
		refs.Add((NiObject)animNotes);
	for (var i1 = 0; i1 < animNoteArrays.Count; i1++) {
		if (animNoteArrays[i1] != null)
			refs.Add((NiObject)animNoteArrays[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (manager != null)
		ptrs.Add((NiObject)manager);
	for (var i1 = 0; i1 < animNoteArrays.Count; i1++) {
	}
	return ptrs;
}


}

}