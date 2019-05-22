/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Geometry morphing data component. */
public class Morph {
	/*! Name of the frame. */
	internal IndexString frameName;
	/*! The number of morph keys that follow. */
	internal uint numKeys;
	/*!
	 * Unlike most objects, the presense of this value is not conditional on there
	 * being keys.
	 */
	internal KeyType interpolation;
	/*! The morph key frames. */
	internal IList<Key<float>> keys;
	/*!  */
	internal float legacyWeight;
	/*! Morph vectors. */
	internal IList<Vector3> vectors;
	//Constructor
	public Morph() { unchecked {
	numKeys = (uint)0;
	interpolation = (KeyType)0;
	legacyWeight = 0.0f;
	
	} }

}

}
