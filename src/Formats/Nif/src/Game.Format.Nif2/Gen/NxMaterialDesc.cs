/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! NxMaterialDesc */
	public class NxMaterialDesc
	{
		/*! dynamicFriction */
		internal float dynamicFriction;
		/*! staticFriction */
		internal float staticFriction;
		/*! restitution */
		internal float restitution;
		/*! dynamicFrictionV */
		internal float dynamicFrictionV;
		/*! staticFrictionV */
		internal float staticFrictionV;
		/*! directionOfAnisotropy */
		internal Vector3 directionOfAnisotropy;
		/*! flags */
		internal NxMaterialFlag flags;
		/*! frictionCombineMode */
		internal NxCombineMode frictionCombineMode;
		/*! restitutionCombineMode */
		internal NxCombineMode restitutionCombineMode;
		/*! hasSpring */
		internal bool hasSpring;
		/*! spring */
		internal NxSpringDesc spring;

		public NxMaterialDesc()
		{
			unchecked
			{
				dynamicFriction = 0.0f;
				staticFriction = 0.0f;
				restitution = 0.0f;
				dynamicFrictionV = 0.0f;
				staticFrictionV = 0.0f;
				flags = (NxMaterialFlag)0;
				frictionCombineMode = (NxCombineMode)0;
				restitutionCombineMode = (NxCombineMode)0;
				hasSpring = false;
			}
		}
	}
}
