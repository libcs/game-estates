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
	 * Controls animation and collision.  Integer holds flags:
	 *         Bit 0 : enable havok, bAnimated(Skyrim)
	 *         Bit 1 : enable collision, bHavok(Skyrim)
	 *         Bit 2 : is skeleton nif?, bRagdoll(Skyrim)
	 *         Bit 3 : enable animation, bComplex(Skyrim)
	 *         Bit 4 : FlameNodes present, bAddon(Skyrim)
	 *         Bit 5 : EditorMarkers present, bEditorMarker(Skyrim)
	 *         Bit 6 : bDynamic(Skyrim)
	 *         Bit 7 : bArticulated(Skyrim)
	 *         Bit 8 : bIKTarget(Skyrim)/needsTransformUpdates
	 *         Bit 9 : bExternalEmit(Skyrim)
	 *         Bit 10: bMagicShaderParticles(Skyrim)
	 *         Bit 11: bLights(Skyrim)
	 *         Bit 12: bBreakable(Skyrim)
	 *         Bit 13: bSearchedBreakable(Skyrim) .. Runtime only?
	 */
	public class BSXFlags : NiIntegerExtraData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSXFlags", NiIntegerExtraData.TYPE);

		public BSXFlags()
		{
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSXFlags();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
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
