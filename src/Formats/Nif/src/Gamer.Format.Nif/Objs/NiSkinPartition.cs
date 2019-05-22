/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*!
 * Skinning data, optimized for hardware skinning. The mesh is partitioned in
 * submeshes such that each vertex of a submesh is influenced only by a limited and
 * fixed number of bones.
 */
public class NiSkinPartition : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSkinPartition", NiObject.TYPE);
	/*!  */
	internal uint numSkinPartitionBlocks;
	/*! Skin partition objects. */
	internal IList<SkinPartition> skinPartitionBlocks;
	/*!  */
	internal uint dataSize;
	/*!  */
	internal uint vertexSize;
	/*!  */
	internal BSVertexDesc vertexDesc;
	/*!  */
	internal IList<BSVertexData> vertexData;
	/*!  */
	internal IList<SkinPartition> partition;

	public NiSkinPartition() {
	numSkinPartitionBlocks = (uint)0;
	dataSize = (uint)0;
	vertexSize = (uint)0;
}

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new NiSkinPartition();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numSkinPartitionBlocks, s, info);
	if ((!((info.version == 0x14020007) && (info.userVersion2 == 100)))) {
		skinPartitionBlocks = new SkinPartition[numSkinPartitionBlocks];
		for (var i2 = 0; i2 < skinPartitionBlocks.Count; i2++) {
			Nif.NifStream(out skinPartitionBlocks[i2].numVertices, s, info);
			Nif.NifStream(out skinPartitionBlocks[i2].numTriangles, s, info);
			Nif.NifStream(out skinPartitionBlocks[i2].numBones, s, info);
			Nif.NifStream(out skinPartitionBlocks[i2].numStrips, s, info);
			Nif.NifStream(out skinPartitionBlocks[i2].numWeightsPerVertex, s, info);
			skinPartitionBlocks[i2].bones = new ushort[skinPartitionBlocks[i2].numBones];
			for (var i3 = 0; i3 < skinPartitionBlocks[i2].bones.Count; i3++) {
				Nif.NifStream(out skinPartitionBlocks[i2].bones[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(out skinPartitionBlocks[i2].hasVertexMap, s, info);
			}
			if (info.version <= 0x0A000102) {
				skinPartitionBlocks[i2].vertexMap = new ushort[skinPartitionBlocks[i2].numVertices];
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].vertexMap.Count; i4++) {
					Nif.NifStream(out skinPartitionBlocks[i2].vertexMap[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000) {
				if (skinPartitionBlocks[i2].hasVertexMap) {
					skinPartitionBlocks[i2].vertexMap = new ushort[skinPartitionBlocks[i2].numVertices];
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].vertexMap.Count; i5++) {
						Nif.NifStream(out (ushort)skinPartitionBlocks[i2].vertexMap[i5], s, info);
					}
				}
				Nif.NifStream(out skinPartitionBlocks[i2].hasVertexWeights, s, info);
			}
			if (info.version <= 0x0A000102) {
				skinPartitionBlocks[i2].vertexWeights = new float[skinPartitionBlocks[i2].numVertices];
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].vertexWeights.Count; i4++) {
					skinPartitionBlocks[i2].vertexWeights[i4].Resize(skinPartitionBlocks[i2].numWeightsPerVertex);
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].vertexWeights[i4].Count; i5++) {
						Nif.NifStream(out skinPartitionBlocks[i2].vertexWeights[i4][i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if (skinPartitionBlocks[i2].hasVertexWeights) {
					skinPartitionBlocks[i2].vertexWeights = new float[skinPartitionBlocks[i2].numVertices];
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].vertexWeights.Count; i5++) {
						skinPartitionBlocks[i2].vertexWeights[i5].Resize(skinPartitionBlocks[i2].numWeightsPerVertex);
						for (var i6 = 0; i6 < skinPartitionBlocks[i2].vertexWeights[i5].Count; i6++) {
							Nif.NifStream(out (float)skinPartitionBlocks[i2].vertexWeights[i5][i6], s, info);
						}
					}
				}
			}
			skinPartitionBlocks[i2].stripLengths = new ushort[skinPartitionBlocks[i2].numStrips];
			for (var i3 = 0; i3 < skinPartitionBlocks[i2].stripLengths.Count; i3++) {
				Nif.NifStream(out skinPartitionBlocks[i2].stripLengths[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(out skinPartitionBlocks[i2].hasFaces, s, info);
			}
			if (info.version <= 0x0A000102) {
				if ((skinPartitionBlocks[i2].numStrips != 0)) {
					skinPartitionBlocks[i2].strips = new ushort[skinPartitionBlocks[i2].numStrips];
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].strips.Count; i5++) {
						skinPartitionBlocks[i2].strips[i5].Resize(skinPartitionBlocks[i2].stripLengths[i5]);
						for (var i6 = 0; i6 < skinPartitionBlocks[i2].stripLengths[i5]; i6++) {
							Nif.NifStream(out skinPartitionBlocks[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((skinPartitionBlocks[i2].hasFaces && (skinPartitionBlocks[i2].numStrips != 0))) {
					skinPartitionBlocks[i2].strips = new ushort[skinPartitionBlocks[i2].numStrips];
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].strips.Count; i5++) {
						skinPartitionBlocks[i2].strips[i5].Resize(skinPartitionBlocks[i2].stripLengths[i5]);
						for (var i6 = 0; i6 < skinPartitionBlocks[i2].stripLengths[i5]; i6++) {
							Nif.NifStream(out (ushort)skinPartitionBlocks[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version <= 0x0A000102) {
				if ((skinPartitionBlocks[i2].numStrips == 0)) {
					skinPartitionBlocks[i2].triangles = new Triangle[skinPartitionBlocks[i2].numTriangles];
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].triangles.Count; i5++) {
						Nif.NifStream(out skinPartitionBlocks[i2].triangles[i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((skinPartitionBlocks[i2].hasFaces && (skinPartitionBlocks[i2].numStrips == 0))) {
					skinPartitionBlocks[i2].triangles = new Triangle[skinPartitionBlocks[i2].numTriangles];
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].triangles.Count; i5++) {
						Nif.NifStream(out (Triangle)skinPartitionBlocks[i2].triangles[i5], s, info);
					}
				}
			}
			Nif.NifStream(out skinPartitionBlocks[i2].hasBoneIndices, s, info);
			if (skinPartitionBlocks[i2].hasBoneIndices) {
				skinPartitionBlocks[i2].boneIndices = new byte[skinPartitionBlocks[i2].numVertices];
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].boneIndices.Count; i4++) {
					skinPartitionBlocks[i2].boneIndices[i4].Resize(skinPartitionBlocks[i2].numWeightsPerVertex);
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].boneIndices[i4].Count; i5++) {
						Nif.NifStream(out skinPartitionBlocks[i2].boneIndices[i4][i5], s, info);
					}
				}
			}
			if ((info.userVersion2 > 34)) {
				Nif.NifStream(out skinPartitionBlocks[i2].unknownShort, s, info);
			}
			if (info.userVersion2 == 100) {
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vf1, s, info);
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vf2, s, info);
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vf3, s, info);
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vf4, s, info);
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vf5, s, info);
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(out skinPartitionBlocks[i2].vertexDesc.vf8, s, info);
				skinPartitionBlocks[i2].trianglesCopy = new Triangle[skinPartitionBlocks[i2].numTriangles];
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].trianglesCopy.Count; i4++) {
					Nif.NifStream(out skinPartitionBlocks[i2].trianglesCopy[i4], s, info);
				}
			}
		}
	}
	if (info.userVersion2 == 100) {
		Nif.NifStream(out dataSize, s, info);
		Nif.NifStream(out vertexSize, s, info);
		Nif.NifStream(out vertexDesc.vf1, s, info);
		Nif.NifStream(out vertexDesc.vf2, s, info);
		Nif.NifStream(out vertexDesc.vf3, s, info);
		Nif.NifStream(out vertexDesc.vf4, s, info);
		Nif.NifStream(out vertexDesc.vf5, s, info);
		Nif.NifStream(out vertexDesc.vertexAttributes, s, info);
		Nif.NifStream(out vertexDesc.vf8, s, info);
		if ((dataSize > 0)) {
			vertexData = new BSVertexData[(dataSize / vertexSize)];
			for (var i3 = 0; i3 < vertexData.Count; i3++) {
				Nif.NifStream(out vertexData[i3], s, info, vertexDesc.vertexAttributes);
			}
		}
		partition = new SkinPartition[numSkinPartitionBlocks];
		for (var i2 = 0; i2 < partition.Count; i2++) {
			Nif.NifStream(out partition[i2].numVertices, s, info);
			Nif.NifStream(out partition[i2].numTriangles, s, info);
			Nif.NifStream(out partition[i2].numBones, s, info);
			Nif.NifStream(out partition[i2].numStrips, s, info);
			Nif.NifStream(out partition[i2].numWeightsPerVertex, s, info);
			partition[i2].bones = new ushort[partition[i2].numBones];
			for (var i3 = 0; i3 < partition[i2].bones.Count; i3++) {
				Nif.NifStream(out partition[i2].bones[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(out partition[i2].hasVertexMap, s, info);
			}
			if (info.version <= 0x0A000102) {
				partition[i2].vertexMap = new ushort[partition[i2].numVertices];
				for (var i4 = 0; i4 < partition[i2].vertexMap.Count; i4++) {
					Nif.NifStream(out partition[i2].vertexMap[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000) {
				if (partition[i2].hasVertexMap) {
					partition[i2].vertexMap = new ushort[partition[i2].numVertices];
					for (var i5 = 0; i5 < partition[i2].vertexMap.Count; i5++) {
						Nif.NifStream(out (ushort)partition[i2].vertexMap[i5], s, info);
					}
				}
				Nif.NifStream(out partition[i2].hasVertexWeights, s, info);
			}
			if (info.version <= 0x0A000102) {
				partition[i2].vertexWeights = new float[partition[i2].numVertices];
				for (var i4 = 0; i4 < partition[i2].vertexWeights.Count; i4++) {
					partition[i2].vertexWeights[i4].Resize(partition[i2].numWeightsPerVertex);
					for (var i5 = 0; i5 < partition[i2].vertexWeights[i4].Count; i5++) {
						Nif.NifStream(out partition[i2].vertexWeights[i4][i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if (partition[i2].hasVertexWeights) {
					partition[i2].vertexWeights = new float[partition[i2].numVertices];
					for (var i5 = 0; i5 < partition[i2].vertexWeights.Count; i5++) {
						partition[i2].vertexWeights[i5].Resize(partition[i2].numWeightsPerVertex);
						for (var i6 = 0; i6 < partition[i2].vertexWeights[i5].Count; i6++) {
							Nif.NifStream(out (float)partition[i2].vertexWeights[i5][i6], s, info);
						}
					}
				}
			}
			partition[i2].stripLengths = new ushort[partition[i2].numStrips];
			for (var i3 = 0; i3 < partition[i2].stripLengths.Count; i3++) {
				Nif.NifStream(out partition[i2].stripLengths[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(out partition[i2].hasFaces, s, info);
			}
			if (info.version <= 0x0A000102) {
				if ((partition[i2].numStrips != 0)) {
					partition[i2].strips = new ushort[partition[i2].numStrips];
					for (var i5 = 0; i5 < partition[i2].strips.Count; i5++) {
						partition[i2].strips[i5].Resize(partition[i2].stripLengths[i5]);
						for (var i6 = 0; i6 < partition[i2].stripLengths[i5]; i6++) {
							Nif.NifStream(out partition[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((partition[i2].hasFaces && (partition[i2].numStrips != 0))) {
					partition[i2].strips = new ushort[partition[i2].numStrips];
					for (var i5 = 0; i5 < partition[i2].strips.Count; i5++) {
						partition[i2].strips[i5].Resize(partition[i2].stripLengths[i5]);
						for (var i6 = 0; i6 < partition[i2].stripLengths[i5]; i6++) {
							Nif.NifStream(out (ushort)partition[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version <= 0x0A000102) {
				if ((partition[i2].numStrips == 0)) {
					partition[i2].triangles = new Triangle[partition[i2].numTriangles];
					for (var i5 = 0; i5 < partition[i2].triangles.Count; i5++) {
						Nif.NifStream(out partition[i2].triangles[i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((partition[i2].hasFaces && (partition[i2].numStrips == 0))) {
					partition[i2].triangles = new Triangle[partition[i2].numTriangles];
					for (var i5 = 0; i5 < partition[i2].triangles.Count; i5++) {
						Nif.NifStream(out (Triangle)partition[i2].triangles[i5], s, info);
					}
				}
			}
			Nif.NifStream(out partition[i2].hasBoneIndices, s, info);
			if (partition[i2].hasBoneIndices) {
				partition[i2].boneIndices = new byte[partition[i2].numVertices];
				for (var i4 = 0; i4 < partition[i2].boneIndices.Count; i4++) {
					partition[i2].boneIndices[i4].Resize(partition[i2].numWeightsPerVertex);
					for (var i5 = 0; i5 < partition[i2].boneIndices[i4].Count; i5++) {
						Nif.NifStream(out partition[i2].boneIndices[i4][i5], s, info);
					}
				}
			}
			if ((info.userVersion2 > 34)) {
				Nif.NifStream(out partition[i2].unknownShort, s, info);
			}
			if (info.userVersion2 == 100) {
				Nif.NifStream(out partition[i2].vertexDesc.vf1, s, info);
				Nif.NifStream(out partition[i2].vertexDesc.vf2, s, info);
				Nif.NifStream(out partition[i2].vertexDesc.vf3, s, info);
				Nif.NifStream(out partition[i2].vertexDesc.vf4, s, info);
				Nif.NifStream(out partition[i2].vertexDesc.vf5, s, info);
				Nif.NifStream(out partition[i2].vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(out partition[i2].vertexDesc.vf8, s, info);
				partition[i2].trianglesCopy = new Triangle[partition[i2].numTriangles];
				for (var i4 = 0; i4 < partition[i2].trianglesCopy.Count; i4++) {
					Nif.NifStream(out partition[i2].trianglesCopy[i4], s, info);
				}
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSkinPartitionBlocks = (uint)skinPartitionBlocks.Count;
	Nif.NifStream(numSkinPartitionBlocks, s, info);
	if ((!((info.version == 0x14020007) && (info.userVersion2 == 100)))) {
		for (var i2 = 0; i2 < skinPartitionBlocks.Count; i2++) {
			for (var i3 = 0; i3 < skinPartitionBlocks[i2].strips.Count; i3++)
				skinPartitionBlocks[i2].stripLengths[i3] = (ushort)skinPartitionBlocks[i2].strips[i3].Count;
			skinPartitionBlocks[i2].numWeightsPerVertex = (ushort)((skinPartitionBlocks[i2].vertexWeights.Count > 0) ? skinPartitionBlocks[i2].vertexWeights[0].Count : 0);
			skinPartitionBlocks[i2].numStrips = (ushort)skinPartitionBlocks[i2].stripLengths.Count;
			skinPartitionBlocks[i2].numBones = (ushort)skinPartitionBlocks[i2].bones.Count;
			skinPartitionBlocks[i2].numTriangles = skinPartitionBlocks[i2].numTrianglesCalc(info);
			skinPartitionBlocks[i2].numVertices = (ushort)skinPartitionBlocks[i2].vertexMap.Count;
			Nif.NifStream(skinPartitionBlocks[i2].numVertices, s, info);
			Nif.NifStream(skinPartitionBlocks[i2].numTriangles, s, info);
			Nif.NifStream(skinPartitionBlocks[i2].numBones, s, info);
			Nif.NifStream(skinPartitionBlocks[i2].numStrips, s, info);
			Nif.NifStream(skinPartitionBlocks[i2].numWeightsPerVertex, s, info);
			for (var i3 = 0; i3 < skinPartitionBlocks[i2].bones.Count; i3++) {
				Nif.NifStream(skinPartitionBlocks[i2].bones[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(skinPartitionBlocks[i2].hasVertexMap, s, info);
			}
			if (info.version <= 0x0A000102) {
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].vertexMap.Count; i4++) {
					Nif.NifStream(skinPartitionBlocks[i2].vertexMap[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000) {
				if (skinPartitionBlocks[i2].hasVertexMap) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].vertexMap.Count; i5++) {
						Nif.NifStream((ushort)skinPartitionBlocks[i2].vertexMap[i5], s, info);
					}
				}
				Nif.NifStream(skinPartitionBlocks[i2].hasVertexWeights, s, info);
			}
			if (info.version <= 0x0A000102) {
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].vertexWeights.Count; i4++) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].vertexWeights[i4].Count; i5++) {
						Nif.NifStream(skinPartitionBlocks[i2].vertexWeights[i4][i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if (skinPartitionBlocks[i2].hasVertexWeights) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].vertexWeights.Count; i5++) {
						for (var i6 = 0; i6 < skinPartitionBlocks[i2].vertexWeights[i5].Count; i6++) {
							Nif.NifStream((float)skinPartitionBlocks[i2].vertexWeights[i5][i6], s, info);
						}
					}
				}
			}
			for (var i3 = 0; i3 < skinPartitionBlocks[i2].stripLengths.Count; i3++) {
				Nif.NifStream(skinPartitionBlocks[i2].stripLengths[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(skinPartitionBlocks[i2].hasFaces, s, info);
			}
			if (info.version <= 0x0A000102) {
				if ((skinPartitionBlocks[i2].numStrips != 0)) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].strips.Count; i5++) {
						for (var i6 = 0; i6 < skinPartitionBlocks[i2].stripLengths[i5]; i6++) {
							Nif.NifStream(skinPartitionBlocks[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((skinPartitionBlocks[i2].hasFaces && (skinPartitionBlocks[i2].numStrips != 0))) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].strips.Count; i5++) {
						for (var i6 = 0; i6 < skinPartitionBlocks[i2].stripLengths[i5]; i6++) {
							Nif.NifStream((ushort)skinPartitionBlocks[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version <= 0x0A000102) {
				if ((skinPartitionBlocks[i2].numStrips == 0)) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].triangles.Count; i5++) {
						Nif.NifStream(skinPartitionBlocks[i2].triangles[i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((skinPartitionBlocks[i2].hasFaces && (skinPartitionBlocks[i2].numStrips == 0))) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].triangles.Count; i5++) {
						Nif.NifStream((Triangle)skinPartitionBlocks[i2].triangles[i5], s, info);
					}
				}
			}
			Nif.NifStream(skinPartitionBlocks[i2].hasBoneIndices, s, info);
			if (skinPartitionBlocks[i2].hasBoneIndices) {
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].boneIndices.Count; i4++) {
					for (var i5 = 0; i5 < skinPartitionBlocks[i2].boneIndices[i4].Count; i5++) {
						Nif.NifStream(skinPartitionBlocks[i2].boneIndices[i4][i5], s, info);
					}
				}
			}
			if ((info.userVersion2 > 34)) {
				Nif.NifStream(skinPartitionBlocks[i2].unknownShort, s, info);
			}
			if (info.userVersion2 == 100) {
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vf1, s, info);
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vf2, s, info);
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vf3, s, info);
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vf4, s, info);
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vf5, s, info);
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(skinPartitionBlocks[i2].vertexDesc.vf8, s, info);
				for (var i4 = 0; i4 < skinPartitionBlocks[i2].trianglesCopy.Count; i4++) {
					Nif.NifStream(skinPartitionBlocks[i2].trianglesCopy[i4], s, info);
				}
			}
		}
	}
	if (info.userVersion2 == 100) {
		Nif.NifStream(dataSize, s, info);
		Nif.NifStream(vertexSize, s, info);
		Nif.NifStream(vertexDesc.vf1, s, info);
		Nif.NifStream(vertexDesc.vf2, s, info);
		Nif.NifStream(vertexDesc.vf3, s, info);
		Nif.NifStream(vertexDesc.vf4, s, info);
		Nif.NifStream(vertexDesc.vf5, s, info);
		Nif.NifStream(vertexDesc.vertexAttributes, s, info);
		Nif.NifStream(vertexDesc.vf8, s, info);
		if ((dataSize > 0)) {
			for (var i3 = 0; i3 < vertexData.Count; i3++) {
				Nif.NifStream(vertexData[i3], s, info, vertexDesc.vertexAttributes);
			}
		}
		for (var i2 = 0; i2 < partition.Count; i2++) {
			for (var i3 = 0; i3 < partition[i2].strips.Count; i3++)
				partition[i2].stripLengths[i3] = (ushort)partition[i2].strips[i3].Count;
			partition[i2].numWeightsPerVertex = (ushort)((partition[i2].vertexWeights.Count > 0) ? partition[i2].vertexWeights[0].Count : 0);
			partition[i2].numStrips = (ushort)partition[i2].stripLengths.Count;
			partition[i2].numBones = (ushort)partition[i2].bones.Count;
			partition[i2].numTriangles = partition[i2].numTrianglesCalc(info);
			partition[i2].numVertices = (ushort)partition[i2].vertexMap.Count;
			Nif.NifStream(partition[i2].numVertices, s, info);
			Nif.NifStream(partition[i2].numTriangles, s, info);
			Nif.NifStream(partition[i2].numBones, s, info);
			Nif.NifStream(partition[i2].numStrips, s, info);
			Nif.NifStream(partition[i2].numWeightsPerVertex, s, info);
			for (var i3 = 0; i3 < partition[i2].bones.Count; i3++) {
				Nif.NifStream(partition[i2].bones[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(partition[i2].hasVertexMap, s, info);
			}
			if (info.version <= 0x0A000102) {
				for (var i4 = 0; i4 < partition[i2].vertexMap.Count; i4++) {
					Nif.NifStream(partition[i2].vertexMap[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000) {
				if (partition[i2].hasVertexMap) {
					for (var i5 = 0; i5 < partition[i2].vertexMap.Count; i5++) {
						Nif.NifStream((ushort)partition[i2].vertexMap[i5], s, info);
					}
				}
				Nif.NifStream(partition[i2].hasVertexWeights, s, info);
			}
			if (info.version <= 0x0A000102) {
				for (var i4 = 0; i4 < partition[i2].vertexWeights.Count; i4++) {
					for (var i5 = 0; i5 < partition[i2].vertexWeights[i4].Count; i5++) {
						Nif.NifStream(partition[i2].vertexWeights[i4][i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if (partition[i2].hasVertexWeights) {
					for (var i5 = 0; i5 < partition[i2].vertexWeights.Count; i5++) {
						for (var i6 = 0; i6 < partition[i2].vertexWeights[i5].Count; i6++) {
							Nif.NifStream((float)partition[i2].vertexWeights[i5][i6], s, info);
						}
					}
				}
			}
			for (var i3 = 0; i3 < partition[i2].stripLengths.Count; i3++) {
				Nif.NifStream(partition[i2].stripLengths[i3], s, info);
			}
			if (info.version >= 0x0A010000) {
				Nif.NifStream(partition[i2].hasFaces, s, info);
			}
			if (info.version <= 0x0A000102) {
				if ((partition[i2].numStrips != 0)) {
					for (var i5 = 0; i5 < partition[i2].strips.Count; i5++) {
						for (var i6 = 0; i6 < partition[i2].stripLengths[i5]; i6++) {
							Nif.NifStream(partition[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((partition[i2].hasFaces && (partition[i2].numStrips != 0))) {
					for (var i5 = 0; i5 < partition[i2].strips.Count; i5++) {
						for (var i6 = 0; i6 < partition[i2].stripLengths[i5]; i6++) {
							Nif.NifStream((ushort)partition[i2].strips[i5][i6], s, info);
						}
					}
				}
			}
			if (info.version <= 0x0A000102) {
				if ((partition[i2].numStrips == 0)) {
					for (var i5 = 0; i5 < partition[i2].triangles.Count; i5++) {
						Nif.NifStream(partition[i2].triangles[i5], s, info);
					}
				}
			}
			if (info.version >= 0x0A010000) {
				if ((partition[i2].hasFaces && (partition[i2].numStrips == 0))) {
					for (var i5 = 0; i5 < partition[i2].triangles.Count; i5++) {
						Nif.NifStream((Triangle)partition[i2].triangles[i5], s, info);
					}
				}
			}
			Nif.NifStream(partition[i2].hasBoneIndices, s, info);
			if (partition[i2].hasBoneIndices) {
				for (var i4 = 0; i4 < partition[i2].boneIndices.Count; i4++) {
					for (var i5 = 0; i5 < partition[i2].boneIndices[i4].Count; i5++) {
						Nif.NifStream(partition[i2].boneIndices[i4][i5], s, info);
					}
				}
			}
			if ((info.userVersion2 > 34)) {
				Nif.NifStream(partition[i2].unknownShort, s, info);
			}
			if (info.userVersion2 == 100) {
				Nif.NifStream(partition[i2].vertexDesc.vf1, s, info);
				Nif.NifStream(partition[i2].vertexDesc.vf2, s, info);
				Nif.NifStream(partition[i2].vertexDesc.vf3, s, info);
				Nif.NifStream(partition[i2].vertexDesc.vf4, s, info);
				Nif.NifStream(partition[i2].vertexDesc.vf5, s, info);
				Nif.NifStream(partition[i2].vertexDesc.vertexAttributes, s, info);
				Nif.NifStream(partition[i2].vertexDesc.vf8, s, info);
				for (var i4 = 0; i4 < partition[i2].trianglesCopy.Count; i4++) {
					Nif.NifStream(partition[i2].trianglesCopy[i4], s, info);
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
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	numSkinPartitionBlocks = (uint)skinPartitionBlocks.Count;
	s.AppendLine($"  Num Skin Partition Blocks:  {numSkinPartitionBlocks}");
	array_output_count = 0;
	for (var i1 = 0; i1 < skinPartitionBlocks.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < skinPartitionBlocks[i1].strips.Count; i2++)
			skinPartitionBlocks[i1].stripLengths[i2] = (ushort)skinPartitionBlocks[i1].strips[i2].Count;
		skinPartitionBlocks[i1].numWeightsPerVertex = (ushort)((skinPartitionBlocks[i1].vertexWeights.Count > 0) ? skinPartitionBlocks[i1].vertexWeights[0].Count : 0);
		skinPartitionBlocks[i1].numStrips = (ushort)skinPartitionBlocks[i1].stripLengths.Count;
		skinPartitionBlocks[i1].numBones = (ushort)skinPartitionBlocks[i1].bones.Count;
		skinPartitionBlocks[i1].numVertices = (ushort)skinPartitionBlocks[i1].vertexMap.Count;
		s.AppendLine($"    Num Vertices:  {skinPartitionBlocks[i1].numVertices}");
		s.AppendLine($"    Num Triangles:  {skinPartitionBlocks[i1].numTriangles}");
		s.AppendLine($"    Num Bones:  {skinPartitionBlocks[i1].numBones}");
		s.AppendLine($"    Num Strips:  {skinPartitionBlocks[i1].numStrips}");
		s.AppendLine($"    Num Weights Per Vertex:  {skinPartitionBlocks[i1].numWeightsPerVertex}");
		array_output_count = 0;
		for (var i2 = 0; i2 < skinPartitionBlocks[i1].bones.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Bones[{i2}]:  {skinPartitionBlocks[i1].bones[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Has Vertex Map:  {skinPartitionBlocks[i1].hasVertexMap}");
		array_output_count = 0;
		for (var i2 = 0; i2 < skinPartitionBlocks[i1].vertexMap.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Map[{i2}]:  {skinPartitionBlocks[i1].vertexMap[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Has Vertex Weights:  {skinPartitionBlocks[i1].hasVertexWeights}");
		array_output_count = 0;
		for (var i2 = 0; i2 < skinPartitionBlocks[i1].vertexWeights.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			for (var i3 = 0; i3 < skinPartitionBlocks[i1].vertexWeights[i2].Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Vertex Weights[{i3}]:  {skinPartitionBlocks[i1].vertexWeights[i2][i3]}");
				array_output_count++;
			}
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < skinPartitionBlocks[i1].stripLengths.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Strip Lengths[{i2}]:  {skinPartitionBlocks[i1].stripLengths[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Has Faces:  {skinPartitionBlocks[i1].hasFaces}");
		if ((skinPartitionBlocks[i1].numStrips != 0)) {
			array_output_count = 0;
			for (var i3 = 0; i3 < skinPartitionBlocks[i1].strips.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < skinPartitionBlocks[i1].stripLengths[i3]; i4++) {
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
						break;
					}
					s.AppendLine($"          Strips[{i4}]:  {skinPartitionBlocks[i1].strips[i3][i4]}");
					array_output_count++;
				}
			}
		}
		if ((skinPartitionBlocks[i1].numStrips == 0)) {
			array_output_count = 0;
			for (var i3 = 0; i3 < skinPartitionBlocks[i1].triangles.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Triangles[{i3}]:  {skinPartitionBlocks[i1].triangles[i3]}");
				array_output_count++;
			}
		}
		s.AppendLine($"    Has Bone Indices:  {skinPartitionBlocks[i1].hasBoneIndices}");
		if (skinPartitionBlocks[i1].hasBoneIndices) {
			array_output_count = 0;
			for (var i3 = 0; i3 < skinPartitionBlocks[i1].boneIndices.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < skinPartitionBlocks[i1].boneIndices[i3].Count; i4++) {
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
						break;
					}
					s.AppendLine($"          Bone Indices[{i4}]:  {skinPartitionBlocks[i1].boneIndices[i3][i4]}");
					array_output_count++;
				}
			}
		}
		s.AppendLine($"    Unknown Short:  {skinPartitionBlocks[i1].unknownShort}");
		s.AppendLine($"    VF1:  {skinPartitionBlocks[i1].vertexDesc.vf1}");
		s.AppendLine($"    VF2:  {skinPartitionBlocks[i1].vertexDesc.vf2}");
		s.AppendLine($"    VF3:  {skinPartitionBlocks[i1].vertexDesc.vf3}");
		s.AppendLine($"    VF4:  {skinPartitionBlocks[i1].vertexDesc.vf4}");
		s.AppendLine($"    VF5:  {skinPartitionBlocks[i1].vertexDesc.vf5}");
		s.AppendLine($"    Vertex Attributes:  {skinPartitionBlocks[i1].vertexDesc.vertexAttributes}");
		s.AppendLine($"    VF8:  {skinPartitionBlocks[i1].vertexDesc.vf8}");
		array_output_count = 0;
		for (var i2 = 0; i2 < skinPartitionBlocks[i1].trianglesCopy.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Triangles Copy[{i2}]:  {skinPartitionBlocks[i1].trianglesCopy[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Data Size:  {dataSize}");
	s.AppendLine($"  Vertex Size:  {vertexSize}");
	s.AppendLine($"  VF1:  {vertexDesc.vf1}");
	s.AppendLine($"  VF2:  {vertexDesc.vf2}");
	s.AppendLine($"  VF3:  {vertexDesc.vf3}");
	s.AppendLine($"  VF4:  {vertexDesc.vf4}");
	s.AppendLine($"  VF5:  {vertexDesc.vf5}");
	s.AppendLine($"  Vertex Attributes:  {vertexDesc.vertexAttributes}");
	s.AppendLine($"  VF8:  {vertexDesc.vf8}");
	if ((dataSize > 0)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < vertexData.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Data[{i2}]:  {vertexData[i2]}");
			array_output_count++;
		}
	}
	array_output_count = 0;
	for (var i1 = 0; i1 < partition.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		for (var i2 = 0; i2 < partition[i1].strips.Count; i2++)
			partition[i1].stripLengths[i2] = (ushort)partition[i1].strips[i2].Count;
		partition[i1].numWeightsPerVertex = (ushort)((partition[i1].vertexWeights.Count > 0) ? partition[i1].vertexWeights[0].Count : 0);
		partition[i1].numStrips = (ushort)partition[i1].stripLengths.Count;
		partition[i1].numBones = (ushort)partition[i1].bones.Count;
		partition[i1].numVertices = (ushort)partition[i1].vertexMap.Count;
		s.AppendLine($"    Num Vertices:  {partition[i1].numVertices}");
		s.AppendLine($"    Num Triangles:  {partition[i1].numTriangles}");
		s.AppendLine($"    Num Bones:  {partition[i1].numBones}");
		s.AppendLine($"    Num Strips:  {partition[i1].numStrips}");
		s.AppendLine($"    Num Weights Per Vertex:  {partition[i1].numWeightsPerVertex}");
		array_output_count = 0;
		for (var i2 = 0; i2 < partition[i1].bones.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Bones[{i2}]:  {partition[i1].bones[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Has Vertex Map:  {partition[i1].hasVertexMap}");
		array_output_count = 0;
		for (var i2 = 0; i2 < partition[i1].vertexMap.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Vertex Map[{i2}]:  {partition[i1].vertexMap[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Has Vertex Weights:  {partition[i1].hasVertexWeights}");
		array_output_count = 0;
		for (var i2 = 0; i2 < partition[i1].vertexWeights.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			for (var i3 = 0; i3 < partition[i1].vertexWeights[i2].Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Vertex Weights[{i3}]:  {partition[i1].vertexWeights[i2][i3]}");
				array_output_count++;
			}
		}
		array_output_count = 0;
		for (var i2 = 0; i2 < partition[i1].stripLengths.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Strip Lengths[{i2}]:  {partition[i1].stripLengths[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Has Faces:  {partition[i1].hasFaces}");
		if ((partition[i1].numStrips != 0)) {
			array_output_count = 0;
			for (var i3 = 0; i3 < partition[i1].strips.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < partition[i1].stripLengths[i3]; i4++) {
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
						break;
					}
					s.AppendLine($"          Strips[{i4}]:  {partition[i1].strips[i3][i4]}");
					array_output_count++;
				}
			}
		}
		if ((partition[i1].numStrips == 0)) {
			array_output_count = 0;
			for (var i3 = 0; i3 < partition[i1].triangles.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Triangles[{i3}]:  {partition[i1].triangles[i3]}");
				array_output_count++;
			}
		}
		s.AppendLine($"    Has Bone Indices:  {partition[i1].hasBoneIndices}");
		if (partition[i1].hasBoneIndices) {
			array_output_count = 0;
			for (var i3 = 0; i3 < partition[i1].boneIndices.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				for (var i4 = 0; i4 < partition[i1].boneIndices[i3].Count; i4++) {
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
						break;
					}
					s.AppendLine($"          Bone Indices[{i4}]:  {partition[i1].boneIndices[i3][i4]}");
					array_output_count++;
				}
			}
		}
		s.AppendLine($"    Unknown Short:  {partition[i1].unknownShort}");
		s.AppendLine($"    VF1:  {partition[i1].vertexDesc.vf1}");
		s.AppendLine($"    VF2:  {partition[i1].vertexDesc.vf2}");
		s.AppendLine($"    VF3:  {partition[i1].vertexDesc.vf3}");
		s.AppendLine($"    VF4:  {partition[i1].vertexDesc.vf4}");
		s.AppendLine($"    VF5:  {partition[i1].vertexDesc.vf5}");
		s.AppendLine($"    Vertex Attributes:  {partition[i1].vertexDesc.vertexAttributes}");
		s.AppendLine($"    VF8:  {partition[i1].vertexDesc.vf8}");
		array_output_count = 0;
		for (var i2 = 0; i2 < partition[i1].trianglesCopy.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Triangles Copy[{i2}]:  {partition[i1].trianglesCopy[i2]}");
			array_output_count++;
		}
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}