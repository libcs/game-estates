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

/*! Root node in NetImmerse .kf files (until version 10.0). */
public class NiSequence : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSequence", NiObject.TYPE);
	/*! The sequence name by which the animation system finds and manages this sequence. */
	internal IndexString name;
	/*!
	 * The name of the NiAVObject serving as the accumulation root. This is where all
	 * accumulated translations, scales, and rotations are applied.
	 */
	internal IndexString accumRootName;
	/*!  */
	internal NiTextKeyExtraData textKeys;
	/*! Divinity 2 */
	internal int unknownInt4;
	/*! Divinity 2 */
	internal int unknownInt5;
	/*!  */
	internal uint numControlledBlocks;
	/*!  */
	internal uint arrayGrowBy;
	/*!  */
	internal IList<ControlledBlock> controlledBlocks;

	public NiSequence() {
	textKeys = null;
	unknownInt4 = (int)0;
	unknownInt5 = (int)0;
	numControlledBlocks = (uint)0;
	arrayGrowBy = (uint)0;
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
public static NiObject Create() => new NiSequence();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out name, s, info);
	if (info.version <= 0x0A010067) {
		Nif.NifStream(out accumRootName, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072)) {
		Nif.NifStream(out unknownInt4, s, info);
		Nif.NifStream(out unknownInt5, s, info);
	}
	Nif.NifStream(out numControlledBlocks, s, info);
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out arrayGrowBy, s, info);
	}
	controlledBlocks = new ControlledBlock[numControlledBlocks];
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
		if (info.version <= 0x0A010067) {
			Nif.NifStream(out controlledBlocks[i1].targetName, s, info);
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
			Nif.NifStream(out controlledBlocks[i1].blendIndex, s, info);
		}
		if ((info.version >= 0x0A01006A) && ((info.userVersion2 > 0))) {
			Nif.NifStream(out controlledBlocks[i1].priority, s, info);
		}
		if ((info.version >= 0x0A010068) && (info.version <= 0x0A010071)) {
			Nif.NifStream(out controlledBlocks[i1].nodeName, s, info);
			Nif.NifStream(out controlledBlocks[i1].propertyType, s, info);
			Nif.NifStream(out controlledBlocks[i1].controllerType, s, info);
			Nif.NifStream(out controlledBlocks[i1].controllerId, s, info);
			Nif.NifStream(out controlledBlocks[i1].interpolatorId, s, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x14010000)) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out controlledBlocks[i1].nodeNameOffset, s, info);
			Nif.NifStream(out controlledBlocks[i1].propertyTypeOffset, s, info);
			Nif.NifStream(out controlledBlocks[i1].controllerTypeOffset, s, info);
			Nif.NifStream(out controlledBlocks[i1].controllerIdOffset, s, info);
			Nif.NifStream(out controlledBlocks[i1].interpolatorIdOffset, s, info);
		}
		if (info.version >= 0x14010001) {
			Nif.NifStream(out (IndexString)controlledBlocks[i1].nodeName, s, info);
			Nif.NifStream(out (IndexString)controlledBlocks[i1].propertyType, s, info);
			Nif.NifStream(out (IndexString)controlledBlocks[i1].controllerType, s, info);
			Nif.NifStream(out (IndexString)controlledBlocks[i1].controllerId, s, info);
			Nif.NifStream(out (IndexString)controlledBlocks[i1].interpolatorId, s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numControlledBlocks = (uint)controlledBlocks.Count;
	Nif.NifStream(name, s, info);
	if (info.version <= 0x0A010067) {
		Nif.NifStream(accumRootName, s, info);
		WriteRef((NiObject)textKeys, s, info, link_map, missing_link_stack);
	}
	if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072)) {
		Nif.NifStream(unknownInt4, s, info);
		Nif.NifStream(unknownInt5, s, info);
	}
	Nif.NifStream(numControlledBlocks, s, info);
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(arrayGrowBy, s, info);
	}
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
		if (info.version <= 0x0A010067) {
			Nif.NifStream(controlledBlocks[i1].targetName, s, info);
		}
		if (info.version >= 0x0A01006A) {
			WriteRef((NiObject)controlledBlocks[i1].interpolator, s, info, link_map, missing_link_stack);
		}
		if (info.version <= 0x14050000) {
			WriteRef((NiObject)controlledBlocks[i1].controller, s, info, link_map, missing_link_stack);
		}
		if ((info.version >= 0x0A010068) && (info.version <= 0x0A01006E)) {
			WriteRef((NiObject)controlledBlocks[i1].blendInterpolator, s, info, link_map, missing_link_stack);
			Nif.NifStream(controlledBlocks[i1].blendIndex, s, info);
		}
		if ((info.version >= 0x0A01006A) && ((info.userVersion2 > 0))) {
			Nif.NifStream(controlledBlocks[i1].priority, s, info);
		}
		if ((info.version >= 0x0A010068) && (info.version <= 0x0A010071)) {
			Nif.NifStream(controlledBlocks[i1].nodeName, s, info);
			Nif.NifStream(controlledBlocks[i1].propertyType, s, info);
			Nif.NifStream(controlledBlocks[i1].controllerType, s, info);
			Nif.NifStream(controlledBlocks[i1].controllerId, s, info);
			Nif.NifStream(controlledBlocks[i1].interpolatorId, s, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x14010000)) {
			WriteRef((NiObject)controlledBlocks[i1].stringPalette, s, info, link_map, missing_link_stack);
			Nif.NifStream(controlledBlocks[i1].nodeNameOffset, s, info);
			Nif.NifStream(controlledBlocks[i1].propertyTypeOffset, s, info);
			Nif.NifStream(controlledBlocks[i1].controllerTypeOffset, s, info);
			Nif.NifStream(controlledBlocks[i1].controllerIdOffset, s, info);
			Nif.NifStream(controlledBlocks[i1].interpolatorIdOffset, s, info);
		}
		if (info.version >= 0x14010001) {
			Nif.NifStream((IndexString)controlledBlocks[i1].nodeName, s, info);
			Nif.NifStream((IndexString)controlledBlocks[i1].propertyType, s, info);
			Nif.NifStream((IndexString)controlledBlocks[i1].controllerType, s, info);
			Nif.NifStream((IndexString)controlledBlocks[i1].controllerId, s, info);
			Nif.NifStream((IndexString)controlledBlocks[i1].interpolatorId, s, info);
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
	numControlledBlocks = (uint)controlledBlocks.Count;
	s.AppendLine($"  Name:  {name}");
	s.AppendLine($"  Accum Root Name:  {accumRootName}");
	s.AppendLine($"  Text Keys:  {textKeys}");
	s.AppendLine($"  Unknown Int 4:  {unknownInt4}");
	s.AppendLine($"  Unknown Int 5:  {unknownInt5}");
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
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x0A010067) {
		textKeys = FixLink<NiTextKeyExtraData>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
		if (info.version >= 0x0A01006A) {
			controlledBlocks[i1].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
		}
		if (info.version <= 0x14050000) {
			controlledBlocks[i1].controller = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
		}
		if ((info.version >= 0x0A010068) && (info.version <= 0x0A01006E)) {
			controlledBlocks[i1].blendInterpolator = FixLink<NiBlendInterpolator>(objects, link_stack, missing_link_stack, info);
		}
		if ((info.version >= 0x0A020000) && (info.version <= 0x14010000)) {
			controlledBlocks[i1].stringPalette = FixLink<NiStringPalette>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (textKeys != null)
		refs.Add((NiObject)textKeys);
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
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < controlledBlocks.Count; i1++) {
	}
	return ptrs;
}


}

}