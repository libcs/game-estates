/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Texture coordinates (u,v). */
	public class HalfTexCoord
	{
		/*! First coordinate. */
		internal hfloat u;
		/*! Second coordinate. */
		internal hfloat v;

		public HalfTexCoord()
		{
			unchecked
			{
				u = (hfloat)0;
				v = (hfloat)0;
			}
		}
	}
}
