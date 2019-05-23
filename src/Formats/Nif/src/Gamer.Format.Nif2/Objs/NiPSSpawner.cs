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
	/*! Creates a new particle whose initial parameters are based on an existing particle. */
	public class NiPSSpawner : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSSpawner", NiObject.TYPE);

		/*! masterParticleSystem */
		internal NiPSParticleSystem masterParticleSystem;
		/*! percentageSpawned */
		internal float percentageSpawned;
		/*! spawnSpeedFactor */
		internal float spawnSpeedFactor;
		/*! spawnSpeedFactorVar */
		internal float spawnSpeedFactorVar;
		/*! spawnDirChaos */
		internal float spawnDirChaos;
		/*! lifeSpan */
		internal float lifeSpan;
		/*! lifeSpanVar */
		internal float lifeSpanVar;
		/*! numSpawnGenerations */
		internal ushort numSpawnGenerations;
		/*! minToSpawn */
		internal uint minToSpawn;
		/*! maxToSpawn */
		internal uint maxToSpawn;
		public NiPSSpawner()
		{
			masterParticleSystem = null;
			percentageSpawned = 0.0f;
			spawnSpeedFactor = 0.0f;
			spawnSpeedFactorVar = 0.0f;
			spawnDirChaos = 0.0f;
			lifeSpan = 0.0f;
			lifeSpanVar = 0.0f;
			numSpawnGenerations = (ushort)0;
			minToSpawn = (uint)0;
			maxToSpawn = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSSpawner();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out percentageSpawned, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out spawnSpeedFactor, s, info);
			}
			Nif.NifStream(out spawnSpeedFactorVar, s, info);
			Nif.NifStream(out spawnDirChaos, s, info);
			Nif.NifStream(out lifeSpan, s, info);
			Nif.NifStream(out lifeSpanVar, s, info);
			Nif.NifStream(out numSpawnGenerations, s, info);
			Nif.NifStream(out minToSpawn, s, info);
			Nif.NifStream(out maxToSpawn, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version >= 0x14060100)
			{
				WriteRef((NiObject)masterParticleSystem, s, info, link_map, missing_link_stack);
			}
			Nif.NifStream(percentageSpawned, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(spawnSpeedFactor, s, info);
			}
			Nif.NifStream(spawnSpeedFactorVar, s, info);
			Nif.NifStream(spawnDirChaos, s, info);
			Nif.NifStream(lifeSpan, s, info);
			Nif.NifStream(lifeSpanVar, s, info);
			Nif.NifStream(numSpawnGenerations, s, info);
			Nif.NifStream(minToSpawn, s, info);
			Nif.NifStream(maxToSpawn, s, info);
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
			s.AppendLine($"      Master Particle System:  {masterParticleSystem}");
			s.AppendLine($"      Percentage Spawned:  {percentageSpawned}");
			s.AppendLine($"      Spawn Speed Factor:  {spawnSpeedFactor}");
			s.AppendLine($"      Spawn Speed Factor Var:  {spawnSpeedFactorVar}");
			s.AppendLine($"      Spawn Dir Chaos:  {spawnDirChaos}");
			s.AppendLine($"      Life Span:  {lifeSpan}");
			s.AppendLine($"      Life Span Var:  {lifeSpanVar}");
			s.AppendLine($"      Num Spawn Generations:  {numSpawnGenerations}");
			s.AppendLine($"      Min to Spawn:  {minToSpawn}");
			s.AppendLine($"      Max to Spawn:  {maxToSpawn}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x14060100)
			{
				masterParticleSystem = FixLink<NiPSParticleSystem>(objects, link_stack, missing_link_stack, info);
			}
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
