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
 * An NiShadowGenerator object is attached to an NiDynamicEffect object to inform
 * the shadowing system that the effect produces shadows.
 */
public class NiShadowGenerator : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiShadowGenerator", NiObject.TYPE);
	/*!  */
	internal IndexString name;
	/*!  */
	internal ushort flags;
	/*!  */
	internal uint numShadowCasters;
	/*!  */
	internal IList<NiNode> shadowCasters;
	/*!  */
	internal uint numShadowReceivers;
	/*!  */
	internal IList<NiNode> shadowReceivers;
	/*!  */
	internal NiDynamicEffect target;
	/*!  */
	internal float depthBias;
	/*!  */
	internal ushort sizeHint;
	/*!  */
	internal float nearClippingDistance;
	/*!  */
	internal float farClippingDistance;
	/*!  */
	internal float directionalLightFrustumWidth;

	public NiShadowGenerator() {
	flags = (ushort)0;
	numShadowCasters = (uint)0;
	numShadowReceivers = (uint)0;
	target = null;
	depthBias = 0.98f;
	sizeHint = (ushort)0;
	nearClippingDistance = 0.0f;
	farClippingDistance = 0.0f;
	directionalLightFrustumWidth = 0.0f;
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
public static NiObject Create() => new NiShadowGenerator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out name, s, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out numShadowCasters, s, info);
	shadowCasters = new Ref[numShadowCasters];
	for (var i1 = 0; i1 < shadowCasters.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numShadowReceivers, s, info);
	shadowReceivers = new Ref[numShadowReceivers];
	for (var i1 = 0; i1 < shadowReceivers.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out depthBias, s, info);
	Nif.NifStream(out sizeHint, s, info);
	if (info.version >= 0x14030007) {
		Nif.NifStream(out nearClippingDistance, s, info);
		Nif.NifStream(out farClippingDistance, s, info);
		Nif.NifStream(out directionalLightFrustumWidth, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numShadowReceivers = (uint)shadowReceivers.Count;
	numShadowCasters = (uint)shadowCasters.Count;
	Nif.NifStream(name, s, info);
	Nif.NifStream(flags, s, info);
	Nif.NifStream(numShadowCasters, s, info);
	for (var i1 = 0; i1 < shadowCasters.Count; i1++) {
		WriteRef((NiObject)shadowCasters[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numShadowReceivers, s, info);
	for (var i1 = 0; i1 < shadowReceivers.Count; i1++) {
		WriteRef((NiObject)shadowReceivers[i1], s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)target, s, info, link_map, missing_link_stack);
	Nif.NifStream(depthBias, s, info);
	Nif.NifStream(sizeHint, s, info);
	if (info.version >= 0x14030007) {
		Nif.NifStream(nearClippingDistance, s, info);
		Nif.NifStream(farClippingDistance, s, info);
		Nif.NifStream(directionalLightFrustumWidth, s, info);
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
	numShadowReceivers = (uint)shadowReceivers.Count;
	numShadowCasters = (uint)shadowCasters.Count;
	s.AppendLine($"  Name:  {name}");
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Num Shadow Casters:  {numShadowCasters}");
	array_output_count = 0;
	for (var i1 = 0; i1 < shadowCasters.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Shadow Casters[{i1}]:  {shadowCasters[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Shadow Receivers:  {numShadowReceivers}");
	array_output_count = 0;
	for (var i1 = 0; i1 < shadowReceivers.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Shadow Receivers[{i1}]:  {shadowReceivers[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Target:  {target}");
	s.AppendLine($"  Depth Bias:  {depthBias}");
	s.AppendLine($"  Size Hint:  {sizeHint}");
	s.AppendLine($"  Near Clipping Distance:  {nearClippingDistance}");
	s.AppendLine($"  Far Clipping Distance:  {farClippingDistance}");
	s.AppendLine($"  Directional Light Frustum Width:  {directionalLightFrustumWidth}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < shadowCasters.Count; i1++) {
		shadowCasters[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < shadowReceivers.Count; i1++) {
		shadowReceivers[i1] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
	}
	target = FixLink<NiDynamicEffect>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < shadowCasters.Count; i1++) {
		if (shadowCasters[i1] != null)
			refs.Add((NiObject)shadowCasters[i1]);
	}
	for (var i1 = 0; i1 < shadowReceivers.Count; i1++) {
		if (shadowReceivers[i1] != null)
			refs.Add((NiObject)shadowReceivers[i1]);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < shadowCasters.Count; i1++) {
	}
	for (var i1 = 0; i1 < shadowReceivers.Count; i1++) {
	}
	if (target != null)
		ptrs.Add((NiObject)target);
	return ptrs;
}


}

}