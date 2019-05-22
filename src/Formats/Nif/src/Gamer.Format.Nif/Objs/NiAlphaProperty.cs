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

/*! Transparency. Flags 0x00ED. */
public class NiAlphaProperty : NiProperty {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiAlphaProperty", NiProperty.TYPE);
	/*!
	 * Bit 0 : alpha blending enable
	 *             Bits 1-4 : source blend mode
	 *             Bits 5-8 : destination blend mode
	 *             Bit 9 : alpha test enable
	 *             Bit 10-12 : alpha test mode
	 *             Bit 13 : no sorter flag ( disables triangle sorting )
	 * 
	 *             blend modes (glBlendFunc):
	 *             0000 GL_ONE
	 *             0001 GL_ZERO
	 *             0010 GL_SRC_COLOR
	 *             0011 GL_ONE_MINUS_SRC_COLOR
	 *             0100 GL_DST_COLOR
	 *             0101 GL_ONE_MINUS_DST_COLOR
	 *             0110 GL_SRC_ALPHA
	 *             0111 GL_ONE_MINUS_SRC_ALPHA
	 *             1000 GL_DST_ALPHA
	 *             1001 GL_ONE_MINUS_DST_ALPHA
	 *             1010 GL_SRC_ALPHA_SATURATE
	 * 
	 *             test modes (glAlphaFunc):
	 *             000 GL_ALWAYS
	 *             001 GL_LESS
	 *             010 GL_EQUAL
	 *             011 GL_LEQUAL
	 *             100 GL_GREATER
	 *             101 GL_NOTEQUAL
	 *             110 GL_GEQUAL
	 *             111 GL_NEVER
	 */
	internal ushort flags;
	/*! Threshold for alpha testing (see: glAlphaFunc) */
	internal byte threshold;
	/*! Unknown */
	internal ushort unknownShort1;
	/*! Unknown */
	internal uint unknownInt2;

	public NiAlphaProperty() {
	flags = (ushort)4844;
	threshold = (byte)128;
	unknownShort1 = (ushort)0;
	unknownInt2 = (uint)0;
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
public static NiObject Create() => new NiAlphaProperty();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out threshold, s, info);
	if (info.version <= 0x02030000) {
		Nif.NifStream(out unknownShort1, s, info);
		Nif.NifStream(out unknownInt2, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(flags, s, info);
	Nif.NifStream(threshold, s, info);
	if (info.version <= 0x02030000) {
		Nif.NifStream(unknownShort1, s, info);
		Nif.NifStream(unknownInt2, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	s.Append(base.AsString());
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Threshold:  {threshold}");
	s.AppendLine($"  Unknown Short 1:  {unknownShort1}");
	s.AppendLine($"  Unknown Int 2:  {unknownInt2}");
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