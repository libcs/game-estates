/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!
 * Skinning data for a submesh, optimized for hardware skinning. Part of
 * NiSkinPartition.
 */
public class SkinPartition {
	/*! Number of vertices in this submesh. */
	internal ushort numVertices;
	/*! Number of triangles in this submesh. */
	internal ushort numTriangles;
	/*! Number of bones influencing this submesh. */
	internal ushort numBones;
	/*! Number of strips in this submesh (zero if not stripped). */
	internal ushort numStrips;
	/*!
	 * Number of weight coefficients per vertex. The Gamebryo engine seems to work well
	 * only if this number is equal to 4, even if there are less than 4 influences per
	 * vertex.
	 */
	internal ushort numWeightsPerVertex;
	/*! List of bones. */
	internal IList<ushort> bones;
	/*! Do we have a vertex map? */
	internal bool hasVertexMap;
	/*!
	 * Maps the weight/influence lists in this submesh to the vertices in the shape
	 * being skinned.
	 */
	internal IList<ushort> vertexMap;
	/*! Do we have vertex weights? */
	internal bool hasVertexWeights;
	/*! The vertex weights. */
	internal IList<float[]> vertexWeights;
	/*! The strip lengths. */
	internal IList<ushort> stripLengths;
	/*! Do we have triangle or strip data? */
	internal bool hasFaces;
	/*! The strips. */
	internal IList<ushort[]> strips;
	/*! The triangles. */
	internal IList<Triangle> triangles;
	/*! Do we have bone indices? */
	internal bool hasBoneIndices;
	/*! Bone indices, they index into 'Bones'. */
	internal IList<byte[]> boneIndices;
	/*! Unknown */
	internal ushort unknownShort;
	/*!  */
	internal BSVertexDesc vertexDesc;
	/*!  */
	internal IList<Triangle> trianglesCopy;
	//Constructor
	public SkinPartition() { unchecked {
	numVertices = (ushort)0;
	numTriangles = (ushort)0;
	numBones = (ushort)0;
	numStrips = (ushort)0;
	numWeightsPerVertex = (ushort)0;
	hasVertexMap = false;
	hasVertexWeights = false;
	hasFaces = false;
	hasBoneIndices = false;
	unknownShort = (ushort)0;
	
	} }

}

}
