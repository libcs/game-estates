/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! hkWorldObjCinfoProperty */
	public class hkWorldObjCinfoProperty
	{
		/*! data */
		internal uint data;
		/*! size */
		internal uint size;
		/*! capacityAndFlags */
		internal uint capacityAndFlags;

		public hkWorldObjCinfoProperty()
		{
			unchecked
			{
				data = (uint)0;
				size = (uint)0;
				capacityAndFlags = (uint)0x80000000;
			}
		}
	}
}
