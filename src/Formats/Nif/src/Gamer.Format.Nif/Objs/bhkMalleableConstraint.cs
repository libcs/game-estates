/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*! A malleable constraint. */
public class bhkMalleableConstraint : bhkConstraint {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkMalleableConstraint", bhkConstraint.TYPE);
	/*! Constraint within constraint. */
	internal MalleableDescriptor malleable;

	public bhkMalleableConstraint() {
	}

	/*!
	 * Used to determine the type of a particular instance of this object.
	 * \return The type constant for the actual type of the object.
	 */
	public override Type_ GetType() => TYPE;

	/*!
	 * A factory function used during file reading to create an instance of this type of object.
	 * \return A pointer to a newly allocated instance of this type of object.
	 */
	public static NiObject Create() => new bhkMalleableConstraint();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		uint block_num;
		base.Read(s, link_stack, info);
		Nif.NifStream(out malleable.type, s, info);
		Nif.NifStream(out malleable.numEntities, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out malleable.priority, s, info);
		if ((malleable.type == 0)) {
			Nif.NifStream(out malleable.ballAndSocket.pivotA, s, info);
			Nif.NifStream(out malleable.ballAndSocket.pivotB, s, info);
		}
		if ((malleable.type == 1)) {
			if (info.version <= 0x14000005) {
				Nif.NifStream(out malleable.hinge.pivotA, s, info);
				Nif.NifStream(out malleable.hinge.perpAxisInA1, s, info);
				Nif.NifStream(out malleable.hinge.perpAxisInA2, s, info);
				Nif.NifStream(out malleable.hinge.pivotB, s, info);
				Nif.NifStream(out malleable.hinge.axisB, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(out malleable.hinge.axisA, s, info);
				Nif.NifStream(out (Vector4)malleable.hinge.perpAxisInA1, s, info);
				Nif.NifStream(out (Vector4)malleable.hinge.perpAxisInA2, s, info);
				Nif.NifStream(out (Vector4)malleable.hinge.pivotA, s, info);
				Nif.NifStream(out (Vector4)malleable.hinge.axisB, s, info);
				Nif.NifStream(out malleable.hinge.perpAxisInB1, s, info);
				Nif.NifStream(out malleable.hinge.perpAxisInB2, s, info);
				Nif.NifStream(out (Vector4)malleable.hinge.pivotB, s, info);
			}
		}
		if ((malleable.type == 2)) {
			if ((info.userVersion2 <= 16)) {
				Nif.NifStream(out malleable.limitedHinge.pivotA, s, info);
				Nif.NifStream(out malleable.limitedHinge.axisA, s, info);
				Nif.NifStream(out malleable.limitedHinge.perpAxisInA1, s, info);
				Nif.NifStream(out malleable.limitedHinge.perpAxisInA2, s, info);
				Nif.NifStream(out malleable.limitedHinge.pivotB, s, info);
				Nif.NifStream(out malleable.limitedHinge.axisB, s, info);
				Nif.NifStream(out malleable.limitedHinge.perpAxisInB2, s, info);
			}
			if ((info.userVersion2 > 16)) {
				Nif.NifStream(out (Vector4)malleable.limitedHinge.axisA, s, info);
				Nif.NifStream(out (Vector4)malleable.limitedHinge.perpAxisInA1, s, info);
				Nif.NifStream(out (Vector4)malleable.limitedHinge.perpAxisInA2, s, info);
				Nif.NifStream(out (Vector4)malleable.limitedHinge.pivotA, s, info);
				Nif.NifStream(out (Vector4)malleable.limitedHinge.axisB, s, info);
				Nif.NifStream(out malleable.limitedHinge.perpAxisInB1, s, info);
				Nif.NifStream(out (Vector4)malleable.limitedHinge.perpAxisInB2, s, info);
				Nif.NifStream(out (Vector4)malleable.limitedHinge.pivotB, s, info);
			}
			Nif.NifStream(out malleable.limitedHinge.minAngle, s, info);
			Nif.NifStream(out malleable.limitedHinge.maxAngle, s, info);
			Nif.NifStream(out malleable.limitedHinge.maxFriction, s, info);
			if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
				Nif.NifStream(out malleable.limitedHinge.motor.type, s, info);
				if ((malleable.limitedHinge.motor.type == 1)) {
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.minForce, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.tau, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.damping, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.positionMotor.motorEnabled, s, info);
				}
				if ((malleable.limitedHinge.motor.type == 2)) {
					Nif.NifStream(out malleable.limitedHinge.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.velocityMotor.tau, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((malleable.limitedHinge.motor.type == 3)) {
					Nif.NifStream(out malleable.limitedHinge.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(out malleable.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}
		if ((malleable.type == 6)) {
			if (info.version <= 0x14000005) {
				Nif.NifStream(out malleable.prismatic.pivotA, s, info);
				Nif.NifStream(out malleable.prismatic.rotationA, s, info);
				Nif.NifStream(out malleable.prismatic.planeA, s, info);
				Nif.NifStream(out malleable.prismatic.slidingA, s, info);
				Nif.NifStream(out malleable.prismatic.slidingB, s, info);
				Nif.NifStream(out malleable.prismatic.pivotB, s, info);
				Nif.NifStream(out malleable.prismatic.rotationB, s, info);
				Nif.NifStream(out malleable.prismatic.planeB, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(out (Vector4)malleable.prismatic.slidingA, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.rotationA, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.planeA, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.pivotA, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.slidingB, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.rotationB, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.planeB, s, info);
				Nif.NifStream(out (Vector4)malleable.prismatic.pivotB, s, info);
			}
			Nif.NifStream(out malleable.prismatic.minDistance, s, info);
			Nif.NifStream(out malleable.prismatic.maxDistance, s, info);
			Nif.NifStream(out malleable.prismatic.friction, s, info);
			if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
				Nif.NifStream(out malleable.prismatic.motor.type, s, info);
				if ((malleable.prismatic.motor.type == 1)) {
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.minForce, s, info);
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.tau, s, info);
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.damping, s, info);
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(out malleable.prismatic.motor.positionMotor.motorEnabled, s, info);
				}
				if ((malleable.prismatic.motor.type == 2)) {
					Nif.NifStream(out malleable.prismatic.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(out malleable.prismatic.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(out malleable.prismatic.motor.velocityMotor.tau, s, info);
					Nif.NifStream(out malleable.prismatic.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(out malleable.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(out malleable.prismatic.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((malleable.prismatic.motor.type == 3)) {
					Nif.NifStream(out malleable.prismatic.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(out malleable.prismatic.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(out malleable.prismatic.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(out malleable.prismatic.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(out malleable.prismatic.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}
		if ((malleable.type == 7)) {
			if ((info.userVersion2 <= 16)) {
				Nif.NifStream(out malleable.ragdoll.pivotA, s, info);
				Nif.NifStream(out malleable.ragdoll.planeA, s, info);
				Nif.NifStream(out malleable.ragdoll.twistA, s, info);
				Nif.NifStream(out malleable.ragdoll.pivotB, s, info);
				Nif.NifStream(out malleable.ragdoll.planeB, s, info);
				Nif.NifStream(out malleable.ragdoll.twistB, s, info);
			}
			if ((info.userVersion2 > 16)) {
				Nif.NifStream(out (Vector4)malleable.ragdoll.twistA, s, info);
				Nif.NifStream(out (Vector4)malleable.ragdoll.planeA, s, info);
				Nif.NifStream(out malleable.ragdoll.motorA, s, info);
				Nif.NifStream(out (Vector4)malleable.ragdoll.pivotA, s, info);
				Nif.NifStream(out (Vector4)malleable.ragdoll.twistB, s, info);
				Nif.NifStream(out (Vector4)malleable.ragdoll.planeB, s, info);
				Nif.NifStream(out malleable.ragdoll.motorB, s, info);
				Nif.NifStream(out (Vector4)malleable.ragdoll.pivotB, s, info);
			}
			Nif.NifStream(out malleable.ragdoll.coneMaxAngle, s, info);
			Nif.NifStream(out malleable.ragdoll.planeMinAngle, s, info);
			Nif.NifStream(out malleable.ragdoll.planeMaxAngle, s, info);
			Nif.NifStream(out malleable.ragdoll.twistMinAngle, s, info);
			Nif.NifStream(out malleable.ragdoll.twistMaxAngle, s, info);
			Nif.NifStream(out malleable.ragdoll.maxFriction, s, info);
			if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
				Nif.NifStream(out malleable.ragdoll.motor.type, s, info);
				if ((malleable.ragdoll.motor.type == 1)) {
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.minForce, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.tau, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.damping, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.positionMotor.motorEnabled, s, info);
				}
				if ((malleable.ragdoll.motor.type == 2)) {
					Nif.NifStream(out malleable.ragdoll.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.velocityMotor.tau, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((malleable.ragdoll.motor.type == 3)) {
					Nif.NifStream(out malleable.ragdoll.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(out malleable.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}
		if ((malleable.type == 8)) {
			Nif.NifStream(out malleable.stiffSpring.pivotA, s, info);
			Nif.NifStream(out malleable.stiffSpring.pivotB, s, info);
			Nif.NifStream(out malleable.stiffSpring.length, s, info);
		}
		if (info.version <= 0x14000005) {
			Nif.NifStream(out malleable.tau, s, info);
			Nif.NifStream(out malleable.damping, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(out malleable.strength, s, info);
		}

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		Nif.NifStream(malleable.type, s, info);
		Nif.NifStream(malleable.numEntities, s, info);
		WriteRef((NiObject)malleable.entityA, s, info, link_map, missing_link_stack);
		WriteRef((NiObject)malleable.entityB, s, info, link_map, missing_link_stack);
		Nif.NifStream(malleable.priority, s, info);
		if ((malleable.type == 0)) {
			Nif.NifStream(malleable.ballAndSocket.pivotA, s, info);
			Nif.NifStream(malleable.ballAndSocket.pivotB, s, info);
		}
		if ((malleable.type == 1)) {
			if (info.version <= 0x14000005) {
				Nif.NifStream(malleable.hinge.pivotA, s, info);
				Nif.NifStream(malleable.hinge.perpAxisInA1, s, info);
				Nif.NifStream(malleable.hinge.perpAxisInA2, s, info);
				Nif.NifStream(malleable.hinge.pivotB, s, info);
				Nif.NifStream(malleable.hinge.axisB, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream(malleable.hinge.axisA, s, info);
				Nif.NifStream((Vector4)malleable.hinge.perpAxisInA1, s, info);
				Nif.NifStream((Vector4)malleable.hinge.perpAxisInA2, s, info);
				Nif.NifStream((Vector4)malleable.hinge.pivotA, s, info);
				Nif.NifStream((Vector4)malleable.hinge.axisB, s, info);
				Nif.NifStream(malleable.hinge.perpAxisInB1, s, info);
				Nif.NifStream(malleable.hinge.perpAxisInB2, s, info);
				Nif.NifStream((Vector4)malleable.hinge.pivotB, s, info);
			}
		}
		if ((malleable.type == 2)) {
			if ((info.userVersion2 <= 16)) {
				Nif.NifStream(malleable.limitedHinge.pivotA, s, info);
				Nif.NifStream(malleable.limitedHinge.axisA, s, info);
				Nif.NifStream(malleable.limitedHinge.perpAxisInA1, s, info);
				Nif.NifStream(malleable.limitedHinge.perpAxisInA2, s, info);
				Nif.NifStream(malleable.limitedHinge.pivotB, s, info);
				Nif.NifStream(malleable.limitedHinge.axisB, s, info);
				Nif.NifStream(malleable.limitedHinge.perpAxisInB2, s, info);
			}
			if ((info.userVersion2 > 16)) {
				Nif.NifStream((Vector4)malleable.limitedHinge.axisA, s, info);
				Nif.NifStream((Vector4)malleable.limitedHinge.perpAxisInA1, s, info);
				Nif.NifStream((Vector4)malleable.limitedHinge.perpAxisInA2, s, info);
				Nif.NifStream((Vector4)malleable.limitedHinge.pivotA, s, info);
				Nif.NifStream((Vector4)malleable.limitedHinge.axisB, s, info);
				Nif.NifStream(malleable.limitedHinge.perpAxisInB1, s, info);
				Nif.NifStream((Vector4)malleable.limitedHinge.perpAxisInB2, s, info);
				Nif.NifStream((Vector4)malleable.limitedHinge.pivotB, s, info);
			}
			Nif.NifStream(malleable.limitedHinge.minAngle, s, info);
			Nif.NifStream(malleable.limitedHinge.maxAngle, s, info);
			Nif.NifStream(malleable.limitedHinge.maxFriction, s, info);
			if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
				Nif.NifStream(malleable.limitedHinge.motor.type, s, info);
				if ((malleable.limitedHinge.motor.type == 1)) {
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.minForce, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.tau, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.damping, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.positionMotor.motorEnabled, s, info);
				}
				if ((malleable.limitedHinge.motor.type == 2)) {
					Nif.NifStream(malleable.limitedHinge.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.velocityMotor.tau, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((malleable.limitedHinge.motor.type == 3)) {
					Nif.NifStream(malleable.limitedHinge.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(malleable.limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}
		if ((malleable.type == 6)) {
			if (info.version <= 0x14000005) {
				Nif.NifStream(malleable.prismatic.pivotA, s, info);
				Nif.NifStream(malleable.prismatic.rotationA, s, info);
				Nif.NifStream(malleable.prismatic.planeA, s, info);
				Nif.NifStream(malleable.prismatic.slidingA, s, info);
				Nif.NifStream(malleable.prismatic.slidingB, s, info);
				Nif.NifStream(malleable.prismatic.pivotB, s, info);
				Nif.NifStream(malleable.prismatic.rotationB, s, info);
				Nif.NifStream(malleable.prismatic.planeB, s, info);
			}
			if (info.version >= 0x14020007) {
				Nif.NifStream((Vector4)malleable.prismatic.slidingA, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.rotationA, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.planeA, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.pivotA, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.slidingB, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.rotationB, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.planeB, s, info);
				Nif.NifStream((Vector4)malleable.prismatic.pivotB, s, info);
			}
			Nif.NifStream(malleable.prismatic.minDistance, s, info);
			Nif.NifStream(malleable.prismatic.maxDistance, s, info);
			Nif.NifStream(malleable.prismatic.friction, s, info);
			if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
				Nif.NifStream(malleable.prismatic.motor.type, s, info);
				if ((malleable.prismatic.motor.type == 1)) {
					Nif.NifStream(malleable.prismatic.motor.positionMotor.minForce, s, info);
					Nif.NifStream(malleable.prismatic.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(malleable.prismatic.motor.positionMotor.tau, s, info);
					Nif.NifStream(malleable.prismatic.motor.positionMotor.damping, s, info);
					Nif.NifStream(malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(malleable.prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(malleable.prismatic.motor.positionMotor.motorEnabled, s, info);
				}
				if ((malleable.prismatic.motor.type == 2)) {
					Nif.NifStream(malleable.prismatic.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(malleable.prismatic.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(malleable.prismatic.motor.velocityMotor.tau, s, info);
					Nif.NifStream(malleable.prismatic.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(malleable.prismatic.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(malleable.prismatic.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((malleable.prismatic.motor.type == 3)) {
					Nif.NifStream(malleable.prismatic.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(malleable.prismatic.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(malleable.prismatic.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(malleable.prismatic.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(malleable.prismatic.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}
		if ((malleable.type == 7)) {
			if ((info.userVersion2 <= 16)) {
				Nif.NifStream(malleable.ragdoll.pivotA, s, info);
				Nif.NifStream(malleable.ragdoll.planeA, s, info);
				Nif.NifStream(malleable.ragdoll.twistA, s, info);
				Nif.NifStream(malleable.ragdoll.pivotB, s, info);
				Nif.NifStream(malleable.ragdoll.planeB, s, info);
				Nif.NifStream(malleable.ragdoll.twistB, s, info);
			}
			if ((info.userVersion2 > 16)) {
				Nif.NifStream((Vector4)malleable.ragdoll.twistA, s, info);
				Nif.NifStream((Vector4)malleable.ragdoll.planeA, s, info);
				Nif.NifStream(malleable.ragdoll.motorA, s, info);
				Nif.NifStream((Vector4)malleable.ragdoll.pivotA, s, info);
				Nif.NifStream((Vector4)malleable.ragdoll.twistB, s, info);
				Nif.NifStream((Vector4)malleable.ragdoll.planeB, s, info);
				Nif.NifStream(malleable.ragdoll.motorB, s, info);
				Nif.NifStream((Vector4)malleable.ragdoll.pivotB, s, info);
			}
			Nif.NifStream(malleable.ragdoll.coneMaxAngle, s, info);
			Nif.NifStream(malleable.ragdoll.planeMinAngle, s, info);
			Nif.NifStream(malleable.ragdoll.planeMaxAngle, s, info);
			Nif.NifStream(malleable.ragdoll.twistMinAngle, s, info);
			Nif.NifStream(malleable.ragdoll.twistMaxAngle, s, info);
			Nif.NifStream(malleable.ragdoll.maxFriction, s, info);
			if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
				Nif.NifStream(malleable.ragdoll.motor.type, s, info);
				if ((malleable.ragdoll.motor.type == 1)) {
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.minForce, s, info);
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.tau, s, info);
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.damping, s, info);
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(malleable.ragdoll.motor.positionMotor.motorEnabled, s, info);
				}
				if ((malleable.ragdoll.motor.type == 2)) {
					Nif.NifStream(malleable.ragdoll.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(malleable.ragdoll.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(malleable.ragdoll.motor.velocityMotor.tau, s, info);
					Nif.NifStream(malleable.ragdoll.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(malleable.ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(malleable.ragdoll.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((malleable.ragdoll.motor.type == 3)) {
					Nif.NifStream(malleable.ragdoll.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(malleable.ragdoll.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(malleable.ragdoll.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(malleable.ragdoll.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(malleable.ragdoll.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}
		if ((malleable.type == 8)) {
			Nif.NifStream(malleable.stiffSpring.pivotA, s, info);
			Nif.NifStream(malleable.stiffSpring.pivotB, s, info);
			Nif.NifStream(malleable.stiffSpring.length, s, info);
		}
		if (info.version <= 0x14000005) {
			Nif.NifStream(malleable.tau, s, info);
			Nif.NifStream(malleable.damping, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(malleable.strength, s, info);
		}

	}

	/*!
	 * Summarizes the information contained in this object in English.
	 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
	 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
	 */
	public override string AsString(bool verbose = false) {

		var s = new System.Text.StringBuilder();
		s.Append(base.AsString());
		s.AppendLine($"    Type:  {malleable.type}");
		s.AppendLine($"    Num Entities:  {malleable.numEntities}");
		s.AppendLine($"    Entity A:  {malleable.entityA}");
		s.AppendLine($"    Entity B:  {malleable.entityB}");
		s.AppendLine($"    Priority:  {malleable.priority}");
		if ((malleable.type == 0)) {
			s.AppendLine($"      Pivot A:  {malleable.ballAndSocket.pivotA}");
			s.AppendLine($"      Pivot B:  {malleable.ballAndSocket.pivotB}");
		}
		if ((malleable.type == 1)) {
			s.AppendLine($"      Pivot A:  {malleable.hinge.pivotA}");
			s.AppendLine($"      Perp Axis In A1:  {malleable.hinge.perpAxisInA1}");
			s.AppendLine($"      Perp Axis In A2:  {malleable.hinge.perpAxisInA2}");
			s.AppendLine($"      Pivot B:  {malleable.hinge.pivotB}");
			s.AppendLine($"      Axis B:  {malleable.hinge.axisB}");
			s.AppendLine($"      Axis A:  {malleable.hinge.axisA}");
			s.AppendLine($"      Perp Axis In B1:  {malleable.hinge.perpAxisInB1}");
			s.AppendLine($"      Perp Axis In B2:  {malleable.hinge.perpAxisInB2}");
		}
		if ((malleable.type == 2)) {
			s.AppendLine($"      Pivot A:  {malleable.limitedHinge.pivotA}");
			s.AppendLine($"      Axis A:  {malleable.limitedHinge.axisA}");
			s.AppendLine($"      Perp Axis In A1:  {malleable.limitedHinge.perpAxisInA1}");
			s.AppendLine($"      Perp Axis In A2:  {malleable.limitedHinge.perpAxisInA2}");
			s.AppendLine($"      Pivot B:  {malleable.limitedHinge.pivotB}");
			s.AppendLine($"      Axis B:  {malleable.limitedHinge.axisB}");
			s.AppendLine($"      Perp Axis In B2:  {malleable.limitedHinge.perpAxisInB2}");
			s.AppendLine($"      Perp Axis In B1:  {malleable.limitedHinge.perpAxisInB1}");
			s.AppendLine($"      Min Angle:  {malleable.limitedHinge.minAngle}");
			s.AppendLine($"      Max Angle:  {malleable.limitedHinge.maxAngle}");
			s.AppendLine($"      Max Friction:  {malleable.limitedHinge.maxFriction}");
			s.AppendLine($"      Type:  {malleable.limitedHinge.motor.type}");
			if ((malleable.limitedHinge.motor.type == 1)) {
				s.AppendLine($"        Min Force:  {malleable.limitedHinge.motor.positionMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.limitedHinge.motor.positionMotor.maxForce}");
				s.AppendLine($"        Tau:  {malleable.limitedHinge.motor.positionMotor.tau}");
				s.AppendLine($"        Damping:  {malleable.limitedHinge.motor.positionMotor.damping}");
				s.AppendLine($"        Proportional Recovery Velocity:  {malleable.limitedHinge.motor.positionMotor.proportionalRecoveryVelocity}");
				s.AppendLine($"        Constant Recovery Velocity:  {malleable.limitedHinge.motor.positionMotor.constantRecoveryVelocity}");
				s.AppendLine($"        Motor Enabled:  {malleable.limitedHinge.motor.positionMotor.motorEnabled}");
			}
			if ((malleable.limitedHinge.motor.type == 2)) {
				s.AppendLine($"        Min Force:  {malleable.limitedHinge.motor.velocityMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.limitedHinge.motor.velocityMotor.maxForce}");
				s.AppendLine($"        Tau:  {malleable.limitedHinge.motor.velocityMotor.tau}");
				s.AppendLine($"        Target Velocity:  {malleable.limitedHinge.motor.velocityMotor.targetVelocity}");
				s.AppendLine($"        Use Velocity Target:  {malleable.limitedHinge.motor.velocityMotor.useVelocityTarget}");
				s.AppendLine($"        Motor Enabled:  {malleable.limitedHinge.motor.velocityMotor.motorEnabled}");
			}
			if ((malleable.limitedHinge.motor.type == 3)) {
				s.AppendLine($"        Min Force:  {malleable.limitedHinge.motor.springDamperMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.limitedHinge.motor.springDamperMotor.maxForce}");
				s.AppendLine($"        Spring Constant:  {malleable.limitedHinge.motor.springDamperMotor.springConstant}");
				s.AppendLine($"        Spring Damping:  {malleable.limitedHinge.motor.springDamperMotor.springDamping}");
				s.AppendLine($"        Motor Enabled:  {malleable.limitedHinge.motor.springDamperMotor.motorEnabled}");
			}
		}
		if ((malleable.type == 6)) {
			s.AppendLine($"      Pivot A:  {malleable.prismatic.pivotA}");
			s.AppendLine($"      Rotation A:  {malleable.prismatic.rotationA}");
			s.AppendLine($"      Plane A:  {malleable.prismatic.planeA}");
			s.AppendLine($"      Sliding A:  {malleable.prismatic.slidingA}");
			s.AppendLine($"      Sliding B:  {malleable.prismatic.slidingB}");
			s.AppendLine($"      Pivot B:  {malleable.prismatic.pivotB}");
			s.AppendLine($"      Rotation B:  {malleable.prismatic.rotationB}");
			s.AppendLine($"      Plane B:  {malleable.prismatic.planeB}");
			s.AppendLine($"      Min Distance:  {malleable.prismatic.minDistance}");
			s.AppendLine($"      Max Distance:  {malleable.prismatic.maxDistance}");
			s.AppendLine($"      Friction:  {malleable.prismatic.friction}");
			s.AppendLine($"      Type:  {malleable.prismatic.motor.type}");
			if ((malleable.prismatic.motor.type == 1)) {
				s.AppendLine($"        Min Force:  {malleable.prismatic.motor.positionMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.prismatic.motor.positionMotor.maxForce}");
				s.AppendLine($"        Tau:  {malleable.prismatic.motor.positionMotor.tau}");
				s.AppendLine($"        Damping:  {malleable.prismatic.motor.positionMotor.damping}");
				s.AppendLine($"        Proportional Recovery Velocity:  {malleable.prismatic.motor.positionMotor.proportionalRecoveryVelocity}");
				s.AppendLine($"        Constant Recovery Velocity:  {malleable.prismatic.motor.positionMotor.constantRecoveryVelocity}");
				s.AppendLine($"        Motor Enabled:  {malleable.prismatic.motor.positionMotor.motorEnabled}");
			}
			if ((malleable.prismatic.motor.type == 2)) {
				s.AppendLine($"        Min Force:  {malleable.prismatic.motor.velocityMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.prismatic.motor.velocityMotor.maxForce}");
				s.AppendLine($"        Tau:  {malleable.prismatic.motor.velocityMotor.tau}");
				s.AppendLine($"        Target Velocity:  {malleable.prismatic.motor.velocityMotor.targetVelocity}");
				s.AppendLine($"        Use Velocity Target:  {malleable.prismatic.motor.velocityMotor.useVelocityTarget}");
				s.AppendLine($"        Motor Enabled:  {malleable.prismatic.motor.velocityMotor.motorEnabled}");
			}
			if ((malleable.prismatic.motor.type == 3)) {
				s.AppendLine($"        Min Force:  {malleable.prismatic.motor.springDamperMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.prismatic.motor.springDamperMotor.maxForce}");
				s.AppendLine($"        Spring Constant:  {malleable.prismatic.motor.springDamperMotor.springConstant}");
				s.AppendLine($"        Spring Damping:  {malleable.prismatic.motor.springDamperMotor.springDamping}");
				s.AppendLine($"        Motor Enabled:  {malleable.prismatic.motor.springDamperMotor.motorEnabled}");
			}
		}
		if ((malleable.type == 7)) {
			s.AppendLine($"      Pivot A:  {malleable.ragdoll.pivotA}");
			s.AppendLine($"      Plane A:  {malleable.ragdoll.planeA}");
			s.AppendLine($"      Twist A:  {malleable.ragdoll.twistA}");
			s.AppendLine($"      Pivot B:  {malleable.ragdoll.pivotB}");
			s.AppendLine($"      Plane B:  {malleable.ragdoll.planeB}");
			s.AppendLine($"      Twist B:  {malleable.ragdoll.twistB}");
			s.AppendLine($"      Motor A:  {malleable.ragdoll.motorA}");
			s.AppendLine($"      Motor B:  {malleable.ragdoll.motorB}");
			s.AppendLine($"      Cone Max Angle:  {malleable.ragdoll.coneMaxAngle}");
			s.AppendLine($"      Plane Min Angle:  {malleable.ragdoll.planeMinAngle}");
			s.AppendLine($"      Plane Max Angle:  {malleable.ragdoll.planeMaxAngle}");
			s.AppendLine($"      Twist Min Angle:  {malleable.ragdoll.twistMinAngle}");
			s.AppendLine($"      Twist Max Angle:  {malleable.ragdoll.twistMaxAngle}");
			s.AppendLine($"      Max Friction:  {malleable.ragdoll.maxFriction}");
			s.AppendLine($"      Type:  {malleable.ragdoll.motor.type}");
			if ((malleable.ragdoll.motor.type == 1)) {
				s.AppendLine($"        Min Force:  {malleable.ragdoll.motor.positionMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.ragdoll.motor.positionMotor.maxForce}");
				s.AppendLine($"        Tau:  {malleable.ragdoll.motor.positionMotor.tau}");
				s.AppendLine($"        Damping:  {malleable.ragdoll.motor.positionMotor.damping}");
				s.AppendLine($"        Proportional Recovery Velocity:  {malleable.ragdoll.motor.positionMotor.proportionalRecoveryVelocity}");
				s.AppendLine($"        Constant Recovery Velocity:  {malleable.ragdoll.motor.positionMotor.constantRecoveryVelocity}");
				s.AppendLine($"        Motor Enabled:  {malleable.ragdoll.motor.positionMotor.motorEnabled}");
			}
			if ((malleable.ragdoll.motor.type == 2)) {
				s.AppendLine($"        Min Force:  {malleable.ragdoll.motor.velocityMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.ragdoll.motor.velocityMotor.maxForce}");
				s.AppendLine($"        Tau:  {malleable.ragdoll.motor.velocityMotor.tau}");
				s.AppendLine($"        Target Velocity:  {malleable.ragdoll.motor.velocityMotor.targetVelocity}");
				s.AppendLine($"        Use Velocity Target:  {malleable.ragdoll.motor.velocityMotor.useVelocityTarget}");
				s.AppendLine($"        Motor Enabled:  {malleable.ragdoll.motor.velocityMotor.motorEnabled}");
			}
			if ((malleable.ragdoll.motor.type == 3)) {
				s.AppendLine($"        Min Force:  {malleable.ragdoll.motor.springDamperMotor.minForce}");
				s.AppendLine($"        Max Force:  {malleable.ragdoll.motor.springDamperMotor.maxForce}");
				s.AppendLine($"        Spring Constant:  {malleable.ragdoll.motor.springDamperMotor.springConstant}");
				s.AppendLine($"        Spring Damping:  {malleable.ragdoll.motor.springDamperMotor.springDamping}");
				s.AppendLine($"        Motor Enabled:  {malleable.ragdoll.motor.springDamperMotor.motorEnabled}");
			}
		}
		if ((malleable.type == 8)) {
			s.AppendLine($"      Pivot A:  {malleable.stiffSpring.pivotA}");
			s.AppendLine($"      Pivot B:  {malleable.stiffSpring.pivotB}");
			s.AppendLine($"      Length:  {malleable.stiffSpring.length}");
		}
		s.AppendLine($"    Tau:  {malleable.tau}");
		s.AppendLine($"    Damping:  {malleable.damping}");
		s.AppendLine($"    Strength:  {malleable.strength}");
		return s.ToString();

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

		base.FixLinks(objects, link_stack, missing_link_stack, info);
		malleable.entityA = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);
		malleable.entityB = FixLink<bhkEntity>(objects, link_stack, missing_link_stack, info);

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetRefs() {
		var refs = base.GetRefs();
		return refs;
	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetPtrs() {
		var ptrs = base.GetPtrs();
		if (malleable.entityA != null)
			ptrs.Add((NiObject)malleable.entityA);
		if (malleable.entityB != null)
			ptrs.Add((NiObject)malleable.entityB);
		return ptrs;
	}


}

}