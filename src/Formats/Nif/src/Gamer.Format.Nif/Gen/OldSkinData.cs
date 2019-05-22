/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Used to store skin weights in NiTriShapeSkinController. */
public class OldSkinData {
	/*! The amount that this bone affects the vertex. */
	internal float vertexWeight;
	/*! The index of the vertex that this weight applies to. */
	internal ushort vertexIndex;
	/*! Unknown.  Perhaps some sort of offset? */
	internal Vector3 unknownVector;
	//Constructor
	public OldSkinData() { unchecked {
	vertexWeight = 0.0f;
	vertexIndex = (ushort)0;
	
	} }

}

}
