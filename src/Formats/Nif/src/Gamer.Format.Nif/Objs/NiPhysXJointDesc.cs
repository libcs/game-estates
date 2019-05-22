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

/*! A PhysX Joint abstract base class. */
public class NiPhysXJointDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXJointDesc", NiObject.TYPE);
	/*!  */
	internal NxJointType jointType;
	/*!  */
	internal IndexString jointName;
	/*!  */
	internal Array2<NiPhysXJointActor> actors;
	/*!  */
	internal float maxForce;
	/*!  */
	internal float maxTorque;
	/*!  */
	internal float solverExtrapolationFactor;
	/*!  */
	internal uint useAccelerationSpring;
	/*!  */
	internal uint jointFlags;
	/*!  */
	internal Vector3 limitPoint;
	/*!  */
	internal uint numLimits;
	/*!  */
	internal IList<NiPhysXJointLimit> limits;

	public NiPhysXJointDesc() {
	jointType = (NxJointType)0;
	maxForce = 0.0f;
	maxTorque = 0.0f;
	solverExtrapolationFactor = 0.0f;
	useAccelerationSpring = (uint)0;
	jointFlags = (uint)0;
	numLimits = (uint)0;
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
public static NiObject Create() => new NiPhysXJointDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out jointType, s, info);
	Nif.NifStream(out jointName, s, info);
	for (var i1 = 0; i1 < 2; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out actors[i1].localNormal, s, info);
		Nif.NifStream(out actors[i1].localAxis, s, info);
		Nif.NifStream(out actors[i1].localAnchor, s, info);
	}
	Nif.NifStream(out maxForce, s, info);
	Nif.NifStream(out maxTorque, s, info);
	if (info.version >= 0x14050003) {
		Nif.NifStream(out solverExtrapolationFactor, s, info);
		Nif.NifStream(out useAccelerationSpring, s, info);
	}
	Nif.NifStream(out jointFlags, s, info);
	Nif.NifStream(out limitPoint, s, info);
	Nif.NifStream(out numLimits, s, info);
	limits = new NiPhysXJointLimit[numLimits];
	for (var i1 = 0; i1 < limits.Count; i1++) {
		Nif.NifStream(out limits[i1].limitPlaneNormal, s, info);
		Nif.NifStream(out limits[i1].limitPlaneD, s, info);
		if (info.version >= 0x14040000) {
			Nif.NifStream(out limits[i1].limitPlaneR, s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numLimits = (uint)limits.Count;
	Nif.NifStream(jointType, s, info);
	Nif.NifStream(jointName, s, info);
	for (var i1 = 0; i1 < 2; i1++) {
		WriteRef((NiObject)actors[i1].actor, s, info, link_map, missing_link_stack);
		Nif.NifStream(actors[i1].localNormal, s, info);
		Nif.NifStream(actors[i1].localAxis, s, info);
		Nif.NifStream(actors[i1].localAnchor, s, info);
	}
	Nif.NifStream(maxForce, s, info);
	Nif.NifStream(maxTorque, s, info);
	if (info.version >= 0x14050003) {
		Nif.NifStream(solverExtrapolationFactor, s, info);
		Nif.NifStream(useAccelerationSpring, s, info);
	}
	Nif.NifStream(jointFlags, s, info);
	Nif.NifStream(limitPoint, s, info);
	Nif.NifStream(numLimits, s, info);
	for (var i1 = 0; i1 < limits.Count; i1++) {
		Nif.NifStream(limits[i1].limitPlaneNormal, s, info);
		Nif.NifStream(limits[i1].limitPlaneD, s, info);
		if (info.version >= 0x14040000) {
			Nif.NifStream(limits[i1].limitPlaneR, s, info);
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
	uint array_output_count = 0;
	s.Append(base.AsString());
	numLimits = (uint)limits.Count;
	s.AppendLine($"  Joint Type:  {jointType}");
	s.AppendLine($"  Joint Name:  {jointName}");
	array_output_count = 0;
	for (var i1 = 0; i1 < 2; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Actor:  {actors[i1].actor}");
		s.AppendLine($"    Local Normal:  {actors[i1].localNormal}");
		s.AppendLine($"    Local Axis:  {actors[i1].localAxis}");
		s.AppendLine($"    Local Anchor:  {actors[i1].localAnchor}");
	}
	s.AppendLine($"  Max Force:  {maxForce}");
	s.AppendLine($"  Max Torque:  {maxTorque}");
	s.AppendLine($"  Solver Extrapolation Factor:  {solverExtrapolationFactor}");
	s.AppendLine($"  Use Acceleration Spring:  {useAccelerationSpring}");
	s.AppendLine($"  Joint Flags:  {jointFlags}");
	s.AppendLine($"  Limit Point:  {limitPoint}");
	s.AppendLine($"  Num Limits:  {numLimits}");
	array_output_count = 0;
	for (var i1 = 0; i1 < limits.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Limit Plane Normal:  {limits[i1].limitPlaneNormal}");
		s.AppendLine($"    Limit Plane D:  {limits[i1].limitPlaneD}");
		s.AppendLine($"    Limit Plane R:  {limits[i1].limitPlaneR}");
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < 2; i1++) {
		actors[i1].actor = FixLink<NiPhysXActorDesc>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < 2; i1++) {
		if (actors[i1].actor != null)
			refs.Add((NiObject)actors[i1].actor);
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < 2; i1++) {
	}
	return ptrs;
}


}

}