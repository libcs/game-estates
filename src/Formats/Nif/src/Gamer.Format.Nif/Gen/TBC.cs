/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Tension, bias, continuity. */
public class TBC {
	/*! Tension. */
	internal float t;
	/*! Bias. */
	internal float b;
	/*! Continuity. */
	internal float c;
	//Constructor
	public TBC() { unchecked {
	t = 0.0f;
	b = 0.0f;
	c = 0.0f;
	
	} }

}

}
