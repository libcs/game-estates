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
	 * Holds mesh data for continuous level of detail shapes.
	 *         Pesumably a progressive mesh with triangles specified by edge splits.
	 *         Seems to be specific to Freedom Force.
	 *         The structure of this is uncertain and highly experimental at this point.
	 */
	public class NiClodData : NiTriBasedGeomData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiClodData", NiTriBasedGeomData.TYPE);

		/*! Unknown. */
		internal ushort unknownShorts;
		/*! Unknown. */
		internal ushort unknownCount1;
		/*! Unknown. */
		internal ushort unknownCount2;
		/*! Unknown. */
		internal ushort unknownCount3;
		/*! Unknown. */
		internal float unknownFloat;
		/*! Unknown. */
		internal ushort unknownShort;
		/*! Unknown. */
		internal IList<Array6<ushort>> unknownClodShorts1;
		/*! Unknown. */
		internal IList<ushort> unknownClodShorts2;
		/*! Unknown. */
		internal IList<Array6<ushort>> unknownClodShorts3;
		public NiClodData()
		{
			unknownShorts = (ushort)0;
			unknownCount1 = (ushort)0;
			unknownCount2 = (ushort)0;
			unknownCount3 = (ushort)0;
			unknownFloat = 0.0f;
			unknownShort = (ushort)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiClodData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out unknownShorts, s, info);
			Nif.NifStream(out unknownCount1, s, info);
			Nif.NifStream(out unknownCount2, s, info);
			Nif.NifStream(out unknownCount3, s, info);
			Nif.NifStream(out unknownFloat, s, info);
			Nif.NifStream(out unknownShort, s, info);
			unknownClodShorts1 = new ushort[unknownCount1];
			for (var i3 = 0; i3 < unknownClodShorts1.Count; i3++)
			{
				for (var i4 = 0; i4 < 6; i4++)
				{
					Nif.NifStream(out unknownClodShorts1[i3][i4], s, info);
				}
			}
			unknownClodShorts2 = new ushort[unknownCount2];
			for (var i3 = 0; i3 < unknownClodShorts2.Count; i3++)
			{
				Nif.NifStream(out unknownClodShorts2[i3], s, info);
			}
			unknownClodShorts3 = new ushort[unknownCount3];
			for (var i3 = 0; i3 < unknownClodShorts3.Count; i3++)
			{
				for (var i4 = 0; i4 < 6; i4++)
				{
					Nif.NifStream(out unknownClodShorts3[i3][i4], s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			unknownCount3 = (ushort)unknownClodShorts3.Count;
			unknownCount2 = (ushort)unknownClodShorts2.Count;
			unknownCount1 = (ushort)unknownClodShorts1.Count;
			Nif.NifStream(unknownShorts, s, info);
			Nif.NifStream(unknownCount1, s, info);
			Nif.NifStream(unknownCount2, s, info);
			Nif.NifStream(unknownCount3, s, info);
			Nif.NifStream(unknownFloat, s, info);
			Nif.NifStream(unknownShort, s, info);
			for (var i3 = 0; i3 < unknownClodShorts1.Count; i3++)
			{
				for (var i4 = 0; i4 < 6; i4++)
				{
					Nif.NifStream(unknownClodShorts1[i3][i4], s, info);
				}
			}
			for (var i3 = 0; i3 < unknownClodShorts2.Count; i3++)
			{
				Nif.NifStream(unknownClodShorts2[i3], s, info);
			}
			for (var i3 = 0; i3 < unknownClodShorts3.Count; i3++)
			{
				for (var i4 = 0; i4 < 6; i4++)
				{
					Nif.NifStream(unknownClodShorts3[i3][i4], s, info);
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
			unknownCount3 = (ushort)unknownClodShorts3.Count;
			unknownCount2 = (ushort)unknownClodShorts2.Count;
			unknownCount1 = (ushort)unknownClodShorts1.Count;
			s.AppendLine($"      Unknown Shorts:  {unknownShorts}");
			s.AppendLine($"      Unknown Count 1:  {unknownCount1}");
			s.AppendLine($"      Unknown Count 2:  {unknownCount2}");
			s.AppendLine($"      Unknown Count 3:  {unknownCount3}");
			s.AppendLine($"      Unknown Float:  {unknownFloat}");
			s.AppendLine($"      Unknown Short:  {unknownShort}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknownClodShorts1.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < 6; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Clod Shorts 1[{i4}]:  {unknownClodShorts1[i3][i4]}");
					array_output_count++;
				}
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < unknownClodShorts2.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Clod Shorts 2[{i3}]:  {unknownClodShorts2[i3]}");
				array_output_count++;
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < unknownClodShorts3.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < 6; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Clod Shorts 3[{i4}]:  {unknownClodShorts3[i3][i4]}");
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
