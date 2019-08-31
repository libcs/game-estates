/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! PhysXMaterialRef */
	public class PhysXMaterialRef
	{
		/*! key */
		internal ushort key;
		/*! materialDesc */
		internal NiPhysXMaterialDesc materialDesc;

		public PhysXMaterialRef()
		{
			unchecked
			{
				key = (ushort)0;
				materialDesc = null;
			}
		}
	}
}
