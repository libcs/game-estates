/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Bethesda-specific. Describes groups of triangles either segmented in a grid (for LOD) or by body part for skinned FO4 meshes. */
	public class BSGeometrySegmentData
	{
		/*! flags */
		internal byte flags;
		/*! Index = previous Index + previous Num Tris in Segment * 3 */
		internal uint index;
		/*! The number of triangles belonging to this segment */
		internal uint numTrisInSegment;
		/*! startIndex */
		internal uint startIndex;
		/*! numPrimitives */
		internal uint numPrimitives;
		/*! parentArrayIndex */
		internal uint parentArrayIndex;
		/*! numSubSegments */
		internal uint numSubSegments;
		/*! subSegment */
		internal IList<BSGeometrySubSegment> subSegment;

		public BSGeometrySegmentData()
		{
			unchecked
			{
				flags = (byte)0;
				index = (uint)0;
				numTrisInSegment = (uint)0;
				startIndex = (uint)0;
				numPrimitives = (uint)0;
				parentArrayIndex = (uint)0;
				numSubSegments = (uint)0;
			}
		}
	}
}
