/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! NiTexturingProperty::ShaderMap. Shader texture description. */
public class ShaderTexDesc {
	/*!  */
	internal bool hasMap;
	/*!  */
	internal TexDesc map;
	/*! Unique identifier for the Gamebryo shader system. */
	internal uint mapId;
	//Constructor
	public ShaderTexDesc() { unchecked {
	hasMap = false;
	mapId = (uint)0;
	
	} }

}

}
