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
	/*! Abstract base class for NiObjects that support names, extra data, and time controllers. */
	public class NiObjectNET : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiObjectNET", NiObject.TYPE);

		/*! Configures the main shader path */
		internal BSLightingShaderPropertyShaderType skyrimShaderType;
		/*! Name of this controllable object, used to refer to the object in .kf files. */
		internal IndexString name;
		/*! Extra data for pre-3.0 versions. */
		internal bool hasOldExtraData;
		/*! (=NiStringExtraData) */
		internal IndexString oldExtraPropName;
		/*! ref */
		internal uint oldExtraInternalId;
		/*! Extra string data. */
		internal IndexString oldExtraString;
		/*! Always 0. */
		internal byte unknownByte;
		/*! Extra data object index. (The first in a chain) */
		internal NiExtraData extraData;
		/*! The number of Extra Data objects referenced through the list. */
		internal uint numExtraDataList;
		/*! List of extra data indices. */
		internal IList<NiExtraData> extraDataList;
		/*! Controller object index. (The first in a chain) */
		internal NiTimeController controller;
		public NiObjectNET()
		{
			skyrimShaderType = (BSLightingShaderPropertyShaderType)0;
			hasOldExtraData = false;
			oldExtraInternalId = (uint)0;
			unknownByte = (byte)0;
			extraData = null;
			numExtraDataList = (uint)0;
			controller = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiObjectNET();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if ((info.userVersion2 >= 83))
			{
				if (IsDerivedType(BSLightingShaderProperty.TYPE))
				{
					Nif.NifStream(out skyrimShaderType, s, info);
				}
			}
			Nif.NifStream(out name, s, info);
			if (info.version <= 0x02030000)
			{
				Nif.NifStream(out hasOldExtraData, s, info);
				if (hasOldExtraData)
				{
					Nif.NifStream(out oldExtraPropName, s, info);
					Nif.NifStream(out oldExtraInternalId, s, info);
					Nif.NifStream(out oldExtraString, s, info);
				}
				Nif.NifStream(out unknownByte, s, info);
			}
			if (info.version >= 0x03000000 && info.version <= 0x04020200)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(out numExtraDataList, s, info);
				extraDataList = new Ref[numExtraDataList];
				for (var i4 = 0; i4 < extraDataList.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
			if (info.version >= 0x03000000)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numExtraDataList = (uint)extraDataList.Count;
			if ((info.userVersion2 >= 83))
			{
				if (IsDerivedType(BSLightingShaderProperty.TYPE))
				{
					Nif.NifStream(skyrimShaderType, s, info);
				}
			}
			Nif.NifStream(name, s, info);
			if (info.version <= 0x02030000)
			{
				Nif.NifStream(hasOldExtraData, s, info);
				if (hasOldExtraData)
				{
					Nif.NifStream(oldExtraPropName, s, info);
					Nif.NifStream(oldExtraInternalId, s, info);
					Nif.NifStream(oldExtraString, s, info);
				}
				Nif.NifStream(unknownByte, s, info);
			}
			if (info.version >= 0x03000000 && info.version <= 0x04020200)
			{
				WriteRef((NiObject)extraData, s, info, link_map, missing_link_stack);
			}
			if (info.version >= 0x0A000100)
			{
				Nif.NifStream(numExtraDataList, s, info);
				for (var i4 = 0; i4 < extraDataList.Count; i4++)
				{
					WriteRef((NiObject)extraDataList[i4], s, info, link_map, missing_link_stack);
				}
			}
			if (info.version >= 0x03000000)
			{
				WriteRef((NiObject)controller, s, info, link_map, missing_link_stack);
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
			numExtraDataList = (uint)extraDataList.Count;
			if (IsDerivedType(BSLightingShaderProperty.TYPE))
			{
				s.AppendLine($"        Skyrim Shader Type:  {skyrimShaderType}");
			}
			s.AppendLine($"      Name:  {name}");
			s.AppendLine($"      Has Old Extra Data:  {hasOldExtraData}");
			if (hasOldExtraData)
			{
				s.AppendLine($"        Old Extra Prop Name:  {oldExtraPropName}");
				s.AppendLine($"        Old Extra Internal Id:  {oldExtraInternalId}");
				s.AppendLine($"        Old Extra String:  {oldExtraString}");
			}
			s.AppendLine($"      Unknown Byte:  {unknownByte}");
			s.AppendLine($"      Extra Data:  {extraData}");
			s.AppendLine($"      Num Extra Data List:  {numExtraDataList}");
			array_output_count = 0;
			for (var i3 = 0; i3 < extraDataList.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Extra Data List[{i3}]:  {extraDataList[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Controller:  {controller}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x03000000 && info.version <= 0x04020200)
			{
				extraData = FixLink<NiExtraData>(objects, link_stack, missing_link_stack, info);
			}
			if (info.version >= 0x0A000100)
			{
				for (var i4 = 0; i4 < extraDataList.Count; i4++)
				{
					extraDataList[i4] = FixLink<NiExtraData>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (info.version >= 0x03000000)
			{
				controller = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (extraData != null)
				refs.Add((NiObject)extraData);
			for (var i3 = 0; i3 < extraDataList.Count; i3++)
			{
				if (extraDataList[i3] != null)
					refs.Add((NiObject)extraDataList[i3]);
			}
			if (controller != null)
				refs.Add((NiObject)controller);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < extraDataList.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
