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
	/*! Extra data, used to name different animation sequences. */
	public class NiTextKeyExtraData : NiExtraData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTextKeyExtraData", NiExtraData.TYPE);

		/*! Unknown.  Always equals zero in all official files. */
		internal uint unknownInt1;
		/*! The number of text keys that follow. */
		internal uint numTextKeys;
		/*! List of textual notes and at which time they take effect. Used for designating the start and stop of animations and the triggering of sounds. */
		internal IList<Key<IndexString>> textKeys;
		public NiTextKeyExtraData()
		{
			unknownInt1 = (uint)0;
			numTextKeys = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTextKeyExtraData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version <= 0x04020200)
			{
				Nif.NifStream(out unknownInt1, s, info);
			}
			Nif.NifStream(out numTextKeys, s, info);
			textKeys = new Key[numTextKeys];
			for (var i3 = 0; i3 < textKeys.Count; i3++)
			{
				Nif.NifStream(out textKeys[i3], s, info, 1);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numTextKeys = (uint)textKeys.Count;
			if (info.version <= 0x04020200)
			{
				Nif.NifStream(unknownInt1, s, info);
			}
			Nif.NifStream(numTextKeys, s, info);
			for (var i3 = 0; i3 < textKeys.Count; i3++)
			{
				Nif.NifStream(textKeys[i3], s, info, 1);
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
			numTextKeys = (uint)textKeys.Count;
			s.AppendLine($"      Unknown Int 1:  {unknownInt1}");
			s.AppendLine($"      Num Text Keys:  {numTextKeys}");
			array_output_count = 0;
			for (var i3 = 0; i3 < textKeys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Text Keys[{i3}]:  {textKeys[i3]}");
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
