/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Stores Bone Level of Detail info in a BSBoneLODExtraData */
	public class BoneLOD
	{
		/*! distance */
		internal uint distance;
		/*! boneName */
		internal IndexString boneName;

		public BoneLOD()
		{
			unchecked
			{
				distance = (uint)0;
			}
		}
	}
}
