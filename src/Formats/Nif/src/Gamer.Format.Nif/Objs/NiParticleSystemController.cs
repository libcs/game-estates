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

/*! A generic particle system time controller object. */
public class NiParticleSystemController : NiTimeController {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiParticleSystemController", NiTimeController.TYPE);
	/*! Particle speed in old files */
	internal uint oldSpeed;
	/*! Particle speed */
	internal float speed;
	/*! Particle random speed modifier */
	internal float speedRandom;
	/*!
	 * vertical emit direction [radians]
	 *             0.0 : up
	 *             1.6 : horizontal
	 *             3.1416 : down
	 */
	internal float verticalDirection;
	/*! emitter's vertical opening angle [radians] */
	internal float verticalAngle;
	/*! horizontal emit direction */
	internal float horizontalDirection;
	/*! emitter's horizontal opening angle */
	internal float horizontalAngle;
	/*! Unknown. */
	internal Vector3 unknownNormal_;
	/*! Unknown. */
	internal Color4 unknownColor_;
	/*! Particle size */
	internal float size;
	/*! Particle emit start time */
	internal float emitStartTime;
	/*! Particle emit stop time */
	internal float emitStopTime;
	/*! Unknown byte, (=0) */
	internal byte unknownByte;
	/*! Particle emission rate in old files */
	internal uint oldEmitRate;
	/*! Particle emission rate (particles per second) */
	internal float emitRate;
	/*! Particle lifetime */
	internal float lifetime;
	/*! Particle lifetime random modifier */
	internal float lifetimeRandom;
	/*! Bit 0: Emit Rate toggle bit (0 = auto adjust, 1 = use Emit Rate value) */
	internal ushort emitFlags;
	/*! Particle random start translation vector */
	internal Vector3 startRandom;
	/*!
	 * This index targets the particle emitter object (TODO: find out what type of
	 * object this refers to).
	 */
	internal NiObject emitter;
	/*! ? short=0 ? */
	internal ushort unknownShort2_;
	/*! ? float=1.0 ? */
	internal float unknownFloat13_;
	/*! ? int=1 ? */
	internal uint unknownInt1_;
	/*! ? int=0 ? */
	internal uint unknownInt2_;
	/*! ? short=0 ? */
	internal ushort unknownShort3_;
	/*! Particle velocity */
	internal Vector3 particleVelocity;
	/*! Unknown */
	internal Vector3 particleUnknownVector;
	/*! The particle's age. */
	internal float particleLifetime;
	/*!  */
	internal NiObject particleLink;
	/*! Timestamp of the last update. */
	internal uint particleTimestamp;
	/*! Unknown short */
	internal ushort particleUnknownShort;
	/*! Particle/vertex index matches array index */
	internal ushort particleVertexId;
	/*! Size of the following array. (Maximum number of simultaneous active particles) */
	internal ushort numParticles;
	/*!
	 * Number of valid entries in the following array. (Number of active particles at
	 * the time the system was saved)
	 */
	internal ushort numValid;
	/*! Individual particle modifiers? */
	internal IList<Particle> particles;
	/*! unknown int (=0xffffffff) */
	internal NiObject unknownLink;
	/*!
	 * Link to some optional particle modifiers (NiGravity, NiParticleGrowFade,
	 * NiParticleBomb, ...)
	 */
	internal NiParticleModifier particleExtra;
	/*! Unknown int (=0xffffffff) */
	internal NiObject unknownLink2;
	/*! Trailing null byte */
	internal byte trailer;
	/*!  */
	internal NiColorData colorData;
	/*! Unknown. */
	internal float unknownFloat1;
	/*! Unknown. */
	internal IList<float> unknownFloats2;

