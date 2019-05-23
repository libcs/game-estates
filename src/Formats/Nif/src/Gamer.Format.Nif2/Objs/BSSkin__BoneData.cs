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
	/*! Fallout 4 Bone Data */
	public class BSSkin__BoneData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSSkin::BoneData", NiObject.TYPE);

		/*! numBones */
		internal uint numBones;
		/*! boneList */
		internal IList<BSSkinBoneTrans> boneList;
		public BSSkin__BoneData()
		{
			numBones = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSSkin__BoneData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numBones, s, info);
			boneList = new BSSkinBoneTrans[numBones];
			for (var i3 = 0; i3 < boneList.Count; i3++)
			{
				Nif.NifStream(out boneList[i3].boundingSphere.center, s, info);
				Nif.NifStream(out boneList[i3].boundingSphere.radius, s, info);
				Nif.NifStream(out boneList[i3].rotation, s, info);
				Nif.NifStream(out boneList[i3].translation, s, info);
				Nif.NifStream(out boneList[i3].scale, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numBones = (uint)boneList.Count;
			Nif.NifStream(numBones, s, info);
			for (var i3 = 0; i3 < boneList.Count; i3++)
			{
				Nif.NifStream(boneList[i3].boundingSphere.center, s, info);
				Nif.NifStream(boneList[i3].boundingSphere.radius, s, info);
				Nif.NifStream(boneList[i3].rotation, s, info);
				Nif.NifStream(boneList[i3].translation, s, info);
				Nif.NifStream(boneList[i3].scale, s, info);
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
			numBones = (uint)boneList.Count;
			s.AppendLine($"      Num Bones:  {numBones}");
			array_output_count = 0;
			for (var i3 = 0; i3 < boneList.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Center:  {boneList[i3].boundingSphere.center}");
				s.AppendLine($"        Radius:  {boneList[i3].boundingSphere.radius}");
				s.AppendLine($"        Rotation:  {boneList[i3].rotation}");
				s.AppendLine($"        Translation:  {boneList[i3].translation}");
				s.AppendLine($"        Scale:  {boneList[i3].scale}");
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
