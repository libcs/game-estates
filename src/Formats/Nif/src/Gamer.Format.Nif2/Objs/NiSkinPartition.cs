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
	/*!
	 * Skinning data, optimized for hardware skinning. The mesh is partitioned in submeshes such that each vertex of a submesh is influenced only by a limited and
	 * fixed number of bones.
	 */
	public class NiSkinPartition : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiSkinPartition", NiObject.TYPE);

		/*! numSkinPartitionBlocks */
		internal uint numSkinPartitionBlocks;
		/*! Skin partition objects. */
		internal IList<SkinPartition> skinPartitionBlocks;
		/*! dataSize */
		internal uint dataSize;
		/*! vertexSize */
		internal uint vertexSize;
		/*! vertexDesc */
		internal BSVertexDesc vertexDesc;
		/*! vertexData */
		internal IList<BSVertexData> vertexData;
		/*! partition */
		internal IList<SkinPartition> partition;
		public NiSkinPartition()
		{
			numSkinPartitionBlocks = (uint)0;
			dataSize = (uint)0;
			vertexSize = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiSkinPartition();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numSkinPartitionBlocks, s, info);
			if ((!((info.version == 0x14020007) && (info.userVersion2 == 100))))
			{
				skinPartitionBlocks = new SkinPartition[numSkinPartitionBlocks];
				for (var i4 = 0; i4 < skinPartitionBlocks.Count; i4++)
				{
					Nif.NifStream(out skinPartitionBlocks[i4].numVertices, s, info);
					Nif.NifStream(out skinPartitionBlocks[i4].numTriangles, s, info);
					Nif.NifStream(out skinPartitionBlocks[i4].numBones, s, info);
					Nif.NifStream(out skinPartitionBlocks[i4].numStrips, s, info);
					Nif.NifStream(out skinPartitionBlocks[i4].numWeightsPerVertex, s, info);
					skinPartitionBlocks[i4].bones = new ushort[skinPartitionBlocks[i4].numBones];
					for (var i5 = 0; i5 < skinPartitionBlocks[i4].bones.Count; i5++)
					{
						Nif.NifStream(out skinPartitionBlocks[i4].bones[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(out skinPartitionBlocks[i4].hasVertexMap, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						skinPartitionBlocks[i4].vertexMap = new ushort[skinPartitionBlocks[i4].numVertices];
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].vertexMap.Count; i6++)
						{
							Nif.NifStream(out skinPartitionBlocks[i4].vertexMap[i6], s, info);
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (skinPartitionBlocks[i4].hasVertexMap)
						{
							skinPartitionBlocks[i4].vertexMap = new ushort[skinPartitionBlocks[i4].numVertices];
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].vertexMap.Count; i7++)
							{
								Nif.NifStream(out (ushort)skinPartitionBlocks[i4].vertexMap[i7], s, info);
							}
						}
						Nif.NifStream(out skinPartitionBlocks[i4].hasVertexWeights, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						skinPartitionBlocks[i4].vertexWeights = new float[skinPartitionBlocks[i4].numVertices];
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].vertexWeights.Count; i6++)
						{
							skinPartitionBlocks[i4].vertexWeights[i6].Resize(skinPartitionBlocks[i4].numWeightsPerVertex);
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].vertexWeights[i6].Count; i7++)
							{
								Nif.NifStream(out skinPartitionBlocks[i4].vertexWeights[i6][i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (skinPartitionBlocks[i4].hasVertexWeights)
						{
							skinPartitionBlocks[i4].vertexWeights = new float[skinPartitionBlocks[i4].numVertices];
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].vertexWeights.Count; i7++)
							{
								skinPartitionBlocks[i4].vertexWeights[i7].Resize(skinPartitionBlocks[i4].numWeightsPerVertex);
								for (var i8 = 0; i8 < skinPartitionBlocks[i4].vertexWeights[i7].Count; i8++)
								{
									Nif.NifStream(out (float)skinPartitionBlocks[i4].vertexWeights[i7][i8], s, info);
								}
							}
						}
					}
					skinPartitionBlocks[i4].stripLengths = new ushort[skinPartitionBlocks[i4].numStrips];
					for (var i5 = 0; i5 < skinPartitionBlocks[i4].stripLengths.Count; i5++)
					{
						Nif.NifStream(out skinPartitionBlocks[i4].stripLengths[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(out skinPartitionBlocks[i4].hasFaces, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						if ((skinPartitionBlocks[i4].numStrips != 0))
						{
							skinPartitionBlocks[i4].strips = new ushort[skinPartitionBlocks[i4].numStrips];
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].strips.Count; i7++)
							{
								skinPartitionBlocks[i4].strips[i7].Resize(skinPartitionBlocks[i4].stripLengths[i7]);
								for (var i8 = 0; i8 < skinPartitionBlocks[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream(out skinPartitionBlocks[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((skinPartitionBlocks[i4].hasFaces && (skinPartitionBlocks[i4].numStrips != 0)))
						{
							skinPartitionBlocks[i4].strips = new ushort[skinPartitionBlocks[i4].numStrips];
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].strips.Count; i7++)
							{
								skinPartitionBlocks[i4].strips[i7].Resize(skinPartitionBlocks[i4].stripLengths[i7]);
								for (var i8 = 0; i8 < skinPartitionBlocks[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream(out (ushort)skinPartitionBlocks[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version <= 0x0A000102)
					{
						if ((skinPartitionBlocks[i4].numStrips == 0))
						{
							skinPartitionBlocks[i4].triangles = new Triangle[skinPartitionBlocks[i4].numTriangles];
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].triangles.Count; i7++)
							{
								Nif.NifStream(out skinPartitionBlocks[i4].triangles[i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((skinPartitionBlocks[i4].hasFaces && (skinPartitionBlocks[i4].numStrips == 0)))
						{
							skinPartitionBlocks[i4].triangles = new Triangle[skinPartitionBlocks[i4].numTriangles];
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].triangles.Count; i7++)
							{
								Nif.NifStream(out (Triangle)skinPartitionBlocks[i4].triangles[i7], s, info);
							}
						}
					}
					Nif.NifStream(out skinPartitionBlocks[i4].hasBoneIndices, s, info);
					if (skinPartitionBlocks[i4].hasBoneIndices)
					{
						skinPartitionBlocks[i4].boneIndices = new byte[skinPartitionBlocks[i4].numVertices];
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].boneIndices.Count; i6++)
						{
							skinPartitionBlocks[i4].boneIndices[i6].Resize(skinPartitionBlocks[i4].numWeightsPerVertex);
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].boneIndices[i6].Count; i7++)
							{
								Nif.NifStream(out skinPartitionBlocks[i4].boneIndices[i6][i7], s, info);
							}
						}
					}
					if ((info.userVersion2 > 34))
					{
						Nif.NifStream(out skinPartitionBlocks[i4].unknownShort, s, info);
					}
					if (info.userVersion2 == 100)
					{
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vf1, s, info);
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vf2, s, info);
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vf3, s, info);
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vf4, s, info);
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vf5, s, info);
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vertexAttributes, s, info);
						Nif.NifStream(out skinPartitionBlocks[i4].vertexDesc.vf8, s, info);
						skinPartitionBlocks[i4].trianglesCopy = new Triangle[skinPartitionBlocks[i4].numTriangles];
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].trianglesCopy.Count; i6++)
						{
							Nif.NifStream(out skinPartitionBlocks[i4].trianglesCopy[i6], s, info);
						}
					}
				}
			}
			if (info.userVersion2 == 100)
			{
				Nif.NifStream(out dataSize, s, info);
				Nif.NifStream(out vertexSize, s, info);
				Nif.NifStream(out vertexDesc.vf1, s, info);
				Nif.NifStream(out vertexDesc.vf2, s, info);
				Nif.NifStream(out vertexDesc.vf3, s, info);
				Nif.NifStream(out vertexDesc.vf4, s, info);
				Nif.NifStream(out vertexDesc.vf5, s, info);
				Nif.NifStream(out vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(out vertexDesc.vf8, s, info);
				if ((dataSize > 0))
				{
					vertexData = new BSVertexData[(dataSize / vertexSize)];
					for (var i5 = 0; i5 < vertexData.Count; i5++)
					{
						Nif.NifStream(out vertexData[i5], s, info, vertexDesc.vertexAttributes);
					}
				}
				partition = new SkinPartition[numSkinPartitionBlocks];
				for (var i4 = 0; i4 < partition.Count; i4++)
				{
					Nif.NifStream(out partition[i4].numVertices, s, info);
					Nif.NifStream(out partition[i4].numTriangles, s, info);
					Nif.NifStream(out partition[i4].numBones, s, info);
					Nif.NifStream(out partition[i4].numStrips, s, info);
					Nif.NifStream(out partition[i4].numWeightsPerVertex, s, info);
					partition[i4].bones = new ushort[partition[i4].numBones];
					for (var i5 = 0; i5 < partition[i4].bones.Count; i5++)
					{
						Nif.NifStream(out partition[i4].bones[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(out partition[i4].hasVertexMap, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						partition[i4].vertexMap = new ushort[partition[i4].numVertices];
						for (var i6 = 0; i6 < partition[i4].vertexMap.Count; i6++)
						{
							Nif.NifStream(out partition[i4].vertexMap[i6], s, info);
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (partition[i4].hasVertexMap)
						{
							partition[i4].vertexMap = new ushort[partition[i4].numVertices];
							for (var i7 = 0; i7 < partition[i4].vertexMap.Count; i7++)
							{
								Nif.NifStream(out (ushort)partition[i4].vertexMap[i7], s, info);
							}
						}
						Nif.NifStream(out partition[i4].hasVertexWeights, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						partition[i4].vertexWeights = new float[partition[i4].numVertices];
						for (var i6 = 0; i6 < partition[i4].vertexWeights.Count; i6++)
						{
							partition[i4].vertexWeights[i6].Resize(partition[i4].numWeightsPerVertex);
							for (var i7 = 0; i7 < partition[i4].vertexWeights[i6].Count; i7++)
							{
								Nif.NifStream(out partition[i4].vertexWeights[i6][i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (partition[i4].hasVertexWeights)
						{
							partition[i4].vertexWeights = new float[partition[i4].numVertices];
							for (var i7 = 0; i7 < partition[i4].vertexWeights.Count; i7++)
							{
								partition[i4].vertexWeights[i7].Resize(partition[i4].numWeightsPerVertex);
								for (var i8 = 0; i8 < partition[i4].vertexWeights[i7].Count; i8++)
								{
									Nif.NifStream(out (float)partition[i4].vertexWeights[i7][i8], s, info);
								}
							}
						}
					}
					partition[i4].stripLengths = new ushort[partition[i4].numStrips];
					for (var i5 = 0; i5 < partition[i4].stripLengths.Count; i5++)
					{
						Nif.NifStream(out partition[i4].stripLengths[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(out partition[i4].hasFaces, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						if ((partition[i4].numStrips != 0))
						{
							partition[i4].strips = new ushort[partition[i4].numStrips];
							for (var i7 = 0; i7 < partition[i4].strips.Count; i7++)
							{
								partition[i4].strips[i7].Resize(partition[i4].stripLengths[i7]);
								for (var i8 = 0; i8 < partition[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream(out partition[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((partition[i4].hasFaces && (partition[i4].numStrips != 0)))
						{
							partition[i4].strips = new ushort[partition[i4].numStrips];
							for (var i7 = 0; i7 < partition[i4].strips.Count; i7++)
							{
								partition[i4].strips[i7].Resize(partition[i4].stripLengths[i7]);
								for (var i8 = 0; i8 < partition[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream(out (ushort)partition[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version <= 0x0A000102)
					{
						if ((partition[i4].numStrips == 0))
						{
							partition[i4].triangles = new Triangle[partition[i4].numTriangles];
							for (var i7 = 0; i7 < partition[i4].triangles.Count; i7++)
							{
								Nif.NifStream(out partition[i4].triangles[i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((partition[i4].hasFaces && (partition[i4].numStrips == 0)))
						{
							partition[i4].triangles = new Triangle[partition[i4].numTriangles];
							for (var i7 = 0; i7 < partition[i4].triangles.Count; i7++)
							{
								Nif.NifStream(out (Triangle)partition[i4].triangles[i7], s, info);
							}
						}
					}
					Nif.NifStream(out partition[i4].hasBoneIndices, s, info);
					if (partition[i4].hasBoneIndices)
					{
						partition[i4].boneIndices = new byte[partition[i4].numVertices];
						for (var i6 = 0; i6 < partition[i4].boneIndices.Count; i6++)
						{
							partition[i4].boneIndices[i6].Resize(partition[i4].numWeightsPerVertex);
							for (var i7 = 0; i7 < partition[i4].boneIndices[i6].Count; i7++)
							{
								Nif.NifStream(out partition[i4].boneIndices[i6][i7], s, info);
							}
						}
					}
					if ((info.userVersion2 > 34))
					{
						Nif.NifStream(out partition[i4].unknownShort, s, info);
					}
					if (info.userVersion2 == 100)
					{
						Nif.NifStream(out partition[i4].vertexDesc.vf1, s, info);
						Nif.NifStream(out partition[i4].vertexDesc.vf2, s, info);
						Nif.NifStream(out partition[i4].vertexDesc.vf3, s, info);
						Nif.NifStream(out partition[i4].vertexDesc.vf4, s, info);
						Nif.NifStream(out partition[i4].vertexDesc.vf5, s, info);
						Nif.NifStream(out partition[i4].vertexDesc.vertexAttributes, s, info);
						Nif.NifStream(out partition[i4].vertexDesc.vf8, s, info);
						partition[i4].trianglesCopy = new Triangle[partition[i4].numTriangles];
						for (var i6 = 0; i6 < partition[i4].trianglesCopy.Count; i6++)
						{
							Nif.NifStream(out partition[i4].trianglesCopy[i6], s, info);
						}
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSkinPartitionBlocks = (uint)skinPartitionBlocks.Count;
			Nif.NifStream(numSkinPartitionBlocks, s, info);
			if ((!((info.version == 0x14020007) && (info.userVersion2 == 100))))
			{
				for (var i4 = 0; i4 < skinPartitionBlocks.Count; i4++)
				{
					for (var i5 = 0; i5 < skinPartitionBlocks[i4].strips.Count; i5++)
						skinPartitionBlocks[i4].stripLengths[i5] = (ushort)skinPartitionBlocks[i4].strips[i5].Count;
					skinPartitionBlocks[i4].numWeightsPerVertex = (ushort)(skinPartitionBlocks[i4].vertexWeights.Count > 0 ? skinPartitionBlocks[i4].vertexWeights[0].Count : 0);
					skinPartitionBlocks[i4].numStrips = (ushort)skinPartitionBlocks[i4].stripLengths.Count;
					skinPartitionBlocks[i4].numBones = (ushort)skinPartitionBlocks[i4].bones.Count;
					skinPartitionBlocks[i4].numTriangles = skinPartitionBlocks[i4].numTrianglesCalc(info);
					skinPartitionBlocks[i4].numVertices = (ushort)skinPartitionBlocks[i4].vertexMap.Count;
					Nif.NifStream(skinPartitionBlocks[i4].numVertices, s, info);
					Nif.NifStream(skinPartitionBlocks[i4].numTriangles, s, info);
					Nif.NifStream(skinPartitionBlocks[i4].numBones, s, info);
					Nif.NifStream(skinPartitionBlocks[i4].numStrips, s, info);
					Nif.NifStream(skinPartitionBlocks[i4].numWeightsPerVertex, s, info);
					for (var i5 = 0; i5 < skinPartitionBlocks[i4].bones.Count; i5++)
					{
						Nif.NifStream(skinPartitionBlocks[i4].bones[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(skinPartitionBlocks[i4].hasVertexMap, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].vertexMap.Count; i6++)
						{
							Nif.NifStream(skinPartitionBlocks[i4].vertexMap[i6], s, info);
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (skinPartitionBlocks[i4].hasVertexMap)
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].vertexMap.Count; i7++)
							{
								Nif.NifStream((ushort)skinPartitionBlocks[i4].vertexMap[i7], s, info);
							}
						}
						Nif.NifStream(skinPartitionBlocks[i4].hasVertexWeights, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].vertexWeights.Count; i6++)
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].vertexWeights[i6].Count; i7++)
							{
								Nif.NifStream(skinPartitionBlocks[i4].vertexWeights[i6][i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (skinPartitionBlocks[i4].hasVertexWeights)
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].vertexWeights.Count; i7++)
							{
								for (var i8 = 0; i8 < skinPartitionBlocks[i4].vertexWeights[i7].Count; i8++)
								{
									Nif.NifStream((float)skinPartitionBlocks[i4].vertexWeights[i7][i8], s, info);
								}
							}
						}
					}
					for (var i5 = 0; i5 < skinPartitionBlocks[i4].stripLengths.Count; i5++)
					{
						Nif.NifStream(skinPartitionBlocks[i4].stripLengths[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(skinPartitionBlocks[i4].hasFaces, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						if ((skinPartitionBlocks[i4].numStrips != 0))
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].strips.Count; i7++)
							{
								for (var i8 = 0; i8 < skinPartitionBlocks[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream(skinPartitionBlocks[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((skinPartitionBlocks[i4].hasFaces && (skinPartitionBlocks[i4].numStrips != 0)))
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].strips.Count; i7++)
							{
								for (var i8 = 0; i8 < skinPartitionBlocks[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream((ushort)skinPartitionBlocks[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version <= 0x0A000102)
					{
						if ((skinPartitionBlocks[i4].numStrips == 0))
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].triangles.Count; i7++)
							{
								Nif.NifStream(skinPartitionBlocks[i4].triangles[i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((skinPartitionBlocks[i4].hasFaces && (skinPartitionBlocks[i4].numStrips == 0)))
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].triangles.Count; i7++)
							{
								Nif.NifStream((Triangle)skinPartitionBlocks[i4].triangles[i7], s, info);
							}
						}
					}
					Nif.NifStream(skinPartitionBlocks[i4].hasBoneIndices, s, info);
					if (skinPartitionBlocks[i4].hasBoneIndices)
					{
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].boneIndices.Count; i6++)
						{
							for (var i7 = 0; i7 < skinPartitionBlocks[i4].boneIndices[i6].Count; i7++)
							{
								Nif.NifStream(skinPartitionBlocks[i4].boneIndices[i6][i7], s, info);
							}
						}
					}
					if ((info.userVersion2 > 34))
					{
						Nif.NifStream(skinPartitionBlocks[i4].unknownShort, s, info);
					}
					if (info.userVersion2 == 100)
					{
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vf1, s, info);
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vf2, s, info);
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vf3, s, info);
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vf4, s, info);
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vf5, s, info);
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vertexAttributes, s, info);
						Nif.NifStream(skinPartitionBlocks[i4].vertexDesc.vf8, s, info);
						for (var i6 = 0; i6 < skinPartitionBlocks[i4].trianglesCopy.Count; i6++)
						{
							Nif.NifStream(skinPartitionBlocks[i4].trianglesCopy[i6], s, info);
						}
					}
				}
			}
			if (info.userVersion2 == 100)
			{
				Nif.NifStream(dataSize, s, info);
				Nif.NifStream(vertexSize, s, info);
				Nif.NifStream(vertexDesc.vf1, s, info);
				Nif.NifStream(vertexDesc.vf2, s, info);
				Nif.NifStream(vertexDesc.vf3, s, info);
				Nif.NifStream(vertexDesc.vf4, s, info);
				Nif.NifStream(vertexDesc.vf5, s, info);
				Nif.NifStream(vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(vertexDesc.vf8, s, info);
				if ((dataSize > 0))
				{
					for (var i5 = 0; i5 < vertexData.Count; i5++)
					{
						Nif.NifStream(vertexData[i5], s, info, vertexDesc.vertexAttributes);
					}
				}
				for (var i4 = 0; i4 < partition.Count; i4++)
				{
					for (var i5 = 0; i5 < partition[i4].strips.Count; i5++)
						partition[i4].stripLengths[i5] = (ushort)partition[i4].strips[i5].Count;
					partition[i4].numWeightsPerVertex = (ushort)(partition[i4].vertexWeights.Count > 0 ? partition[i4].vertexWeights[0].Count : 0);
					partition[i4].numStrips = (ushort)partition[i4].stripLengths.Count;
					partition[i4].numBones = (ushort)partition[i4].bones.Count;
					partition[i4].numTriangles = partition[i4].numTrianglesCalc(info);
					partition[i4].numVertices = (ushort)partition[i4].vertexMap.Count;
					Nif.NifStream(partition[i4].numVertices, s, info);
					Nif.NifStream(partition[i4].numTriangles, s, info);
					Nif.NifStream(partition[i4].numBones, s, info);
					Nif.NifStream(partition[i4].numStrips, s, info);
					Nif.NifStream(partition[i4].numWeightsPerVertex, s, info);
					for (var i5 = 0; i5 < partition[i4].bones.Count; i5++)
					{
						Nif.NifStream(partition[i4].bones[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(partition[i4].hasVertexMap, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						for (var i6 = 0; i6 < partition[i4].vertexMap.Count; i6++)
						{
							Nif.NifStream(partition[i4].vertexMap[i6], s, info);
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (partition[i4].hasVertexMap)
						{
							for (var i7 = 0; i7 < partition[i4].vertexMap.Count; i7++)
							{
								Nif.NifStream((ushort)partition[i4].vertexMap[i7], s, info);
							}
						}
						Nif.NifStream(partition[i4].hasVertexWeights, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						for (var i6 = 0; i6 < partition[i4].vertexWeights.Count; i6++)
						{
							for (var i7 = 0; i7 < partition[i4].vertexWeights[i6].Count; i7++)
							{
								Nif.NifStream(partition[i4].vertexWeights[i6][i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if (partition[i4].hasVertexWeights)
						{
							for (var i7 = 0; i7 < partition[i4].vertexWeights.Count; i7++)
							{
								for (var i8 = 0; i8 < partition[i4].vertexWeights[i7].Count; i8++)
								{
									Nif.NifStream((float)partition[i4].vertexWeights[i7][i8], s, info);
								}
							}
						}
					}
					for (var i5 = 0; i5 < partition[i4].stripLengths.Count; i5++)
					{
						Nif.NifStream(partition[i4].stripLengths[i5], s, info);
					}
					if (info.version >= 0x0A010000)
					{
						Nif.NifStream(partition[i4].hasFaces, s, info);
					}
					if (info.version <= 0x0A000102)
					{
						if ((partition[i4].numStrips != 0))
						{
							for (var i7 = 0; i7 < partition[i4].strips.Count; i7++)
							{
								for (var i8 = 0; i8 < partition[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream(partition[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((partition[i4].hasFaces && (partition[i4].numStrips != 0)))
						{
							for (var i7 = 0; i7 < partition[i4].strips.Count; i7++)
							{
								for (var i8 = 0; i8 < partition[i4].stripLengths[i7]; i8++)
								{
									Nif.NifStream((ushort)partition[i4].strips[i7][i8], s, info);
								}
							}
						}
					}
					if (info.version <= 0x0A000102)
					{
						if ((partition[i4].numStrips == 0))
						{
							for (var i7 = 0; i7 < partition[i4].triangles.Count; i7++)
							{
								Nif.NifStream(partition[i4].triangles[i7], s, info);
							}
						}
					}
					if (info.version >= 0x0A010000)
					{
						if ((partition[i4].hasFaces && (partition[i4].numStrips == 0)))
						{
							for (var i7 = 0; i7 < partition[i4].triangles.Count; i7++)
							{
								Nif.NifStream((Triangle)partition[i4].triangles[i7], s, info);
							}
						}
					}
					Nif.NifStream(partition[i4].hasBoneIndices, s, info);
					if (partition[i4].hasBoneIndices)
					{
						for (var i6 = 0; i6 < partition[i4].boneIndices.Count; i6++)
						{
							for (var i7 = 0; i7 < partition[i4].boneIndices[i6].Count; i7++)
							{
								Nif.NifStream(partition[i4].boneIndices[i6][i7], s, info);
							}
						}
					}
					if ((info.userVersion2 > 34))
					{
						Nif.NifStream(partition[i4].unknownShort, s, info);
					}
					if (info.userVersion2 == 100)
					{
						Nif.NifStream(partition[i4].vertexDesc.vf1, s, info);
						Nif.NifStream(partition[i4].vertexDesc.vf2, s, info);
						Nif.NifStream(partition[i4].vertexDesc.vf3, s, info);
						Nif.NifStream(partition[i4].vertexDesc.vf4, s, info);
						Nif.NifStream(partition[i4].vertexDesc.vf5, s, info);
						Nif.NifStream(partition[i4].vertexDesc.vertexAttributes, s, info);
						Nif.NifStream(partition[i4].vertexDesc.vf8, s, info);
						for (var i6 = 0; i6 < partition[i4].trianglesCopy.Count; i6++)
						{
							Nif.NifStream(partition[i4].trianglesCopy[i6], s, info);
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
			numSkinPartitionBlocks = (uint)skinPartitionBlocks.Count;
			s.AppendLine($"      Num Skin Partition Blocks:  {numSkinPartitionBlocks}");
			array_output_count = 0;
			for (var i3 = 0; i3 < skinPartitionBlocks.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < skinPartitionBlocks[i3].strips.Count; i4++)
					skinPartitionBlocks[i3].stripLengths[i4] = (ushort)skinPartitionBlocks[i3].strips[i4].Count;
				skinPartitionBlocks[i3].numWeightsPerVertex = (ushort)(skinPartitionBlocks[i3].vertexWeights.Count > 0 ? skinPartitionBlocks[i3].vertexWeights[0].Count : 0);
				skinPartitionBlocks[i3].numStrips = (ushort)skinPartitionBlocks[i3].stripLengths.Count;
				skinPartitionBlocks[i3].numBones = (ushort)skinPartitionBlocks[i3].bones.Count;
				skinPartitionBlocks[i3].numVertices = (ushort)skinPartitionBlocks[i3].vertexMap.Count;
				s.AppendLine($"        Num Vertices:  {skinPartitionBlocks[i3].numVertices}");
				s.AppendLine($"        Num Triangles:  {skinPartitionBlocks[i3].numTriangles}");
				s.AppendLine($"        Num Bones:  {skinPartitionBlocks[i3].numBones}");
				s.AppendLine($"        Num Strips:  {skinPartitionBlocks[i3].numStrips}");
				s.AppendLine($"        Num Weights Per Vertex:  {skinPartitionBlocks[i3].numWeightsPerVertex}");
				array_output_count = 0;
				for (var i4 = 0; i4 < skinPartitionBlocks[i3].bones.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Bones[{i4}]:  {skinPartitionBlocks[i3].bones[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Has Vertex Map:  {skinPartitionBlocks[i3].hasVertexMap}");
				array_output_count = 0;
				for (var i4 = 0; i4 < skinPartitionBlocks[i3].vertexMap.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Vertex Map[{i4}]:  {skinPartitionBlocks[i3].vertexMap[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Has Vertex Weights:  {skinPartitionBlocks[i3].hasVertexWeights}");
				array_output_count = 0;
				for (var i4 = 0; i4 < skinPartitionBlocks[i3].vertexWeights.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					for (var i5 = 0; i5 < skinPartitionBlocks[i3].vertexWeights[i4].Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Vertex Weights[{i5}]:  {skinPartitionBlocks[i3].vertexWeights[i4][i5]}");
						array_output_count++;
					}
				}
				array_output_count = 0;
				for (var i4 = 0; i4 < skinPartitionBlocks[i3].stripLengths.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Strip Lengths[{i4}]:  {skinPartitionBlocks[i3].stripLengths[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Has Faces:  {skinPartitionBlocks[i3].hasFaces}");
				if ((skinPartitionBlocks[i3].numStrips != 0))
				{
					array_output_count = 0;
					for (var i5 = 0; i5 < skinPartitionBlocks[i3].strips.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						for (var i6 = 0; i6 < skinPartitionBlocks[i3].stripLengths[i5]; i6++)
						{
							if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
								break;
							s.AppendLine($"              Strips[{i6}]:  {skinPartitionBlocks[i3].strips[i5][i6]}");
							array_output_count++;
						}
					}
				}
				if ((skinPartitionBlocks[i3].numStrips == 0))
				{
					array_output_count = 0;
					for (var i5 = 0; i5 < skinPartitionBlocks[i3].triangles.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Triangles[{i5}]:  {skinPartitionBlocks[i3].triangles[i5]}");
						array_output_count++;
					}
				}
				s.AppendLine($"        Has Bone Indices:  {skinPartitionBlocks[i3].hasBoneIndices}");
				if (skinPartitionBlocks[i3].hasBoneIndices)
				{
					array_output_count = 0;
					for (var i5 = 0; i5 < skinPartitionBlocks[i3].boneIndices.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						for (var i6 = 0; i6 < skinPartitionBlocks[i3].boneIndices[i5].Count; i6++)
						{
							if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
								break;
							s.AppendLine($"              Bone Indices[{i6}]:  {skinPartitionBlocks[i3].boneIndices[i5][i6]}");
							array_output_count++;
						}
					}
				}
				s.AppendLine($"        Unknown Short:  {skinPartitionBlocks[i3].unknownShort}");
				s.AppendLine($"        VF1:  {skinPartitionBlocks[i3].vertexDesc.vf1}");
				s.AppendLine($"        VF2:  {skinPartitionBlocks[i3].vertexDesc.vf2}");
				s.AppendLine($"        VF3:  {skinPartitionBlocks[i3].vertexDesc.vf3}");
				s.AppendLine($"        VF4:  {skinPartitionBlocks[i3].vertexDesc.vf4}");
				s.AppendLine($"        VF5:  {skinPartitionBlocks[i3].vertexDesc.vf5}");
				s.AppendLine($"        Vertex Attributes:  {skinPartitionBlocks[i3].vertexDesc.vertexAttributes}");
				s.AppendLine($"        VF8:  {skinPartitionBlocks[i3].vertexDesc.vf8}");
				array_output_count = 0;
				for (var i4 = 0; i4 < skinPartitionBlocks[i3].trianglesCopy.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Triangles Copy[{i4}]:  {skinPartitionBlocks[i3].trianglesCopy[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Data Size:  {dataSize}");
			s.AppendLine($"      Vertex Size:  {vertexSize}");
			s.AppendLine($"      VF1:  {vertexDesc.vf1}");
			s.AppendLine($"      VF2:  {vertexDesc.vf2}");
			s.AppendLine($"      VF3:  {vertexDesc.vf3}");
			s.AppendLine($"      VF4:  {vertexDesc.vf4}");
			s.AppendLine($"      VF5:  {vertexDesc.vf5}");
			s.AppendLine($"      Vertex Attributes:  {vertexDesc.vertexAttributes}");
			s.AppendLine($"      VF8:  {vertexDesc.vf8}");
			if ((dataSize > 0))
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < vertexData.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Vertex Data[{i4}]:  {vertexData[i4]}");
					array_output_count++;
				}
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < partition.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < partition[i3].strips.Count; i4++)
					partition[i3].stripLengths[i4] = (ushort)partition[i3].strips[i4].Count;
				partition[i3].numWeightsPerVertex = (ushort)(partition[i3].vertexWeights.Count > 0 ? partition[i3].vertexWeights[0].Count : 0);
				partition[i3].numStrips = (ushort)partition[i3].stripLengths.Count;
				partition[i3].numBones = (ushort)partition[i3].bones.Count;
				partition[i3].numVertices = (ushort)partition[i3].vertexMap.Count;
				s.AppendLine($"        Num Vertices:  {partition[i3].numVertices}");
				s.AppendLine($"        Num Triangles:  {partition[i3].numTriangles}");
				s.AppendLine($"        Num Bones:  {partition[i3].numBones}");
				s.AppendLine($"        Num Strips:  {partition[i3].numStrips}");
				s.AppendLine($"        Num Weights Per Vertex:  {partition[i3].numWeightsPerVertex}");
				array_output_count = 0;
				for (var i4 = 0; i4 < partition[i3].bones.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Bones[{i4}]:  {partition[i3].bones[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Has Vertex Map:  {partition[i3].hasVertexMap}");
				array_output_count = 0;
				for (var i4 = 0; i4 < partition[i3].vertexMap.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Vertex Map[{i4}]:  {partition[i3].vertexMap[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Has Vertex Weights:  {partition[i3].hasVertexWeights}");
				array_output_count = 0;
				for (var i4 = 0; i4 < partition[i3].vertexWeights.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					for (var i5 = 0; i5 < partition[i3].vertexWeights[i4].Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Vertex Weights[{i5}]:  {partition[i3].vertexWeights[i4][i5]}");
						array_output_count++;
					}
				}
				array_output_count = 0;
				for (var i4 = 0; i4 < partition[i3].stripLengths.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Strip Lengths[{i4}]:  {partition[i3].stripLengths[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Has Faces:  {partition[i3].hasFaces}");
				if ((partition[i3].numStrips != 0))
				{
					array_output_count = 0;
					for (var i5 = 0; i5 < partition[i3].strips.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						for (var i6 = 0; i6 < partition[i3].stripLengths[i5]; i6++)
						{
							if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
								break;
							s.AppendLine($"              Strips[{i6}]:  {partition[i3].strips[i5][i6]}");
							array_output_count++;
						}
					}
				}
				if ((partition[i3].numStrips == 0))
				{
					array_output_count = 0;
					for (var i5 = 0; i5 < partition[i3].triangles.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Triangles[{i5}]:  {partition[i3].triangles[i5]}");
						array_output_count++;
					}
				}
				s.AppendLine($"        Has Bone Indices:  {partition[i3].hasBoneIndices}");
				if (partition[i3].hasBoneIndices)
				{
					array_output_count = 0;
					for (var i5 = 0; i5 < partition[i3].boneIndices.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						for (var i6 = 0; i6 < partition[i3].boneIndices[i5].Count; i6++)
						{
							if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
								break;
							s.AppendLine($"              Bone Indices[{i6}]:  {partition[i3].boneIndices[i5][i6]}");
							array_output_count++;
						}
					}
				}
				s.AppendLine($"        Unknown Short:  {partition[i3].unknownShort}");
				s.AppendLine($"        VF1:  {partition[i3].vertexDesc.vf1}");
				s.AppendLine($"        VF2:  {partition[i3].vertexDesc.vf2}");
				s.AppendLine($"        VF3:  {partition[i3].vertexDesc.vf3}");
				s.AppendLine($"        VF4:  {partition[i3].vertexDesc.vf4}");
				s.AppendLine($"        VF5:  {partition[i3].vertexDesc.vf5}");
				s.AppendLine($"        Vertex Attributes:  {partition[i3].vertexDesc.vertexAttributes}");
				s.AppendLine($"        VF8:  {partition[i3].vertexDesc.vf8}");
				array_output_count = 0;
				for (var i4 = 0; i4 < partition[i3].trianglesCopy.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Triangles Copy[{i4}]:  {partition[i3].trianglesCopy[i4]}");
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
