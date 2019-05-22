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
 * NiPortal objects are grouping nodes that support aggressive visibility culling.
 *         They represent flat polygonal regions through which a part of a scene
 * graph can be viewed.
 */
public class NiPortal : NiAVObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPortal", NiAVObject.TYPE);
	/*!  */
	internal ushort portalFlags;
	/*! Unused in 20.x, possibly also 10.x. */
	internal ushort planeCount;
	/*!  */
	internal ushort numVertices;
	/*!  */
	internal IList<Vector3> vertices;
	/*! Root of the scenegraph which is to be seen through this portal. */
	internal NiNode adjoiner;

	public NiPortal() {
	portalFlags = (ushort)0;
	planeCount = (ushort)0;
	numVertices = (ushort)0;
	adjoiner = null;
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
public static NiObject Create() => new NiPortal();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out portalFlags, s, info);
	Nif.NifStream(out planeCount, s, info);
	Nif.NifStream(out numVertices, s, info);
	vertices = new Vector3[numVertices];
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		Nif.NifStream(out vertices[i1], s, info);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numVertices = (ushort)vertices.Count;
	Nif.NifStream(portalFlags, s, info);
	Nif.NifStream(planeCount, s, info);
	Nif.NifStream(numVertices, s, info);
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		Nif.NifStream(vertices[i1], s, info);
	}
	WriteRef((NiObject)adjoiner, s, info, link_map, missing_link_stack);

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
	numVertices = (ushort)vertices.Count;
	s.AppendLine($"  Portal Flags:  {portalFlags}");
	s.AppendLine($"  Plane Count:  {planeCount}");
	s.AppendLine($"  Num Vertices:  {numVertices}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vertices.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Vertices[{i1}]:  {vertices[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Adjoiner:  {adjoiner}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	adjoiner = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (adjoiner != null)
		ptrs.Add((NiObject)adjoiner);
	return ptrs;
}


}

}