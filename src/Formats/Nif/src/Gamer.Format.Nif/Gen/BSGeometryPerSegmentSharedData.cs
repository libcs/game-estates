/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BSGeometryPerSegmentSharedData {
	/*!
	 * If Bone ID is 0xffffffff, this value refers to the Segment at the listed index.
	 * Otherwise this is the "Biped Object", which is like the body part types in
	 * Skyrim and earlier.
	 */
	internal uint userIndex;
	/*! A hash of the bone name string. */
	internal uint boneId;
	/*! Maximum of 8. */
	internal uint numCutOffsets;
	/*!  */
	internal IList<float> cutOffsets;
	//Constructor
	public BSGeometryPerSegmentSharedData() { unchecked {
	userIndex = (uint)0;
	boneId = (uint)0;
	numCutOffsets = (uint)0;
	
	} }

}

}
