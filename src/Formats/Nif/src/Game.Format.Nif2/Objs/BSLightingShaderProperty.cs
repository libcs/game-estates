/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.
//-----------------------------------NOTICE----------------------------------//
// Only add custom code in the designated areas to preserve between builds   //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Bethesda shader property for Skyrim and later. */
	public class BSLightingShaderProperty : BSShaderProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSLightingShaderProperty", BSShaderProperty.TYPE);

		/*! Skyrim Shader Flags for setting render/shader options. */
		internal SkyrimShaderPropertyFlags1 shaderFlags1_sk;
		/*! Skyrim Shader Flags for setting render/shader options. */
		internal SkyrimShaderPropertyFlags2 shaderFlags2_sk;
		/*! Fallout 4 Shader Flags. Mostly overridden if "Name" is a path to a BGSM/BGEM file. */
		internal Fallout4ShaderPropertyFlags1 shaderFlags1_fo4;
		/*! Fallout 4 Shader Flags. Mostly overridden if "Name" is a path to a BGSM/BGEM file. */
		internal Fallout4ShaderPropertyFlags2 shaderFlags2_fo4;
		/*! Offset UVs */
		internal TexCoord uvOffset;
		/*! Offset UV Scale to repeat tiling textures, see above. */
		internal TexCoord uvScale;
		/*! Texture Set, can have override in an esm/esp */
		internal BSShaderTextureSet textureSet;
		/*! Glow color and alpha */
		internal Color3 emissiveColor;
		/*! Multiplied emissive colors */
		internal float emissiveMultiple;
		/*! wetMaterial */
		internal IndexString wetMaterial;
		/*! How to handle texture borders. */
		internal TexClampMode textureClampMode;
		/*! The material opacity (1=non-transparent). */
		internal float alpha;
		/*! The amount of distortion. **Not based on physically accurate refractive index** (0=none) (0-1) */
		internal float refractionStrength;
		/*! The material specular power, or glossiness (0-999). */
		internal float glossiness;
		/*! The base roughness (0.0-1.0), multiplied by the smoothness map. */
		internal float smoothness;
		/*! Adds a colored highlight. */
		internal Color3 specularColor;
		/*! Brightness of specular highlight. (0=not visible) (0-999) */
		internal float specularStrength;
		/*! Controls strength for envmap/backlight/rim/softlight lighting effect? */
		internal float lightingEffect1;
		/*! Controls strength for envmap/backlight/rim/softlight lighting effect? */
		internal float lightingEffect2;
		/*! subsurfaceRolloff */
		internal float subsurfaceRolloff;
		/*! rimlightPower */
		internal float rimlightPower;
		/*! backlightPower */
		internal float backlightPower;
		/*! grayscaleToPaletteScale */
		internal float grayscaleToPaletteScale;
		/*! fresnelPower */
		internal float fresnelPower;
		/*! wetnessSpecScale */
		internal float wetnessSpecScale;
		/*! wetnessSpecPower */
		internal float wetnessSpecPower;
		/*! wetnessMinVar */
		internal float wetnessMinVar;
		/*! wetnessEnvMapScale */
		internal float wetnessEnvMapScale;
		/*! wetnessFresnelPower */
		internal float wetnessFresnelPower;
		/*! wetnessMetalness */
		internal float wetnessMetalness;
		/*! Scales the intensity of the environment/cube map. (0-1) */
		internal float environmentMapScale;
		/*! Unknown. */
		internal ushort unknownEnvMapShort;
		/*! Tints the base texture. Overridden by game settings. */
		internal Color3 skinTintColor;
		/*! Unknown. */
		internal uint unknownSkinTintInt;
		/*! Tints the base texture. Overridden by game settings. */
		internal Color3 hairTintColor;
		/*! Max Passes */
		internal float maxPasses;
		/*! Scale */
		internal float scale;
		/*! How far from the surface the inner layer appears to be. */
		internal float parallaxInnerLayerThickness;
		/*! Depth of inner parallax layer effect. */
		internal float parallaxRefractionScale;
		/*! Scales the inner parallax layer texture. */
		internal TexCoord parallaxInnerLayerTextureScale;
		/*! How strong the environment/cube map is. (0-??) */
		internal float parallaxEnvmapStrength;
		/*! CK lists "snow material" when used. */
		internal Vector4 sparkleParameters;
		/*! Eye cubemap scale */
		internal float eyeCubemapScale;
		/*! Offset to set center for left eye cubemap */
		internal Vector3 leftEyeReflectionCenter;
		/*! Offset to set center for right eye cubemap */
		internal Vector3 rightEyeReflectionCenter;
		public BSLightingShaderProperty()
		{
			shaderFlags1_sk = (SkyrimShaderPropertyFlags1)2185233153;
			shaderFlags2_sk = (SkyrimShaderPropertyFlags2)32801;
			shaderFlags1_fo4 = (Fallout4ShaderPropertyFlags1)2151678465;
			shaderFlags2_fo4 = (Fallout4ShaderPropertyFlags2)1;
			uvScale = (1.0, 1.0);
			textureSet = null;
			emissiveColor = (0.0, 0.0, 0.0);
			emissiveMultiple = 0.0f;
			textureClampMode = (TexClampMode)3;
			alpha = 1.0f;
			refractionStrength = 0.0f;
			glossiness = 80f;
			smoothness = 1.0f;
			specularStrength = 1.0f;
			lightingEffect1 = 0.3f;
			lightingEffect2 = 2.0f;
			subsurfaceRolloff = 0.3f;
			rimlightPower = 3.402823466e+38f;
			backlightPower = 0.0f;
			grayscaleToPaletteScale = 0.0f;
			fresnelPower = 5.0f;
			wetnessSpecScale = -1.0f;
			wetnessSpecPower = -1.0f;
			wetnessMinVar = -1.0f;
			wetnessEnvMapScale = -1.0f;
			wetnessFresnelPower = -1.0f;
			wetnessMetalness = -1.0f;
			environmentMapScale = 1.0f;
			unknownEnvMapShort = (ushort)0;
			unknownSkinTintInt = (uint)0;
			maxPasses = 0.0f;
			scale = 0.0f;
			parallaxInnerLayerThickness = 0.0f;
			parallaxRefractionScale = 0.0f;
			parallaxEnvmapStrength = 0.0f;
			eyeCubemapScale = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSLightingShaderProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if ((info.userVersion2 != 130))
			{
				Nif.NifStream(out shaderFlags1_sk, s, info);
				Nif.NifStream(out shaderFlags2_sk, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(out shaderFlags1_fo4, s, info);
				Nif.NifStream(out shaderFlags2_fo4, s, info);
			}
			Nif.NifStream(out uvOffset, s, info);
			Nif.NifStream(out uvScale, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out emissiveColor, s, info);
			Nif.NifStream(out emissiveMultiple, s, info);
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(out wetMaterial, s, info);
			}
			Nif.NifStream(out textureClampMode, s, info);
			Nif.NifStream(out alpha, s, info);
			Nif.NifStream(out refractionStrength, s, info);
			if ((info.userVersion2 < 130))
			{
				Nif.NifStream(out glossiness, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(out smoothness, s, info);
			}
			Nif.NifStream(out specularColor, s, info);
			Nif.NifStream(out specularStrength, s, info);
			if ((info.userVersion2 < 130))
			{
				Nif.NifStream(out lightingEffect1, s, info);
				Nif.NifStream(out lightingEffect2, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(out subsurfaceRolloff, s, info);
				Nif.NifStream(out rimlightPower, s, info);
				if ((rimlightPower == 0x7F7FFFFF))
				{
					Nif.NifStream(out backlightPower, s, info);
				}
				Nif.NifStream(out grayscaleToPaletteScale, s, info);
				Nif.NifStream(out fresnelPower, s, info);
				Nif.NifStream(out wetnessSpecScale, s, info);
				Nif.NifStream(out wetnessSpecPower, s, info);
				Nif.NifStream(out wetnessMinVar, s, info);
				Nif.NifStream(out wetnessEnvMapScale, s, info);
				Nif.NifStream(out wetnessFresnelPower, s, info);
				Nif.NifStream(out wetnessMetalness, s, info);
			}
			if ((skyrimShaderType == 1))
			{
				Nif.NifStream(out environmentMapScale, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				if ((skyrimShaderType == 1))
				{
					Nif.NifStream(out unknownEnvMapShort, s, info);
				}
			}
			if ((skyrimShaderType == 5))
			{
				Nif.NifStream(out skinTintColor, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				if ((skyrimShaderType == 5))
				{
					Nif.NifStream(out unknownSkinTintInt, s, info);
				}
			}
			if ((skyrimShaderType == 6))
			{
				Nif.NifStream(out hairTintColor, s, info);
			}
			if ((skyrimShaderType == 7))
			{
				Nif.NifStream(out maxPasses, s, info);
				Nif.NifStream(out scale, s, info);
			}
			if ((skyrimShaderType == 11))
			{
				Nif.NifStream(out parallaxInnerLayerThickness, s, info);
				Nif.NifStream(out parallaxRefractionScale, s, info);
				Nif.NifStream(out parallaxInnerLayerTextureScale, s, info);
				Nif.NifStream(out parallaxEnvmapStrength, s, info);
			}
			if ((skyrimShaderType == 14))
			{
				Nif.NifStream(out sparkleParameters, s, info);
			}
			if ((skyrimShaderType == 16))
			{
				Nif.NifStream(out eyeCubemapScale, s, info);
				Nif.NifStream(out leftEyeReflectionCenter, s, info);
				Nif.NifStream(out rightEyeReflectionCenter, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if ((info.userVersion2 != 130))
			{
				Nif.NifStream(shaderFlags1_sk, s, info);
				Nif.NifStream(shaderFlags2_sk, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(shaderFlags1_fo4, s, info);
				Nif.NifStream(shaderFlags2_fo4, s, info);
			}
			Nif.NifStream(uvOffset, s, info);
			Nif.NifStream(uvScale, s, info);
			WriteRef((NiObject)textureSet, s, info, link_map, missing_link_stack);
			Nif.NifStream(emissiveColor, s, info);
			Nif.NifStream(emissiveMultiple, s, info);
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(wetMaterial, s, info);
			}
			Nif.NifStream(textureClampMode, s, info);
			Nif.NifStream(alpha, s, info);
			Nif.NifStream(refractionStrength, s, info);
			if ((info.userVersion2 < 130))
			{
				Nif.NifStream(glossiness, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(smoothness, s, info);
			}
			Nif.NifStream(specularColor, s, info);
			Nif.NifStream(specularStrength, s, info);
			if ((info.userVersion2 < 130))
			{
				Nif.NifStream(lightingEffect1, s, info);
				Nif.NifStream(lightingEffect2, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				Nif.NifStream(subsurfaceRolloff, s, info);
				Nif.NifStream(rimlightPower, s, info);
				if ((rimlightPower == 0x7F7FFFFF))
				{
					Nif.NifStream(backlightPower, s, info);
				}
				Nif.NifStream(grayscaleToPaletteScale, s, info);
				Nif.NifStream(fresnelPower, s, info);
				Nif.NifStream(wetnessSpecScale, s, info);
				Nif.NifStream(wetnessSpecPower, s, info);
				Nif.NifStream(wetnessMinVar, s, info);
				Nif.NifStream(wetnessEnvMapScale, s, info);
				Nif.NifStream(wetnessFresnelPower, s, info);
				Nif.NifStream(wetnessMetalness, s, info);
			}
			if ((skyrimShaderType == 1))
			{
				Nif.NifStream(environmentMapScale, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				if ((skyrimShaderType == 1))
				{
					Nif.NifStream(unknownEnvMapShort, s, info);
				}
			}
			if ((skyrimShaderType == 5))
			{
				Nif.NifStream(skinTintColor, s, info);
			}
			if ((info.userVersion2 == 130))
			{
				if ((skyrimShaderType == 5))
				{
					Nif.NifStream(unknownSkinTintInt, s, info);
				}
			}
			if ((skyrimShaderType == 6))
			{
				Nif.NifStream(hairTintColor, s, info);
			}
			if ((skyrimShaderType == 7))
			{
				Nif.NifStream(maxPasses, s, info);
				Nif.NifStream(scale, s, info);
			}
			if ((skyrimShaderType == 11))
			{
				Nif.NifStream(parallaxInnerLayerThickness, s, info);
				Nif.NifStream(parallaxRefractionScale, s, info);
				Nif.NifStream(parallaxInnerLayerTextureScale, s, info);
				Nif.NifStream(parallaxEnvmapStrength, s, info);
			}
			if ((skyrimShaderType == 14))
			{
				Nif.NifStream(sparkleParameters, s, info);
			}
			if ((skyrimShaderType == 16))
			{
				Nif.NifStream(eyeCubemapScale, s, info);
				Nif.NifStream(leftEyeReflectionCenter, s, info);
				Nif.NifStream(rightEyeReflectionCenter, s, info);
			}
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			var array_output_count = 0U;
			s.Append(base.AsString());
			s.AppendLine($"      Shader Flags 1:  {shaderFlags1_sk}");
			s.AppendLine($"      Shader Flags 2:  {shaderFlags2_sk}");
			s.AppendLine($"      Shader Flags 1:  {shaderFlags1_fo4}");
			s.AppendLine($"      Shader Flags 2:  {shaderFlags2_fo4}");
			s.AppendLine($"      UV Offset:  {uvOffset}");
			s.AppendLine($"      UV Scale:  {uvScale}");
			s.AppendLine($"      Texture Set:  {textureSet}");
			s.AppendLine($"      Emissive Color:  {emissiveColor}");
			s.AppendLine($"      Emissive Multiple:  {emissiveMultiple}");
			s.AppendLine($"      Wet Material:  {wetMaterial}");
			s.AppendLine($"      Texture Clamp Mode:  {textureClampMode}");
			s.AppendLine($"      Alpha:  {alpha}");
			s.AppendLine($"      Refraction Strength:  {refractionStrength}");
			s.AppendLine($"      Glossiness:  {glossiness}");
			s.AppendLine($"      Smoothness:  {smoothness}");
			s.AppendLine($"      Specular Color:  {specularColor}");
			s.AppendLine($"      Specular Strength:  {specularStrength}");
			s.AppendLine($"      Lighting Effect 1:  {lightingEffect1}");
			s.AppendLine($"      Lighting Effect 2:  {lightingEffect2}");
			s.AppendLine($"      Subsurface Rolloff:  {subsurfaceRolloff}");
			s.AppendLine($"      Rimlight Power:  {rimlightPower}");
			if ((rimlightPower == 0x7F7FFFFF))
			{
				s.AppendLine($"        Backlight Power:  {backlightPower}");
			}
			s.AppendLine($"      Grayscale to Palette Scale:  {grayscaleToPaletteScale}");
			s.AppendLine($"      Fresnel Power:  {fresnelPower}");
			s.AppendLine($"      Wetness Spec Scale:  {wetnessSpecScale}");
			s.AppendLine($"      Wetness Spec Power:  {wetnessSpecPower}");
			s.AppendLine($"      Wetness Min Var:  {wetnessMinVar}");
			s.AppendLine($"      Wetness Env Map Scale:  {wetnessEnvMapScale}");
			s.AppendLine($"      Wetness Fresnel Power:  {wetnessFresnelPower}");
			s.AppendLine($"      Wetness Metalness:  {wetnessMetalness}");
			if ((skyrimShaderType == 1))
			{
				s.AppendLine($"        Environment Map Scale:  {environmentMapScale}");
				s.AppendLine($"        Unknown Env Map Short:  {unknownEnvMapShort}");
			}
			if ((skyrimShaderType == 5))
			{
				s.AppendLine($"        Skin Tint Color:  {skinTintColor}");
				s.AppendLine($"        Unknown Skin Tint Int:  {unknownSkinTintInt}");
			}
			if ((skyrimShaderType == 6))
			{
				s.AppendLine($"        Hair Tint Color:  {hairTintColor}");
			}
			if ((skyrimShaderType == 7))
			{
				s.AppendLine($"        Max Passes:  {maxPasses}");
				s.AppendLine($"        Scale:  {scale}");
			}
			if ((skyrimShaderType == 11))
			{
				s.AppendLine($"        Parallax Inner Layer Thickness:  {parallaxInnerLayerThickness}");
				s.AppendLine($"        Parallax Refraction Scale:  {parallaxRefractionScale}");
				s.AppendLine($"        Parallax Inner Layer Texture Scale:  {parallaxInnerLayerTextureScale}");
				s.AppendLine($"        Parallax Envmap Strength:  {parallaxEnvmapStrength}");
			}
			if ((skyrimShaderType == 14))
			{
				s.AppendLine($"        Sparkle Parameters:  {sparkleParameters}");
			}
			if ((skyrimShaderType == 16))
			{
				s.AppendLine($"        Eye Cubemap Scale:  {eyeCubemapScale}");
				s.AppendLine($"        Left Eye Reflection Center:  {leftEyeReflectionCenter}");
				s.AppendLine($"        Right Eye Reflection Center:  {rightEyeReflectionCenter}");
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			textureSet = FixLink<BSShaderTextureSet>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (textureSet != null)
				refs.Add((NiObject)textureSet);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			return ptrs;
		}
	}
}
