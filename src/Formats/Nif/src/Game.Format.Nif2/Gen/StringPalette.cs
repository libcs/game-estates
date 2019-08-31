/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A list of \\0 terminated strings. */
	public class StringPalette
	{
		/*! A bunch of 0x00 seperated strings. */
		internal string palette;
		/*! Length of the palette string is repeated here. */
		internal uint length;

		public StringPalette()
		{
			unchecked
			{
				length = (uint)0;
			}
		}
	}
}
