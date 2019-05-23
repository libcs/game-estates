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
	/*! Represents a particle system that uses mesh particles instead of sprite-based particles. */
	public class NiPSMeshParticleSystem : NiPSParticleSystem
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSMeshParticleSystem", NiPSParticleSystem.TYPE);

		/*! numGenerations */
		internal uint numGenerations;
		/*! masterParticles */
		internal IList<NiAVObject> masterParticles;
		/*! poolSize */
		internal uint poolSize;
		/*! auto_fillPools */
		internal bool auto_fillPools;
		public NiPSMeshParticleSystem()
		{
			numGenerations = (uint)0;
			poolSize = (uint)0;
			auto_fillPools = false;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSMeshParticleSystem();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out numGenerations, s, info);
			masterParticles = new Ref[numGenerations];
			for (var i3 = 0; i3 < masterParticles.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out poolSize, s, info);
			Nif.NifStream(out auto_fillPools, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numGenerations = (uint)masterParticles.Count;
			Nif.NifStream(numGenerations, s, info);
			for (var i3 = 0; i3 < masterParticles.Count; i3++)
			{
				WriteRef((NiObject)masterParticles[i3], s, info, link_map, missing_link_stack);
			}
			Nif.NifStream(poolSize, s, info);
			Nif.NifStream(auto_fillPools, s, info);
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			var array_output_count = 0U;
			s.Append(base.AsString());
			numGenerations = (uint)masterParticles.Count;
			s.AppendLine($"      Num Generations:  {numGenerations}");
			array_output_count = 0;
			for (var i3 = 0; i3 < masterParticles.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Master Particles[{i3}]:  {masterParticles[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Pool Size:  {poolSize}");
			s.AppendLine($"      Auto-Fill Pools:  {auto_fillPools}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < masterParticles.Count; i3++)
			{
				masterParticles[i3] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < masterParticles.Count; i3++)
			{
				if (masterParticles[i3] != null)
					refs.Add((NiObject)masterParticles[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < masterParticles.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
