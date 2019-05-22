/* Copyright (c) 2006, NIF File Format Library and Tools
All rights reserved.  Please see niflib.h for license. */

//-----------------------------------NOTICE----------------------------------//
// Some of this file is automatically filled in by a Python script.  Only    //
// add custom code in the designated areas or it will be overwritten during  //
// the next update.                                                          //
//-----------------------------------NOTICE----------------------------------//

using System;
using System.IO;
using System.Collections.Generic;


namespace Niflib {

/*!
 * Abstract base class for all NiInterpolators that blend the results of sub-
 * interpolators together to compute a final weighted value.
 */
public class NiBlendInterpolator : NiInterpolator {
	//Definition of TYPE constant
	public static readonly Type_ TYPE = new Type_("NiBlendInterpolator", NiInterpolator.TYPE);
	/*!  */
	internal InterpBlendFlags flags;
	/*!  */
	internal ushort arraySize;
	/*!  */
	internal ushort arrayGrowBy;
	/*!  */
	internal float weightThreshold;
	/*!  */
	internal byte interpCount;
	/*!  */
	internal byte singleIndex;
	/*!  */
	internal byte highPriority;
	/*!  */
	internal byte nextHighPriority;
	/*!  */
	internal float singleTime;
	/*!  */
	internal float highWeightsSum;
	/*!  */
	internal float nextHighWeightsSum;
	/*!  */
	internal float highEaseSpinner;
	/*!  */
	internal IList<InterpBlendItem> interpArrayItems;
	/*!  */
	internal bool managedControlled;
	/*!  */
	internal bool onlyUseHighestWeight;
	/*!  */
	internal NiInterpolator singleInterpolator;

