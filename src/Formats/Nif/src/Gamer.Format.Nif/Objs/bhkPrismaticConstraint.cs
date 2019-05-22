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

/*! A prismatic constraint. */
public class bhkPrismaticConstraint : bhkConstraint {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkPrismaticConstraint", bhkConstraint.TYPE);
	/*! Describes a prismatic constraint */
	internal PrismaticDescriptor prismatic;

	public bhkPrismaticConstraint() {
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
	public static NiObject Create() => new bhkPrismaticConstraint();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);
		if (info.version <= 0x14000005) {
			Nif.NifStream(out prismatic.pivotA, s, info);
			Nif.NifStream(out prismatic.rotationA, s, info);
			Nif.NifStream(out prismatic.planeA, s, info);
			Nif.NifStream(out prismatic.slidingA, s, info);
			Nif.NifStream(out prismatic.slidingB, s, info);
			Nif.NifStream(out prismatic.pivotB, s, info);
			Nif.NifStream(out prismatic.rotationB, s, info);
			Nif.NifStream(out prismatic.planeB, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream(out (Vector4)prismatic.slidingA, s, info);
			Nif.NifStream(out (Vector4)prismatic.rotationA, s, info);
			Nif.NifStream(out (Vector4)prismatic.planeA, s, info);
			Nif.NifStream(out (Vector4)prismatic.pivotA, s, info);
			Nif.NifStream(out (Vector4)prismatic.slidingB, s, info);
			Nif.NifStream(out (Vector4)prismatic.rotationB, s, info);
			Nif.NifStream(out (Vector4)prismatic.planeB, s, info);
			Nif.NifStream(out (Vector4)prismatic.pivotB, s, info);
		}
		Nif.NifStream(out prismatic.minDistance, s, info);
		Nif.NifStream(out prismatic.maxDistance, s, info);
		Nif.NifStream(out prismatic.friction, s, info);
		if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
			Nif.NifStream(out prismatic.motor.type, s, info);
			if ((prismatic.motor.type == 1)) {
				Nif.NifStream(out prismatic.motor.positionMotor.minForce, s, info);
				Nif.NifStream(out prismatic.motor.positionMotor.maxForce, s, info);
				Nif.NifStream(out prismatic.motor.positionMotor.tau, s, info);
				Nif.NifStream(out prismatic.motor.positionMotor.damping, s, info);
				Nif.NifStream(out prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
				Nif.NifStream(out prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
				Nif.NifStream(out prismatic.motor.positionMotor.motorEnabled, s, info);
			}
			if ((prismatic.motor.type == 2)) {
				Nif.NifStream(out prismatic.motor.velocityMotor.minForce, s, info);
				Nif.NifStream(out prismatic.motor.velocityMotor.maxForce, s, info);
				Nif.NifStream(out prismatic.motor.velocityMotor.tau, s, info);
				Nif.NifStream(out prismatic.motor.velocityMotor.targetVelocity, s, info);
				Nif.NifStream(out prismatic.motor.velocityMotor.useVelocityTarget, s, info);
				Nif.NifStream(out prismatic.motor.velocityMotor.motorEnabled, s, info);
			}
			if ((prismatic.motor.type == 3)) {
				Nif.NifStream(out prismatic.motor.springDamperMotor.minForce, s, info);
				Nif.NifStream(out prismatic.motor.springDamperMotor.maxForce, s, info);
				Nif.NifStream(out prismatic.motor.springDamperMotor.springConstant, s, info);
				Nif.NifStream(out prismatic.motor.springDamperMotor.springDamping, s, info);
				Nif.NifStream(out prismatic.motor.springDamperMotor.motorEnabled, s, info);
			}
		}

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		if (info.version <= 0x14000005) {
			Nif.NifStream(prismatic.pivotA, s, info);
			Nif.NifStream(prismatic.rotationA, s, info);
			Nif.NifStream(prismatic.planeA, s, info);
			Nif.NifStream(prismatic.slidingA, s, info);
			Nif.NifStream(prismatic.slidingB, s, info);
			Nif.NifStream(prismatic.pivotB, s, info);
			Nif.NifStream(prismatic.rotationB, s, info);
			Nif.NifStream(prismatic.planeB, s, info);
		}
		if (info.version >= 0x14020007) {
			Nif.NifStream((Vector4)prismatic.slidingA, s, info);
			Nif.NifStream((Vector4)prismatic.rotationA, s, info);
			Nif.NifStream((Vector4)prismatic.planeA, s, info);
			Nif.NifStream((Vector4)prismatic.pivotA, s, info);
			Nif.NifStream((Vector4)prismatic.slidingB, s, info);
			Nif.NifStream((Vector4)prismatic.rotationB, s, info);
			Nif.NifStream((Vector4)prismatic.planeB, s, info);
			Nif.NifStream((Vector4)prismatic.pivotB, s, info);
		}
		Nif.NifStream(prismatic.minDistance, s, info);
		Nif.NifStream(prismatic.maxDistance, s, info);
		Nif.NifStream(prismatic.friction, s, info);
		if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
			Nif.NifStream(prismatic.motor.type, s, info);
			if ((prismatic.motor.type == 1)) {
				Nif.NifStream(prismatic.motor.positionMotor.minForce, s, info);
				Nif.NifStream(prismatic.motor.positionMotor.maxForce, s, info);
				Nif.NifStream(prismatic.motor.positionMotor.tau, s, info);
				Nif.NifStream(prismatic.motor.positionMotor.damping, s, info);
				Nif.NifStream(prismatic.motor.positionMotor.proportionalRecoveryVelocity, s, info);
				Nif.NifStream(prismatic.motor.positionMotor.constantRecoveryVelocity, s, info);
				Nif.NifStream(prismatic.motor.positionMotor.motorEnabled, s, info);
			}
			if ((prismatic.motor.type == 2)) {
				Nif.NifStream(prismatic.motor.velocityMotor.minForce, s, info);
				Nif.NifStream(prismatic.motor.velocityMotor.maxForce, s, info);
				Nif.NifStream(prismatic.motor.velocityMotor.tau, s, info);
				Nif.NifStream(prismatic.motor.velocityMotor.targetVelocity, s, info);
				Nif.NifStream(prismatic.motor.velocityMotor.useVelocityTarget, s, info);
				Nif.NifStream(prismatic.motor.velocityMotor.motorEnabled, s, info);
			}
			if ((prismatic.motor.type == 3)) {
				Nif.NifStream(prismatic.motor.springDamperMotor.minForce, s, info);
				Nif.NifStream(prismatic.motor.springDamperMotor.maxForce, s, info);
				Nif.NifStream(prismatic.motor.springDamperMotor.springConstant, s, info);
				Nif.NifStream(prismatic.motor.springDamperMotor.springDamping, s, info);
				Nif.NifStream(prismatic.motor.springDamperMotor.motorEnabled, s, info);
			}
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
		s.AppendLine($"    Pivot A:  {prismatic.pivotA}");
		s.AppendLine($"    Rotation A:  {prismatic.rotationA}");
		s.AppendLine($"    Plane A:  {prismatic.planeA}");
		s.AppendLine($"    Sliding A:  {prismatic.slidingA}");
		s.AppendLine($"    Sliding B:  {prismatic.slidingB}");
		s.AppendLine($"    Pivot B:  {prismatic.pivotB}");
		s.AppendLine($"    Rotation B:  {prismatic.rotationB}");
		s.AppendLine($"    Plane B:  {prismatic.planeB}");
		s.AppendLine($"    Min Distance:  {prismatic.minDistance}");
		s.AppendLine($"    Max Distance:  {prismatic.maxDistance}");
		s.AppendLine($"    Friction:  {prismatic.friction}");
		s.AppendLine($"    Type:  {prismatic.motor.type}");
		if ((prismatic.motor.type == 1)) {
			s.AppendLine($"      Min Force:  {prismatic.motor.positionMotor.minForce}");
			s.AppendLine($"      Max Force:  {prismatic.motor.positionMotor.maxForce}");
			s.AppendLine($"      Tau:  {prismatic.motor.positionMotor.tau}");
			s.AppendLine($"      Damping:  {prismatic.motor.positionMotor.damping}");
			s.AppendLine($"      Proportional Recovery Velocity:  {prismatic.motor.positionMotor.proportionalRecoveryVelocity}");
			s.AppendLine($"      Constant Recovery Velocity:  {prismatic.motor.positionMotor.constantRecoveryVelocity}");
			s.AppendLine($"      Motor Enabled:  {prismatic.motor.positionMotor.motorEnabled}");
		}
		if ((prismatic.motor.type == 2)) {
			s.AppendLine($"      Min Force:  {prismatic.motor.velocityMotor.minForce}");
			s.AppendLine($"      Max Force:  {prismatic.motor.velocityMotor.maxForce}");
			s.AppendLine($"      Tau:  {prismatic.motor.velocityMotor.tau}");
			s.AppendLine($"      Target Velocity:  {prismatic.motor.velocityMotor.targetVelocity}");
			s.AppendLine($"      Use Velocity Target:  {prismatic.motor.velocityMotor.useVelocityTarget}");
			s.AppendLine($"      Motor Enabled:  {prismatic.motor.velocityMotor.motorEnabled}");
		}
		if ((prismatic.motor.type == 3)) {
			s.AppendLine($"      Min Force:  {prismatic.motor.springDamperMotor.minForce}");
			s.AppendLine($"      Max Force:  {prismatic.motor.springDamperMotor.maxForce}");
			s.AppendLine($"      Spring Constant:  {prismatic.motor.springDamperMotor.springConstant}");
			s.AppendLine($"      Spring Damping:  {prismatic.motor.springDamperMotor.springDamping}");
			s.AppendLine($"      Motor Enabled:  {prismatic.motor.springDamperMotor.motorEnabled}");
		}
		return s.ToString();

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

		base.FixLinks(objects, link_stack, missing_link_stack, info);

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetRefs() {
		var refs = base.GetRefs();
		return refs;
	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override List<NiObject> GetPtrs() {
		var ptrs = base.GetPtrs();
		return ptrs;
	}


}

}