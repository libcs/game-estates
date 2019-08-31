/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Two dimensional screen elements. */
	public class Polygon
	{
		/*! numVertices */
		internal ushort numVertices;
		/*! Offset in vertex array. */
		internal ushort vertexOffset;
		/*! numTriangles */
		internal ushort numTriangles;
		/*! Offset in indices array. */
		internal ushort triangleOffset;

		public Polygon()
		{
			unchecked
			{
				numVertices = (ushort)0;
				vertexOffset = (ushort)0;
				numTriangles = (ushort)0;
				triangleOffset = (ushort)0;
			}
		}
	}
}
