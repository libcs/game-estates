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
	/*! NiMesh */
	public class NiMesh : NiRenderObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiMesh", NiRenderObject.TYPE);

		/*! The primitive type of the mesh, such as triangles or lines. */
		internal MeshPrimitiveType primitiveType;
		/*! Unknown. */
		internal int unknown51;
		/*! Unknown. */
		internal int unknown52;
		/*! Unknown. */
		internal int unknown53;
		/*! Unknown. */
		internal int unknown54;
		/*! Unknown. */
		internal float unknown55;
		/*! Unknown. */
		internal int unknown56;
		/*! The number of submeshes contained in this mesh. */
		internal ushort numSubmeshes;
		/*! Sets whether hardware instancing is being used. */
		internal bool instancingEnabled;
		/*! The combined bounding volume of all submeshes. */
		internal NiBound bound;
		/*! numDatastreams */
		internal uint numDatastreams;
		/*! datastreams */
		internal IList<DataStreamRef> datastreams;
		/*! numModifiers */
		internal uint numModifiers;
		/*! modifiers */
		internal IList<NiMeshModifier> modifiers;
		/*! Unknown. */
		internal byte unknown100;
		/*! Unknown. */
		internal int unknown101;
		/*! Size of additional data. */
		internal uint unknown102;
		/*! Unknown. */
		internal IList<float> unknown103;
		/*! Unknown. */
		internal int unknown200;
		/*! Unknown. */
		internal IList<ExtraMeshDataEpicMickey> unknown201;
		/*! Unknown. */
		internal int unknown250;
		/*! Unknown. */
		internal IList<int> unknown251;
		/*! Unknown. */
		internal int unknown300;
		/*! Unknown. */
		internal short unknown301;
		/*! Unknown. */
		internal int unknown302;
		/*! Unknown. */
		internal IList<byte> unknown303;
		/*! Unknown. */
		internal int unknown350;
		/*! Unknown. */
		internal IList<ExtraMeshDataEpicMickey2> unknown351;
		/*! Unknown. */
		internal int unknown400;
		public NiMesh()
		{
			primitiveType = (MeshPrimitiveType)0;
			unknown51 = (int)0;
			unknown52 = (int)0;
			unknown53 = (int)0;
			unknown54 = (int)0;
			unknown55 = 0.0f;
			unknown56 = (int)0;
			numSubmeshes = (ushort)0;
			instancingEnabled = false;
			numDatastreams = (uint)0;
			numModifiers = (uint)0;
			unknown100 = (byte)0;
			unknown101 = (int)0;
			unknown102 = (uint)0;
			unknown200 = (int)0;
			unknown250 = (int)0;
			unknown300 = (int)0;
			unknown301 = (short)0;
			unknown302 = (int)0;
			unknown350 = (int)0;
			unknown400 = (int)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiMesh();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out primitiveType, s, info);
			if (info.userVersion == 15)
			{
				Nif.NifStream(out unknown51, s, info);
				Nif.NifStream(out unknown52, s, info);
				Nif.NifStream(out unknown53, s, info);
				Nif.NifStream(out unknown54, s, info);
				Nif.NifStream(out unknown55, s, info);
				Nif.NifStream(out unknown56, s, info);
			}
			Nif.NifStream(out numSubmeshes, s, info);
			Nif.NifStream(out instancingEnabled, s, info);
			Nif.NifStream(out bound.center, s, info);
			Nif.NifStream(out bound.radius, s, info);
			Nif.NifStream(out numDatastreams, s, info);
			datastreams = new DataStreamRef[numDatastreams];
			for (var i3 = 0; i3 < datastreams.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out datastreams[i3].isPerInstance, s, info);
				Nif.NifStream(out datastreams[i3].numSubmeshes, s, info);
				datastreams[i3].submeshToRegionMap = new ushort[datastreams[i3].numSubmeshes];
				for (var i4 = 0; i4 < datastreams[i3].submeshToRegionMap.Count; i4++)
				{
					Nif.NifStream(out datastreams[i3].submeshToRegionMap[i4], s, info);
				}
				Nif.NifStream(out datastreams[i3].numComponents, s, info);
				datastreams[i3].componentSemantics = new SemanticData[datastreams[i3].numComponents];
				for (var i4 = 0; i4 < datastreams[i3].componentSemantics.Count; i4++)
				{
					Nif.NifStream(out datastreams[i3].componentSemantics[i4].name, s, info);
					Nif.NifStream(out datastreams[i3].componentSemantics[i4].index, s, info);
				}
			}
			Nif.NifStream(out numModifiers, s, info);
			modifiers = new Ref[numModifiers];
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.userVersion == 15)
			{
				Nif.NifStream(out unknown100, s, info);
				Nif.NifStream(out unknown101, s, info);
				Nif.NifStream(out unknown102, s, info);
				unknown103 = new float[unknown102];
				for (var i4 = 0; i4 < unknown103.Count; i4++)
				{
					Nif.NifStream(out unknown103[i4], s, info);
				}
				Nif.NifStream(out unknown200, s, info);
				unknown201 = new ExtraMeshDataEpicMickey[unknown200];
				for (var i4 = 0; i4 < unknown201.Count; i4++)
				{
					Nif.NifStream(out unknown201[i4].unknownInt1, s, info);
					Nif.NifStream(out unknown201[i4].unknownInt2, s, info);
					Nif.NifStream(out unknown201[i4].unknownInt3, s, info);
					Nif.NifStream(out unknown201[i4].unknownInt4, s, info);
					Nif.NifStream(out unknown201[i4].unknownInt5, s, info);
					Nif.NifStream(out unknown201[i4].unknownInt6, s, info);
				}
				Nif.NifStream(out unknown250, s, info);
				unknown251 = new int[unknown250];
				for (var i4 = 0; i4 < unknown251.Count; i4++)
				{
					Nif.NifStream(out unknown251[i4], s, info);
				}
				Nif.NifStream(out unknown300, s, info);
				Nif.NifStream(out unknown301, s, info);
				Nif.NifStream(out unknown302, s, info);
				unknown303 = new byte[unknown302];
				for (var i4 = 0; i4 < unknown303.Count; i4++)
				{
					Nif.NifStream(out unknown303[i4], s, info);
				}
				Nif.NifStream(out unknown350, s, info);
				unknown351 = new ExtraMeshDataEpicMickey2[unknown350];
				for (var i4 = 0; i4 < unknown351.Count; i4++)
				{
					Nif.NifStream(out unknown351[i4].start, s, info);
					Nif.NifStream(out unknown351[i4].end, s, info);
					for (var i5 = 0; i5 < 10; i5++)
					{
						Nif.NifStream(out unknown351[i4].unknownShorts[i5], s, info);
					}
				}
				Nif.NifStream(out unknown400, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			unknown350 = (int)unknown351.Count;
			unknown302 = (int)unknown303.Count;
			unknown250 = (int)unknown251.Count;
			unknown200 = (int)unknown201.Count;
			unknown102 = (uint)unknown103.Count;
			numModifiers = (uint)modifiers.Count;
			numDatastreams = (uint)datastreams.Count;
			Nif.NifStream(primitiveType, s, info);
			if (info.userVersion == 15)
			{
				Nif.NifStream(unknown51, s, info);
				Nif.NifStream(unknown52, s, info);
				Nif.NifStream(unknown53, s, info);
				Nif.NifStream(unknown54, s, info);
				Nif.NifStream(unknown55, s, info);
				Nif.NifStream(unknown56, s, info);
			}
			Nif.NifStream(numSubmeshes, s, info);
			Nif.NifStream(instancingEnabled, s, info);
			Nif.NifStream(bound.center, s, info);
			Nif.NifStream(bound.radius, s, info);
			Nif.NifStream(numDatastreams, s, info);
			for (var i3 = 0; i3 < datastreams.Count; i3++)
			{
				datastreams[i3].numComponents = (uint)datastreams[i3].componentSemantics.Count;
				datastreams[i3].numSubmeshes = (ushort)datastreams[i3].submeshToRegionMap.Count;
				WriteRef((NiObject)datastreams[i3].stream, s, info, link_map, missing_link_stack);
				Nif.NifStream(datastreams[i3].isPerInstance, s, info);
				Nif.NifStream(datastreams[i3].numSubmeshes, s, info);
				for (var i4 = 0; i4 < datastreams[i3].submeshToRegionMap.Count; i4++)
				{
					Nif.NifStream(datastreams[i3].submeshToRegionMap[i4], s, info);
				}
				Nif.NifStream(datastreams[i3].numComponents, s, info);
				for (var i4 = 0; i4 < datastreams[i3].componentSemantics.Count; i4++)
				{
					Nif.NifStream(datastreams[i3].componentSemantics[i4].name, s, info);
					Nif.NifStream(datastreams[i3].componentSemantics[i4].index, s, info);
				}
			}
			Nif.NifStream(numModifiers, s, info);
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				WriteRef((NiObject)modifiers[i3], s, info, link_map, missing_link_stack);
			}
			if (info.userVersion == 15)
			{
				Nif.NifStream(unknown100, s, info);
				Nif.NifStream(unknown101, s, info);
				Nif.NifStream(unknown102, s, info);
				for (var i4 = 0; i4 < unknown103.Count; i4++)
				{
					Nif.NifStream(unknown103[i4], s, info);
				}
				Nif.NifStream(unknown200, s, info);
				for (var i4 = 0; i4 < unknown201.Count; i4++)
				{
					Nif.NifStream(unknown201[i4].unknownInt1, s, info);
					Nif.NifStream(unknown201[i4].unknownInt2, s, info);
					Nif.NifStream(unknown201[i4].unknownInt3, s, info);
					Nif.NifStream(unknown201[i4].unknownInt4, s, info);
					Nif.NifStream(unknown201[i4].unknownInt5, s, info);
					Nif.NifStream(unknown201[i4].unknownInt6, s, info);
				}
				Nif.NifStream(unknown250, s, info);
				for (var i4 = 0; i4 < unknown251.Count; i4++)
				{
					Nif.NifStream(unknown251[i4], s, info);
				}
				Nif.NifStream(unknown300, s, info);
				Nif.NifStream(unknown301, s, info);
				Nif.NifStream(unknown302, s, info);
				for (var i4 = 0; i4 < unknown303.Count; i4++)
				{
					Nif.NifStream(unknown303[i4], s, info);
				}
				Nif.NifStream(unknown350, s, info);
				for (var i4 = 0; i4 < unknown351.Count; i4++)
				{
					Nif.NifStream(unknown351[i4].start, s, info);
					Nif.NifStream(unknown351[i4].end, s, info);
					for (var i5 = 0; i5 < 10; i5++)
					{
						Nif.NifStream(unknown351[i4].unknownShorts[i5], s, info);
					}
				}
				Nif.NifStream(unknown400, s, info);
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
			unknown350 = (int)unknown351.Count;
			unknown302 = (int)unknown303.Count;
			unknown250 = (int)unknown251.Count;
			unknown200 = (int)unknown201.Count;
			unknown102 = (uint)unknown103.Count;
			numModifiers = (uint)modifiers.Count;
			numDatastreams = (uint)datastreams.Count;
			s.AppendLine($"      Primitive Type:  {primitiveType}");
			s.AppendLine($"      Unknown 51:  {unknown51}");
			s.AppendLine($"      Unknown 52:  {unknown52}");
			s.AppendLine($"      Unknown 53:  {unknown53}");
			s.AppendLine($"      Unknown 54:  {unknown54}");
			s.AppendLine($"      Unknown 55:  {unknown55}");
			s.AppendLine($"      Unknown 56:  {unknown56}");
			s.AppendLine($"      Num Submeshes:  {numSubmeshes}");
			s.AppendLine($"      Instancing Enabled:  {instancingEnabled}");
			s.AppendLine($"      Center:  {bound.center}");
			s.AppendLine($"      Radius:  {bound.radius}");
			s.AppendLine($"      Num Datastreams:  {numDatastreams}");
			array_output_count = 0;
			for (var i3 = 0; i3 < datastreams.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				datastreams[i3].numComponents = (uint)datastreams[i3].componentSemantics.Count;
				datastreams[i3].numSubmeshes = (ushort)datastreams[i3].submeshToRegionMap.Count;
				s.AppendLine($"        Stream:  {datastreams[i3].stream}");
				s.AppendLine($"        Is Per Instance:  {datastreams[i3].isPerInstance}");
				s.AppendLine($"        Num Submeshes:  {datastreams[i3].numSubmeshes}");
				array_output_count = 0;
				for (var i4 = 0; i4 < datastreams[i3].submeshToRegionMap.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Submesh To Region Map[{i4}]:  {datastreams[i3].submeshToRegionMap[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Num Components:  {datastreams[i3].numComponents}");
				array_output_count = 0;
				for (var i4 = 0; i4 < datastreams[i3].componentSemantics.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					s.AppendLine($"          Name:  {datastreams[i3].componentSemantics[i4].name}");
					s.AppendLine($"          Index:  {datastreams[i3].componentSemantics[i4].index}");
				}
			}
			s.AppendLine($"      Num Modifiers:  {numModifiers}");
			array_output_count = 0;
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Modifiers[{i3}]:  {modifiers[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown 100:  {unknown100}");
			s.AppendLine($"      Unknown 101:  {unknown101}");
			s.AppendLine($"      Unknown 102:  {unknown102}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknown103.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 103[{i3}]:  {unknown103[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown 200:  {unknown200}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknown201.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Unknown Int 1:  {unknown201[i3].unknownInt1}");
				s.AppendLine($"        Unknown Int 2:  {unknown201[i3].unknownInt2}");
				s.AppendLine($"        Unknown Int 3:  {unknown201[i3].unknownInt3}");
				s.AppendLine($"        Unknown Int 4:  {unknown201[i3].unknownInt4}");
				s.AppendLine($"        Unknown Int 5:  {unknown201[i3].unknownInt5}");
				s.AppendLine($"        Unknown Int 6:  {unknown201[i3].unknownInt6}");
			}
			s.AppendLine($"      Unknown 250:  {unknown250}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknown251.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 251[{i3}]:  {unknown251[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown 300:  {unknown300}");
			s.AppendLine($"      Unknown 301:  {unknown301}");
			s.AppendLine($"      Unknown 302:  {unknown302}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknown303.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unknown 303[{i3}]:  {unknown303[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Unknown 350:  {unknown350}");
			array_output_count = 0;
			for (var i3 = 0; i3 < unknown351.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Start:  {unknown351[i3].start}");
				s.AppendLine($"        End:  {unknown351[i3].end}");
				array_output_count = 0;
				for (var i4 = 0; i4 < 10; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Shorts[{i4}]:  {unknown351[i3].unknownShorts[i4]}");
					array_output_count++;
				}
			}
			s.AppendLine($"      Unknown 400:  {unknown400}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < datastreams.Count; i3++)
			{
				datastreams[i3].stream = FixLink<NiDataStream>(objects, link_stack, missing_link_stack, info);
			}
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				modifiers[i3] = FixLink<NiMeshModifier>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < datastreams.Count; i3++)
			{
				if (datastreams[i3].stream != null)
					refs.Add((NiObject)datastreams[i3].stream);
			}
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
				if (modifiers[i3] != null)
					refs.Add((NiObject)modifiers[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < datastreams.Count; i3++)
			{
			}
			for (var i3 = 0; i3 < modifiers.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
