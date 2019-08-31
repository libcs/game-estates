/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! The distance range where a specific level of detail applies. */
	public class LODRange
	{
		/*! Begining of range. */
		internal float nearExtent;
		/*! End of Range. */
		internal float farExtent;
		/*! Unknown (0,0,0). */
		internal Array3<uint> unknownInts;

		public LODRange()
		{
			unchecked
			{
				nearExtent = 0.0f;
				farExtent = 0.0f;
			}
		}
	}
}
