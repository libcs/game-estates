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
	/*! Ragdoll constraint. */
	public class bhkRagdollConstraint : bhkConstraint
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkRagdollConstraint", bhkConstraint.TYPE);

		/*! Ragdoll constraint. */
		internal RagdollDescriptor ragdoll;
		public bhkRagdollConstraint()
		{
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkRagdollConstraint();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if ((info.userVersion2 <= 16))
			{
				Nif.NifStream(out ragdoll.pivotA, s, info);
				Nif.NifStream(out ragdoll.planeA, s, info);
				Nif.NifStream(out ragdoll.twistA, s, info);
				Nif.NifStream(out ragdoll.pivotB, s, info);
				Nif.NifStream(out ragdoll.planeB, s, info);
				Nif.NifStream(out ragdoll.twistB, s, info);
			}
			if ((info.userVersion2 > 16))
			{
				Nif.NifStream(out (Vector4)ragdoll.twistA, s, info);
				Nif.NifStream(out (Vector4)ragdoll.planeA, s, info);
				Nif.NifStream(out ragdoll.motorA, s, info);
				Nif.NifStream(out (Vector4)ragdoll.pivotA, s, info);
				Nif.NifStream(out (Vector4)ragdoll.twistB, s, info);
				Nif.NifStream(out (Vector4)ragdoll.planeB, s, info);
				Nif.NifStream(out ragdoll.motorB, s, info);
				Nif.NifStream(out (Vector4)ragdoll.pivotB, s, info);
			}
			Nif.NifStream(out ragdoll.coneMaxAngle, s, info);
			Nif.NifStream(out ragdoll.planeMinAngle, s, info);
			Nif.NifStream(out ragdoll.planeMaxAngle, s, info);
			Nif.NifStream(out ragdoll.twistMinAngle, s, info);
			Nif.NifStream(out ragdoll.twistMaxAngle, s, info);
			Nif.NifStream(out ragdoll.maxFriction, s, info);
			if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
			{
				Nif.NifStream(out ragdoll.motor.type, s, info);
				if ((ragdoll.motor.type == 1))
				{
					Nif.NifStream(out ragdoll.motor.positionMotor.minForce, s, info);
					Nif.NifStream(out ragdoll.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(out ragdoll.motor.positionMotor.tau, s, info);
					Nif.NifStream(out ragdoll.motor.positionMotor.damping, s, info);
					Nif.NifStream(out ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(out ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(out ragdoll.motor.positionMotor.motorEnabled, s, info);
				}
				if ((ragdoll.motor.type == 2))
				{
					Nif.NifStream(out ragdoll.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(out ragdoll.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(out ragdoll.motor.velocityMotor.tau, s, info);
					Nif.NifStream(out ragdoll.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(out ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(out ragdoll.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((ragdoll.motor.type == 3))
				{
					Nif.NifStream(out ragdoll.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(out ragdoll.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(out ragdoll.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(out ragdoll.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(out ragdoll.motor.springDamperMotor.motorEnabled, s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if ((info.userVersion2 <= 16))
			{
				Nif.NifStream(ragdoll.pivotA, s, info);
				Nif.NifStream(ragdoll.planeA, s, info);
				Nif.NifStream(ragdoll.twistA, s, info);
				Nif.NifStream(ragdoll.pivotB, s, info);
				Nif.NifStream(ragdoll.planeB, s, info);
				Nif.NifStream(ragdoll.twistB, s, info);
			}
			if ((info.userVersion2 > 16))
			{
				Nif.NifStream((Vector4)ragdoll.twistA, s, info);
				Nif.NifStream((Vector4)ragdoll.planeA, s, info);
				Nif.NifStream(ragdoll.motorA, s, info);
				Nif.NifStream((Vector4)ragdoll.pivotA, s, info);
				Nif.NifStream((Vector4)ragdoll.twistB, s, info);
				Nif.NifStream((Vector4)ragdoll.planeB, s, info);
				Nif.NifStream(ragdoll.motorB, s, info);
				Nif.NifStream((Vector4)ragdoll.pivotB, s, info);
			}
			Nif.NifStream(ragdoll.coneMaxAngle, s, info);
			Nif.NifStream(ragdoll.planeMinAngle, s, info);
			Nif.NifStream(ragdoll.planeMaxAngle, s, info);
			Nif.NifStream(ragdoll.twistMinAngle, s, info);
			Nif.NifStream(ragdoll.twistMaxAngle, s, info);
			Nif.NifStream(ragdoll.maxFriction, s, info);
			if (info.version >= 0x14020007 && ((info.userVersion2 > 16)))
			{
				Nif.NifStream(ragdoll.motor.type, s, info);
				if ((ragdoll.motor.type == 1))
				{
					Nif.NifStream(ragdoll.motor.positionMotor.minForce, s, info);
					Nif.NifStream(ragdoll.motor.positionMotor.maxForce, s, info);
					Nif.NifStream(ragdoll.motor.positionMotor.tau, s, info);
					Nif.NifStream(ragdoll.motor.positionMotor.damping, s, info);
					Nif.NifStream(ragdoll.motor.positionMotor.proportionalRecoveryVelocity, s, info);
					Nif.NifStream(ragdoll.motor.positionMotor.constantRecoveryVelocity, s, info);
					Nif.NifStream(ragdoll.motor.positionMotor.motorEnabled, s, info);
				}
				if ((ragdoll.motor.type == 2))
				{
					Nif.NifStream(ragdoll.motor.velocityMotor.minForce, s, info);
					Nif.NifStream(ragdoll.motor.velocityMotor.maxForce, s, info);
					Nif.NifStream(ragdoll.motor.velocityMotor.tau, s, info);
					Nif.NifStream(ragdoll.motor.velocityMotor.targetVelocity, s, info);
					Nif.NifStream(ragdoll.motor.velocityMotor.useVelocityTarget, s, info);
					Nif.NifStream(ragdoll.motor.velocityMotor.motorEnabled, s, info);
				}
				if ((ragdoll.motor.type == 3))
				{
					Nif.NifStream(ragdoll.motor.springDamperMotor.minForce, s, info);
					Nif.NifStream(ragdoll.motor.springDamperMotor.maxForce, s, info);
					Nif.NifStream(ragdoll.motor.springDamperMotor.springConstant, s, info);
					Nif.NifStream(ragdoll.motor.springDamperMotor.springDamping, s, info);
					Nif.NifStream(ragdoll.motor.springDamperMotor.motorEnabled, s, info);
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
			s.Append(base.AsString());
			s.AppendLine($"      Pivot A:  {ragdoll.pivotA}");
			s.AppendLine($"      Plane A:  {ragdoll.planeA}");
			s.AppendLine($"      Twist A:  {ragdoll.twistA}");
			s.AppendLine($"      Pivot B:  {ragdoll.pivotB}");
			s.AppendLine($"      Plane B:  {ragdoll.planeB}");
			s.AppendLine($"      Twist B:  {ragdoll.twistB}");
			s.AppendLine($"      Motor A:  {ragdoll.motorA}");
			s.AppendLine($"      Motor B:  {ragdoll.motorB}");
			s.AppendLine($"      Cone Max Angle:  {ragdoll.coneMaxAngle}");
			s.AppendLine($"      Plane Min Angle:  {ragdoll.planeMinAngle}");
			s.AppendLine($"      Plane Max Angle:  {ragdoll.planeMaxAngle}");
			s.AppendLine($"      Twist Min Angle:  {ragdoll.twistMinAngle}");
			s.AppendLine($"      Twist Max Angle:  {ragdoll.twistMaxAngle}");
			s.AppendLine($"      Max Friction:  {ragdoll.maxFriction}");
			s.AppendLine($"      Type:  {ragdoll.motor.type}");
			if ((ragdoll.motor.type == 1))
			{
				s.AppendLine($"        Min Force:  {ragdoll.motor.positionMotor.minForce}");
				s.AppendLine($"        Max Force:  {ragdoll.motor.positionMotor.maxForce}");
				s.AppendLine($"        Tau:  {ragdoll.motor.positionMotor.tau}");
				s.AppendLine($"        Damping:  {ragdoll.motor.positionMotor.damping}");
				s.AppendLine($"        Proportional Recovery Velocity:  {ragdoll.motor.positionMotor.proportionalRecoveryVelocity}");
				s.AppendLine($"        Constant Recovery Velocity:  {ragdoll.motor.positionMotor.constantRecoveryVelocity}");
				s.AppendLine($"        Motor Enabled:  {ragdoll.motor.positionMotor.motorEnabled}");
			}
			if ((ragdoll.motor.type == 2))
			{
				s.AppendLine($"        Min Force:  {ragdoll.motor.velocityMotor.minForce}");
				s.AppendLine($"        Max Force:  {ragdoll.motor.velocityMotor.maxForce}");
				s.AppendLine($"        Tau:  {ragdoll.motor.velocityMotor.tau}");
				s.AppendLine($"        Target Velocity:  {ragdoll.motor.velocityMotor.targetVelocity}");
				s.AppendLine($"        Use Velocity Target:  {ragdoll.motor.velocityMotor.useVelocityTarget}");
				s.AppendLine($"        Motor Enabled:  {ragdoll.motor.velocityMotor.motorEnabled}");
			}
			if ((ragdoll.motor.type == 3))
			{
				s.AppendLine($"        Min Force:  {ragdoll.motor.springDamperMotor.minForce}");
				s.AppendLine($"        Max Force:  {ragdoll.motor.springDamperMotor.maxForce}");
				s.AppendLine($"        Spring Constant:  {ragdoll.motor.springDamperMotor.springConstant}");
				s.AppendLine($"        Spring Damping:  {ragdoll.motor.springDamperMotor.springDamping}");
				s.AppendLine($"        Motor Enabled:  {ragdoll.motor.springDamperMotor.motorEnabled}");
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
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
			return ptrs;
		}
	}
}
