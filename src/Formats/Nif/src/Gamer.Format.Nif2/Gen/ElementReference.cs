/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! ElementReference */
	public class ElementReference
	{
		/*! The element semantic. */
		internal SemanticData semantic;
		/*! Whether or not to normalize the data. */
		internal uint normalizeFlag;

		public ElementReference()
		{
			unchecked
			{
				normalizeFlag = (uint)0;
			}
		}
	}
}
