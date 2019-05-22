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

/*! A Ball and Socket Constraint chain. */
public class bhkBallSocketConstraintChain : bhkSerializable {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("bhkBallSocketConstraintChain", bhkSerializable.TYPE);
	/*! Number of pivot points. Divide by 2 to get the number of constraints. */
	internal uint numPivots;
	/*! Two pivot points A and B for each constraint. */
	internal IList<ConstraintInfo> pivots;
	/*! High values are harder and more reactive, lower values are smoother. */
	internal float tau;
	/*! Defines damping strength for the current velocity. */
	internal float damping;
	/*!
	 * Restitution (amount of elasticity) of constraints. Added to the diagonal of the
	 * constraint matrix. A value of 0.0 can result in a division by zero with some
	 * chain configurations.
	 */
	internal float constraintForceMixing;
	/*!
	 * Maximum distance error in constraints allowed before stabilization algorithm
	 * kicks in. A smaller distance causes more resistance.
	 */
	internal float maxErrorDistance;
	/*! Number of links in the chain */
	internal uint numEntitiesA;
	/*!  */
	internal IList<bhkRigidBody> entitiesA;
	/*! Hardcoded to 2. Don't change. */
	internal uint numEntities;
	/*!  */
	internal bhkRigidBody entityA;
	/*!  */
	internal bhkRigidBody entityB;
	/*!  */
	internal uint priority;

	public bhkBallSocketConstraintChain() {
	numPivots = (uint)0;
	tau = 1.0f;
	damping = 0.6f;
	constraintForceMixing = 1.1920929e-08f;
	maxErrorDistance = 0.1f;
	numEntitiesA = (uint)0;
	numEntities = (uint)2;
	entityA = null;
	entityB = null;
	priority = (uint)0;
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
public static NiObject Create() => new bhkBallSocketConstraintChain();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out numPivots, s, info);
	pivots = new ConstraintInfo[(numPivots / 2)];
	for (var i1 = 0; i1 < pivots.Count; i1++) {
		Nif.NifStream(out pivots[i1].pivotInA, s, info);
		Nif.NifStream(out pivots[i1].pivotInB, s, info);
	}
	Nif.NifStream(out tau, s, info);
	Nif.NifStream(out damping, s, info);
	Nif.NifStream(out constraintForceMixing, s, info);
	Nif.NifStream(out maxErrorDistance, s, info);
	Nif.NifStream(out numEntitiesA, s, info);
	entitiesA = new *[numEntitiesA];
	for (var i1 = 0; i1 < entitiesA.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numEntities, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out priority, s, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numEntitiesA = (uint)entitiesA.Count;
	numPivots = (uint)pivots.Count;
	Nif.NifStream(numPivots, s, info);
	for (var i1 = 0; i1 < pivots.Count; i1++) {
		Nif.NifStream(pivots[i1].pivotInA, s, info);
		Nif.NifStream(pivots[i1].pivotInB, s, info);
	}
	Nif.NifStream(tau, s, info);
	Nif.NifStream(damping, s, info);
	Nif.NifStream(constraintForceMixing, s, info);
	Nif.NifStream(maxErrorDistance, s, info);
	Nif.NifStream(numEntitiesA, s, info);
	for (var i1 = 0; i1 < entitiesA.Count; i1++) {
		WriteRef((NiObject)entitiesA[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numEntities, s, info);
	WriteRef((NiObject)entityA, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)entityB, s, info, link_map, missing_link_stack);
	Nif.NifStream(priority, s, info);

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
	numEntitiesA = (uint)entitiesA.Count;
	numPivots = (uint)pivots.Count;
	s.AppendLine($"  Num Pivots:  {numPivots}");
	array_output_count = 0;
	for (var i1 = 0; i1 < pivots.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Pivot In A:  {pivots[i1].pivotInA}");
		s.AppendLine($"    Pivot In B:  {pivots[i1].pivotInB}");
	}
	s.AppendLine($"  Tau:  {tau}");
	s.AppendLine($"  Damping:  {damping}");
	s.AppendLine($"  Constraint Force Mixing:  {constraintForceMixing}");
	s.AppendLine($"  Max Error Distance:  {maxErrorDistance}");
	s.AppendLine($"  Num Entities A:  {numEntitiesA}");
	array_output_count = 0;
	for (var i1 = 0; i1 < entitiesA.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Entities A[{i1}]:  {entitiesA[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Entities:  {numEntities}");
	s.AppendLine($"  Entity A:  {entityA}");
	s.AppendLine($"  Entity B:  {entityB}");
	s.AppendLine($"  Priority:  {priority}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < entitiesA.Count; i1++) {
		entitiesA[i1] = FixLink<bhkRigidBody>(objects, link_stack, missing_link_stack, info);
	}
	entityA = FixLink<bhkRigidBody>(objects, link_stack, missing_link_stack, info);
	entityB = FixLink<bhkRigidBody>(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < entitiesA.Count; i1++) {
	}
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < entitiesA.Count; i1++) {
		if (entitiesA[i1] != null)
			ptrs.Add((NiObject)entitiesA[i1]);
	}
	if (entityA != null)
		ptrs.Add((NiObject)entityA);
	if (entityB != null)
		ptrs.Add((NiObject)entityB);
	return ptrs;
}


}

}