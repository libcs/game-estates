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

/*! Hinge constraint. */
public class bhkLimitedHingeConstraint : bhkConstraint {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkLimitedHingeConstraint", bhkConstraint.TYPE);
	/*! Describes a limited hinge constraint */
	internal LimitedHingeDescriptor limitedHinge;

	public bhkLimitedHingeConstraint() {
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
	public static NiObject Create() => new bhkLimitedHingeConstraint();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);
		if ((info.userVersion2 <= 16)) {
			Nif.NifStream(out limitedHinge.pivotA, s, info);
			Nif.NifStream(out limitedHinge.axisA, s, info);
			Nif.NifStream(out limitedHinge.perpAxisInA1, s, info);
			Nif.NifStream(out limitedHinge.perpAxisInA2, s, info);
			Nif.NifStream(out limitedHinge.pivotB, s, info);
			Nif.NifStream(out limitedHinge.axisB, s, info);
			Nif.NifStream(out limitedHinge.perpAxisInB2, s, info);
		}
		if ((info.userVersion2 > 16)) {
			Nif.NifStream(out (Vector4)limitedHinge.axisA, s, info);
			Nif.NifStream(out (Vector4)limitedHinge.perpAxisInA1, s, info);
			Nif.NifStream(out (Vector4)limitedHinge.perpAxisInA2, s, info);
			Nif.NifStream(out (Vector4)limitedHinge.pivotA, s, info);
			Nif.NifStream(out (Vector4)limitedHinge.axisB, s, info);
			Nif.NifStream(out limitedHinge.perpAxisInB1, s, info);
			Nif.NifStream(out (Vector4)limitedHinge.perpAxisInB2, s, info);
			Nif.NifStream(out (Vector4)limitedHinge.pivotB, s, info);
		}
		Nif.NifStream(out limitedHinge.minAngle, s, info);
		Nif.NifStream(out limitedHinge.maxAngle, s, info);
		Nif.NifStream(out limitedHinge.maxFriction, s, info);
		if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
			Nif.NifStream(out limitedHinge.motor.type, s, info);
			if ((limitedHinge.motor.type == 1)) {
				Nif.NifStream(out limitedHinge.motor.positionMotor.minForce, s, info);
				Nif.NifStream(out limitedHinge.motor.positionMotor.maxForce, s, info);
				Nif.NifStream(out limitedHinge.motor.positionMotor.tau, s, info);
				Nif.NifStream(out limitedHinge.motor.positionMotor.damping, s, info);
				Nif.NifStream(out limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
				Nif.NifStream(out limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
				Nif.NifStream(out limitedHinge.motor.positionMotor.motorEnabled, s, info);
			}
			if ((limitedHinge.motor.type == 2)) {
				Nif.NifStream(out limitedHinge.motor.velocityMotor.minForce, s, info);
				Nif.NifStream(out limitedHinge.motor.velocityMotor.maxForce, s, info);
				Nif.NifStream(out limitedHinge.motor.velocityMotor.tau, s, info);
				Nif.NifStream(out limitedHinge.motor.velocityMotor.targetVelocity, s, info);
				Nif.NifStream(out limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
				Nif.NifStream(out limitedHinge.motor.velocityMotor.motorEnabled, s, info);
			}
			if ((limitedHinge.motor.type == 3)) {
				Nif.NifStream(out limitedHinge.motor.springDamperMotor.minForce, s, info);
				Nif.NifStream(out limitedHinge.motor.springDamperMotor.maxForce, s, info);
				Nif.NifStream(out limitedHinge.motor.springDamperMotor.springConstant, s, info);
				Nif.NifStream(out limitedHinge.motor.springDamperMotor.springDamping, s, info);
				Nif.NifStream(out limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
			}
		}

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		if ((info.userVersion2 <= 16)) {
			Nif.NifStream(limitedHinge.pivotA, s, info);
			Nif.NifStream(limitedHinge.axisA, s, info);
			Nif.NifStream(limitedHinge.perpAxisInA1, s, info);
			Nif.NifStream(limitedHinge.perpAxisInA2, s, info);
			Nif.NifStream(limitedHinge.pivotB, s, info);
			Nif.NifStream(limitedHinge.axisB, s, info);
			Nif.NifStream(limitedHinge.perpAxisInB2, s, info);
		}
		if ((info.userVersion2 > 16)) {
			Nif.NifStream((Vector4)limitedHinge.axisA, s, info);
			Nif.NifStream((Vector4)limitedHinge.perpAxisInA1, s, info);
			Nif.NifStream((Vector4)limitedHinge.perpAxisInA2, s, info);
			Nif.NifStream((Vector4)limitedHinge.pivotA, s, info);
			Nif.NifStream((Vector4)limitedHinge.axisB, s, info);
			Nif.NifStream(limitedHinge.perpAxisInB1, s, info);
			Nif.NifStream((Vector4)limitedHinge.perpAxisInB2, s, info);
			Nif.NifStream((Vector4)limitedHinge.pivotB, s, info);
		}
		Nif.NifStream(limitedHinge.minAngle, s, info);
		Nif.NifStream(limitedHinge.maxAngle, s, info);
		Nif.NifStream(limitedHinge.maxFriction, s, info);
		if ((info.version >= 0x14020007) && ((info.userVersion2 > 16))) {
			Nif.NifStream(limitedHinge.motor.type, s, info);
			if ((limitedHinge.motor.type == 1)) {
				Nif.NifStream(limitedHinge.motor.positionMotor.minForce, s, info);
				Nif.NifStream(limitedHinge.motor.positionMotor.maxForce, s, info);
				Nif.NifStream(limitedHinge.motor.positionMotor.tau, s, info);
				Nif.NifStream(limitedHinge.motor.positionMotor.damping, s, info);
				Nif.NifStream(limitedHinge.motor.positionMotor.proportionalRecoveryVelocity, s, info);
				Nif.NifStream(limitedHinge.motor.positionMotor.constantRecoveryVelocity, s, info);
				Nif.NifStream(limitedHinge.motor.positionMotor.motorEnabled, s, info);
			}
			if ((limitedHinge.motor.type == 2)) {
				Nif.NifStream(limitedHinge.motor.velocityMotor.minForce, s, info);
				Nif.NifStream(limitedHinge.motor.velocityMotor.maxForce, s, info);
				Nif.NifStream(limitedHinge.motor.velocityMotor.tau, s, info);
				Nif.NifStream(limitedHinge.motor.velocityMotor.targetVelocity, s, info);
				Nif.NifStream(limitedHinge.motor.velocityMotor.useVelocityTarget, s, info);
				Nif.NifStream(limitedHinge.motor.velocityMotor.motorEnabled, s, info);
			}
			if ((limitedHinge.motor.type == 3)) {
				Nif.NifStream(limitedHinge.motor.springDamperMotor.minForce, s, info);
				Nif.NifStream(limitedHinge.motor.springDamperMotor.maxForce, s, info);
				Nif.NifStream(limitedHinge.motor.springDamperMotor.springConstant, s, info);
				Nif.NifStream(limitedHinge.motor.springDamperMotor.springDamping, s, info);
				Nif.NifStream(limitedHinge.motor.springDamperMotor.motorEnabled, s, info);
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
		s.AppendLine($"    Pivot A:  {limitedHinge.pivotA}");
		s.AppendLine($"    Axis A:  {limitedHinge.axisA}");
		s.AppendLine($"    Perp Axis In A1:  {limitedHinge.perpAxisInA1}");
		s.AppendLine($"    Perp Axis In A2:  {limitedHinge.perpAxisInA2}");
		s.AppendLine($"    Pivot B:  {limitedHinge.pivotB}");
		s.AppendLine($"    Axis B:  {limitedHinge.axisB}");
		s.AppendLine($"    Perp Axis In B2:  {limitedHinge.perpAxisInB2}");
		s.AppendLine($"    Perp Axis In B1:  {limitedHinge.perpAxisInB1}");
		s.AppendLine($"    Min Angle:  {limitedHinge.minAngle}");
		s.AppendLine($"    Max Angle:  {limitedHinge.maxAngle}");
		s.AppendLine($"    Max Friction:  {limitedHinge.maxFriction}");
		s.AppendLine($"    Type:  {limitedHinge.motor.type}");
		if ((limitedHinge.motor.type == 1)) {
			s.AppendLine($"      Min Force:  {limitedHinge.motor.positionMotor.minForce}");
			s.AppendLine($"      Max Force:  {limitedHinge.motor.positionMotor.maxForce}");
			s.AppendLine($"      Tau:  {limitedHinge.motor.positionMotor.tau}");
			s.AppendLine($"      Damping:  {limitedHinge.motor.positionMotor.damping}");
			s.AppendLine($"      Proportional Recovery Velocity:  {limitedHinge.motor.positionMotor.proportionalRecoveryVelocity}");
			s.AppendLine($"      Constant Recovery Velocity:  {limitedHinge.motor.positionMotor.constantRecoveryVelocity}");
			s.AppendLine($"      Motor Enabled:  {limitedHinge.motor.positionMotor.motorEnabled}");
		}
		if ((limitedHinge.motor.type == 2)) {
			s.AppendLine($"      Min Force:  {limitedHinge.motor.velocityMotor.minForce}");
			s.AppendLine($"      Max Force:  {limitedHinge.motor.velocityMotor.maxForce}");
			s.AppendLine($"      Tau:  {limitedHinge.motor.velocityMotor.tau}");
			s.AppendLine($"      Target Velocity:  {limitedHinge.motor.velocityMotor.targetVelocity}");
			s.AppendLine($"      Use Velocity Target:  {limitedHinge.motor.velocityMotor.useVelocityTarget}");
			s.AppendLine($"      Motor Enabled:  {limitedHinge.motor.velocityMotor.motorEnabled}");
		}
		if ((limitedHinge.motor.type == 3)) {
			s.AppendLine($"      Min Force:  {limitedHinge.motor.springDamperMotor.minForce}");
			s.AppendLine($"      Max Force:  {limitedHinge.motor.springDamperMotor.maxForce}");
			s.AppendLine($"      Spring Constant:  {limitedHinge.motor.springDamperMotor.springConstant}");
			s.AppendLine($"      Spring Damping:  {limitedHinge.motor.springDamperMotor.springDamping}");
			s.AppendLine($"      Motor Enabled:  {limitedHinge.motor.springDamperMotor.motorEnabled}");
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