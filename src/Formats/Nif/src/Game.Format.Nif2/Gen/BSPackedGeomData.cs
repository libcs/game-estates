/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! BSPackedGeomData */
	public class BSPackedGeomData
	{
		/*! numVerts */
		internal uint numVerts;
		/*! lodLevels */
		internal uint lodLevels;
		/*! triCountLod0 */
		internal uint triCountLod0;
		/*! triOffsetLod0 */
		internal uint triOffsetLod0;
		/*! triCountLod1 */
		internal uint triCountLod1;
		/*! triOffsetLod1 */
		internal uint triOffsetLod1;
		/*! triCountLod2 */
		internal uint triCountLod2;
		/*! triOffsetLod2 */
		internal uint triOffsetLod2;
		/*! numCombined */
		internal uint numCombined;
		/*! combined */
		internal IList<BSPackedGeomDataCombined> combined;
		/*! vertexDesc */
		internal BSVertexDesc vertexDesc;
		/*! vertexData */
		internal IList<BSVertexData> vertexData;
		/*! triangles */
		internal IList<Triangle> triangles;

		public BSPackedGeomData()
		{
			unchecked
			{
				numVerts = (uint)0;
				lodLevels = (uint)0;
				triCountLod0 = (uint)0;
				triOffsetLod0 = (uint)0;
				triCountLod1 = (uint)0;
				triOffsetLod1 = (uint)0;
				triCountLod2 = (uint)0;
				triOffsetLod2 = (uint)0;
				numCombined = (uint)0;
			}
		}
	}
}
