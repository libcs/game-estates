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
	/*! NiFogProperty allows the application to enable, disable and control the appearance of fog. */
	public class NiFogProperty : NiProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiFogProperty", NiProperty.TYPE);

		/*!
		 * Bit 0: Enables Fog
		 *             Bit 1: Sets Fog Function to FOG_RANGE_SQ
		 *             Bit 2: Sets Fog Function to FOG_VERTEX_ALPHA
		 * 
		 *             If Bit 1 and Bit 2 are not set, but fog is enabled, Fog function is FOG_Z_LINEAR.
		 */
		internal ushort flags;
		/*! Depth of the fog in normalized units. 1.0 = begins at near plane. 0.5 = begins halfway between the near and far planes. */
		internal float fogDepth;
		/*! The color of the fog. */
		internal Color3 fogColor;
		public NiFogProperty()
		{
			flags = (ushort)0;
			fogDepth = 1.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiFogProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out flags, s, info);
			Nif.NifStream(out fogDepth, s, info);
			Nif.NifStream(out fogColor, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(flags, s, info);
			Nif.NifStream(fogDepth, s, info);
			Nif.NifStream(fogColor, s, info);
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
			s.AppendLine($"      Fog Depth:  {fogDepth}");
			s.AppendLine($"      Fog Color:  {fogColor}");
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
