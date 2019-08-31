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
	/*! Bethesda-specific property. */
	public class BSShaderPPLightingProperty : BSShaderLightingProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSShaderPPLightingProperty", BSShaderLightingProperty.TYPE);

		/*! Texture Set */
		internal BSShaderTextureSet textureSet;
		/*! The amount of distortion. **Not based on physically accurate refractive index** (0=none) (0-1) */
		internal float refractionStrength;
		/*! Rate of texture movement for refraction shader. */
		internal int refractionFirePeriod;
		/*! The number of passes the parallax shader can apply. */
		internal float parallaxMaxPasses;
		/*! The strength of the parallax. */
		internal float parallaxScale;
		/*! Glow color and alpha */
		internal Color4 emissiveColor;
		public BSShaderPPLightingProperty()
		{
			textureSet = null;
			refractionStrength = 0.0f;
			refractionFirePeriod = (int)0;
			parallaxMaxPasses = 4.0f;
			parallaxScale = 1.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSShaderPPLightingProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			if ((info.userVersion2 > 14))
			{
				Nif.NifStream(out refractionStrength, s, info);
				Nif.NifStream(out refractionFirePeriod, s, info);
			}
			if ((info.userVersion2 > 24))
			{
				Nif.NifStream(out parallaxMaxPasses, s, info);
				Nif.NifStream(out parallaxScale, s, info);
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(out emissiveColor, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)textureSet, s, info, link_map, missing_link_stack);
			if ((info.userVersion2 > 14))
			{
				Nif.NifStream(refractionStrength, s, info);
				Nif.NifStream(refractionFirePeriod, s, info);
			}
			if ((info.userVersion2 > 24))
			{
				Nif.NifStream(parallaxMaxPasses, s, info);
				Nif.NifStream(parallaxScale, s, info);
			}
			if ((info.userVersion2 > 34))
			{
				Nif.NifStream(emissiveColor, s, info);
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
			s.Append(base.AsString());
			s.AppendLine($"      Texture Set:  {textureSet}");
			s.AppendLine($"      Refraction Strength:  {refractionStrength}");
			s.AppendLine($"      Refraction Fire Period:  {refractionFirePeriod}");
			s.AppendLine($"      Parallax Max Passes:  {parallaxMaxPasses}");
			s.AppendLine($"      Parallax Scale:  {parallaxScale}");
			s.AppendLine($"      Emissive Color:  {emissiveColor}");
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
