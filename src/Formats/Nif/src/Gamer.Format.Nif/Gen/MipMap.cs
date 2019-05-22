/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Description of a mipmap within an NiPixelData object. */
public class MipMap {
	/*! Width of the mipmap image. */
	internal uint width;
	/*! Height of the mipmap image. */
	internal uint height;
	/*! Offset into the pixel data array where this mipmap starts. */
	internal uint offset;
	//Constructor
	public MipMap() { unchecked {
	width = (uint)0;
	height = (uint)0;
	offset = (uint)0;
	
	} }

}

}
