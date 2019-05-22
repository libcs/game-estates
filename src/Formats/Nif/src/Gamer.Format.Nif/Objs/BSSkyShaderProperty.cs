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

/*! Skyrim Sky shader block. */
public class BSSkyShaderProperty : BSShaderProperty {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSSkyShaderProperty", BSShaderProperty.TYPE);
	/*!  */
	internal SkyrimShaderPropertyFlags1 shaderFlags1;
	/*!  */
	internal SkyrimShaderPropertyFlags2 shaderFlags2;
	/*!
	 * Offset UVs. Seems to be unused, but it fits with the other Skyrim shader
	 * properties.
	 */
	internal TexCoord uvOffset;
	/*! Offset UV Scale to repeat tiling textures, see above. */
	internal TexCoord uvScale;
	/*! points to an external texture. */
	internal string sourceTexture;
	/*!  */
	internal SkyObjectType skyObjectType;

	public BSSkyShaderProperty() {
	shaderFlags1 = (SkyrimShaderPropertyFlags1)0;
	shaderFlags2 = (SkyrimShaderPropertyFlags2)0;
	uvScale = 1.0, 1.0;
	skyObjectType = (SkyObjectType)0;
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
public static NiObject Create() => new BSSkyShaderProperty();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out shaderFlags1, s, info);
	Nif.NifStream(out shaderFlags2, s, info);
	Nif.NifStream(out uvOffset, s, info);
	Nif.NifStream(out uvScale, s, info);
	Nif.NifStream(out sourceTexture, s, info);
	Nif.NifStream(out skyObjectType, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(shaderFlags1, s, info);
	Nif.NifStream(shaderFlags2, s, info);
	Nif.NifStream(uvOffset, s, info);
	Nif.NifStream(uvScale, s, info);
	Nif.NifStream(sourceTexture, s, info);
	Nif.NifStream(skyObjectType, s, info);

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
	s.AppendLine($"  Shader Flags 1:  {shaderFlags1}");
	s.AppendLine($"  Shader Flags 2:  {shaderFlags2}");
	s.AppendLine($"  UV Offset:  {uvOffset}");
	s.AppendLine($"  UV Scale:  {uvScale}");
	s.AppendLine($"  Source Texture:  {sourceTexture}");
	s.AppendLine($"  Sky Object Type:  {skyObjectType}");
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