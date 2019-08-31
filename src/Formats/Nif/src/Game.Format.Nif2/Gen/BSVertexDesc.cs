/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! BSVertexDesc */
	public class BSVertexDesc
	{
		/*! vf1 */
		internal byte vf1;
		/*! vf2 */
		internal byte vf2;
		/*! vf3 */
		internal byte vf3;
		/*! vf4 */
		internal byte vf4;
		/*! vf5 */
		internal byte vf5;
		/*! vertexAttributes */
		internal VertexFlags vertexAttributes;
		/*! vf8 */
		internal byte vf8;

		public BSVertexDesc()
		{
			unchecked
			{
				vf1 = (byte)0;
				vf2 = (byte)0;
				vf3 = (byte)0;
				vf4 = (byte)0;
				vf5 = (byte)0;
				vertexAttributes = (VertexFlags)0;
				vf8 = (byte)0;
			}
		}
	}
}
