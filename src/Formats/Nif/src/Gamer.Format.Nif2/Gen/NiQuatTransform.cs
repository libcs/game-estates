/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiQuatTransform */
	public class NiQuatTransform
	{
		/*! translation */
		internal Vector3 translation;
		/*! rotation */
		internal Quaternion rotation;
		/*! scale */
		internal float scale;
		/*! Whether each transform component is valid. */
		internal Array3<bool> trsValid;

		public NiQuatTransform()
		{
			unchecked
			{
				scale = 1.0f;
			}
		}
	}
}
