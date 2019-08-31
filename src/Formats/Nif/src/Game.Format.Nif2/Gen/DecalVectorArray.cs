/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Array of Vectors for Decal placement in BSDecalPlacementVectorExtraData. */
	public class DecalVectorArray
	{
		/*! numVectors */
		internal short numVectors;
		/*! Vector XYZ coords */
		internal IList<Vector3> points;
		/*! Vector Normals */
		internal IList<Vector3> normals;

		public DecalVectorArray()
		{
			unchecked
			{
				numVectors = (short)0;
			}
		}
	}
}
