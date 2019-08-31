/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Geometry morphing data component. */
	public class Morph
	{
		/*! Name of the frame. */
		internal IndexString frameName;
		/*! The number of morph keys that follow. */
		internal uint numKeys;
		/*! Unlike most objects, the presense of this value is not conditional on there being keys. */
		internal KeyType interpolation;
		/*! The morph key frames. */
		internal IList<Key<float>> keys;
		/*! legacyWeight */
		internal float legacyWeight;
		/*! Morph vectors. */
		internal IList<Vector3> vectors;

		public Morph()
		{
			unchecked
			{
				numKeys = (uint)0;
				interpolation = (KeyType)0;
				legacyWeight = 0.0f;
			}
		}
	}
}
