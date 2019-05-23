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
	/*! Holds mesh data using strips of triangles. */
	public class NiTriStripsData : NiTriBasedGeomData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTriStripsData", NiTriBasedGeomData.TYPE);

		/*! Number of OpenGL triangle strips that are present. */
		internal ushort numStrips;
		/*! The number of points in each triangle strip. */
		internal IList<ushort> stripLengths;
		/*! Do we have strip point data? */
		internal bool hasPoints;
		/*! The points in the Triangle strips.  Size is the sum of all entries in Strip Lengths. */
		internal IList<ushort[]> points;
		public NiTriStripsData()
		{
			numStrips = (ushort)0;
			hasPoints = false;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTriStripsData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numStrips, s, info);
			stripLengths = new ushort[numStrips];
			for (var i3 = 0; i3 < stripLengths.Count; i3++)
			{
				Nif.NifStream(out stripLengths[i3], s, info);
			}
			if (info.version >= 0x0A000103)
			{
				Nif.NifStream(out hasPoints, s, info);
			}
			if (info.version <= 0x0A000102)
			{
				points = new ushort[numStrips];
				for (var i4 = 0; i4 < points.Count; i4++)
				{
					points[i4].Resize(stripLengths[i4]);
					for (var i5 = 0; i5 < stripLengths[i4]; i5++)
					{
						Nif.NifStream(out points[i4][i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A000103)
			{
				if (hasPoints)
				{
					points = new ushort[numStrips];
					for (var i5 = 0; i5 < points.Count; i5++)
					{
						points[i5].Resize(stripLengths[i5]);
						for (var i6 = 0; i6 < stripLengths[i5]; i6++)
						{
							Nif.NifStream(out (ushort)points[i5][i6], s, info);
						}
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			for (var i3 = 0; i3 < points.Count; i3++)
				stripLengths[i3] = (ushort)points[i3].Count;
			numStrips = (ushort)stripLengths.Count;
			Nif.NifStream(numStrips, s, info);
			for (var i3 = 0; i3 < stripLengths.Count; i3++)
			{
				Nif.NifStream(stripLengths[i3], s, info);
			}
			if (info.version >= 0x0A000103)
			{
				Nif.NifStream(hasPoints, s, info);
			}
			if (info.version <= 0x0A000102)
			{
				for (var i4 = 0; i4 < points.Count; i4++)
				{
					for (var i5 = 0; i5 < stripLengths[i4]; i5++)
					{
						Nif.NifStream(points[i4][i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A000103)
			{
				if (hasPoints)
				{
					for (var i5 = 0; i5 < points.Count; i5++)
					{
						for (var i6 = 0; i6 < stripLengths[i5]; i6++)
						{
							Nif.NifStream((ushort)points[i5][i6], s, info);
						}
					}
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
			for (var i3 = 0; i3 < points.Count; i3++)
				stripLengths[i3] = (ushort)points[i3].Count;
			numStrips = (ushort)stripLengths.Count;
			s.AppendLine($"      Num Strips:  {numStrips}");
			array_output_count = 0;
			for (var i3 = 0; i3 < stripLengths.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Strip Lengths[{i3}]:  {stripLengths[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Has Points:  {hasPoints}");
			array_output_count = 0;
			for (var i3 = 0; i3 < points.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < stripLengths[i3]; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Points[{i4}]:  {points[i3][i4]}");
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
