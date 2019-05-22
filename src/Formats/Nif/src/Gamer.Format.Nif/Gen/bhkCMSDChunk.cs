/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Defines subshape chunks in bhkCompressedMeshShapeData */
public class bhkCMSDChunk {
	/*!  */
	internal Vector4 translation;
	/*! Index of material in bhkCompressedMeshShapeData::Chunk Materials */
	internal uint materialIndex;
	/*! Always 65535? */
	internal ushort reference;
	/*! Index of transformation in bhkCompressedMeshShapeData::Chunk Transforms */
	internal ushort transformIndex;
	/*!  */
	internal uint numVertices;
	/*!  */
	internal IList<ushort> vertices;
	/*!  */
	internal uint numIndices;
	/*!  */
	internal IList<ushort> indices;
	/*!  */
	internal uint numStrips;
	/*!  */
	internal IList<ushort> strips;
	/*!  */
	internal uint numWeldingInfo;
	/*!  */
	internal IList<ushort> weldingInfo;
	//Constructor
	public bhkCMSDChunk() { unchecked {
	materialIndex = (uint)0;
	reference = (ushort)0;
	transformIndex = (ushort)0;
	numVertices = (uint)0;
	numIndices = (uint)0;
	numStrips = (uint)0;
	numWeldingInfo = (uint)0;
	
	} }

}

}