	public NiParticleSystemController() {
	oldSpeed = (uint)0;
	speed = 0.0f;
	speedRandom = 0.0f;
	verticalDirection = 0.0f;
	verticalAngle = 0.0f;
	horizontalDirection = 0.0f;
	horizontalAngle = 0.0f;
	size = 0.0f;
	emitStartTime = 0.0f;
	emitStopTime = 0.0f;
	unknownByte = (byte)0;
	oldEmitRate = (uint)0;
	emitRate = 0.0f;
	lifetime = 0.0f;
	lifetimeRandom = 0.0f;
	emitFlags = (ushort)0;
	emitter = null;
	unknownShort2_ = (ushort)0;
	unknownFloat13_ = 0.0f;
	unknownInt1_ = (uint)0;
	unknownInt2_ = (uint)0;
	unknownShort3_ = (ushort)0;
	particleLifetime = 0.0f;
	particleLink = null;
	particleTimestamp = (uint)0;
	particleUnknownShort = (ushort)0;
	particleVertexId = (ushort)0;
	numParticles = (ushort)0;
	numValid = (ushort)0;
	unknownLink = null;
	particleExtra = null;
	unknownLink2 = null;
	trailer = (byte)0;
	colorData = null;
	unknownFloat1 = 0.0f;
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
public static NiObject Create() => new NiParticleSystemController();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version <= 0x03010000) {
		Nif.NifStream(out oldSpeed, s, info);
	}
	if (info.version >= 0x0303000D) {
		Nif.NifStream(out speed, s, info);
	}
	Nif.NifStream(out speedRandom, s, info);
	Nif.NifStream(out verticalDirection, s, info);
	Nif.NifStream(out verticalAngle, s, info);
	Nif.NifStream(out horizontalDirection, s, info);
	Nif.NifStream(out horizontalAngle, s, info);
	Nif.NifStream(out unknownNormal_, s, info);
	Nif.NifStream(out unknownColor_, s, info);
	Nif.NifStream(out size, s, info);
	Nif.NifStream(out emitStartTime, s, info);
	Nif.NifStream(out emitStopTime, s, info);
	if (info.version >= 0x04000002) {
		Nif.NifStream(out unknownByte, s, info);
	}
	if (info.version <= 0x03010000) {
		Nif.NifStream(out oldEmitRate, s, info);
	}
	if (info.version >= 0x0303000D) {
		Nif.NifStream(out emitRate, s, info);
	}
	Nif.NifStream(out lifetime, s, info);
	Nif.NifStream(out lifetimeRandom, s, info);
	if (info.version >= 0x04000002) {
		Nif.NifStream(out emitFlags, s, info);
	}
	Nif.NifStream(out startRandom, s, info);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	if (info.version >= 0x04000002) {
		Nif.NifStream(out unknownShort2_, s, info);
		Nif.NifStream(out unknownFloat13_, s, info);
		Nif.NifStream(out unknownInt1_, s, info);
		Nif.NifStream(out unknownInt2_, s, info);
		Nif.NifStream(out unknownShort3_, s, info);
	}
	if (info.version <= 0x03010000) {
		Nif.NifStream(out particleVelocity, s, info);
		Nif.NifStream(out particleUnknownVector, s, info);
		Nif.NifStream(out particleLifetime, s, info);
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out particleTimestamp, s, info);
		Nif.NifStream(out particleUnknownShort, s, info);
		Nif.NifStream(out particleVertexId, s, info);
	}
	if (info.version >= 0x04000002) {
		Nif.NifStream(out numParticles, s, info);
		Nif.NifStream(out numValid, s, info);
		particles = new Particle[numParticles];
		for (var i2 = 0; i2 < particles.Count; i2++) {
			Nif.NifStream(out particles[i2].velocity, s, info);
			Nif.NifStream(out particles[i2].unknownVector, s, info);
			Nif.NifStream(out particles[i2].lifetime, s, info);
			Nif.NifStream(out particles[i2].lifespan, s, info);
			Nif.NifStream(out particles[i2].timestamp, s, info);
			Nif.NifStream(out particles[i2].unknownShort, s, info);
			Nif.NifStream(out particles[i2].vertexId, s, info);
		}
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	Nif.NifStream(out block_num, s, info);
	link_stack.Add(block_num);
	if (info.version >= 0x04000002) {
		Nif.NifStream(out trailer, s, info);
	}
	if (info.version <= 0x03010000) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out unknownFloat1, s, info);
		unknownFloats2 = new float[particleUnknownShort];
		for (var i2 = 0; i2 < unknownFloats2.Count; i2++) {
			Nif.NifStream(out unknownFloats2[i2], s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numParticles = (ushort)particles.Count;
	particleUnknownShort = (ushort)unknownFloats2.Count;
	if (info.version <= 0x03010000) {
		Nif.NifStream(oldSpeed, s, info);
	}
	if (info.version >= 0x0303000D) {
		Nif.NifStream(speed, s, info);
	}
	Nif.NifStream(speedRandom, s, info);
	Nif.NifStream(verticalDirection, s, info);
	Nif.NifStream(verticalAngle, s, info);
	Nif.NifStream(horizontalDirection, s, info);
	Nif.NifStream(horizontalAngle, s, info);
	Nif.NifStream(unknownNormal_, s, info);
	Nif.NifStream(unknownColor_, s, info);
	Nif.NifStream(size, s, info);
	Nif.NifStream(emitStartTime, s, info);
	Nif.NifStream(emitStopTime, s, info);
	if (info.version >= 0x04000002) {
		Nif.NifStream(unknownByte, s, info);
	}
	if (info.version <= 0x03010000) {
		Nif.NifStream(oldEmitRate, s, info);
	}
	if (info.version >= 0x0303000D) {
		Nif.NifStream(emitRate, s, info);
	}
	Nif.NifStream(lifetime, s, info);
	Nif.NifStream(lifetimeRandom, s, info);
	if (info.version >= 0x04000002) {
		Nif.NifStream(emitFlags, s, info);
	}
	Nif.NifStream(startRandom, s, info);
	WriteRef((NiObject)emitter, s, info, link_map, missing_link_stack);
	if (info.version >= 0x04000002) {
		Nif.NifStream(unknownShort2_, s, info);
		Nif.NifStream(unknownFloat13_, s, info);
		Nif.NifStream(unknownInt1_, s, info);
		Nif.NifStream(unknownInt2_, s, info);
		Nif.NifStream(unknownShort3_, s, info);
	}
	if (info.version <= 0x03010000) {
		Nif.NifStream(particleVelocity, s, info);
		Nif.NifStream(particleUnknownVector, s, info);
		Nif.NifStream(particleLifetime, s, info);
		WriteRef((NiObject)particleLink, s, info, link_map, missing_link_stack);
		Nif.NifStream(particleTimestamp, s, info);
		Nif.NifStream(particleUnknownShort, s, info);
		Nif.NifStream(particleVertexId, s, info);
	}
	if (info.version >= 0x04000002) {
		Nif.NifStream(numParticles, s, info);
		Nif.NifStream(numValid, s, info);
		for (var i2 = 0; i2 < particles.Count; i2++) {
			Nif.NifStream(particles[i2].velocity, s, info);
			Nif.NifStream(particles[i2].unknownVector, s, info);
			Nif.NifStream(particles[i2].lifetime, s, info);
			Nif.NifStream(particles[i2].lifespan, s, info);
			Nif.NifStream(particles[i2].timestamp, s, info);
			Nif.NifStream(particles[i2].unknownShort, s, info);
			Nif.NifStream(particles[i2].vertexId, s, info);
		}
		WriteRef((NiObject)unknownLink, s, info, link_map, missing_link_stack);
	}
	WriteRef((NiObject)particleExtra, s, info, link_map, missing_link_stack);
	WriteRef((NiObject)unknownLink2, s, info, link_map, missing_link_stack);
	if (info.version >= 0x04000002) {
		Nif.NifStream(trailer, s, info);
	}
	if (info.version <= 0x03010000) {
		WriteRef((NiObject)colorData, s, info, link_map, missing_link_stack);
		Nif.NifStream(unknownFloat1, s, info);
		for (var i2 = 0; i2 < unknownFloats2.Count; i2++) {
			Nif.NifStream(unknownFloats2[i2], s, info);
		}
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
	numParticles = (ushort)particles.Count;
	particleUnknownShort = (ushort)unknownFloats2.Count;
	s.AppendLine($"  Old Speed:  {oldSpeed}");
	s.AppendLine($"  Speed:  {speed}");
	s.AppendLine($"  Speed Random:  {speedRandom}");
	s.AppendLine($"  Vertical Direction:  {verticalDirection}");
	s.AppendLine($"  Vertical Angle:  {verticalAngle}");
	s.AppendLine($"  Horizontal Direction:  {horizontalDirection}");
	s.AppendLine($"  Horizontal Angle:  {horizontalAngle}");
	s.AppendLine($"  Unknown Normal?:  {unknownNormal_}");
	s.AppendLine($"  Unknown Color?:  {unknownColor_}");
	s.AppendLine($"  Size:  {size}");
	s.AppendLine($"  Emit Start Time:  {emitStartTime}");
	s.AppendLine($"  Emit Stop Time:  {emitStopTime}");
	s.AppendLine($"  Unknown Byte:  {unknownByte}");
	s.AppendLine($"  Old Emit Rate:  {oldEmitRate}");
	s.AppendLine($"  Emit Rate:  {emitRate}");
	s.AppendLine($"  Lifetime:  {lifetime}");
	s.AppendLine($"  Lifetime Random:  {lifetimeRandom}");
	s.AppendLine($"  Emit Flags:  {emitFlags}");
	s.AppendLine($"  Start Random:  {startRandom}");
	s.AppendLine($"  Emitter:  {emitter}");
	s.AppendLine($"  Unknown Short 2?:  {unknownShort2_}");
	s.AppendLine($"  Unknown Float 13?:  {unknownFloat13_}");
	s.AppendLine($"  Unknown Int 1?:  {unknownInt1_}");
	s.AppendLine($"  Unknown Int 2?:  {unknownInt2_}");
	s.AppendLine($"  Unknown Short 3?:  {unknownShort3_}");
	s.AppendLine($"  Particle Velocity:  {particleVelocity}");
	s.AppendLine($"  Particle Unknown Vector:  {particleUnknownVector}");
	s.AppendLine($"  Particle Lifetime:  {particleLifetime}");
	s.AppendLine($"  Particle Link:  {particleLink}");
	s.AppendLine($"  Particle Timestamp:  {particleTimestamp}");
	s.AppendLine($"  Particle Unknown Short:  {particleUnknownShort}");
	s.AppendLine($"  Particle Vertex Id:  {particleVertexId}");
	s.AppendLine($"  Num Particles:  {numParticles}");
	s.AppendLine($"  Num Valid:  {numValid}");
	array_output_count = 0;
	for (var i1 = 0; i1 < particles.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Velocity:  {particles[i1].velocity}");
		s.AppendLine($"    Unknown Vector:  {particles[i1].unknownVector}");
		s.AppendLine($"    Lifetime:  {particles[i1].lifetime}");
		s.AppendLine($"    Lifespan:  {particles[i1].lifespan}");
		s.AppendLine($"    Timestamp:  {particles[i1].timestamp}");
		s.AppendLine($"    Unknown Short:  {particles[i1].unknownShort}");
		s.AppendLine($"    Vertex ID:  {particles[i1].vertexId}");
	}
	s.AppendLine($"  Unknown Link:  {unknownLink}");
	s.AppendLine($"  Particle Extra:  {particleExtra}");
	s.AppendLine($"  Unknown Link 2:  {unknownLink2}");
	s.AppendLine($"  Trailer:  {trailer}");
	s.AppendLine($"  Color Data:  {colorData}");
	s.AppendLine($"  Unknown Float 1:  {unknownFloat1}");
	array_output_count = 0;
	for (var i1 = 0; i1 < unknownFloats2.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			break;
		}
		s.AppendLine($"    Unknown Floats 2[{i1}]:  {unknownFloats2[i1]}");
		array_output_count++;
	}
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	emitter = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x03010000) {
		particleLink = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	}
	if (info.version >= 0x04000002) {
		unknownLink = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	}
	particleExtra = FixLink<NiParticleModifier>(objects, link_stack, missing_link_stack, info);
	unknownLink2 = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
	if (info.version <= 0x03010000) {
		colorData = FixLink<NiColorData>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (particleLink != null)
		refs.Add((NiObject)particleLink);
	if (unknownLink != null)
		refs.Add((NiObject)unknownLink);
	if (particleExtra != null)
		refs.Add((NiObject)particleExtra);
	if (unknownLink2 != null)
		refs.Add((NiObject)unknownLink2);
	if (colorData != null)
		refs.Add((NiObject)colorData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	if (emitter != null)
		ptrs.Add((NiObject)emitter);
	return ptrs;
}


}

}