/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*! Represents a particle system. */
public class NiPSParticleSystem : NiMesh {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSParticleSystem", NiMesh.TYPE);
	/*!  */
	internal NiPSSimulator simulator;
	/*!  */
	internal NiPSBoundUpdater generator;
	/*!  */
	internal uint numEmitters;
	/*!  */
	internal IList<NiPSEmitter> emitters;
	/*!  */
	internal uint numSpawners;
	/*!  */
	internal IList<NiPSSpawner> spawners;
	/*!  */
	internal NiPSSpawner deathSpawner;
	/*!  */
	internal uint maxNumParticles;
	/*!  */
	internal bool hasColors;
	/*!  */
	internal bool hasRotations;
	/*!  */
	internal bool hasRotationAxes;
	/*!  */
	internal bool hasAnimatedTextures;
	/*!  */
	internal bool worldSpace;
	/*!  */
	internal AlignMethod normalMethod;
	/*!  */
	internal Vector3 normalDirection;
	/*!  */
	internal AlignMethod upMethod;
	/*!  */
	internal Vector3 upDirection;
	/*!  */
	internal NiPSSpawner livingSpawner;
	/*!  */
	internal byte numSpawnRateKeys;
	/*!  */
	internal IList<PSSpawnRateKey> spawnRateKeys;
	/*!  */
	internal bool pre_rpi;

