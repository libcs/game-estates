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
 * DEPRECATED (20.5), Replaced by NiSkinningLODController.
 *         Level of detail controller for bones.  Priority is arranged from low to
 * high.
 */
public class NiBoneLODController : NiTimeController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBoneLODController", NiTimeController.TYPE);
	/*! Unknown. */
	internal uint lod;
	/*! Number of LODs. */
	internal uint numLods;
	/*! Number of node arrays. */
	internal uint numNodeGroups;
	/*! A list of node sets (each set a sequence of bones). */
	internal IList<NodeSet> nodeGroups;
	/*! Number of shape groups. */
	internal uint numShapeGroups;
	/*! List of shape groups. */
	internal IList<SkinInfoSet> shapeGroups1;
	/*! The size of the second list of shape groups. */
	internal uint numShapeGroups2;
	/*! Group of NiTriShape indices. */
	internal IList<NiTriBasedGeom> shapeGroups2;
	/*! Unknown. */
	internal int unknownInt2;
	/*! Unknown. */
	internal int unknownInt3;

	public NiBoneLODController() {
	lod = (uint)0;
	numLods = (uint)0;
	numNodeGroups = (uint)0;
	numShapeGroups = (uint)0;
	numShapeGroups2 = (uint)0;
	unknownInt2 = (int)0;
	unknownInt3 = (int)0;
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
public static NiObject Create() => new NiBoneLODController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out lod, s, info);
	Nif.NifStream(out numLods, s, info);
	Nif.NifStream(out numNodeGroups, s, info);
	nodeGroups = new NodeSet[numLods];
	for (var i1 = 0; i1 < nodeGroups.Count; i1++) {
		Nif.NifStream(out nodeGroups[i1].numNodes, s, info);
		nodeGroups[i1].nodes = new *[nodeGroups[i1].numNodes];
		for (var i2 = 0; i2 < nodeGroups[i1].nodes.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		Nif.NifStream(out numShapeGroups, s, info);
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		Nif.NifStream(out (uint)numShapeGroups, s, info);
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		shapeGroups1 = new SkinInfoSet[numShapeGroups];
		for (var i2 = 0; i2 < shapeGroups1.Count; i2++) {
			Nif.NifStream(out shapeGroups1[i2].numSkinInfo, s, info);
			shapeGroups1[i2].skinInfo = new SkinInfo[shapeGroups1[i2].numSkinInfo];
			for (var i3 = 0; i3 < shapeGroups1[i2].skinInfo.Count; i3++) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		shapeGroups1 = new SkinInfoSet[numShapeGroups];
		for (var i2 = 0; i2 < shapeGroups1.Count; i2++) {
			Nif.NifStream(out shapeGroups1[i2].numSkinInfo, s, info);
			shapeGroups1[i2].skinInfo = new SkinInfo[shapeGroups1[i2].numSkinInfo];
			for (var i3 = 0; i3 < shapeGroups1[i2].skinInfo.Count; i3++) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		Nif.NifStream(out numShapeGroups2, s, info);
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		Nif.NifStream(out (uint)numShapeGroups2, s, info);
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		shapeGroups2 = new Ref[numShapeGroups2];
		for (var i2 = 0; i2 < shapeGroups2.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		shapeGroups2 = new Ref[numShapeGroups2];
		for (var i2 = 0; i2 < shapeGroups2.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072)) {
		Nif.NifStream(out unknownInt2, s, info);
		Nif.NifStream(out unknownInt3, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numShapeGroups2 = (uint)shapeGroups2.Count;
	numShapeGroups = (uint)shapeGroups1.Count;
	numLods = (uint)nodeGroups.Count;
	Nif.NifStream(lod, s, info);
	Nif.NifStream(numLods, s, info);
	Nif.NifStream(numNodeGroups, s, info);
	for (var i1 = 0; i1 < nodeGroups.Count; i1++) {
		nodeGroups[i1].numNodes = (uint)nodeGroups[i1].nodes.Count;
		Nif.NifStream(nodeGroups[i1].numNodes, s, info);
		for (var i2 = 0; i2 < nodeGroups[i1].nodes.Count; i2++) {
			WriteRef((NiObject)nodeGroups[i1].nodes[i2], s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		Nif.NifStream(numShapeGroups, s, info);
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		Nif.NifStream((uint)numShapeGroups, s, info);
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		for (var i2 = 0; i2 < shapeGroups1.Count; i2++) {
			shapeGroups1[i2].numSkinInfo = (uint)shapeGroups1[i2].skinInfo.Count;
			Nif.NifStream(shapeGroups1[i2].numSkinInfo, s, info);
			for (var i3 = 0; i3 < shapeGroups1[i2].skinInfo.Count; i3++) {
				WriteRef((NiObject)shapeGroups1[i2].skinInfo[i3].shape, s, info, link_map, missing_link_stack);
				WriteRef((NiObject)shapeGroups1[i2].skinInfo[i3].skinInstance, s, info, link_map, missing_link_stack);
			}
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		for (var i2 = 0; i2 < shapeGroups1.Count; i2++) {
			shapeGroups1[i2].numSkinInfo = (uint)shapeGroups1[i2].skinInfo.Count;
			Nif.NifStream(shapeGroups1[i2].numSkinInfo, s, info);
			for (var i3 = 0; i3 < shapeGroups1[i2].skinInfo.Count; i3++) {
				WriteRef((NiObject)shapeGroups1[i2].skinInfo[i3].shape, s, info, link_map, missing_link_stack);
				WriteRef((NiObject)shapeGroups1[i2].skinInfo[i3].skinInstance, s, info, link_map, missing_link_stack);
			}
		}
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		Nif.NifStream(numShapeGroups2, s, info);
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		Nif.NifStream((uint)numShapeGroups2, s, info);
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		for (var i2 = 0; i2 < shapeGroups2.Count; i2++) {
			WriteRef((NiObject)shapeGroups2[i2], s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		for (var i2 = 0; i2 < shapeGroups2.Count; i2++) {
			WriteRef((NiObject)shapeGroups2[i2], s, info, link_map, missing_link_stack);
		}
	}
	if ((info.version >= 0x14030009) && (info.version <= 0x14030009) && (info.userVersion == 131072)) {
		Nif.NifStream(unknownInt2, s, info);
		Nif.NifStream(unknownInt3, s, info);
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
	numShapeGroups2 = (uint)shapeGroups2.Count;
	numShapeGroups = (uint)shapeGroups1.Count;
	numLods = (uint)nodeGroups.Count;
	s.AppendLine($"  LOD:  {lod}");
	s.AppendLine($"  Num LODs:  {numLods}");
	s.AppendLine($"  Num Node Groups:  {numNodeGroups}");
	array_output_count = 0;
	for (var i1 = 0; i1 < nodeGroups.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		nodeGroups[i1].numNodes = (uint)nodeGroups[i1].nodes.Count;
		s.AppendLine($"    Num Nodes:  {nodeGroups[i1].numNodes}");
		array_output_count = 0;
		for (var i2 = 0; i2 < nodeGroups[i1].nodes.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Nodes[{i2}]:  {nodeGroups[i1].nodes[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Num Shape Groups:  {numShapeGroups}");
	array_output_count = 0;
	for (var i1 = 0; i1 < shapeGroups1.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		shapeGroups1[i1].numSkinInfo = (uint)shapeGroups1[i1].skinInfo.Count;
		s.AppendLine($"    Num Skin Info:  {shapeGroups1[i1].numSkinInfo}");
		array_output_count = 0;
		for (var i2 = 0; i2 < shapeGroups1[i1].skinInfo.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      Shape:  {shapeGroups1[i1].skinInfo[i2].shape}");
			s.AppendLine($"      Skin Instance:  {shapeGroups1[i1].skinInfo[i2].skinInstance}");
		}
	}
	s.AppendLine($"  Num Shape Groups 2:  {numShapeGroups2}");
	array_output_count = 0;
	for (var i1 = 0; i1 < shapeGroups2.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Shape Groups 2[{i1}]:  {shapeGroups2[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
	s.AppendLine($"  Unknown Int 3:  {unknownInt3}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < nodeGroups.Count; i1++) {
		for (var i2 = 0; i2 < nodeGroups[i1].nodes.Count; i2++) {
			nodeGroups[i1].nodes[i2] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		for (var i2 = 0; i2 < shapeGroups1.Count; i2++) {
			for (var i3 = 0; i3 < shapeGroups1[i2].skinInfo.Count; i3++) {
				shapeGroups1[i2].skinInfo[i3].shape = FixLink<NiTriBasedGeom>(objects, link_stack, missing_link_stack, info);
				shapeGroups1[i2].skinInfo[i3].skinInstance = FixLink<NiSkinInstance>(objects, link_stack, missing_link_stack, info);
			}
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		for (var i2 = 0; i2 < shapeGroups1.Count; i2++) {
			for (var i3 = 0; i3 < shapeGroups1[i2].skinInfo.Count; i3++) {
				shapeGroups1[i2].skinInfo[i3].shape = FixLink<NiTriBasedGeom>(objects, link_stack, missing_link_stack, info);
				shapeGroups1[i2].skinInfo[i3].skinInstance = FixLink<NiSkinInstance>(objects, link_stack, missing_link_stack, info);
			}
		}
	}
	if ((info.version >= 0x04020200) && (info.userVersion == 0)) {
		for (var i2 = 0; i2 < shapeGroups2.Count; i2++) {
			shapeGroups2[i2] = FixLink<NiTriBasedGeom>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x0A020000) && (info.version <= 0x0A020000) && (info.userVersion == 1)) {
		for (var i2 = 0; i2 < shapeGroups2.Count; i2++) {
			shapeGroups2[i2] = FixLink<NiTriBasedGeom>(objects, link_stack, missing_link_stack, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < nodeGroups.Count; i1++) {
		for (var i2 = 0; i2 < nodeGroups[i1].nodes.Count; i2++) {
		}
	}
	for (var i1 = 0; i1 < shapeGroups1.Count; i1++) {
		for (var i2 = 0; i2 < shapeGroups1[i1].skinInfo.Count; i2++) {
			if (shapeGroups1[i1].skinInfo[i2].skinInstance != null)
				refs.Add((NiObject)shapeGroups1[i1].skinInfo[i2].skinInstance);
		}
	}
	for (var i1 = 0; i1 < shapeGroups1.Count; i1++) {
		for (var i2 = 0; i2 < shapeGroups1[i1].skinInfo.Count; i2++) {
			if (shapeGroups1[i1].skinInfo[i2].skinInstance != null)
				refs.Add((NiObject)shapeGroups1[i1].skinInfo[i2].skinInstance);
		}
	}
	for (var i1 = 0; i1 < shapeGroups2.Count; i1++) {
		if (shapeGroups2[i1] != null)
			refs.Add((NiObject)shapeGroups2[i1]);
	}
	for (var i1 = 0; i1 < shapeGroups2.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < nodeGroups.Count; i1++) {
		for (var i2 = 0; i2 < nodeGroups[i1].nodes.Count; i2++) {
			if (nodeGroups[i1].nodes[i2] != null)
				ptrs.Add((NiObject)nodeGroups[i1].nodes[i2]);
		}
	}
	for (var i1 = 0; i1 < shapeGroups1.Count; i1++) {
		for (var i2 = 0; i2 < shapeGroups1[i1].skinInfo.Count; i2++) {
			if (shapeGroups1[i1].skinInfo[i2].shape != null)
				ptrs.Add((NiObject)shapeGroups1[i1].skinInfo[i2].shape);
		}
	}
	for (var i1 = 0; i1 < shapeGroups1.Count; i1++) {
		for (var i2 = 0; i2 < shapeGroups1[i1].skinInfo.Count; i2++) {
			if (shapeGroups1[i1].skinInfo[i2].shape != null)
				ptrs.Add((NiObject)shapeGroups1[i1].skinInfo[i2].shape);
		}
	}
	for (var i1 = 0; i1 < shapeGroups2.Count; i1++) {
	}
	for (var i1 = 0; i1 < shapeGroups2.Count; i1++) {
	}
	return ptrs;
}


}

}