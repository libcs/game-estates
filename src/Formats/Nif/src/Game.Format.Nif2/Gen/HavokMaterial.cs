/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Bethesda Havok. Material wrapper for varying material enums by game. */
	public class HavokMaterial
	{
		/*! Unknown. */
		internal uint unknownInt;
		/*! The material of the shape. */
		internal OblivionHavokMaterial material_ob;
		/*! The material of the shape. */
		internal Fallout3HavokMaterial material_fo;
		/*! The material of the shape. */
		internal SkyrimHavokMaterial material_sk;

		public HavokMaterial()
		{
			unchecked
			{
				unknownInt = (uint)0;
				material_ob = (OblivionHavokMaterial)0;
				material_fo = (Fallout3HavokMaterial)0;
				material_sk = (SkyrimHavokMaterial)0;
			}
		}
	}
}
