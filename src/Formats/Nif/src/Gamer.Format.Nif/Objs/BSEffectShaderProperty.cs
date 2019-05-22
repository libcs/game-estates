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

/*! Bethesda effect shader property for Skyrim and later. */
public class BSEffectShaderProperty : BSShaderProperty {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("BSEffectShaderProperty", BSShaderProperty.TYPE);
	/*!  */
	internal SkyrimShaderPropertyFlags1 shaderFlags1_sk;
	/*!  */
	internal SkyrimShaderPropertyFlags2 shaderFlags2_sk;
	/*!  */
	internal Fallout4ShaderPropertyFlags1 shaderFlags1_fo4;
	/*!  */
	internal Fallout4ShaderPropertyFlags2 shaderFlags2_fo4;
	/*! Offset UVs */
	internal TexCoord uvOffset;
	/*! Offset UV Scale to repeat tiling textures */
	internal TexCoord uvScale;
	/*! points to an external texture. */
	internal string sourceTexture;
	/*! How to handle texture borders. */
	internal byte textureClampMode;
	/*!  */
	internal byte lightingInfluence;
	/*!  */
	internal byte envMapMinLod;
	/*! Unknown. */
	internal byte unknownByte;
	/*! At this cosine of angle falloff will be equal to Falloff Start Opacity */
	internal float falloffStartAngle;
	/*! At this cosine of angle falloff will be equal to Falloff Stop Opacity */
	internal float falloffStopAngle;
	/*! Alpha falloff multiplier at start angle */
	internal float falloffStartOpacity;
	/*! Alpha falloff multiplier at end angle */
	internal float falloffStopOpacity;
	/*! Emissive color */
	internal Color4 emissiveColor;
	/*! Multiplier for Emissive Color (RGB part) */
	internal float emissiveMultiple;
	/*!  */
	internal float softFalloffDepth;
	/*!
	 * Points to an external texture, used as palette for
	 * SLSF1_Greyscale_To_PaletteColor/SLSF1_Greyscale_To_PaletteAlpha.
	 */
	internal string greyscaleTexture;
	/*!  */
	internal string envMapTexture;
	/*!  */
	internal string normalTexture;
	/*!  */
	internal string envMaskTexture;
	/*!  */
	internal float environmentMapScale;

