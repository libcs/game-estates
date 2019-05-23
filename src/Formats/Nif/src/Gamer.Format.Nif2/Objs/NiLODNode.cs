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
	/*! Level of detail selector. Links to different levels of detail of the same model, used to switch a geometry at a specified distance. */
	public class NiLODNode : NiSwitchNode
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiLODNode", NiSwitchNode.TYPE);

		/*! lodCenter */
		internal Vector3 lodCenter;
		/*! numLodLevels */
		internal uint numLodLevels;
		/*! lodLevels */
		internal IList<LODRange> lodLevels;
		/*! lodLevelData */
		internal NiLODData lodLevelData;
		public NiLODNode()
		{
			numLodLevels = (uint)0;
			lodLevelData = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiLODNode();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x04000002 && info.version <= 0x0A000100)
			{
				Nif.NifStream(out lodCenter, s, info);
			}
			if (info.version <= 0x0A000100)
			{
				Nif.NifStream(out numLodLevels, s, info);
				lodLevels = new LODRange[numLodLevels];
				for (var i4 = 0; i4 < lodLevels.Count; i4++)
				{
					Nif.NifStream(out lodLevels[i4].nearExtent, s, info);
					Nif.NifStream(out lodLevels[i4].farExtent, s, info);
					if (info.version <= 0x03010000)
					{
						for (var i6 = 0; i6 < 3; i6++)
						{
							Nif.NifStream(out lodLevels[i4].unknownInts[i6], s, info);
						}
					}
				}
			}
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numLodLevels = (uint)lodLevels.Count;
			if (info.version >= 0x04000002 && info.version <= 0x0A000100)
			{
				Nif.NifStream(lodCenter, s, info);
			}
			if (info.version <= 0x0A000100)
			{
				Nif.NifStream(numLodLevels, s, info);
				for (var i4 = 0; i4 < lodLevels.Count; i4++)
				{
					Nif.NifStream(lodLevels[i4].nearExtent, s, info);
					Nif.NifStream(lodLevels[i4].farExtent, s, info);
					if (info.version <= 0x03010000)
					{
						for (var i6 = 0; i6 < 3; i6++)
						{
							Nif.NifStream(lodLevels[i4].unknownInts[i6], s, info);
						}
					}
				}
			}
			if (info.version >= 0x0A010000)
			{
				WriteRef((NiObject)lodLevelData, s, info, link_map, missing_link_stack);
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
			numLodLevels = (uint)lodLevels.Count;
			s.AppendLine($"      LOD Center:  {lodCenter}");
			s.AppendLine($"      Num LOD Levels:  {numLodLevels}");
			array_output_count = 0;
			for (var i3 = 0; i3 < lodLevels.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Near Extent:  {lodLevels[i3].nearExtent}");
				s.AppendLine($"        Far Extent:  {lodLevels[i3].farExtent}");
				array_output_count = 0;
				for (var i4 = 0; i4 < 3; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Ints[{i4}]:  {lodLevels[i3].unknownInts[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      LOD Level Data:  {lodLevelData}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x0A010000)
			{
				lodLevelData = FixLink<NiLODData>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (lodLevelData != null)
				refs.Add((NiObject)lodLevelData);
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
