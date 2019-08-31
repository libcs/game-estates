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
	/*! A 6DOF (6 degrees of freedom) joint. */
	public class NiPhysXD6JointDesc : NiPhysXJointDesc
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPhysXD6JointDesc", NiPhysXJointDesc.TYPE);

		/*! xMotion */
		internal NxD6JointMotion xMotion;
		/*! yMotion */
		internal NxD6JointMotion yMotion;
		/*! zMotion */
		internal NxD6JointMotion zMotion;
		/*! swing1Motion */
		internal NxD6JointMotion swing1Motion;
		/*! swing2Motion */
		internal NxD6JointMotion swing2Motion;
		/*! twistMotion */
		internal NxD6JointMotion twistMotion;
		/*! linearLimit */
		internal NxJointLimitSoftDesc linearLimit;
		/*! swing1Limit */
		internal NxJointLimitSoftDesc swing1Limit;
		/*! swing2Limit */
		internal NxJointLimitSoftDesc swing2Limit;
		/*! twistLowLimit */
		internal NxJointLimitSoftDesc twistLowLimit;
		/*! twistHighLimit */
		internal NxJointLimitSoftDesc twistHighLimit;
		/*! xDrive */
		internal NxJointDriveDesc xDrive;
		/*! yDrive */
		internal NxJointDriveDesc yDrive;
		/*! zDrive */
		internal NxJointDriveDesc zDrive;
		/*! swingDrive */
		internal NxJointDriveDesc swingDrive;
		/*! twistDrive */
		internal NxJointDriveDesc twistDrive;
		/*! slerpDrive */
		internal NxJointDriveDesc slerpDrive;
		/*! drivePosition */
		internal Vector3 drivePosition;
		/*! driveOrientation */
		internal Quaternion driveOrientation;
		/*! driveLinearVelocity */
		internal Vector3 driveLinearVelocity;
		/*! driveAngularVelocity */
		internal Vector3 driveAngularVelocity;
		/*! projectionMode */
		internal NxJointProjectionMode projectionMode;
		/*! projectionDistance */
		internal float projectionDistance;
		/*! projectionAngle */
		internal float projectionAngle;
		/*! gearRatio */
		internal float gearRatio;
		/*! flags */
		internal uint flags;
		public NiPhysXD6JointDesc()
		{
			xMotion = (NxD6JointMotion)0;
			yMotion = (NxD6JointMotion)0;
			zMotion = (NxD6JointMotion)0;
			swing1Motion = (NxD6JointMotion)0;
			swing2Motion = (NxD6JointMotion)0;
			twistMotion = (NxD6JointMotion)0;
			projectionMode = (NxJointProjectionMode)0;
			projectionDistance = 0.0f;
			projectionAngle = 0.0f;
			gearRatio = 0.0f;
			flags = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPhysXD6JointDesc();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out xMotion, s, info);
			Nif.NifStream(out yMotion, s, info);
			Nif.NifStream(out zMotion, s, info);
			Nif.NifStream(out swing1Motion, s, info);
			Nif.NifStream(out swing2Motion, s, info);
			Nif.NifStream(out twistMotion, s, info);
			Nif.NifStream(out linearLimit.value, s, info);
			Nif.NifStream(out linearLimit.restitution, s, info);
			Nif.NifStream(out linearLimit.spring, s, info);
			Nif.NifStream(out linearLimit.damping, s, info);
			Nif.NifStream(out swing1Limit.value, s, info);
			Nif.NifStream(out swing1Limit.restitution, s, info);
			Nif.NifStream(out swing1Limit.spring, s, info);
			Nif.NifStream(out swing1Limit.damping, s, info);
			Nif.NifStream(out swing2Limit.value, s, info);
			Nif.NifStream(out swing2Limit.restitution, s, info);
			Nif.NifStream(out swing2Limit.spring, s, info);
			Nif.NifStream(out swing2Limit.damping, s, info);
			Nif.NifStream(out twistLowLimit.value, s, info);
			Nif.NifStream(out twistLowLimit.restitution, s, info);
			Nif.NifStream(out twistLowLimit.spring, s, info);
			Nif.NifStream(out twistLowLimit.damping, s, info);
			Nif.NifStream(out twistHighLimit.value, s, info);
			Nif.NifStream(out twistHighLimit.restitution, s, info);
			Nif.NifStream(out twistHighLimit.spring, s, info);
			Nif.NifStream(out twistHighLimit.damping, s, info);
			Nif.NifStream(out xDrive.driveType, s, info);
			Nif.NifStream(out xDrive.restitution, s, info);
			Nif.NifStream(out xDrive.spring, s, info);
			Nif.NifStream(out xDrive.damping, s, info);
			Nif.NifStream(out yDrive.driveType, s, info);
			Nif.NifStream(out yDrive.restitution, s, info);
			Nif.NifStream(out yDrive.spring, s, info);
			Nif.NifStream(out yDrive.damping, s, info);
			Nif.NifStream(out zDrive.driveType, s, info);
			Nif.NifStream(out zDrive.restitution, s, info);
			Nif.NifStream(out zDrive.spring, s, info);
			Nif.NifStream(out zDrive.damping, s, info);
			Nif.NifStream(out swingDrive.driveType, s, info);
			Nif.NifStream(out swingDrive.restitution, s, info);
			Nif.NifStream(out swingDrive.spring, s, info);
			Nif.NifStream(out swingDrive.damping, s, info);
			Nif.NifStream(out twistDrive.driveType, s, info);
			Nif.NifStream(out twistDrive.restitution, s, info);
			Nif.NifStream(out twistDrive.spring, s, info);
			Nif.NifStream(out twistDrive.damping, s, info);
			Nif.NifStream(out slerpDrive.driveType, s, info);
			Nif.NifStream(out slerpDrive.restitution, s, info);
			Nif.NifStream(out slerpDrive.spring, s, info);
			Nif.NifStream(out slerpDrive.damping, s, info);
			Nif.NifStream(out drivePosition, s, info);
			Nif.NifStream(out driveOrientation, s, info);
			Nif.NifStream(out driveLinearVelocity, s, info);
			Nif.NifStream(out driveAngularVelocity, s, info);
			Nif.NifStream(out projectionMode, s, info);
			Nif.NifStream(out projectionDistance, s, info);
			Nif.NifStream(out projectionAngle, s, info);
			Nif.NifStream(out gearRatio, s, info);
			Nif.NifStream(out flags, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(xMotion, s, info);
			Nif.NifStream(yMotion, s, info);
			Nif.NifStream(zMotion, s, info);
			Nif.NifStream(swing1Motion, s, info);
			Nif.NifStream(swing2Motion, s, info);
			Nif.NifStream(twistMotion, s, info);
			Nif.NifStream(linearLimit.value, s, info);
			Nif.NifStream(linearLimit.restitution, s, info);
			Nif.NifStream(linearLimit.spring, s, info);
			Nif.NifStream(linearLimit.damping, s, info);
			Nif.NifStream(swing1Limit.value, s, info);
			Nif.NifStream(swing1Limit.restitution, s, info);
			Nif.NifStream(swing1Limit.spring, s, info);
			Nif.NifStream(swing1Limit.damping, s, info);
			Nif.NifStream(swing2Limit.value, s, info);
			Nif.NifStream(swing2Limit.restitution, s, info);
			Nif.NifStream(swing2Limit.spring, s, info);
			Nif.NifStream(swing2Limit.damping, s, info);
			Nif.NifStream(twistLowLimit.value, s, info);
			Nif.NifStream(twistLowLimit.restitution, s, info);
			Nif.NifStream(twistLowLimit.spring, s, info);
			Nif.NifStream(twistLowLimit.damping, s, info);
			Nif.NifStream(twistHighLimit.value, s, info);
			Nif.NifStream(twistHighLimit.restitution, s, info);
			Nif.NifStream(twistHighLimit.spring, s, info);
			Nif.NifStream(twistHighLimit.damping, s, info);
			Nif.NifStream(xDrive.driveType, s, info);
			Nif.NifStream(xDrive.restitution, s, info);
			Nif.NifStream(xDrive.spring, s, info);
			Nif.NifStream(xDrive.damping, s, info);
			Nif.NifStream(yDrive.driveType, s, info);
			Nif.NifStream(yDrive.restitution, s, info);
			Nif.NifStream(yDrive.spring, s, info);
			Nif.NifStream(yDrive.damping, s, info);
			Nif.NifStream(zDrive.driveType, s, info);
			Nif.NifStream(zDrive.restitution, s, info);
			Nif.NifStream(zDrive.spring, s, info);
			Nif.NifStream(zDrive.damping, s, info);
			Nif.NifStream(swingDrive.driveType, s, info);
			Nif.NifStream(swingDrive.restitution, s, info);
			Nif.NifStream(swingDrive.spring, s, info);
			Nif.NifStream(swingDrive.damping, s, info);
			Nif.NifStream(twistDrive.driveType, s, info);
			Nif.NifStream(twistDrive.restitution, s, info);
			Nif.NifStream(twistDrive.spring, s, info);
			Nif.NifStream(twistDrive.damping, s, info);
			Nif.NifStream(slerpDrive.driveType, s, info);
			Nif.NifStream(slerpDrive.restitution, s, info);
			Nif.NifStream(slerpDrive.spring, s, info);
			Nif.NifStream(slerpDrive.damping, s, info);
			Nif.NifStream(drivePosition, s, info);
			Nif.NifStream(driveOrientation, s, info);
			Nif.NifStream(driveLinearVelocity, s, info);
			Nif.NifStream(driveAngularVelocity, s, info);
			Nif.NifStream(projectionMode, s, info);
			Nif.NifStream(projectionDistance, s, info);
			Nif.NifStream(projectionAngle, s, info);
			Nif.NifStream(gearRatio, s, info);
			Nif.NifStream(flags, s, info);
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
			s.AppendLine($"      X Motion:  {xMotion}");
			s.AppendLine($"      Y Motion:  {yMotion}");
			s.AppendLine($"      Z Motion:  {zMotion}");
			s.AppendLine($"      Swing 1 Motion:  {swing1Motion}");
			s.AppendLine($"      Swing 2 Motion:  {swing2Motion}");
			s.AppendLine($"      Twist Motion:  {twistMotion}");
			s.AppendLine($"      Value:  {linearLimit.value}");
			s.AppendLine($"      Restitution:  {linearLimit.restitution}");
			s.AppendLine($"      Spring:  {linearLimit.spring}");
			s.AppendLine($"      Damping:  {linearLimit.damping}");
			s.AppendLine($"      Value:  {swing1Limit.value}");
			s.AppendLine($"      Restitution:  {swing1Limit.restitution}");
			s.AppendLine($"      Spring:  {swing1Limit.spring}");
			s.AppendLine($"      Damping:  {swing1Limit.damping}");
			s.AppendLine($"      Value:  {swing2Limit.value}");
			s.AppendLine($"      Restitution:  {swing2Limit.restitution}");
			s.AppendLine($"      Spring:  {swing2Limit.spring}");
			s.AppendLine($"      Damping:  {swing2Limit.damping}");
			s.AppendLine($"      Value:  {twistLowLimit.value}");
			s.AppendLine($"      Restitution:  {twistLowLimit.restitution}");
			s.AppendLine($"      Spring:  {twistLowLimit.spring}");
			s.AppendLine($"      Damping:  {twistLowLimit.damping}");
			s.AppendLine($"      Value:  {twistHighLimit.value}");
			s.AppendLine($"      Restitution:  {twistHighLimit.restitution}");
			s.AppendLine($"      Spring:  {twistHighLimit.spring}");
			s.AppendLine($"      Damping:  {twistHighLimit.damping}");
			s.AppendLine($"      Drive Type:  {xDrive.driveType}");
			s.AppendLine($"      Restitution:  {xDrive.restitution}");
			s.AppendLine($"      Spring:  {xDrive.spring}");
			s.AppendLine($"      Damping:  {xDrive.damping}");
			s.AppendLine($"      Drive Type:  {yDrive.driveType}");
			s.AppendLine($"      Restitution:  {yDrive.restitution}");
			s.AppendLine($"      Spring:  {yDrive.spring}");
			s.AppendLine($"      Damping:  {yDrive.damping}");
			s.AppendLine($"      Drive Type:  {zDrive.driveType}");
			s.AppendLine($"      Restitution:  {zDrive.restitution}");
			s.AppendLine($"      Spring:  {zDrive.spring}");
			s.AppendLine($"      Damping:  {zDrive.damping}");
			s.AppendLine($"      Drive Type:  {swingDrive.driveType}");
			s.AppendLine($"      Restitution:  {swingDrive.restitution}");
			s.AppendLine($"      Spring:  {swingDrive.spring}");
			s.AppendLine($"      Damping:  {swingDrive.damping}");
			s.AppendLine($"      Drive Type:  {twistDrive.driveType}");
			s.AppendLine($"      Restitution:  {twistDrive.restitution}");
			s.AppendLine($"      Spring:  {twistDrive.spring}");
			s.AppendLine($"      Damping:  {twistDrive.damping}");
			s.AppendLine($"      Drive Type:  {slerpDrive.driveType}");
			s.AppendLine($"      Restitution:  {slerpDrive.restitution}");
			s.AppendLine($"      Spring:  {slerpDrive.spring}");
			s.AppendLine($"      Damping:  {slerpDrive.damping}");
			s.AppendLine($"      Drive Position:  {drivePosition}");
			s.AppendLine($"      Drive Orientation:  {driveOrientation}");
			s.AppendLine($"      Drive Linear Velocity:  {driveLinearVelocity}");
			s.AppendLine($"      Drive Angular Velocity:  {driveAngularVelocity}");
			s.AppendLine($"      Projection Mode:  {projectionMode}");
			s.AppendLine($"      Projection Distance:  {projectionDistance}");
			s.AppendLine($"      Projection Angle:  {projectionAngle}");
			s.AppendLine($"      Gear Ratio:  {gearRatio}");
			s.AppendLine($"      Flags:  {flags}");
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
