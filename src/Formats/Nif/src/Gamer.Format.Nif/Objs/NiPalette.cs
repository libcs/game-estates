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
 * NiPalette objects represent mappings from 8-bit indices to 24-bit RGB or 32-bit
 * RGBA colors.
 */
public class NiPalette : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiPalette", NiObject.TYPE);
	/*!  */
	internal byte hasAlpha;
	/*! The number of palette entries. Always 256 but can also be 16. */
	internal uint numEntries;
	/*! The color palette. */
	internal Array16<ByteColor4> palette;

	public NiPalette() {
	hasAlpha = (byte)0;
	numEntries = (uint)256;
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
public static NiObject Create() => new NiPalette();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out hasAlpha, s, info);
	Nif.NifStream(out numEntries, s, info);
	if ((numEntries == 16)) {
		for (var i2 = 0; i2 < 16; i2++) {
			Nif.NifStream(out palette[i2].r, s, info);
			Nif.NifStream(out palette[i2].g, s, info);
			Nif.NifStream(out palette[i2].b, s, info);
			Nif.NifStream(out palette[i2].a, s, info);
		}
	}
	if ((numEntries != 16)) {
		for (var i2 = 0; i2 < 256; i2++) {
			Nif.NifStream(out palette[i2].r, s, info);
			Nif.NifStream(out palette[i2].g, s, info);
			Nif.NifStream(out palette[i2].b, s, info);
			Nif.NifStream(out palette[i2].a, s, info);
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(hasAlpha, s, info);
	Nif.NifStream(numEntries, s, info);
	if ((numEntries == 16)) {
		for (var i2 = 0; i2 < 16; i2++) {
			Nif.NifStream(palette[i2].r, s, info);
			Nif.NifStream(palette[i2].g, s, info);
			Nif.NifStream(palette[i2].b, s, info);
			Nif.NifStream(palette[i2].a, s, info);
		}
	}
	if ((numEntries != 16)) {
		for (var i2 = 0; i2 < 256; i2++) {
			Nif.NifStream(palette[i2].r, s, info);
			Nif.NifStream(palette[i2].g, s, info);
			Nif.NifStream(palette[i2].b, s, info);
			Nif.NifStream(palette[i2].a, s, info);
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
	s.AppendLine($"  Has Alpha:  {hasAlpha}");
	s.AppendLine($"  Num Entries:  {numEntries}");
	if ((numEntries == 16)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < 16; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      r:  {palette[i2].r}");
			s.AppendLine($"      g:  {palette[i2].g}");
			s.AppendLine($"      b:  {palette[i2].b}");
			s.AppendLine($"      a:  {palette[i2].a}");
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