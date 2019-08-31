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
	public class NiTextureProperty : NiProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTextureProperty", NiProperty.TYPE);

		/*! Property flags. */
		internal Array2<uint> unknownInts1;
		/*! Property flags. */
		internal ushort flags;
		/*! Link to the texture image. */
		internal NiImage image;
		/*! Unknown.  0? */
		internal Array2<uint> unknownInts2;
		public NiTextureProperty()
		{
			flags = (ushort)0;
			image = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTextureProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version <= 0x02030000)
			{
				for (var i4 = 0; i4 < 2; i4++)
				{
					Nif.NifStream(out unknownInts1[i4], s, info);
				}
			}
			if (info.version >= 0x03000000)
			{
				Nif.NifStream(out flags, s, info);
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			if (info.version >= 0x03000000 && info.version <= 0x03000300)
			{
				for (var i4 = 0; i4 < 2; i4++)
				{
					Nif.NifStream(out unknownInts2[i4], s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version <= 0x02030000)
			{
				for (var i4 = 0; i4 < 2; i4++)
				{
					Nif.NifStream(unknownInts1[i4], s, info);
				}
			}
			if (info.version >= 0x03000000)
			{
				Nif.NifStream(flags, s, info);
			}
			WriteRef((NiObject)image, s, info, link_map, missing_link_stack);
			if (info.version >= 0x03000000 && info.version <= 0x03000300)
			{
				for (var i4 = 0; i4 < 2; i4++)
				{
					Nif.NifStream(unknownInts2[i4], s, info);
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
			for (var i3 = 0; i3 < 2; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Ints 1[{i3}]:  {unknownInts1[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Image:  {image}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 2; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Ints 2[{i3}]:  {unknownInts2[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (image != null)
				refs.Add((NiObject)image);
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
