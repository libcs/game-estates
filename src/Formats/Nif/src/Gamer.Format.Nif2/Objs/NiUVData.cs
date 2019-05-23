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
	/*!
	 * DEPRECATED (pre-10.1), REMOVED (20.3)
	 *         Texture coordinate data.
	 */
	public class NiUVData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiUVData", NiObject.TYPE);

		/*! Four UV data groups. Appear to be U translation, V translation, U scaling/tiling, V scaling/tiling. */
		internal Array4<KeyGroup<float>> uvGroups;
		public NiUVData()
		{
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiUVData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			for (var i3 = 0; i3 < 4; i3++)
			{
				Nif.NifStream(out uvGroups[i3].numKeys, s, info);
				if ((uvGroups[i3].numKeys != 0))
				{
					Nif.NifStream(out uvGroups[i3].interpolation, s, info);
				}
				uvGroups[i3].keys = new Key[uvGroups[i3].numKeys];
				for (var i4 = 0; i4 < uvGroups[i3].keys.Count; i4++)
				{
					Nif.NifStream(out uvGroups[i3].keys[i4], s, info, uvGroups[i3].interpolation);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			for (var i3 = 0; i3 < 4; i3++)
			{
				uvGroups[i3].numKeys = (uint)uvGroups[i3].keys.Count;
				Nif.NifStream(uvGroups[i3].numKeys, s, info);
				if ((uvGroups[i3].numKeys != 0))
				{
					Nif.NifStream(uvGroups[i3].interpolation, s, info);
				}
				for (var i4 = 0; i4 < uvGroups[i3].keys.Count; i4++)
				{
					Nif.NifStream(uvGroups[i3].keys[i4], s, info, uvGroups[i3].interpolation);
				}
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
			array_output_count = 0;
			for (var i3 = 0; i3 < 4; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				uvGroups[i3].numKeys = (uint)uvGroups[i3].keys.Count;
				s.AppendLine($"        Num Keys:  {uvGroups[i3].numKeys}");
				if ((uvGroups[i3].numKeys != 0))
				{
					s.AppendLine($"          Interpolation:  {uvGroups[i3].interpolation}");
				}
				array_output_count = 0;
				for (var i4 = 0; i4 < uvGroups[i3].keys.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Keys[{i4}]:  {uvGroups[i3].keys[i4]}");
					array_output_count++;
				}
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
