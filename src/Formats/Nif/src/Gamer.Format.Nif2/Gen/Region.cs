/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A range of indices, which make up a region (such as a submesh). */
	public class Region
	{
		/*! startIndex */
		internal uint startIndex;
		/*! numIndices */
		internal uint numIndices;

		public Region()
		{
			unchecked
			{
				startIndex = (uint)0;
				numIndices = (uint)0;
			}
		}
	}
}
