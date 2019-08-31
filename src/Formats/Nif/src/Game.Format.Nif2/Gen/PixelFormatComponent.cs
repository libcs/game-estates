/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! PixelFormatComponent */
	public class PixelFormatComponent
	{
		/*! Component Type */
		internal PixelComponent type;
		/*! Data Storage Convention */
		internal PixelRepresentation convention;
		/*! Bits per component */
		internal byte bitsPerChannel;
		/*! isSigned */
		internal bool isSigned;

		public PixelFormatComponent()
		{
			unchecked
			{
				type = (PixelComponent)0;
				convention = (PixelRepresentation)0;
				bitsPerChannel = (byte)0;
				isSigned = false;
			}
		}
	}
}
