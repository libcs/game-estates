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
	/*! Fallout 4 Sub-Index Tri Shape */
	public class BSSubIndexTriShape : BSTriShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSSubIndexTriShape", BSTriShape.TYPE);

		/*! numPrimitives */
		internal uint numPrimitives;
		/*! numSegments */
		internal uint numSegments;
		/*! totalSegments */
		internal uint totalSegments;
		/*! segment */
		internal IList<BSGeometrySegmentData> segment;
		/*! segmentData */
		internal BSGeometrySegmentSharedData segmentData;
		public BSSubIndexTriShape()
		{
			numPrimitives = (uint)0;
			numSegments = (uint)0;
			totalSegments = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSSubIndexTriShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.userVersion2 == 130)
			{
				if ((dataSize > 0))
				{
					Nif.NifStream(out numPrimitives, s, info);
					Nif.NifStream(out numSegments, s, info);
					Nif.NifStream(out totalSegments, s, info);
					segment = new BSGeometrySegmentData[numSegments];
					for (var i5 = 0; i5 < segment.Count; i5++)
					{
						if ((info.userVersion2 < 130))
						{
							Nif.NifStream(out segment[i5].flags, s, info);
							Nif.NifStream(out segment[i5].index, s, info);
							Nif.NifStream(out segment[i5].numTrisInSegment, s, info);
						}
						if (info.userVersion2 == 130)
						{
							Nif.NifStream(out segment[i5].startIndex, s, info);
							Nif.NifStream(out segment[i5].numPrimitives, s, info);
							Nif.NifStream(out segment[i5].parentArrayIndex, s, info);
							Nif.NifStream(out segment[i5].numSubSegments, s, info);
							segment[i5].subSegment = new BSGeometrySubSegment[segment[i5].numSubSegments];
							for (var i7 = 0; i7 < segment[i5].subSegment.Count; i7++)
							{
								Nif.NifStream(out segment[i5].subSegment[i7].startIndex, s, info);
								Nif.NifStream(out segment[i5].subSegment[i7].numPrimitives, s, info);
								Nif.NifStream(out segment[i5].subSegment[i7].parentArrayIndex, s, info);
								Nif.NifStream(out segment[i5].subSegment[i7].unused, s, info);
							}
						}
					}
				}
				if (((numSegments < totalSegments) && (dataSize > 0)))
				{
					Nif.NifStream(out segmentData.numSegments, s, info);
					Nif.NifStream(out segmentData.totalSegments, s, info);
					segmentData.segmentStarts = new uint[segmentData.numSegments];
					for (var i5 = 0; i5 < segmentData.segmentStarts.Count; i5++)
					{
						Nif.NifStream(out segmentData.segmentStarts[i5], s, info);
					}
					segmentData.perSegmentData = new BSGeometryPerSegmentSharedData[segmentData.totalSegments];
					for (var i5 = 0; i5 < segmentData.perSegmentData.Count; i5++)
					{
						Nif.NifStream(out segmentData.perSegmentData[i5].userIndex, s, info);
						Nif.NifStream(out segmentData.perSegmentData[i5].boneId, s, info);
						Nif.NifStream(out segmentData.perSegmentData[i5].numCutOffsets, s, info);
						segmentData.perSegmentData[i5].cutOffsets = new float[segmentData.perSegmentData[i5].numCutOffsets];
						for (var i6 = 0; i6 < segmentData.perSegmentData[i5].cutOffsets.Count; i6++)
						{
							Nif.NifStream(out segmentData.perSegmentData[i5].cutOffsets[i6], s, info);
						}
					}
					Nif.NifStream(out segmentData.ssfLength, s, info);
					segmentData.ssfFile = new byte[segmentData.ssfLength];
					for (var i5 = 0; i5 < segmentData.ssfFile.Count; i5++)
					{
						Nif.NifStream(out segmentData.ssfFile[i5], s, info);
					}
				}
			}
			if (info.userVersion2 == 100)
			{
				Nif.NifStream(out (uint)numSegments, s, info);
				segment = new BSGeometrySegmentData[numSegments];
				for (var i4 = 0; i4 < segment.Count; i4++)
				{
					if ((info.userVersion2 < 130))
					{
						Nif.NifStream(out segment[i4].flags, s, info);
						Nif.NifStream(out segment[i4].index, s, info);
						Nif.NifStream(out segment[i4].numTrisInSegment, s, info);
					}
					if (info.userVersion2 == 130)
					{
						Nif.NifStream(out segment[i4].startIndex, s, info);
						Nif.NifStream(out segment[i4].numPrimitives, s, info);
						Nif.NifStream(out segment[i4].parentArrayIndex, s, info);
						Nif.NifStream(out segment[i4].numSubSegments, s, info);
						segment[i4].subSegment = new BSGeometrySubSegment[segment[i4].numSubSegments];
						for (var i6 = 0; i6 < segment[i4].subSegment.Count; i6++)
						{
							Nif.NifStream(out segment[i4].subSegment[i6].startIndex, s, info);
							Nif.NifStream(out segment[i4].subSegment[i6].numPrimitives, s, info);
							Nif.NifStream(out segment[i4].subSegment[i6].parentArrayIndex, s, info);
							Nif.NifStream(out segment[i4].subSegment[i6].unused, s, info);
						}
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSegments = (uint)segment.Count;
			if (info.userVersion2 == 130)
			{
				if ((dataSize > 0))
				{
					Nif.NifStream(numPrimitives, s, info);
					Nif.NifStream(numSegments, s, info);
					Nif.NifStream(totalSegments, s, info);
					for (var i5 = 0; i5 < segment.Count; i5++)
					{
						segment[i5].numSubSegments = (uint)segment[i5].subSegment.Count;
						if ((info.userVersion2 < 130))
						{
							Nif.NifStream(segment[i5].flags, s, info);
							Nif.NifStream(segment[i5].index, s, info);
							Nif.NifStream(segment[i5].numTrisInSegment, s, info);
						}
						if (info.userVersion2 == 130)
						{
							Nif.NifStream(segment[i5].startIndex, s, info);
							Nif.NifStream(segment[i5].numPrimitives, s, info);
							Nif.NifStream(segment[i5].parentArrayIndex, s, info);
							Nif.NifStream(segment[i5].numSubSegments, s, info);
							for (var i7 = 0; i7 < segment[i5].subSegment.Count; i7++)
							{
								Nif.NifStream(segment[i5].subSegment[i7].startIndex, s, info);
								Nif.NifStream(segment[i5].subSegment[i7].numPrimitives, s, info);
								Nif.NifStream(segment[i5].subSegment[i7].parentArrayIndex, s, info);
								Nif.NifStream(segment[i5].subSegment[i7].unused, s, info);
							}
						}
					}
				}
				if (((numSegments < totalSegments) && (dataSize > 0)))
				{
					segmentData.ssfLength = (ushort)segmentData.ssfFile.Count;
					segmentData.totalSegments = (uint)segmentData.perSegmentData.Count;
					segmentData.numSegments = (uint)segmentData.segmentStarts.Count;
					Nif.NifStream(segmentData.numSegments, s, info);
					Nif.NifStream(segmentData.totalSegments, s, info);
					for (var i5 = 0; i5 < segmentData.segmentStarts.Count; i5++)
					{
						Nif.NifStream(segmentData.segmentStarts[i5], s, info);
					}
					for (var i5 = 0; i5 < segmentData.perSegmentData.Count; i5++)
					{
						segmentData.perSegmentData[i5].numCutOffsets = (uint)segmentData.perSegmentData[i5].cutOffsets.Count;
						Nif.NifStream(segmentData.perSegmentData[i5].userIndex, s, info);
						Nif.NifStream(segmentData.perSegmentData[i5].boneId, s, info);
						Nif.NifStream(segmentData.perSegmentData[i5].numCutOffsets, s, info);
						for (var i6 = 0; i6 < segmentData.perSegmentData[i5].cutOffsets.Count; i6++)
						{
							Nif.NifStream(segmentData.perSegmentData[i5].cutOffsets[i6], s, info);
						}
					}
					Nif.NifStream(segmentData.ssfLength, s, info);
					for (var i5 = 0; i5 < segmentData.ssfFile.Count; i5++)
					{
						Nif.NifStream(segmentData.ssfFile[i5], s, info);
					}
				}
			}
			if (info.userVersion2 == 100)
			{
				Nif.NifStream((uint)numSegments, s, info);
				for (var i4 = 0; i4 < segment.Count; i4++)
				{
					segment[i4].numSubSegments = (uint)segment[i4].subSegment.Count;
					if ((info.userVersion2 < 130))
					{
						Nif.NifStream(segment[i4].flags, s, info);
						Nif.NifStream(segment[i4].index, s, info);
						Nif.NifStream(segment[i4].numTrisInSegment, s, info);
					}
					if (info.userVersion2 == 130)
					{
						Nif.NifStream(segment[i4].startIndex, s, info);
						Nif.NifStream(segment[i4].numPrimitives, s, info);
						Nif.NifStream(segment[i4].parentArrayIndex, s, info);
						Nif.NifStream(segment[i4].numSubSegments, s, info);
						for (var i6 = 0; i6 < segment[i4].subSegment.Count; i6++)
						{
							Nif.NifStream(segment[i4].subSegment[i6].startIndex, s, info);
							Nif.NifStream(segment[i4].subSegment[i6].numPrimitives, s, info);
							Nif.NifStream(segment[i4].subSegment[i6].parentArrayIndex, s, info);
							Nif.NifStream(segment[i4].subSegment[i6].unused, s, info);
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
			numSegments = (uint)segment.Count;
			if ((dataSize > 0))
			{
				s.AppendLine($"        Num Primitives:  {numPrimitives}");
				s.AppendLine($"        Num Segments:  {numSegments}");
				s.AppendLine($"        Total Segments:  {totalSegments}");
				array_output_count = 0;
				for (var i4 = 0; i4 < segment.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					segment[i4].numSubSegments = (uint)segment[i4].subSegment.Count;
					s.AppendLine($"          Flags:  {segment[i4].flags}");
					s.AppendLine($"          Index:  {segment[i4].index}");
					s.AppendLine($"          Num Tris in Segment:  {segment[i4].numTrisInSegment}");
					s.AppendLine($"          Start Index:  {segment[i4].startIndex}");
					s.AppendLine($"          Num Primitives:  {segment[i4].numPrimitives}");
					s.AppendLine($"          Parent Array Index:  {segment[i4].parentArrayIndex}");
					s.AppendLine($"          Num Sub Segments:  {segment[i4].numSubSegments}");
					array_output_count = 0;
					for (var i5 = 0; i5 < segment[i4].subSegment.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						s.AppendLine($"            Start Index:  {segment[i4].subSegment[i5].startIndex}");
						s.AppendLine($"            Num Primitives:  {segment[i4].subSegment[i5].numPrimitives}");
						s.AppendLine($"            Parent Array Index:  {segment[i4].subSegment[i5].parentArrayIndex}");
						s.AppendLine($"            Unused:  {segment[i4].subSegment[i5].unused}");
					}
				}
			}
			if (((numSegments < totalSegments) && (dataSize > 0)))
			{
				segmentData.ssfLength = (ushort)segmentData.ssfFile.Count;
				segmentData.totalSegments = (uint)segmentData.perSegmentData.Count;
				segmentData.numSegments = (uint)segmentData.segmentStarts.Count;
				s.AppendLine($"        Num Segments:  {segmentData.numSegments}");
				s.AppendLine($"        Total Segments:  {segmentData.totalSegments}");
				array_output_count = 0;
				for (var i4 = 0; i4 < segmentData.segmentStarts.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Segment Starts[{i4}]:  {segmentData.segmentStarts[i4]}");
					array_output_count++;
				}
				array_output_count = 0;
				for (var i4 = 0; i4 < segmentData.perSegmentData.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					segmentData.perSegmentData[i4].numCutOffsets = (uint)segmentData.perSegmentData[i4].cutOffsets.Count;
					s.AppendLine($"          User Index:  {segmentData.perSegmentData[i4].userIndex}");
					s.AppendLine($"          Bone ID:  {segmentData.perSegmentData[i4].boneId}");
					s.AppendLine($"          Num Cut Offsets:  {segmentData.perSegmentData[i4].numCutOffsets}");
					array_output_count = 0;
					for (var i5 = 0; i5 < segmentData.perSegmentData[i4].cutOffsets.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Cut Offsets[{i5}]:  {segmentData.perSegmentData[i4].cutOffsets[i5]}");
						array_output_count++;
					}
				}
				s.AppendLine($"        SSF Length:  {segmentData.ssfLength}");
				array_output_count = 0;
				for (var i4 = 0; i4 < segmentData.ssfFile.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          SSF File[{i4}]:  {segmentData.ssfFile[i4]}");
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
