/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A list of transforms for each bone in bhkPoseArray. */
	public class BonePose
	{
		/*! numTransforms */
		internal uint numTransforms;
		/*! transforms */
		internal IList<BoneTransform> transforms;

		public BonePose()
		{
			unchecked
			{
				numTransforms = (uint)0;
			}
		}
	}
}
