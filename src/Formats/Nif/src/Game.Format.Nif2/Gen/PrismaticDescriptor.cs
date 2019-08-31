/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! PrismaticDescriptor */
	public class PrismaticDescriptor
	{
		/*! Pivot. */
		internal Vector4 pivotA;
		/*! Rotation axis. */
		internal Vector4 rotationA;
		/*! Plane normal. Describes the plane the object is able to move on. */
		internal Vector4 planeA;
		/*! Describes the axis the object is able to travel along. Unit vector. */
		internal Vector4 slidingA;
		/*! Describes the axis the object is able to travel along in B coordinates. Unit vector. */
		internal Vector4 slidingB;
		/*! Pivot in B coordinates. */
		internal Vector4 pivotB;
		/*! Rotation axis. */
		internal Vector4 rotationB;
		/*! Plane normal. Describes the plane the object is able to move on in B coordinates. */
		internal Vector4 planeB;
		/*! Describe the min distance the object is able to travel. */
		internal float minDistance;
		/*! Describe the max distance the object is able to travel. */
		internal float maxDistance;
		/*! Friction. */
		internal float friction;
		/*! motor */
		internal MotorDescriptor motor;

		public PrismaticDescriptor()
		{
			unchecked
			{
				minDistance = 0.0f;
				maxDistance = 0.0f;
				friction = 0.0f;
			}
		}
	}
}
