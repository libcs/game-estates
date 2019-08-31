/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Particle Description. */
	public class ParticleDesc
	{
		/*! Unknown. */
		internal Vector3 translation;
		/*! Unknown. */
		internal Array3<float> unknownFloats1;
		/*! Unknown. */
		internal float unknownFloat1;
		/*! Unknown. */
		internal float unknownFloat2;
		/*! Unknown. */
		internal float unknownFloat3;
		/*! Unknown. */
		internal int unknownInt1;

		public ParticleDesc()
		{
			unchecked
			{
				unknownFloat1 = 0.9f;
				unknownFloat2 = 0.9f;
				unknownFloat3 = 3.0f;
				unknownInt1 = (int)0;
			}
		}
	}
}