	public NiPSParticleSystem() {
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

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new NiPSParticleSystem();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out numEmitters, s, info);
	emitters = new Ref[numEmitters];
	for (var i1 = 0; i1 < emitters.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out numSpawners, s, info);
	spawners = new Ref[numSpawners];
	for (var i1 = 0; i1 < spawners.Count; i1++) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out maxNumParticles, s, info);
	Nif.NifStream(out hasColors, s, info);
	Nif.NifStream(out hasRotations, s, info);
	Nif.NifStream(out hasRotationAxes, s, info);
	if (info.version >= 0x14060100) {
		Nif.NifStream(out hasAnimatedTextures, s, info);
	}
	Nif.NifStream(out worldSpace, s, info);
	if (info.version >= 0x14060100) {
		Nif.NifStream(out normalMethod, s, info);
		Nif.NifStream(out normalDirection, s, info);
		Nif.NifStream(out upMethod, s, info);
		Nif.NifStream(out upDirection, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out numSpawnRateKeys, s, info);
		spawnRateKeys = new PSSpawnRateKey[numSpawnRateKeys];
		for (var i2 = 0; i2 < spawnRateKeys.Count; i2++) {
			Nif.NifStream(out spawnRateKeys[i2].value, s, info);
			Nif.NifStream(out spawnRateKeys[i2].time, s, info);
		}
		Nif.NifStream(out pre_rpi, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numSpawnRateKeys = (byte)spawnRateKeys.Count;
	numSpawners = (uint)spawners.Count;
	numEmitters = (uint)emitters.Count;
	WriteRef((NiObject)simulator, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)generator, s, info, link_map, missing_link_stack);
	Nif.NifStream(numEmitters, s, info);
	for (var i1 = 0; i1 < emitters.Count; i1++) {
		WriteRef((NiObject)emitters[i1], s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(numSpawners, s, info);
	for (var i1 = 0; i1 < spawners.Count; i1++) {
		WriteRef((NiObject)spawners[i1], s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)deathSpawner, s, info, link_map, missing_link_stack);
	Nif.NifStream(maxNumParticles, s, info);
	Nif.NifStream(hasColors, s, info);
	Nif.NifStream(hasRotations, s, info);
	Nif.NifStream(hasRotationAxes, s, info);
	if (info.version >= 0x14060100) {
		Nif.NifStream(hasAnimatedTextures, s, info);
	}
	Nif.NifStream(worldSpace, s, info);
	if (info.version >= 0x14060100) {
		Nif.NifStream(normalMethod, s, info);
		Nif.NifStream(normalDirection, s, info);
		Nif.NifStream(upMethod, s, info);
		Nif.NifStream(upDirection, s, info);
		WriteRef((NiObject)livingSpawner, s, info, link_map, missing_link_stack);
		Nif.NifStream(numSpawnRateKeys, s, info);
		for (var i2 = 0; i2 < spawnRateKeys.Count; i2++) {
			Nif.NifStream(spawnRateKeys[i2].value, s, info);
			Nif.NifStream(spawnRateKeys[i2].time, s, info);
		}
		Nif.NifStream(pre_rpi, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	numSpawnRateKeys = (byte)spawnRateKeys.Count;
	numSpawners = (uint)spawners.Count;
	numEmitters = (uint)emitters.Count;
	s.AppendLine($"  Simulator:  {simulator}");
	s.AppendLine($"  Generator:  {generator}");
	s.AppendLine($"  Num Emitters:  {numEmitters}");
	array_output_count = 0;
	for (var i1 = 0; i1 < emitters.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Emitters[{i1}]:  {emitters[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Num Spawners:  {numSpawners}");
	array_output_count = 0;
	for (var i1 = 0; i1 < spawners.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Spawners[{i1}]:  {spawners[i1]}");
		array_output_count++;
	}
	s.AppendLine($"  Death Spawner:  {deathSpawner}");
	s.AppendLine($"  Max Num Particles:  {maxNumParticles}");
	s.AppendLine($"  Has Colors:  {hasColors}");
	s.AppendLine($"  Has Rotations:  {hasRotations}");
	s.AppendLine($"  Has Rotation Axes:  {hasRotationAxes}");
	s.AppendLine($"  Has Animated Textures:  {hasAnimatedTextures}");
	s.AppendLine($"  World Space:  {worldSpace}");
	s.AppendLine($"  Normal Method:  {normalMethod}");
	s.AppendLine($"  Normal Direction:  {normalDirection}");
	s.AppendLine($"  Up Method:  {upMethod}");
	s.AppendLine($"  Up Direction:  {upDirection}");
	s.AppendLine($"  Living Spawner:  {livingSpawner}");
	s.AppendLine($"  Num Spawn Rate Keys:  {numSpawnRateKeys}");
	array_output_count = 0;
	for (var i1 = 0; i1 < spawnRateKeys.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Value:  {spawnRateKeys[i1].value}");
		s.AppendLine($"    Time:  {spawnRateKeys[i1].time}");
	}
	s.AppendLine($"  Pre-RPI:  {pre_rpi}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	simulator = FixLink<NiPSSimulator>(objects, link_stack, missing_link_stack, info);
	generator = FixLink<NiPSBoundUpdater>(objects, link_stack, missing_link_stack, info);
	for (var i1 = 0; i1 < emitters.Count; i1++) {
		emitters[i1] = FixLink<NiPSEmitter>(objects, link_stack, missing_link_stack, info);
	}
	for (var i1 = 0; i1 < spawners.Count; i1++) {
		spawners[i1] = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);
	}
	deathSpawner = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x14060100) {
		livingSpawner = FixLink<NiPSSpawner>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (simulator != null)
		refs.Add((NiObject)simulator);
	if (generator != null)
		refs.Add((NiObject)generator);
	for (var i1 = 0; i1 < emitters.Count; i1++) {
		if (emitters[i1] != null)
			refs.Add((NiObject)emitters[i1]);
	}
	for (var i1 = 0; i1 < spawners.Count; i1++) {
		if (spawners[i1] != null)
			refs.Add((NiObject)spawners[i1]);
	}
	if (deathSpawner != null)
		refs.Add((NiObject)deathSpawner);
	if (livingSpawner != null)
		refs.Add((NiObject)livingSpawner);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < emitters.Count; i1++) {
	}
	for (var i1 = 0; i1 < spawners.Count; i1++) {
	}
	return ptrs;
}


}

}