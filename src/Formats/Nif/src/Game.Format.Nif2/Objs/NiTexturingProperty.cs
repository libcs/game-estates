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
	/*! Describes how a fragment shader should be configured for a given piece of geometry. */
	public class NiTexturingProperty : NiProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTexturingProperty", NiProperty.TYPE);

		/*! Property flags. */
		internal ushort flags;
		/*! Determines how the texture will be applied.  Seems to have special functions in Oblivion. */
		internal ApplyMode applyMode;
		/*! Number of textures. */
		internal uint textureCount;
		/*! Do we have a base texture? */
		internal bool hasBaseTexture;
		/*! The base texture. */
		internal TexDesc baseTexture;
		/*! Do we have a dark texture? */
		internal bool hasDarkTexture;
		/*! The dark texture. */
		internal TexDesc darkTexture;
		/*! Do we have a detail texture? */
		internal bool hasDetailTexture;
		/*! The detail texture. */
		internal TexDesc detailTexture;
		/*! Do we have a gloss texture? */
		internal bool hasGlossTexture;
		/*! The gloss texture. */
		internal TexDesc glossTexture;
		/*! Do we have a glow texture? */
		internal bool hasGlowTexture;
		/*! The glowing texture. */
		internal TexDesc glowTexture;
		/*! Do we have a bump map texture? */
		internal bool hasBumpMapTexture;
		/*! The bump map texture. */
		internal TexDesc bumpMapTexture;
		/*! bumpMapLumaScale */
		internal float bumpMapLumaScale;
		/*! bumpMapLumaOffset */
		internal float bumpMapLumaOffset;
		/*! bumpMapMatrix */
		internal Matrix22 bumpMapMatrix;
		/*! Do we have a normal texture? */
		internal bool hasNormalTexture;
		/*! Normal texture. */
		internal TexDesc normalTexture;
		/*! hasParallaxTexture */
		internal bool hasParallaxTexture;
		/*! parallaxTexture */
		internal TexDesc parallaxTexture;
		/*! parallaxOffset */
		internal float parallaxOffset;
		/*! Do we have a decal 0 texture? */
		internal bool hasDecal0Texture;
		/*! The decal texture. */
		internal TexDesc decal0Texture;
		/*! Do we have a decal 1 texture? */
		internal bool hasDecal1Texture;
		/*! Another decal texture. */
		internal TexDesc decal1Texture;
		/*! Do we have a decal 2 texture? */
		internal bool hasDecal2Texture;
		/*! Another decal texture. */
		internal TexDesc decal2Texture;
		/*! Do we have a decal 3 texture? */
		internal bool hasDecal3Texture;
		/*! Another decal texture. Who knows the limit. */
		internal TexDesc decal3Texture;
		/*! Number of Shader textures that follow. */
		internal uint numShaderTextures;
		/*! Shader textures. */
		internal IList<ShaderTexDesc> shaderTextures;
		public NiTexturingProperty()
		{
			flags = (ushort)0;
			applyMode = ApplyMode.APPLY_MODULATE;
			textureCount = (uint)7;
			hasBaseTexture = false;
			hasDarkTexture = false;
			hasDetailTexture = false;
			hasGlossTexture = false;
			hasGlowTexture = false;
			hasBumpMapTexture = false;
			bumpMapLumaScale = 0.0f;
			bumpMapLumaOffset = 0.0f;
			hasNormalTexture = false;
			hasParallaxTexture = false;
			parallaxOffset = 0.0f;
			hasDecal0Texture = false;
			hasDecal1Texture = false;
			hasDecal2Texture = false;
			hasDecal3Texture = false;
			numShaderTextures = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTexturingProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(out flags, s, info);
			}
			if (info.version >= 0x14010002)
			{
				Nif.NifStream(out (ushort)flags, s, info);
			}
			if (info.version >= 0x0303000D && info.version <= 0x14010001)
			{
				Nif.NifStream(out applyMode, s, info);
			}
			Nif.NifStream(out textureCount, s, info);
			Nif.NifStream(out hasBaseTexture, s, info);
			if (hasBaseTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out baseTexture.clampMode, s, info);
					Nif.NifStream(out baseTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out baseTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out baseTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out baseTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out baseTexture.ps2L, s, info);
					Nif.NifStream(out baseTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out baseTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out baseTexture.hasTextureTransform, s, info);
					if (baseTexture.hasTextureTransform)
					{
						Nif.NifStream(out baseTexture.translation, s, info);
						Nif.NifStream(out baseTexture.scale, s, info);
						Nif.NifStream(out baseTexture.rotation, s, info);
						Nif.NifStream(out baseTexture.transformMethod, s, info);
						Nif.NifStream(out baseTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(out hasDarkTexture, s, info);
			if (hasDarkTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out darkTexture.clampMode, s, info);
					Nif.NifStream(out darkTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out darkTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out darkTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out darkTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out darkTexture.ps2L, s, info);
					Nif.NifStream(out darkTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out darkTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out darkTexture.hasTextureTransform, s, info);
					if (darkTexture.hasTextureTransform)
					{
						Nif.NifStream(out darkTexture.translation, s, info);
						Nif.NifStream(out darkTexture.scale, s, info);
						Nif.NifStream(out darkTexture.rotation, s, info);
						Nif.NifStream(out darkTexture.transformMethod, s, info);
						Nif.NifStream(out darkTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(out hasDetailTexture, s, info);
			if (hasDetailTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out detailTexture.clampMode, s, info);
					Nif.NifStream(out detailTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out detailTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out detailTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out detailTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out detailTexture.ps2L, s, info);
					Nif.NifStream(out detailTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out detailTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out detailTexture.hasTextureTransform, s, info);
					if (detailTexture.hasTextureTransform)
					{
						Nif.NifStream(out detailTexture.translation, s, info);
						Nif.NifStream(out detailTexture.scale, s, info);
						Nif.NifStream(out detailTexture.rotation, s, info);
						Nif.NifStream(out detailTexture.transformMethod, s, info);
						Nif.NifStream(out detailTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(out hasGlossTexture, s, info);
			if (hasGlossTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out glossTexture.clampMode, s, info);
					Nif.NifStream(out glossTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out glossTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out glossTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out glossTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out glossTexture.ps2L, s, info);
					Nif.NifStream(out glossTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out glossTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out glossTexture.hasTextureTransform, s, info);
					if (glossTexture.hasTextureTransform)
					{
						Nif.NifStream(out glossTexture.translation, s, info);
						Nif.NifStream(out glossTexture.scale, s, info);
						Nif.NifStream(out glossTexture.rotation, s, info);
						Nif.NifStream(out glossTexture.transformMethod, s, info);
						Nif.NifStream(out glossTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(out hasGlowTexture, s, info);
			if (hasGlowTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out glowTexture.clampMode, s, info);
					Nif.NifStream(out glowTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out glowTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out glowTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out glowTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out glowTexture.ps2L, s, info);
					Nif.NifStream(out glowTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out glowTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out glowTexture.hasTextureTransform, s, info);
					if (glowTexture.hasTextureTransform)
					{
						Nif.NifStream(out glowTexture.translation, s, info);
						Nif.NifStream(out glowTexture.scale, s, info);
						Nif.NifStream(out glowTexture.rotation, s, info);
						Nif.NifStream(out glowTexture.transformMethod, s, info);
						Nif.NifStream(out glowTexture.center, s, info);
					}
				}
			}
			if (info.version >= 0x0303000D)
			{
				if ((textureCount > 5))
				{
					Nif.NifStream(out hasBumpMapTexture, s, info);
				}
			}
			if (hasBumpMapTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out bumpMapTexture.clampMode, s, info);
					Nif.NifStream(out bumpMapTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out bumpMapTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out bumpMapTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out bumpMapTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out bumpMapTexture.ps2L, s, info);
					Nif.NifStream(out bumpMapTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out bumpMapTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out bumpMapTexture.hasTextureTransform, s, info);
					if (bumpMapTexture.hasTextureTransform)
					{
						Nif.NifStream(out bumpMapTexture.translation, s, info);
						Nif.NifStream(out bumpMapTexture.scale, s, info);
						Nif.NifStream(out bumpMapTexture.rotation, s, info);
						Nif.NifStream(out bumpMapTexture.transformMethod, s, info);
						Nif.NifStream(out bumpMapTexture.center, s, info);
					}
				}
				Nif.NifStream(out bumpMapLumaScale, s, info);
				Nif.NifStream(out bumpMapLumaOffset, s, info);
				Nif.NifStream(out bumpMapMatrix, s, info);
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 6))
				{
					Nif.NifStream(out hasNormalTexture, s, info);
				}
			}
			if (hasNormalTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out normalTexture.clampMode, s, info);
					Nif.NifStream(out normalTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out normalTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out normalTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out normalTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out normalTexture.ps2L, s, info);
					Nif.NifStream(out normalTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out normalTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out normalTexture.hasTextureTransform, s, info);
					if (normalTexture.hasTextureTransform)
					{
						Nif.NifStream(out normalTexture.translation, s, info);
						Nif.NifStream(out normalTexture.scale, s, info);
						Nif.NifStream(out normalTexture.rotation, s, info);
						Nif.NifStream(out normalTexture.transformMethod, s, info);
						Nif.NifStream(out normalTexture.center, s, info);
					}
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 7))
				{
					Nif.NifStream(out hasParallaxTexture, s, info);
				}
			}
			if (hasParallaxTexture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out parallaxTexture.clampMode, s, info);
					Nif.NifStream(out parallaxTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out parallaxTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out parallaxTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out parallaxTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out parallaxTexture.ps2L, s, info);
					Nif.NifStream(out parallaxTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out parallaxTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out parallaxTexture.hasTextureTransform, s, info);
					if (parallaxTexture.hasTextureTransform)
					{
						Nif.NifStream(out parallaxTexture.translation, s, info);
						Nif.NifStream(out parallaxTexture.scale, s, info);
						Nif.NifStream(out parallaxTexture.rotation, s, info);
						Nif.NifStream(out parallaxTexture.transformMethod, s, info);
						Nif.NifStream(out parallaxTexture.center, s, info);
					}
				}
				Nif.NifStream(out parallaxOffset, s, info);
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 6))
				{
					Nif.NifStream(out hasDecal0Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 8))
				{
					Nif.NifStream(out (bool)hasDecal0Texture, s, info);
				}
			}
			if (hasDecal0Texture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal0Texture.clampMode, s, info);
					Nif.NifStream(out decal0Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out decal0Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out decal0Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal0Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out decal0Texture.ps2L, s, info);
					Nif.NifStream(out decal0Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out decal0Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out decal0Texture.hasTextureTransform, s, info);
					if (decal0Texture.hasTextureTransform)
					{
						Nif.NifStream(out decal0Texture.translation, s, info);
						Nif.NifStream(out decal0Texture.scale, s, info);
						Nif.NifStream(out decal0Texture.rotation, s, info);
						Nif.NifStream(out decal0Texture.transformMethod, s, info);
						Nif.NifStream(out decal0Texture.center, s, info);
					}
				}
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 7))
				{
					Nif.NifStream(out hasDecal1Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 9))
				{
					Nif.NifStream(out (bool)hasDecal1Texture, s, info);
				}
			}
			if (hasDecal1Texture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal1Texture.clampMode, s, info);
					Nif.NifStream(out decal1Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out decal1Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out decal1Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal1Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out decal1Texture.ps2L, s, info);
					Nif.NifStream(out decal1Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out decal1Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out decal1Texture.hasTextureTransform, s, info);
					if (decal1Texture.hasTextureTransform)
					{
						Nif.NifStream(out decal1Texture.translation, s, info);
						Nif.NifStream(out decal1Texture.scale, s, info);
						Nif.NifStream(out decal1Texture.rotation, s, info);
						Nif.NifStream(out decal1Texture.transformMethod, s, info);
						Nif.NifStream(out decal1Texture.center, s, info);
					}
				}
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 8))
				{
					Nif.NifStream(out hasDecal2Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 10))
				{
					Nif.NifStream(out (bool)hasDecal2Texture, s, info);
				}
			}
			if (hasDecal2Texture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal2Texture.clampMode, s, info);
					Nif.NifStream(out decal2Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out decal2Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out decal2Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal2Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out decal2Texture.ps2L, s, info);
					Nif.NifStream(out decal2Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out decal2Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out decal2Texture.hasTextureTransform, s, info);
					if (decal2Texture.hasTextureTransform)
					{
						Nif.NifStream(out decal2Texture.translation, s, info);
						Nif.NifStream(out decal2Texture.scale, s, info);
						Nif.NifStream(out decal2Texture.rotation, s, info);
						Nif.NifStream(out decal2Texture.transformMethod, s, info);
						Nif.NifStream(out decal2Texture.center, s, info);
					}
				}
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 9))
				{
					Nif.NifStream(out hasDecal3Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 11))
				{
					Nif.NifStream(out (bool)hasDecal3Texture, s, info);
				}
			}
			if (hasDecal3Texture)
			{
				if (info.version <= 0x03010000)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version >= 0x0303000D)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal3Texture.clampMode, s, info);
					Nif.NifStream(out decal3Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(out decal3Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(out decal3Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(out decal3Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(out decal3Texture.ps2L, s, info);
					Nif.NifStream(out decal3Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(out decal3Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(out decal3Texture.hasTextureTransform, s, info);
					if (decal3Texture.hasTextureTransform)
					{
						Nif.NifStream(out decal3Texture.translation, s, info);
						Nif.NifStream(out decal3Texture.scale, s, info);
						Nif.NifStream(out decal3Texture.rotation, s, info);
						Nif.NifStream(out decal3Texture.transformMethod, s, info);
						Nif.NifStream(out decal3Texture.center, s, info);
					}
				}
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(out numShaderTextures, s, info);
				shaderTextures = new ShaderTexDesc[numShaderTextures];
				for (var i4 = 0; i4 < shaderTextures.Count; i4++)
				{
					Nif.NifStream(out shaderTextures[i4].hasMap, s, info);
					if (shaderTextures[i4].hasMap)
					{
						if (info.version <= 0x03010000)
						{
							Nif.NifStream(out block_num, s, info);
							link_stack.Add(block_num);
						}
						if (info.version >= 0x0303000D)
						{
							Nif.NifStream(out block_num, s, info);
							link_stack.Add(block_num);
						}
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(out shaderTextures[i4].map.clampMode, s, info);
							Nif.NifStream(out shaderTextures[i4].map.filterMode, s, info);
						}
						if (info.version >= 0x14010003)
						{
							Nif.NifStream(out shaderTextures[i4].map.flags, s, info);
						}
						if (info.version >= 0x14050004)
						{
							Nif.NifStream(out shaderTextures[i4].map.maxAnisotropy, s, info);
						}
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(out shaderTextures[i4].map.uvSet, s, info);
						}
						if (info.version <= 0x0A040001)
						{
							Nif.NifStream(out shaderTextures[i4].map.ps2L, s, info);
							Nif.NifStream(out shaderTextures[i4].map.ps2K, s, info);
						}
						if (info.version <= 0x0401000C)
						{
							Nif.NifStream(out shaderTextures[i4].map.unknown1, s, info);
						}
						if (info.version >= 0x0A010000)
						{
							Nif.NifStream(out shaderTextures[i4].map.hasTextureTransform, s, info);
							if (shaderTextures[i4].map.hasTextureTransform)
							{
								Nif.NifStream(out shaderTextures[i4].map.translation, s, info);
								Nif.NifStream(out shaderTextures[i4].map.scale, s, info);
								Nif.NifStream(out shaderTextures[i4].map.rotation, s, info);
								Nif.NifStream(out shaderTextures[i4].map.transformMethod, s, info);
								Nif.NifStream(out shaderTextures[i4].map.center, s, info);
							}
						}
						Nif.NifStream(out shaderTextures[i4].mapId, s, info);
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numShaderTextures = (uint)shaderTextures.Count;
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(flags, s, info);
			}
			if (info.version >= 0x14010002)
			{
				Nif.NifStream((ushort)flags, s, info);
			}
			if (info.version >= 0x0303000D && info.version <= 0x14010001)
			{
				Nif.NifStream(applyMode, s, info);
			}
			Nif.NifStream(textureCount, s, info);
			Nif.NifStream(hasBaseTexture, s, info);
			if (hasBaseTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)baseTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)baseTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(baseTexture.clampMode, s, info);
					Nif.NifStream(baseTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(baseTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(baseTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(baseTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(baseTexture.ps2L, s, info);
					Nif.NifStream(baseTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(baseTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(baseTexture.hasTextureTransform, s, info);
					if (baseTexture.hasTextureTransform)
					{
						Nif.NifStream(baseTexture.translation, s, info);
						Nif.NifStream(baseTexture.scale, s, info);
						Nif.NifStream(baseTexture.rotation, s, info);
						Nif.NifStream(baseTexture.transformMethod, s, info);
						Nif.NifStream(baseTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(hasDarkTexture, s, info);
			if (hasDarkTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)darkTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)darkTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(darkTexture.clampMode, s, info);
					Nif.NifStream(darkTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(darkTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(darkTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(darkTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(darkTexture.ps2L, s, info);
					Nif.NifStream(darkTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(darkTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(darkTexture.hasTextureTransform, s, info);
					if (darkTexture.hasTextureTransform)
					{
						Nif.NifStream(darkTexture.translation, s, info);
						Nif.NifStream(darkTexture.scale, s, info);
						Nif.NifStream(darkTexture.rotation, s, info);
						Nif.NifStream(darkTexture.transformMethod, s, info);
						Nif.NifStream(darkTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(hasDetailTexture, s, info);
			if (hasDetailTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)detailTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)detailTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(detailTexture.clampMode, s, info);
					Nif.NifStream(detailTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(detailTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(detailTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(detailTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(detailTexture.ps2L, s, info);
					Nif.NifStream(detailTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(detailTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(detailTexture.hasTextureTransform, s, info);
					if (detailTexture.hasTextureTransform)
					{
						Nif.NifStream(detailTexture.translation, s, info);
						Nif.NifStream(detailTexture.scale, s, info);
						Nif.NifStream(detailTexture.rotation, s, info);
						Nif.NifStream(detailTexture.transformMethod, s, info);
						Nif.NifStream(detailTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(hasGlossTexture, s, info);
			if (hasGlossTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)glossTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)glossTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(glossTexture.clampMode, s, info);
					Nif.NifStream(glossTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(glossTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(glossTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(glossTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(glossTexture.ps2L, s, info);
					Nif.NifStream(glossTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(glossTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(glossTexture.hasTextureTransform, s, info);
					if (glossTexture.hasTextureTransform)
					{
						Nif.NifStream(glossTexture.translation, s, info);
						Nif.NifStream(glossTexture.scale, s, info);
						Nif.NifStream(glossTexture.rotation, s, info);
						Nif.NifStream(glossTexture.transformMethod, s, info);
						Nif.NifStream(glossTexture.center, s, info);
					}
				}
			}
			Nif.NifStream(hasGlowTexture, s, info);
			if (hasGlowTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)glowTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)glowTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(glowTexture.clampMode, s, info);
					Nif.NifStream(glowTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(glowTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(glowTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(glowTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(glowTexture.ps2L, s, info);
					Nif.NifStream(glowTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(glowTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(glowTexture.hasTextureTransform, s, info);
					if (glowTexture.hasTextureTransform)
					{
						Nif.NifStream(glowTexture.translation, s, info);
						Nif.NifStream(glowTexture.scale, s, info);
						Nif.NifStream(glowTexture.rotation, s, info);
						Nif.NifStream(glowTexture.transformMethod, s, info);
						Nif.NifStream(glowTexture.center, s, info);
					}
				}
			}
			if (info.version >= 0x0303000D)
			{
				if ((textureCount > 5))
				{
					Nif.NifStream(hasBumpMapTexture, s, info);
				}
			}
			if (hasBumpMapTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)bumpMapTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)bumpMapTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(bumpMapTexture.clampMode, s, info);
					Nif.NifStream(bumpMapTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(bumpMapTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(bumpMapTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(bumpMapTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(bumpMapTexture.ps2L, s, info);
					Nif.NifStream(bumpMapTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(bumpMapTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(bumpMapTexture.hasTextureTransform, s, info);
					if (bumpMapTexture.hasTextureTransform)
					{
						Nif.NifStream(bumpMapTexture.translation, s, info);
						Nif.NifStream(bumpMapTexture.scale, s, info);
						Nif.NifStream(bumpMapTexture.rotation, s, info);
						Nif.NifStream(bumpMapTexture.transformMethod, s, info);
						Nif.NifStream(bumpMapTexture.center, s, info);
					}
				}
				Nif.NifStream(bumpMapLumaScale, s, info);
				Nif.NifStream(bumpMapLumaOffset, s, info);
				Nif.NifStream(bumpMapMatrix, s, info);
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 6))
				{
					Nif.NifStream(hasNormalTexture, s, info);
				}
			}
			if (hasNormalTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)normalTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)normalTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(normalTexture.clampMode, s, info);
					Nif.NifStream(normalTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(normalTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(normalTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(normalTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(normalTexture.ps2L, s, info);
					Nif.NifStream(normalTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(normalTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(normalTexture.hasTextureTransform, s, info);
					if (normalTexture.hasTextureTransform)
					{
						Nif.NifStream(normalTexture.translation, s, info);
						Nif.NifStream(normalTexture.scale, s, info);
						Nif.NifStream(normalTexture.rotation, s, info);
						Nif.NifStream(normalTexture.transformMethod, s, info);
						Nif.NifStream(normalTexture.center, s, info);
					}
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 7))
				{
					Nif.NifStream(hasParallaxTexture, s, info);
				}
			}
			if (hasParallaxTexture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)parallaxTexture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)parallaxTexture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(parallaxTexture.clampMode, s, info);
					Nif.NifStream(parallaxTexture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(parallaxTexture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(parallaxTexture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(parallaxTexture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(parallaxTexture.ps2L, s, info);
					Nif.NifStream(parallaxTexture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(parallaxTexture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(parallaxTexture.hasTextureTransform, s, info);
					if (parallaxTexture.hasTextureTransform)
					{
						Nif.NifStream(parallaxTexture.translation, s, info);
						Nif.NifStream(parallaxTexture.scale, s, info);
						Nif.NifStream(parallaxTexture.rotation, s, info);
						Nif.NifStream(parallaxTexture.transformMethod, s, info);
						Nif.NifStream(parallaxTexture.center, s, info);
					}
				}
				Nif.NifStream(parallaxOffset, s, info);
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 6))
				{
					Nif.NifStream(hasDecal0Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 8))
				{
					Nif.NifStream((bool)hasDecal0Texture, s, info);
				}
			}
			if (hasDecal0Texture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)decal0Texture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)decal0Texture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal0Texture.clampMode, s, info);
					Nif.NifStream(decal0Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(decal0Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(decal0Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal0Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(decal0Texture.ps2L, s, info);
					Nif.NifStream(decal0Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(decal0Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(decal0Texture.hasTextureTransform, s, info);
					if (decal0Texture.hasTextureTransform)
					{
						Nif.NifStream(decal0Texture.translation, s, info);
						Nif.NifStream(decal0Texture.scale, s, info);
						Nif.NifStream(decal0Texture.rotation, s, info);
						Nif.NifStream(decal0Texture.transformMethod, s, info);
						Nif.NifStream(decal0Texture.center, s, info);
					}
				}
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 7))
				{
					Nif.NifStream(hasDecal1Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 9))
				{
					Nif.NifStream((bool)hasDecal1Texture, s, info);
				}
			}
			if (hasDecal1Texture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)decal1Texture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)decal1Texture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal1Texture.clampMode, s, info);
					Nif.NifStream(decal1Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(decal1Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(decal1Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal1Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(decal1Texture.ps2L, s, info);
					Nif.NifStream(decal1Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(decal1Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(decal1Texture.hasTextureTransform, s, info);
					if (decal1Texture.hasTextureTransform)
					{
						Nif.NifStream(decal1Texture.translation, s, info);
						Nif.NifStream(decal1Texture.scale, s, info);
						Nif.NifStream(decal1Texture.rotation, s, info);
						Nif.NifStream(decal1Texture.transformMethod, s, info);
						Nif.NifStream(decal1Texture.center, s, info);
					}
				}
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 8))
				{
					Nif.NifStream(hasDecal2Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 10))
				{
					Nif.NifStream((bool)hasDecal2Texture, s, info);
				}
			}
			if (hasDecal2Texture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)decal2Texture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)decal2Texture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal2Texture.clampMode, s, info);
					Nif.NifStream(decal2Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(decal2Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(decal2Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal2Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(decal2Texture.ps2L, s, info);
					Nif.NifStream(decal2Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(decal2Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(decal2Texture.hasTextureTransform, s, info);
					if (decal2Texture.hasTextureTransform)
					{
						Nif.NifStream(decal2Texture.translation, s, info);
						Nif.NifStream(decal2Texture.scale, s, info);
						Nif.NifStream(decal2Texture.rotation, s, info);
						Nif.NifStream(decal2Texture.transformMethod, s, info);
						Nif.NifStream(decal2Texture.center, s, info);
					}
				}
			}
			if (info.version <= 0x14020004)
			{
				if ((textureCount > 9))
				{
					Nif.NifStream(hasDecal3Texture, s, info);
				}
			}
			if (info.version >= 0x14020005)
			{
				if ((textureCount > 11))
				{
					Nif.NifStream((bool)hasDecal3Texture, s, info);
				}
			}
			if (hasDecal3Texture)
			{
				if (info.version <= 0x03010000)
				{
					WriteRef((NiObject)decal3Texture.image, s, info, link_map, missing_link_stack);
				}
				if (info.version >= 0x0303000D)
				{
					WriteRef((NiObject)decal3Texture.source, s, info, link_map, missing_link_stack);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal3Texture.clampMode, s, info);
					Nif.NifStream(decal3Texture.filterMode, s, info);
				}
				if (info.version >= 0x14010003)
				{
					Nif.NifStream(decal3Texture.flags, s, info);
				}
				if (info.version >= 0x14050004)
				{
					Nif.NifStream(decal3Texture.maxAnisotropy, s, info);
				}
				if (info.version <= 0x14000005)
				{
					Nif.NifStream(decal3Texture.uvSet, s, info);
				}
				if (info.version <= 0x0A040001)
				{
					Nif.NifStream(decal3Texture.ps2L, s, info);
					Nif.NifStream(decal3Texture.ps2K, s, info);
				}
				if (info.version <= 0x0401000C)
				{
					Nif.NifStream(decal3Texture.unknown1, s, info);
				}
				if (info.version >= 0x0A010000)
				{
					Nif.NifStream(decal3Texture.hasTextureTransform, s, info);
					if (decal3Texture.hasTextureTransform)
					{
						Nif.NifStream(decal3Texture.translation, s, info);
						Nif.NifStream(decal3Texture.scale, s, info);
						Nif.NifStream(decal3Texture.rotation, s, info);
						Nif.NifStream(decal3Texture.transformMethod, s, info);
						Nif.NifStream(decal3Texture.center, s, info);
					}
				}
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(numShaderTextures, s, info);
				for (var i4 = 0; i4 < shaderTextures.Count; i4++)
				{
					Nif.NifStream(shaderTextures[i4].hasMap, s, info);
					if (shaderTextures[i4].hasMap)
					{
						if (info.version <= 0x03010000)
						{
							WriteRef((NiObject)shaderTextures[i4].map.image, s, info, link_map, missing_link_stack);
						}
						if (info.version >= 0x0303000D)
						{
							WriteRef((NiObject)shaderTextures[i4].map.source, s, info, link_map, missing_link_stack);
						}
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(shaderTextures[i4].map.clampMode, s, info);
							Nif.NifStream(shaderTextures[i4].map.filterMode, s, info);
						}
						if (info.version >= 0x14010003)
						{
							Nif.NifStream(shaderTextures[i4].map.flags, s, info);
						}
						if (info.version >= 0x14050004)
						{
							Nif.NifStream(shaderTextures[i4].map.maxAnisotropy, s, info);
						}
						if (info.version <= 0x14000005)
						{
							Nif.NifStream(shaderTextures[i4].map.uvSet, s, info);
						}
						if (info.version <= 0x0A040001)
						{
							Nif.NifStream(shaderTextures[i4].map.ps2L, s, info);
							Nif.NifStream(shaderTextures[i4].map.ps2K, s, info);
						}
						if (info.version <= 0x0401000C)
						{
							Nif.NifStream(shaderTextures[i4].map.unknown1, s, info);
						}
						if (info.version >= 0x0A010000)
						{
							Nif.NifStream(shaderTextures[i4].map.hasTextureTransform, s, info);
							if (shaderTextures[i4].map.hasTextureTransform)
							{
								Nif.NifStream(shaderTextures[i4].map.translation, s, info);
								Nif.NifStream(shaderTextures[i4].map.scale, s, info);
								Nif.NifStream(shaderTextures[i4].map.rotation, s, info);
								Nif.NifStream(shaderTextures[i4].map.transformMethod, s, info);
								Nif.NifStream(shaderTextures[i4].map.center, s, info);
							}
						}
						Nif.NifStream(shaderTextures[i4].mapId, s, info);
					}
				}
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
			numShaderTextures = (uint)shaderTextures.Count;
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Apply Mode:  {applyMode}");
			s.AppendLine($"      Texture Count:  {textureCount}");
			s.AppendLine($"      Has Base Texture:  {hasBaseTexture}");
			if (hasBaseTexture)
			{
				s.AppendLine($"        Image:  {baseTexture.image}");
				s.AppendLine($"        Source:  {baseTexture.source}");
				s.AppendLine($"        Clamp Mode:  {baseTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {baseTexture.filterMode}");
				s.AppendLine($"        Flags:  {baseTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {baseTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {baseTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {baseTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {baseTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {baseTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {baseTexture.hasTextureTransform}");
				if (baseTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {baseTexture.translation}");
					s.AppendLine($"          Scale:  {baseTexture.scale}");
					s.AppendLine($"          Rotation:  {baseTexture.rotation}");
					s.AppendLine($"          Transform Method:  {baseTexture.transformMethod}");
					s.AppendLine($"          Center:  {baseTexture.center}");
				}
			}
			s.AppendLine($"      Has Dark Texture:  {hasDarkTexture}");
			if (hasDarkTexture)
			{
				s.AppendLine($"        Image:  {darkTexture.image}");
				s.AppendLine($"        Source:  {darkTexture.source}");
				s.AppendLine($"        Clamp Mode:  {darkTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {darkTexture.filterMode}");
				s.AppendLine($"        Flags:  {darkTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {darkTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {darkTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {darkTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {darkTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {darkTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {darkTexture.hasTextureTransform}");
				if (darkTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {darkTexture.translation}");
					s.AppendLine($"          Scale:  {darkTexture.scale}");
					s.AppendLine($"          Rotation:  {darkTexture.rotation}");
					s.AppendLine($"          Transform Method:  {darkTexture.transformMethod}");
					s.AppendLine($"          Center:  {darkTexture.center}");
				}
			}
			s.AppendLine($"      Has Detail Texture:  {hasDetailTexture}");
			if (hasDetailTexture)
			{
				s.AppendLine($"        Image:  {detailTexture.image}");
				s.AppendLine($"        Source:  {detailTexture.source}");
				s.AppendLine($"        Clamp Mode:  {detailTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {detailTexture.filterMode}");
				s.AppendLine($"        Flags:  {detailTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {detailTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {detailTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {detailTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {detailTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {detailTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {detailTexture.hasTextureTransform}");
				if (detailTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {detailTexture.translation}");
					s.AppendLine($"          Scale:  {detailTexture.scale}");
					s.AppendLine($"          Rotation:  {detailTexture.rotation}");
					s.AppendLine($"          Transform Method:  {detailTexture.transformMethod}");
					s.AppendLine($"          Center:  {detailTexture.center}");
				}
			}
			s.AppendLine($"      Has Gloss Texture:  {hasGlossTexture}");
			if (hasGlossTexture)
			{
				s.AppendLine($"        Image:  {glossTexture.image}");
				s.AppendLine($"        Source:  {glossTexture.source}");
				s.AppendLine($"        Clamp Mode:  {glossTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {glossTexture.filterMode}");
				s.AppendLine($"        Flags:  {glossTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {glossTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {glossTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {glossTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {glossTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {glossTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {glossTexture.hasTextureTransform}");
				if (glossTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {glossTexture.translation}");
					s.AppendLine($"          Scale:  {glossTexture.scale}");
					s.AppendLine($"          Rotation:  {glossTexture.rotation}");
					s.AppendLine($"          Transform Method:  {glossTexture.transformMethod}");
					s.AppendLine($"          Center:  {glossTexture.center}");
				}
			}
			s.AppendLine($"      Has Glow Texture:  {hasGlowTexture}");
			if (hasGlowTexture)
			{
				s.AppendLine($"        Image:  {glowTexture.image}");
				s.AppendLine($"        Source:  {glowTexture.source}");
				s.AppendLine($"        Clamp Mode:  {glowTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {glowTexture.filterMode}");
				s.AppendLine($"        Flags:  {glowTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {glowTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {glowTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {glowTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {glowTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {glowTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {glowTexture.hasTextureTransform}");
				if (glowTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {glowTexture.translation}");
					s.AppendLine($"          Scale:  {glowTexture.scale}");
					s.AppendLine($"          Rotation:  {glowTexture.rotation}");
					s.AppendLine($"          Transform Method:  {glowTexture.transformMethod}");
					s.AppendLine($"          Center:  {glowTexture.center}");
				}
			}
			if ((textureCount > 5))
			{
				s.AppendLine($"        Has Bump Map Texture:  {hasBumpMapTexture}");
			}
			if (hasBumpMapTexture)
			{
				s.AppendLine($"        Image:  {bumpMapTexture.image}");
				s.AppendLine($"        Source:  {bumpMapTexture.source}");
				s.AppendLine($"        Clamp Mode:  {bumpMapTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {bumpMapTexture.filterMode}");
				s.AppendLine($"        Flags:  {bumpMapTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {bumpMapTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {bumpMapTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {bumpMapTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {bumpMapTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {bumpMapTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {bumpMapTexture.hasTextureTransform}");
				if (bumpMapTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {bumpMapTexture.translation}");
					s.AppendLine($"          Scale:  {bumpMapTexture.scale}");
					s.AppendLine($"          Rotation:  {bumpMapTexture.rotation}");
					s.AppendLine($"          Transform Method:  {bumpMapTexture.transformMethod}");
					s.AppendLine($"          Center:  {bumpMapTexture.center}");
				}
				s.AppendLine($"        Bump Map Luma Scale:  {bumpMapLumaScale}");
				s.AppendLine($"        Bump Map Luma Offset:  {bumpMapLumaOffset}");
				s.AppendLine($"        Bump Map Matrix:  {bumpMapMatrix}");
			}
			if ((textureCount > 6))
			{
				s.AppendLine($"        Has Normal Texture:  {hasNormalTexture}");
			}
			if (hasNormalTexture)
			{
				s.AppendLine($"        Image:  {normalTexture.image}");
				s.AppendLine($"        Source:  {normalTexture.source}");
				s.AppendLine($"        Clamp Mode:  {normalTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {normalTexture.filterMode}");
				s.AppendLine($"        Flags:  {normalTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {normalTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {normalTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {normalTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {normalTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {normalTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {normalTexture.hasTextureTransform}");
				if (normalTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {normalTexture.translation}");
					s.AppendLine($"          Scale:  {normalTexture.scale}");
					s.AppendLine($"          Rotation:  {normalTexture.rotation}");
					s.AppendLine($"          Transform Method:  {normalTexture.transformMethod}");
					s.AppendLine($"          Center:  {normalTexture.center}");
				}
			}
			if ((textureCount > 7))
			{
				s.AppendLine($"        Has Parallax Texture:  {hasParallaxTexture}");
			}
			if (hasParallaxTexture)
			{
				s.AppendLine($"        Image:  {parallaxTexture.image}");
				s.AppendLine($"        Source:  {parallaxTexture.source}");
				s.AppendLine($"        Clamp Mode:  {parallaxTexture.clampMode}");
				s.AppendLine($"        Filter Mode:  {parallaxTexture.filterMode}");
				s.AppendLine($"        Flags:  {parallaxTexture.flags}");
				s.AppendLine($"        Max Anisotropy:  {parallaxTexture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {parallaxTexture.uvSet}");
				s.AppendLine($"        PS2 L:  {parallaxTexture.ps2L}");
				s.AppendLine($"        PS2 K:  {parallaxTexture.ps2K}");
				s.AppendLine($"        Unknown1:  {parallaxTexture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {parallaxTexture.hasTextureTransform}");
				if (parallaxTexture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {parallaxTexture.translation}");
					s.AppendLine($"          Scale:  {parallaxTexture.scale}");
					s.AppendLine($"          Rotation:  {parallaxTexture.rotation}");
					s.AppendLine($"          Transform Method:  {parallaxTexture.transformMethod}");
					s.AppendLine($"          Center:  {parallaxTexture.center}");
				}
				s.AppendLine($"        Parallax Offset:  {parallaxOffset}");
			}
			if ((textureCount > 6))
			{
				s.AppendLine($"        Has Decal 0 Texture:  {hasDecal0Texture}");
			}
			if (hasDecal0Texture)
			{
				s.AppendLine($"        Image:  {decal0Texture.image}");
				s.AppendLine($"        Source:  {decal0Texture.source}");
				s.AppendLine($"        Clamp Mode:  {decal0Texture.clampMode}");
				s.AppendLine($"        Filter Mode:  {decal0Texture.filterMode}");
				s.AppendLine($"        Flags:  {decal0Texture.flags}");
				s.AppendLine($"        Max Anisotropy:  {decal0Texture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {decal0Texture.uvSet}");
				s.AppendLine($"        PS2 L:  {decal0Texture.ps2L}");
				s.AppendLine($"        PS2 K:  {decal0Texture.ps2K}");
				s.AppendLine($"        Unknown1:  {decal0Texture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {decal0Texture.hasTextureTransform}");
				if (decal0Texture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {decal0Texture.translation}");
					s.AppendLine($"          Scale:  {decal0Texture.scale}");
					s.AppendLine($"          Rotation:  {decal0Texture.rotation}");
					s.AppendLine($"          Transform Method:  {decal0Texture.transformMethod}");
					s.AppendLine($"          Center:  {decal0Texture.center}");
				}
			}
			if ((textureCount > 7))
			{
				s.AppendLine($"        Has Decal 1 Texture:  {hasDecal1Texture}");
			}
			if (hasDecal1Texture)
			{
				s.AppendLine($"        Image:  {decal1Texture.image}");
				s.AppendLine($"        Source:  {decal1Texture.source}");
				s.AppendLine($"        Clamp Mode:  {decal1Texture.clampMode}");
				s.AppendLine($"        Filter Mode:  {decal1Texture.filterMode}");
				s.AppendLine($"        Flags:  {decal1Texture.flags}");
				s.AppendLine($"        Max Anisotropy:  {decal1Texture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {decal1Texture.uvSet}");
				s.AppendLine($"        PS2 L:  {decal1Texture.ps2L}");
				s.AppendLine($"        PS2 K:  {decal1Texture.ps2K}");
				s.AppendLine($"        Unknown1:  {decal1Texture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {decal1Texture.hasTextureTransform}");
				if (decal1Texture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {decal1Texture.translation}");
					s.AppendLine($"          Scale:  {decal1Texture.scale}");
					s.AppendLine($"          Rotation:  {decal1Texture.rotation}");
					s.AppendLine($"          Transform Method:  {decal1Texture.transformMethod}");
					s.AppendLine($"          Center:  {decal1Texture.center}");
				}
			}
			if ((textureCount > 8))
			{
				s.AppendLine($"        Has Decal 2 Texture:  {hasDecal2Texture}");
			}
			if (hasDecal2Texture)
			{
				s.AppendLine($"        Image:  {decal2Texture.image}");
				s.AppendLine($"        Source:  {decal2Texture.source}");
				s.AppendLine($"        Clamp Mode:  {decal2Texture.clampMode}");
				s.AppendLine($"        Filter Mode:  {decal2Texture.filterMode}");
				s.AppendLine($"        Flags:  {decal2Texture.flags}");
				s.AppendLine($"        Max Anisotropy:  {decal2Texture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {decal2Texture.uvSet}");
				s.AppendLine($"        PS2 L:  {decal2Texture.ps2L}");
				s.AppendLine($"        PS2 K:  {decal2Texture.ps2K}");
				s.AppendLine($"        Unknown1:  {decal2Texture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {decal2Texture.hasTextureTransform}");
				if (decal2Texture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {decal2Texture.translation}");
					s.AppendLine($"          Scale:  {decal2Texture.scale}");
					s.AppendLine($"          Rotation:  {decal2Texture.rotation}");
					s.AppendLine($"          Transform Method:  {decal2Texture.transformMethod}");
					s.AppendLine($"          Center:  {decal2Texture.center}");
				}
			}
			if ((textureCount > 9))
			{
				s.AppendLine($"        Has Decal 3 Texture:  {hasDecal3Texture}");
			}
			if (hasDecal3Texture)
			{
				s.AppendLine($"        Image:  {decal3Texture.image}");
				s.AppendLine($"        Source:  {decal3Texture.source}");
				s.AppendLine($"        Clamp Mode:  {decal3Texture.clampMode}");
				s.AppendLine($"        Filter Mode:  {decal3Texture.filterMode}");
				s.AppendLine($"        Flags:  {decal3Texture.flags}");
				s.AppendLine($"        Max Anisotropy:  {decal3Texture.maxAnisotropy}");
				s.AppendLine($"        UV Set:  {decal3Texture.uvSet}");
				s.AppendLine($"        PS2 L:  {decal3Texture.ps2L}");
				s.AppendLine($"        PS2 K:  {decal3Texture.ps2K}");
				s.AppendLine($"        Unknown1:  {decal3Texture.unknown1}");
				s.AppendLine($"        Has Texture Transform:  {decal3Texture.hasTextureTransform}");
				if (decal3Texture.hasTextureTransform)
				{
					s.AppendLine($"          Translation:  {decal3Texture.translation}");
					s.AppendLine($"          Scale:  {decal3Texture.scale}");
					s.AppendLine($"          Rotation:  {decal3Texture.rotation}");
					s.AppendLine($"          Transform Method:  {decal3Texture.transformMethod}");
					s.AppendLine($"          Center:  {decal3Texture.center}");
				}
			}
			s.AppendLine($"      Num Shader Textures:  {numShaderTextures}");
			array_output_count = 0;
			for (var i3 = 0; i3 < shaderTextures.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Has Map:  {shaderTextures[i3].hasMap}");
				if (shaderTextures[i3].hasMap)
				{
					s.AppendLine($"          Image:  {shaderTextures[i3].map.image}");
					s.AppendLine($"          Source:  {shaderTextures[i3].map.source}");
					s.AppendLine($"          Clamp Mode:  {shaderTextures[i3].map.clampMode}");
					s.AppendLine($"          Filter Mode:  {shaderTextures[i3].map.filterMode}");
					s.AppendLine($"          Flags:  {shaderTextures[i3].map.flags}");
					s.AppendLine($"          Max Anisotropy:  {shaderTextures[i3].map.maxAnisotropy}");
					s.AppendLine($"          UV Set:  {shaderTextures[i3].map.uvSet}");
					s.AppendLine($"          PS2 L:  {shaderTextures[i3].map.ps2L}");
					s.AppendLine($"          PS2 K:  {shaderTextures[i3].map.ps2K}");
					s.AppendLine($"          Unknown1:  {shaderTextures[i3].map.unknown1}");
					s.AppendLine($"          Has Texture Transform:  {shaderTextures[i3].map.hasTextureTransform}");
					if (shaderTextures[i3].map.hasTextureTransform)
					{
						s.AppendLine($"            Translation:  {shaderTextures[i3].map.translation}");
						s.AppendLine($"            Scale:  {shaderTextures[i3].map.scale}");
						s.AppendLine($"            Rotation:  {shaderTextures[i3].map.rotation}");
						s.AppendLine($"            Transform Method:  {shaderTextures[i3].map.transformMethod}");
						s.AppendLine($"            Center:  {shaderTextures[i3].map.center}");
					}
					s.AppendLine($"          Map ID:  {shaderTextures[i3].mapId}");
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (hasBaseTexture)
			{
				if (info.version <= 0x03010000)
				{
					baseTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					baseTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasDarkTexture)
			{
				if (info.version <= 0x03010000)
				{
					darkTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					darkTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasDetailTexture)
			{
				if (info.version <= 0x03010000)
				{
					detailTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					detailTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasGlossTexture)
			{
				if (info.version <= 0x03010000)
				{
					glossTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					glossTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasGlowTexture)
			{
				if (info.version <= 0x03010000)
				{
					glowTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					glowTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasBumpMapTexture)
			{
				if (info.version <= 0x03010000)
				{
					bumpMapTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					bumpMapTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasNormalTexture)
			{
				if (info.version <= 0x03010000)
				{
					normalTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					normalTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasParallaxTexture)
			{
				if (info.version <= 0x03010000)
				{
					parallaxTexture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					parallaxTexture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasDecal0Texture)
			{
				if (info.version <= 0x03010000)
				{
					decal0Texture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					decal0Texture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasDecal1Texture)
			{
				if (info.version <= 0x03010000)
				{
					decal1Texture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					decal1Texture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasDecal2Texture)
			{
				if (info.version <= 0x03010000)
				{
					decal2Texture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					decal2Texture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (hasDecal3Texture)
			{
				if (info.version <= 0x03010000)
				{
					decal3Texture.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
				if (info.version >= 0x0303000D)
				{
					decal3Texture.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (info.version >= 0x0A000100)
			{
				for (var i4 = 0; i4 < shaderTextures.Count; i4++)
				{
					if (shaderTextures[i4].hasMap)
					{
						if (info.version <= 0x03010000)
						{
							shaderTextures[i4].map.image = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
						}
						if (info.version >= 0x0303000D)
						{
							shaderTextures[i4].map.source = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
						}
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (baseTexture.image != null)
				refs.Add((NiObject)baseTexture.image);
			if (baseTexture.source != null)
				refs.Add((NiObject)baseTexture.source);
			if (darkTexture.image != null)
				refs.Add((NiObject)darkTexture.image);
			if (darkTexture.source != null)
				refs.Add((NiObject)darkTexture.source);
			if (detailTexture.image != null)
				refs.Add((NiObject)detailTexture.image);
			if (detailTexture.source != null)
				refs.Add((NiObject)detailTexture.source);
			if (glossTexture.image != null)
				refs.Add((NiObject)glossTexture.image);
			if (glossTexture.source != null)
				refs.Add((NiObject)glossTexture.source);
			if (glowTexture.image != null)
				refs.Add((NiObject)glowTexture.image);
			if (glowTexture.source != null)
				refs.Add((NiObject)glowTexture.source);
			if (bumpMapTexture.image != null)
				refs.Add((NiObject)bumpMapTexture.image);
			if (bumpMapTexture.source != null)
				refs.Add((NiObject)bumpMapTexture.source);
			if (normalTexture.image != null)
				refs.Add((NiObject)normalTexture.image);
			if (normalTexture.source != null)
				refs.Add((NiObject)normalTexture.source);
			if (parallaxTexture.image != null)
				refs.Add((NiObject)parallaxTexture.image);
			if (parallaxTexture.source != null)
				refs.Add((NiObject)parallaxTexture.source);
			if (decal0Texture.image != null)
				refs.Add((NiObject)decal0Texture.image);
			if (decal0Texture.source != null)
				refs.Add((NiObject)decal0Texture.source);
			if (decal1Texture.image != null)
				refs.Add((NiObject)decal1Texture.image);
			if (decal1Texture.source != null)
				refs.Add((NiObject)decal1Texture.source);
			if (decal2Texture.image != null)
				refs.Add((NiObject)decal2Texture.image);
			if (decal2Texture.source != null)
				refs.Add((NiObject)decal2Texture.source);
			if (decal3Texture.image != null)
				refs.Add((NiObject)decal3Texture.image);
			if (decal3Texture.source != null)
				refs.Add((NiObject)decal3Texture.source);
			for (var i3 = 0; i3 < shaderTextures.Count; i3++)
			{
				if (shaderTextures[i3].map.image != null)
					refs.Add((NiObject)shaderTextures[i3].map.image);
				if (shaderTextures[i3].map.source != null)
					refs.Add((NiObject)shaderTextures[i3].map.source);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < shaderTextures.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
