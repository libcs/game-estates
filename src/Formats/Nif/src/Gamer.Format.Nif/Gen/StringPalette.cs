/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! A list of \\0 terminated strings. */
public class StringPalette {
	/*! A bunch of 0x00 seperated strings. */
	internal string palette;
	/*! Length of the palette string is repeated here. */
	internal uint length;
	//Constructor
	public StringPalette() { unchecked {
	length = (uint)0;
	
	} }

}

}
