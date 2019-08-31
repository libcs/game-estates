/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Capsule Bounding Volume */
	public class CapsuleBV
	{
		/*! center */
		internal Vector3 center;
		/*! origin */
		internal Vector3 origin;
		/*! extent */
		internal float extent;
		/*! radius */
		internal float radius;

		public CapsuleBV()
		{
			unchecked
			{
				extent = 0.0f;
				radius = 0.0f;
			}
		}
	}
}
