/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A vector in 3D space (x,y,z). */
	public class ByteVector3
	{
		/*! First coordinate. */
		internal byte x;
		/*! Second coordinate. */
		internal byte y;
		/*! Third coordinate. */
		internal byte z;

		public ByteVector3()
		{
			unchecked
			{
				x = (byte)0;
				y = (byte)0;
				z = (byte)0;
			}
		}
	}
}
