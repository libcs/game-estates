/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! The NIF file footer. */
	public class Footer
	{
		/*! The number of root references. */
		internal uint numRoots;
		/*!
		 * List of root NIF objects. If there is a camera, for 1st person view, then this NIF object is referred to as well in this list, even if it is not a root object
		 * (usually we want the camera to be attached to the Bip Head node).
		 */
		internal IList<NiObject> roots;

		public Footer()
		{
			unchecked
			{
				numRoots = (uint)0;
			}
		}

		public void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			if (info.version >= 0x0303000D)
			{
				Nif.NifStream(out numRoots, s, info);
				roots = new Ref[numRoots];
				for (var i4 = 0; i4 < roots.Count; i4++)
				{
					Nif.NifStream(out block_num, s, info);
					link_stack.Add(block_num);
				}
			}
		}

		public void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			numRoots = (uint)roots.Count;
			if (info.version >= 0x0303000D)
			{
				Nif.NifStream(numRoots, s, info);
				for (var i4 = 0; i4 < roots.Count; i4++)
				{
					WriteRef((NiObject)roots[i4], s, info, link_map, missing_link_stack);
				}
			}
		}

		public string AsString(bool verbose = false)
		{
			var s = new System.Text.StringBuilder();
			var array_output_count = 0U;
			numRoots = (uint)roots.Count;
			s.AppendLine($"      Num Roots:  {numRoots}");
			array_output_count = 0;
			for (var i3 = 0; i3 < roots.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
					break;
				s.AppendLine($"        Roots[{i3}]:  {roots[i3]}");
				array_output_count++;
			}
			return s.ToString();
		}
	}
}
