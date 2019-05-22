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

/*!  */
public class NiDataStream : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiDataStream", NiObject.TYPE);
	/*!  */
	internal DataStreamUsage usage;
	/*!  */
	internal DataStreamAccess access;
	/*! The size in bytes of this data stream. */
	internal uint numBytes;
	/*!  */
	internal CloningBehavior cloningBehavior;
	/*! Number of regions (such as submeshes). */
	internal uint numRegions;
	/*!
	 * The regions in the mesh. Regions can be used to mark off submeshes which are
	 * independent draw calls.
	 */
	internal IList<Region> regions;
	/*! Number of components of the data (matches corresponding field in MeshData). */
	internal uint numComponents;
	/*! The format of each component in this data stream. */
	internal IList<ComponentFormat> componentFormats;
	/*!  */
	internal IList<byte> data;
	/*!  */
	internal bool streamable;

	public NiDataStream() {
	usage = (DataStreamUsage)0;
	access = (DataStreamAccess)0;
	numBytes = (uint)0;
	cloningBehavior = CloningBehavior.CLONING_SHARE;
	numRegions = (uint)0;
	numComponents = (uint)0;
	streamable = 1;
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
public static NiObject Create() => new NiDataStream();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numBytes, s, info);
	Nif.NifStream(out cloningBehavior, s, info);
	Nif.NifStream(out numRegions, s, info);
	regions = new Region[numRegions];
	for (var i1 = 0; i1 < regions.Count; i1++) {
		Nif.NifStream(out regions[i1].startIndex, s, info);
		Nif.NifStream(out regions[i1].numIndices, s, info);
	}
	Nif.NifStream(out numComponents, s, info);
	componentFormats = new ComponentFormat[numComponents];
	for (var i1 = 0; i1 < componentFormats.Count; i1++) {
		Nif.NifStream(out componentFormats[i1], s, info);
	}
	data = new byte[numBytes];
	for (var i1 = 0; i1 < data.Count; i1++) {
		Nif.NifStream(out data[i1], s, info);
	}
	Nif.NifStream(out streamable, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numComponents = (uint)componentFormats.Count;
	numRegions = (uint)regions.Count;
	numBytes = (uint)data.Count;
	Nif.NifStream(numBytes, s, info);
	Nif.NifStream(cloningBehavior, s, info);
	Nif.NifStream(numRegions, s, info);
	for (var i1 = 0; i1 < regions.Count; i1++) {
		Nif.NifStream(regions[i1].startIndex, s, info);
		Nif.NifStream(regions[i1].numIndices, s, info);
	}
	Nif.NifStream(numComponents, s, info);
	for (var i1 = 0; i1 < componentFormats.Count; i1++) {
		Nif.NifStream(componentFormats[i1], s, info);
	}
	for (var i1 = 0; i1 < data.Count; i1++) {
		Nif.NifStream(data[i1], s, info);
	}
	Nif.NifStream(streamable, s, info);

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
	numComponents = (uint)componentFormats.Count;
	numRegions = (uint)regions.Count;
	numBytes = (uint)data.Count;
	s.AppendLine($"  Usage:  {usage}");
	s.AppendLine($"  Access:  {access}");
	s.AppendLine($"  Num Bytes:  {numBytes}");
	s.AppendLine($"  Cloning Behavior:  {cloningBehavior}");
	s.AppendLine($"  Num Regions:  {numRegions}");
	array_output_count = 0;
	for (var i1 = 0; i1 < regions.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Start Index:  {regions[i1].startIndex}");
		s.AppendLine($"    Num Indices:  {regions[i1].numIndices}");
	}
	s.AppendLine($"  Num Components:  {numComponents}");
	array_output_count = 0;
	for (var i1 = 0; i1 < componentFormats.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Component Formats[{i1}]:  {componentFormats[i1]}");
		array_output_count++;
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < data.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Data[{i1}]:  {data[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Streamable:  {streamable}");
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