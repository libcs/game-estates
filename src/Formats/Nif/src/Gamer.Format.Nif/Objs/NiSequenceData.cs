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
 * Root node in Gamebryo .kf files (20.5.0.1 and up).
 *         For 20.5.0.0, "NiSequenceData" is an alias for "NiControllerSequence"
 * and this is not handled in nifxml.
 *         This was not found in any 20.5.0.0 KFs available and they instead use
 * NiControllerSequence directly.
 */
public class NiSequenceData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSequenceData", NiObject.TYPE);
	/*!  */
	internal IndexString name;
	/*!  */
	internal uint numControlledBlocks;
	/*!  */
	internal uint arrayGrowBy;
	/*!  */
	internal IList<ControlledBlock> controlledBlocks;
	/*!  */
	internal uint numEvaluators;
	/*!  */
	internal IList<NiEvaluator> evaluators;
	/*!  */
	internal NiTextKeyExtraData textKeys;
	/*!  */
	internal float duration;
	/*!  */
	internal CycleType cycleType;
	/*!  */
	internal float frequency;
	/*!
	 * The name of the NiAVObject serving as the accumulation root. This is where all
	 * accumulated translations, scales, and rotations are applied.
	 */
	internal IndexString accumRootName;
	/*!  */
	internal AccumFlags accumFlags;

	public NiSequenceData() {
	numControlledBlocks = (uint)0;
	arrayGrowBy = (uint)0;
	numEvaluators = (uint)0;
	textKeys = null;
	duration = 0.0f;
	cycleType = (CycleType)0;
	frequency = 1.0f;
	accumFlags = (AccumFlags)ACCUM_X_FRONT;
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
public static NiObject Create() => new NiSequenceData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out name, s, info);
	if (info.version <= 0x14050001) {
		Nif.NifStream(out numControlledBlocks, s, info);
		Nif.NifStream(out arrayGrowBy, s, info);
		controlledBlocks = new ControlledBlock[numControlledBlocks];
		for (var i2 = 0; i2 < controlledBlocks.Count; i2++) {
			if (info.version <= 0x0A010067) {
				Nif.NifStream(out controlledBlocks[i2].targetName, s, info);
			}
			if (info.version >= 0x0A01006A) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version <= 0x14050000) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if ((info.version >= 0x0A010068) && (info.version <= 0x0A01006E)) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out controlledBlocks[i2].blendIndex, s, info);
			}
			if ((info.version >= 0x0A01006A) && ((info.userVersion2 > 0))) {
				Nif.NifStream(out controlledBlocks[i2].priority, s, info);
			}
			if ((info.version >= 0x0A010068) && (info.version <= 0x0A010071)) {
				Nif.NifStream(out controlledBlocks[i2].nodeName, s, info);
				Nif.NifStream(out controlledBlocks[i2].propertyType, s, info);
				Nif.NifStream(out controlledBlocks[i2].controllerType, s, info);
				Nif.NifStream(out controlledBlocks[i2].controllerId, s, info);
				Nif.NifStream(out controlledBlocks[i2].interpolatorId, s, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x14010000)) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out controlledBlocks[i2].nodeNameOffset, s, info);
				Nif.NifStream(out controlledBlocks[i2].propertyTypeOffset, s, info);
				Nif.NifStream(out controlledBlocks[i2].controllerTypeOffset, s, info);
				Nif.NifStream(out controlledBlocks[i2].controllerIdOffset, s, info);
				Nif.NifStream(out controlledBlocks[i2].interpolatorIdOffset, s, info);
			}
			if (info.version >= 0x14010001) {
				Nif.NifStream(out (IndexString)controlledBlocks[i2].nodeName, s, info);
				Nif.NifStream(out (IndexString)controlledBlocks[i2].propertyType, s, info);
				Nif.NifStream(out (IndexString)controlledBlocks[i2].controllerType, s, info);
				Nif.NifStream(out (IndexString)controlledBlocks[i2].controllerId, s, info);
				Nif.NifStream(out (IndexString)controlledBlocks[i2].interpolatorId, s, info);
			}
		}
	}
	if (info.version >= 0x14050002) {
		Nif.NifStream(out numEvaluators, s, info);
		evaluators = new Ref[numEvaluators];
		for (var i2 = 0; i2 < evaluators.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out duration, s, info);
	Nif.NifStream(out cycleType, s, info);
	Nif.NifStream(out frequency, s, info);
	Nif.NifStream(out accumRootName, s, info);
	Nif.NifStream(out accumFlags, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numEvaluators = (uint)evaluators.Count;
	numControlledBlocks = (uint)controlledBlocks.Count;
	Nif.NifStream(name, s, info);
	if (info.version <= 0x14050001) {
		Nif.NifStream(numControlledBlocks, s, info);
		Nif.NifStream(arrayGrowBy, s, info);
		for (var i2 = 0; i2 < controlledBlocks.Count; i2++) {
			if (info.version <= 0x0A010067) {
				Nif.NifStream(controlledBlocks[i2].targetName, s, info);
			}
			if (info.version >= 0x0A01006A) {
				WriteRef((NiObject)controlledBlocks[i2].interpolator, s, info, link_map, missing_link_stack);
			}
			if (info.version <= 0x14050000) {
				WriteRef((NiObject)controlledBlocks[i2].controller, s, info, link_map, missing_link_stack);
			}
			if ((info.version >= 0x0A010068) && (info.version <= 0x0A01006E)) {
				WriteRef((NiObject)controlledBlocks[i2].blendInterpolator, s, info, link_map, missing_link_stack);
				Nif.NifStream(controlledBlocks[i2].blendIndex, s, info);
			}
			if ((info.version >= 0x0A01006A) && ((info.userVersion2 > 0))) {
				Nif.NifStream(controlledBlocks[i2].priority, s, info);
			}
			if ((info.version >= 0x0A010068) && (info.version <= 0x0A010071)) {
				Nif.NifStream(controlledBlocks[i2].nodeName, s, info);
				Nif.NifStream(controlledBlocks[i2].propertyType, s, info);
				Nif.NifStream(controlledBlocks[i2].controllerType, s, info);
				Nif.NifStream(controlledBlocks[i2].controllerId, s, info);
				Nif.NifStream(controlledBlocks[i2].interpolatorId, s, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x14010000)) {
				WriteRef((NiObject)controlledBlocks[i2].stringPalette, s, info, link_map, missing_link_stack);
				Nif.NifStream(controlledBlocks[i2].nodeNameOffset, s, info);
				Nif.NifStream(controlledBlocks[i2].propertyTypeOffset, s, info);
				Nif.NifStream(controlledBlocks[i2].controllerTypeOffset, s, info);
				Nif.NifStream(controlledBlocks[i2].controllerIdOffset, s, info);
				Nif.NifStream(controlledBlocks[i2].interpolatorIdOffset, s, info);
			}
			if (info.version >= 0x14010001) {
				Nif.NifStream((IndexString)controlledBlocks[i2].nodeName, s, info);
				Nif.NifStream((IndexString)controlledBlocks[i2].propertyType, s, info);
				Nif.NifStream((IndexString)controlledBlocks[i2].controllerType, s, info);
				Nif.NifStream((IndexString)controlledBlocks[i2].controllerId, s, info);
				Nif.NifStream((IndexString)controlledBlocks[i2].interpolatorId, s, info);
			}
		}
	}
	if (info.version >= 0x14050002) {
		Nif.NifStream(numEvaluators, s, info);
		for (var i2 = 0; i2 < evaluators.Count; i2++) {
			WriteRef((NiObject)evaluators[i2], s, info, link_map, missing_link_stack);
		}
	}
	WriteRef((NiObject)textKeys, s, info, link_map, missing_link_stack);
	Nif.NifStream(duration, s, info);
	Nif.NifStream(cycleType, s, info);
	Nif.NifStream(frequency, s, info);
	Nif.NifStream(accumRootName, s, info);
	Nif.NifStream(accumFlags, s, info);

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
	numEvaluators = (uint)evaluators.Count;
	numControlledBlocks = (uint)controlledBlocks.Count;
	s.AppendLine($"  Name:  {name}");
	s.AppendLine($"  Num Controlled Blocks:  {numControlledBlocks}");
	s.AppendLine($"  Array Grow By:  {arrayGrowBy}");
	array_output_count = 0;
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Target Name:  {controlledBlocks[i1].targetName}");
		s.AppendLine($"    Interpolator:  {controlledBlocks[i1].interpolator}");
		s.AppendLine($"    Controller:  {controlledBlocks[i1].controller}");
		s.AppendLine($"    Blend Interpolator:  {controlledBlocks[i1].blendInterpolator}");
		s.AppendLine($"    Blend Index:  {controlledBlocks[i1].blendIndex}");
		s.AppendLine($"    Priority:  {controlledBlocks[i1].priority}");
		s.AppendLine($"    Node Name:  {controlledBlocks[i1].nodeName}");
		s.AppendLine($"    Property Type:  {controlledBlocks[i1].propertyType}");
		s.AppendLine($"    Controller Type:  {controlledBlocks[i1].controllerType}");
		s.AppendLine($"    Controller ID:  {controlledBlocks[i1].controllerId}");
		s.AppendLine($"    Interpolator ID:  {controlledBlocks[i1].interpolatorId}");
		s.AppendLine($"    String Palette:  {controlledBlocks[i1].stringPalette}");
		s.AppendLine($"    Node Name Offset:  {controlledBlocks[i1].nodeNameOffset}");
		s.AppendLine($"    Property Type Offset:  {controlledBlocks[i1].propertyTypeOffset}");
		s.AppendLine($"    Controller Type Offset:  {controlledBlocks[i1].controllerTypeOffset}");
		s.AppendLine($"    Controller ID Offset:  {controlledBlocks[i1].controllerIdOffset}");
		s.AppendLine($"    Interpolator ID Offset:  {controlledBlocks[i1].interpolatorIdOffset}");
	}
	s.AppendLine($"  Num Evaluators:  {numEvaluators}");
	array_output_count = 0;
	for (var i1 = 0; i1 < evaluators.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Evaluators[{i1}]:  {evaluators[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Text Keys:  {textKeys}");
	s.AppendLine($"  Duration:  {duration}");
	s.AppendLine($"  Cycle Type:  {cycleType}");
	s.AppendLine($"  Frequency:  {frequency}");
	s.AppendLine($"  Accum Root Name:  {accumRootName}");
	s.AppendLine($"  Accum Flags:  {accumFlags}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x14050001) {
		for (var i2 = 0; i2 < controlledBlocks.Count; i2++) {
			if (info.version >= 0x0A01006A) {
				controlledBlocks[i2].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
			}
			if (info.version <= 0x14050000) {
				controlledBlocks[i2].controller = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
			}
			if ((info.version >= 0x0A010068) && (info.version <= 0x0A01006E)) {
				controlledBlocks[i2].blendInterpolator = FixLink<NiBlendInterpolator>(objects, link_stack, missing_link_stack, info);
			}
			if ((info.version >= 0x0A020000) && (info.version <= 0x14010000)) {
				controlledBlocks[i2].stringPalette = FixLink<NiStringPalette>(objects, link_stack, missing_link_stack, info);
			}
		}
	}
	if (info.version >= 0x14050002) {
		for (var i2 = 0; i2 < evaluators.Count; i2++) {
			evaluators[i2] = FixLink<NiEvaluator>(objects, link_stack, missing_link_stack, info);
		}
	}
	textKeys = FixLink<NiTextKeyExtraData>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
		if (controlledBlocks[i1].interpolator != null)
			refs.Add((NiObject)controlledBlocks[i1].interpolator);
		if (controlledBlocks[i1].controller != null)
			refs.Add((NiObject)controlledBlocks[i1].controller);
		if (controlledBlocks[i1].blendInterpolator != null)
			refs.Add((NiObject)controlledBlocks[i1].blendInterpolator);
		if (controlledBlocks[i1].stringPalette != null)
			refs.Add((NiObject)controlledBlocks[i1].stringPalette);
	}
	for (var i1 = 0; i1 < evaluators.Count; i1++) {
		if (evaluators[i1] != null)
			refs.Add((NiObject)evaluators[i1]);
	}
	if (textKeys != null)
		refs.Add((NiObject)textKeys);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
	}
	for (var i1 = 0; i1 < evaluators.Count; i1++) {
	}
	return ptrs;
}


}

}