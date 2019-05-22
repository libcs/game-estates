/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class PhysXBodyStoredVels {
	/*!  */
	internal Vector3 linearVelocity;
	/*!  */
	internal Vector3 angularVelocity;
	/*!  */
	internal bool sleep;
	//Constructor
	public PhysXBodyStoredVels() { unchecked {
	sleep = false;
	
	} }

}

}
