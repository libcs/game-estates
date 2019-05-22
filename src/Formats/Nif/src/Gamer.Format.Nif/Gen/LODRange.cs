/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! The distance range where a specific level of detail applies. */
public class LODRange {
	/*! Begining of range. */
	internal float nearExtent;
	/*! End of Range. */
	internal float farExtent;
	/*! Unknown (0,0,0). */
	internal Array3<uint> unknownInts;
	//Constructor
	public LODRange() { unchecked {
	nearExtent = 0.0f;
	farExtent = 0.0f;
	
	} }

}

}
