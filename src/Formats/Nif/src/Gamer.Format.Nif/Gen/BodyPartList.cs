/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Body part list for DismemberSkinInstance */
public class BodyPartList {
	/*! Flags related to the Body Partition */
	internal BSPartFlag partFlag;
	/*! Body Part Index */
	internal BSDismemberBodyPartType bodyPart;
	//Constructor
	public BodyPartList() { unchecked {
	partFlag = (BSPartFlag)257;
	bodyPart = (BSDismemberBodyPartType)0;
	
	} }

}

}
