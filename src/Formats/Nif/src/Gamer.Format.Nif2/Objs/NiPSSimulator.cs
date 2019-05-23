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
	/*! The mesh modifier that performs all particle system simulation. */
	public class NiPSSimulator : NiMeshModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSSimulator", NiMeshModifier.TYPE);

		/*! numSimulationSteps */
		internal uint numSimulationSteps;
		/*! simulationSteps */
		internal IList<NiPSSimulatorStep> simulationSteps;
		public NiPSSimulator()
		{
			numSimulationSteps = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSSimulator();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out numSimulationSteps, s, info);
			simulationSteps = new Ref[numSimulationSteps];
			for (var i3 = 0; i3 < simulationSteps.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numSimulationSteps = (uint)simulationSteps.Count;
			Nif.NifStream(numSimulationSteps, s, info);
			for (var i3 = 0; i3 < simulationSteps.Count; i3++)
			{
				WriteRef((NiObject)simulationSteps[i3], s, info, link_map, missing_link_stack);
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
			numSimulationSteps = (uint)simulationSteps.Count;
			s.AppendLine($"      Num Simulation Steps:  {numSimulationSteps}");
			array_output_count = 0;
			for (var i3 = 0; i3 < simulationSteps.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Simulation Steps[{i3}]:  {simulationSteps[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < simulationSteps.Count; i3++)
			{
				simulationSteps[i3] = FixLink<NiPSSimulatorStep>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < simulationSteps.Count; i3++)
			{
				if (simulationSteps[i3] != null)
					refs.Add((NiObject)simulationSteps[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < simulationSteps.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
