/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A color with alpha (red, green, blue, alpha). */
	public class ByteColor4
	{
		/*! Red color component. */
		internal byte r;
		/*! Green color component. */
		internal byte g;
		/*! Blue color component. */
		internal byte b;
		/*! Alpha color component. */
		internal byte a;

		public ByteColor4()
		{
			unchecked
			{
				r = (byte)0;
				g = (byte)0;
				b = (byte)0;
				a = (byte)0;
			}
		}
	}
}
