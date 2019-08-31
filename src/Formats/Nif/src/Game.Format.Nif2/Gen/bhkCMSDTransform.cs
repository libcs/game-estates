/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! A set of transformation data: translation and rotation */
	public class bhkCMSDTransform
	{
		/*! A vector that moves the chunk by the specified amount. W is not used. */
		internal Vector4 translation;
		/*! Rotation. Reference point for rotation is bhkRigidBody translation. */
		internal hkQuaternion rotation;

	}
}
