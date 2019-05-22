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

/*! Describes texture source and properties. */
public class NiSourceTexture : NiTexture {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiSourceTexture", NiTexture.TYPE);
	/*! Is the texture external? */
	internal byte useExternal;
	/*! The external texture file name. */
	internal IndexString fileName;
	/*! Unknown. */
	internal NiObject unknownLink;
	/*! Unknown. Seems to be set if Pixel Data is present? */
	internal byte unknownByte;
	/*! NiPixelData or NiPersistentSrcTextureRendererData */
	internal NiPixelFormat pixelData;
	/*!
	 * A set of preferences for the texture format. They are a request only and the
	 * renderer may ignore them.
	 */
	internal FormatPrefs formatPrefs;
	/*!
	 * If set, then the application cannot assume that any dynamic changes to the pixel
	 * data will show in the rendered image.
	 */
	internal byte isStatic;
	/*!
	 * A hint to the renderer that the texture can be loaded directly from a texture
	 * file into a renderer-specific resource, bypassing the NiPixelData object.
	 */
	internal bool directRender;
	/*! Pixel Data is NiPersistentSrcTextureRendererData instead of NiPixelData. */
	internal bool persistRenderData;

	public NiSourceTexture() {
	useExternal = (byte)1;
	unknownLink = null;
	unknownByte = (byte)1;
	pixelData = null;
	isStatic = (byte)1;
	directRender = 1;
	persistRenderData = 0;
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
public static NiObject Create() => new NiSourceTexture();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	Nif.NifStream(out useExternal, s, info);
	if ((useExternal == 1)) {
		Nif.NifStream(out fileName, s, info);
	}
	if (info.version >= 0x0A010000) {
		if ((useExternal == 1)) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}
	}
	if (info.version <= 0x0A000100) {
		if ((useExternal == 0)) {
			Nif.NifStream(out unknownByte, s, info);
		}
	}
	if (info.version >= 0x0A010000) {
		if ((useExternal == 0)) {
			Nif.NifStream(out (IndexString)fileName, s, info);
		}
	}
	if ((useExternal == 0)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
	}
	Nif.NifStream(out formatPrefs.pixelLayout, s, info);
	Nif.NifStream(out formatPrefs.useMipmaps, s, info);
	Nif.NifStream(out formatPrefs.alphaFormat, s, info);
	Nif.NifStream(out isStatic, s, info);
	if (info.version >= 0x0A010067) {
		Nif.NifStream(out directRender, s, info);
	}
	if (info.version >= 0x14020004) {
		Nif.NifStream(out persistRenderData, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	Nif.NifStream(useExternal, s, info);
	if ((useExternal == 1)) {
		Nif.NifStream(fileName, s, info);
	}
	if (info.version >= 0x0A010000) {
		if ((useExternal == 1)) {
			WriteRef((NiObject)unknownLink, s, info, link_map, missing_link_stack);
		}
	}
	if (info.version <= 0x0A000100) {
		if ((useExternal == 0)) {
			Nif.NifStream(unknownByte, s, info);
		}
	}
	if (info.version >= 0x0A010000) {
		if ((useExternal == 0)) {
			Nif.NifStream((IndexString)fileName, s, info);
		}
	}
	if ((useExternal == 0)) {
		WriteRef((NiObject)pixelData, s, info, link_map, missing_link_stack);
	}
	Nif.NifStream(formatPrefs.pixelLayout, s, info);
	Nif.NifStream(formatPrefs.useMipmaps, s, info);
	Nif.NifStream(formatPrefs.alphaFormat, s, info);
	Nif.NifStream(isStatic, s, info);
	if (info.version >= 0x0A010067) {
		Nif.NifStream(directRender, s, info);
	}
	if (info.version >= 0x14020004) {
		Nif.NifStream(persistRenderData, s, info);
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
	s.AppendLine($"  Use External:  {useExternal}");
	if ((useExternal == 1)) {
		s.AppendLine($"    File Name:  {fileName}");
		s.AppendLine($"    Unknown Link:  {unknownLink}");
	}
	if ((useExternal == 0)) {
		s.AppendLine($"    Unknown Byte:  {unknownByte}");
		s.AppendLine($"    Pixel Data:  {pixelData}");
	}
	s.AppendLine($"  Pixel Layout:  {formatPrefs.pixelLayout}");
	s.AppendLine($"  Use Mipmaps:  {formatPrefs.useMipmaps}");
	s.AppendLine($"  Alpha Format:  {formatPrefs.alphaFormat}");
	s.AppendLine($"  Is Static:  {isStatic}");
	s.AppendLine($"  Direct Render:  {directRender}");
	s.AppendLine($"  Persist Render Data:  {persistRenderData}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x0A010000) {
		if ((useExternal == 1)) {
			unknownLink = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((useExternal == 0)) {
		pixelData = FixLink<NiPixelFormat>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	if (unknownLink != null)
		refs.Add((NiObject)unknownLink);
	if (pixelData != null)
		refs.Add((NiObject)pixelData);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	return ptrs;
}


}

}