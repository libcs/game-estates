/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiSkinData::BoneVertData. A vertex and its weight. */
	public class BoneVertData
	{
		/*! The vertex index, in the mesh. */
		internal ushort index;
		/*! The vertex weight - between 0.0 and 1.0 */
		internal float weight;

		public BoneVertData()
		{
			unchecked
			{
				index = (ushort)0;
				weight = 0.0f;
			}
		}
	}
}
