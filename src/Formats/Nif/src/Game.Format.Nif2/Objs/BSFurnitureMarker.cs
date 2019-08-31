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
	/*! Unknown. Marks furniture sitting positions? */
	public class BSFurnitureMarker : NiExtraData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSFurnitureMarker", NiExtraData.TYPE);

		/*! Number of positions. */
		internal uint numPositions;
		/*! Unknown. Probably has something to do with the furniture positions? */
		internal IList<FurniturePosition> positions;
		public BSFurnitureMarker()
		{
			numPositions = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSFurnitureMarker();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numPositions, s, info);
			positions = new FurniturePosition[numPositions];
			for (var i3 = 0; i3 < positions.Count; i3++)
			{
				Nif.NifStream(out positions[i3].offset, s, info);
				if ((info.userVersion2 <= 34))
				{
					Nif.NifStream(out positions[i3].orientation, s, info);
					Nif.NifStream(out positions[i3].positionRef1, s, info);
					Nif.NifStream(out positions[i3].positionRef2, s, info);
				}
				if ((info.userVersion2 > 34))
				{
					Nif.NifStream(out positions[i3].heading, s, info);
					Nif.NifStream(out positions[i3].animationType, s, info);
					Nif.NifStream(out positions[i3].entryProperties, s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numPositions = (uint)positions.Count;
			Nif.NifStream(numPositions, s, info);
			for (var i3 = 0; i3 < positions.Count; i3++)
			{
				Nif.NifStream(positions[i3].offset, s, info);
				if ((info.userVersion2 <= 34))
				{
					Nif.NifStream(positions[i3].orientation, s, info);
					Nif.NifStream(positions[i3].positionRef1, s, info);
					Nif.NifStream(positions[i3].positionRef2, s, info);
				}
				if ((info.userVersion2 > 34))
				{
					Nif.NifStream(positions[i3].heading, s, info);
					Nif.NifStream(positions[i3].animationType, s, info);
					Nif.NifStream(positions[i3].entryProperties, s, info);
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
			numPositions = (uint)positions.Count;
			s.AppendLine($"      Num Positions:  {numPositions}");
			array_output_count = 0;
			for (var i3 = 0; i3 < positions.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Offset:  {positions[i3].offset}");
				s.AppendLine($"        Orientation:  {positions[i3].orientation}");
				s.AppendLine($"        Position Ref 1:  {positions[i3].positionRef1}");
				s.AppendLine($"        Position Ref 2:  {positions[i3].positionRef2}");
				s.AppendLine($"        Heading:  {positions[i3].heading}");
				s.AppendLine($"        Animation Type:  {positions[i3].animationType}");
				s.AppendLine($"        Entry Properties:  {positions[i3].entryProperties}");
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
