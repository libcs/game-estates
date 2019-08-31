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
	/*! Bethesda-Specific particle system. */
	public class BSMasterParticleSystem : NiNode
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSMasterParticleSystem", NiNode.TYPE);

		/*! maxEmitterObjects */
		internal ushort maxEmitterObjects;
		/*! numParticleSystems */
		internal int numParticleSystems;
		/*! particleSystems */
		internal IList<NiAVObject> particleSystems;
		public BSMasterParticleSystem()
		{
			maxEmitterObjects = (ushort)0;
			numParticleSystems = (int)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSMasterParticleSystem();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out maxEmitterObjects, s, info);
			Nif.NifStream(out numParticleSystems, s, info);
			particleSystems = new Ref[numParticleSystems];
			for (var i3 = 0; i3 < particleSystems.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numParticleSystems = (int)particleSystems.Count;
			Nif.NifStream(maxEmitterObjects, s, info);
			Nif.NifStream(numParticleSystems, s, info);
			for (var i3 = 0; i3 < particleSystems.Count; i3++)
			{
				WriteRef((NiObject)particleSystems[i3], s, info, link_map, missing_link_stack);
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
			var array_output_count = 0U;
			s.Append(base.AsString());
			numParticleSystems = (int)particleSystems.Count;
			s.AppendLine($"      Max Emitter Objects:  {maxEmitterObjects}");
			s.AppendLine($"      Num Particle Systems:  {numParticleSystems}");
			array_output_count = 0;
			for (var i3 = 0; i3 < particleSystems.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Particle Systems[{i3}]:  {particleSystems[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < particleSystems.Count; i3++)
			{
				particleSystems[i3] = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < particleSystems.Count; i3++)
			{
				if (particleSystems[i3] != null)
					refs.Add((NiObject)particleSystems[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < particleSystems.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
