/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! BSConnectPoint */
	public class BSConnectPoint
	{
		/*! parent */
		internal string parent;
		/*! name */
		internal string name;
		/*! rotation */
		internal Quaternion rotation;
		/*! translation */
		internal Vector3 translation;
		/*! scale */
		internal float scale;

		public BSConnectPoint()
		{
			unchecked
			{
				parent = (string)WorkshopConnectPoints;
				scale = 1.0f;
			}
		}
	}
}
