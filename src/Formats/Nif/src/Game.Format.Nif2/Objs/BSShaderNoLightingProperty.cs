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
	public class BSShaderNoLightingProperty : BSShaderLightingProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSShaderNoLightingProperty", BSShaderLightingProperty.TYPE);

		/*! The texture glow map. */
		internal string fileName;
		/*! At this cosine of angle falloff will be equal to Falloff Start Opacity */
		internal float falloffStartAngle;
		/*! At this cosine of angle falloff will be equal to Falloff Stop Opacity */
		internal float falloffStopAngle;
		/*! Alpha falloff multiplier at start angle */
		internal float falloffStartOpacity;
		/*! Alpha falloff multiplier at end angle */
		internal float falloffStopOpacity;
		public BSShaderNoLightingProperty()
		{
			falloffStartAngle = 1.0f;
			falloffStopAngle = 0.0f;
			falloffStartOpacity = 1.0f;
			falloffStopOpacity = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSShaderNoLightingProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out fileName, s, info);
			if ((info.userVersion2 > 26))
			{
				Nif.NifStream(out falloffStartAngle, s, info);
				Nif.NifStream(out falloffStopAngle, s, info);
				Nif.NifStream(out falloffStartOpacity, s, info);
				Nif.NifStream(out falloffStopOpacity, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(fileName, s, info);
			if ((info.userVersion2 > 26))
			{
				Nif.NifStream(falloffStartAngle, s, info);
				Nif.NifStream(falloffStopAngle, s, info);
				Nif.NifStream(falloffStartOpacity, s, info);
				Nif.NifStream(falloffStopOpacity, s, info);
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
			s.AppendLine($"      File Name:  {fileName}");
			s.AppendLine($"      Falloff Start Angle:  {falloffStartAngle}");
			s.AppendLine($"      Falloff Stop Angle:  {falloffStopAngle}");
			s.AppendLine($"      Falloff Start Opacity:  {falloffStartOpacity}");
			s.AppendLine($"      Falloff Stop Opacity:  {falloffStopOpacity}");
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
