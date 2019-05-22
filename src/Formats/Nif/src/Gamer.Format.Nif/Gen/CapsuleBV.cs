/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Capsule Bounding Volume */
public class CapsuleBV {
	/*!  */
	internal Vector3 center;
	/*!  */
	internal Vector3 origin;
	/*!  */
	internal float extent;
	/*!  */
	internal float radius;
	//Constructor
	public CapsuleBV() { unchecked {
	extent = 0.0f;
	radius = 0.0f;
	
	} }

}

}
