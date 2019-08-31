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
	/*! Abstract base class that provides the base timing and update functionality for all the Gamebryo animation controllers. */
	public class NiTimeController : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiTimeController", NiObject.TYPE);

		/*! Index of the next controller. */
		internal NiTimeController nextController;
		/*!
		 * Controller flags.
		 *             Bit 0 : Anim type, 0=APP_TIME 1=APP_INIT
		 *             Bit 1-2 : Cycle type, 00=Loop 01=Reverse 10=Clamp
		 *             Bit 3 : Active
		 *             Bit 4 : Play backwards
		 *             Bit 5 : Is manager controlled
		 *             Bit 6 : Compute scaled time (take frequency and phase into account)
		 *             Bit 7 : Force update
		 */
		internal ushort flags;
		/*! Frequency (is usually 1.0). */
		internal float frequency;
		/*! Phase (usually 0.0). */
		internal float phase;
		/*! Controller start time. */
		internal float startTime;
		/*! Controller stop time. */
		internal float stopTime;
		/*! Controller target (object index of the first controllable ancestor of this object). */
		internal NiObjectNET target;
		/*! Unknown integer. */
		internal uint unknownInteger;
		public NiTimeController()
		{
			nextController = null;
			flags = (ushort)0;
			frequency = 1.0f;
			phase = 0.0f;
			startTime = 3.402823466e+38f;
			stopTime = -3.402823466e+38f;
			target = null;
			unknownInteger = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiTimeController();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out flags, s, info);
			Nif.NifStream(out frequency, s, info);
			Nif.NifStream(out phase, s, info);
			Nif.NifStream(out startTime, s, info);
			Nif.NifStream(out stopTime, s, info);
			if (info.version >= 0x0303000D)
			{
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
			}
			if (info.version <= 0x03010000)
			{
				Nif.NifStream(out unknownInteger, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)nextController, s, info, link_map, missing_link_stack);
			Nif.NifStream(flags, s, info);
			Nif.NifStream(frequency, s, info);
			Nif.NifStream(phase, s, info);
			Nif.NifStream(startTime, s, info);
			Nif.NifStream(stopTime, s, info);
			if (info.version >= 0x0303000D)
			{
				WriteRef((NiObject)target, s, info, link_map, missing_link_stack);
			}
			if (info.version <= 0x03010000)
			{
				Nif.NifStream(unknownInteger, s, info);
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
			s.Append(base.AsString());
			s.AppendLine($"      Next Controller:  {nextController}");
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Frequency:  {frequency}");
			s.AppendLine($"      Phase:  {phase}");
			s.AppendLine($"      Start Time:  {startTime}");
			s.AppendLine($"      Stop Time:  {stopTime}");
			s.AppendLine($"      Target:  {target}");
			s.AppendLine($"      Unknown Integer:  {unknownInteger}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			nextController = FixLink<NiTimeController>(objects, link_stack, missing_link_stack, info);
			if (info.version >= 0x0303000D)
			{
				target = FixLink<NiObjectNET>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (nextController != null)
				refs.Add((NiObject)nextController);
			return refs;
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetPtrs()
		{
			var ptrs = base.GetPtrs();
			if (target != null)
				ptrs.Add((NiObject)target);
			return ptrs;
		}
	}
}
