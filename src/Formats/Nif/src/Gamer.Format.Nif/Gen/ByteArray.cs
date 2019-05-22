/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! An array of bytes. */
public class ByteArray {
	/*! The number of bytes in this array */
	internal uint dataSize;
	/*! The bytes which make up the array */
	internal IList<byte> data;
	//Constructor
	public ByteArray() { unchecked {
	dataSize = (uint)0;
	
	} }

}

}
