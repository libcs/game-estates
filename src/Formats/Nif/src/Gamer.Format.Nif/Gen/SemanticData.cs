/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class SemanticData {
	/*!
	 * Type of data (POSITION, POSITION_BP, INDEX, NORMAL, NORMAL_BP,
	 *             TEXCOORD, BLENDINDICES, BLENDWEIGHT, BONE_PALETTE, COLOR,
	 * DISPLAYLIST,
	 *             MORPH_POSITION, BINORMAL_BP, TANGENT_BP).
	 */
	internal IndexString name;
	/*!
	 * An extra index of the data. For example, if there are 3 uv maps,
	 *             then the corresponding TEXCOORD data components would have indices
	 *             0, 1, and 2, respectively.
	 */
	internal uint index;
	//Constructor
	public SemanticData() { unchecked {
	index = (uint)0;
	
	} }

}

}
