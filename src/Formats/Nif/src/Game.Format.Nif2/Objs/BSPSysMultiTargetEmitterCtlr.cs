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
	/*! Particle system (multi?) emitter controller. */
	public class BSPSysMultiTargetEmitterCtlr : NiPSysEmitterCtlr
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSPSysMultiTargetEmitterCtlr", NiPSysEmitterCtlr.TYPE);

		/*! maxEmitters */
		internal ushort maxEmitters;
		/*! masterParticleSystem */
		internal BSMasterParticleSystem masterParticleSystem;
		public BSPSysMultiTargetEmitterCtlr()
		{
			maxEmitters = (ushort)0;
			masterParticleSystem = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSPSysMultiTargetEmitterCtlr();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out maxEmitters, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(maxEmitters, s, info);
			WriteRef((NiObject)masterParticleSystem, s, info, link_map, missing_link_stack);
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
			s.AppendLine($"      Max Emitters:  {maxEmitters}");
			s.AppendLine($"      Master Particle System:  {masterParticleSystem}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			masterParticleSystem = FixLink<BSMasterParticleSystem>(objects, link_stack, missing_link_stack, info);
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
			if (masterParticleSystem != null)
				ptrs.Add((NiObject)masterParticleSystem);
			return ptrs;
		}
	}
}
