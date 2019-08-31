/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! ConstraintData */
	public class ConstraintData
	{
		/*! Type of constraint. */
		internal hkConstraintType type;
		/*! Always 2 (Hardcoded). Number of bodies affected by this constraint. */
		internal uint numEntities2;
		/*! Usually NONE. The entity affected by this constraint. */
		internal bhkEntity entityA;
		/*! Usually NONE. The entity affected by this constraint. */
		internal bhkEntity entityB;
		/*! Usually 1. Higher values indicate higher priority of this constraint? */
		internal uint priority;
		/*! ballAndSocket */
		internal BallAndSocketDescriptor ballAndSocket;
		/*! hinge */
		internal HingeDescriptor hinge;
		/*! limitedHinge */
		internal LimitedHingeDescriptor limitedHinge;
		/*! prismatic */
		internal PrismaticDescriptor prismatic;
		/*! ragdoll */
		internal RagdollDescriptor ragdoll;
		/*! stiffSpring */
		internal StiffSpringDescriptor stiffSpring;
		/*! malleable */
		internal MalleableDescriptor malleable;

		public ConstraintData()
		{
			unchecked
			{
				type = (hkConstraintType)0;
				numEntities2 = (uint)2;
				entityA = null;
				entityB = null;
				priority = (uint)1;
			}
		}
	}
}
