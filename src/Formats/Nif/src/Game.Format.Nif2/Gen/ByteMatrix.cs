/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! An array of bytes. */
	public class ByteMatrix
	{
		/*! The number of bytes in this array */
		internal uint dataSize1;
		/*! The number of bytes in this array */
		internal uint dataSize2;
		/*! The bytes which make up the array */
		internal IList<byte[]> data;

		public ByteMatrix()
		{
			unchecked
			{
				dataSize1 = (uint)0;
				dataSize2 = (uint)0;
			}
		}
	}
}
