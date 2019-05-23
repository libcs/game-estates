/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Defines subshape chunks in bhkCompressedMeshShapeData */
	public class bhkCMSDChunk
	{
		/*! translation */
		internal Vector4 translation;
		/*! Index of material in bhkCompressedMeshShapeData::Chunk Materials */
		internal uint materialIndex;
		/*! Always 65535? */
		internal ushort reference;
		/*! Index of transformation in bhkCompressedMeshShapeData::Chunk Transforms */
		internal ushort transformIndex;
		/*! numVertices */
		internal uint numVertices;
		/*! vertices */
		internal IList<ushort> vertices;
		/*! numIndices */
		internal uint numIndices;
		/*! indices */
		internal IList<ushort> indices;
		/*! numStrips */
		internal uint numStrips;
		/*! strips */
		internal IList<ushort> strips;
		/*! numWeldingInfo */
		internal uint numWeldingInfo;
		/*! weldingInfo */
		internal IList<ushort> weldingInfo;

		public bhkCMSDChunk()
		{
			unchecked
			{
				materialIndex = (uint)0;
				reference = (ushort)0;
				transformIndex = (ushort)0;
				numVertices = (uint)0;
				numIndices = (uint)0;
				numStrips = (uint)0;
				numWeldingInfo = (uint)0;
			}
		}
	}
}
