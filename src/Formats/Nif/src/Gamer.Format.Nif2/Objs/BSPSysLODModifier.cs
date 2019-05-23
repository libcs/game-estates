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
	/*! BSPSysLODModifier */
	public class BSPSysLODModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSPSysLODModifier", NiPSysModifier.TYPE);

		/*! lodBeginDistance */
		internal float lodBeginDistance;
		/*! lodEndDistance */
		internal float lodEndDistance;
		/*! endEmitScale */
		internal float endEmitScale;
		/*! endSize */
		internal float endSize;
		public BSPSysLODModifier()
		{
			lodBeginDistance = 0.1f;
			lodEndDistance = 0.7f;
			endEmitScale = 0.2f;
			endSize = 1.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSPSysLODModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out lodBeginDistance, s, info);
			Nif.NifStream(out lodEndDistance, s, info);
			Nif.NifStream(out endEmitScale, s, info);
			Nif.NifStream(out endSize, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(lodBeginDistance, s, info);
			Nif.NifStream(lodEndDistance, s, info);
			Nif.NifStream(endEmitScale, s, info);
			Nif.NifStream(endSize, s, info);
		}

		/*!
		 * Summarizes the information contained in this object in English.
		 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
		 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
		 */
		public override string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			s.Append(base.AsString());
			s.AppendLine($"      LOD Begin Distance:  {lodBeginDistance}");
			s.AppendLine($"      LOD End Distance:  {lodEndDistance}");
			s.AppendLine($"      End Emit Scale:  {endEmitScale}");
			s.AppendLine($"      End Size:  {endSize}");
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
