/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! This appears to be a 64-bit hash but nif.xml does not have a 64-bit type. */
	public class BSPackedGeomObject
	{
		/*! shapeId1 */
		internal uint shapeId1;
		/*! shapeId2 */
		internal uint shapeId2;

		public BSPackedGeomObject()
		{
			unchecked
			{
				shapeId1 = (uint)0;
				shapeId2 = (uint)0;
			}
		}
	}
}
