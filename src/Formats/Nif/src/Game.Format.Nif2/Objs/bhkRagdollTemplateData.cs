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
	/*! Data for bhkRagdollTemplate */
	public class bhkRagdollTemplateData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkRagdollTemplateData", NiObject.TYPE);

		/*! name */
		internal IndexString name;
		/*! mass */
		internal float mass;
		/*! restitution */
		internal float restitution;
		/*! friction */
		internal float friction;
		/*! radius */
		internal float radius;
		/*! material */
		internal HavokMaterial material;
		/*! numConstraints */
		internal uint numConstraints;
		/*! constraint */
		internal IList<ConstraintData> constraint;
		public bhkRagdollTemplateData()
		{
			mass = 9.0f;
			restitution = 0.8f;
			friction = 0.3f;
			radius = 1.0f;
			numConstraints = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkRagdollTemplateData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out name, s, info);
			Nif.NifStream(out mass, s, info);
			Nif.NifStream(out restitution, s, info);
			Nif.NifStream(out friction, s, info);
			Nif.NifStream(out radius, s, info);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(out material.unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(out material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(out material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(out material.material_sk, s, info);
			}
			Nif.NifStream(out numConstraints, s, info);
			constraint = new ConstraintData[numConstraints];
			for (var i3 = 0; i3 < constraint.Count; i3++)
			{
				Nif.NifStream(out constraint[i3].type, s, info);
				Nif.NifStream(out constraint[i3].numEntities2, s, info);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out constraint[i3].priority, s, info);
				if ((constraint[i3].type == 0))
				{
					Nif.NifStream(out constraint[i3].ballAndSocket.pivotA, s, info);
					Nif.NifStream(out constraint[i3].ballAndSocket.pivotB, s, info);
				}
				if ((constraint[i3].type == 1))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(out constraint[i3].hinge.pivotA, s, info);
						Nif.NifStream(out constraint[i3].hinge.perpAxisInA1, s, info);
						Nif.NifStream(out constraint[i3].hinge.perpAxisInA2, s, info);
						Nif.NifStream(out constraint[i3].hinge.pivotB, s, info);
						Nif.NifStream(out constraint[i3].hinge.axisB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(out constraint[i3].hinge.axisA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].hinge.perpAxisInA1, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].hinge.perpAxisInA2, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].hinge.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].hinge.axisB, s, info);
						Nif.NifStream(out constraint[i3].hinge.perpAxisInB1, s, info);
						Nif.NifStream(out constraint[i3].hinge.perpAxisInB2, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].hinge.pivotB, s, info);
					}
				}
				if ((constraint[i3].type == 2))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(out constraint[i3].limitedHinge.pivotA, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.axisA, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.pivotB, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.axisB, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.perpAxisInB2, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.axisA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.axisB, s, info);
						Nif.NifStream(out constraint[i3].limitedHinge.perpAxisInB1, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.perpAxisInB2, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].limitedHinge.pivotB, s, info);
					}
					Nif.NifStream(out constraint[i3].limitedHinge.minAngle, s, info);
					Nif.NifStream(out constraint[i3].limitedHinge.maxAngle, s, info);
					Nif.NifStream(out constraint[i3].limitedHinge.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(out constraint[i3].limitedHinge.motor.type, s, info);
						if ((constraint[i3].limitedHinge.motor.type == 1))
						{
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.tau, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.damping, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].limitedHinge.motor.type == 2))
						{
							Nif.NifStream(out constraint[i3].limitedHinge.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.velocityMotor.tau, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].limitedHinge.motor.type == 3))
						{
							Nif.NifStream(out constraint[i3].limitedHinge.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(out constraint[i3].limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraint[i3].type == 6))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(out constraint[i3].prismatic.pivotA, s, info);
						Nif.NifStream(out constraint[i3].prismatic.rotationA, s, info);
						Nif.NifStream(out constraint[i3].prismatic.planeA, s, info);
						Nif.NifStream(out constraint[i3].prismatic.slidingA, s, info);
						Nif.NifStream(out constraint[i3].prismatic.slidingB, s, info);
						Nif.NifStream(out constraint[i3].prismatic.pivotB, s, info);
						Nif.NifStream(out constraint[i3].prismatic.rotationB, s, info);
						Nif.NifStream(out constraint[i3].prismatic.planeB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.slidingA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.rotationA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.planeA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.slidingB, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.rotationB, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.planeB, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].prismatic.pivotB, s, info);
					}
					Nif.NifStream(out constraint[i3].prismatic.minDistance, s, info);
					Nif.NifStream(out constraint[i3].prismatic.maxDistance, s, info);
					Nif.NifStream(out constraint[i3].prismatic.friction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(out constraint[i3].prismatic.motor.type, s, info);
						if ((constraint[i3].prismatic.motor.type == 1))
						{
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.tau, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.damping, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].prismatic.motor.type == 2))
						{
							Nif.NifStream(out constraint[i3].prismatic.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.velocityMotor.tau, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].prismatic.motor.type == 3))
						{
							Nif.NifStream(out constraint[i3].prismatic.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(out constraint[i3].prismatic.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraint[i3].type == 7))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(out constraint[i3].ragdoll.pivotA, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.planeA, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.twistA, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.pivotB, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.planeB, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.twistB, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream(out (Vector4)constraint[i3].ragdoll.twistA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].ragdoll.planeA, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.motorA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].ragdoll.pivotA, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].ragdoll.twistB, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].ragdoll.planeB, s, info);
						Nif.NifStream(out constraint[i3].ragdoll.motorB, s, info);
						Nif.NifStream(out (Vector4)constraint[i3].ragdoll.pivotB, s, info);
					}
					Nif.NifStream(out constraint[i3].ragdoll.coneMaxAngle, s, info);
					Nif.NifStream(out constraint[i3].ragdoll.planeMinAngle, s, info);
					Nif.NifStream(out constraint[i3].ragdoll.planeMaxAngle, s, info);
					Nif.NifStream(out constraint[i3].ragdoll.twistMinAngle, s, info);
					Nif.NifStream(out constraint[i3].ragdoll.twistMaxAngle, s, info);
					Nif.NifStream(out constraint[i3].ragdoll.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(out constraint[i3].ragdoll.motor.type, s, info);
						if ((constraint[i3].ragdoll.motor.type == 1))
						{
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.tau, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.damping, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].ragdoll.motor.type == 2))
						{
							Nif.NifStream(out constraint[i3].ragdoll.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.velocityMotor.tau, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].ragdoll.motor.type == 3))
						{
							Nif.NifStream(out constraint[i3].ragdoll.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(out constraint[i3].ragdoll.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraint[i3].type == 8))
				{
					Nif.NifStream(out constraint[i3].stiffSpring.pivotA, s, info);
					Nif.NifStream(out constraint[i3].stiffSpring.pivotB, s, info);
					Nif.NifStream(out constraint[i3].stiffSpring.length, s, info);
				}
				if ((constraint[i3].type == 13))
				{
					Nif.NifStream(out constraint[i3].malleable.type, s, info);
					Nif.NifStream(out constraint[i3].malleable.numEntities, s, info);
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
					Nif.NifStream(out constraint[i3].malleable.priority, s, info);
					if ((constraint[i3].malleable.type == 0))
					{
						Nif.NifStream(out constraint[i3].malleable.ballAndSocket.pivotA, s, info);
						Nif.NifStream(out constraint[i3].malleable.ballAndSocket.pivotB, s, info);
					}
					if ((constraint[i3].malleable.type == 1))
					{
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(out constraint[i3].malleable.hinge.pivotA, s, info);
							Nif.NifStream(out constraint[i3].malleable.hinge.perpAxisInA1, s, info);
							Nif.NifStream(out constraint[i3].malleable.hinge.perpAxisInA2, s, info);
							Nif.NifStream(out constraint[i3].malleable.hinge.pivotB, s, info);
							Nif.NifStream(out constraint[i3].malleable.hinge.axisB, s, info);
						}
						if (info.version >= 0x14020007)
						{
							Nif.NifStream(out constraint[i3].malleable.hinge.axisA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.hinge.perpAxisInA1, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.hinge.perpAxisInA2, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.hinge.pivotA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.hinge.axisB, s, info);
							Nif.NifStream(out constraint[i3].malleable.hinge.perpAxisInB1, s, info);
							Nif.NifStream(out constraint[i3].malleable.hinge.perpAxisInB2, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.hinge.pivotB, s, info);
						}
					}
					if ((constraint[i3].malleable.type == 2))
					{
						if ((info.userVersion2 <= 16))
						{
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.pivotA, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.axisA, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.perpAxisInA1, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.perpAxisInA2, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.pivotB, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.axisB, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.perpAxisInB2, s, info);
						}
						if ((info.userVersion2 > 16))
						{
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.axisA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.perpAxisInA1, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.perpAxisInA2, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.pivotA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.axisB, s, info);
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.perpAxisInB1, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.perpAxisInB2, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.limitedHinge.pivotB, s, info);
						}
						Nif.NifStream(out constraint[i3].malleable.limitedHinge.minAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.limitedHinge.maxAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.limitedHinge.maxFriction, s, info);
						if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
						{
							Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.type, s, info);
							if ((constraint[i3].malleable.limitedHinge.motor.type == 1))
							{
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.tau, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.damping, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.positionMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.limitedHinge.motor.type == 2))
							{
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.velocityMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.velocityMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.velocityMotor.tau, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.limitedHinge.motor.type == 3))
							{
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.springDamperMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.springDamperMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.springDamperMotor.springConstant, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.springDamperMotor.springDamping, s, info);
								Nif.NifStream(out constraint[i3].malleable.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
							}
						}
					}
					if ((constraint[i3].malleable.type == 6))
					{
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(out constraint[i3].malleable.prismatic.pivotA, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.rotationA, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.planeA, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.slidingA, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.slidingB, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.pivotB, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.rotationB, s, info);
							Nif.NifStream(out constraint[i3].malleable.prismatic.planeB, s, info);
						}
						if (info.version >= 0x14020007)
						{
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.slidingA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.rotationA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.planeA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.pivotA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.slidingB, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.rotationB, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.planeB, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.prismatic.pivotB, s, info);
						}
						Nif.NifStream(out constraint[i3].malleable.prismatic.minDistance, s, info);
						Nif.NifStream(out constraint[i3].malleable.prismatic.maxDistance, s, info);
						Nif.NifStream(out constraint[i3].malleable.prismatic.friction, s, info);
						if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
						{
							Nif.NifStream(out constraint[i3].malleable.prismatic.motor.type, s, info);
							if ((constraint[i3].malleable.prismatic.motor.type == 1))
							{
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.tau, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.damping, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.positionMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.prismatic.motor.type == 2))
							{
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.velocityMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.velocityMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.velocityMotor.tau, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.velocityMotor.targetVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.velocityMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.prismatic.motor.type == 3))
							{
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.springDamperMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.springDamperMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.springDamperMotor.springConstant, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.springDamperMotor.springDamping, s, info);
								Nif.NifStream(out constraint[i3].malleable.prismatic.motor.springDamperMotor.motorEnabled, s, info);
							}
						}
					}
					if ((constraint[i3].malleable.type == 7))
					{
						if ((info.userVersion2 <= 16))
						{
							Nif.NifStream(out constraint[i3].malleable.ragdoll.pivotA, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.planeA, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.twistA, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.pivotB, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.planeB, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.twistB, s, info);
						}
						if ((info.userVersion2 > 16))
						{
							Nif.NifStream(out (Vector4)constraint[i3].malleable.ragdoll.twistA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.ragdoll.planeA, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.motorA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.ragdoll.pivotA, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.ragdoll.twistB, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.ragdoll.planeB, s, info);
							Nif.NifStream(out constraint[i3].malleable.ragdoll.motorB, s, info);
							Nif.NifStream(out (Vector4)constraint[i3].malleable.ragdoll.pivotB, s, info);
						}
						Nif.NifStream(out constraint[i3].malleable.ragdoll.coneMaxAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.ragdoll.planeMinAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.ragdoll.planeMaxAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.ragdoll.twistMinAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.ragdoll.twistMaxAngle, s, info);
						Nif.NifStream(out constraint[i3].malleable.ragdoll.maxFriction, s, info);
						if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
						{
							Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.type, s, info);
							if ((constraint[i3].malleable.ragdoll.motor.type == 1))
							{
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.tau, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.damping, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.positionMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.ragdoll.motor.type == 2))
							{
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.velocityMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.velocityMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.velocityMotor.tau, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.velocityMotor.targetVelocity, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.velocityMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.ragdoll.motor.type == 3))
							{
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.springDamperMotor.minForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.springDamperMotor.maxForce, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.springDamperMotor.springConstant, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.springDamperMotor.springDamping, s, info);
								Nif.NifStream(out constraint[i3].malleable.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
							}
						}
					}
					if ((constraint[i3].malleable.type == 8))
					{
						Nif.NifStream(out constraint[i3].malleable.stiffSpring.pivotA, s, info);
						Nif.NifStream(out constraint[i3].malleable.stiffSpring.pivotB, s, info);
						Nif.NifStream(out constraint[i3].malleable.stiffSpring.length, s, info);
					}
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(out constraint[i3].malleable.tau, s, info);
						Nif.NifStream(out constraint[i3].malleable.damping, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(out constraint[i3].malleable.strength, s, info);
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numConstraints = (uint)constraint.Count;
			Nif.NifStream(name, s, info);
			Nif.NifStream(mass, s, info);
			Nif.NifStream(restitution, s, info);
			Nif.NifStream(friction, s, info);
			Nif.NifStream(radius, s, info);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(material.unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(material.material_sk, s, info);
			}
			Nif.NifStream(numConstraints, s, info);
			for (var i3 = 0; i3 < constraint.Count; i3++)
			{
				Nif.NifStream(constraint[i3].type, s, info);
				Nif.NifStream(constraint[i3].numEntities2, s, info);
				WriteRef((NiObject)constraint[i3].entityA, s, info, link_map, missing_link_stack);
				WriteRef((NiObject)constraint[i3].entityB, s, info, link_map, missing_link_stack);
				Nif.NifStream(constraint[i3].priority, s, info);
				if ((constraint[i3].type == 0))
				{
					Nif.NifStream(constraint[i3].ballAndSocket.pivotA, s, info);
					Nif.NifStream(constraint[i3].ballAndSocket.pivotB, s, info);
				}
				if ((constraint[i3].type == 1))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(constraint[i3].hinge.pivotA, s, info);
						Nif.NifStream(constraint[i3].hinge.perpAxisInA1, s, info);
						Nif.NifStream(constraint[i3].hinge.perpAxisInA2, s, info);
						Nif.NifStream(constraint[i3].hinge.pivotB, s, info);
						Nif.NifStream(constraint[i3].hinge.axisB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(constraint[i3].hinge.axisA, s, info);
						Nif.NifStream((Vector4)constraint[i3].hinge.perpAxisInA1, s, info);
						Nif.NifStream((Vector4)constraint[i3].hinge.perpAxisInA2, s, info);
						Nif.NifStream((Vector4)constraint[i3].hinge.pivotA, s, info);
						Nif.NifStream((Vector4)constraint[i3].hinge.axisB, s, info);
						Nif.NifStream(constraint[i3].hinge.perpAxisInB1, s, info);
						Nif.NifStream(constraint[i3].hinge.perpAxisInB2, s, info);
						Nif.NifStream((Vector4)constraint[i3].hinge.pivotB, s, info);
					}
				}
				if ((constraint[i3].type == 2))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(constraint[i3].limitedHinge.pivotA, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.axisA, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.pivotB, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.axisB, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.perpAxisInB2, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.axisA, s, info);
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.perpAxisInA1, s, info);
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.perpAxisInA2, s, info);
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.pivotA, s, info);
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.axisB, s, info);
						Nif.NifStream(constraint[i3].limitedHinge.perpAxisInB1, s, info);
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.perpAxisInB2, s, info);
						Nif.NifStream((Vector4)constraint[i3].limitedHinge.pivotB, s, info);
					}
					Nif.NifStream(constraint[i3].limitedHinge.minAngle, s, info);
					Nif.NifStream(constraint[i3].limitedHinge.maxAngle, s, info);
					Nif.NifStream(constraint[i3].limitedHinge.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(constraint[i3].limitedHinge.motor.type, s, info);
						if ((constraint[i3].limitedHinge.motor.type == 1))
						{
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.tau, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.damping, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].limitedHinge.motor.type == 2))
						{
							Nif.NifStream(constraint[i3].limitedHinge.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.velocityMotor.tau, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].limitedHinge.motor.type == 3))
						{
							Nif.NifStream(constraint[i3].limitedHinge.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(constraint[i3].limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraint[i3].type == 6))
				{
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(constraint[i3].prismatic.pivotA, s, info);
						Nif.NifStream(constraint[i3].prismatic.rotationA, s, info);
						Nif.NifStream(constraint[i3].prismatic.planeA, s, info);
						Nif.NifStream(constraint[i3].prismatic.slidingA, s, info);
						Nif.NifStream(constraint[i3].prismatic.slidingB, s, info);
						Nif.NifStream(constraint[i3].prismatic.pivotB, s, info);
						Nif.NifStream(constraint[i3].prismatic.rotationB, s, info);
						Nif.NifStream(constraint[i3].prismatic.planeB, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream((Vector4)constraint[i3].prismatic.slidingA, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.rotationA, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.planeA, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.pivotA, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.slidingB, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.rotationB, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.planeB, s, info);
						Nif.NifStream((Vector4)constraint[i3].prismatic.pivotB, s, info);
					}
					Nif.NifStream(constraint[i3].prismatic.minDistance, s, info);
					Nif.NifStream(constraint[i3].prismatic.maxDistance, s, info);
					Nif.NifStream(constraint[i3].prismatic.friction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(constraint[i3].prismatic.motor.type, s, info);
						if ((constraint[i3].prismatic.motor.type == 1))
						{
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.tau, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.damping, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].prismatic.motor.type == 2))
						{
							Nif.NifStream(constraint[i3].prismatic.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.velocityMotor.tau, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].prismatic.motor.type == 3))
						{
							Nif.NifStream(constraint[i3].prismatic.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(constraint[i3].prismatic.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraint[i3].type == 7))
				{
					if ((info.userVersion2 <= 16))
					{
						Nif.NifStream(constraint[i3].ragdoll.pivotA, s, info);
						Nif.NifStream(constraint[i3].ragdoll.planeA, s, info);
						Nif.NifStream(constraint[i3].ragdoll.twistA, s, info);
						Nif.NifStream(constraint[i3].ragdoll.pivotB, s, info);
						Nif.NifStream(constraint[i3].ragdoll.planeB, s, info);
						Nif.NifStream(constraint[i3].ragdoll.twistB, s, info);
					}
					if ((info.userVersion2 > 16))
					{
						Nif.NifStream((Vector4)constraint[i3].ragdoll.twistA, s, info);
						Nif.NifStream((Vector4)constraint[i3].ragdoll.planeA, s, info);
						Nif.NifStream(constraint[i3].ragdoll.motorA, s, info);
						Nif.NifStream((Vector4)constraint[i3].ragdoll.pivotA, s, info);
						Nif.NifStream((Vector4)constraint[i3].ragdoll.twistB, s, info);
						Nif.NifStream((Vector4)constraint[i3].ragdoll.planeB, s, info);
						Nif.NifStream(constraint[i3].ragdoll.motorB, s, info);
						Nif.NifStream((Vector4)constraint[i3].ragdoll.pivotB, s, info);
					}
					Nif.NifStream(constraint[i3].ragdoll.coneMaxAngle, s, info);
					Nif.NifStream(constraint[i3].ragdoll.planeMinAngle, s, info);
					Nif.NifStream(constraint[i3].ragdoll.planeMaxAngle, s, info);
					Nif.NifStream(constraint[i3].ragdoll.twistMinAngle, s, info);
					Nif.NifStream(constraint[i3].ragdoll.twistMaxAngle, s, info);
					Nif.NifStream(constraint[i3].ragdoll.maxFriction, s, info);
					if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
					{
						Nif.NifStream(constraint[i3].ragdoll.motor.type, s, info);
						if ((constraint[i3].ragdoll.motor.type == 1))
						{
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.tau, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.damping, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.positionMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].ragdoll.motor.type == 2))
						{
							Nif.NifStream(constraint[i3].ragdoll.motor.velocityMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.velocityMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.velocityMotor.tau, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.velocityMotor.targetVelocity, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.velocityMotor.motorEnabled, s, info);
						}
						if ((constraint[i3].ragdoll.motor.type == 3))
						{
							Nif.NifStream(constraint[i3].ragdoll.motor.springDamperMotor.minForce, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.springDamperMotor.maxForce, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.springDamperMotor.springConstant, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.springDamperMotor.springDamping, s, info);
							Nif.NifStream(constraint[i3].ragdoll.motor.springDamperMotor.motorEnabled, s, info);
						}
					}
				}
				if ((constraint[i3].type == 8))
				{
					Nif.NifStream(constraint[i3].stiffSpring.pivotA, s, info);
					Nif.NifStream(constraint[i3].stiffSpring.pivotB, s, info);
					Nif.NifStream(constraint[i3].stiffSpring.length, s, info);
				}
				if ((constraint[i3].type == 13))
				{
					Nif.NifStream(constraint[i3].malleable.type, s, info);
					Nif.NifStream(constraint[i3].malleable.numEntities, s, info);
					WriteRef((NiObject)constraint[i3].malleable.entityA, s, info, link_map, missing_link_stack);
					WriteRef((NiObject)constraint[i3].malleable.entityB, s, info, link_map, missing_link_stack);
					Nif.NifStream(constraint[i3].malleable.priority, s, info);
					if ((constraint[i3].malleable.type == 0))
					{
						Nif.NifStream(constraint[i3].malleable.ballAndSocket.pivotA, s, info);
						Nif.NifStream(constraint[i3].malleable.ballAndSocket.pivotB, s, info);
					}
					if ((constraint[i3].malleable.type == 1))
					{
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(constraint[i3].malleable.hinge.pivotA, s, info);
							Nif.NifStream(constraint[i3].malleable.hinge.perpAxisInA1, s, info);
							Nif.NifStream(constraint[i3].malleable.hinge.perpAxisInA2, s, info);
							Nif.NifStream(constraint[i3].malleable.hinge.pivotB, s, info);
							Nif.NifStream(constraint[i3].malleable.hinge.axisB, s, info);
						}
						if (info.version >= 0x14020007)
						{
							Nif.NifStream(constraint[i3].malleable.hinge.axisA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.hinge.perpAxisInA1, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.hinge.perpAxisInA2, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.hinge.pivotA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.hinge.axisB, s, info);
							Nif.NifStream(constraint[i3].malleable.hinge.perpAxisInB1, s, info);
							Nif.NifStream(constraint[i3].malleable.hinge.perpAxisInB2, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.hinge.pivotB, s, info);
						}
					}
					if ((constraint[i3].malleable.type == 2))
					{
						if ((info.userVersion2 <= 16))
						{
							Nif.NifStream(constraint[i3].malleable.limitedHinge.pivotA, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.axisA, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.perpAxisInA1, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.perpAxisInA2, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.pivotB, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.axisB, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.perpAxisInB2, s, info);
						}
						if ((info.userVersion2 > 16))
						{
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.axisA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.perpAxisInA1, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.perpAxisInA2, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.pivotA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.axisB, s, info);
							Nif.NifStream(constraint[i3].malleable.limitedHinge.perpAxisInB1, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.perpAxisInB2, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.limitedHinge.pivotB, s, info);
						}
						Nif.NifStream(constraint[i3].malleable.limitedHinge.minAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.limitedHinge.maxAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.limitedHinge.maxFriction, s, info);
						if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
						{
							Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.type, s, info);
							if ((constraint[i3].malleable.limitedHinge.motor.type == 1))
							{
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.tau, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.damping, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.positionMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.limitedHinge.motor.type == 2))
							{
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.velocityMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.velocityMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.velocityMotor.tau, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.limitedHinge.motor.type == 3))
							{
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.springDamperMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.springDamperMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.springDamperMotor.springConstant, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.springDamperMotor.springDamping, s, info);
								Nif.NifStream(constraint[i3].malleable.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
							}
						}
					}
					if ((constraint[i3].malleable.type == 6))
					{
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(constraint[i3].malleable.prismatic.pivotA, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.rotationA, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.planeA, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.slidingA, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.slidingB, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.pivotB, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.rotationB, s, info);
							Nif.NifStream(constraint[i3].malleable.prismatic.planeB, s, info);
						}
						if (info.version >= 0x14020007)
						{
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.slidingA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.rotationA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.planeA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.pivotA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.slidingB, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.rotationB, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.planeB, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.prismatic.pivotB, s, info);
						}
						Nif.NifStream(constraint[i3].malleable.prismatic.minDistance, s, info);
						Nif.NifStream(constraint[i3].malleable.prismatic.maxDistance, s, info);
						Nif.NifStream(constraint[i3].malleable.prismatic.friction, s, info);
						if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
						{
							Nif.NifStream(constraint[i3].malleable.prismatic.motor.type, s, info);
							if ((constraint[i3].malleable.prismatic.motor.type == 1))
							{
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.tau, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.damping, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.positionMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.prismatic.motor.type == 2))
							{
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.velocityMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.velocityMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.velocityMotor.tau, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.velocityMotor.targetVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.velocityMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.prismatic.motor.type == 3))
							{
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.springDamperMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.springDamperMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.springDamperMotor.springConstant, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.springDamperMotor.springDamping, s, info);
								Nif.NifStream(constraint[i3].malleable.prismatic.motor.springDamperMotor.motorEnabled, s, info);
							}
						}
					}
					if ((constraint[i3].malleable.type == 7))
					{
						if ((info.userVersion2 <= 16))
						{
							Nif.NifStream(constraint[i3].malleable.ragdoll.pivotA, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.planeA, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.twistA, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.pivotB, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.planeB, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.twistB, s, info);
						}
						if ((info.userVersion2 > 16))
						{
							Nif.NifStream((Vector4)constraint[i3].malleable.ragdoll.twistA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.ragdoll.planeA, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.motorA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.ragdoll.pivotA, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.ragdoll.twistB, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.ragdoll.planeB, s, info);
							Nif.NifStream(constraint[i3].malleable.ragdoll.motorB, s, info);
							Nif.NifStream((Vector4)constraint[i3].malleable.ragdoll.pivotB, s, info);
						}
						Nif.NifStream(constraint[i3].malleable.ragdoll.coneMaxAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.ragdoll.planeMinAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.ragdoll.planeMaxAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.ragdoll.twistMinAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.ragdoll.twistMaxAngle, s, info);
						Nif.NifStream(constraint[i3].malleable.ragdoll.maxFriction, s, info);
						if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
						{
							Nif.NifStream(constraint[i3].malleable.ragdoll.motor.type, s, info);
							if ((constraint[i3].malleable.ragdoll.motor.type == 1))
							{
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.tau, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.damping, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.positionMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.ragdoll.motor.type == 2))
							{
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.velocityMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.velocityMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.velocityMotor.tau, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.velocityMotor.targetVelocity, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.velocityMotor.motorEnabled, s, info);
							}
							if ((constraint[i3].malleable.ragdoll.motor.type == 3))
							{
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.springDamperMotor.minForce, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.springDamperMotor.maxForce, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.springDamperMotor.springConstant, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.springDamperMotor.springDamping, s, info);
								Nif.NifStream(constraint[i3].malleable.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
							}
						}
					}
					if ((constraint[i3].malleable.type == 8))
					{
						Nif.NifStream(constraint[i3].malleable.stiffSpring.pivotA, s, info);
						Nif.NifStream(constraint[i3].malleable.stiffSpring.pivotB, s, info);
						Nif.NifStream(constraint[i3].malleable.stiffSpring.length, s, info);
					}
					if (info.version <= 0x14000005)
					{
						Nif.NifStream(constraint[i3].malleable.tau, s, info);
						Nif.NifStream(constraint[i3].malleable.damping, s, info);
					}
					if (info.version >= 0x14020007)
					{
						Nif.NifStream(constraint[i3].malleable.strength, s, info);
					}
				}
			}
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			var array_output_count = 0U;
			s.Append(base.AsString());
			numConstraints = (uint)constraint.Count;
			s.AppendLine($"      Name:  {name}");
			s.AppendLine($"      Mass:  {mass}");
			s.AppendLine($"      Restitution:  {restitution}");
			s.AppendLine($"      Friction:  {friction}");
			s.AppendLine($"      Radius:  {radius}");
			s.AppendLine($"      Unknown Int:  {material.unknownInt}");
			s.AppendLine($"      Material:  {material.material_ob}");
			s.AppendLine($"      Material:  {material.material_fo}");
			s.AppendLine($"      Material:  {material.material_sk}");
			s.AppendLine($"      Num Constraints:  {numConstraints}");
			array_output_count = 0;
			for (var i3 = 0; i3 < constraint.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Type:  {constraint[i3].type}");
				s.AppendLine($"        Num Entities 2:  {constraint[i3].numEntities2}");
				s.AppendLine($"        Entity A:  {constraint[i3].entityA}");
				s.AppendLine($"        Entity B:  {constraint[i3].entityB}");
				s.AppendLine($"        Priority:  {constraint[i3].priority}");
				if ((constraint[i3].type == 0))
				{
					s.AppendLine($"          Pivot A:  {constraint[i3].ballAndSocket.pivotA}");
					s.AppendLine($"          Pivot B:  {constraint[i3].ballAndSocket.pivotB}");
				}
				if ((constraint[i3].type == 1))
				{
					s.AppendLine($"          Pivot A:  {constraint[i3].hinge.pivotA}");
					s.AppendLine($"          Perp Axis In A1:  {constraint[i3].hinge.perpAxisInA1}");
					s.AppendLine($"          Perp Axis In A2:  {constraint[i3].hinge.perpAxisInA2}");
					s.AppendLine($"          Pivot B:  {constraint[i3].hinge.pivotB}");
					s.AppendLine($"          Axis B:  {constraint[i3].hinge.axisB}");
					s.AppendLine($"          Axis A:  {constraint[i3].hinge.axisA}");
					s.AppendLine($"          Perp Axis In B1:  {constraint[i3].hinge.perpAxisInB1}");
					s.AppendLine($"          Perp Axis In B2:  {constraint[i3].hinge.perpAxisInB2}");
				}
				if ((constraint[i3].type == 2))
				{
					s.AppendLine($"          Pivot A:  {constraint[i3].limitedHinge.pivotA}");
					s.AppendLine($"          Axis A:  {constraint[i3].limitedHinge.axisA}");
					s.AppendLine($"          Perp Axis In A1:  {constraint[i3].limitedHinge.perpAxisInA1}");
					s.AppendLine($"          Perp Axis In A2:  {constraint[i3].limitedHinge.perpAxisInA2}");
					s.AppendLine($"          Pivot B:  {constraint[i3].limitedHinge.pivotB}");
					s.AppendLine($"          Axis B:  {constraint[i3].limitedHinge.axisB}");
					s.AppendLine($"          Perp Axis In B2:  {constraint[i3].limitedHinge.perpAxisInB2}");
					s.AppendLine($"          Perp Axis In B1:  {constraint[i3].limitedHinge.perpAxisInB1}");
					s.AppendLine($"          Min Angle:  {constraint[i3].limitedHinge.minAngle}");
					s.AppendLine($"          Max Angle:  {constraint[i3].limitedHinge.maxAngle}");
					s.AppendLine($"          Max Friction:  {constraint[i3].limitedHinge.maxFriction}");
					s.AppendLine($"          Type:  {constraint[i3].limitedHinge.motor.type}");
					if ((constraint[i3].limitedHinge.motor.type == 1))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].limitedHinge.motor.positionMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].limitedHinge.motor.positionMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraint[i3].limitedHinge.motor.positionMotor.tau}");
						s.AppendLine($"            Damping:  {constraint[i3].limitedHinge.motor.positionMotor.damping}");
						s.AppendLine($"            Proportional Recovery Velocity:  {constraint[i3].limitedHinge.motor.positionMotor.proportionalRecoveryVelocity}");
						s.AppendLine($"            Constant Recovery Velocity:  {constraint[i3].limitedHinge.motor.positionMotor.constantRecoveryVelocity}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].limitedHinge.motor.positionMotor.motorEnabled}");
					}
					if ((constraint[i3].limitedHinge.motor.type == 2))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].limitedHinge.motor.velocityMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].limitedHinge.motor.velocityMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraint[i3].limitedHinge.motor.velocityMotor.tau}");
						s.AppendLine($"            Target Velocity:  {constraint[i3].limitedHinge.motor.velocityMotor.targetVelocity}");
						s.AppendLine($"            Use Velocity Target:  {constraint[i3].limitedHinge.motor.velocityMotor.useVelocityTarget}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].limitedHinge.motor.velocityMotor.motorEnabled}");
					}
					if ((constraint[i3].limitedHinge.motor.type == 3))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].limitedHinge.motor.springDamperMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].limitedHinge.motor.springDamperMotor.maxForce}");
						s.AppendLine($"            Spring Constant:  {constraint[i3].limitedHinge.motor.springDamperMotor.springConstant}");
						s.AppendLine($"            Spring Damping:  {constraint[i3].limitedHinge.motor.springDamperMotor.springDamping}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].limitedHinge.motor.springDamperMotor.motorEnabled}");
					}
				}
				if ((constraint[i3].type == 6))
				{
					s.AppendLine($"          Pivot A:  {constraint[i3].prismatic.pivotA}");
					s.AppendLine($"          Rotation A:  {constraint[i3].prismatic.rotationA}");
					s.AppendLine($"          Plane A:  {constraint[i3].prismatic.planeA}");
					s.AppendLine($"          Sliding A:  {constraint[i3].prismatic.slidingA}");
					s.AppendLine($"          Sliding B:  {constraint[i3].prismatic.slidingB}");
					s.AppendLine($"          Pivot B:  {constraint[i3].prismatic.pivotB}");
					s.AppendLine($"          Rotation B:  {constraint[i3].prismatic.rotationB}");
					s.AppendLine($"          Plane B:  {constraint[i3].prismatic.planeB}");
					s.AppendLine($"          Min Distance:  {constraint[i3].prismatic.minDistance}");
					s.AppendLine($"          Max Distance:  {constraint[i3].prismatic.maxDistance}");
					s.AppendLine($"          Friction:  {constraint[i3].prismatic.friction}");
					s.AppendLine($"          Type:  {constraint[i3].prismatic.motor.type}");
					if ((constraint[i3].prismatic.motor.type == 1))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].prismatic.motor.positionMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].prismatic.motor.positionMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraint[i3].prismatic.motor.positionMotor.tau}");
						s.AppendLine($"            Damping:  {constraint[i3].prismatic.motor.positionMotor.damping}");
						s.AppendLine($"            Proportional Recovery Velocity:  {constraint[i3].prismatic.motor.positionMotor.proportionalRecoveryVelocity}");
						s.AppendLine($"            Constant Recovery Velocity:  {constraint[i3].prismatic.motor.positionMotor.constantRecoveryVelocity}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].prismatic.motor.positionMotor.motorEnabled}");
					}
					if ((constraint[i3].prismatic.motor.type == 2))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].prismatic.motor.velocityMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].prismatic.motor.velocityMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraint[i3].prismatic.motor.velocityMotor.tau}");
						s.AppendLine($"            Target Velocity:  {constraint[i3].prismatic.motor.velocityMotor.targetVelocity}");
						s.AppendLine($"            Use Velocity Target:  {constraint[i3].prismatic.motor.velocityMotor.useVelocityTarget}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].prismatic.motor.velocityMotor.motorEnabled}");
					}
					if ((constraint[i3].prismatic.motor.type == 3))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].prismatic.motor.springDamperMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].prismatic.motor.springDamperMotor.maxForce}");
						s.AppendLine($"            Spring Constant:  {constraint[i3].prismatic.motor.springDamperMotor.springConstant}");
						s.AppendLine($"            Spring Damping:  {constraint[i3].prismatic.motor.springDamperMotor.springDamping}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].prismatic.motor.springDamperMotor.motorEnabled}");
					}
				}
				if ((constraint[i3].type == 7))
				{
					s.AppendLine($"          Pivot A:  {constraint[i3].ragdoll.pivotA}");
					s.AppendLine($"          Plane A:  {constraint[i3].ragdoll.planeA}");
					s.AppendLine($"          Twist A:  {constraint[i3].ragdoll.twistA}");
					s.AppendLine($"          Pivot B:  {constraint[i3].ragdoll.pivotB}");
					s.AppendLine($"          Plane B:  {constraint[i3].ragdoll.planeB}");
					s.AppendLine($"          Twist B:  {constraint[i3].ragdoll.twistB}");
					s.AppendLine($"          Motor A:  {constraint[i3].ragdoll.motorA}");
					s.AppendLine($"          Motor B:  {constraint[i3].ragdoll.motorB}");
					s.AppendLine($"          Cone Max Angle:  {constraint[i3].ragdoll.coneMaxAngle}");
					s.AppendLine($"          Plane Min Angle:  {constraint[i3].ragdoll.planeMinAngle}");
					s.AppendLine($"          Plane Max Angle:  {constraint[i3].ragdoll.planeMaxAngle}");
					s.AppendLine($"          Twist Min Angle:  {constraint[i3].ragdoll.twistMinAngle}");
					s.AppendLine($"          Twist Max Angle:  {constraint[i3].ragdoll.twistMaxAngle}");
					s.AppendLine($"          Max Friction:  {constraint[i3].ragdoll.maxFriction}");
					s.AppendLine($"          Type:  {constraint[i3].ragdoll.motor.type}");
					if ((constraint[i3].ragdoll.motor.type == 1))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].ragdoll.motor.positionMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].ragdoll.motor.positionMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraint[i3].ragdoll.motor.positionMotor.tau}");
						s.AppendLine($"            Damping:  {constraint[i3].ragdoll.motor.positionMotor.damping}");
						s.AppendLine($"            Proportional Recovery Velocity:  {constraint[i3].ragdoll.motor.positionMotor.proportionalRecoveryVelocity}");
						s.AppendLine($"            Constant Recovery Velocity:  {constraint[i3].ragdoll.motor.positionMotor.constantRecoveryVelocity}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].ragdoll.motor.positionMotor.motorEnabled}");
					}
					if ((constraint[i3].ragdoll.motor.type == 2))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].ragdoll.motor.velocityMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].ragdoll.motor.velocityMotor.maxForce}");
						s.AppendLine($"            Tau:  {constraint[i3].ragdoll.motor.velocityMotor.tau}");
						s.AppendLine($"            Target Velocity:  {constraint[i3].ragdoll.motor.velocityMotor.targetVelocity}");
						s.AppendLine($"            Use Velocity Target:  {constraint[i3].ragdoll.motor.velocityMotor.useVelocityTarget}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].ragdoll.motor.velocityMotor.motorEnabled}");
					}
					if ((constraint[i3].ragdoll.motor.type == 3))
					{
						s.AppendLine($"            Min Force:  {constraint[i3].ragdoll.motor.springDamperMotor.minForce}");
						s.AppendLine($"            Max Force:  {constraint[i3].ragdoll.motor.springDamperMotor.maxForce}");
						s.AppendLine($"            Spring Constant:  {constraint[i3].ragdoll.motor.springDamperMotor.springConstant}");
						s.AppendLine($"            Spring Damping:  {constraint[i3].ragdoll.motor.springDamperMotor.springDamping}");
						s.AppendLine($"            Motor Enabled:  {constraint[i3].ragdoll.motor.springDamperMotor.motorEnabled}");
					}
				}
				if ((constraint[i3].type == 8))
				{
					s.AppendLine($"          Pivot A:  {constraint[i3].stiffSpring.pivotA}");
					s.AppendLine($"          Pivot B:  {constraint[i3].stiffSpring.pivotB}");
					s.AppendLine($"          Length:  {constraint[i3].stiffSpring.length}");
				}
				if ((constraint[i3].type == 13))
				{
					s.AppendLine($"          Type:  {constraint[i3].malleable.type}");
					s.AppendLine($"          Num Entities:  {constraint[i3].malleable.numEntities}");
					s.AppendLine($"          Entity A:  {constraint[i3].malleable.entityA}");
					s.AppendLine($"          Entity B:  {constraint[i3].malleable.entityB}");
					s.AppendLine($"          Priority:  {constraint[i3].malleable.priority}");
					if ((constraint[i3].malleable.type == 0))
					{
						s.AppendLine($"            Pivot A:  {constraint[i3].malleable.ballAndSocket.pivotA}");
						s.AppendLine($"            Pivot B:  {constraint[i3].malleable.ballAndSocket.pivotB}");
					}
					if ((constraint[i3].malleable.type == 1))
					{
						s.AppendLine($"            Pivot A:  {constraint[i3].malleable.hinge.pivotA}");
						s.AppendLine($"            Perp Axis In A1:  {constraint[i3].malleable.hinge.perpAxisInA1}");
						s.AppendLine($"            Perp Axis In A2:  {constraint[i3].malleable.hinge.perpAxisInA2}");
						s.AppendLine($"            Pivot B:  {constraint[i3].malleable.hinge.pivotB}");
						s.AppendLine($"            Axis B:  {constraint[i3].malleable.hinge.axisB}");
						s.AppendLine($"            Axis A:  {constraint[i3].malleable.hinge.axisA}");
						s.AppendLine($"            Perp Axis In B1:  {constraint[i3].malleable.hinge.perpAxisInB1}");
						s.AppendLine($"            Perp Axis In B2:  {constraint[i3].malleable.hinge.perpAxisInB2}");
					}
					if ((constraint[i3].malleable.type == 2))
					{
						s.AppendLine($"            Pivot A:  {constraint[i3].malleable.limitedHinge.pivotA}");
						s.AppendLine($"            Axis A:  {constraint[i3].malleable.limitedHinge.axisA}");
						s.AppendLine($"            Perp Axis In A1:  {constraint[i3].malleable.limitedHinge.perpAxisInA1}");
						s.AppendLine($"            Perp Axis In A2:  {constraint[i3].malleable.limitedHinge.perpAxisInA2}");
						s.AppendLine($"            Pivot B:  {constraint[i3].malleable.limitedHinge.pivotB}");
						s.AppendLine($"            Axis B:  {constraint[i3].malleable.limitedHinge.axisB}");
						s.AppendLine($"            Perp Axis In B2:  {constraint[i3].malleable.limitedHinge.perpAxisInB2}");
						s.AppendLine($"            Perp Axis In B1:  {constraint[i3].malleable.limitedHinge.perpAxisInB1}");
						s.AppendLine($"            Min Angle:  {constraint[i3].malleable.limitedHinge.minAngle}");
						s.AppendLine($"            Max Angle:  {constraint[i3].malleable.limitedHinge.maxAngle}");
						s.AppendLine($"            Max Friction:  {constraint[i3].malleable.limitedHinge.maxFriction}");
						s.AppendLine($"            Type:  {constraint[i3].malleable.limitedHinge.motor.type}");
						if ((constraint[i3].malleable.limitedHinge.motor.type == 1))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.maxForce}");
							s.AppendLine($"              Tau:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.tau}");
							s.AppendLine($"              Damping:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.damping}");
							s.AppendLine($"              Proportional Recovery Velocity:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity}");
							s.AppendLine($"              Constant Recovery Velocity:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.limitedHinge.motor.positionMotor.motorEnabled}");
						}
						if ((constraint[i3].malleable.limitedHinge.motor.type == 2))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.limitedHinge.motor.velocityMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.limitedHinge.motor.velocityMotor.maxForce}");
							s.AppendLine($"              Tau:  {constraint[i3].malleable.limitedHinge.motor.velocityMotor.tau}");
							s.AppendLine($"              Target Velocity:  {constraint[i3].malleable.limitedHinge.motor.velocityMotor.targetVelocity}");
							s.AppendLine($"              Use Velocity Target:  {constraint[i3].malleable.limitedHinge.motor.velocityMotor.useVelocityTarget}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.limitedHinge.motor.velocityMotor.motorEnabled}");
						}
						if ((constraint[i3].malleable.limitedHinge.motor.type == 3))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.limitedHinge.motor.springDamperMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.limitedHinge.motor.springDamperMotor.maxForce}");
							s.AppendLine($"              Spring Constant:  {constraint[i3].malleable.limitedHinge.motor.springDamperMotor.springConstant}");
							s.AppendLine($"              Spring Damping:  {constraint[i3].malleable.limitedHinge.motor.springDamperMotor.springDamping}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.limitedHinge.motor.springDamperMotor.motorEnabled}");
						}
					}
					if ((constraint[i3].malleable.type == 6))
					{
						s.AppendLine($"            Pivot A:  {constraint[i3].malleable.prismatic.pivotA}");
						s.AppendLine($"            Rotation A:  {constraint[i3].malleable.prismatic.rotationA}");
						s.AppendLine($"            Plane A:  {constraint[i3].malleable.prismatic.planeA}");
						s.AppendLine($"            Sliding A:  {constraint[i3].malleable.prismatic.slidingA}");
						s.AppendLine($"            Sliding B:  {constraint[i3].malleable.prismatic.slidingB}");
						s.AppendLine($"            Pivot B:  {constraint[i3].malleable.prismatic.pivotB}");
						s.AppendLine($"            Rotation B:  {constraint[i3].malleable.prismatic.rotationB}");
						s.AppendLine($"            Plane B:  {constraint[i3].malleable.prismatic.planeB}");
						s.AppendLine($"            Min Distance:  {constraint[i3].malleable.prismatic.minDistance}");
						s.AppendLine($"            Max Distance:  {constraint[i3].malleable.prismatic.maxDistance}");
						s.AppendLine($"            Friction:  {constraint[i3].malleable.prismatic.friction}");
						s.AppendLine($"            Type:  {constraint[i3].malleable.prismatic.motor.type}");
						if ((constraint[i3].malleable.prismatic.motor.type == 1))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.prismatic.motor.positionMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.prismatic.motor.positionMotor.maxForce}");
							s.AppendLine($"              Tau:  {constraint[i3].malleable.prismatic.motor.positionMotor.tau}");
							s.AppendLine($"              Damping:  {constraint[i3].malleable.prismatic.motor.positionMotor.damping}");
							s.AppendLine($"              Proportional Recovery Velocity:  {constraint[i3].malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity}");
							s.AppendLine($"              Constant Recovery Velocity:  {constraint[i3].malleable.prismatic.motor.positionMotor.constantRecoveryVelocity}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.prismatic.motor.positionMotor.motorEnabled}");
						}
						if ((constraint[i3].malleable.prismatic.motor.type == 2))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.prismatic.motor.velocityMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.prismatic.motor.velocityMotor.maxForce}");
							s.AppendLine($"              Tau:  {constraint[i3].malleable.prismatic.motor.velocityMotor.tau}");
							s.AppendLine($"              Target Velocity:  {constraint[i3].malleable.prismatic.motor.velocityMotor.targetVelocity}");
							s.AppendLine($"              Use Velocity Target:  {constraint[i3].malleable.prismatic.motor.velocityMotor.useVelocityTarget}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.prismatic.motor.velocityMotor.motorEnabled}");
						}
						if ((constraint[i3].malleable.prismatic.motor.type == 3))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.prismatic.motor.springDamperMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.prismatic.motor.springDamperMotor.maxForce}");
							s.AppendLine($"              Spring Constant:  {constraint[i3].malleable.prismatic.motor.springDamperMotor.springConstant}");
							s.AppendLine($"              Spring Damping:  {constraint[i3].malleable.prismatic.motor.springDamperMotor.springDamping}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.prismatic.motor.springDamperMotor.motorEnabled}");
						}
					}
					if ((constraint[i3].malleable.type == 7))
					{
						s.AppendLine($"            Pivot A:  {constraint[i3].malleable.ragdoll.pivotA}");
						s.AppendLine($"            Plane A:  {constraint[i3].malleable.ragdoll.planeA}");
						s.AppendLine($"            Twist A:  {constraint[i3].malleable.ragdoll.twistA}");
						s.AppendLine($"            Pivot B:  {constraint[i3].malleable.ragdoll.pivotB}");
						s.AppendLine($"            Plane B:  {constraint[i3].malleable.ragdoll.planeB}");
						s.AppendLine($"            Twist B:  {constraint[i3].malleable.ragdoll.twistB}");
						s.AppendLine($"            Motor A:  {constraint[i3].malleable.ragdoll.motorA}");
						s.AppendLine($"            Motor B:  {constraint[i3].malleable.ragdoll.motorB}");
						s.AppendLine($"            Cone Max Angle:  {constraint[i3].malleable.ragdoll.coneMaxAngle}");
						s.AppendLine($"            Plane Min Angle:  {constraint[i3].malleable.ragdoll.planeMinAngle}");
						s.AppendLine($"            Plane Max Angle:  {constraint[i3].malleable.ragdoll.planeMaxAngle}");
						s.AppendLine($"            Twist Min Angle:  {constraint[i3].malleable.ragdoll.twistMinAngle}");
						s.AppendLine($"            Twist Max Angle:  {constraint[i3].malleable.ragdoll.twistMaxAngle}");
						s.AppendLine($"            Max Friction:  {constraint[i3].malleable.ragdoll.maxFriction}");
						s.AppendLine($"            Type:  {constraint[i3].malleable.ragdoll.motor.type}");
						if ((constraint[i3].malleable.ragdoll.motor.type == 1))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.ragdoll.motor.positionMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.ragdoll.motor.positionMotor.maxForce}");
							s.AppendLine($"              Tau:  {constraint[i3].malleable.ragdoll.motor.positionMotor.tau}");
							s.AppendLine($"              Damping:  {constraint[i3].malleable.ragdoll.motor.positionMotor.damping}");
							s.AppendLine($"              Proportional Recovery Velocity:  {constraint[i3].malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity}");
							s.AppendLine($"              Constant Recovery Velocity:  {constraint[i3].malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.ragdoll.motor.positionMotor.motorEnabled}");
						}
						if ((constraint[i3].malleable.ragdoll.motor.type == 2))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.ragdoll.motor.velocityMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.ragdoll.motor.velocityMotor.maxForce}");
							s.AppendLine($"              Tau:  {constraint[i3].malleable.ragdoll.motor.velocityMotor.tau}");
							s.AppendLine($"              Target Velocity:  {constraint[i3].malleable.ragdoll.motor.velocityMotor.targetVelocity}");
							s.AppendLine($"              Use Velocity Target:  {constraint[i3].malleable.ragdoll.motor.velocityMotor.useVelocityTarget}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.ragdoll.motor.velocityMotor.motorEnabled}");
						}
						if ((constraint[i3].malleable.ragdoll.motor.type == 3))
						{
							s.AppendLine($"              Min Force:  {constraint[i3].malleable.ragdoll.motor.springDamperMotor.minForce}");
							s.AppendLine($"              Max Force:  {constraint[i3].malleable.ragdoll.motor.springDamperMotor.maxForce}");
							s.AppendLine($"              Spring Constant:  {constraint[i3].malleable.ragdoll.motor.springDamperMotor.springConstant}");
							s.AppendLine($"              Spring Damping:  {constraint[i3].malleable.ragdoll.motor.springDamperMotor.springDamping}");
							s.AppendLine($"              Motor Enabled:  {constraint[i3].malleable.ragdoll.motor.springDamperMotor.motorEnabled}");
						}
					}
					if ((constraint[i3].malleable.type == 8))
					{
						s.AppendLine($"            Pivot A:  {constraint[i3].malleable.stiffSpring.pivotA}");
						s.AppendLine($"            Pivot B:  {constraint[i3].malleable.stiffSpring.pivotB}");
						s.AppendLine($"            Length:  {constraint[i3].malleable.stiffSpring.length}");
					}
					s.AppendLine($"          Tau:  {constraint[i3].malleable.tau}");
					s.AppendLine($"          Damping:  {constraint[i3].malleable.damping}");
					s.AppendLine($"          Strength:  {constraint[i3].malleable.strength}");
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < constraint.Count; i3++)
			{
				constraint[i3].entityA = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
				constraint[i3].entityB = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
				if ((constraint[i3].type == 13))
				{
					constraint[i3].malleable.entityA = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
					constraint[i3].malleable.entityB = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < constraint.Count; i3++)
			{
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < constraint.Count; i3++)
			{
				if (constraint[i3].entityA != null)
					ptrs.Add((NiObject)constraint[i3].entityA);
				if (constraint[i3].entityB != null)
					ptrs.Add((NiObject)constraint[i3].entityB);
				if (constraint[i3].malleable.entityA != null)
					ptrs.Add((NiObject)constraint[i3].malleable.entityA);
				if (constraint[i3].malleable.entityB != null)
					ptrs.Add((NiObject)constraint[i3].malleable.entityB);
			}
			return ptrs;
		}
	}
}
