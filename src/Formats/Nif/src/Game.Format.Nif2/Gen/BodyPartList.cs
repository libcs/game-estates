/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Body part list for DismemberSkinInstance */
	public class BodyPartList
	{
		/*! Flags related to the Body Partition */
		internal BSPartFlag partFlag;
		/*! Body Part Index */
		internal BSDismemberBodyPartType bodyPart;

		public BodyPartList()
		{
			unchecked
			{
				partFlag = (BSPartFlag)257;
				bodyPart = (BSDismemberBodyPartType)0;
			}
		}
	}
}
