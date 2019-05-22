/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BoundingVolume {
	/*! Type of collision data. */
	internal BoundVolumeType collisionType;
	/*!  */
	internal NiBound sphere;
	/*!  */
	internal BoxBV box;
	/*!  */
	internal CapsuleBV capsule;
	/*!  */
	internal HalfSpaceBV halfSpace;
	//Constructor
	public BoundingVolume() { unchecked {
	collisionType = (BoundVolumeType)0;
	
	} }

}

}
