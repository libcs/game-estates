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
	 * Root node in Gamebryo .kf files (20.5.0.1 and up).
	 *         For 20.5.0.0, "NiSequenceData" is an alias for "NiControllerSequence" and this is not handled in nifxml.
	 *         This was not found in any 20.5.0.0 KFs available and they instead use NiControllerSequence directly.
	 */
	public class NiSequenceData : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiSequenceData", NiObject.TYPE);

		/*! name */
		internal IndexString name;
		/*! numControlledBlocks */
		internal uint numControlledBlocks;
		/*! arrayGrowBy */
		internal uint arrayGrowBy;
		/*! controlledBlocks */
		internal IList<ControlledBlock> controlledBlocks;
		/*! numEvaluators */
		internal uint numEvaluators;
		/*! evaluators */
		internal IList<NiEvaluator> evaluators;
		/*! textKeys */
		internal NiTextKeyExtraData textKeys;
		/*! duration */
		internal float duration;
		/*! cycleType */
		internal CycleType cycleType;
		/*! frequency */
		internal float frequency;
		/*! The name of the NiAVObject serving as the accumulation root. This is where all accumulated translations, scales, and rotations are applied. */
		internal IndexString accumRootName;
		/*! accumFlags */
		internal AccumFlags accumFlags;
		public NiSequenceData()
		{
			numControlledBlocks = (uint)0;
			arrayGrowBy = (uint)0;
			numEvaluators = (uint)0;
			textKeys = null;
			duration = 0.0f;
			cycleType = (CycleType)0;
			frequency = 1.0f;
			accumFlags = (AccumFlags)ACCUM_X_FRONT;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiSequenceData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out name, s, info);
			if (info.version <= 0x14050001)
			{
				Nif.NifStream(out numControlledBlocks, s, info);
				Nif.NifStream(out arrayGrowBy, s, info);
				controlledBlocks = new ControlledBlock[numControlledBlocks];
				for (var i4 = 0; i4 < controlledBlocks.Count; i4++)
				{
					if (info.version <= 0x0A010067)
					{
						Nif.NifStream(out controlledBlocks[i4].targetName, s, info);
					}
					if (info.version >= 0x0A01006A)
					{
						Nif.NifStream(out block_num, s, info);
						link_stack.Add(block_num);
					}
					if (info.version <= 0x14050000)
					{
						Nif.NifStream(out block_num, s, info);
						link_stack.Add(block_num);
					}
					if (info.version >= 0x0A010068 && info.version <= 0x0A01006E)
					{
						Nif.NifStream(out block_num, s, info);
						link_stack.Add(block_num);
						Nif.NifStream(out controlledBlocks[i4].blendIndex, s, info);
					}
					if (info.version >= 0x0A01006A && ((info.userVersion2 > 0)))
					{
						Nif.NifStream(out controlledBlocks[i4].priority, s, info);
					}
					if (info.version >= 0x0A010068 && info.version <= 0x0A010071)
					{
						Nif.NifStream(out controlledBlocks[i4].nodeName, s, info);
						Nif.NifStream(out controlledBlocks[i4].propertyType, s, info);
						Nif.NifStream(out controlledBlocks[i4].controllerType, s, info);
						Nif.NifStream(out controlledBlocks[i4].controllerId, s, info);
						Nif.NifStream(out controlledBlocks[i4].interpolatorId, s, info);
					}
					if (info.version >= 0x0A020000 && info.version <= 0x14010000)
					{
						Nif.NifStream(out block_num, s, info);
						link_stack.Add(block_num);
						Nif.NifStream(out controlledBlocks[i4].nodeNameOffset, s, info);
						Nif.NifStream(out controlledBlocks[i4].propertyTypeOffset, s, info);
						Nif.NifStream(out controlledBlocks[i4].controllerTypeOffset, s, info);
						Nif.NifStream(out controlledBlocks[i4].controllerIdOffset, s, info);
						Nif.NifStream(out controlledBlocks[i4].interpolatorIdOffset, s, info);
					}
					if (info.version >= 0x14010001)
					{
						Nif.NifStream(out (IndexString)controlledBlocks[i4].nodeName, s, info);
						Nif.NifStream(out (IndexString)controlledBlocks[i4].propertyType, s, info);
						Nif.NifStream(out (IndexString)controlledBlocks[i4].controllerType, s, info);
						Nif.NifStream(out (IndexString)controlledBlocks[i4].controllerId, s, info);
						Nif.NifStream(out (IndexString)controlledBlocks[i4].interpolatorId, s, info);
					}
				}
			}
			if (info.version >= 0x14050002)
			{
				Nif.NifStream(out numEvaluators, s, info);
				evaluators = new Ref[numEvaluators];
				for (var i4 = 0; i4 < evaluators.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out duration, s, info);
			Nif.NifStream(out cycleType, s, info);
			Nif.NifStream(out frequency, s, info);
			Nif.NifStream(out accumRootName, s, info);
			Nif.NifStream(out accumFlags, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numEvaluators = (uint)evaluators.Count;
			numControlledBlocks = (uint)controlledBlocks.Count;
			Nif.NifStream(name, s, info);
			if (info.version <= 0x14050001)
			{
				Nif.NifStream(numControlledBlocks, s, info);
				Nif.NifStream(arrayGrowBy, s, info);
				for (var i4 = 0; i4 < controlledBlocks.Count; i4++)
				{
					if (info.version <= 0x0A010067)
					{
						Nif.NifStream(controlledBlocks[i4].targetName, s, info);
					}
					if (info.version >= 0x0A01006A)
					{
						WriteRef((NiObject)controlledBlocks[i4].interpolator, s, info, link_map, missing_link_stack);
					}
					if (info.version <= 0x14050000)
					{
						WriteRef((NiObject)controlledBlocks[i4].controller, s, info, link_map, missing_link_stack);
					}
					if (info.version >= 0x0A010068 && info.version <= 0x0A01006E)
					{
						WriteRef((NiObject)controlledBlocks[i4].blendInterpolator, s, info, link_map, missing_link_stack);
						Nif.NifStream(controlledBlocks[i4].blendIndex, s, info);
					}
					if (info.version >= 0x0A01006A && ((info.userVersion2 > 0)))
					{
						Nif.NifStream(controlledBlocks[i4].priority, s, info);
					}
					if (info.version >= 0x0A010068 && info.version <= 0x0A010071)
					{
						Nif.NifStream(controlledBlocks[i4].nodeName, s, info);
						Nif.NifStream(controlledBlocks[i4].propertyType, s, info);
						Nif.NifStream(controlledBlocks[i4].controllerType, s, info);
						Nif.NifStream(controlledBlocks[i4].controllerId, s, info);
						Nif.NifStream(controlledBlocks[i4].interpolatorId, s, info);
					}
					if (info.version >= 0x0A020000 && info.version <= 0x14010000)
					{
						WriteRef((NiObject)controlledBlocks[i4].stringPalette, s, info, link_map, missing_link_stack);
						Nif.NifStream(controlledBlocks[i4].nodeNameOffset, s, info);
						Nif.NifStream(controlledBlocks[i4].propertyTypeOffset, s, info);
						Nif.NifStream(controlledBlocks[i4].controllerTypeOffset, s, info);
						Nif.NifStream(controlledBlocks[i4].controllerIdOffset, s, info);
						Nif.NifStream(controlledBlocks[i4].interpolatorIdOffset, s, info);
					}
					if (info.version >= 0x14010001)
					{
						Nif.NifStream((IndexString)controlledBlocks[i4].nodeName, s, info);
						Nif.NifStream((IndexString)controlledBlocks[i4].propertyType, s, info);
						Nif.NifStream((IndexString)controlledBlocks[i4].controllerType, s, info);
						Nif.NifStream((IndexString)controlledBlocks[i4].controllerId, s, info);
						Nif.NifStream((IndexString)controlledBlocks[i4].interpolatorId, s, info);
					}
				}
			}
			if (info.version >= 0x14050002)
			{
				Nif.NifStream(numEvaluators, s, info);
				for (var i4 = 0; i4 < evaluators.Count; i4++)
				{
					WriteRef((NiObject)evaluators[i4], s, info, link_map, missing_link_stack);
				}
			}
			WriteRef((NiObject)textKeys, s, info, link_map, missing_link_stack);
			Nif.NifStream(duration, s, info);
			Nif.NifStream(cycleType, s, info);
			Nif.NifStream(frequency, s, info);
			Nif.NifStream(accumRootName, s, info);
			Nif.NifStream(accumFlags, s, info);
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
			numEvaluators = (uint)evaluators.Count;
			numControlledBlocks = (uint)controlledBlocks.Count;
			s.AppendLine($"      Name:  {name}");
			s.AppendLine($"      Num Controlled Blocks:  {numControlledBlocks}");
			s.AppendLine($"      Array Grow By:  {arrayGrowBy}");
			array_output_count = 0;
			for (var i3 = 0; i3 < controlledBlocks.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Target Name:  {controlledBlocks[i3].targetName}");
				s.AppendLine($"        Interpolator:  {controlledBlocks[i3].interpolator}");
				s.AppendLine($"        Controller:  {controlledBlocks[i3].controller}");
				s.AppendLine($"        Blend Interpolator:  {controlledBlocks[i3].blendInterpolator}");
				s.AppendLine($"        Blend Index:  {controlledBlocks[i3].blendIndex}");
				s.AppendLine($"        Priority:  {controlledBlocks[i3].priority}");
				s.AppendLine($"        Node Name:  {controlledBlocks[i3].nodeName}");
				s.AppendLine($"        Property Type:  {controlledBlocks[i3].propertyType}");
				s.AppendLine($"        Controller Type:  {controlledBlocks[i3].controllerType}");
				s.AppendLine($"        Controller ID:  {controlledBlocks[i3].controllerId}");
				s.AppendLine($"        Interpolator ID:  {controlledBlocks[i3].interpolatorId}");
				s.AppendLine($"        String Palette:  {controlledBlocks[i3].stringPalette}");
				s.AppendLine($"        Node Name Offset:  {controlledBlocks[i3].nodeNameOffset}");
				s.AppendLine($"        Property Type Offset:  {controlledBlocks[i3].propertyTypeOffset}");
				s.AppendLine($"        Controller Type Offset:  {controlledBlocks[i3].controllerTypeOffset}");
				s.AppendLine($"        Controller ID Offset:  {controlledBlocks[i3].controllerIdOffset}");
				s.AppendLine($"        Interpolator ID Offset:  {controlledBlocks[i3].interpolatorIdOffset}");
			}
			s.AppendLine($"      Num Evaluators:  {numEvaluators}");
			array_output_count = 0;
			for (var i3 = 0; i3 < evaluators.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Evaluators[{i3}]:  {evaluators[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Text Keys:  {textKeys}");
			s.AppendLine($"      Duration:  {duration}");
			s.AppendLine($"      Cycle Type:  {cycleType}");
			s.AppendLine($"      Frequency:  {frequency}");
			s.AppendLine($"      Accum Root Name:  {accumRootName}");
			s.AppendLine($"      Accum Flags:  {accumFlags}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version <= 0x14050001)
			{
				for (var i4 = 0; i4 < controlledBlocks.Count; i4++)
				{
					if (info.version >= 0x0A01006A)
					{
						controlledBlocks[i4].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
					}
					if (info.version <= 0x14050000)
					{
						controlledBlocks[i4].controller = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
					}
					if (info.version >= 0x0A010068 && info.version <= 0x0A01006E)
					{
						controlledBlocks[i4].blendInterpolator = FixLink<NiBlendInterpolator>(objects, link_stack, missing_link_stack, info);
					}
					if (info.version >= 0x0A020000 && info.version <= 0x14010000)
					{
						controlledBlocks[i4].stringPalette = FixLink<NiStringPalette>(objects, link_stack, missing_link_stack, info);
					}
				}
			}
			if (info.version >= 0x14050002)
			{
				for (var i4 = 0; i4 < evaluators.Count; i4++)
				{
					evaluators[i4] = FixLink<NiEvaluator>(objects, link_stack, missing_link_stack, info);
				}
			}
			textKeys = FixLink<NiTextKeyExtraData>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < controlledBlocks.Count; i3++)
			{
				if (controlledBlocks[i3].interpolator != null)
					refs.Add((NiObject)controlledBlocks[i3].interpolator);
				if (controlledBlocks[i3].controller != null)
					refs.Add((NiObject)controlledBlocks[i3].controller);
				if (controlledBlocks[i3].blendInterpolator != null)
					refs.Add((NiObject)controlledBlocks[i3].blendInterpolator);
				if (controlledBlocks[i3].stringPalette != null)
					refs.Add((NiObject)controlledBlocks[i3].stringPalette);
			}
			for (var i3 = 0; i3 < evaluators.Count; i3++)
			{
				if (evaluators[i3] != null)
					refs.Add((NiObject)evaluators[i3]);
			}
			if (textKeys != null)
				refs.Add((NiObject)textKeys);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < controlledBlocks.Count; i3++)
			{
			}
			for (var i3 = 0; i3 < evaluators.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
