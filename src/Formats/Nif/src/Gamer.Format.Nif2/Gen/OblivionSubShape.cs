/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Bethesda Havok. Havok Information for packed TriStrip shapes. */
	public class OblivionSubShape
	{
		/*! havokFilter */
		internal HavokFilter havokFilter;
		/*! The number of vertices that form this sub shape. */
		internal uint numVertices;
		/*! The material of the subshape. */
		internal HavokMaterial material;

		public OblivionSubShape()
		{
			unchecked
			{
				numVertices = (uint)0;
			}
		}
	}
}
