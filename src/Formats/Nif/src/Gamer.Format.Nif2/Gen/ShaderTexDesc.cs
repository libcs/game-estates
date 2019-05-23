/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiTexturingProperty::ShaderMap. Shader texture description. */
	public class ShaderTexDesc
	{
		/*! hasMap */
		internal bool hasMap;
		/*! map */
		internal TexDesc map;
		/*! Unique identifier for the Gamebryo shader system. */
		internal uint mapId;

		public ShaderTexDesc()
		{
			unchecked
			{
				hasMap = false;
				mapId = (uint)0;
			}
		}
	}
}
