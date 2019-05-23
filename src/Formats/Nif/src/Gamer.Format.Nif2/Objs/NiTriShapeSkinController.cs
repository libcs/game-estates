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
	/*! Old version of skinning instance. */
	public class NiTriShapeSkinController : NiTimeController
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTriShapeSkinController", NiTimeController.TYPE);

		/*! The number of node bones referenced as influences. */
		internal uint numBones;
		/*! The number of vertex weights stored for each bone. */
		internal IList<uint> vertexCounts;
		/*! List of all armature bones. */
		internal IList<NiBone> bones;
		/*! Contains skin weight data for each node that this skin is influenced by. */
		internal IList<OldSkinData[]> boneData;
		public NiTriShapeSkinController()
		{
			numBones = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTriShapeSkinController();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out numBones, s, info);
			vertexCounts = new uint[numBones];
			for (var i3 = 0; i3 < vertexCounts.Count; i3++)
			{
				Nif.NifStream(out vertexCounts[i3], s, info);
			}
			bones = new *[numBones];
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			boneData = new OldSkinData[numBones];
			for (var i3 = 0; i3 < boneData.Count; i3++)
			{
				boneData[i3].Resize(vertexCounts[i3]);
				for (var i4 = 0; i4 < vertexCounts[i3]; i4++)
				{
					Nif.NifStream(out boneData[i3][i4].vertexWeight, s, info);
					Nif.NifStream(out boneData[i3][i4].vertexIndex, s, info);
					Nif.NifStream(out boneData[i3][i4].unknownVector, s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			for (var i3 = 0; i3 < boneData.Count; i3++)
				vertexCounts[i3] = (uint)boneData[i3].Count;
			numBones = (uint)vertexCounts.Count;
			Nif.NifStream(numBones, s, info);
			for (var i3 = 0; i3 < vertexCounts.Count; i3++)
			{
				Nif.NifStream(vertexCounts[i3], s, info);
			}
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				WriteRef((NiObject)bones[i3], s, info, link_map, missing_link_stack);
			}
			for (var i3 = 0; i3 < boneData.Count; i3++)
			{
				for (var i4 = 0; i4 < vertexCounts[i3]; i4++)
				{
					Nif.NifStream(boneData[i3][i4].vertexWeight, s, info);
					Nif.NifStream(boneData[i3][i4].vertexIndex, s, info);
					Nif.NifStream(boneData[i3][i4].unknownVector, s, info);
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
			for (var i3 = 0; i3 < boneData.Count; i3++)
				vertexCounts[i3] = (uint)boneData[i3].Count;
			numBones = (uint)vertexCounts.Count;
			s.AppendLine($"      Num Bones:  {numBones}");
			array_output_count = 0;
			for (var i3 = 0; i3 < vertexCounts.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Vertex Counts[{i3}]:  {vertexCounts[i3]}");
				array_output_count++;
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Bones[{i3}]:  {bones[i3]}");
				array_output_count++;
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < boneData.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < vertexCounts[i3]; i4++)
				{
					s.AppendLine($"          Vertex Weight:  {boneData[i3][i4].vertexWeight}");
					s.AppendLine($"          Vertex Index:  {boneData[i3][i4].vertexIndex}");
					s.AppendLine($"          Unknown Vector:  {boneData[i3][i4].unknownVector}");
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				bones[i3] = FixLink<NiBone>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				if (bones[i3] != null)
					ptrs.Add((NiObject)bones[i3]);
			}
			return ptrs;
		}
	}
}
