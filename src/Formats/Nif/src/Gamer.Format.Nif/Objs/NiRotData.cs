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

/*! Wrapper for rotation animation keys. */
public class NiRotData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRotData", NiObject.TYPE);
	/*!  */
	internal uint numRotationKeys;
	/*!  */
	internal KeyType rotationType;
	/*!  */
	internal IList<Key<Quaternion>> quaternionKeys;
	/*!  */
	internal Array3<KeyGroup<float>> xyzRotations;

	public NiRotData() {
	numRotationKeys = (uint)0;
	rotationType = (KeyType)0;
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
public static NiObject Create() => new NiRotData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out numRotationKeys, s, info);
	if ((numRotationKeys != 0)) {
		Nif.NifStream(out rotationType, s, info);
	}
	if ((rotationType != 4)) {
		quaternionKeys = new Key[numRotationKeys];
		for (var i2 = 0; i2 < quaternionKeys.Count; i2++) {
			Nif.NifStream(out quaternionKeys[i2], s, info, rotationType);
		}
	}
	if ((rotationType == 4)) {
		for (var i2 = 0; i2 < 3; i2++) {
			Nif.NifStream(out xyzRotations[i2].numKeys, s, info);
			if ((xyzRotations[i2].numKeys != 0)) {
				Nif.NifStream(out xyzRotations[i2].interpolation, s, info);
			}
			xyzRotations[i2].keys = new Key[xyzRotations[i2].numKeys];
			for (var i3 = 0; i3 < xyzRotations[i2].keys.Count; i3++) {
				Nif.NifStream(out xyzRotations[i2].keys[i3], s, info, xyzRotations[i2].interpolation);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	numRotationKeys = (uint)quaternionKeys.Count;
	Nif.NifStream(numRotationKeys, s, info);
	if ((numRotationKeys != 0)) {
		Nif.NifStream(rotationType, s, info);
	}
	if ((rotationType != 4)) {
		for (var i2 = 0; i2 < quaternionKeys.Count; i2++) {
			Nif.NifStream(quaternionKeys[i2], s, info, rotationType);
		}
	}
	if ((rotationType == 4)) {
		for (var i2 = 0; i2 < 3; i2++) {
			xyzRotations[i2].numKeys = (uint)xyzRotations[i2].keys.Count;
			Nif.NifStream(xyzRotations[i2].numKeys, s, info);
			if ((xyzRotations[i2].numKeys != 0)) {
				Nif.NifStream(xyzRotations[i2].interpolation, s, info);
			}
			for (var i3 = 0; i3 < xyzRotations[i2].keys.Count; i3++) {
				Nif.NifStream(xyzRotations[i2].keys[i3], s, info, xyzRotations[i2].interpolation);
			}
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
	numRotationKeys = (uint)quaternionKeys.Count;
	s.AppendLine($"  Num Rotation Keys:  {numRotationKeys}");
	if ((numRotationKeys != 0)) {
		s.AppendLine($"    Rotation Type:  {rotationType}");
	}
	if ((rotationType != 4)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < quaternionKeys.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				break;
			}
			s.AppendLine($"      Quaternion Keys[{i2}]:  {quaternionKeys[i2]}");
			array_output_count++;
		}
	}
	if ((rotationType == 4)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < 3; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			xyzRotations[i2].numKeys = (uint)xyzRotations[i2].keys.Count;
			s.AppendLine($"      Num Keys:  {xyzRotations[i2].numKeys}");
			if ((xyzRotations[i2].numKeys != 0)) {
				s.AppendLine($"        Interpolation:  {xyzRotations[i2].interpolation}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < xyzRotations[i2].keys.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Keys[{i3}]:  {xyzRotations[i2].keys[i3]}");
				array_output_count++;
			}
		}
	}
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