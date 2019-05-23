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
	/*!
	 * LEGACY (pre-10.1)
	 *         Raw image data.
	 */
	public class NiRawImageData : NiObject
	{
		// Definition of TYPE constant
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
		public NiRawImageData()
		{
			width = (uint)0;
			height = (uint)0;
			imageType = (ImageType)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiRawImageData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out width, s, info);
			Nif.NifStream(out height, s, info);
			Nif.NifStream(out imageType, s, info);
			if ((imageType == 1))
			{
				rgbImageData = new ByteColor3[width];
				for (var i4 = 0; i4 < rgbImageData.Count; i4++)
				{
					rgbImageData[i4].Resize(height);
					for (var i5 = 0; i5 < rgbImageData[i4].Count; i5++)
					{
						Nif.NifStream(out rgbImageData[i4][i5].r, s, info);
						Nif.NifStream(out rgbImageData[i4][i5].g, s, info);
						Nif.NifStream(out rgbImageData[i4][i5].b, s, info);
					}
				}
			}
			if ((imageType == 2))
			{
				rgbaImageData = new ByteColor4[width];
				for (var i4 = 0; i4 < rgbaImageData.Count; i4++)
				{
					rgbaImageData[i4].Resize(height);
					for (var i5 = 0; i5 < rgbaImageData[i4].Count; i5++)
					{
						Nif.NifStream(out rgbaImageData[i4][i5].r, s, info);
						Nif.NifStream(out rgbaImageData[i4][i5].g, s, info);
						Nif.NifStream(out rgbaImageData[i4][i5].b, s, info);
						Nif.NifStream(out rgbaImageData[i4][i5].a, s, info);
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			height = (uint)(rgbImageData.Count > 0 ? rgbImageData[0].Count : 0);
			width = (uint)rgbImageData.Count;
			Nif.NifStream(width, s, info);
			Nif.NifStream(height, s, info);
			Nif.NifStream(imageType, s, info);
			if ((imageType == 1))
			{
				for (var i4 = 0; i4 < rgbImageData.Count; i4++)
				{
					for (var i5 = 0; i5 < rgbImageData[i4].Count; i5++)
					{
						Nif.NifStream(rgbImageData[i4][i5].r, s, info);
						Nif.NifStream(rgbImageData[i4][i5].g, s, info);
						Nif.NifStream(rgbImageData[i4][i5].b, s, info);
					}
				}
			}
			if ((imageType == 2))
			{
				for (var i4 = 0; i4 < rgbaImageData.Count; i4++)
				{
					for (var i5 = 0; i5 < rgbaImageData[i4].Count; i5++)
					{
						Nif.NifStream(rgbaImageData[i4][i5].r, s, info);
						Nif.NifStream(rgbaImageData[i4][i5].g, s, info);
						Nif.NifStream(rgbaImageData[i4][i5].b, s, info);
						Nif.NifStream(rgbaImageData[i4][i5].a, s, info);
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
			height = (uint)(rgbImageData.Count > 0 ? rgbImageData[0].Count : 0);
			width = (uint)rgbImageData.Count;
			s.AppendLine($"      Width:  {width}");
			s.AppendLine($"      Height:  {height}");
			s.AppendLine($"      Image Type:  {imageType}");
			if ((imageType == 1))
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < rgbImageData.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					for (var i5 = 0; i5 < rgbImageData[i4].Count; i5++)
					{
						s.AppendLine($"            r:  {rgbImageData[i4][i5].r}");
						s.AppendLine($"            g:  {rgbImageData[i4][i5].g}");
						s.AppendLine($"            b:  {rgbImageData[i4][i5].b}");
					}
				}
			}
			if ((imageType == 2))
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < rgbaImageData.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					for (var i5 = 0; i5 < rgbaImageData[i4].Count; i5++)
					{
						s.AppendLine($"            r:  {rgbaImageData[i4][i5].r}");
						s.AppendLine($"            g:  {rgbaImageData[i4][i5].g}");
						s.AppendLine($"            b:  {rgbaImageData[i4][i5].b}");
						s.AppendLine($"            a:  {rgbaImageData[i4][i5].a}");
					}
				}
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
