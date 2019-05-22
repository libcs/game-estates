/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! A range of indices, which make up a region (such as a submesh). */
public class Region {
	/*!  */
	internal uint startIndex;
	/*!  */
	internal uint numIndices;
	//Constructor
	public Region() { unchecked {
	startIndex = (uint)0;
	numIndices = (uint)0;
	
	} }

}

}
