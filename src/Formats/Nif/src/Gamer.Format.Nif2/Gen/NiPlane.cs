/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A plane. */
	public class NiPlane
	{
		/*! The plane normal. */
		internal Vector3 normal;
		/*! The plane constant. */
		internal float constant;

		public NiPlane()
		{
			unchecked
			{
				constant = 0.0f;
			}
		}
	}
}
