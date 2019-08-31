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
	/*! Bethesda-specific particle modifier. */
	public class BSPSysSimpleColorModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSPSysSimpleColorModifier", NiPSysModifier.TYPE);

		/*! fadeInPercent */
		internal float fadeInPercent;
		/*! fadeOutPercent */
		internal float fadeOutPercent;
		/*! color1EndPercent */
		internal float color1EndPercent;
		/*! color1StartPercent */
		internal float color1StartPercent;
		/*! color2EndPercent */
		internal float color2EndPercent;
		/*! color2StartPercent */
		internal float color2StartPercent;
		/*! colors */
		internal Array3<Color4> colors;
		public BSPSysSimpleColorModifier()
		{
			fadeInPercent = 0.0f;
			fadeOutPercent = 0.0f;
			color1EndPercent = 0.0f;
			color1StartPercent = 0.0f;
			color2EndPercent = 0.0f;
			color2StartPercent = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSPSysSimpleColorModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out fadeInPercent, s, info);
			Nif.NifStream(out fadeOutPercent, s, info);
			Nif.NifStream(out color1EndPercent, s, info);
			Nif.NifStream(out color1StartPercent, s, info);
			Nif.NifStream(out color2EndPercent, s, info);
			Nif.NifStream(out color2StartPercent, s, info);
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(out colors[i3], s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(fadeInPercent, s, info);
			Nif.NifStream(fadeOutPercent, s, info);
			Nif.NifStream(color1EndPercent, s, info);
			Nif.NifStream(color1StartPercent, s, info);
			Nif.NifStream(color2EndPercent, s, info);
			Nif.NifStream(color2StartPercent, s, info);
			for (var i3 = 0; i3 < 3; i3++)
			{
				Nif.NifStream(colors[i3], s, info);
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
			s.AppendLine($"      Fade In Percent:  {fadeInPercent}");
			s.AppendLine($"      Fade out Percent:  {fadeOutPercent}");
			s.AppendLine($"      Color 1 End Percent:  {color1EndPercent}");
			s.AppendLine($"      Color 1 Start Percent:  {color1StartPercent}");
			s.AppendLine($"      Color 2 End Percent:  {color2EndPercent}");
			s.AppendLine($"      Color 2 Start Percent:  {color2StartPercent}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 3; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Colors[{i3}]:  {colors[i3]}");
				array_output_count++;
			}
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
