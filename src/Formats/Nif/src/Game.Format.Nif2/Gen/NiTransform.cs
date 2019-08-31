/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiTransform */
	public class NiTransform
	{
		/*! The rotation part of the transformation matrix. */
		internal Matrix33 rotation;
		/*! The translation vector. */
		internal Vector3 translation;
		/*! Scaling part (only uniform scaling is supported). */
		internal float scale;

		public NiTransform()
		{
			unchecked
			{
				scale = 1.0f;
			}
		}
	}
}
