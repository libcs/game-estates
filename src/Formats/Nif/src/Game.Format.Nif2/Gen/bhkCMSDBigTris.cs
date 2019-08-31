/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Triangle indices used in pair with "Big Verts" in a bhkCompressedMeshShapeData. */
	public class bhkCMSDBigTris
	{
		/*! triangle1 */
		internal ushort triangle1;
		/*! triangle2 */
		internal ushort triangle2;
		/*! triangle3 */
		internal ushort triangle3;
		/*! Always 0? */
		internal uint material;
		/*! weldingInfo */
		internal ushort weldingInfo;

		public bhkCMSDBigTris()
		{
			unchecked
			{
				triangle1 = (ushort)0;
				triangle2 = (ushort)0;
				triangle3 = (ushort)0;
				material = (uint)0;
				weldingInfo = (ushort)0;
			}
		}
	}
}
