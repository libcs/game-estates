/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! bhkVelocityConstraintMotor */
	public class bhkVelocityConstraintMotor
	{
		/*! Minimum motor force */
		internal float minForce;
		/*! Maximum motor force */
		internal float maxForce;
		/*! Relative stiffness */
		internal float tau;
		/*! targetVelocity */
		internal float targetVelocity;
		/*! useVelocityTarget */
		internal bool useVelocityTarget;
		/*! Is Motor enabled */
		internal bool motorEnabled;

		public bhkVelocityConstraintMotor()
		{
			unchecked
			{
				minForce = -1000000.0f;
				maxForce = 1000000.0f;
				tau = 0f;
				targetVelocity = 0f;
				useVelocityTarget = 0;
				motorEnabled = 0;
			}
		}
	}
}
