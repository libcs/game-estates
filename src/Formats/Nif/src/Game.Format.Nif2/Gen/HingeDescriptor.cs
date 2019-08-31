/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! This constraint allows rotation about a specified axis. */
	public class HingeDescriptor
	{
		/*! Pivot point around which the object will rotate. */
		internal Vector4 pivotA;
		/*! Vector in the rotation plane which defines the zero angle. */
		internal Vector4 perpAxisInA1;
		/*! Vector in the rotation plane, orthogonal on the previous one, which defines the positive direction of rotation. */
		internal Vector4 perpAxisInA2;
		/*! Pivot A in second entity coordinate system. */
		internal Vector4 pivotB;
		/*! Axis A (vector orthogonal on Perp Axes) in second entity coordinate system. */
		internal Vector4 axisB;
		/*! Axis of rotation. */
		internal Vector4 axisA;
		/*! Perp Axis In A1 in second entity coordinate system. */
		internal Vector4 perpAxisInB1;
		/*! Perp Axis In A2 in second entity coordinate system. */
		internal Vector4 perpAxisInB2;

	}
}
