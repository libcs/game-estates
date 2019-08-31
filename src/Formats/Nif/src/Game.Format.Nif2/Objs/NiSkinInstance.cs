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
	/*! Skinning instance. */
	public class NiSkinInstance : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiSkinInstance", NiObject.TYPE);

		/*! Skinning data reference. */
		internal NiSkinData data;
		/*! Refers to a NiSkinPartition objects, which partitions the mesh such that every vertex is only influenced by a limited number of bones. */
		internal NiSkinPartition skinPartition;
		/*! Armature root node. */
		internal NiNode skeletonRoot;
		/*! The number of node bones referenced as influences. */
		internal uint numBones;
		/*! List of all armature bones. */
		internal IList<NiNode> bones;
		public NiSkinInstance()
		{
			data = null;
			skinPartition = null;
			skeletonRoot = null;
			numBones = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiSkinInstance();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			if (info.version >= 0x0A010065)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out numBones, s, info);
			bones = new *[numBones];
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numBones = (uint)bones.Count;
			WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
			if (info.version >= 0x0A010065)
			{
				WriteRef((NiObject)skinPartition, s, info, link_map, missing_link_stack);
			}
			WriteRef((NiObject)skeletonRoot, s, info, link_map, missing_link_stack);
			Nif.NifStream(numBones, s, info);
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				WriteRef((NiObject)bones[i3], s, info, link_map, missing_link_stack);
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
			numBones = (uint)bones.Count;
			s.AppendLine($"      Data:  {data}");
			s.AppendLine($"      Skin Partition:  {skinPartition}");
			s.AppendLine($"      Skeleton Root:  {skeletonRoot}");
			s.AppendLine($"      Num Bones:  {numBones}");
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
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			data = FixLink<NiSkinData>(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x0A010065)
			{
				skinPartition = FixLink<NiSkinPartition>(objects, link_stack, missing_link_stack, info);
			}
			skeletonRoot = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				bones[i3] = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (data != null)
				refs.Add((NiObject)data);
			if (skinPartition != null)
				refs.Add((NiObject)skinPartition);
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			if (skeletonRoot != null)
				ptrs.Add((NiObject)skeletonRoot);
			for (var i3 = 0; i3 < bones.Count; i3++)
			{
				if (bones[i3] != null)
					ptrs.Add((NiObject)bones[i3]);
			}
			return ptrs;
		}
	}
}
