/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*!
 * This constraint defines a cone in which an object can rotate. The shape of the
 * cone can be controlled in two (orthogonal) directions.
 */
public class RagdollDescriptor {
	/*! The point where the constraint is attached to its parent rigidbody. */
	internal Vector4 pivotA;
	/*!
	 * Defines the orthogonal plane in which the body can move, the orthogonal
	 * directions in which the shape can be controlled (the direction orthogonal on
	 * this one and Twist A).
	 */
	internal Vector4 planeA;
	/*!
	 * Central directed axis of the cone in which the object can rotate. Orthogonal on
	 * Plane A.
	 */
	internal Vector4 twistA;
	/*! The point where the constraint is attached to the other rigidbody. */
	internal Vector4 pivotB;
	/*!
	 * Defines the orthogonal plane in which the shape can be controlled (the direction
	 * orthogonal on this one and Twist B).
	 */
	internal Vector4 planeB;
	/*!
	 * Central directed axis of the cone in which the object can rotate. Orthogonal on
	 * Plane B.
	 */
	internal Vector4 twistB;
	/*!
	 * Defines the orthogonal directions in which the shape can be controlled (namely
	 * in this direction, and in the direction orthogonal on this one and Twist A).
	 */
	internal Vector4 motorA;
	/*!
	 * Defines the orthogonal directions in which the shape can be controlled (namely
	 * in this direction, and in the direction orthogonal on this one and Twist A).
	 */
	internal Vector4 motorB;
	/*!
	 * Maximum angle the object can rotate around the vector orthogonal on Plane A and
	 * Twist A relative to the Twist A vector. Note that Cone Min Angle is not stored,
	 * but is simply minus this angle.
	 */
	internal float coneMaxAngle;
	/*! Minimum angle the object can rotate around Plane A, relative to Twist A. */
	internal float planeMinAngle;
	/*! Maximum angle the object can rotate around Plane A, relative to Twist A. */
	internal float planeMaxAngle;
	/*! Minimum angle the object can rotate around Twist A, relative to Plane A. */
	internal float twistMinAngle;
	/*! Maximum angle the object can rotate around Twist A, relative to Plane A. */
	internal float twistMaxAngle;
	/*! Maximum friction, typically 0 or 10. In Fallout 3, typically 100. */
	internal float maxFriction;
	/*!  */
	internal MotorDescriptor motor;
	//Constructor
	public RagdollDescriptor() { unchecked {
	coneMaxAngle = 0.0f;
	planeMinAngle = 0.0f;
	planeMaxAngle = 0.0f;
	twistMinAngle = 0.0f;
	twistMaxAngle = 0.0f;
	maxFriction = 0.0f;
	
	} }

}

}
