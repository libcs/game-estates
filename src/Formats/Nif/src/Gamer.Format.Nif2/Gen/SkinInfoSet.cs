/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A set of NiBoneLODController::SkinInfo. */
	public class SkinInfoSet
	{
		/*! numSkinInfo */
		internal uint numSkinInfo;
		/*! skinInfo */
		internal IList<SkinInfo> skinInfo;

		public SkinInfoSet()
		{
			unchecked
			{
				numSkinInfo = (uint)0;
			}
		}
	}
}
