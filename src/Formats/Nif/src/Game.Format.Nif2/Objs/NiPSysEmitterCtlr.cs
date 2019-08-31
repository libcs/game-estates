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
	 * Particle system emitter controller.
	 *         NiInterpController::GetInterpolatorID() string format:
	 *             ['BirthRate', 'EmitterActive'] (for "Interpolator" and "Visibility Interpolator" respectively)
	 */
	public class NiPSysEmitterCtlr : NiPSysModifierCtlr
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysEmitterCtlr", NiPSysModifierCtlr.TYPE);

		/*! visibilityInterpolator */
		internal NiInterpolator visibilityInterpolator;
		/*! data */
		internal NiPSysEmitterCtlrData data;
		public NiPSysEmitterCtlr()
		{
			visibilityInterpolator = null;
			data = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysEmitterCtlr();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version <= 0x0A010067)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version >= 0x0A010000)
			{
				WriteRef((NiObject)visibilityInterpolator, s, info, link_map, missing_link_stack);
			}
			if (info.version <= 0x0A010067)
			{
				WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
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
			s.Append(base.AsString());
			s.AppendLine($"      Visibility Interpolator:  {visibilityInterpolator}");
			s.AppendLine($"      Data:  {data}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x0A010000)
			{
				visibilityInterpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
			}
			if (info.version <= 0x0A010067)
			{
				data = FixLink<NiPSysEmitterCtlrData>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (visibilityInterpolator != null)
				refs.Add((NiObject)visibilityInterpolator);
			if (data != null)
				refs.Add((NiObject)data);
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
