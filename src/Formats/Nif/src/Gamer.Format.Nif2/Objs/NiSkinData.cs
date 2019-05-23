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
	/*! Skinning data. */
	public class NiSkinData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiSkinData", NiObject.TYPE);

		/*! Offset of the skin from this bone in bind position. */
		internal NiTransform skinTransform;
		/*! Number of bones. */
		internal uint numBones;
		/*! This optionally links a NiSkinPartition for hardware-acceleration information. */
		internal NiSkinPartition skinPartition;
		/*! Enables Vertex Weights for this NiSkinData. */
		internal byte hasVertexWeights;
		/*! Contains offset data for each node that this skin is influenced by. */
		internal IList<BoneData> boneList;
		public NiSkinData()
		{
			numBones = (uint)0;
			skinPartition = null;
			hasVertexWeights = (byte)1;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiSkinData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out skinTransform.rotation, s, info);
			Nif.NifStream(out skinTransform.translation, s, info);
			Nif.NifStream(out skinTransform.scale, s, info);
			Nif.NifStream(out numBones, s, info);
			if (info.version >= 0x04000002 && info.version <= 0x0A010000)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version >= 0x04020100)
			{
				Nif.NifStream(out hasVertexWeights, s, info);
			}
			boneList = new BoneData[numBones];
			for (var i3 = 0; i3 < boneList.Count; i3++)
			{
				Nif.NifStream(out boneList[i3].skinTransform.rotation, s, info);
				Nif.NifStream(out boneList[i3].skinTransform.translation, s, info);
				Nif.NifStream(out boneList[i3].skinTransform.scale, s, info);
				Nif.NifStream(out boneList[i3].boundingSphereOffset, s, info);
				Nif.NifStream(out boneList[i3].boundingSphereRadius, s, info);
				if (info.version >= 0x14030009 && info.version <= 0x14030009 && info.userVersion == 131072)
				{
					for (var i5 = 0; i5 < 13; i5++)
					{
						Nif.NifStream(out boneList[i3].unknown13Shorts[i5], s, info);
					}
				}
				Nif.NifStream(out boneList[i3].numVertices, s, info);
				if (info.version <= 0x04020100)
				{
					boneList[i3].vertexWeights = new BoneVertData[boneList[i3].numVertices];
					for (var i5 = 0; i5 < boneList[i3].vertexWeights.Count; i5++)
					{
						Nif.NifStream(out boneList[i3].vertexWeights[i5].index, s, info);
						Nif.NifStream(out boneList[i3].vertexWeights[i5].weight, s, info);
					}
				}
				if (info.version >= 0x04020200)
				{
					if ((hasVertexWeights != 0))
					{
						boneList[i3].vertexWeights = new BoneVertData[boneList[i3].numVertices];
						for (var i6 = 0; i6 < boneList[i3].vertexWeights.Count; i6++)
						{
							Nif.NifStream(out boneList[i3].vertexWeights[i6].index, s, info);
							Nif.NifStream(out boneList[i3].vertexWeights[i6].weight, s, info);
						}
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numBones = (uint)boneList.Count;
			Nif.NifStream(skinTransform.rotation, s, info);
			Nif.NifStream(skinTransform.translation, s, info);
			Nif.NifStream(skinTransform.scale, s, info);
			Nif.NifStream(numBones, s, info);
			if (info.version >= 0x04000002 && info.version <= 0x0A010000)
			{
				WriteRef((NiObject)skinPartition, s, info, link_map, missing_link_stack);
			}
			if (info.version >= 0x04020100)
			{
				Nif.NifStream(hasVertexWeights, s, info);
			}
			for (var i3 = 0; i3 < boneList.Count; i3++)
			{
				boneList[i3].numVertices = (ushort)boneList[i3].vertexWeights.Count;
				Nif.NifStream(boneList[i3].skinTransform.rotation, s, info);
				Nif.NifStream(boneList[i3].skinTransform.translation, s, info);
				Nif.NifStream(boneList[i3].skinTransform.scale, s, info);
				Nif.NifStream(boneList[i3].boundingSphereOffset, s, info);
				Nif.NifStream(boneList[i3].boundingSphereRadius, s, info);
				if (info.version >= 0x14030009 && info.version <= 0x14030009 && info.userVersion == 131072)
				{
					for (var i5 = 0; i5 < 13; i5++)
					{
						Nif.NifStream(boneList[i3].unknown13Shorts[i5], s, info);
					}
				}
				Nif.NifStream(boneList[i3].numVertices, s, info);
				if (info.version <= 0x04020100)
				{
					for (var i5 = 0; i5 < boneList[i3].vertexWeights.Count; i5++)
					{
						Nif.NifStream(boneList[i3].vertexWeights[i5].index, s, info);
						Nif.NifStream(boneList[i3].vertexWeights[i5].weight, s, info);
					}
				}
				if (info.version >= 0x04020200)
				{
					if ((hasVertexWeights != 0))
					{
						for (var i6 = 0; i6 < boneList[i3].vertexWeights.Count; i6++)
						{
							Nif.NifStream(boneList[i3].vertexWeights[i6].index, s, info);
							Nif.NifStream(boneList[i3].vertexWeights[i6].weight, s, info);
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
			numBones = (uint)boneList.Count;
			s.AppendLine($"      Rotation:  {skinTransform.rotation}");
			s.AppendLine($"      Translation:  {skinTransform.translation}");
			s.AppendLine($"      Scale:  {skinTransform.scale}");
			s.AppendLine($"      Num Bones:  {numBones}");
			s.AppendLine($"      Skin Partition:  {skinPartition}");
			s.AppendLine($"      Has Vertex Weights:  {hasVertexWeights}");
			array_output_count = 0;
			for (var i3 = 0; i3 < boneList.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				boneList[i3].numVertices = (ushort)boneList[i3].vertexWeights.Count;
				s.AppendLine($"        Rotation:  {boneList[i3].skinTransform.rotation}");
				s.AppendLine($"        Translation:  {boneList[i3].skinTransform.translation}");
				s.AppendLine($"        Scale:  {boneList[i3].skinTransform.scale}");
				s.AppendLine($"        Bounding Sphere Offset:  {boneList[i3].boundingSphereOffset}");
				s.AppendLine($"        Bounding Sphere Radius:  {boneList[i3].boundingSphereRadius}");
				array_output_count = 0;
				for (var i4 = 0; i4 < 13; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown 13 Shorts[{i4}]:  {boneList[i3].unknown13Shorts[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Num Vertices:  {boneList[i3].numVertices}");
				array_output_count = 0;
				for (var i4 = 0; i4 < boneList[i3].vertexWeights.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					s.AppendLine($"          Index:  {boneList[i3].vertexWeights[i4].index}");
					s.AppendLine($"          Weight:  {boneList[i3].vertexWeights[i4].weight}");
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x04000002 && info.version <= 0x0A010000)
			{
				skinPartition = FixLink<NiSkinPartition>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (skinPartition != null)
				refs.Add((NiObject)skinPartition);
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
