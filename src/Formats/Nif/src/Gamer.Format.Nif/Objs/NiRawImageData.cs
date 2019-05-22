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
 * LEGACY (pre-10.1)
 *         Raw image data.
 */
public class NiRawImageData : NiObject {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiRawImageData", NiObject.TYPE);
	/*! Image width */
	internal uint width;
	/*! Image height */
	internal uint height;
	/*! The format of the raw image data. */
	internal ImageType imageType;
	/*! Image pixel data. */
	internal IList<ByteColor3[]> rgbImageData;
	/*! Image pixel data. */
	internal IList<ByteColor4[]> rgbaImageData;

	public NiRawImageData() {
	width = (uint)0;
	height = (uint)0;
	imageType = (ImageType)0;
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
public static NiObject Create() => new NiRawImageData();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	base.Read(s, link_stack, info);
	Nif.NifStream(out width, s, info);
	Nif.NifStream(out height, s, info);
	Nif.NifStream(out imageType, s, info);
	if ((imageType == 1)) {
		rgbImageData = new ByteColor3[width];
		for (var i2 = 0; i2 < rgbImageData.Count; i2++) {
			rgbImageData[i2].Resize(height);
			for (var i3 = 0; i3 < rgbImageData[i2].Count; i3++) {
				Nif.NifStream(out rgbImageData[i2][i3].r, s, info);
				Nif.NifStream(out rgbImageData[i2][i3].g, s, info);
				Nif.NifStream(out rgbImageData[i2][i3].b, s, info);
			}
		}
	}
	if ((imageType == 2)) {
		rgbaImageData = new ByteColor4[width];
		for (var i2 = 0; i2 < rgbaImageData.Count; i2++) {
			rgbaImageData[i2].Resize(height);
			for (var i3 = 0; i3 < rgbaImageData[i2].Count; i3++) {
				Nif.NifStream(out rgbaImageData[i2][i3].r, s, info);
				Nif.NifStream(out rgbaImageData[i2][i3].g, s, info);
				Nif.NifStream(out rgbaImageData[i2][i3].b, s, info);
				Nif.NifStream(out rgbaImageData[i2][i3].a, s, info);
			}
		}
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	height = (uint)((rgbImageData.Count > 0) ? rgbImageData[0].Count : 0);
	width = (uint)rgbImageData.Count;
	Nif.NifStream(width, s, info);
	Nif.NifStream(height, s, info);
	Nif.NifStream(imageType, s, info);
	if ((imageType == 1)) {
		for (var i2 = 0; i2 < rgbImageData.Count; i2++) {
			for (var i3 = 0; i3 < rgbImageData[i2].Count; i3++) {
				Nif.NifStream(rgbImageData[i2][i3].r, s, info);
				Nif.NifStream(rgbImageData[i2][i3].g, s, info);
				Nif.NifStream(rgbImageData[i2][i3].b, s, info);
			}
		}
	}
	if ((imageType == 2)) {
		for (var i2 = 0; i2 < rgbaImageData.Count; i2++) {
			for (var i3 = 0; i3 < rgbaImageData[i2].Count; i3++) {
				Nif.NifStream(rgbaImageData[i2][i3].r, s, info);
				Nif.NifStream(rgbaImageData[i2][i3].g, s, info);
				Nif.NifStream(rgbaImageData[i2][i3].b, s, info);
				Nif.NifStream(rgbaImageData[i2][i3].a, s, info);
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
	height = (uint)((rgbImageData.Count > 0) ? rgbImageData[0].Count : 0);
	width = (uint)rgbImageData.Count;
	s.AppendLine($"  Width:  {width}");
	s.AppendLine($"  Height:  {height}");
	s.AppendLine($"  Image Type:  {imageType}");
	if ((imageType == 1)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < rgbImageData.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			for (var i3 = 0; i3 < rgbImageData[i2].Count; i3++) {
				s.AppendLine($"        r:  {rgbImageData[i2][i3].r}");
				s.AppendLine($"        g:  {rgbImageData[i2][i3].g}");
				s.AppendLine($"        b:  {rgbImageData[i2][i3].b}");
			}
		}
	}
	if ((imageType == 2)) {
		array_output_count = 0;
		for (var i2 = 0; i2 < rgbaImageData.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			for (var i3 = 0; i3 < rgbaImageData[i2].Count; i3++) {
				s.AppendLine($"        r:  {rgbaImageData[i2][i3].r}");
				s.AppendLine($"        g:  {rgbaImageData[i2][i3].g}");
				s.AppendLine($"        b:  {rgbaImageData[i2][i3].b}");
				s.AppendLine($"        a:  {rgbaImageData[i2][i3].a}");
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