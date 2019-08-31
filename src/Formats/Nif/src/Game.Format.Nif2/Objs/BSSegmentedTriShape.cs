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
	/*! Bethesda-specific AV object. */
	public class BSSegmentedTriShape : NiTriShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSSegmentedTriShape", NiTriShape.TYPE);

		/*! Number of segments in the square grid */
		internal int numSegments;
		/*! Configuration of each segment */
		internal IList<BSGeometrySegmentData> segment;
		public BSSegmentedTriShape()
		{
			numSegments = (int)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSSegmentedTriShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numSegments, s, info);
			segment = new BSGeometrySegmentData[numSegments];
			for (var i3 = 0; i3 < segment.Count; i3++)
			{
				if ((info.userVersion2 < 130))
				{
					Nif.NifStream(out segment[i3].flags, s, info);
					Nif.NifStream(out segment[i3].index, s, info);
					Nif.NifStream(out segment[i3].numTrisInSegment, s, info);
				}
				if (info.userVersion2 == 130)
				{
					Nif.NifStream(out segment[i3].startIndex, s, info);
					Nif.NifStream(out segment[i3].numPrimitives, s, info);
					Nif.NifStream(out segment[i3].parentArrayIndex, s, info);
					Nif.NifStream(out segment[i3].numSubSegments, s, info);
					segment[i3].subSegment = new BSGeometrySubSegment[segment[i3].numSubSegments];
					for (var i5 = 0; i5 < segment[i3].subSegment.Count; i5++)
					{
						Nif.NifStream(out segment[i3].subSegment[i5].startIndex, s, info);
						Nif.NifStream(out segment[i3].subSegment[i5].numPrimitives, s, info);
						Nif.NifStream(out segment[i3].subSegment[i5].parentArrayIndex, s, info);
						Nif.NifStream(out segment[i3].subSegment[i5].unused, s, info);
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSegments = (int)segment.Count;
			Nif.NifStream(numSegments, s, info);
			for (var i3 = 0; i3 < segment.Count; i3++)
			{
				segment[i3].numSubSegments = (uint)segment[i3].subSegment.Count;
				if ((info.userVersion2 < 130))
				{
					Nif.NifStream(segment[i3].flags, s, info);
					Nif.NifStream(segment[i3].index, s, info);
					Nif.NifStream(segment[i3].numTrisInSegment, s, info);
				}
				if (info.userVersion2 == 130)
				{
					Nif.NifStream(segment[i3].startIndex, s, info);
					Nif.NifStream(segment[i3].numPrimitives, s, info);
					Nif.NifStream(segment[i3].parentArrayIndex, s, info);
					Nif.NifStream(segment[i3].numSubSegments, s, info);
					for (var i5 = 0; i5 < segment[i3].subSegment.Count; i5++)
					{
						Nif.NifStream(segment[i3].subSegment[i5].startIndex, s, info);
						Nif.NifStream(segment[i3].subSegment[i5].numPrimitives, s, info);
						Nif.NifStream(segment[i3].subSegment[i5].parentArrayIndex, s, info);
						Nif.NifStream(segment[i3].subSegment[i5].unused, s, info);
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
			numSegments = (int)segment.Count;
			s.AppendLine($"      Num Segments:  {numSegments}");
			array_output_count = 0;
			for (var i3 = 0; i3 < segment.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				segment[i3].numSubSegments = (uint)segment[i3].subSegment.Count;
				s.AppendLine($"        Flags:  {segment[i3].flags}");
				s.AppendLine($"        Index:  {segment[i3].index}");
				s.AppendLine($"        Num Tris in Segment:  {segment[i3].numTrisInSegment}");
				s.AppendLine($"        Start Index:  {segment[i3].startIndex}");
				s.AppendLine($"        Num Primitives:  {segment[i3].numPrimitives}");
				s.AppendLine($"        Parent Array Index:  {segment[i3].parentArrayIndex}");
				s.AppendLine($"        Num Sub Segments:  {segment[i3].numSubSegments}");
				array_output_count = 0;
				for (var i4 = 0; i4 < segment[i3].subSegment.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					s.AppendLine($"          Start Index:  {segment[i3].subSegment[i4].startIndex}");
					s.AppendLine($"          Num Primitives:  {segment[i3].subSegment[i4].numPrimitives}");
					s.AppendLine($"          Parent Array Index:  {segment[i3].subSegment[i4].parentArrayIndex}");
					s.AppendLine($"          Unused:  {segment[i3].subSegment[i4].unused}");
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
