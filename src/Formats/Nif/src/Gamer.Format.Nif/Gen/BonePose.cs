/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! A list of transforms for each bone in bhkPoseArray. */
public class BonePose {
	/*!  */
	internal uint numTransforms;
	/*!  */
	internal IList<BoneTransform> transforms;
	//Constructor
	public BonePose() { unchecked {
	numTransforms = (uint)0;
	
	} }

}

}
