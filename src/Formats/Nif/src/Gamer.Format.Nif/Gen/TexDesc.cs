/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//---THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT---//

//To change this file, alter the /niflib/gen_niflib_cs Python script.

using System;
using System.IO;
using System.Collections.Generic;
namespace Niflib {

/*! NiTexturingProperty::Map. Texture description. */
public class TexDesc {
	/*! Link to the texture image. */
	internal NiImage image;
	/*! NiSourceTexture object index. */
	internal NiSourceTexture source;
	/*! 0=clamp S clamp T, 1=clamp S wrap T, 2=wrap S clamp T, 3=wrap S wrap T */
	internal TexClampMode clampMode;
	/*! 0=nearest, 1=bilinear, 2=trilinear, 3=..., 4=..., 5=... */
	internal TexFilterMode filterMode;
	/*!
	 * Texture mode flags; clamp and filter mode stored in upper byte with 0xYZ00 =
	 * clamp mode Y, filter mode Z.
	 */
	internal ushort flags;
	/*!  */
	internal ushort maxAnisotropy;
	/*! The texture coordinate set in NiGeometryData that this texture slot will use. */
	internal uint uvSet;
	/*! L can range from 0 to 3 and are used to specify how fast a texture gets blurry. */
	internal short ps2L;
	/*!
	 * K is used as an offset into the mipmap levels and can range from -2047 to 2047.
	 * Positive values push the mipmap towards being blurry and negative values make
	 * the mipmap sharper.
	 */
	internal short ps2K;
	/*! Unknown, 0 or 0x0101? */
	internal ushort unknown1;
	/*! Whether or not the texture coordinates are transformed. */
	internal bool hasTextureTransform;
	/*! The UV translation. */
	internal TexCoord translation;
	/*! The UV scale. */
	internal TexCoord scale;
	/*! The W axis rotation in texture space. */
	internal float rotation;
	/*! Depending on the source, scaling can occur before or after rotation. */
	internal TransformMethod transformMethod;
	/*! The origin around which the texture rotates. */
	internal TexCoord center;
	//Constructor
	public TexDesc() { unchecked {
	image = null;
	source = null;
	clampMode = TexClampMode.WRAP_S_WRAP_T;
	filterMode = TexFilterMode.FILTER_TRILERP;
	flags = (ushort)0;
	maxAnisotropy = (ushort)0;
	uvSet = (uint)0;
	ps2L = (short)0;
	ps2K = (short)-75;
	unknown1 = (ushort)0;
	hasTextureTransform = false;
	scale = 1.0, 1.0;
	rotation = 0.0f;
	transformMethod = (TransformMethod)0;
	
	} }

}

}
