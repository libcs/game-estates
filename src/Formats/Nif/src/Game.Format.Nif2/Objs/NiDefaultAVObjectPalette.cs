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
	/*! NiAVObjectPalette implementation. Used to quickly look up objects by name. */
	public class NiDefaultAVObjectPalette : NiAVObjectPalette
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiDefaultAVObjectPalette", NiAVObjectPalette.TYPE);

		/*! Scene root of the object palette. */
		internal NiAVObject scene;
		/*! Number of objects. */
		internal uint numObjs;
		/*! The objects. */
		internal IList<AVObject> objs;
		public NiDefaultAVObjectPalette()
		{
			scene = null;
			numObjs = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiDefaultAVObjectPalette();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out numObjs, s, info);
			objs = new AVObject[numObjs];
			for (var i3 = 0; i3 < objs.Count; i3++)
			{
				Nif.NifStream(out objs[i3].name, s, info);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numObjs = (uint)objs.Count;
			WriteRef((NiObject)scene, s, info, link_map, missing_link_stack);
			Nif.NifStream(numObjs, s, info);
			for (var i3 = 0; i3 < objs.Count; i3++)
			{
				Nif.NifStream(objs[i3].name, s, info);
				WriteRef((NiObject)objs[i3].avObject, s, info, link_map, missing_link_stack);
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
			numObjs = (uint)objs.Count;
			s.AppendLine($"      Scene:  {scene}");
			s.AppendLine($"      Num Objs:  {numObjs}");
			array_output_count = 0;
			for (var i3 = 0; i3 < objs.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Name:  {objs[i3].name}");
				s.AppendLine($"        AV Object:  {objs[i3].avObject}");
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			scene = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < objs.Count; i3++)
			{
				objs[i3].avObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < objs.Count; i3++)
			{
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			if (scene != null)
				ptrs.Add((NiObject)scene);
			for (var i3 = 0; i3 < objs.Count; i3++)
			{
				if (objs[i3].avObject != null)
					ptrs.Add((NiObject)objs[i3].avObject);
			}
			return ptrs;
		}
	}
}
