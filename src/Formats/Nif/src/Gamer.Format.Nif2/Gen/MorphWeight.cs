/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! MorphWeight */
	public class MorphWeight
	{
		/*! interpolator */
		internal NiInterpolator interpolator;
		/*! weight */
		internal float weight;

		public MorphWeight()
		{
			unchecked
			{
				interpolator = null;
				weight = 0.0f;
			}
		}
	}
}
