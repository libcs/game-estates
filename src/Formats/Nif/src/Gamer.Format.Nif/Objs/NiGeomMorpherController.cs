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
 * DEPRECATED (20.5), replaced by NiMorphMeshModifier.
 *         Time controller for geometry morphing.
 */
public class NiGeomMorpherController : NiInterpController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiGeomMorpherController", NiInterpController.TYPE);
	/*! 1 = UPDATE NORMALS */
	internal ushort extraFlags;
	/*! Geometry morphing data index. */
	internal NiMorphData data;
	/*!  */
	internal byte alwaysUpdate;
	/*!  */
	internal uint numInterpolators;
	/*!  */
	internal IList<NiInterpolator> interpolators;
	/*!  */
	internal IList<MorphWeight> interpolatorWeights;
	/*!  */
	internal uint numUnknownInts;
	/*! Unknown. */
	internal IList<uint> unknownInts;

	public NiGeomMorpherController() {
	extraFlags = (ushort)0;
	data = null;
	alwaysUpdate = (byte)0;
	numInterpolators = (uint)0;
	numUnknownInts = (uint)0;
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
public static NiObject Create() => new NiGeomMorpherController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A000102) {
		Nif.NifStream(out extraFlags, s, info);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	if (info.version >= 0x04000001) {
		Nif.NifStream(out alwaysUpdate, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(out numInterpolators, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x14000005)) {
		interpolators = new Ref[numInterpolators];
		for (var i2 = 0; i2 < interpolators.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if (info.version >= 0x14010003) {
		interpolatorWeights = new MorphWeight[numInterpolators];
		for (var i2 = 0; i2 < interpolatorWeights.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out interpolatorWeights[i2].weight, s, info);
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x14000005) && ((info.userVersion2 > 9))) {
		Nif.NifStream(out numUnknownInts, s, info);
		unknownInts = new uint[numUnknownInts];
		for (var i2 = 0; i2 < unknownInts.Count; i2++) {
			Nif.NifStream(out unknownInts[i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numUnknownInts = (uint)unknownInts.Count;
	numInterpolators = (uint)interpolators.Count;
	if (info.version >= 0x0A000102) {
		Nif.NifStream(extraFlags, s, info);
	}
	WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
	if (info.version >= 0x04000001) {
		Nif.NifStream(alwaysUpdate, s, info);
	}
	if (info.version >= 0x0A01006A) {
		Nif.NifStream(numInterpolators, s, info);
	}
	if ((info.version >= 0x0A01006A) && (info.version <= 0x14000005)) {
		for (var i2 = 0; i2 < interpolators.Count; i2++) {
			WriteRef((NiObject)interpolators[i2], s, info, link_map, missing_link_stack);
		}
	}
	if (info.version >= 0x14010003) {
		for (var i2 = 0; i2 < interpolatorWeights.Count; i2++) {
			WriteRef((NiObject)interpolatorWeights[i2].interpolator, s, info, link_map, missing_link_stack);
			Nif.NifStream(interpolatorWeights[i2].weight, s, info);
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x14000005) && ((info.userVersion2 > 9))) {
		Nif.NifStream(numUnknownInts, s, info);
		for (var i2 = 0; i2 < unknownInts.Count; i2++) {
			Nif.NifStream(unknownInts[i2], s, info);
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
	numUnknownInts = (uint)unknownInts.Count;
	numInterpolators = (uint)interpolators.Count;
	s.AppendLine($"  Extra Flags:  {extraFlags}");
	s.AppendLine($"  Data:  {data}");
	s.AppendLine($"  Always Update:  {alwaysUpdate}");
	s.AppendLine($"  Num Interpolators:  {numInterpolators}");
	array_output_count = 0;
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Interpolators[{i1}]:  {interpolators[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < interpolatorWeights.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Interpolator:  {interpolatorWeights[i1].interpolator}");
		s.AppendLine($"    Weight:  {interpolatorWeights[i1].weight}");
	}
	s.AppendLine($"  Num Unknown Ints:  {numUnknownInts}");
	array_output_count = 0;
	for (var i1 = 0; i1 < unknownInts.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Ints[{i1}]:  {unknownInts[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	data = FixLink<NiMorphData>(objects, link_stack, missing_link_stack, info);
	if ((info.version >= 0x0A01006A) && (info.version <= 0x14000005)) {
		for (var i2 = 0; i2 < interpolators.Count; i2++) {
			interpolators[i2] = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
		}
	}
	if (info.version >= 0x14010003) {
		for (var i2 = 0; i2 < interpolatorWeights.Count; i2++) {
			interpolatorWeights[i2].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (data != null)
		refs.Add((NiObject)data);
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
		if (interpolators[i1] != null)
			refs.Add((NiObject)interpolators[i1]);
	}
	for (var i1 = 0; i1 < interpolatorWeights.Count; i1++) {
		if (interpolatorWeights[i1].interpolator != null)
			refs.Add((NiObject)interpolatorWeights[i1].interpolator);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < interpolators.Count; i1++) {
	}
	for (var i1 = 0; i1 < interpolatorWeights.Count; i1++) {
	}
	return ptrs;
}


}

}