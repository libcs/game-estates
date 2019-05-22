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

/*! Bethesda-specific property. */
public class BSShaderProperty : NiShadeProperty {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSShaderProperty", NiShadeProperty.TYPE);
	/*!  */
	internal BSShaderType shaderType;
	/*!  */
	internal BSShaderFlags shaderFlags;
	/*!  */
	internal BSShaderFlags2 shaderFlags2;
	/*! Scales the intensity of the environment/cube map. */
	internal float environmentMapScale;

	public BSShaderProperty() {
	shaderType = BSShaderType.SHADER_DEFAULT;
	shaderFlags = (BSShaderFlags)0x82000000;
	shaderFlags2 = (BSShaderFlags2)1;
	environmentMapScale = 1.0f;
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
public static NiObject Create() => new BSShaderProperty();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(out shaderType, s, info);
		Nif.NifStream(out shaderFlags, s, info);
		Nif.NifStream(out shaderFlags2, s, info);
		Nif.NifStream(out environmentMapScale, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if ((info.userVersion2 <= 34)) {
		Nif.NifStream(shaderType, s, info);
		Nif.NifStream(shaderFlags, s, info);
		Nif.NifStream(shaderFlags2, s, info);
		Nif.NifStream(environmentMapScale, s, info);
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
	s.AppendLine($"  Shader Type:  {shaderType}");
	s.AppendLine($"  Shader Flags:  {shaderFlags}");
	s.AppendLine($"  Shader Flags 2:  {shaderFlags2}");
	s.AppendLine($"  Environment Map Scale:  {environmentMapScale}");
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