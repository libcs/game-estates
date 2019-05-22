/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Group of vertex indices of vertices that match. */
public class MatchGroup {
	/*! Number of vertices in this group. */
	internal ushort numVertices;
	/*! The vertex indices. */
	internal IList<ushort> vertexIndices;
	//Constructor
	public MatchGroup() { unchecked {
	numVertices = (ushort)0;
	
	} }

}

}
