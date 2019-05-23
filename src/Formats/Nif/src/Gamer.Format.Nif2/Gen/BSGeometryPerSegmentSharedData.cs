/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! BSGeometryPerSegmentSharedData */
	public class BSGeometryPerSegmentSharedData
	{
		/*!
		 * If Bone ID is 0xffffffff, this value refers to the Segment at the listed index. Otherwise this is the "Biped Object", which is like the body part types in
		 * Skyrim and earlier.
		 */
		internal uint userIndex;
		/*! A hash of the bone name string. */
		internal uint boneId;
		/*! Maximum of 8. */
		internal uint numCutOffsets;
		/*! cutOffsets */
		internal IList<float> cutOffsets;

		public BSGeometryPerSegmentSharedData()
		{
			unchecked
			{
				userIndex = (uint)0;
				boneId = (uint)0;
				numCutOffsets = (uint)0;
			}
		}
	}
}
