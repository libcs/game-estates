/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NiPhysXJointLimit */
	public class NiPhysXJointLimit
	{
		/*! limitPlaneNormal */
		internal Vector3 limitPlaneNormal;
		/*! limitPlaneD */
		internal float limitPlaneD;
		/*! limitPlaneR */
		internal float limitPlaneR;

		public NiPhysXJointLimit()
		{
			unchecked
			{
				limitPlaneD = 0.0f;
				limitPlaneR = 0.0f;
			}
		}
	}
}
