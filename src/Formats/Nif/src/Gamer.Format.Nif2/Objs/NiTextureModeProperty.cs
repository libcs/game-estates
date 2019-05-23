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
	/*! LEGACY (pre-10.1) */
	public class NiTextureModeProperty : NiProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTextureModeProperty", NiProperty.TYPE);

		/*! Unknown. */
		internal Array3<uint> unknownInts;
		/*! Unknown. Either 210 or 194. */
		internal short unknownShort;
		/*! 0? */
		internal short ps2L;
		/*! -75? */
		internal short ps2K;
		public NiTextureModeProperty()
		{
			unknownShort = (short)0;
			ps2L = (short)0;
			ps2K = (short)-75;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTextureModeProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version <= 0x02030000)
			{
				for (var i4 = 0; i4 < 3; i4++)
				{
					Nif.NifStream(out unknownInts[i4], s, info);
				}
			}
			if (info.version >= 0x03000000)
			{
				Nif.NifStream(out unknownShort, s, info);
			}
			if (info.version >= 0x03010000 && info.version <= 0x0A020000)
			{
				Nif.NifStream(out ps2L, s, info);
				Nif.NifStream(out ps2K, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version <= 0x02030000)
			{
				for (var i4 = 0; i4 < 3; i4++)
				{
					Nif.NifStream(unknownInts[i4], s, info);
				}
			}
			if (info.version >= 0x03000000)
			{
				Nif.NifStream(unknownShort, s, info);
			}
			if (info.version >= 0x03010000 && info.version <= 0x0A020000)
			{
				Nif.NifStream(ps2L, s, info);
				Nif.NifStream(ps2K, s, info);
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
			for (var i3 = 0; i3 < 3; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Ints[{i3}]:  {unknownInts[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown Short:  {unknownShort}");
			s.AppendLine($"      PS2 L:  {ps2L}");
			s.AppendLine($"      PS2 K:  {ps2K}");
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
