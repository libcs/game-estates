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

/*!
 * DEPRECATED (pre-10.1), REMOVED (20.3)
 *         Texture coordinate data.
 */
public class NiUVData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiUVData", NiObject.TYPE);
	/*!
	 * Four UV data groups. Appear to be U translation, V translation, U
	 * scaling/tiling, V scaling/tiling.
	 */
	internal Array4<KeyGroup<float>> uvGroups;

	public NiUVData() {
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
	public static NiObject Create() => new NiUVData();

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

		base.Read(s, link_stack, info);
		for (var i2 = 0; i2 < 4; i2++) {
			Nif.NifStream(out uvGroups[i2].numKeys, s, info);
			if ((uvGroups[i2].numKeys != 0)) {
				Nif.NifStream(out uvGroups[i2].interpolation, s, info);
			}
			uvGroups[i2].keys = new Key[uvGroups[i2].numKeys];
			for (var i3 = 0; i3 < uvGroups[i2].keys.Count; i3++) {
				Nif.NifStream(out uvGroups[i2].keys[i3], s, info, uvGroups[i2].interpolation);
			}
		}

	}

	/*! NIFLIB_HIDDEN function.  For internal use only. */
	internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

		base.Write(s, link_map, missing_link_stack, info);
		for (var i2 = 0; i2 < 4; i2++) {
			uvGroups[i2].numKeys = (uint)uvGroups[i2].keys.Count;
			Nif.NifStream(uvGroups[i2].numKeys, s, info);
			if ((uvGroups[i2].numKeys != 0)) {
				Nif.NifStream(uvGroups[i2].interpolation, s, info);
			}
			for (var i3 = 0; i3 < uvGroups[i2].keys.Count; i3++) {
				Nif.NifStream(uvGroups[i2].keys[i3], s, info, uvGroups[i2].interpolation);
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
		array_output_count = 0;
		for (var i2 = 0; i2 < 4; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			uvGroups[i2].numKeys = (uint)uvGroups[i2].keys.Count;
			s.AppendLine($"      Num Keys:  {uvGroups[i2].numKeys}");
			if ((uvGroups[i2].numKeys != 0)) {
				s.AppendLine($"        Interpolation:  {uvGroups[i2].interpolation}");
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < uvGroups[i2].keys.Count; i3++) {
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
					break;
				}
				s.AppendLine($"        Keys[{i3}]:  {uvGroups[i2].keys[i3]}");
				array_output_count++;
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