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

/*! Particle system data. */
public class NiPSysData : NiParticlesData {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPSysData", NiParticlesData.TYPE);
	/*!  */
	internal IList<ParticleDesc> particleDescriptions;
	/*!  */
	internal bool hasRotationSpeeds;
	/*!  */
	internal IList<float> rotationSpeeds;
	/*!  */
	internal ushort numAddedParticles;
	/*!  */
	internal ushort addedParticlesBase;

	public NiPSysData() {
	hasRotationSpeeds = false;
	numAddedParticles = (ushort)0;
	addedParticlesBase = (ushort)0;
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
public static NiObject Create() => new NiPSysData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	if ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))) {
		particleDescriptions = new ParticleDesc[numVertices];
		for (var i2 = 0; i2 < particleDescriptions.Count; i2++) {
			Nif.NifStream(out particleDescriptions[i2].translation, s, info);
			if (info.version <= 0x0A040001) {
				for (var i4 = 0; i4 < 3; i4++) {
					Nif.NifStream(out particleDescriptions[i2].unknownFloats1[i4], s, info);
				}
			}
			Nif.NifStream(out particleDescriptions[i2].unknownFloat1, s, info);
			Nif.NifStream(out particleDescriptions[i2].unknownFloat2, s, info);
			Nif.NifStream(out particleDescriptions[i2].unknownFloat3, s, info);
			Nif.NifStream(out particleDescriptions[i2].unknownInt1, s, info);
		}
	}
	if (info.version >= 0x14000002) {
		Nif.NifStream(out hasRotationSpeeds, s, info);
	}
	if ((info.version >= 0x14000002) && ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))) {
		if (hasRotationSpeeds) {
			rotationSpeeds = new float[numVertices];
			for (var i3 = 0; i3 < rotationSpeeds.Count; i3++) {
				Nif.NifStream(out rotationSpeeds[i3], s, info);
			}
		}
	}
	if ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))) {
		Nif.NifStream(out numAddedParticles, s, info);
		Nif.NifStream(out addedParticlesBase, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	if ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))) {
		for (var i2 = 0; i2 < particleDescriptions.Count; i2++) {
			Nif.NifStream(particleDescriptions[i2].translation, s, info);
			if (info.version <= 0x0A040001) {
				for (var i4 = 0; i4 < 3; i4++) {
					Nif.NifStream(particleDescriptions[i2].unknownFloats1[i4], s, info);
				}
			}
			Nif.NifStream(particleDescriptions[i2].unknownFloat1, s, info);
			Nif.NifStream(particleDescriptions[i2].unknownFloat2, s, info);
			Nif.NifStream(particleDescriptions[i2].unknownFloat3, s, info);
			Nif.NifStream(particleDescriptions[i2].unknownInt1, s, info);
		}
	}
	if (info.version >= 0x14000002) {
		Nif.NifStream(hasRotationSpeeds, s, info);
	}
	if ((info.version >= 0x14000002) && ((!((info.version == 0x14020007) && (info.userVersion2 > 0))))) {
		if (hasRotationSpeeds) {
			for (var i3 = 0; i3 < rotationSpeeds.Count; i3++) {
				Nif.NifStream(rotationSpeeds[i3], s, info);
			}
		}
	}
	if ((!((info.version == 0x14020007) && (info.userVersion2 > 0)))) {
		Nif.NifStream(numAddedParticles, s, info);
		Nif.NifStream(addedParticlesBase, s, info);
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
	array_output_count = 0;
	for (var i1 = 0; i1 < particleDescriptions.Count; i1++) {
		if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
			s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
			break;
		}
		s.AppendLine($"    Translation:  {particleDescriptions[i1].translation}");
		array_output_count = 0;
		for (var i2 = 0; i2 < 3; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Unknown Floats 1[{i2}]:  {particleDescriptions[i1].unknownFloats1[i2]}");
			array_output_count++;
		}
		s.AppendLine($"    Unknown Float 1:  {particleDescriptions[i1].unknownFloat1}");
		s.AppendLine($"    Unknown Float 2:  {particleDescriptions[i1].unknownFloat2}");
		s.AppendLine($"    Unknown Float 3:  {particleDescriptions[i1].unknownFloat3}");
		s.AppendLine($"    Unknown Int 1:  {particleDescriptions[i1].unknownInt1}");
	}
	s.AppendLine($"  Has Rotation Speeds:  {hasRotationSpeeds}");
	if (hasRotationSpeeds) {
		array_output_count = 0;
		for (var i2 = 0; i2 < rotationSpeeds.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Rotation Speeds[{i2}]:  {rotationSpeeds[i2]}");
			array_output_count++;
		}
	}
	s.AppendLine($"  Num Added Particles:  {numAddedParticles}");
	s.AppendLine($"  Added Particles Base:  {addedParticlesBase}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}