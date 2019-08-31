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
	/*! Mesh modifier that provides per-frame instancing capabilities in Gamebryo. */
	public class NiInstancingMeshModifier : NiMeshModifier
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiInstancingMeshModifier", NiMeshModifier.TYPE);

		/*! hasInstanceNodes */
		internal bool hasInstanceNodes;
		/*! perInstanceCulling */
		internal bool perInstanceCulling;
		/*! hasStaticBounds */
		internal bool hasStaticBounds;
		/*! affectedMesh */
		internal NiMesh affectedMesh;
		/*! bound */
		internal NiBound bound;
		/*! numInstanceNodes */
		internal uint numInstanceNodes;
		/*! instanceNodes */
		internal IList<NiMeshHWInstance> instanceNodes;
		public NiInstancingMeshModifier()
		{
			hasInstanceNodes = false;
			perInstanceCulling = false;
			hasStaticBounds = false;
			affectedMesh = null;
			numInstanceNodes = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiInstancingMeshModifier();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out hasInstanceNodes, s, info);
			Nif.NifStream(out perInstanceCulling, s, info);
			Nif.NifStream(out hasStaticBounds, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			if (hasStaticBounds)
			{
				Nif.NifStream(out bound.center, s, info);
				Nif.NifStream(out bound.radius, s, info);
			}
			if (hasInstanceNodes)
			{
				Nif.NifStream(out numInstanceNodes, s, info);
				instanceNodes = new Ref[numInstanceNodes];
				for (var i4 = 0; i4 < instanceNodes.Count; i4++)
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
			numInstanceNodes = (uint)instanceNodes.Count;
			Nif.NifStream(hasInstanceNodes, s, info);
			Nif.NifStream(perInstanceCulling, s, info);
			Nif.NifStream(hasStaticBounds, s, info);
			WriteRef((NiObject)affectedMesh, s, info, link_map, missing_link_stack);
			if (hasStaticBounds)
			{
				Nif.NifStream(bound.center, s, info);
				Nif.NifStream(bound.radius, s, info);
			}
			if (hasInstanceNodes)
			{
				Nif.NifStream(numInstanceNodes, s, info);
				for (var i4 = 0; i4 < instanceNodes.Count; i4++)
				{
					WriteRef((NiObject)instanceNodes[i4], s, info, link_map, missing_link_stack);
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
			numInstanceNodes = (uint)instanceNodes.Count;
			s.AppendLine($"      Has Instance Nodes:  {hasInstanceNodes}");
			s.AppendLine($"      Per Instance Culling:  {perInstanceCulling}");
			s.AppendLine($"      Has Static Bounds:  {hasStaticBounds}");
			s.AppendLine($"      Affected Mesh:  {affectedMesh}");
			if (hasStaticBounds)
			{
				s.AppendLine($"        Center:  {bound.center}");
				s.AppendLine($"        Radius:  {bound.radius}");
			}
			if (hasInstanceNodes)
			{
				s.AppendLine($"        Num Instance Nodes:  {numInstanceNodes}");
				array_output_count = 0;
				for (var i4 = 0; i4 < instanceNodes.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Instance Nodes[{i4}]:  {instanceNodes[i4]}");
					array_output_count++;
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			affectedMesh = FixLink<NiMesh>(objects, link_stack, missing_link_stack, info);
			if (hasInstanceNodes)
			{
				for (var i4 = 0; i4 < instanceNodes.Count; i4++)
				{
					instanceNodes[i4] = FixLink<NiMeshHWInstance>(objects, link_stack, missing_link_stack, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (affectedMesh != null)
				refs.Add((NiObject)affectedMesh);
			for (var i3 = 0; i3 < instanceNodes.Count; i3++)
			{
				if (instanceNodes[i3] != null)
					refs.Add((NiObject)instanceNodes[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < instanceNodes.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
