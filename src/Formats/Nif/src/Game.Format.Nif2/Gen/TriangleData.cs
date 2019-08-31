/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Bethesda Havok. A triangle with extra data used for physics. */
	public class TriangleData
	{
		/*! The triangle. */
		internal Triangle triangle;
		/*! Additional havok information on how triangles are welded. */
		internal ushort weldingInfo;
		/*! This is the triangle's normal. */
		internal Vector3 normal;

		public TriangleData()
		{
			unchecked
			{
				weldingInfo = (ushort)0;
			}
		}
	}
}
