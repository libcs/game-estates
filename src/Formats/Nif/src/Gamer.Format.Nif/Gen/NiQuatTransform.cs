/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class NiQuatTransform {
	/*!  */
	internal Vector3 translation;
	/*!  */
	internal Quaternion rotation;
	/*!  */
	internal float scale;
	/*! Whether each transform component is valid. */
	internal Array3<bool> trsValid;
	//Constructor
	public NiQuatTransform() { unchecked {
	scale = 1.0f;
	
	} }

}

}
