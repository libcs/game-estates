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
	/*! Particle modifier that applies an explosive force to particles. */
	public class NiPSysBombModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysBombModifier", NiPSysModifier.TYPE);

		/*! The object whose position and orientation are the basis of the force. */
		internal NiNode bombObject;
		/*! The local direction of the force. */
		internal Vector3 bombAxis;
		/*! How the bomb force will decrease with distance. */
		internal float decay;
		/*! The acceleration the bomb will apply to particles. */
		internal float deltaV;
		/*! decayType */
		internal DecayType decayType;
		/*! symmetryType */
		internal SymmetryType symmetryType;
		public NiPSysBombModifier()
		{
			bombObject = null;
			decay = 0.0f;
			deltaV = 0.0f;
			decayType = (DecayType)0;
			symmetryType = (SymmetryType)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysBombModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out bombAxis, s, info);
			Nif.NifStream(out decay, s, info);
			Nif.NifStream(out deltaV, s, info);
			Nif.NifStream(out decayType, s, info);
			Nif.NifStream(out symmetryType, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)bombObject, s, info, link_map, missing_link_stack);
			Nif.NifStream(bombAxis, s, info);
			Nif.NifStream(decay, s, info);
			Nif.NifStream(deltaV, s, info);
			Nif.NifStream(decayType, s, info);
			Nif.NifStream(symmetryType, s, info);
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
			s.AppendLine($"      Bomb Object:  {bombObject}");
			s.AppendLine($"      Bomb Axis:  {bombAxis}");
			s.AppendLine($"      Decay:  {decay}");
			s.AppendLine($"      Delta V:  {deltaV}");
			s.AppendLine($"      Decay Type:  {decayType}");
			s.AppendLine($"      Symmetry Type:  {symmetryType}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			bombObject = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
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
			if (bombObject != null)
				ptrs.Add((NiObject)bombObject);
			return ptrs;
		}
	}
}
