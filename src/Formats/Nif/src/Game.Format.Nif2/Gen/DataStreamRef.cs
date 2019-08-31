/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! DataStreamRef */
	public class DataStreamRef
	{
		/*!
		 * Reference to a data stream object which holds the data used by
		 *             this reference.
		 */
		internal NiDataStream stream;
		/*!
		 * Sets whether this stream data is per-instance data for use in
		 *             hardware instancing.
		 */
		internal bool isPerInstance;
		/*!
		 * The number of submesh-to-region mappings that this data stream
		 *             has.
		 */
		internal ushort numSubmeshes;
		/*! submeshToRegionMap */
		internal IList<ushort> submeshToRegionMap;
		/*! numComponents */
		internal uint numComponents;
		/*! Describes the semantic of each component. */
		internal IList<SemanticData> componentSemantics;

		public DataStreamRef()
		{
			unchecked
			{
				stream = null;
				isPerInstance = 0;
				numSubmeshes = (ushort)1;
				numComponents = (uint)1;
			}
		}
	}
}
