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
	/*! Voxel data object. */
	public class NiBinaryVoxelData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiBinaryVoxelData", NiObject.TYPE);

		/*! Unknown. */
		internal ushort unknownShort1;
		/*! Unknown. */
		internal ushort unknownShort2;
		/*! Unknown. Is this^3 the Unknown Bytes 1 size? */
		internal ushort unknownShort3;
		/*! Unknown. */
		internal Array7<float> unknown7Floats;
		/*! Unknown. Always a multiple of 7. */
		internal Array7<Array12<byte>> unknownBytes1;
		/*! Unknown. */
		internal uint numUnknownVectors;
		/*! Vectors on the unit sphere. */
		internal IList<Vector4> unknownVectors;
		/*! Unknown. */
		internal uint numUnknownBytes2;
		/*! Unknown. */
		internal IList<byte> unknownBytes2;
		/*! Unknown. */
		internal Array5<uint> unknown5Ints;
		public NiBinaryVoxelData()
		{
			unknownShort1 = (ushort)0;
			unknownShort2 = (ushort)0;
			unknownShort3 = (ushort)0;
			numUnknownVectors = (uint)0;
			numUnknownBytes2 = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiBinaryVoxelData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out unknownShort1, s, info);
			Nif.NifStream(out unknownShort2, s, info);
			Nif.NifStream(out unknownShort3, s, info);
			for (var i3 = 0; i3 < 7; i3++)
			{
				Nif.NifStream(out unknown7Floats[i3], s, info);
			}
			for (var i3 = 0; i3 < 7; i3++)
			{
				for (var i4 = 0; i4 < 12; i4++)
				{
					Nif.NifStream(out unknownBytes1[i3][i4], s, info);
				}
			}
			Nif.NifStream(out numUnknownVectors, s, info);
			unknownVectors = new Vector4[numUnknownVectors];
			for (var i3 = 0; i3 < unknownVectors.Count; i3++)
			{
				Nif.NifStream(out unknownVectors[i3], s, info);
			}
			Nif.NifStream(out numUnknownBytes2, s, info);
			unknownBytes2 = new byte[numUnknownBytes2];
			for (var i3 = 0; i3 < unknownBytes2.Count; i3++)
			{
				Nif.NifStream(out unknownBytes2[i3], s, info);
			}
			for (var i3 = 0; i3 < 5; i3++)
			{
				Nif.NifStream(out unknown5Ints[i3], s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numUnknownBytes2 = (uint)unknownBytes2.Count;
			numUnknownVectors = (uint)unknownVectors.Count;
			Nif.NifStream(unknownShort1, s, info);
			Nif.NifStream(unknownShort2, s, info);
			Nif.NifStream(unknownShort3, s, info);
			for (var i3 = 0; i3 < 7; i3++)
			{
				Nif.NifStream(unknown7Floats[i3], s, info);
			}
			for (var i3 = 0; i3 < 7; i3++)
			{
				for (var i4 = 0; i4 < 12; i4++)
				{
					Nif.NifStream(unknownBytes1[i3][i4], s, info);
				}
			}
			Nif.NifStream(numUnknownVectors, s, info);
			for (var i3 = 0; i3 < unknownVectors.Count; i3++)
			{
				Nif.NifStream(unknownVectors[i3], s, info);
			}
			Nif.NifStream(numUnknownBytes2, s, info);
			for (var i3 = 0; i3 < unknownBytes2.Count; i3++)
			{
				Nif.NifStream(unknownBytes2[i3], s, info);
			}
			for (var i3 = 0; i3 < 5; i3++)
			{
				Nif.NifStream(unknown5Ints[i3], s, info);
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
			numUnknownBytes2 = (uint)unknownBytes2.Count;
			numUnknownVectors = (uint)unknownVectors.Count;
			s.AppendLine($"      Unknown Short 1:  {unknownShort1}");
			s.AppendLine($"      Unknown Short 2:  {unknownShort2}");
			s.AppendLine($"      Unknown Short 3:  {unknownShort3}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 7; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 7 Floats[{i3}]:  {unknown7Floats[i3]}");
				array_output_count++;
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < 7; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < 12; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Bytes 1[{i4}]:  {unknownBytes1[i3][i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Num Unknown Vectors:  {numUnknownVectors}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknownVectors.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Vectors[{i3}]:  {unknownVectors[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Num Unknown Bytes 2:  {numUnknownBytes2}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknownBytes2.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown Bytes 2[{i3}]:  {unknownBytes2[i3]}");
				array_output_count++;
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < 5; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 5 Ints[{i3}]:  {unknown5Ints[i3]}");
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