	public BSEffectShaderProperty() {
	shaderFlags1_sk = (SkyrimShaderPropertyFlags1)2147483648;
	shaderFlags2_sk = (SkyrimShaderPropertyFlags2)32;
	shaderFlags1_fo4 = (Fallout4ShaderPropertyFlags1)2147483648;
	shaderFlags2_fo4 = (Fallout4ShaderPropertyFlags2)32;
	uvScale = 1.0, 1.0;
	textureClampMode = (byte)0;
	lightingInfluence = (byte)0;
	envMapMinLod = (byte)0;
	unknownByte = (byte)0;
	falloffStartAngle = 1.0f;
	falloffStopAngle = 1.0f;
	falloffStartOpacity = 0.0f;
	falloffStopOpacity = 0.0f;
	emissiveMultiple = 0.0f;
	softFalloffDepth = 0.0f;
	environmentMapScale = 0.0f;
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
public static NiObject Create() => new BSEffectShaderProperty();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if ((info.userVersion2 != 130)) {
		Nif.NifStream(out shaderFlags1_sk, s, info);
		Nif.NifStream(out shaderFlags2_sk, s, info);
	}
	if ((info.userVersion2 == 130)) {
		Nif.NifStream(out shaderFlags1_fo4, s, info);
		Nif.NifStream(out shaderFlags2_fo4, s, info);
	}
	Nif.NifStream(out uvOffset, s, info);
	Nif.NifStream(out uvScale, s, info);
	Nif.NifStream(out sourceTexture, s, info);
	Nif.NifStream(out textureClampMode, s, info);
	Nif.NifStream(out lightingInfluence, s, info);
	Nif.NifStream(out envMapMinLod, s, info);
	Nif.NifStream(out unknownByte, s, info);
	Nif.NifStream(out falloffStartAngle, s, info);
	Nif.NifStream(out falloffStopAngle, s, info);
	Nif.NifStream(out falloffStartOpacity, s, info);
	Nif.NifStream(out falloffStopOpacity, s, info);
	Nif.NifStream(out emissiveColor, s, info);
	Nif.NifStream(out emissiveMultiple, s, info);
	Nif.NifStream(out softFalloffDepth, s, info);
	Nif.NifStream(out greyscaleTexture, s, info);
	if ((info.userVersion2 == 130)) {
		Nif.NifStream(out envMapTexture, s, info);
		Nif.NifStream(out normalTexture, s, info);
		Nif.NifStream(out envMaskTexture, s, info);
		Nif.NifStream(out environmentMapScale, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if ((info.userVersion2 != 130)) {
		Nif.NifStream(shaderFlags1_sk, s, info);
		Nif.NifStream(shaderFlags2_sk, s, info);
	}
	if ((info.userVersion2 == 130)) {
		Nif.NifStream(shaderFlags1_fo4, s, info);
		Nif.NifStream(shaderFlags2_fo4, s, info);
	}
	Nif.NifStream(uvOffset, s, info);
	Nif.NifStream(uvScale, s, info);
	Nif.NifStream(sourceTexture, s, info);
	Nif.NifStream(textureClampMode, s, info);
	Nif.NifStream(lightingInfluence, s, info);
	Nif.NifStream(envMapMinLod, s, info);
	Nif.NifStream(unknownByte, s, info);
	Nif.NifStream(falloffStartAngle, s, info);
	Nif.NifStream(falloffStopAngle, s, info);
	Nif.NifStream(falloffStartOpacity, s, info);
	Nif.NifStream(falloffStopOpacity, s, info);
	Nif.NifStream(emissiveColor, s, info);
	Nif.NifStream(emissiveMultiple, s, info);
	Nif.NifStream(softFalloffDepth, s, info);
	Nif.NifStream(greyscaleTexture, s, info);
	if ((info.userVersion2 == 130)) {
		Nif.NifStream(envMapTexture, s, info);
		Nif.NifStream(normalTexture, s, info);
		Nif.NifStream(envMaskTexture, s, info);
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
	uint array_output_count = 0;
	s.Append(base.AsString());
	s.AppendLine($"  Shader Flags 1:  {shaderFlags1_sk}");
	s.AppendLine($"  Shader Flags 2:  {shaderFlags2_sk}");
	s.AppendLine($"  Shader Flags 1:  {shaderFlags1_fo4}");
	s.AppendLine($"  Shader Flags 2:  {shaderFlags2_fo4}");
	s.AppendLine($"  UV Offset:  {uvOffset}");
	s.AppendLine($"  UV Scale:  {uvScale}");
	s.AppendLine($"  Source Texture:  {sourceTexture}");
	s.AppendLine($"  Texture Clamp Mode:  {textureClampMode}");
	s.AppendLine($"  Lighting Influence:  {lightingInfluence}");
	s.AppendLine($"  Env Map Min LOD:  {envMapMinLod}");
	s.AppendLine($"  Unknown Byte:  {unknownByte}");
	s.AppendLine($"  Falloff Start Angle:  {falloffStartAngle}");
	s.AppendLine($"  Falloff Stop Angle:  {falloffStopAngle}");
	s.AppendLine($"  Falloff Start Opacity:  {falloffStartOpacity}");
	s.AppendLine($"  Falloff Stop Opacity:  {falloffStopOpacity}");
	s.AppendLine($"  Emissive Color:  {emissiveColor}");
	s.AppendLine($"  Emissive Multiple:  {emissiveMultiple}");
	s.AppendLine($"  Soft Falloff Depth:  {softFalloffDepth}");
	s.AppendLine($"  Greyscale Texture:  {greyscaleTexture}");
	s.AppendLine($"  Env Map Texture:  {envMapTexture}");
	s.AppendLine($"  Normal Texture:  {normalTexture}");
	s.AppendLine($"  Env Mask Texture:  {envMaskTexture}");
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