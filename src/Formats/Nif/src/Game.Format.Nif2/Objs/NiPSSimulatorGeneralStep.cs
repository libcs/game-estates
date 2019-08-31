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
	/*! Encapsulates a floodgate kernel that updates particle size, colors, and rotations. */
	public class NiPSSimulatorGeneralStep : NiPSSimulatorStep
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiPSSimulatorGeneralStep", NiPSSimulatorStep.TYPE);

		/*! numSizeKeys */
		internal byte numSizeKeys;
		/*! The particle size keys. */
		internal IList<Key<float>> sizeKeys;
		/*! The loop behavior for the size keys. */
		internal PSLoopBehavior sizeLoopBehavior;
		/*! numColorKeys */
		internal byte numColorKeys;
		/*! The particle color keys. */
		internal IList<Key<ByteColor4>> colorKeys;
		/*! The loop behavior for the color keys. */
		internal PSLoopBehavior colorLoopBehavior;
		/*! numRotationKeys */
		internal byte numRotationKeys;
		/*! The particle rotation keys. */
		internal IList<Key<Quaternion>> rotationKeys;
		/*! The loop behavior for the rotation keys. */
		internal PSLoopBehavior rotationLoopBehavior;
		/*! The the amount of time over which a particle's size is ramped from 0.0 to 1.0 in seconds */
		internal float growTime;
		/*! The the amount of time over which a particle's size is ramped from 1.0 to 0.0 in seconds */
		internal float shrinkTime;
		/*! Specifies the particle generation to which the grow effect should be applied. This is usually generation 0, so that newly created particles will grow. */
		internal ushort growGeneration;
		/*!
		 * Specifies the particle generation to which the shrink effect should be applied. This is usually the highest supported generation for the particle system, so
		 * that particles will shrink immediately before getting killed.
		 */
		internal ushort shrinkGeneration;
		public NiPSSimulatorGeneralStep()
		{
			numSizeKeys = (byte)0;
			sizeLoopBehavior = (PSLoopBehavior)0;
			numColorKeys = (byte)0;
			colorLoopBehavior = (PSLoopBehavior)0;
			numRotationKeys = (byte)0;
			rotationLoopBehavior = (PSLoopBehavior)0;
			growTime = 0.0f;
			shrinkTime = 0.0f;
			growGeneration = (ushort)0;
			shrinkGeneration = (ushort)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiPSSimulatorGeneralStep();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out numSizeKeys, s, info);
				sizeKeys = new Key[numSizeKeys];
				for (var i4 = 0; i4 < sizeKeys.Count; i4++)
				{
					Nif.NifStream(out sizeKeys[i4], s, info, 1);
				}
				Nif.NifStream(out sizeLoopBehavior, s, info);
			}
			Nif.NifStream(out numColorKeys, s, info);
			colorKeys = new Key[numColorKeys];
			for (var i3 = 0; i3 < colorKeys.Count; i3++)
			{
				Nif.NifStream(out colorKeys[i3], s, info, 1);
			}
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(out colorLoopBehavior, s, info);
				Nif.NifStream(out numRotationKeys, s, info);
				rotationKeys = new Key[numRotationKeys];
				for (var i4 = 0; i4 < rotationKeys.Count; i4++)
				{
					Nif.NifStream(out rotationKeys[i4], s, info, 1);
				}
				Nif.NifStream(out rotationLoopBehavior, s, info);
			}
			Nif.NifStream(out growTime, s, info);
			Nif.NifStream(out shrinkTime, s, info);
			Nif.NifStream(out growGeneration, s, info);
			Nif.NifStream(out shrinkGeneration, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numRotationKeys = (byte)rotationKeys.Count;
			numColorKeys = (byte)colorKeys.Count;
			numSizeKeys = (byte)sizeKeys.Count;
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(numSizeKeys, s, info);
				for (var i4 = 0; i4 < sizeKeys.Count; i4++)
				{
					Nif.NifStream(sizeKeys[i4], s, info, 1);
				}
				Nif.NifStream(sizeLoopBehavior, s, info);
			}
			Nif.NifStream(numColorKeys, s, info);
			for (var i3 = 0; i3 < colorKeys.Count; i3++)
			{
				Nif.NifStream(colorKeys[i3], s, info, 1);
			}
			if (info.version >= 0x14060100)
			{
				Nif.NifStream(colorLoopBehavior, s, info);
				Nif.NifStream(numRotationKeys, s, info);
				for (var i4 = 0; i4 < rotationKeys.Count; i4++)
				{
					Nif.NifStream(rotationKeys[i4], s, info, 1);
				}
				Nif.NifStream(rotationLoopBehavior, s, info);
			}
			Nif.NifStream(growTime, s, info);
			Nif.NifStream(shrinkTime, s, info);
			Nif.NifStream(growGeneration, s, info);
			Nif.NifStream(shrinkGeneration, s, info);
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
			numRotationKeys = (byte)rotationKeys.Count;
			numColorKeys = (byte)colorKeys.Count;
			numSizeKeys = (byte)sizeKeys.Count;
			s.AppendLine($"      Num Size Keys:  {numSizeKeys}");
			array_output_count = 0;
			for (var i3 = 0; i3 < sizeKeys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Size Keys[{i3}]:  {sizeKeys[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Size Loop Behavior:  {sizeLoopBehavior}");
			s.AppendLine($"      Num Color Keys:  {numColorKeys}");
			array_output_count = 0;
			for (var i3 = 0; i3 < colorKeys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Color Keys[{i3}]:  {colorKeys[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Color Loop Behavior:  {colorLoopBehavior}");
			s.AppendLine($"      Num Rotation Keys:  {numRotationKeys}");
			array_output_count = 0;
			for (var i3 = 0; i3 < rotationKeys.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Rotation Keys[{i3}]:  {rotationKeys[i3]}");
				array_output_count++;
			}
			s.AppendLine($"      Rotation Loop Behavior:  {rotationLoopBehavior}");
			s.AppendLine($"      Grow Time:  {growTime}");
			s.AppendLine($"      Shrink Time:  {shrinkTime}");
			s.AppendLine($"      Grow Generation:  {growGeneration}");
			s.AppendLine($"      Shrink Generation:  {shrinkGeneration}");
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
