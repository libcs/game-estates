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
	/*! Describes the surface properties of an object e.g. translucency, ambient color, diffuse color, emissive color, and specular color. */
	public class NiMaterialProperty : NiProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiMaterialProperty", NiProperty.TYPE);

		/*! Property flags. */
		internal ushort flags;
		/*! How much the material reflects ambient light. */
		internal Color3 ambientColor;
		/*! How much the material reflects diffuse light. */
		internal Color3 diffuseColor;
		/*! How much light the material reflects in a specular manner. */
		internal Color3 specularColor;
		/*! How much light the material emits. */
		internal Color3 emissiveColor;
		/*! The material glossiness. */
		internal float glossiness;
		/*! The material transparency (1=non-transparant). Refer to a NiAlphaProperty object in this material's parent NiTriShape object, when alpha is not 1. */
		internal float alpha;
		/*! emissiveMult */
		internal float emissiveMult;
		public NiMaterialProperty()
		{
			flags = (ushort)0;
			ambientColor = (1.0, 1.0, 1.0);
			diffuseColor = (1.0, 1.0, 1.0);
			specularColor = (1.0, 1.0, 1.0);
			emissiveColor = (0.0, 0.0, 0.0);
			glossiness = 10.0f;
			alpha = 1.0f;
			emissiveMult = 1.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiMaterialProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version >= 0x03000000 && info.version <= 0x0A000102)
			{
				Nif.NifStream(out flags, s, info);
			}
			if ((info.userVersion2 < 26))
			{
				Nif.NifStream(out ambientColor, s, info);
				Nif.NifStream(out diffuseColor, s, info);
			}
			Nif.NifStream(out specularColor, s, info);
			Nif.NifStream(out emissiveColor, s, info);
			Nif.NifStream(out glossiness, s, info);
			Nif.NifStream(out alpha, s, info);
			if ((info.userVersion2 > 21))
			{
				Nif.NifStream(out emissiveMult, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version >= 0x03000000 && info.version <= 0x0A000102)
			{
				Nif.NifStream(flags, s, info);
			}
			if ((info.userVersion2 < 26))
			{
				Nif.NifStream(ambientColor, s, info);
				Nif.NifStream(diffuseColor, s, info);
			}
			Nif.NifStream(specularColor, s, info);
			Nif.NifStream(emissiveColor, s, info);
			Nif.NifStream(glossiness, s, info);
			Nif.NifStream(alpha, s, info);
			if ((info.userVersion2 > 21))
			{
				Nif.NifStream(emissiveMult, s, info);
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
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Ambient Color:  {ambientColor}");
			s.AppendLine($"      Diffuse Color:  {diffuseColor}");
			s.AppendLine($"      Specular Color:  {specularColor}");
			s.AppendLine($"      Emissive Color:  {emissiveColor}");
			s.AppendLine($"      Glossiness:  {glossiness}");
			s.AppendLine($"      Alpha:  {alpha}");
			s.AppendLine($"      Emissive Mult:  {emissiveMult}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
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
