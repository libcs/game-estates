/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.
//-----------------------------------NOTICE----------------------------------//
// Only add custom code in the designated areas to preserve between builds   //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A breakable constraint. */
	public class bhkBreakableConstraint : bhkConstraint
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkBreakableConstraint", bhkConstraint.TYPE);

		/*! Constraint within constraint. */
		internal ConstraintData constraintData;
		/*! Amount of force to break the rigid bodies apart? */
		internal float threshold;
		/*! No: Constraint stays active. Yes: Constraint gets removed when breaking threshold is exceeded. */
		internal bool removeWhenBroken;
		public bhkBreakableConstraint()
		{
			threshold = 0.0f;
			removeWhenBroken = 0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkBreakableConstraint();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out constraintData.type, s, info);
			Nif.NifStream(out constraintData.numEntities2, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out constraintData.priority, s, info);
			if ((constraintData.type == 0))
			{
				Nif.NifStream(out constraintData.ballAndSocket.pivotA, s, info);
				Nif.NifStream(out constraintData.ballAndSocket.pivotB, s, info);
			}
			if ((constraintData.type == 1))
			{
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out constraintData.hinge.pivotA, s, info);
					Nif.NifStream(out constraintData.hinge.perpAxisInA1, s, info);
					Nif.NifStream(out constraintData.hinge.perpAxisInA2, s, info);
					Nif.NifStream(out constraintData.hinge.pivotB, s, info);
					Nif.NifStream(out constraintData.hinge.axisB, s, info);
				}
				if (info.version >= 0x14020007)
				{
					Nif.NifStream(out constraintData.hinge.axisA, s, info);
					Nif.NifStream(out (Vector4)constraintData.hinge.perpAxisInA1, s, info);
					Nif.NifStream(out (Vector4)constraintData.hinge.perpAxisInA2, s, info);
					Nif.NifStream(out (Vector4)constraintData.hinge.pivotA, s, info);
					Nif.NifStream(out (Vector4)constraintData.hinge.axisB, s, info);
					Nif.NifStream(out constraintData.hinge.perpAxisInB1, s, info);
					Nif.NifStream(out constraintData.hinge.perpAxisInB2, s, info);
					Nif.NifStream(out (Vector4)constraintData.hinge.pivotB, s, info);
				}
			}
			if ((constraintData.type == 2))
			{
				if ((info.userVersion2 <= 16))
				{
					Nif.NifStream(out constraintData.limitedHinge.pivotA, s, info);
					Nif.NifStream(out constraintData.limitedHinge.axisA, s, info);
					Nif.NifStream(out constraintData.limitedHinge.perpAxisInA1, s, info);
					Nif.NifStream(out constraintData.limitedHinge.perpAxisInA2, s, info);
					Nif.NifStream(out constraintData.limitedHinge.pivotB, s, info);
					Nif.NifStream(out constraintData.limitedHinge.axisB, s, info);
					Nif.NifStream(out constraintData.limitedHinge.perpAxisInB2, s, info);
				}
				if ((info.userVersion2 > 16))
				{
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.axisA, s, info);
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.perpAxisInA1, s, info);
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.perpAxisInA2, s, info);
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.pivotA, s, info);
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.axisB, s, info);
					Nif.NifStream(out constraintData.limitedHinge.perpAxisInB1, s, info);
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.perpAxisInB2, s, info);
					Nif.NifStream(out (Vector4)constraintData.limitedHinge.pivotB, s, info);
				}
				Nif.NifStream(out constraintData.limitedHinge.minAngle, s, info);
				Nif.NifStream(out constraintData.limitedHinge.maxAngle, s, info);
				Nif.NifStream(out constraintData.limitedHinge.maxFriction, s, info);
				if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
				{
					Nif.NifStream(out constraintData.limitedHinge.motor.type, s, info);
					if ((constraintData.limitedHinge.motor.type == 1))
					{
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.minForce, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.tau, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.damping, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.positionMotor.motorEnabled, s, info);
					}
					if ((constraintData.limitedHinge.motor.type == 2))
					{
						Nif.NifStream(out constraintData.limitedHinge.motor.velocityMotor.minForce, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.velocityMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.velocityMotor.tau, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
					}
					if ((constraintData.limitedHinge.motor.type == 3))
					{
						Nif.NifStream(out constraintData.limitedHinge.motor.springDamperMotor.minForce, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.springDamperMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.springDamperMotor.springConstant, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.springDamperMotor.springDamping, s, info);
						Nif.NifStream(out constraintData.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
					}
				}
			}
			if ((constraintData.type == 6))
			{
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out constraintData.prismatic.pivotA, s, info);
					Nif.NifStream(out constraintData.prismatic.rotationA, s, info);
					Nif.NifStream(out constraintData.prismatic.planeA, s, info);
					Nif.NifStream(out constraintData.prismatic.slidingA, s, info);
					Nif.NifStream(out constraintData.prismatic.slidingB, s, info);
					Nif.NifStream(out constraintData.prismatic.pivotB, s, info);
					Nif.NifStream(out constraintData.prismatic.rotationB, s, info);
					Nif.NifStream(out constraintData.prismatic.planeB, s, info);
				}
				if (info.version >= 0x14020007)
				{
					Nif.NifStream(out (Vector4)constraintData.prismatic.slidingA, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.rotationA, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.planeA, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.pivotA, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.slidingB, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.rotationB, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.planeB, s, info);
					Nif.NifStream(out (Vector4)constraintData.prismatic.pivotB, s, info);
				}
				Nif.NifStream(out constraintData.prismatic.minDistance, s, info);
				Nif.NifStream(out constraintData.prismatic.maxDistance, s, info);
				Nif.NifStream(out constraintData.prismatic.friction, s, info);
				if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
				{
					Nif.NifStream(out constraintData.prismatic.motor.type, s, info);
					if ((constraintData.prismatic.motor.type == 1))
					{
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.minForce, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.tau, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.damping, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.positionMotor.motorEnabled, s, info);
					}
					if ((constraintData.prismatic.motor.type == 2))
					{
						Nif.NifStream(out constraintData.prismatic.motor.velocityMotor.minForce, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.velocityMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.velocityMotor.tau, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.velocityMotor.targetVelocity, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.velocityMotor.motorEnabled, s, info);
					}
					if ((constraintData.prismatic.motor.type == 3))
					{
						Nif.NifStream(out constraintData.prismatic.motor.springDamperMotor.minForce, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.springDamperMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.springDamperMotor.springConstant, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.springDamperMotor.springDamping, s, info);
						Nif.NifStream(out constraintData.prismatic.motor.springDamperMotor.motorEnabled, s, info);
					}
				}
			}
			if ((constraintData.type == 7))
			{
				if ((info.userVersion2 <= 16))
				{
					Nif.NifStream(out constraintData.ragdoll.pivotA, s, info);
					Nif.NifStream(out constraintData.ragdoll.planeA, s, info);
					Nif.NifStream(out constraintData.ragdoll.twistA, s, info);
					Nif.NifStream(out constraintData.ragdoll.pivotB, s, info);
					Nif.NifStream(out constraintData.ragdoll.planeB, s, info);
					Nif.NifStream(out constraintData.ragdoll.twistB, s, info);
				}
				if ((info.userVersion2 > 16))
				{
					Nif.NifStream(out (Vector4)constraintData.ragdoll.twistA, s, info);
					Nif.NifStream(out (Vector4)constraintData.ragdoll.planeA, s, info);
					Nif.NifStream(out constraintData.ragdoll.motorA, s, info);
					Nif.NifStream(out (Vector4)constraintData.ragdoll.pivotA, s, info);
					Nif.NifStream(out (Vector4)constraintData.ragdoll.twistB, s, info);
					Nif.NifStream(out (Vector4)constraintData.ragdoll.planeB, s, info);
					Nif.NifStream(out constraintData.ragdoll.motorB, s, info);
					Nif.NifStream(out (Vector4)constraintData.ragdoll.pivotB, s, info);
				}
				Nif.NifStream(out constraintData.ragdoll.coneMaxAngle, s, info);
				Nif.NifStream(out constraintData.ragdoll.planeMinAngle, s, info);
				Nif.NifStream(out constraintData.ragdoll.planeMaxAngle, s, info);
				Nif.NifStream(out constraintData.ragdoll.twistMinAngle, s, info);
				Nif.NifStream(out constraintData.ragdoll.twistMaxAngle, s, info);
				Nif.NifStream(out constraintData.ragdoll.maxFriction, s, info);
				if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
				{
					Nif.NifStream(out constraintData.ragdoll.motor.type, s, info);
					if ((constraintData.ragdoll.motor.type == 1))
					{
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.minForce, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.tau, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.damping, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.positionMotor.motorEnabled, s, info);
					}
					if ((constraintData.ragdoll.motor.type == 2))
					{
						Nif.NifStream(out constraintData.ragdoll.motor.velocityMotor.minForce, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.velocityMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.velocityMotor.tau, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.velocityMotor.targetVelocity, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.velocityMotor.motorEnabled, s, info);
					}
					if ((constraintData.ragdoll.motor.type == 3))
					{
						Nif.NifStream(out constraintData.ragdoll.motor.springDamperMotor.minForce, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.springDamperMotor.maxForce, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.springDamperMotor.springConstant, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.springDamperMotor.springDamping, s, info);
						Nif.NifStream(out constraintData.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
					}
				}
			}
			if ((constraintData.type == 8))
			{
				Nif.NifStream(out constraintData.stiffSpring.pivotA, s, info);
				Nif.NifStream(out constraintData.stiffSpring.pivotB, s, info);
				Nif.NifStream(out constraintData.stiffSpring.length, s, info);
			}
			if ((constraintData.type == 13))
			{
				Nif.NifStream(out constraintData.malleable.type, s, info);
				Nif.NifStream(out constraintData.malleable.numEntities, s, info);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out constraintData.malleable.priority, s, info);
				if ((constraintData.malleable.type == 0))
				{
					Nif.NifStream(out constraintData.malleable.ballAndSocket.pivotA, s, info);
					Nif.NifStream(out constraintData.malleable.ballAndSocket.pivotB, s, info);
				}
				if ((constraintData.malleable.type == 1))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(out constraintData.malleable.hinge.pivotA, s, info);
						Nif.NifStream(out constraintData.malleable.hinge.perpAxisInA1, s, info);
						Nif.NifStream(out constraintData.malleable.hinge.perpAxisInA2, s, info);
						Nif.NifStream(out constraintData.malleable.hinge.pivotB, s, info);
						Nif.NifStream(out constraintData.malleable.hinge.axisB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(out constraintData.malleable.hinge.axisA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.hinge.perpAxisInA1, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.hinge.perpAxisInA2, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.hinge.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.hinge.axisB, s, info);
						Nif.NifStream(out constraintData.malleable.hinge.perpAxisInB1, s, info);
						Nif.NifStream(out constraintData.malleable.hinge.perpAxisInB2, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.hinge.pivotB, s, info);
					}
				}
				if ((constraintData.malleable.type == 2))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(out constraintData.malleable.limitedHinge.pivotA, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.axisA, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.pivotB, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.axisB, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.perpAxisInB2, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.axisA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.axisB, s, info);
						Nif.NifStream(out constraintData.malleable.limitedHinge.perpAxisInB1, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.perpAxisInB2, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.limitedHinge.pivotB, s, info);
					}
					Nif.NifStream(out constraintData.malleable.limitedHinge.minAngle, s, info);
					Nif.NifStream(out constraintData.malleable.limitedHinge.maxAngle, s, info);
					Nif.NifStream(out constraintData.malleable.limitedHinge.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(out constraintData.malleable.limitedHinge.motor.type, s, info);
						if ((constraintData.malleable.limitedHinge.motor.type == 1))
						{
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.tau, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.damping, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.limitedHinge.motor.type == 2))
						{
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.velocityMotor.tau, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.limitedHinge.motor.type == 3))
						{
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(out constraintData.malleable.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraintData.malleable.type == 6))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(out constraintData.malleable.prismatic.pivotA, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.rotationA, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.planeA, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.slidingA, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.slidingB, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.pivotB, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.rotationB, s, info);
						Nif.NifStream(out constraintData.malleable.prismatic.planeB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.slidingA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.rotationA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.planeA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.slidingB, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.rotationB, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.planeB, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.prismatic.pivotB, s, info);
					}
					Nif.NifStream(out constraintData.malleable.prismatic.minDistance, s, info);
					Nif.NifStream(out constraintData.malleable.prismatic.maxDistance, s, info);
					Nif.NifStream(out constraintData.malleable.prismatic.friction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(out constraintData.malleable.prismatic.motor.type, s, info);
						if ((constraintData.malleable.prismatic.motor.type == 1))
						{
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.tau, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.damping, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.prismatic.motor.type == 2))
						{
							Nif.NifStream(out constraintData.malleable.prismatic.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.velocityMotor.tau, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.prismatic.motor.type == 3))
						{
							Nif.NifStream(out constraintData.malleable.prismatic.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(out constraintData.malleable.prismatic.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraintData.malleable.type == 7))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(out constraintData.malleable.ragdoll.pivotA, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.planeA, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.twistA, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.pivotB, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.planeB, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.twistB, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream(out (Vector4)constraintData.malleable.ragdoll.twistA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.ragdoll.planeA, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.motorA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.ragdoll.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.ragdoll.twistB, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.ragdoll.planeB, s, info);
						Nif.NifStream(out constraintData.malleable.ragdoll.motorB, s, info);
						Nif.NifStream(out (Vector4)constraintData.malleable.ragdoll.pivotB, s, info);
					}
					Nif.NifStream(out constraintData.malleable.ragdoll.coneMaxAngle, s, info);
					Nif.NifStream(out constraintData.malleable.ragdoll.planeMinAngle, s, info);
					Nif.NifStream(out constraintData.malleable.ragdoll.planeMaxAngle, s, info);
					Nif.NifStream(out constraintData.malleable.ragdoll.twistMinAngle, s, info);
					Nif.NifStream(out constraintData.malleable.ragdoll.twistMaxAngle, s, info);
					Nif.NifStream(out constraintData.malleable.ragdoll.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(out constraintData.malleable.ragdoll.motor.type, s, info);
						if ((constraintData.malleable.ragdoll.motor.type == 1))
						{
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.tau, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.damping, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.ragdoll.motor.type == 2))
						{
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.velocityMotor.tau, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.ragdoll.motor.type == 3))
						{
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(out constraintData.malleable.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraintData.malleable.type == 8))
				{
					Nif.NifStream(out constraintData.malleable.stiffSpring.pivotA, s, info);
					Nif.NifStream(out constraintData.malleable.stiffSpring.pivotB, s, info);
					Nif.NifStream(out constraintData.malleable.stiffSpring.length, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out constraintData.malleable.tau, s, info);
					Nif.NifStream(out constraintData.malleable.damping, s, info);
				}
				if (info.version >= 0x14020007)
				{
					Nif.NifStream(out constraintData.malleable.strength, s, info);
				}
			}
			Nif.NifStream(out threshold, s, info);
			Nif.NifStream(out removeWhenBroken, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(constraintData.type, s, info);
			Nif.NifStream(constraintData.numEntities2, s, info);
			WriteRef((NiObject)constraintData.entityA, s, info, link_map, missing_link_stack);
			WriteRef((NiObject)constraintData.entityB, s, info, link_map, missing_link_stack);
			Nif.NifStream(constraintData.priority, s, info);
			if ((constraintData.type == 0))
			{
				Nif.NifStream(constraintData.ballAndSocket.pivotA, s, info);
				Nif.NifStream(constraintData.ballAndSocket.pivotB, s, info);
			}
			if ((constraintData.type == 1))
			{
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(constraintData.hinge.pivotA, s, info);
					Nif.NifStream(constraintData.hinge.perpAxisInA1, s, info);
					Nif.NifStream(constraintData.hinge.perpAxisInA2, s, info);
					Nif.NifStream(constraintData.hinge.pivotB, s, info);
					Nif.NifStream(constraintData.hinge.axisB, s, info);
				}
				if (info.version >= 0x14020007)
				{
					Nif.NifStream(constraintData.hinge.axisA, s, info);
					Nif.NifStream((Vector4)constraintData.hinge.perpAxisInA1, s, info);
					Nif.NifStream((Vector4)constraintData.hinge.perpAxisInA2, s, info);
					Nif.NifStream((Vector4)constraintData.hinge.pivotA, s, info);
					Nif.NifStream((Vector4)constraintData.hinge.axisB, s, info);
					Nif.NifStream(constraintData.hinge.perpAxisInB1, s, info);
					Nif.NifStream(constraintData.hinge.perpAxisInB2, s, info);
					Nif.NifStream((Vector4)constraintData.hinge.pivotB, s, info);
				}
			}
			if ((constraintData.type == 2))
			{
				if ((info.userVersion2 <= 16))
				{
					Nif.NifStream(constraintData.limitedHinge.pivotA, s, info);
					Nif.NifStream(constraintData.limitedHinge.axisA, s, info);
					Nif.NifStream(constraintData.limitedHinge.perpAxisInA1, s, info);
					Nif.NifStream(constraintData.limitedHinge.perpAxisInA2, s, info);
					Nif.NifStream(constraintData.limitedHinge.pivotB, s, info);
					Nif.NifStream(constraintData.limitedHinge.axisB, s, info);
					Nif.NifStream(constraintData.limitedHinge.perpAxisInB2, s, info);
				}
				if ((info.userVersion2 > 16))
				{
					Nif.NifStream((Vector4)constraintData.limitedHinge.axisA, s, info);
					Nif.NifStream((Vector4)constraintData.limitedHinge.perpAxisInA1, s, info);
					Nif.NifStream((Vector4)constraintData.limitedHinge.perpAxisInA2, s, info);
					Nif.NifStream((Vector4)constraintData.limitedHinge.pivotA, s, info);
					Nif.NifStream((Vector4)constraintData.limitedHinge.axisB, s, info);
					Nif.NifStream(constraintData.limitedHinge.perpAxisInB1, s, info);
					Nif.NifStream((Vector4)constraintData.limitedHinge.perpAxisInB2, s, info);
					Nif.NifStream((Vector4)constraintData.limitedHinge.pivotB, s, info);
				}
				Nif.NifStream(constraintData.limitedHinge.minAngle, s, info);
				Nif.NifStream(constraintData.limitedHinge.maxAngle, s, info);
				Nif.NifStream(constraintData.limitedHinge.maxFriction, s, info);
				if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
				{
					Nif.NifStream(constraintData.limitedHinge.motor.type, s, info);
					if ((constraintData.limitedHinge.motor.type == 1))
					{
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.minForce, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.maxForce, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.tau, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.damping, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.positionMotor.motorEnabled, s, info);
					}
					if ((constraintData.limitedHinge.motor.type == 2))
					{
						Nif.NifStream(constraintData.limitedHinge.motor.velocityMotor.minForce, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.velocityMotor.maxForce, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.velocityMotor.tau, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
					}
					if ((constraintData.limitedHinge.motor.type == 3))
					{
						Nif.NifStream(constraintData.limitedHinge.motor.springDamperMotor.minForce, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.springDamperMotor.maxForce, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.springDamperMotor.springConstant, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.springDamperMotor.springDamping, s, info);
						Nif.NifStream(constraintData.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
					}
				}
			}
			if ((constraintData.type == 6))
			{
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(constraintData.prismatic.pivotA, s, info);
					Nif.NifStream(constraintData.prismatic.rotationA, s, info);
					Nif.NifStream(constraintData.prismatic.planeA, s, info);
					Nif.NifStream(constraintData.prismatic.slidingA, s, info);
					Nif.NifStream(constraintData.prismatic.slidingB, s, info);
					Nif.NifStream(constraintData.prismatic.pivotB, s, info);
					Nif.NifStream(constraintData.prismatic.rotationB, s, info);
					Nif.NifStream(constraintData.prismatic.planeB, s, info);
				}
				if (info.version >= 0x14020007)
				{
					Nif.NifStream((Vector4)constraintData.prismatic.slidingA, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.rotationA, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.planeA, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.pivotA, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.slidingB, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.rotationB, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.planeB, s, info);
					Nif.NifStream((Vector4)constraintData.prismatic.pivotB, s, info);
				}
				Nif.NifStream(constraintData.prismatic.minDistance, s, info);
				Nif.NifStream(constraintData.prismatic.maxDistance, s, info);
				Nif.NifStream(constraintData.prismatic.friction, s, info);
				if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
				{
					Nif.NifStream(constraintData.prismatic.motor.type, s, info);
					if ((constraintData.prismatic.motor.type == 1))
					{
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.minForce, s, info);
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.maxForce, s, info);
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.tau, s, info);
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.damping, s, info);
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
						Nif.NifStream(constraintData.prismatic.motor.positionMotor.motorEnabled, s, info);
					}
					if ((constraintData.prismatic.motor.type == 2))
					{
						Nif.NifStream(constraintData.prismatic.motor.velocityMotor.minForce, s, info);
						Nif.NifStream(constraintData.prismatic.motor.velocityMotor.maxForce, s, info);
						Nif.NifStream(constraintData.prismatic.motor.velocityMotor.tau, s, info);
						Nif.NifStream(constraintData.prismatic.motor.velocityMotor.targetVelocity, s, info);
						Nif.NifStream(constraintData.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
						Nif.NifStream(constraintData.prismatic.motor.velocityMotor.motorEnabled, s, info);
					}
					if ((constraintData.prismatic.motor.type == 3))
					{
						Nif.NifStream(constraintData.prismatic.motor.springDamperMotor.minForce, s, info);
						Nif.NifStream(constraintData.prismatic.motor.springDamperMotor.maxForce, s, info);
						Nif.NifStream(constraintData.prismatic.motor.springDamperMotor.springConstant, s, info);
						Nif.NifStream(constraintData.prismatic.motor.springDamperMotor.springDamping, s, info);
						Nif.NifStream(constraintData.prismatic.motor.springDamperMotor.motorEnabled, s, info);
					}
				}
			}
			if ((constraintData.type == 7))
			{
				if ((info.userVersion2 <= 16))
				{
					Nif.NifStream(constraintData.ragdoll.pivotA, s, info);
					Nif.NifStream(constraintData.ragdoll.planeA, s, info);
					Nif.NifStream(constraintData.ragdoll.twistA, s, info);
					Nif.NifStream(constraintData.ragdoll.pivotB, s, info);
					Nif.NifStream(constraintData.ragdoll.planeB, s, info);
					Nif.NifStream(constraintData.ragdoll.twistB, s, info);
				}
				if ((info.userVersion2 > 16))
				{
					Nif.NifStream((Vector4)constraintData.ragdoll.twistA, s, info);
					Nif.NifStream((Vector4)constraintData.ragdoll.planeA, s, info);
					Nif.NifStream(constraintData.ragdoll.motorA, s, info);
					Nif.NifStream((Vector4)constraintData.ragdoll.pivotA, s, info);
					Nif.NifStream((Vector4)constraintData.ragdoll.twistB, s, info);
					Nif.NifStream((Vector4)constraintData.ragdoll.planeB, s, info);
					Nif.NifStream(constraintData.ragdoll.motorB, s, info);
					Nif.NifStream((Vector4)constraintData.ragdoll.pivotB, s, info);
				}
				Nif.NifStream(constraintData.ragdoll.coneMaxAngle, s, info);
				Nif.NifStream(constraintData.ragdoll.planeMinAngle, s, info);
				Nif.NifStream(constraintData.ragdoll.planeMaxAngle, s, info);
				Nif.NifStream(constraintData.ragdoll.twistMinAngle, s, info);
				Nif.NifStream(constraintData.ragdoll.twistMaxAngle, s, info);
				Nif.NifStream(constraintData.ragdoll.maxFriction, s, info);
				if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
				{
					Nif.NifStream(constraintData.ragdoll.motor.type, s, info);
					if ((constraintData.ragdoll.motor.type == 1))
					{
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.minForce, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.maxForce, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.tau, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.damping, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.positionMotor.motorEnabled, s, info);
					}
					if ((constraintData.ragdoll.motor.type == 2))
					{
						Nif.NifStream(constraintData.ragdoll.motor.velocityMotor.minForce, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.velocityMotor.maxForce, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.velocityMotor.tau, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.velocityMotor.targetVelocity, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.velocityMotor.motorEnabled, s, info);
					}
					if ((constraintData.ragdoll.motor.type == 3))
					{
						Nif.NifStream(constraintData.ragdoll.motor.springDamperMotor.minForce, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.springDamperMotor.maxForce, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.springDamperMotor.springConstant, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.springDamperMotor.springDamping, s, info);
						Nif.NifStream(constraintData.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
					}
				}
			}
			if ((constraintData.type == 8))
			{
				Nif.NifStream(constraintData.stiffSpring.pivotA, s, info);
				Nif.NifStream(constraintData.stiffSpring.pivotB, s, info);
				Nif.NifStream(constraintData.stiffSpring.length, s, info);
			}
			if ((constraintData.type == 13))
			{
				Nif.NifStream(constraintData.malleable.type, s, info);
				Nif.NifStream(constraintData.malleable.numEntities, s, info);
				WriteRef((NiObject)constraintData.malleable.entityA, s, info, link_map, missing_link_stack);
				WriteRef((NiObject)constraintData.malleable.entityB, s, info, link_map, missing_link_stack);
				Nif.NifStream(constraintData.malleable.priority, s, info);
				if ((constraintData.malleable.type == 0))
				{
					Nif.NifStream(constraintData.malleable.ballAndSocket.pivotA, s, info);
					Nif.NifStream(constraintData.malleable.ballAndSocket.pivotB, s, info);
				}
				if ((constraintData.malleable.type == 1))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(constraintData.malleable.hinge.pivotA, s, info);
						Nif.NifStream(constraintData.malleable.hinge.perpAxisInA1, s, info);
						Nif.NifStream(constraintData.malleable.hinge.perpAxisInA2, s, info);
						Nif.NifStream(constraintData.malleable.hinge.pivotB, s, info);
						Nif.NifStream(constraintData.malleable.hinge.axisB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(constraintData.malleable.hinge.axisA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.hinge.perpAxisInA1, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.hinge.perpAxisInA2, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.hinge.pivotA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.hinge.axisB, s, info);
						Nif.NifStream(constraintData.malleable.hinge.perpAxisInB1, s, info);
						Nif.NifStream(constraintData.malleable.hinge.perpAxisInB2, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.hinge.pivotB, s, info);
					}
				}
				if ((constraintData.malleable.type == 2))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(constraintData.malleable.limitedHinge.pivotA, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.axisA, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.pivotB, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.axisB, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.perpAxisInB2, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.axisA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.pivotA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.axisB, s, info);
						Nif.NifStream(constraintData.malleable.limitedHinge.perpAxisInB1, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.perpAxisInB2, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.limitedHinge.pivotB, s, info);
					}
					Nif.NifStream(constraintData.malleable.limitedHinge.minAngle, s, info);
					Nif.NifStream(constraintData.malleable.limitedHinge.maxAngle, s, info);
					Nif.NifStream(constraintData.malleable.limitedHinge.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(constraintData.malleable.limitedHinge.motor.type, s, info);
						if ((constraintData.malleable.limitedHinge.motor.type == 1))
						{
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.tau, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.damping, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.limitedHinge.motor.type == 2))
						{
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.velocityMotor.tau, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.limitedHinge.motor.type == 3))
						{
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(constraintData.malleable.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraintData.malleable.type == 6))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(constraintData.malleable.prismatic.pivotA, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.rotationA, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.planeA, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.slidingA, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.slidingB, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.pivotB, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.rotationB, s, info);
						Nif.NifStream(constraintData.malleable.prismatic.planeB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.slidingA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.rotationA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.planeA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.pivotA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.slidingB, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.rotationB, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.planeB, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.prismatic.pivotB, s, info);
					}
					Nif.NifStream(constraintData.malleable.prismatic.minDistance, s, info);
					Nif.NifStream(constraintData.malleable.prismatic.maxDistance, s, info);
					Nif.NifStream(constraintData.malleable.prismatic.friction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(constraintData.malleable.prismatic.motor.type, s, info);
						if ((constraintData.malleable.prismatic.motor.type == 1))
						{
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.tau, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.damping, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.prismatic.motor.type == 2))
						{
							Nif.NifStream(constraintData.malleable.prismatic.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.velocityMotor.tau, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.prismatic.motor.type == 3))
						{
							Nif.NifStream(constraintData.malleable.prismatic.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(constraintData.malleable.prismatic.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraintData.malleable.type == 7))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(constraintData.malleable.ragdoll.pivotA, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.planeA, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.twistA, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.pivotB, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.planeB, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.twistB, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream((Vector4)constraintData.malleable.ragdoll.twistA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.ragdoll.planeA, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.motorA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.ragdoll.pivotA, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.ragdoll.twistB, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.ragdoll.planeB, s, info);
						Nif.NifStream(constraintData.malleable.ragdoll.motorB, s, info);
						Nif.NifStream((Vector4)constraintData.malleable.ragdoll.pivotB, s, info);
					}
					Nif.NifStream(constraintData.malleable.ragdoll.coneMaxAngle, s, info);
					Nif.NifStream(constraintData.malleable.ragdoll.planeMinAngle, s, info);
					Nif.NifStream(constraintData.malleable.ragdoll.planeMaxAngle, s, info);
					Nif.NifStream(constraintData.malleable.ragdoll.twistMinAngle, s, info);
					Nif.NifStream(constraintData.malleable.ragdoll.twistMaxAngle, s, info);
					Nif.NifStream(constraintData.malleable.ragdoll.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(constraintData.malleable.ragdoll.motor.type, s, info);
						if ((constraintData.malleable.ragdoll.motor.type == 1))
						{
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.tau, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.damping, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.ragdoll.motor.type == 2))
						{
							Nif.NifStream(constraintData.malleable.ragdoll.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.velocityMotor.tau, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraintData.malleable.ragdoll.motor.type == 3))
						{
							Nif.NifStream(constraintData.malleable.ragdoll.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(constraintData.malleable.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraintData.malleable.type == 8))
				{
					Nif.NifStream(constraintData.malleable.stiffSpring.pivotA, s, info);
					Nif.NifStream(constraintData.malleable.stiffSpring.pivotB, s, info);
					Nif.NifStream(constraintData.malleable.stiffSpring.length, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(constraintData.malleable.tau, s, info);
					Nif.NifStream(constraintData.malleable.damping, s, info);
				}
				if (info.version >= 0x14020007)
				{
					Nif.NifStream(constraintData.malleable.strength, s, info);
				}
			}
			Nif.NifStream(threshold, s, info);
			Nif.NifStream(removeWhenBroken, s, info);
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			s.Append(base.AsString());
			s.AppendLine($"      Type:  {constraintData.type}");
			s.AppendLine($"      Num Entities 2:  {constraintData.numEntities2}");
			s.AppendLine($"      Entity A:  {constraintData.entityA}");
			s.AppendLine($"      Entity B:  {constraintData.entityB}");
			s.AppendLine($"      Priority:  {constraintData.priority}");
			if ((constraintData.type == 0))
			{
				s.AppendLine($"        Pivot A:  {constraintData.ballAndSocket.pivotA}");
				s.AppendLine($"        Pivot B:  {constraintData.ballAndSocket.pivotB}");
			}
			if ((constraintData.type == 1))
			{
				s.AppendLine($"        Pivot A:  {constraintData.hinge.pivotA}");
				s.AppendLine($"        Perp Axis In A1:  {constraintData.hinge.perpAxisInA1}");
				s.AppendLine($"        Perp Axis In A2:  {constraintData.hinge.perpAxisInA2}");
				s.AppendLine($"        Pivot B:  {constraintData.hinge.pivotB}");
				s.AppendLine($"        Axis B:  {constraintData.hinge.axisB}");
				s.AppendLine($"        Axis A:  {constraintData.hinge.axisA}");
				s.AppendLine($"        Perp Axis In B1:  {constraintData.hinge.perpAxisInB1}");
				s.AppendLine($"        Perp Axis In B2:  {constraintData.hinge.perpAxisInB2}");
			}
			if ((constraintData.type == 2))
			{
				s.AppendLine($"        Pivot A:  {constraintData.limitedHinge.pivotA}");
				s.AppendLine($"        Axis A:  {constraintData.limitedHinge.axisA}");
				s.AppendLine($"        Perp Axis In A1:  {constraintData.limitedHinge.perpAxisInA1}");
				s.AppendLine($"        Perp Axis In A2:  {constraintData.limitedHinge.perpAxisInA2}");
				s.AppendLine($"        Pivot B:  {constraintData.limitedHinge.pivotB}");
				s.AppendLine($"        Axis B:  {constraintData.limitedHinge.axisB}");
				s.AppendLine($"        Perp Axis In B2:  {constraintData.limitedHinge.perpAxisInB2}");
				s.AppendLine($"        Perp Axis In B1:  {constraintData.limitedHinge.perpAxisInB1}");
				s.AppendLine($"        Min Angle:  {constraintData.limitedHinge.minAngle}");
				s.AppendLine($"        Max Angle:  {constraintData.limitedHinge.maxAngle}");
				s.AppendLine($"        Max Friction:  {constraintData.limitedHinge.maxFriction}");
				s.AppendLine($"        Type:  {constraintData.limitedHinge.motor.type}");
				if ((constraintData.limitedHinge.motor.type == 1))
				{
					s.AppendLine($"          Min Force:  {constraintData.limitedHinge.motor.positionMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.limitedHinge.motor.positionMotor.maxForce}");
					s.AppendLine($"          Tau:  {constraintData.limitedHinge.motor.positionMotor.tau}");
					s.AppendLine($"          Damping:  {constraintData.limitedHinge.motor.positionMotor.damping}");
					s.AppendLine($"          Proportional Recovery Velocity:  {constraintData.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity}");
					s.AppendLine($"          Constant Recovery Velocity:  {constraintData.limitedHinge.motor.positionMotor.constantRecoveryVelocity}");
					s.AppendLine($"          Motor Enabled:  {constraintData.limitedHinge.motor.positionMotor.motorEnabled}");
				}
				if ((constraintData.limitedHinge.motor.type == 2))
				{
					s.AppendLine($"          Min Force:  {constraintData.limitedHinge.motor.velocityMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.limitedHinge.motor.velocityMotor.maxForce}");
					s.AppendLine($"          Tau:  {constraintData.limitedHinge.motor.velocityMotor.tau}");
					s.AppendLine($"          Target Velocity:  {constraintData.limitedHinge.motor.velocityMotor.targetVelocity}");
					s.AppendLine($"          Use Velocity Target:  {constraintData.limitedHinge.motor.velocityMotor.useVelocityTarget}");
					s.AppendLine($"          Motor Enabled:  {constraintData.limitedHinge.motor.velocityMotor.motorEnabled}");
				}
				if ((constraintData.limitedHinge.motor.type == 3))
				{
					s.AppendLine($"          Min Force:  {constraintData.limitedHinge.motor.springDamperMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.limitedHinge.motor.springDamperMotor.maxForce}");
					s.AppendLine($"          Spring Constant:  {constraintData.limitedHinge.motor.springDamperMotor.springConstant}");
					s.AppendLine($"          Spring Damping:  {constraintData.limitedHinge.motor.springDamperMotor.springDamping}");
					s.AppendLine($"          Motor Enabled:  {constraintData.limitedHinge.motor.springDamperMotor.motorEnabled}");
				}
			}
			if ((constraintData.type == 6))
			{
				s.AppendLine($"        Pivot A:  {constraintData.prismatic.pivotA}");
				s.AppendLine($"        Rotation A:  {constraintData.prismatic.rotationA}");
				s.AppendLine($"        Plane A:  {constraintData.prismatic.planeA}");
				s.AppendLine($"        Sliding A:  {constraintData.prismatic.slidingA}");
				s.AppendLine($"        Sliding B:  {constraintData.prismatic.slidingB}");
				s.AppendLine($"        Pivot B:  {constraintData.prismatic.pivotB}");
				s.AppendLine($"        Rotation B:  {constraintData.prismatic.rotationB}");
				s.AppendLine($"        Plane B:  {constraintData.prismatic.planeB}");
				s.AppendLine($"        Min Distance:  {constraintData.prismatic.minDistance}");
				s.AppendLine($"        Max Distance:  {constraintData.prismatic.maxDistance}");
				s.AppendLine($"        Friction:  {constraintData.prismatic.friction}");
				s.AppendLine($"        Type:  {constraintData.prismatic.motor.type}");
				if ((constraintData.prismatic.motor.type == 1))
				{
					s.AppendLine($"          Min Force:  {constraintData.prismatic.motor.positionMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.prismatic.motor.positionMotor.maxForce}");
					s.AppendLine($"          Tau:  {constraintData.prismatic.motor.positionMotor.tau}");
					s.AppendLine($"          Damping:  {constraintData.prismatic.motor.positionMotor.damping}");
					s.AppendLine($"          Proportional Recovery Velocity:  {constraintData.prismatic.motor.positionMotor.proportionalRecoveryVelocity}");
					s.AppendLine($"          Constant Recovery Velocity:  {constraintData.prismatic.motor.positionMotor.constantRecoveryVelocity}");
					s.AppendLine($"          Motor Enabled:  {constraintData.prismatic.motor.positionMotor.motorEnabled}");
				}
				if ((constraintData.prismatic.motor.type == 2))
				{
					s.AppendLine($"          Min Force:  {constraintData.prismatic.motor.velocityMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.prismatic.motor.velocityMotor.maxForce}");
					s.AppendLine($"          Tau:  {constraintData.prismatic.motor.velocityMotor.tau}");
					s.AppendLine($"          Target Velocity:  {constraintData.prismatic.motor.velocityMotor.targetVelocity}");
					s.AppendLine($"          Use Velocity Target:  {constraintData.prismatic.motor.velocityMotor.useVelocityTarget}");
					s.AppendLine($"          Motor Enabled:  {constraintData.prismatic.motor.velocityMotor.motorEnabled}");
				}
				if ((constraintData.prismatic.motor.type == 3))
				{
					s.AppendLine($"          Min Force:  {constraintData.prismatic.motor.springDamperMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.prismatic.motor.springDamperMotor.maxForce}");
					s.AppendLine($"          Spring Constant:  {constraintData.prismatic.motor.springDamperMotor.springConstant}");
					s.AppendLine($"          Spring Damping:  {constraintData.prismatic.motor.springDamperMotor.springDamping}");
					s.AppendLine($"          Motor Enabled:  {constraintData.prismatic.motor.springDamperMotor.motorEnabled}");
				}
			}
			if ((constraintData.type == 7))
			{
				s.AppendLine($"        Pivot A:  {constraintData.ragdoll.pivotA}");
				s.AppendLine($"        Plane A:  {constraintData.ragdoll.planeA}");
				s.AppendLine($"        Twist A:  {constraintData.ragdoll.twistA}");
				s.AppendLine($"        Pivot B:  {constraintData.ragdoll.pivotB}");
				s.AppendLine($"        Plane B:  {constraintData.ragdoll.planeB}");
				s.AppendLine($"        Twist B:  {constraintData.ragdoll.twistB}");
				s.AppendLine($"        Motor A:  {constraintData.ragdoll.motorA}");
				s.AppendLine($"        Motor B:  {constraintData.ragdoll.motorB}");
				s.AppendLine($"        Cone Max Angle:  {constraintData.ragdoll.coneMaxAngle}");
				s.AppendLine($"        Plane Min Angle:  {constraintData.ragdoll.planeMinAngle}");
				s.AppendLine($"        Plane Max Angle:  {constraintData.ragdoll.planeMaxAngle}");
				s.AppendLine($"        Twist Min Angle:  {constraintData.ragdoll.twistMinAngle}");
				s.AppendLine($"        Twist Max Angle:  {constraintData.ragdoll.twistMaxAngle}");
				s.AppendLine($"        Max Friction:  {constraintData.ragdoll.maxFriction}");
				s.AppendLine($"        Type:  {constraintData.ragdoll.motor.type}");
				if ((constraintData.ragdoll.motor.type == 1))
				{
					s.AppendLine($"          Min Force:  {constraintData.ragdoll.motor.positionMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.ragdoll.motor.positionMotor.maxForce}");
					s.AppendLine($"          Tau:  {constraintData.ragdoll.motor.positionMotor.tau}");
					s.AppendLine($"          Damping:  {constraintData.ragdoll.motor.positionMotor.damping}");
					s.AppendLine($"          Proportional Recovery Velocity:  {constraintData.ragdoll.motor.positionMotor.proportionalRecoveryVelocity}");
					s.AppendLine($"          Constant Recovery Velocity:  {constraintData.ragdoll.motor.positionMotor.constantRecoveryVelocity}");
					s.AppendLine($"          Motor Enabled:  {constraintData.ragdoll.motor.positionMotor.motorEnabled}");
				}
				if ((constraintData.ragdoll.motor.type == 2))
				{
					s.AppendLine($"          Min Force:  {constraintData.ragdoll.motor.velocityMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.ragdoll.motor.velocityMotor.maxForce}");
					s.AppendLine($"          Tau:  {constraintData.ragdoll.motor.velocityMotor.tau}");
					s.AppendLine($"          Target Velocity:  {constraintData.ragdoll.motor.velocityMotor.targetVelocity}");
					s.AppendLine($"          Use Velocity Target:  {constraintData.ragdoll.motor.velocityMotor.useVelocityTarget}");
					s.AppendLine($"          Motor Enabled:  {constraintData.ragdoll.motor.velocityMotor.motorEnabled}");
				}
				if ((constraintData.ragdoll.motor.type == 3))
				{
					s.AppendLine($"          Min Force:  {constraintData.ragdoll.motor.springDamperMotor.minForce}");
					s.AppendLine($"          Max Force:  {constraintData.ragdoll.motor.springDamperMotor.maxForce}");
					s.AppendLine($"          Spring Constant:  {constraintData.ragdoll.motor.springDamperMotor.springConstant}");
					s.AppendLine($"          Spring Damping:  {constraintData.ragdoll.motor.springDamperMotor.springDamping}");
					s.AppendLine($"          Motor Enabled:  {constraintData.ragdoll.motor.springDamperMotor.motorEnabled}");
				}
			}
			if ((constraintData.type == 8))
			{
				s.AppendLine($"        Pivot A:  {constraintData.stiffSpring.pivotA}");
				s.AppendLine($"        Pivot B:  {constraintData.stiffSpring.pivotB}");
				s.AppendLine($"        Length:  {constraintData.stiffSpring.length}");
			}
			if ((constraintData.type == 13))
			{
				s.AppendLine($"        Type:  {constraintData.malleable.type}");
				s.AppendLine($"        Num Entities:  {constraintData.malleable.numEntities}");
				s.AppendLine($"        Entity A:  {constraintData.malleable.entityA}");
				s.AppendLine($"        Entity B:  {constraintData.malleable.entityB}");
				s.AppendLine($"        Priority:  {constraintData.malleable.priority}");
				if ((constraintData.malleable.type == 0))
				{
					s.AppendLine($"          Pivot A:  {constraintData.malleable.ballAndSocket.pivotA}");
					s.AppendLine($"          Pivot B:  {constraintData.malleable.ballAndSocket.pivotB}");
				}
				if ((constraintData.malleable.type == 1))
				{
					s.AppendLine($"          Pivot A:  {constraintData.malleable.hinge.pivotA}");
					s.AppendLine($"          Perp Axis In A1:  {constraintData.malleable.hinge.perpAxisInA1}");
					s.AppendLine($"          Perp Axis In A2:  {constraintData.malleable.hinge.perpAxisInA2}");
					s.AppendLine($"          Pivot B:  {constraintData.malleable.hinge.pivotB}");
					s.AppendLine($"          Axis B:  {constraintData.malleable.hinge.axisB}");
					s.AppendLine($"          Axis A:  {constraintData.malleable.hinge.axisA}");
					s.AppendLine($"          Perp Axis In B1:  {constraintData.malleable.hinge.perpAxisInB1}");
					s.AppendLine($"          Perp Axis In B2:  {constraintData.malleable.hinge.perpAxisInB2}");
				}
				if ((constraintData.malleable.type == 2))
				{
					s.AppendLine($"          Pivot A:  {constraintData.malleable.limitedHinge.pivotA}");
					s.AppendLine($"          Axis A:  {constraintData.malleable.limitedHinge.axisA}");
					s.AppendLine($"          Perp Axis In A1:  {constraintData.malleable.limitedHinge.perpAxisInA1}");
					s.AppendLine($"          Perp Axis In A2:  {constraintData.malleable.limitedHinge.perpAxisInA2}");
					s.AppendLine($"          Pivot B:  {constraintData.malleable.limitedHinge.pivotB}");
					s.AppendLine($"          Axis B:  {constraintData.malleable.limitedHinge.axisB}");
					s.AppendLine($"          Perp Axis In B2:  {constraintData.malleable.limitedHinge.perpAxisInB2}");
					s.AppendLine($"          Perp Axis In B1:  {constraintData.malleable.limitedHinge.perpAxisInB1}");
					s.AppendLine($"          Min Angle:  {constraintData.malleable.limitedHinge.minAngle}");
					s.AppendLine($"          Max Angle:  {constraintData.malleable.limitedHinge.maxAngle}");
					s.AppendLine($"          Max Friction:  {constraintData.malleable.limitedHinge.maxFriction}");
					s.AppendLine($"          Type:  {constraintData.malleable.limitedHinge.motor.type}");
					if ((constraintData.malleable.limitedHinge.motor.type == 1))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.limitedHinge.motor.positionMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.limitedHinge.motor.positionMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraintData.malleable.limitedHinge.motor.positionMotor.tau}");
						s.AppendLine($"            Damping:  {constraintData.malleable.limitedHinge.motor.positionMotor.damping}");
						s.AppendLine($"            Proportional Recovery Velocity:  {constraintData.malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity}");
						s.AppendLine($"            Constant Recovery Velocity:  {constraintData.malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.limitedHinge.motor.positionMotor.motorEnabled}");
					}
					if ((constraintData.malleable.limitedHinge.motor.type == 2))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.limitedHinge.motor.velocityMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.limitedHinge.motor.velocityMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraintData.malleable.limitedHinge.motor.velocityMotor.tau}");
						s.AppendLine($"            Target Velocity:  {constraintData.malleable.limitedHinge.motor.velocityMotor.targetVelocity}");
						s.AppendLine($"            Use Velocity Target:  {constraintData.malleable.limitedHinge.motor.velocityMotor.useVelocityTarget}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.limitedHinge.motor.velocityMotor.motorEnabled}");
					}
					if ((constraintData.malleable.limitedHinge.motor.type == 3))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.limitedHinge.motor.springDamperMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.limitedHinge.motor.springDamperMotor.maxForce}");
						s.AppendLine($"            Spring Constant:  {constraintData.malleable.limitedHinge.motor.springDamperMotor.springConstant}");
						s.AppendLine($"            Spring Damping:  {constraintData.malleable.limitedHinge.motor.springDamperMotor.springDamping}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.limitedHinge.motor.springDamperMotor.motorEnabled}");
					}
				}
				if ((constraintData.malleable.type == 6))
				{
					s.AppendLine($"          Pivot A:  {constraintData.malleable.prismatic.pivotA}");
					s.AppendLine($"          Rotation A:  {constraintData.malleable.prismatic.rotationA}");
					s.AppendLine($"          Plane A:  {constraintData.malleable.prismatic.planeA}");
					s.AppendLine($"          Sliding A:  {constraintData.malleable.prismatic.slidingA}");
					s.AppendLine($"          Sliding B:  {constraintData.malleable.prismatic.slidingB}");
					s.AppendLine($"          Pivot B:  {constraintData.malleable.prismatic.pivotB}");
					s.AppendLine($"          Rotation B:  {constraintData.malleable.prismatic.rotationB}");
					s.AppendLine($"          Plane B:  {constraintData.malleable.prismatic.planeB}");
					s.AppendLine($"          Min Distance:  {constraintData.malleable.prismatic.minDistance}");
					s.AppendLine($"          Max Distance:  {constraintData.malleable.prismatic.maxDistance}");
					s.AppendLine($"          Friction:  {constraintData.malleable.prismatic.friction}");
					s.AppendLine($"          Type:  {constraintData.malleable.prismatic.motor.type}");
					if ((constraintData.malleable.prismatic.motor.type == 1))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.prismatic.motor.positionMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.prismatic.motor.positionMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraintData.malleable.prismatic.motor.positionMotor.tau}");
						s.AppendLine($"            Damping:  {constraintData.malleable.prismatic.motor.positionMotor.damping}");
						s.AppendLine($"            Proportional Recovery Velocity:  {constraintData.malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity}");
						s.AppendLine($"            Constant Recovery Velocity:  {constraintData.malleable.prismatic.motor.positionMotor.constantRecoveryVelocity}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.prismatic.motor.positionMotor.motorEnabled}");
					}
					if ((constraintData.malleable.prismatic.motor.type == 2))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.prismatic.motor.velocityMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.prismatic.motor.velocityMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraintData.malleable.prismatic.motor.velocityMotor.tau}");
						s.AppendLine($"            Target Velocity:  {constraintData.malleable.prismatic.motor.velocityMotor.targetVelocity}");
						s.AppendLine($"            Use Velocity Target:  {constraintData.malleable.prismatic.motor.velocityMotor.useVelocityTarget}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.prismatic.motor.velocityMotor.motorEnabled}");
					}
					if ((constraintData.malleable.prismatic.motor.type == 3))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.prismatic.motor.springDamperMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.prismatic.motor.springDamperMotor.maxForce}");
						s.AppendLine($"            Spring Constant:  {constraintData.malleable.prismatic.motor.springDamperMotor.springConstant}");
						s.AppendLine($"            Spring Damping:  {constraintData.malleable.prismatic.motor.springDamperMotor.springDamping}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.prismatic.motor.springDamperMotor.motorEnabled}");
					}
				}
				if ((constraintData.malleable.type == 7))
				{
					s.AppendLine($"          Pivot A:  {constraintData.malleable.ragdoll.pivotA}");
					s.AppendLine($"          Plane A:  {constraintData.malleable.ragdoll.planeA}");
					s.AppendLine($"          Twist A:  {constraintData.malleable.ragdoll.twistA}");
					s.AppendLine($"          Pivot B:  {constraintData.malleable.ragdoll.pivotB}");
					s.AppendLine($"          Plane B:  {constraintData.malleable.ragdoll.planeB}");
					s.AppendLine($"          Twist B:  {constraintData.malleable.ragdoll.twistB}");
					s.AppendLine($"          Motor A:  {constraintData.malleable.ragdoll.motorA}");
					s.AppendLine($"          Motor B:  {constraintData.malleable.ragdoll.motorB}");
					s.AppendLine($"          Cone Max Angle:  {constraintData.malleable.ragdoll.coneMaxAngle}");
					s.AppendLine($"          Plane Min Angle:  {constraintData.malleable.ragdoll.planeMinAngle}");
					s.AppendLine($"          Plane Max Angle:  {constraintData.malleable.ragdoll.planeMaxAngle}");
					s.AppendLine($"          Twist Min Angle:  {constraintData.malleable.ragdoll.twistMinAngle}");
					s.AppendLine($"          Twist Max Angle:  {constraintData.malleable.ragdoll.twistMaxAngle}");
					s.AppendLine($"          Max Friction:  {constraintData.malleable.ragdoll.maxFriction}");
					s.AppendLine($"          Type:  {constraintData.malleable.ragdoll.motor.type}");
					if ((constraintData.malleable.ragdoll.motor.type == 1))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.ragdoll.motor.positionMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.ragdoll.motor.positionMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraintData.malleable.ragdoll.motor.positionMotor.tau}");
						s.AppendLine($"            Damping:  {constraintData.malleable.ragdoll.motor.positionMotor.damping}");
						s.AppendLine($"            Proportional Recovery Velocity:  {constraintData.malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity}");
						s.AppendLine($"            Constant Recovery Velocity:  {constraintData.malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.ragdoll.motor.positionMotor.motorEnabled}");
					}
					if ((constraintData.malleable.ragdoll.motor.type == 2))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.ragdoll.motor.velocityMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.ragdoll.motor.velocityMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraintData.malleable.ragdoll.motor.velocityMotor.tau}");
						s.AppendLine($"            Target Velocity:  {constraintData.malleable.ragdoll.motor.velocityMotor.targetVelocity}");
						s.AppendLine($"            Use Velocity Target:  {constraintData.malleable.ragdoll.motor.velocityMotor.useVelocityTarget}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.ragdoll.motor.velocityMotor.motorEnabled}");
					}
					if ((constraintData.malleable.ragdoll.motor.type == 3))
					{
						s.AppendLine($"            Min Force:  {constraintData.malleable.ragdoll.motor.springDamperMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraintData.malleable.ragdoll.motor.springDamperMotor.maxForce}");
						s.AppendLine($"            Spring Constant:  {constraintData.malleable.ragdoll.motor.springDamperMotor.springConstant}");
						s.AppendLine($"            Spring Damping:  {constraintData.malleable.ragdoll.motor.springDamperMotor.springDamping}");
						s.AppendLine($"            Motor Enabled:  {constraintData.malleable.ragdoll.motor.springDamperMotor.motorEnabled}");
					}
				}
				if ((constraintData.malleable.type == 8))
				{
					s.AppendLine($"          Pivot A:  {constraintData.malleable.stiffSpring.pivotA}");
					s.AppendLine($"          Pivot B:  {constraintData.malleable.stiffSpring.pivotB}");
					s.AppendLine($"          Length:  {constraintData.malleable.stiffSpring.length}");
				}
				s.AppendLine($"        Tau:  {constraintData.malleable.tau}");
				s.AppendLine($"        Damping:  {constraintData.malleable.damping}");
				s.AppendLine($"        Strength:  {constraintData.malleable.strength}");
			}
			s.AppendLine($"      Threshold:  {threshold}");
			s.AppendLine($"      Remove When Broken:  {removeWhenBroken}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			constraintData.entityA = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
			constraintData.entityB = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
			if ((constraintData.type == 13))
			{
				constraintData.malleable.entityA = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
				constraintData.malleable.entityB = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			if (constraintData.entityA != null)
				ptrs.Add((NiObject)constraintData.entityA);
			if (constraintData.entityB != null)
				ptrs.Add((NiObject)constraintData.entityB);
			if (constraintData.malleable.entityA != null)
				ptrs.Add((NiObject)constraintData.malleable.entityA);
			if (constraintData.malleable.entityB != null)
				ptrs.Add((NiObject)constraintData.malleable.entityB);
			return ptrs;
		}
	}
}
