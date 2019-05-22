/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! This is only defined because of recursion issues. */
public class BSGeometrySubSegment {
	/*!  */
	internal uint startIndex;
	/*!  */
	internal uint numPrimitives;
	/*!  */
	internal uint parentArrayIndex;
	/*!  */
	internal uint unused;
	//Constructor
	public BSGeometrySubSegment() { unchecked {
	startIndex = (uint)0;
	numPrimitives = (uint)0;
	parentArrayIndex = (uint)0;
	unused = (uint)0;
	
	} }

}

}
