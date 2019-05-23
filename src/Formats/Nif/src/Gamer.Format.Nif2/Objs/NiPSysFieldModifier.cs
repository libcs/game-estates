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
	/*! Base for all force field particle modifiers. */
	public class NiPSysFieldModifier : NiPSysModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSysFieldModifier", NiPSysModifier.TYPE);

		/*! The object whose position and orientation are the basis of the field. */
		internal NiAVObject fieldObject;
		/*! Magnitude of the force. */
		internal float magnitude;
		/*! How the magnitude diminishes with distance from the Field Object. */
		internal float attenuation;
		/*! Whether or not to use a distance from the Field Object after which there is no effect. */
		internal bool useMaxDistance;
		/*! Maximum distance after which there is no effect. */
		internal float maxDistance;
		public NiPSysFieldModifier()
		{
			fieldObject = null;
			magnitude = 0.0f;
			attenuation = 0.0f;
			useMaxDistance = false;
			maxDistance = 0.0f;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSysFieldModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out magnitude, s, info);
			Nif.NifStream(out attenuation, s, info);
			Nif.NifStream(out useMaxDistance, s, info);
			Nif.NifStream(out maxDistance, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)fieldObject, s, info, link_map, missing_link_stack);
			Nif.NifStream(magnitude, s, info);
			Nif.NifStream(attenuation, s, info);
			Nif.NifStream(useMaxDistance, s, info);
			Nif.NifStream(maxDistance, s, info);
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
			s.AppendLine($"      Field Object:  {fieldObject}");
			s.AppendLine($"      Magnitude:  {magnitude}");
			s.AppendLine($"      Attenuation:  {attenuation}");
			s.AppendLine($"      Use Max Distance:  {useMaxDistance}");
			s.AppendLine($"      Max Distance:  {maxDistance}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			fieldObject = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (fieldObject != null)
				refs.Add((NiObject)fieldObject);
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
