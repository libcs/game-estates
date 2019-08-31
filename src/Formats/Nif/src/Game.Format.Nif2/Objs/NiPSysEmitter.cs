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
	/*! Abstract base class for all particle system emitters. */
	public class NiPSysEmitter : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysEmitter", NiPSysModifier.TYPE);

		/*! Speed / Inertia of particle movement. */
		internal float speed;
		/*! Adds an amount of randomness to Speed. */
		internal float speedVariation;
		/*! Declination / First axis. */
		internal float declination;
		/*! Declination randomness / First axis. */
		internal float declinationVariation;
		/*! Planar Angle / Second axis. */
		internal float planarAngle;
		/*! Planar Angle randomness / Second axis . */
		internal float planarAngleVariation;
		/*! Defines color of a birthed particle. */
		internal Color4 initialColor;
		/*! Size of a birthed particle. */
		internal float initialRadius;
		/*! Particle Radius randomness. */
		internal float radiusVariation;
		/*! Duration until a particle dies. */
		internal float lifeSpan;
		/*! Adds randomness to Life Span. */
		internal float lifeSpanVariation;
		public NiPSysEmitter()
		{
			speed = 0.0f;
			speedVariation = 0.0f;
			declination = 0.0f;
			declinationVariation = 0.0f;
			planarAngle = 0.0f;
			planarAngleVariation = 0.0f;
			initialRadius = 1.0f;
			radiusVariation = 0.0f;
			lifeSpan = 0.0f;
			lifeSpanVariation = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysEmitter();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out speed, s, info);
			Nif.NifStream(out speedVariation, s, info);
			Nif.NifStream(out declination, s, info);
			Nif.NifStream(out declinationVariation, s, info);
			Nif.NifStream(out planarAngle, s, info);
			Nif.NifStream(out planarAngleVariation, s, info);
			Nif.NifStream(out initialColor, s, info);
			Nif.NifStream(out initialRadius, s, info);
			if (info.version >= 0x0A040001)
			{
				Nif.NifStream(out radiusVariation, s, info);
			}
			Nif.NifStream(out lifeSpan, s, info);
			Nif.NifStream(out lifeSpanVariation, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(speed, s, info);
			Nif.NifStream(speedVariation, s, info);
			Nif.NifStream(declination, s, info);
			Nif.NifStream(declinationVariation, s, info);
			Nif.NifStream(planarAngle, s, info);
			Nif.NifStream(planarAngleVariation, s, info);
			Nif.NifStream(initialColor, s, info);
			Nif.NifStream(initialRadius, s, info);
			if (info.version >= 0x0A040001)
			{
				Nif.NifStream(radiusVariation, s, info);
			}
			Nif.NifStream(lifeSpan, s, info);
			Nif.NifStream(lifeSpanVariation, s, info);
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
			s.AppendLine($"      Speed:  {speed}");
			s.AppendLine($"      Speed Variation:  {speedVariation}");
			s.AppendLine($"      Declination:  {declination}");
			s.AppendLine($"      Declination Variation:  {declinationVariation}");
			s.AppendLine($"      Planar Angle:  {planarAngle}");
			s.AppendLine($"      Planar Angle Variation:  {planarAngleVariation}");
			s.AppendLine($"      Initial Color:  {initialColor}");
			s.AppendLine($"      Initial Radius:  {initialRadius}");
			s.AppendLine($"      Radius Variation:  {radiusVariation}");
			s.AppendLine($"      Life Span:  {lifeSpan}");
			s.AppendLine($"      Life Span Variation:  {lifeSpanVariation}");
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
