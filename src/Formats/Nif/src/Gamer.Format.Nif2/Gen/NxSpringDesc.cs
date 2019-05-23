/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NxSpringDesc */
	public class NxSpringDesc
	{
		/*! spring */
		internal float spring;
		/*! damper */
		internal float damper;
		/*! targetValue */
		internal float targetValue;

		public NxSpringDesc()
		{
			unchecked
			{
				spring = 0.0f;
				damper = 0.0f;
				targetValue = 0.0f;
			}
		}
	}
}
