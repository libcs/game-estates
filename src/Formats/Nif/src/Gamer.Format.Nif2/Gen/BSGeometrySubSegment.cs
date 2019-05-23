/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! This is only defined because of recursion issues. */
	public class BSGeometrySubSegment
	{
		/*! startIndex */
		internal uint startIndex;
		/*! numPrimitives */
		internal uint numPrimitives;
		/*! parentArrayIndex */
		internal uint parentArrayIndex;
		/*! unused */
		internal uint unused;

		public BSGeometrySubSegment()
		{
			unchecked
			{
				startIndex = (uint)0;
				numPrimitives = (uint)0;
				parentArrayIndex = (uint)0;
				unused = (uint)0;
			}
		}
	}
}
