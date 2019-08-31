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
	/*! Particle meshes data. */
	public class NiMeshPSysData : NiPSysData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiMeshPSysData", NiPSysData.TYPE);

		/*! defaultPoolSize */
		internal uint defaultPoolSize;
		/*! fillPoolsOnLoad */
		internal bool fillPoolsOnLoad;
		/*! numGenerations */
		internal uint numGenerations;
		/*! generations */
		internal IList<uint> generations;
		/*! particleMeshes */
		internal NiNode particleMeshes;
		public NiMeshPSysData()
		{
			defaultPoolSize = (uint)0;
			fillPoolsOnLoad = false;
			numGenerations = (uint)0;
			particleMeshes = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiMeshPSysData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x0A020000)
			{
				Nif.NifStream(out defaultPoolSize, s, info);
				Nif.NifStream(out fillPoolsOnLoad, s, info);
				Nif.NifStream(out numGenerations, s, info);
				generations = new uint[numGenerations];
				for (var i4 = 0; i4 < generations.Count; i4++)
				{
					Nif.NifStream(out generations[i4], s, info);
				}
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numGenerations = (uint)generations.Count;
			if (info.version >= 0x0A020000)
			{
				Nif.NifStream(defaultPoolSize, s, info);
				Nif.NifStream(fillPoolsOnLoad, s, info);
				Nif.NifStream(numGenerations, s, info);
				for (var i4 = 0; i4 < generations.Count; i4++)
				{
					Nif.NifStream(generations[i4], s, info);
				}
			}
			WriteRef((NiObject)particleMeshes, s, info, link_map, missing_link_stack);
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
			numGenerations = (uint)generations.Count;
			s.AppendLine($"      Default Pool Size:  {defaultPoolSize}");
			s.AppendLine($"      Fill Pools On Load:  {fillPoolsOnLoad}");
			s.AppendLine($"      Num Generations:  {numGenerations}");
			array_output_count = 0;
			for (var i3 = 0; i3 < generations.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Generations[{i3}]:  {generations[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Particle Meshes:  {particleMeshes}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			particleMeshes = FixLink<NiNode>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (particleMeshes != null)
				refs.Add((NiObject)particleMeshes);
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
