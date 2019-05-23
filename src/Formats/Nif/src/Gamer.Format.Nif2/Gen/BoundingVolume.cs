/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! BoundingVolume */
	public class BoundingVolume
	{
		/*! Type of collision data. */
		internal BoundVolumeType collisionType;
		/*! sphere */
		internal NiBound sphere;
		/*! box */
		internal BoxBV box;
		/*! capsule */
		internal CapsuleBV capsule;
		/*! halfSpace */
		internal HalfSpaceBV halfSpace;

		public BoundingVolume()
		{
			unchecked
			{
				collisionType = (BoundVolumeType)0;
			}
		}
	}
}
