/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! LODInfo */
	public class LODInfo
	{
		/*! numBones */
		internal uint numBones;
		/*! numActiveSkins */
		internal uint numActiveSkins;
		/*! skinIndices */
		internal IList<uint> skinIndices;

		public LODInfo()
		{
			unchecked
			{
				numBones = (uint)0;
				numActiveSkins = (uint)0;
			}
		}
	}
}
