/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!  */
public class BSPackedSharedGeomData {
	/*!  */
	internal uint numVerts;
	/*!  */
	internal uint lodLevels;
	/*!  */
	internal uint triCountLod0;
	/*!  */
	internal uint triOffsetLod0;
	/*!  */
	internal uint triCountLod1;
	/*!  */
	internal uint triOffsetLod1;
	/*!  */
	internal uint triCountLod2;
	/*!  */
	internal uint triOffsetLod2;
	/*!  */
	internal uint numCombined;
	/*!  */
	internal IList<BSPackedGeomDataCombined> combined;
	/*!  */
	internal BSVertexDesc vertexDesc;
	//Constructor
	public BSPackedSharedGeomData() { unchecked {
	numVerts = (uint)0;
	lodLevels = (uint)0;
	triCountLod0 = (uint)0;
	triOffsetLod0 = (uint)0;
	triCountLod1 = (uint)0;
	triOffsetLod1 = (uint)0;
	triCountLod2 = (uint)0;
	triOffsetLod2 = (uint)0;
	numCombined = (uint)0;
	
	} }

}

}
