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
	 * Changes the image a Map (TexDesc) will use. Uses a float interpolator to animate the texture index.
	 *         Often used for performing flipbook animation.
	 */
	public class NiFlipController : NiFloatInterpController
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiFlipController", NiFloatInterpController.TYPE);

		/*! Target texture slot (0=base, 4=glow). */
		internal TexType textureSlot;
		/*! startTime */
		internal float startTime;
		/*!
		 * Time between two flips.
		 *             delta = (start_time - stop_time) / sources.num_indices
		 */
		internal float delta;
		/*! numSources */
		internal uint numSources;
		/*! The texture sources. */
		internal IList<NiSourceTexture> sources;
		/*! The image sources */
		internal IList<NiImage> images;
		public NiFlipController()
		{
			textureSlot = (TexType)0;
			startTime = 0.0f;
			delta = 0.0f;
			numSources = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiFlipController();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out textureSlot, s, info);
			if (info.version >= 0x0303000D && info.version <= 0x0A010067)
			{
				Nif.NifStream(out startTime, s, info);
			}
			if (info.version <= 0x0A010067)
			{
				Nif.NifStream(out delta, s, info);
			}
			Nif.NifStream(out numSources, s, info);
			if (info.version >= 0x04000000)
			{
				sources = new Ref[numSources];
				for (var i4 = 0; i4 < sources.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
			if (info.version <= 0x03010000)
			{
				images = new Ref[numSources];
				for (var i4 = 0; i4 < images.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSources = (uint)sources.Count;
			Nif.NifStream(textureSlot, s, info);
			if (info.version >= 0x0303000D && info.version <= 0x0A010067)
			{
				Nif.NifStream(startTime, s, info);
			}
			if (info.version <= 0x0A010067)
			{
				Nif.NifStream(delta, s, info);
			}
			Nif.NifStream(numSources, s, info);
			if (info.version >= 0x04000000)
			{
				for (var i4 = 0; i4 < sources.Count; i4++)
				{
					WriteRef((NiObject)sources[i4], s, info, link_map, missing_link_stack);
				}
			}
			if (info.version <= 0x03010000)
			{
				for (var i4 = 0; i4 < images.Count; i4++)
				{
					WriteRef((NiObject)images[i4], s, info, link_map, missing_link_stack);
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
			numSources = (uint)sources.Count;
			s.AppendLine($"      Texture Slot:  {textureSlot}");
			s.AppendLine($"      Start Time:  {startTime}");
			s.AppendLine($"      Delta:  {delta}");
			s.AppendLine($"      Num Sources:  {numSources}");
			array_output_count = 0;
			for (var i3 = 0; i3 < sources.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Sources[{i3}]:  {sources[i3]}");
				array_output_count++;
			}
			array_output_count = 0;
			for (var i3 = 0; i3 < images.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Images[{i3}]:  {images[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x04000000)
			{
				for (var i4 = 0; i4 < sources.Count; i4++)
				{
					sources[i4] = FixLink<NiSourceTexture>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (info.version <= 0x03010000)
			{
				for (var i4 = 0; i4 < images.Count; i4++)
				{
					images[i4] = FixLink<NiImage>(objects, link_stack, missing_link_stack, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < sources.Count; i3++)
			{
				if (sources[i3] != null)
					refs.Add((NiObject)sources[i3]);
			}
			for (var i3 = 0; i3 < images.Count; i3++)
			{
				if (images[i3] != null)
					refs.Add((NiObject)images[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < sources.Count; i3++)
			{
			}
			for (var i3 = 0; i3 < images.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