	public NiBlendInterpolator() {
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

/*!
 * Used to determine the type of a particular instance of this object.
 * \return The type constant for the actual type of the object.
 */
public override Type_ GetType() => TYPE;

/*!
 * A factory function used during file reading to create an instance of this type of object.
 * \return A pointer to a newly allocated instance of this type of object.
 */
public static NiObject Create() => new NiBlendInterpolator();

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Read(IStream s, List<uint> link_stack, NifInfo info) {

	uint block_num;
	base.Read(s, link_stack, info);
	if (info.version >= 0x0A010070) {
		Nif.NifStream(out flags, s, info);
	}
	if (info.version <= 0x0A01006D) {
		Nif.NifStream(out arraySize, s, info);
		Nif.NifStream(out arrayGrowBy, s, info);
	}
	if (info.version >= 0x0A01006E) {
		Nif.NifStream(out (byte)arraySize, s, info);
	}
	if (info.version >= 0x0A010070) {
		Nif.NifStream(out weightThreshold, s, info);
		if (((flags & 1) == 0)) {
			Nif.NifStream(out interpCount, s, info);
			Nif.NifStream(out singleIndex, s, info);
			Nif.NifStream(out highPriority, s, info);
			Nif.NifStream(out nextHighPriority, s, info);
			Nif.NifStream(out singleTime, s, info);
			Nif.NifStream(out highWeightsSum, s, info);
			Nif.NifStream(out nextHighWeightsSum, s, info);
			Nif.NifStream(out highEaseSpinner, s, info);
			interpArrayItems = new InterpBlendItem[arraySize];
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++) {
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				Nif.NifStream(out interpArrayItems[i3].weight, s, info);
				Nif.NifStream(out interpArrayItems[i3].normalizedWeight, s, info);
				if (info.version <= 0x0A01006D) {
					Nif.NifStream(out interpArrayItems[i3].priority, s, info);
				}
				if (info.version >= 0x0A01006E) {
					Nif.NifStream(out (byte)interpArrayItems[i3].priority, s, info);
				}
				Nif.NifStream(out interpArrayItems[i3].easeSpinner, s, info);
			}
		}
	}
	if (info.version <= 0x0A01006F) {
		interpArrayItems = new InterpBlendItem[arraySize];
		for (var i2 = 0; i2 < interpArrayItems.Count; i2++) {
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out interpArrayItems[i2].weight, s, info);
			Nif.NifStream(out interpArrayItems[i2].normalizedWeight, s, info);
			if (info.version <= 0x0A01006D) {
				Nif.NifStream(out interpArrayItems[i2].priority, s, info);
			}
			if (info.version >= 0x0A01006E) {
				Nif.NifStream(out (byte)interpArrayItems[i2].priority, s, info);
			}
			Nif.NifStream(out interpArrayItems[i2].easeSpinner, s, info);
		}
		Nif.NifStream(out managedControlled, s, info);
		Nif.NifStream(out (float)weightThreshold, s, info);
		Nif.NifStream(out onlyUseHighestWeight, s, info);
	}
	if (info.version <= 0x0A01006D) {
		Nif.NifStream(out (ushort)interpCount, s, info);
		Nif.NifStream(out (ushort)singleIndex, s, info);
	}
	if ((info.version >= 0x0A01006E) && (info.version <= 0x0A01006F)) {
		Nif.NifStream(out (byte)interpCount, s, info);
		Nif.NifStream(out (byte)singleIndex, s, info);
	}
	if ((info.version >= 0x0A01006C) && (info.version <= 0x0A01006F)) {
		Nif.NifStream(out block_num, s, info);
		link_stack.Add(block_num);
		Nif.NifStream(out (float)singleTime, s, info);
	}
	if (info.version <= 0x0A01006D) {
		Nif.NifStream(out (int)highPriority, s, info);
		Nif.NifStream(out (int)nextHighPriority, s, info);
	}
	if ((info.version >= 0x0A01006E) && (info.version <= 0x0A01006F)) {
		Nif.NifStream(out (byte)highPriority, s, info);
		Nif.NifStream(out (byte)nextHighPriority, s, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info) {

	base.Write(s, link_map, missing_link_stack, info);
	arraySize = (ushort)interpArrayItems.Count;
	if (info.version >= 0x0A010070) {
		Nif.NifStream(flags, s, info);
	}
	if (info.version <= 0x0A01006D) {
		Nif.NifStream(arraySize, s, info);
		Nif.NifStream(arrayGrowBy, s, info);
	}
	if (info.version >= 0x0A01006E) {
		Nif.NifStream((byte)arraySize, s, info);
	}
	if (info.version >= 0x0A010070) {
		Nif.NifStream(weightThreshold, s, info);
		if (((flags & 1) == 0)) {
			Nif.NifStream(interpCount, s, info);
			Nif.NifStream(singleIndex, s, info);
			Nif.NifStream(highPriority, s, info);
			Nif.NifStream(nextHighPriority, s, info);
			Nif.NifStream(singleTime, s, info);
			Nif.NifStream(highWeightsSum, s, info);
			Nif.NifStream(nextHighWeightsSum, s, info);
			Nif.NifStream(highEaseSpinner, s, info);
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++) {
				WriteRef((NiObject)interpArrayItems[i3].interpolator, s, info, link_map, missing_link_stack);
				Nif.NifStream(interpArrayItems[i3].weight, s, info);
				Nif.NifStream(interpArrayItems[i3].normalizedWeight, s, info);
				if (info.version <= 0x0A01006D) {
					Nif.NifStream(interpArrayItems[i3].priority, s, info);
				}
				if (info.version >= 0x0A01006E) {
					Nif.NifStream((byte)interpArrayItems[i3].priority, s, info);
				}
				Nif.NifStream(interpArrayItems[i3].easeSpinner, s, info);
			}
		}
	}
	if (info.version <= 0x0A01006F) {
		for (var i2 = 0; i2 < interpArrayItems.Count; i2++) {
			WriteRef((NiObject)interpArrayItems[i2].interpolator, s, info, link_map, missing_link_stack);
			Nif.NifStream(interpArrayItems[i2].weight, s, info);
			Nif.NifStream(interpArrayItems[i2].normalizedWeight, s, info);
			if (info.version <= 0x0A01006D) {
				Nif.NifStream(interpArrayItems[i2].priority, s, info);
			}
			if (info.version >= 0x0A01006E) {
				Nif.NifStream((byte)interpArrayItems[i2].priority, s, info);
			}
			Nif.NifStream(interpArrayItems[i2].easeSpinner, s, info);
		}
		Nif.NifStream(managedControlled, s, info);
		Nif.NifStream((float)weightThreshold, s, info);
		Nif.NifStream(onlyUseHighestWeight, s, info);
	}
	if (info.version <= 0x0A01006D) {
		Nif.NifStream((ushort)interpCount, s, info);
		Nif.NifStream((ushort)singleIndex, s, info);
	}
	if ((info.version >= 0x0A01006E) && (info.version <= 0x0A01006F)) {
		Nif.NifStream((byte)interpCount, s, info);
		Nif.NifStream((byte)singleIndex, s, info);
	}
	if ((info.version >= 0x0A01006C) && (info.version <= 0x0A01006F)) {
		WriteRef((NiObject)singleInterpolator, s, info, link_map, missing_link_stack);
		Nif.NifStream((float)singleTime, s, info);
	}
	if (info.version <= 0x0A01006D) {
		Nif.NifStream((int)highPriority, s, info);
		Nif.NifStream((int)nextHighPriority, s, info);
	}
	if ((info.version >= 0x0A01006E) && (info.version <= 0x0A01006F)) {
		Nif.NifStream((byte)highPriority, s, info);
		Nif.NifStream((byte)nextHighPriority, s, info);
	}

}

/*!
 * Summarizes the information contained in this object in English.
 * \param[in] verbose Determines whether or not detailed information about large areas of data will be printed cs.
 * \return A string containing a summary of the information within the object in English.  This is the function that Niflyze calls to generate its analysis, so the output is the same.
 */
public override string AsString(bool verbose = false) {

	var s = new System.Text.StringBuilder();
	uint array_output_count = 0;
	s.Append(base.AsString());
	arraySize = (ushort)interpArrayItems.Count;
	s.AppendLine($"  Flags:  {flags}");
	s.AppendLine($"  Array Size:  {arraySize}");
	s.AppendLine($"  Array Grow By:  {arrayGrowBy}");
	s.AppendLine($"  Weight Threshold:  {weightThreshold}");
	if (((flags & 1) == 0)) {
		s.AppendLine($"    Interp Count:  {interpCount}");
		s.AppendLine($"    Single Index:  {singleIndex}");
		s.AppendLine($"    High Priority:  {highPriority}");
		s.AppendLine($"    Next High Priority:  {nextHighPriority}");
		s.AppendLine($"    Single Time:  {singleTime}");
		s.AppendLine($"    High Weights Sum:  {highWeightsSum}");
		s.AppendLine($"    Next High Weights Sum:  {nextHighWeightsSum}");
		s.AppendLine($"    High Ease Spinner:  {highEaseSpinner}");
		array_output_count = 0;
		for (var i2 = 0; i2 < interpArrayItems.Count; i2++) {
			if (!verbose && (array_output_count > Nif.MAXARRAYDUMP)) {
				s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
				break;
			}
			s.AppendLine($"      Interpolator:  {interpArrayItems[i2].interpolator}");
			s.AppendLine($"      Weight:  {interpArrayItems[i2].weight}");
			s.AppendLine($"      Normalized Weight:  {interpArrayItems[i2].normalizedWeight}");
			s.AppendLine($"      Priority:  {interpArrayItems[i2].priority}");
			s.AppendLine($"      Ease Spinner:  {interpArrayItems[i2].easeSpinner}");
		}
	}
	s.AppendLine($"  Managed Controlled:  {managedControlled}");
	s.AppendLine($"  Only Use Highest Weight:  {onlyUseHighestWeight}");
	s.AppendLine($"  Single Interpolator:  {singleInterpolator}");
	return s.ToString();

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info) {

	base.FixLinks(objects, link_stack, missing_link_stack, info);
	if (info.version >= 0x0A010070) {
		if (((flags & 1) == 0)) {
			for (var i3 = 0; i3 < interpArrayItems.Count; i3++) {
				interpArrayItems[i3].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
			}
		}
	}
	if (info.version <= 0x0A01006F) {
		for (var i2 = 0; i2 < interpArrayItems.Count; i2++) {
			interpArrayItems[i2].interpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
		}
	}
	if ((info.version >= 0x0A01006C) && (info.version <= 0x0A01006F)) {
		singleInterpolator = FixLink<NiInterpolator>(objects, link_stack, missing_link_stack, info);
	}

}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetRefs() {
	var refs = base.GetRefs();
	for (var i1 = 0; i1 < interpArrayItems.Count; i1++) {
		if (interpArrayItems[i1].interpolator != null)
			refs.Add((NiObject)interpArrayItems[i1].interpolator);
	}
	for (var i1 = 0; i1 < interpArrayItems.Count; i1++) {
		if (interpArrayItems[i1].interpolator != null)
			refs.Add((NiObject)interpArrayItems[i1].interpolator);
	}
	if (singleInterpolator != null)
		refs.Add((NiObject)singleInterpolator);
	return refs;
}

/*! NIFLIB_HIDDEN function.  For internal use only. */
internal override List<NiObject> GetPtrs() {
	var ptrs = base.GetPtrs();
	for (var i1 = 0; i1 < interpArrayItems.Count; i1++) {
	}
	for (var i1 = 0; i1 < interpArrayItems.Count; i1++) {
	}
	return ptrs;
}


}

}