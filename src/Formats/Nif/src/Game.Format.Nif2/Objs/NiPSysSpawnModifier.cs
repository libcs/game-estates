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
	/*! Particle modifier that spawns additional copies of a particle. */
	public class NiPSysSpawnModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysSpawnModifier", NiPSysModifier.TYPE);

		/*! Number of allowed generations for spawning. Particles whose generations are >= will not be spawned. */
		internal ushort numSpawnGenerations;
		/*! The likelihood of a particular particle being spawned. Must be between 0.0 and 1.0. */
		internal float percentageSpawned;
		/*! The minimum particles to spawn for any given original particle. */
		internal ushort minNumToSpawn;
		/*! The maximum particles to spawn for any given original particle. */
		internal ushort maxNumToSpawn;
		/*! WorldShift */
		internal int unknownInt;
		/*! How much the spawned particle speed can vary. */
		internal float spawnSpeedVariation;
		/*! How much the spawned particle direction can vary. */
		internal float spawnDirVariation;
		/*! Lifespan assigned to spawned particles. */
		internal float lifeSpan;
		/*! The amount the lifespan can vary. */
		internal float lifeSpanVariation;
		public NiPSysSpawnModifier()
		{
			numSpawnGenerations = (ushort)0;
			percentageSpawned = 1.0f;
			minNumToSpawn = (ushort)1;
			maxNumToSpawn = (ushort)1;
			unknownInt = (int)0;
			spawnSpeedVariation = 0.0f;
			spawnDirVariation = 0.0f;
			lifeSpan = 0.0f;
			lifeSpanVariation = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysSpawnModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numSpawnGenerations, s, info);
			Nif.NifStream(out percentageSpawned, s, info);
			Nif.NifStream(out minNumToSpawn, s, info);
			Nif.NifStream(out maxNumToSpawn, s, info);
			if (info.version >= 0x0A040001 && info.version <= 0x0A040001)
			{
				Nif.NifStream(out unknownInt, s, info);
			}
			Nif.NifStream(out spawnSpeedVariation, s, info);
			Nif.NifStream(out spawnDirVariation, s, info);
			Nif.NifStream(out lifeSpan, s, info);
			Nif.NifStream(out lifeSpanVariation, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(numSpawnGenerations, s, info);
			Nif.NifStream(percentageSpawned, s, info);
			Nif.NifStream(minNumToSpawn, s, info);
			Nif.NifStream(maxNumToSpawn, s, info);
			if (info.version >= 0x0A040001 && info.version <= 0x0A040001)
			{
				Nif.NifStream(unknownInt, s, info);
			}
			Nif.NifStream(spawnSpeedVariation, s, info);
			Nif.NifStream(spawnDirVariation, s, info);
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
			s.AppendLine($"      Num Spawn Generations:  {numSpawnGenerations}");
			s.AppendLine($"      Percentage Spawned:  {percentageSpawned}");
			s.AppendLine($"      Min Num to Spawn:  {minNumToSpawn}");
			s.AppendLine($"      Max Num to Spawn:  {maxNumToSpawn}");
			s.AppendLine($"      Unknown Int:  {unknownInt}");
			s.AppendLine($"      Spawn Speed Variation:  {spawnSpeedVariation}");
			s.AppendLine($"      Spawn Dir Variation:  {spawnDirVariation}");
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
