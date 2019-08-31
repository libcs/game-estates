/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Tension, bias, continuity. */
	public class TBC
	{
		/*! Tension. */
		internal float t;
		/*! Bias. */
		internal float b;
		/*! Continuity. */
		internal float c;

		public TBC()
		{
			unchecked
			{
				t = 0.0f;
				b = 0.0f;
				c = 0.0f;
			}
		}
	}
}
