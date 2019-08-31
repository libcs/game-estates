/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! BSGeometrySegmentSharedData */
	public class BSGeometrySegmentSharedData
	{
		/*! numSegments */
		internal uint numSegments;
		/*! totalSegments */
		internal uint totalSegments;
		/*! segmentStarts */
		internal IList<uint> segmentStarts;
		/*! perSegmentData */
		internal IList<BSGeometryPerSegmentSharedData> perSegmentData;
		/*! ssfLength */
		internal ushort ssfLength;
		/*! ssfFile */
		internal IList<byte> ssfFile;

		public BSGeometrySegmentSharedData()
		{
			unchecked
			{
				numSegments = (uint)0;
				totalSegments = (uint)0;
				ssfLength = (ushort)0;
			}
		}
	}
}
