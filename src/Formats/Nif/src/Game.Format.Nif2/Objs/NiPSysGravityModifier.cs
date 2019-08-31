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
	/*! Particle modifier that applies a gravitational force to particles. */
	public class NiPSysGravityModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysGravityModifier", NiPSysModifier.TYPE);

		/*! The object whose position and orientation are the basis of the force. */
		internal NiAVObject gravityObject;
		/*! The local direction of the force. */
		internal Vector3 gravityAxis;
		/*! How the force diminishes by distance. */
		internal float decay;
		/*! The acceleration of the force. */
		internal float strength;
		/*! The type of gravitational force. */
		internal ForceType forceType;
		/*! Adds a degree of randomness. */
		internal float turbulence;
		/*! Scale for turbulence. */
		internal float turbulenceScale;
		/*! worldAligned */
		internal bool worldAligned;
		public NiPSysGravityModifier()
		{
			gravityObject = null;
			gravityAxis = 1.0, 0.0, 0.0;
			decay = 0.0f;
			strength = 1.0f;
			forceType = (ForceType)0;
			turbulence = 0.0f;
			turbulenceScale = 1.0f;
			worldAligned = false;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysGravityModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out gravityAxis, s, info);
			Nif.NifStream(out decay, s, info);
			Nif.NifStream(out strength, s, info);
			Nif.NifStream(out forceType, s, info);
			Nif.NifStream(out turbulence, s, info);
			Nif.NifStream(out turbulenceScale, s, info);
			if ((info.userVersion2 > 16))
			{
				Nif.NifStream(out worldAligned, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)gravityObject, s, info, link_map, missing_link_stack);
			Nif.NifStream(gravityAxis, s, info);
			Nif.NifStream(decay, s, info);
			Nif.NifStream(strength, s, info);
			Nif.NifStream(forceType, s, info);
			Nif.NifStream(turbulence, s, info);
			Nif.NifStream(turbulenceScale, s, info);
			if ((info.userVersion2 > 16))
			{
				Nif.NifStream(worldAligned, s, info);
			}
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
			s.AppendLine($"      Gravity Object:  {gravityObject}");
			s.AppendLine($"      Gravity Axis:  {gravityAxis}");
			s.AppendLine($"      Decay:  {decay}");
			s.AppendLine($"      Strength:  {strength}");
			s.AppendLine($"      Force Type:  {forceType}");
			s.AppendLine($"      Turbulence:  {turbulence}");
			s.AppendLine($"      Turbulence Scale:  {turbulenceScale}");
			s.AppendLine($"      World Aligned:  {worldAligned}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			gravityObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
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
			if (gravityObject != null)
				ptrs.Add((NiObject)gravityObject);
			return ptrs;
		}
	}
}
