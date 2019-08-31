/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.
//-----------------------------------NOTICE----------------------------------//
// Only add custom code in the designated areas to preserve between builds   //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! Particle modifier that adds rotations to particles. */
	public class NiPSysRotationModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysRotationModifier", NiPSysModifier.TYPE);

		/*! Initial Rotation Speed in radians per second. */
		internal float rotationSpeed;
		/*! Distributes rotation speed over the range [Speed - Variation, Speed + Variation]. */
		internal float rotationSpeedVariation;
		/*! Initial Rotation Angle in radians. */
		internal float rotationAngle;
		/*! Distributes rotation angle over the range [Angle - Variation, Angle + Variation]. */
		internal float rotationAngleVariation;
		/*! Randomly negate the initial rotation speed? */
		internal bool randomRotSpeedSign;
		/*! Assign a random axis to new particles? */
		internal bool randomAxis;
		/*! Initial rotation axis. */
		internal Vector3 axis;
		public NiPSysRotationModifier()
		{
			rotationSpeed = 0.0f;
			rotationSpeedVariation = 0.0f;
			rotationAngle = 0.0f;
			rotationAngleVariation = 0.0f;
			randomRotSpeedSign = false;
			randomAxis = 1;
			axis = 1.0, 0.0, 0.0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysRotationModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out rotationSpeed, s, info);
			if (info.version >= 0x14000002)
			{
				Nif.NifStream(out rotationSpeedVariation, s, info);
				Nif.NifStream(out rotationAngle, s, info);
				Nif.NifStream(out rotationAngleVariation, s, info);
				Nif.NifStream(out randomRotSpeedSign, s, info);
			}
			Nif.NifStream(out randomAxis, s, info);
			Nif.NifStream(out axis, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(rotationSpeed, s, info);
			if (info.version >= 0x14000002)
			{
				Nif.NifStream(rotationSpeedVariation, s, info);
				Nif.NifStream(rotationAngle, s, info);
				Nif.NifStream(rotationAngleVariation, s, info);
				Nif.NifStream(randomRotSpeedSign, s, info);
			}
			Nif.NifStream(randomAxis, s, info);
			Nif.NifStream(axis, s, info);
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			s.Append(base.AsString());
			s.AppendLine($"      Rotation Speed:  {rotationSpeed}");
			s.AppendLine($"      Rotation Speed Variation:  {rotationSpeedVariation}");
			s.AppendLine($"      Rotation Angle:  {rotationAngle}");
			s.AppendLine($"      Rotation Angle Variation:  {rotationAngleVariation}");
			s.AppendLine($"      Random Rot Speed Sign:  {randomRotSpeedSign}");
			s.AppendLine($"      Random Axis:  {randomAxis}");
			s.AppendLine($"      Axis:  {axis}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			return ptrs;
		}
	}
}
