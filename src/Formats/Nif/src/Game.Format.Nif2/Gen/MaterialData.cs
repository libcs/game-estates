/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! MaterialData */
	public class MaterialData
	{
		/*! Shader. */
		internal bool hasShader;
		/*! The shader name. */
		internal IndexString shaderName;
		/*! Extra data associated with the shader. A value of -1 means the shader is the default implementation. */
		internal int shaderExtraData;
		/*! numMaterials */
		internal uint numMaterials;
		/*! The name of the material. */
		internal IList<IndexString> materialName;
		/*! Extra data associated with the material. A value of -1 means the material is the default implementation. */
		internal IList<int> materialExtraData;
		/*! The index of the currently active material. */
		internal int activeMaterial;
		/*! Cyanide extension (only in version 10.2.0.0?). */
		internal byte unknownByte;
		/*! Unknown. */
		internal int unknownInteger2;
		/*! Whether the materials for this object always needs to be updated before rendering with them. */
		internal bool materialNeedsUpdate;

		public MaterialData()
		{
			unchecked
			{
				hasShader = false;
				shaderExtraData = (int)0;
				numMaterials = (uint)0;
				activeMaterial = (int)-1;
				unknownByte = (byte)255;
				unknownInteger2 = (int)0;
				materialNeedsUpdate = false;
			}
		}
	}
}
