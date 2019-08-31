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
	/*! Abstract base class for all NiInterpolators that blend the results of sub-interpolators together to compute a final weighted value. */
	public class NiBlendInterpolator : NiInterpolator
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiBlendInterpolator", NiInterpolator.TYPE);

		/*! flags */
		internal InterpBlendFlags flags;
		/*! arraySize */
		internal ushort arraySize;
		/*! arrayGrowBy */
		internal ushort arrayGrowBy;
		/*! weightThreshold */
		internal float weightThreshold;
		/*! interpCount */
		internal byte interpCount;
		/*! singleIndex */
		internal byte singleIndex;
		/*! highPriority */
		internal byte highPriority;
		/*! nextHighPriority */
		internal byte nextHighPriority;
		/*! singleTime */
		internal float singleTime;
		/*! highWeightsSum */
		internal float highWeightsSum;
		/*! nextHighWeightsSum */
		internal float nextHighWeightsSum;
		/*! highEaseSpinner */
		internal float highEaseSpinner;
		/*! interpArrayItems */
		internal IList<InterpBlendItem> interpArrayItems;
		/*! managedControlled */
		internal bool managedControlled;
		/*! onlyUseHighestWeight */
		internal bool onlyUseHighestWeight;
		/*! singleInterpolator */
		internal NiInterpolator singleInterpolator;
		public NiBlendInterpolator()
		{
			flags = (InterpBlendFlags)0;
			arraySize = (ushort)0;
			arrayGrowBy = (ushort)0;
			weightThreshold = 0.0f;
			interpCount = (byte)0;
			singleIndex = (byte)255;
			highPriority = (byte)-128;
			nextHighPriority = (byte)-128;
			singleTime = -3.402823466e+38f;
			highWeightsSum = -3.402823466e+38f;
			nextHighWeightsSum = -3.402823466e+38f;
			highEaseSpinner = -3.402823466e+38f;
			managedControlled = false;
			onlyUseHighestWeight = false;
			singleInterpolator = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiBlendInterpolator();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x0A010070)
			{
				Nif.NifStream(out flags, s, info);
			}
			if (info.version <= 0x0A01006D)
			{
				Nif.NifStream(out arraySize, s, info);
				Nif.NifStream(out arrayGrowBy, s, info);
			}
			if (info.version >= 0x0A01006E)
			{
				Nif.NifStream(out (byte)arraySize, s, info);
			}
			if (info.version >= 0x0A010070)
			{
				Nif.NifStream(out weightThreshold, s, info);
				if (((flags & 1) == 0))
				{
					Nif.NifStream(out interpCount, s, info);
					Nif.NifStream(out singleIndex, s, info);
					Nif.NifStream(out highPriority, s, info);
					Nif.NifStream(out nextHighPriority, s, info);
					Nif.NifStream(out singleTime, s, info);
					Nif.NifStream(out highWeightsSum, s, info);
					Nif.NifStream(out nextHighWeightsSum, s, info);
					Nif.NifStream(out highEaseSpinner, s, info);
					interpArrayItems = new InterpBlendItem[arraySize];
					for (var i5 = 0; i5 < interpArrayItems.Count; i5++)
					{
						Nif.NifStream(out block_num, s, info);
						link_stack.Add(block_num);
						Nif.NifStream(out interpArrayItems[i5].weight, s, info);
						Nif.NifStream(out interpArrayItems[i5].normalizedWeight, s, info);
						if (info.version <= 0x0A01006D)
						{
							Nif.NifStream(out interpArrayItems[i5].priority, s, info);
						}
						if (info.version >= 0x0A01006E)
						{
							Nif.NifStream(out (byte)interpArrayItems[i5].priority, s, info);
						}
						Nif.NifStream(out interpArrayItems[i5].easeSpinner, s, info);
					}
				}
			}
			if (info.version <= 0x0A01006F)
			{
				interpArrayItems = new InterpBlendItem[arraySize];
				for (var i4 = 0; i4 < interpArrayItems.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
					Nif.NifStream(out interpArrayItems[i4].weight, s, info);
					Nif.NifStream(out interpArrayItems[i4].normalizedWeight, s, info);
					if (info.version <= 0x0A01006D)
					{
						Nif.NifStream(out interpArrayItems[i4].priority, s, info);
					}
					if (info.version >= 0x0A01006E)
					{
						Nif.NifStream(out (byte)interpArrayItems[i4].priority, s, info);
					}
					Nif.NifStream(out interpArrayItems[i4].easeSpinner, s, info);
				}
				Nif.NifStream(out managedControlled, s, info);
				Nif.NifStream(out (float)weightThreshold, s, info);
				Nif.NifStream(out onlyUseHighestWeight, s, info);
			}
			if (info.version <= 0x0A01006D)
			{
				Nif.NifStream(out (ushort)interpCount, s, info);
				Nif.NifStream(out (ushort)singleIndex, s, info);
			}
			if (info.version >= 0x0A01006E && info.version <= 0x0A01006F)
			{
				Nif.NifStream(out (byte)interpCount, s, info);
				Nif.NifStream(out (byte)singleIndex, s, info);
			}
			if (info.version >= 0x0A01006C && info.version <= 0x0A01006F)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out (float)singleTime, s, info);
			}
			if (info.version <= 0x0A01006D)
			{
				Nif.NifStream(out (int)highPriority, s, info);
				Nif.NifStream(out (int)nextHighPriority, s, info);
			}
			if (info.version >= 0x0A01006E && info.version <= 0x0A01006F)
			{
				Nif.NifStream(out (byte)highPriority, s, info);
				Nif.NifStream(out (byte)nextHighPriority, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			arraySize = (ushort)interpArrayItems.Count;
			if (info.version >= 0x0A010070)
			{
				Nif.NifStream(flags, s, info);
			}
			if (info.version <= 0x0A01006D)
			{
				Nif.NifStream(arraySize, s, info);
				Nif.NifStream(arrayGrowBy, s, info);
			}
			if (info.version >= 0x0A01006E)
			{
				Nif.NifStream((byte)arraySize, s, info);
			}
			if (info.version >= 0x0A010070)
			{
				Nif.NifStream(weightThreshold, s, info);
				if (((flags & 1) == 0))
				{
					Nif.NifStream(interpCount, s, info);
					Nif.NifStream(singleIndex, s, info);
					Nif.NifStream(highPriority, s, info);
					Nif.NifStream(nextHighPriority, s, info);
					Nif.NifStream(singleTime, s, info);
					Nif.NifStream(highWeightsSum, s, info);
					Nif.NifStream(nextHighWeightsSum, s, info);
					Nif.NifStream(highEaseSpinner, s, info);
					for (var i5 = 0; i5 < interpArrayItems.Count; i5++)
					{
						WriteRef((NiObject)interpArrayItems[i5].interpolator, s, info, link_map, missing_link_stack);
						Nif.NifStream(interpArrayItems[i5].weight, s, info);
						Nif.NifStream(interpArrayItems[i5].normalizedWeight, s, info);
						if (info.version <= 0x0A01006D)
						{
							Nif.NifStream(interpArrayItems[i5].priority, s, info);
						}
						if (info.version >= 0x0A01006E)
						{
							Nif.NifStream((byte)interpArrayItems[i5].priority, s, info);
						}
						Nif.NifStream(interpArrayItems[i5].easeSpinner, s, info);
					}
				}
			}
			if (info.version <= 0x0A01006F)
			{
				for (var i4 = 0; i4 < interpArrayItems.Count; i4++)
				{
					WriteRef((NiObject)interpArrayItems[i4].interpolator, s, info, link_map, missing_link_stack);
					Nif.NifStream(interpArrayItems[i4].weight, s, info);
					Nif.NifStream(interpArrayItems[i4].normalizedWeight, s, info);
					if (info.version <= 0x0A01006D)
					{
						Nif.NifStream(interpArrayItems[i4].priority, s, info);
					}
					if (info.version >= 0x0A01006E)
					{
						Nif.NifStream((byte)interpArrayItems[i4].priority, s, info);
					}
					Nif.NifStream(interpArrayItems[i4].easeSpinner, s, info);
				}
				Nif.NifStream(managedControlled, s, info);
				Nif.NifStream((float)weightThreshold, s, info);
				Nif.NifStream(onlyUseHighestWeight, s, info);
			}
			if (info.version <= 0x0A01006D)
			{
				Nif.NifStream((ushort)interpCount, s, info);
				Nif.NifStream((ushort)singleIndex, s, info);
			}
			if (info.version >= 0x0A01006E && info.version <= 0x0A01006F)
			{
				Nif.NifStream((byte)interpCount, s, info);
				Nif.NifStream((byte)singleIndex, s, info);
			}
			if (info.version >= 0x0A01006C && info.version <= 0x0A01006F)
			{
				WriteRef((NiObject)singleInterpolator, s, info, link_map, missing_link_stack);
				Nif.NifStream((float)singleTime, s, info);
			}
			if (info.version <= 0x0A01006D)
			{
				Nif.NifStream((int)highPriority, s, info);
				Nif.NifStream((int)nextHighPriority, s, info);
			}
			if (info.version >= 0x0A01006E && info.version <= 0x0A01006F)
			{
				Nif.NifStream((byte)highPriority, s, info);
				Nif.NifStream((byte)nextHighPriority, s, info);
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
			arraySize = (ushort)interpArrayItems.Count;
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Array Size:  {arraySize}");
			s.AppendLine($"      Array Grow By:  {arrayGrowBy}");
			s.AppendLine($"      Weight Threshold:  {weightThreshold}");
			if (((flags & 1) == 0))
			{
				s.AppendLine($"        Interp Count:  {interpCount}");
				s.AppendLine($"        Single Index:  {singleIndex}");
				s.AppendLine($"        High Priority:  {highPriority}");
				s.AppendLine($"        Next High Priority:  {nextHighPriority}");
				s.AppendLine($"        Single Time:  {singleTime}");
				s.AppendLine($"        High Weights Sum:  {highWeightsSum}");
				s.AppendLine($"        Next High Weights Sum:  {nextHighWeightsSum}");
				s.AppendLine($"        High Ease Spinner:  {highEaseSpinner}");
				array_output_count = 0;
				for (var i4 = 0; i4 < interpArrayItems.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					s.AppendLine($"          Interpolator:  {interpArrayItems[i4].interpolator}");
					s.AppendLine($"          Weight:  {interpArrayItems[i4].weight}");
					s.AppendLine($"          Normalized Weight:  {interpArrayItems[i4].normalizedWeight}");
					s.AppendLine($"          Priority:  {interpArrayItems[i4].priority}");
					s.AppendLine($"          Ease Spinner:  {interpArrayItems[i4].easeSpinner}");
				}
			}
			s.AppendLine($"      Managed Controlled:  {managedControlled}");
			s.AppendLine($"      Only Use Highest Weight:  {onlyUseHighestWeight}");
			s.AppendLine($"      Single Interpolator:  {singleInterpolator}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x0A010070)
			{
				if (((flags & 1) == 0))
				{
					for (var i5 = 0; i5 < interpArrayItems.Count; i5++)
					{
						interpArrayItems[i5].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
					}
				}
			}
			if (info.version <= 0x0A01006F)
			{
				for (var i4 = 0; i4 < interpArrayItems.Count; i4++)
				{
					interpArrayItems[i4].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
				}
			}
			if (info.version >= 0x0A01006C && info.version <= 0x0A01006F)
			{
				singleInterpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++)
			{
				if (interpArrayItems[i3].interpolator != null)
					refs.Add((NiObject)interpArrayItems[i3].interpolator);
			}
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++)
			{
				if (interpArrayItems[i3].interpolator != null)
					refs.Add((NiObject)interpArrayItems[i3].interpolator);
			}
			if (singleInterpolator != null)
				refs.Add((NiObject)singleInterpolator);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++)
			{
			}
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++)
			{
			}
			return ptrs;
		}
	}
}
