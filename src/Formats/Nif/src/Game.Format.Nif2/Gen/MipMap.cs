/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Description of a mipmap within an NiPixelData object. */
	public class MipMap
	{
		/*! Width of the mipmap image. */
		internal uint width;
		/*! Height of the mipmap image. */
		internal uint height;
		/*! Offset into the pixel data array where this mipmap starts. */
		internal uint offset;

		public MipMap()
		{
			unchecked
			{
				width = (uint)0;
				height = (uint)0;
				offset = (uint)0;
			}
		}
	}
}
