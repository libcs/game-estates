/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! bhkSpringDamperConstraintMotor */
	public class bhkSpringDamperConstraintMotor
	{
		/*! Minimum motor force */
		internal float minForce;
		/*! Maximum motor force */
		internal float maxForce;
		/*! The spring constant in N/m */
		internal float springConstant;
		/*! The spring damping in Nsec/m */
		internal float springDamping;
		/*! Is Motor enabled */
		internal bool motorEnabled;

		public bhkSpringDamperConstraintMotor()
		{
			unchecked
			{
				minForce = -1000000.0f;
				maxForce = 1000000.0f;
				springConstant = 0f;
				springDamping = 0f;
				motorEnabled = 0;
			}
		}
	}
}
