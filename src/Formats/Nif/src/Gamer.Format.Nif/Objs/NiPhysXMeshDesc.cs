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

/*! Holds mesh data for streaming. */
public class NiPhysXMeshDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXMeshDesc", NiObject.TYPE);
	/*!  */
	internal bool isConvex;
	/*!  */
	internal IndexString meshName;
	/*!  */
	internal ByteArray meshData;
	/*!  */
	internal ushort meshSize;
	/*!  */
	internal uint meshFlags;
	/*!  */
	internal uint meshPagingMode;
	/*!  */
	internal bool isHardware;
	/*!  */
	internal byte flags;

	public NiPhysXMeshDesc() {
	isConvex = false;
	meshSize = (ushort)0;
	meshFlags = (uint)0;
	meshPagingMode = (uint)0;
	isHardware = false;
	flags = (byte)0;
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
public static NiObject Create() => new NiPhysXMeshDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if (info.version <= 0x14030004) {
		Nif.NifStream(out isConvex, s, info);
	}
	Nif.NifStream(out meshName, s, info);
	Nif.NifStream(out meshData.dataSize, s, info);
	meshData.data = new byte[meshData.dataSize];
	for (var i1 = 0; i1 < meshData.data.Count; i1++) {
		Nif.NifStream(out meshData.data[i1], s, info);
	}
	if ((info.version >= 0x14030005) && (info.version <= 0x1E020002)) {
		Nif.NifStream(out meshSize, s, info);
		meshData = new ushort[meshSize];
		for (var i2 = 0; i2 < meshData.Count; i2++) {
			Nif.NifStream(out (ushort)meshData[i2], s, info);
		}
	}
	Nif.NifStream(out meshFlags, s, info);
	if (info.version >= 0x14030001) {
		Nif.NifStream(out meshPagingMode, s, info);
	}
	if ((info.version >= 0x14030002) && (info.version <= 0x14030004)) {
		Nif.NifStream(out isHardware, s, info);
	}
	if (info.version >= 0x14030005) {
		Nif.NifStream(out flags, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	meshSize = (ushort)meshData.Count;
	if (info.version <= 0x14030004) {
		Nif.NifStream(isConvex, s, info);
	}
	Nif.NifStream(meshName, s, info);
	meshData.dataSize = (uint)meshData.data.Count;
	Nif.NifStream(meshData.dataSize, s, info);
	for (var i1 = 0; i1 < meshData.data.Count; i1++) {
		Nif.NifStream(meshData.data[i1], s, info);
	}
	if ((info.version >= 0x14030005) && (info.version <= 0x1E020002)) {
		Nif.NifStream(meshSize, s, info);
		for (var i2 = 0; i2 < meshData.Count; i2++) {
			Nif.NifStream((ushort)meshData[i2], s, info);
		}
	}
	Nif.NifStream(meshFlags, s, info);
	if (info.version >= 0x14030001) {
		Nif.NifStream(meshPagingMode, s, info);
	}
	if ((info.version >= 0x14030002) && (info.version <= 0x14030004)) {
		Nif.NifStream(isHardware, s, info);
	}
	if (info.version >= 0x14030005) {
		Nif.NifStream(flags, s, info);
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
	meshSize = (ushort)meshData.Count;
	s.AppendLine($"  Is Convex:  {isConvex}");
	s.AppendLine($"  Mesh Name:  {meshName}");
	meshData.dataSize = (uint)meshData.data.Count;
	s.AppendLine($"  Data Size:  {meshData.dataSize}");
	array_output_count = 0;
	for (var i1 = 0; i1 < meshData.data.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Data[{i1}]:  {meshData.data[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Mesh Size:  {meshSize}");
	s.AppendLine($"  Mesh Flags:  {meshFlags}");
	s.AppendLine($"  Mesh Paging Mode:  {meshPagingMode}");
	s.AppendLine($"  Is Hardware:  {isHardware}");
	s.AppendLine($"  Flags:  {flags}");
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