/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! MotorDescriptor */
	public class MotorDescriptor
	{
		/*! type */
		internal MotorType type;
		/*! positionMotor */
		internal bhkPositionConstraintMotor positionMotor;
		/*! velocityMotor */
		internal bhkVelocityConstraintMotor velocityMotor;
		/*! springDamperMotor */
		internal bhkSpringDamperConstraintMotor springDamperMotor;

		public MotorDescriptor()
		{
			unchecked
			{
				type = MotorType.MOTOR_NONE;
			}
		}
	}
}
