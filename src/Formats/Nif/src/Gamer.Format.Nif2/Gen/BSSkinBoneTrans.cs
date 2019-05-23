/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Fallout 4 Bone Transform */
	public class BSSkinBoneTrans
	{
		/*! boundingSphere */
		internal NiBound boundingSphere;
		/*! rotation */
		internal Matrix33 rotation;
		/*! translation */
		internal Vector3 translation;
		/*! scale */
		internal float scale;

		public BSSkinBoneTrans()
		{
			unchecked
			{
				scale = 0.0f;
			}
		}
	}
}
