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

/*! LEGACY (pre-10.1) particle modifier. */
public class NiSphericalCollider : NiParticleModifier {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSphericalCollider", NiParticleModifier.TYPE);
	/*! Unknown. */
	internal float unknownFloat1;
	/*! Unknown. */
	internal ushort unknownShort1;
	/*! Unknown. */
	internal float unknownFloat2;
	/*! Unknown. */
	internal ushort unknownShort2;
	/*! Unknown. */
	internal float unknownFloat3;
	/*! Unknown. */
	internal float unknownFloat4;
	/*! Unknown. */
	internal float unknownFloat5;

	public NiSphericalCollider() {
	unknownFloat1 = 0.0f;
	unknownShort1 = (ushort)0;
	unknownFloat2 = 0.0f;
	unknownShort2 = (ushort)0;
	unknownFloat3 = 0.0f;
	unknownFloat4 = 0.0f;
	unknownFloat5 = 0.0f;
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
public static NiObject Create() => new NiSphericalCollider();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out unknownFloat1, s, info);
	Nif.NifStream(out unknownShort1, s, info);
	Nif.NifStream(out unknownFloat2, s, info);
	if (info.version <= 0x04020002) {
		Nif.NifStream(out unknownShort2, s, info);
	}
	if (info.version >= 0x04020100) {
		Nif.NifStream(out unknownFloat3, s, info);
	}
	Nif.NifStream(out unknownFloat4, s, info);
	Nif.NifStream(out unknownFloat5, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(unknownFloat1, s, info);
	Nif.NifStream(unknownShort1, s, info);
	Nif.NifStream(unknownFloat2, s, info);
	if (info.version <= 0x04020002) {
		Nif.NifStream(unknownShort2, s, info);
	}
	if (info.version >= 0x04020100) {
		Nif.NifStream(unknownFloat3, s, info);
	}
	Nif.NifStream(unknownFloat4, s, info);
	Nif.NifStream(unknownFloat5, s, info);

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	s.AppendLine($"  Unknown Short 1:  {unknownShort1}");
	s.AppendLine($"  Unknown Float 2:  {unknownFloat2}");
	s.AppendLine($"  Unknown Short 2:  {unknownShort2}");
	s.AppendLine($"  Unknown Float 3:  {unknownFloat3}");
	s.AppendLine($"  Unknown Float 4:  {unknownFloat4}");
	s.AppendLine($"  Unknown Float 5:  {unknownFloat5}");
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