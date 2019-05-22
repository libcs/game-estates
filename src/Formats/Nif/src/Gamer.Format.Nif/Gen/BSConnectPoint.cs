/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BSConnectPoint {
	/*!  */
	internal string parent;
	/*!  */
	internal string name;
	/*!  */
	internal Quaternion rotation;
	/*!  */
	internal Vector3 translation;
	/*!  */
	internal float scale;
	//Constructor
	public BSConnectPoint() { unchecked {
	parent = (string)WorkshopConnectPoints;
	scale = 1.0f;
	
	} }

}

}
