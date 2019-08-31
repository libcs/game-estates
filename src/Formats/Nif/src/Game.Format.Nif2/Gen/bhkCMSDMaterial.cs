/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Per-chunk material, used in bhkCompressedMeshShapeData */
	public class bhkCMSDMaterial
	{
		/*! material */
		internal SkyrimHavokMaterial material;
		/*! filter */
		internal HavokFilter filter;

		public bhkCMSDMaterial()
		{
			unchecked
			{
				material = (SkyrimHavokMaterial)0;
			}
		}
	}
}
