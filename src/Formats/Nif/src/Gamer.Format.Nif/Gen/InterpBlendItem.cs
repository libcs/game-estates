/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! Interpolator item for array in NiBlendInterpolator. */
public class InterpBlendItem {
	/*! Reference to an interpolator. */
	internal NiInterpolator interpolator;
	/*!  */
	internal float weight;
	/*!  */
	internal float normalizedWeight;
	/*!  */
	internal int priority;
	/*!  */
	internal float easeSpinner;
	//Constructor
	public InterpBlendItem() { unchecked {
	interpolator = null;
	weight = 0.0f;
	normalizedWeight = 0.0f;
	priority = (int)0;
	easeSpinner = 0.0f;
	
	} }

}

}
