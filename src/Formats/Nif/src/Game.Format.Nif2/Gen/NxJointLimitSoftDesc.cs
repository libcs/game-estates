/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NxJointLimitSoftDesc */
	public class NxJointLimitSoftDesc
	{
		/*! value */
		internal float value;
		/*! restitution */
		internal float restitution;
		/*! spring */
		internal float spring;
		/*! damping */
		internal float damping;

		public NxJointLimitSoftDesc()
		{
			unchecked
			{
				value = 0.0f;
				restitution = 0.0f;
				spring = 0.0f;
				damping = 0.0f;
			}
		}
	}
}
