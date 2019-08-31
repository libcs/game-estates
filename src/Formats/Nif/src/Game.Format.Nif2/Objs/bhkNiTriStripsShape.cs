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
	/*! A shape constructed from a bunch of strips. */
	public class bhkNiTriStripsShape : bhkShapeCollection
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkNiTriStripsShape", bhkShapeCollection.TYPE);

		/*! The material of the shape. */
		internal HavokMaterial material;
		/*! radius */
		internal float radius;
		/*! Garbage data from memory though the last 3 are referred to as maxSize, size, and eSize. */
		internal Array5<uint> unused;
		/*! growBy */
		internal uint growBy;
		/*! Scale. Usually (1.0, 1.0, 1.0, 0.0). */
		internal Vector4 scale;
		/*! The number of strips data objects referenced. */
		internal uint numStripsData;
		/*! Refers to a bunch of NiTriStripsData objects that make up this shape. */
		internal IList<NiTriStripsData> stripsData;
		/*! Number of Havok Layers, equal to Number of strips data objects. */
		internal uint numDataLayers;
		/*! Havok Layers for each strip data. */
		internal IList<HavokFilter> dataLayers;
		public bhkNiTriStripsShape()
		{
			radius = 0.1f;
			growBy = (uint)1;
			scale = (1.0, 1.0, 1.0, 0.0);
			numStripsData = (uint)0;
			numDataLayers = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkNiTriStripsShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(out material.unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(out material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(out material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(out material.material_sk, s, info);
			}
			Nif.NifStream(out radius, s, info);
			for (var i3 = 0; i3 < 5; i3++)
			{
				Nif.NifStream(out unused[i3], s, info);
			}
			Nif.NifStream(out growBy, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out scale, s, info);
			}
			Nif.NifStream(out numStripsData, s, info);
			stripsData = new Ref[numStripsData];
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			Nif.NifStream(out numDataLayers, s, info);
			dataLayers = new HavokFilter[numDataLayers];
			for (var i3 = 0; i3 < dataLayers.Count; i3++)
			{
				if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
				{
					Nif.NifStream(out dataLayers[i3].layer_ob, s, info);
				}
				if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
				{
					Nif.NifStream(out dataLayers[i3].layer_fo, s, info);
				}
				if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
				{
					Nif.NifStream(out dataLayers[i3].layer_sk, s, info);
				}
				Nif.NifStream(out dataLayers[i3].flagsAndPartNumber, s, info);
				Nif.NifStream(out dataLayers[i3].group, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numDataLayers = (uint)dataLayers.Count;
			numStripsData = (uint)stripsData.Count;
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(material.unknownInt, s, info);
			}
			if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
			{
				Nif.NifStream(material.material_ob, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
			{
				Nif.NifStream(material.material_fo, s, info);
			}
			if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
			{
				Nif.NifStream(material.material_sk, s, info);
			}
			Nif.NifStream(radius, s, info);
			for (var i3 = 0; i3 < 5; i3++)
			{
				Nif.NifStream(unused[i3], s, info);
			}
			Nif.NifStream(growBy, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(scale, s, info);
			}
			Nif.NifStream(numStripsData, s, info);
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				WriteRef((NiObject)stripsData[i3], s, info, link_map, missing_link_stack);
			}
			Nif.NifStream(numDataLayers, s, info);
			for (var i3 = 0; i3 < dataLayers.Count; i3++)
			{
				if (info.version <= 0x14000005 && ((info.userVersion2 < 16)))
				{
					Nif.NifStream(dataLayers[i3].layer_ob, s, info);
				}
				if (((info.version == 0x14020007) && (info.userVersion2 <= 34)))
				{
					Nif.NifStream(dataLayers[i3].layer_fo, s, info);
				}
				if (((info.version == 0x14020007) && (info.userVersion2 > 34)))
				{
					Nif.NifStream(dataLayers[i3].layer_sk, s, info);
				}
				Nif.NifStream(dataLayers[i3].flagsAndPartNumber, s, info);
				Nif.NifStream(dataLayers[i3].group, s, info);
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
			numDataLayers = (uint)dataLayers.Count;
			numStripsData = (uint)stripsData.Count;
			s.AppendLine($"      Unknown Int:  {material.unknownInt}");
			s.AppendLine($"      Material:  {material.material_ob}");
			s.AppendLine($"      Material:  {material.material_fo}");
			s.AppendLine($"      Material:  {material.material_sk}");
			s.AppendLine($"      Radius:  {radius}");
			array_output_count = 0;
			for (var i3 = 0; i3 < 5; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Unused[{i3}]:  {unused[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Grow By:  {growBy}");
			s.AppendLine($"      Scale:  {scale}");
			s.AppendLine($"      Num Strips Data:  {numStripsData}");
			array_output_count = 0;
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Strips Data[{i3}]:  {stripsData[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Num Data Layers:  {numDataLayers}");
			array_output_count = 0;
			for (var i3 = 0; i3 < dataLayers.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Layer:  {dataLayers[i3].layer_ob}");
				s.AppendLine($"        Layer:  {dataLayers[i3].layer_fo}");
				s.AppendLine($"        Layer:  {dataLayers[i3].layer_sk}");
				s.AppendLine($"        Flags and Part Number:  {dataLayers[i3].flagsAndPartNumber}");
				s.AppendLine($"        Group:  {dataLayers[i3].group}");
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				stripsData[i3] = FixLink<NiTriStripsData>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
				if (stripsData[i3] != null)
					refs.Add((NiObject)stripsData[i3]);
			}
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < stripsData.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
