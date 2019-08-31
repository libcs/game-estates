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
	/*! LEGACY (pre-10.1) */
	public class NiImage : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiImage", NiObject.TYPE);

		/*! 0 if the texture is internal to the NIF file. */
		internal byte useExternal;
		/*! The filepath to the texture. */
		internal IndexString fileName;
		/*! Link to the internally stored image data. */
		internal NiRawImageData imageData;
		/*! Unknown.  Often seems to be 7. Perhaps m_uiMipLevels? */
		internal uint unknownInt;
		/*! Unknown.  Perhaps fImageScale? */
		internal float unknownFloat;
		public NiImage()
		{
			useExternal = (byte)0;
			imageData = null;
			unknownInt = (uint)7;
			unknownFloat = 128.5f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiImage();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out useExternal, s, info);
			if ((useExternal != 0))
			{
				Nif.NifStream(out fileName, s, info);
			}
			if ((useExternal == 0))
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out unknownInt, s, info);
			if (info.version >= 0x03010000)
			{
				Nif.NifStream(out unknownFloat, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(useExternal, s, info);
			if ((useExternal != 0))
			{
				Nif.NifStream(fileName, s, info);
			}
			if ((useExternal == 0))
			{
				WriteRef((NiObject)imageData, s, info, link_map, missing_link_stack);
			}
			Nif.NifStream(unknownInt, s, info);
			if (info.version >= 0x03010000)
			{
				Nif.NifStream(unknownFloat, s, info);
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
			s.AppendLine($"      Use External:  {useExternal}");
			if ((useExternal != 0))
			{
				s.AppendLine($"        File Name:  {fileName}");
			}
			if ((useExternal == 0))
			{
				s.AppendLine($"        Image Data:  {imageData}");
			}
			s.AppendLine($"      Unknown Int:  {unknownInt}");
			s.AppendLine($"      Unknown Float:  {unknownFloat}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if ((useExternal == 0))
			{
				imageData = FixLink<NiRawImageData>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (imageData != null)
				refs.Add((NiObject)imageData);
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
