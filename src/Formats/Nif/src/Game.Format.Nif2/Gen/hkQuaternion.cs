/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A quaternion as it appears in the havok objects. */
	public class hkQuaternion
	{
		/*! The x-coordinate. */
		internal float x;
		/*! The y-coordinate. */
		internal float y;
		/*! The z-coordinate. */
		internal float z;
		/*! The w-coordinate. */
		internal float w;

		public hkQuaternion()
		{
			unchecked
			{
				x = 0.0f;
				y = 0.0f;
				z = 0.0f;
				w = 1.0f;
			}
		}
	}
}
