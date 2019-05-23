/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! An array of bytes. */
	public class ByteArray
	{
		/*! The number of bytes in this array */
		internal uint dataSize;
		/*! The bytes which make up the array */
		internal IList<byte> data;

		public ByteArray()
		{
			unchecked
			{
				dataSize = (uint)0;
			}
		}
	}
}
