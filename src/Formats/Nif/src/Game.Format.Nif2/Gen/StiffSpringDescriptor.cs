/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! StiffSpringDescriptor */
	public class StiffSpringDescriptor
	{
		/*! pivotA */
		internal Vector4 pivotA;
		/*! pivotB */
		internal Vector4 pivotB;
		/*! length */
		internal float length;

		public StiffSpringDescriptor()
		{
			unchecked
			{
				length = 0.0f;
			}
		}
	}
}
