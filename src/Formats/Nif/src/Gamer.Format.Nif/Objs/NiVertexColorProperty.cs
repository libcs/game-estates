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
 * Property of vertex colors. This object is referred to by the root object of the
 * NIF file whenever some NiTriShapeData object has vertex colors with non-default
 * settings; if not present, vertex colors have vertex_mode=2 and lighting_mode=1.
 */
public class NiVertexColorProperty : NiProperty {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiVertexColorProperty", NiProperty.TYPE);
	/*!
	 * Bits 0-2: Unknown
	 *             Bit 3: Lighting Mode
	 *             Bits 4-5: Vertex Mode
	 */
	internal ushort flags;
	/*! In Flags from 20.1.0.3 on. */
	internal VertMode vertexMode;
	/*! In Flags from 20.1.0.3 on. */
	internal LightMode lightingMode;

	public NiVertexColorProperty() {
	flags = (ushort)0;
	vertexMode = (VertMode)0;
	lightingMode = (LightMode)0;
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
public static NiObject Create() => new NiVertexColorProperty();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out flags, s, info);
	if (info.version <= 0x14000005) {
		Nif.NifStream(out vertexMode, s, info);
		Nif.NifStream(out lightingMode, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(flags, s, info);
	if (info.version <= 0x14000005) {
		Nif.NifStream(vertexMode, s, info);
		Nif.NifStream(lightingMode, s, info);
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
	s.AppendLine($"  Vertex Mode:  {vertexMode}");
	s.AppendLine($"  Lighting Mode:  {lightingMode}");
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