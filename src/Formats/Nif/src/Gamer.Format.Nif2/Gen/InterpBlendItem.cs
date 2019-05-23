/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Interpolator item for array in NiBlendInterpolator. */
	public class InterpBlendItem
	{
		/*! Reference to an interpolator. */
		internal NiInterpolator interpolator;
		/*! weight */
		internal float weight;
		/*! normalizedWeight */
		internal float normalizedWeight;
		/*! priority */
		internal int priority;
		/*! easeSpinner */
		internal float easeSpinner;

		public InterpBlendItem()
		{
			unchecked
			{
				interpolator = null;
				weight = 0.0f;
				normalizedWeight = 0.0f;
				priority = (int)0;
				easeSpinner = 0.0f;
			}
		}
	}
}
