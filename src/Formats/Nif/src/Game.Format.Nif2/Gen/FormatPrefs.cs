/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiTexture::FormatPrefs. These preferences are a request to the renderer to use a format the most closely matches the settings and may be ignored. */
	public class FormatPrefs
	{
		/*! Requests the way the image will be stored. */
		internal PixelLayout pixelLayout;
		/*! Requests if mipmaps are used or not. */
		internal MipMapFormat useMipmaps;
		/*! Requests no alpha, 1-bit alpha, or */
		internal AlphaFormat alphaFormat;

		public FormatPrefs()
		{
			unchecked
			{
				pixelLayout = (PixelLayout)0;
				useMipmaps = MipMapFormat.MIP_FMT_DEFAULT;
				alphaFormat = AlphaFormat.ALPHA_DEFAULT;
			}
		}
	}
}
