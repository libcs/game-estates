/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiBoneLODController::SkinInfo. Reference to shape and skin instance. */
	public class SkinInfo
	{
		/*! shape */
		internal NiTriBasedGeom shape;
		/*! skinInstance */
		internal NiSkinInstance skinInstance;

		public SkinInfo()
		{
			unchecked
			{
				shape = null;
				skinInstance = null;
			}
		}
	}
}
