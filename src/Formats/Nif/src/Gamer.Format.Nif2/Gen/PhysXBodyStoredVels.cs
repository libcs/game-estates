/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! PhysXBodyStoredVels */
	public class PhysXBodyStoredVels
	{
		/*! linearVelocity */
		internal Vector3 linearVelocity;
		/*! angularVelocity */
		internal Vector3 angularVelocity;
		/*! sleep */
		internal bool sleep;

		public PhysXBodyStoredVels()
		{
			unchecked
			{
				sleep = false;
			}
		}
	}
}
