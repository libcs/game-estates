/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class ElementReference {
	/*! The element semantic. */
	internal SemanticData semantic;
	/*! Whether or not to normalize the data. */
	internal uint normalizeFlag;
	//Constructor
	public ElementReference() { unchecked {
	normalizeFlag = (uint)0;
	
	} }

}

}
