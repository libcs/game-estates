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
	/*! Unknown. Only found in 2.3 nifs. */
	public class Ni3dsAnimationNode : NiObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("Ni3dsAnimationNode", NiObject.TYPE);

		/*! Name of this object. */
		internal IndexString name;
		/*! Unknown. */
		internal bool hasData;
		/*! Unknown. Matrix? */
		internal Array21<float> unknownFloats1;
		/*! Unknown. */
		internal ushort unknownShort;
		/*! Child? */
		internal NiObject child;
		/*! Unknown. */
		internal Array12<float> unknownFloats2;
		/*! A count. */
		internal uint count;
		/*! Unknown. */
		internal IList<Array5<byte>> unknownArray;
		public Ni3dsAnimationNode()
		{
			hasData = false;
			unknownShort = (ushort)0;
			child = null;
			count = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new Ni3dsAnimationNode();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out name, s, info);
			Nif.NifStream(out hasData, s, info);
			if (hasData)
			{
				for (var i4 = 0; i4 < 21; i4++)
				{
					Nif.NifStream(out unknownFloats1[i4], s, info);
				}
				Nif.NifStream(out unknownShort, s, info);
				Nif.NifStream(out block_num, s, info);
				link_stack.Add(block_num);
				for (var i4 = 0; i4 < 12; i4++)
				{
					Nif.NifStream(out unknownFloats2[i4], s, info);
				}
				Nif.NifStream(out count, s, info);
				unknownArray = new byte[count];
				for (var i4 = 0; i4 < unknownArray.Count; i4++)
				{
					for (var i5 = 0; i5 < 5; i5++)
					{
						Nif.NifStream(out unknownArray[i4][i5], s, info);
					}
				}
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			count = (uint)unknownArray.Count;
			Nif.NifStream(name, s, info);
			Nif.NifStream(hasData, s, info);
			if (hasData)
			{
				for (var i4 = 0; i4 < 21; i4++)
				{
					Nif.NifStream(unknownFloats1[i4], s, info);
				}
				Nif.NifStream(unknownShort, s, info);
				WriteRef((NiObject)child, s, info, link_map, missing_link_stack);
				for (var i4 = 0; i4 < 12; i4++)
				{
					Nif.NifStream(unknownFloats2[i4], s, info);
				}
				Nif.NifStream(count, s, info);
				for (var i4 = 0; i4 < unknownArray.Count; i4++)
				{
					for (var i5 = 0; i5 < 5; i5++)
					{
						Nif.NifStream(unknownArray[i4][i5], s, info);
					}
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
			count = (uint)unknownArray.Count;
			s.AppendLine($"      Name:  {name}");
			s.AppendLine($"      Has Data:  {hasData}");
			if (hasData)
			{
				array_output_count = 0;
				for (var i4 = 0; i4 < 21; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Floats 1[{i4}]:  {unknownFloats1[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Unknown Short:  {unknownShort}");
				s.AppendLine($"        Child:  {child}");
				array_output_count = 0;
				for (var i4 = 0; i4 < 12; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						break;
					s.AppendLine($"          Unknown Floats 2[{i4}]:  {unknownFloats2[i4]}");
					array_output_count++;
				}
				s.AppendLine($"        Count:  {count}");
				array_output_count = 0;
				for (var i4 = 0; i4 < unknownArray.Count; i4++)
				{
					if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					{
						s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
						break;
					}
					for (var i5 = 0; i5 < 5; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Unknown Array[{i5}]:  {unknownArray[i4][i5]}");
						array_output_count++;
					}
				}
			}
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			if (hasData)
			{
				child = FixLink<NiObject>(objects, link_stack, missing_link_stack, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (child != null)
				refs.Add((NiObject)child);
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
