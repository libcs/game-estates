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
	 * DEPRECATED (20.5), replaced by NiMorphMeshModifier.
	 *         Geometry morphing data.
	 */
	public class NiMorphData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiMorphData", NiObject.TYPE);

		/*! Number of morphing object. */
		internal uint numMorphs;
		/*! Number of vertices. */
		internal uint numVertices;
		/*! This byte is always 1 in all official files. */
		internal byte relativeTargets;
		/*! The geometry morphing objects. */
		internal IList<Morph> morphs;
		public NiMorphData()
		{
			numMorphs = (uint)0;
			numVertices = (uint)0;
			relativeTargets = (byte)1;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiMorphData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numMorphs, s, info);
			Nif.NifStream(out numVertices, s, info);
			Nif.NifStream(out relativeTargets, s, info);
			morphs = new Morph[numMorphs];
			for (var i3 = 0; i3 < morphs.Count; i3++)
			{
				if (info.version >= 0x0A01006A)
				{
					Nif.NifStream(out morphs[i3].frameName, s, info);
				}
				if (info.version <= 0x0A010000)
				{
					Nif.NifStream(out morphs[i3].numKeys, s, info);
					Nif.NifStream(out morphs[i3].interpolation, s, info);
					morphs[i3].keys = new Key[morphs[i3].numKeys];
					for (var i5 = 0; i5 < morphs[i3].keys.Count; i5++)
					{
						Nif.NifStream(out morphs[i3].keys[i5], s, info, morphs[i3].interpolation);
					}
				}
				if (info.version >= 0x0A010068 && info.version <= 0x14010002 && ((info.userVersion2 < 10)))
				{
					Nif.NifStream(out morphs[i3].legacyWeight, s, info);
				}
				morphs[i3].vectors = new Vector3[numVertices];
				for (var i4 = 0; i4 < morphs[i3].vectors.Count; i4++)
				{
					Nif.NifStream(out morphs[i3].vectors[i4], s, info);
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numMorphs = (uint)morphs.Count;
			Nif.NifStream(numMorphs, s, info);
			Nif.NifStream(numVertices, s, info);
			Nif.NifStream(relativeTargets, s, info);
			for (var i3 = 0; i3 < morphs.Count; i3++)
			{
				morphs[i3].numKeys = (uint)morphs[i3].keys.Count;
				if (info.version >= 0x0A01006A)
				{
					Nif.NifStream(morphs[i3].frameName, s, info);
				}
				if (info.version <= 0x0A010000)
				{
					Nif.NifStream(morphs[i3].numKeys, s, info);
					Nif.NifStream(morphs[i3].interpolation, s, info);
					for (var i5 = 0; i5 < morphs[i3].keys.Count; i5++)
					{
						Nif.NifStream(morphs[i3].keys[i5], s, info, morphs[i3].interpolation);
					}
				}
				if (info.version >= 0x0A010068 && info.version <= 0x14010002 && ((info.userVersion2 < 10)))
				{
					Nif.NifStream(morphs[i3].legacyWeight, s, info);
				}
				for (var i4 = 0; i4 < morphs[i3].vectors.Count; i4++)
				{
					Nif.NifStream(morphs[i3].vectors[i4], s, info);
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
			numMorphs = (uint)morphs.Count;
			s.AppendLine($"      Num Morphs:  {numMorphs}");
			s.AppendLine($"      Num Vertices:  {numVertices}");
			s.AppendLine($"      Relative Targets:  {relativeTargets}");
			array_output_count = 0;
			for (var i3 = 0; i3 < morphs.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				morphs[i3].numKeys = (uint)morphs[i3].keys.Count;
				s.AppendLine($"        Frame Name:  {morphs[i3].frameName}");
				s.AppendLine($"        Num Keys:  {morphs[i3].numKeys}");
				s.AppendLine($"        Interpolation:  {morphs[i3].interpolation}");
				array_output_count = 0;
				for (var i4 = 0; i4 < morphs[i3].keys.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Keys[{i4}]:  {morphs[i3].keys[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Legacy Weight:  {morphs[i3].legacyWeight}");
				array_output_count = 0;
				for (var i4 = 0; i4 < morphs[i3].vectors.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Vectors[{i4}]:  {morphs[i3].vectors[i4]}");
					array_output_count++;
				}
			}
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
