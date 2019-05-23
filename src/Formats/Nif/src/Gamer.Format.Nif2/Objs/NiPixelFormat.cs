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
	/*! NiPixelFormat */
	public class NiPixelFormat : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPixelFormat", NiObject.TYPE);

		/*! The format of the pixels in this internally stored image. */
		internal PixelFormat pixelFormat;
		/*! 0x000000ff (for 24bpp and 32bpp) or 0x00000000 (for 8bpp) */
		internal uint redMask;
		/*! 0x0000ff00 (for 24bpp and 32bpp) or 0x00000000 (for 8bpp) */
		internal uint greenMask;
		/*! 0x00ff0000 (for 24bpp and 32bpp) or 0x00000000 (for 8bpp) */
		internal uint blueMask;
		/*! 0xff000000 (for 32bpp) or 0x00000000 (for 24bpp and 8bpp) */
		internal uint alphaMask;
		/*! Bits per pixel, 0 (Compressed), 8, 24 or 32. */
		internal uint bitsPerPixel;
		/*!
		 * [96,8,130,0,0,65,0,0] if 24 bits per pixel
		 *             [129,8,130,32,0,65,12,0] if 32 bits per pixel
		 *             [34,0,0,0,0,0,0,0] if 8 bits per pixel
		 *             [X,0,0,0,0,0,0,0] if 0 (Compressed) bits per pixel where X = PixelFormat
		 */
		internal Array8<byte> oldFastCompare;
		/*! Seems to always be zero. */
		internal PixelTiling tiling;
		/*! rendererHint */
		internal uint rendererHint;
		/*! extraData */
		internal uint extraData;
		/*! flags */
		internal byte flags;
		/*! srgbSpace */
		internal bool srgbSpace;
		/*! Channel Data */
		internal Array4<PixelFormatComponent> channels;
		public NiPixelFormat()
		{
			pixelFormat = (PixelFormat)0;
			redMask = (uint)0;
			greenMask = (uint)0;
			blueMask = (uint)0;
			alphaMask = (uint)0;
			bitsPerPixel = (uint)0;
			tiling = (PixelTiling)0;
			rendererHint = (uint)0;
			extraData = (uint)0;
			flags = (byte)0;
			srgbSpace = false;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPixelFormat();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out pixelFormat, s, info);
			if (info.version <= 0x0A030002)
			{
				Nif.NifStream(out redMask, s, info);
				Nif.NifStream(out greenMask, s, info);
				Nif.NifStream(out blueMask, s, info);
				Nif.NifStream(out alphaMask, s, info);
				Nif.NifStream(out bitsPerPixel, s, info);
				for (var i4 = 0; i4 < 8; i4++)
				{
					Nif.NifStream(out oldFastCompare[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000 && info.version <= 0x0A030002)
			{
				Nif.NifStream(out tiling, s, info);
			}
			if (info.version >= 0x0A030003)
			{
				Nif.NifStream(out (byte)bitsPerPixel, s, info);
				Nif.NifStream(out rendererHint, s, info);
				Nif.NifStream(out extraData, s, info);
				Nif.NifStream(out flags, s, info);
				Nif.NifStream(out (PixelTiling)tiling, s, info);
			}
			if (info.version >= 0x14030004)
			{
				Nif.NifStream(out srgbSpace, s, info);
			}
			if (info.version >= 0x0A030003)
			{
				for (var i4 = 0; i4 < 4; i4++)
				{
					Nif.NifStream(out channels[i4].type, s, info);
					Nif.NifStream(out channels[i4].convention, s, info);
					Nif.NifStream(out channels[i4].bitsPerChannel, s, info);
					Nif.NifStream(out channels[i4].isSigned, s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(pixelFormat, s, info);
			if (info.version <= 0x0A030002)
			{
				Nif.NifStream(redMask, s, info);
				Nif.NifStream(greenMask, s, info);
				Nif.NifStream(blueMask, s, info);
				Nif.NifStream(alphaMask, s, info);
				Nif.NifStream(bitsPerPixel, s, info);
				for (var i4 = 0; i4 < 8; i4++)
				{
					Nif.NifStream(oldFastCompare[i4], s, info);
				}
			}
			if (info.version >= 0x0A010000 && info.version <= 0x0A030002)
			{
				Nif.NifStream(tiling, s, info);
			}
			if (info.version >= 0x0A030003)
			{
				Nif.NifStream((byte)bitsPerPixel, s, info);
				Nif.NifStream(rendererHint, s, info);
				Nif.NifStream(extraData, s, info);
				Nif.NifStream(flags, s, info);
				Nif.NifStream((PixelTiling)tiling, s, info);
			}
			if (info.version >= 0x14030004)
			{
				Nif.NifStream(srgbSpace, s, info);
			}
			if (info.version >= 0x0A030003)
			{
				for (var i4 = 0; i4 < 4; i4++)
				{
					Nif.NifStream(channels[i4].type, s, info);
					Nif.NifStream(channels[i4].convention, s, info);
					Nif.NifStream(channels[i4].bitsPerChannel, s, info);
					Nif.NifStream(channels[i4].isSigned, s, info);
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
			s.AppendLine($"      Pixel Format:  {pixelFormat}");
			s.AppendLine($"      Red Mask:  {redMask}");
			s.AppendLine($"      Green Mask:  {greenMask}");
			s.AppendLine($"      Blue Mask:  {blueMask}");
			s.AppendLine($"      Alpha Mask:  {alphaMask}");
			s.AppendLine($"      Bits Per Pixel:  {bitsPerPixel}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 8; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Old Fast Compare[{i3}]:  {oldFastCompare[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Tiling:  {tiling}");
			s.AppendLine($"      Renderer Hint:  {rendererHint}");
			s.AppendLine($"      Extra Data:  {extraData}");
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      sRGB Space:  {srgbSpace}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 4; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Type:  {channels[i3].type}");
				s.AppendLine($"        Convention:  {channels[i3].convention}");
				s.AppendLine($"        Bits Per Channel:  {channels[i3].bitsPerChannel}");
				s.AppendLine($"        Is Signed:  {channels[i3].isSigned}");
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			return ptrs;
		}
	}
}
