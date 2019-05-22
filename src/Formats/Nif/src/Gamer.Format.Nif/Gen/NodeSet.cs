/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! A set of NiNode references. */
public class NodeSet {
	/*! Number of node references that follow. */
	internal uint numNodes;
	/*! The list of NiNode references. */
	internal IList<NiNode> nodes;
	//Constructor
	public NodeSet() { unchecked {
	numNodes = (uint)0;
	
	} }

}

}
