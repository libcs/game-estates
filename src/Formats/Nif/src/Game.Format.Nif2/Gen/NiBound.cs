/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A sphere. */
	public class NiBound
	{
		/*! The sphere's center. */
		internal Vector3 center;
		/*! The sphere's radius. */
		internal float radius;

		public NiBound()
		{
			unchecked
			{
				radius = 0.0f;
			}
		}
	}
}
