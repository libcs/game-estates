/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Group of vertex indices of vertices that match. */
	public class MatchGroup
	{
		/*! Number of vertices in this group. */
		internal ushort numVertices;
		/*! The vertex indices. */
		internal IList<ushort> vertexIndices;

		public MatchGroup()
		{
			unchecked
			{
				numVertices = (ushort)0;
			}
		}
	}
}
