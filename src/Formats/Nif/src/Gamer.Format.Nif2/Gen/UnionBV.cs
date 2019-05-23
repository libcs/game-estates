/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! UnionBV */
	public class UnionBV
	{
		/*! numBv */
		internal uint numBv;
		/*! boundingVolumes */
		internal IList<BoundingVolume> boundingVolumes;

		public UnionBV()
		{
			unchecked
			{
				numBv = (uint)0;
			}
		}
	}
}
