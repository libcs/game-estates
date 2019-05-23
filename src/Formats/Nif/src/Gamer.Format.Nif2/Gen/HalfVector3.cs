/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A vector in 3D space (x,y,z). */
	public class HalfVector3
	{
		/*! First coordinate. */
		internal hfloat x;
		/*! Second coordinate. */
		internal hfloat y;
		/*! Third coordinate. */
		internal hfloat z;

		public HalfVector3()
		{
			unchecked
			{
				x = (hfloat)0;
				y = (hfloat)0;
				z = (hfloat)0;
			}
		}
	}
}
