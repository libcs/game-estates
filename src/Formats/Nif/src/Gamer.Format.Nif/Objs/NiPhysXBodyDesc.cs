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

/*! For serializing NxBodyDesc objects. */
public class NiPhysXBodyDesc : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPhysXBodyDesc", NiObject.TYPE);
	/*!  */
	internal Matrix34 localPose;
	/*!  */
	internal Vector3 spaceInertia;
	/*!  */
	internal float mass;
	/*!  */
	internal uint numVels;
	/*!  */
	internal IList<PhysXBodyStoredVels> vels;
	/*!  */
	internal float wakeUpCounter;
	/*!  */
	internal float linearDamping;
	/*!  */
	internal float angularDamping;
	/*!  */
	internal float maxAngularVelocity;
	/*!  */
	internal float ccdMotionThreshold;
	/*!  */
	internal uint flags;
	/*!  */
	internal float sleepLinearVelocity;
	/*!  */
	internal float sleepAngularVelocity;
	/*!  */
	internal uint solverIterationCount;
	/*!  */
	internal float sleepEnergyThreshold;
	/*!  */
	internal float sleepDamping;
	/*!  */
	internal float contactReportThreshold;

	public NiPhysXBodyDesc() {
	mass = 0.0f;
	numVels = (uint)0;
	wakeUpCounter = 0.0f;
	linearDamping = 0.0f;
	angularDamping = 0.0f;
	maxAngularVelocity = 0.0f;
	ccdMotionThreshold = 0.0f;
	flags = (uint)0;
	sleepLinearVelocity = 0.0f;
	sleepAngularVelocity = 0.0f;
	solverIterationCount = (uint)0;
	sleepEnergyThreshold = 0.0f;
	sleepDamping = 0.0f;
	contactReportThreshold = 0.0f;
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
public static NiObject Create() => new NiPhysXBodyDesc();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out localPose, s, info);
	Nif.NifStream(out spaceInertia, s, info);
	Nif.NifStream(out mass, s, info);
	Nif.NifStream(out numVels, s, info);
	vels = new PhysXBodyStoredVels[numVels];
	for (var i1 = 0; i1 < vels.Count; i1++) {
		Nif.NifStream(out vels[i1].linearVelocity, s, info);
		Nif.NifStream(out vels[i1].angularVelocity, s, info);
		if (info.version >= 0x1E020003) {
			Nif.NifStream(out vels[i1].sleep, s, info);
		}
	}
	Nif.NifStream(out wakeUpCounter, s, info);
	Nif.NifStream(out linearDamping, s, info);
	Nif.NifStream(out angularDamping, s, info);
	Nif.NifStream(out maxAngularVelocity, s, info);
	Nif.NifStream(out ccdMotionThreshold, s, info);
	Nif.NifStream(out flags, s, info);
	Nif.NifStream(out sleepLinearVelocity, s, info);
	Nif.NifStream(out sleepAngularVelocity, s, info);
	Nif.NifStream(out solverIterationCount, s, info);
	if (info.version >= 0x14030000) {
		Nif.NifStream(out sleepEnergyThreshold, s, info);
		Nif.NifStream(out sleepDamping, s, info);
	}
	if (info.version >= 0x14040000) {
		Nif.NifStream(out contactReportThreshold, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numVels = (uint)vels.Count;
	Nif.NifStream(localPose, s, info);
	Nif.NifStream(spaceInertia, s, info);
	Nif.NifStream(mass, s, info);
	Nif.NifStream(numVels, s, info);
	for (var i1 = 0; i1 < vels.Count; i1++) {
		Nif.NifStream(vels[i1].linearVelocity, s, info);
		Nif.NifStream(vels[i1].angularVelocity, s, info);
		if (info.version >= 0x1E020003) {
			Nif.NifStream(vels[i1].sleep, s, info);
		}
	}
	Nif.NifStream(wakeUpCounter, s, info);
	Nif.NifStream(linearDamping, s, info);
	Nif.NifStream(angularDamping, s, info);
	Nif.NifStream(maxAngularVelocity, s, info);
	Nif.NifStream(ccdMotionThreshold, s, info);
	Nif.NifStream(flags, s, info);
	Nif.NifStream(sleepLinearVelocity, s, info);
	Nif.NifStream(sleepAngularVelocity, s, info);
	Nif.NifStream(solverIterationCount, s, info);
	if (info.version >= 0x14030000) {
		Nif.NifStream(sleepEnergyThreshold, s, info);
		Nif.NifStream(sleepDamping, s, info);
	}
	if (info.version >= 0x14040000) {
		Nif.NifStream(contactReportThreshold, s, info);
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
	numVels = (uint)vels.Count;
	s.AppendLine($"  Local Pose:  {localPose}");
	s.AppendLine($"  Space Inertia:  {spaceInertia}");
	s.AppendLine($"  Mass:  {mass}");
	s.AppendLine($"  Num Vels:  {numVels}");
	array_output_count = 0;
	for (var i1 = 0; i1 < vels.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Linear Velocity:  {vels[i1].linearVelocity}");
		s.AppendLine($"    Angular Velocity:  {vels[i1].angularVelocity}");
		s.AppendLine($"    Sleep:  {vels[i1].sleep}");
	}
	s.AppendLine($"  Wake Up Counter:  {wakeUpCounter}");
	s.AppendLine($"  Linear Damping:  {linearDamping}");
	s.AppendLine($"  Angular Damping:  {angularDamping}");
	s.AppendLine($"  Max Angular Velocity:  {maxAngularVelocity}");
	s.AppendLine($"  CCD Motion Threshold:  {ccdMotionThreshold}");
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Sleep Linear Velocity:  {sleepLinearVelocity}");
	s.AppendLine($"  Sleep Angular Velocity:  {sleepAngularVelocity}");
	s.AppendLine($"  Solver Iteration Count:  {solverIterationCount}");
	s.AppendLine($"  Sleep Energy Threshold:  {sleepEnergyThreshold}");
	s.AppendLine($"  Sleep Damping:  {sleepDamping}");
	s.AppendLine($"  Contact Report Threshold:  {contactReportThreshold}");
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