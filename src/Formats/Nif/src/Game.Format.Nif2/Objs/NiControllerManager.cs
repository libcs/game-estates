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
	/*! Controls animation sequences on a specific branch of the scene graph. */
	public class NiControllerManager : NiTimeController
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiControllerManager", NiTimeController.TYPE);

		/*!
		 * Whether transformation accumulation is enabled. If accumulation is not enabled, the manager will treat all sequence data on the accumulation root as absolute
		 * data instead of relative delta values.
		 */
		internal bool cumulative;
		/*! numControllerSequences */
		internal uint numControllerSequences;
		/*! controllerSequences */
		internal IList<NiControllerSequence> controllerSequences;
		/*! objectPalette */
		internal NiDefaultAVObjectPalette objectPalette;
		public NiControllerManager()
		{
			cumulative = false;
			numControllerSequences = (uint)0;
			objectPalette = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiControllerManager();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out cumulative, s, info);
			Nif.NifStream(out numControllerSequences, s, info);
			controllerSequences = new Ref[numControllerSequences];
			for (var i3 = 0; i3 < controllerSequences.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numControllerSequences = (uint)controllerSequences.Count;
			Nif.NifStream(cumulative, s, info);
			Nif.NifStream(numControllerSequences, s, info);
			for (var i3 = 0; i3 < controllerSequences.Count; i3++)
			{
				WriteRef((NiObject)controllerSequences[i3], s, info, link_map, missing_link_stack);
			}
			WriteRef((NiObject)objectPalette, s, info, link_map, missing_link_stack);
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
			numControllerSequences = (uint)controllerSequences.Count;
			s.AppendLine($"      Cumulative:  {cumulative}");
			s.AppendLine($"      Num Controller Sequences:  {numControllerSequences}");
			array_output_count = 0;
			for (var i3 = 0; i3 < controllerSequences.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Controller Sequences[{i3}]:  {controllerSequences[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Object Palette:  {objectPalette}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < controllerSequences.Count; i3++)
			{
				controllerSequences[i3] = FixLink<NiControllerSequence>(objects, link_stack, missing_link_stack, info);
			}
			objectPalette = FixLink<NiDefaultAVObjectPalette>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < controllerSequences.Count; i3++)
			{
				if (controllerSequences[i3] != null)
					refs.Add((NiObject)controllerSequences[i3]);
			}
			if (objectPalette != null)
				refs.Add((NiObject)objectPalette);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < controllerSequences.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
