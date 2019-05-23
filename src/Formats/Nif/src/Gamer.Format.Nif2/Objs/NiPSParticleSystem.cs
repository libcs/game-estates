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
	/*! Represents a particle system. */
	public class NiPSParticleSystem : NiMesh
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSParticleSystem", NiMesh.TYPE);

		/*! simulator */
		internal NiPSSimulator simulator;
		/*! generator */
		internal NiPSBoundUpdater generator;
		/*! numEmitters */
		internal uint numEmitters;
		/*! emitters */
		internal IList<NiPSEmitter> emitters;
		/*! numSpawners */
		internal uint numSpawners;
		/*! spawners */
		internal IList<NiPSSpawner> spawners;
		/*! deathSpawner */
		internal NiPSSpawner deathSpawner;
		/*! maxNumParticles */
		internal uint maxNumParticles;
		/*! hasColors */
		internal bool hasColors;
		/*! hasRotations */
		internal bool hasRotations;
		/*! hasRotationAxes */
		internal bool hasRotationAxes;
		/*! hasAnimatedTextures */
		internal bool hasAnimatedTextures;
		/*! worldSpace */
		internal bool worldSpace;
		/*! normalMethod */
		internal AlignMethod normalMethod;
		/*! normalDirection */
		internal Vector3 normalDirection;
		/*! upMethod */
		internal AlignMethod upMethod;
		/*! upDirection */
		internal Vector3 upDirection;
		/*! livingSpawner */
		internal NiPSSpawner livingSpawner;
		/*! numSpawnRateKeys */
		internal byte numSpawnRateKeys;
		/*! spawnRateKeys */
		internal IList<PSSpawnRateKey> spawnRateKeys;
		/*! pre_rpi */
		internal bool pre_rpi;
		public NiPSParticleSystem()
		{
			simulator = null;
			generator = null;
			numEmitters = (uint)0;
			numSpawners = (uint)0;
			deathSpawner = null;
			maxNumParticles = (uint)0;
			hasColors = false;
			hasRotations = false;
			hasRotationAxes = false;
			hasAnimatedTextures = false;
			worldSpace = false;
			normalMethod = (AlignMethod)0;
			upMethod = (AlignMethod)0;
			livingSpawner = null;
			numSpawnRateKeys = (byte)0;
			pre_rpi = false;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSParticleSystem();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out numEmitters, s, info);
			emitters = new Ref[numEmitters];
			for (var i3 = 0; i3 < emitters.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out numSpawners, s, info);
			spawners = new Ref[numSpawners];
			for (var i3 = 0; i3 < spawners.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out maxNumParticles, s, info);
			Nif.NifStream(out hasColors, s, info);
			Nif.NifStream(out hasRotations, s, info);
			Nif.NifStream(out hasRotationAxes, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out hasAnimatedTextures, s, info);
			}
			Nif.NifStream(out worldSpace, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out normalMethod, s, info);
				Nif.NifStream(out normalDirection, s, info);
				Nif.NifStream(out upMethod, s, info);
				Nif.NifStream(out upDirection, s, info);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out numSpawnRateKeys, s, info);
				spawnRateKeys = new PSSpawnRateKey[numSpawnRateKeys];
				for (var i4 = 0; i4 < spawnRateKeys.Count; i4++)
				{
					Nif.NifStream(out spawnRateKeys[i4].value, s, info);
					Nif.NifStream(out spawnRateKeys[i4].time, s, info);
				}
				Nif.NifStream(out pre_rpi, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSpawnRateKeys = (byte)spawnRateKeys.Count;
			numSpawners = (uint)spawners.Count;
			numEmitters = (uint)emitters.Count;
			WriteRef((NiObject)simulator, s, info, link_map, missing_link_stack);
			WriteRef((NiObject)generator, s, info, link_map, missing_link_stack);
			Nif.NifStream(numEmitters, s, info);
			for (var i3 = 0; i3 < emitters.Count; i3++)
			{
				WriteRef((NiObject)emitters[i3], s, info, link_map, missing_link_stack);
			}
			Nif.NifStream(numSpawners, s, info);
			for (var i3 = 0; i3 < spawners.Count; i3++)
			{
				WriteRef((NiObject)spawners[i3], s, info, link_map, missing_link_stack);
			}
			WriteRef((NiObject)deathSpawner, s, info, link_map, missing_link_stack);
			Nif.NifStream(maxNumParticles, s, info);
			Nif.NifStream(hasColors, s, info);
			Nif.NifStream(hasRotations, s, info);
			Nif.NifStream(hasRotationAxes, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(hasAnimatedTextures, s, info);
			}
			Nif.NifStream(worldSpace, s, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(normalMethod, s, info);
				Nif.NifStream(normalDirection, s, info);
				Nif.NifStream(upMethod, s, info);
				Nif.NifStream(upDirection, s, info);
				WriteRef((NiObject)livingSpawner, s, info, link_map, missing_link_stack);
				Nif.NifStream(numSpawnRateKeys, s, info);
				for (var i4 = 0; i4 < spawnRateKeys.Count; i4++)
				{
					Nif.NifStream(spawnRateKeys[i4].value, s, info);
					Nif.NifStream(spawnRateKeys[i4].time, s, info);
				}
				Nif.NifStream(pre_rpi, s, info);
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
			numSpawnRateKeys = (byte)spawnRateKeys.Count;
			numSpawners = (uint)spawners.Count;
			numEmitters = (uint)emitters.Count;
			s.AppendLine($"      Simulator:  {simulator}");
			s.AppendLine($"      Generator:  {generator}");
			s.AppendLine($"      Num Emitters:  {numEmitters}");
			array_output_count = 0;
			for (var i3 = 0; i3 < emitters.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Emitters[{i3}]:  {emitters[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Num Spawners:  {numSpawners}");
			array_output_count = 0;
			for (var i3 = 0; i3 < spawners.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Spawners[{i3}]:  {spawners[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Death Spawner:  {deathSpawner}");
			s.AppendLine($"      Max Num Particles:  {maxNumParticles}");
			s.AppendLine($"      Has Colors:  {hasColors}");
			s.AppendLine($"      Has Rotations:  {hasRotations}");
			s.AppendLine($"      Has Rotation Axes:  {hasRotationAxes}");
			s.AppendLine($"      Has Animated Textures:  {hasAnimatedTextures}");
			s.AppendLine($"      World Space:  {worldSpace}");
			s.AppendLine($"      Normal Method:  {normalMethod}");
			s.AppendLine($"      Normal Direction:  {normalDirection}");
			s.AppendLine($"      Up Method:  {upMethod}");
			s.AppendLine($"      Up Direction:  {upDirection}");
			s.AppendLine($"      Living Spawner:  {livingSpawner}");
			s.AppendLine($"      Num Spawn Rate Keys:  {numSpawnRateKeys}");
			array_output_count = 0;
			for (var i3 = 0; i3 < spawnRateKeys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Value:  {spawnRateKeys[i3].value}");
				s.AppendLine($"        Time:  {spawnRateKeys[i3].time}");
			}
			s.AppendLine($"      Pre-RPI:  {pre_rpi}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			simulator = FixLink<NiPSSimulator>(objects, link_stack, missing_link_stack, info);
			generator = FixLink<NiPSBoundUpdater>(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < emitters.Count; i3++)
			{
				emitters[i3] = FixLink<NiPSEmitter>(objects, link_stack, missing_link_stack, info);
			}
			for (var i3 = 0; i3 < spawners.Count; i3++)
			{
				spawners[i3] = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);
			}
			deathSpawner = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x14060100)
			{
				livingSpawner = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (simulator != null)
				refs.Add((NiObject)simulator);
			if (generator != null)
				refs.Add((NiObject)generator);
			for (var i3 = 0; i3 < emitters.Count; i3++)
			{
				if (emitters[i3] != null)
					refs.Add((NiObject)emitters[i3]);
			}
			for (var i3 = 0; i3 < spawners.Count; i3++)
			{
				if (spawners[i3] != null)
					refs.Add((NiObject)spawners[i3]);
			}
			if (deathSpawner != null)
				refs.Add((NiObject)deathSpawner);
			if (livingSpawner != null)
				refs.Add((NiObject)livingSpawner);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < emitters.Count; i3++)
			{
			}
			for (var i3 = 0; i3 < spawners.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
