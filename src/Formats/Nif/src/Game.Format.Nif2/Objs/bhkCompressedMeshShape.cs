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
	/*! Compressed collision mesh. */
	public class bhkCompressedMeshShape : bhkShape
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("bhkCompressedMeshShape", bhkShape.TYPE);

		/*! Points to root node? */
		internal NiAVObject target;
		/*! Unknown. */
		internal uint userData;
		/*! A shell that is added around the shape. */
		internal float radius;
		/*! Unknown. */
		internal float unknownFloat1;
		/*! Scale */
		internal Vector4 scale;
		/*! A shell that is added around the shape. */
		internal float radiusCopy;
		/*! Scale */
		internal Vector4 scaleCopy;
		/*! The collision mesh data. */
		internal bhkCompressedMeshShapeData data;
		public bhkCompressedMeshShape()
		{
			target = null;
			userData = (uint)0;
			radius = 0.005f;
			unknownFloat1 = 0.0f;
			scale = (1.0, 1.0, 1.0, 0.0);
			radiusCopy = 0.005f;
			scaleCopy = (1.0, 1.0, 1.0, 0.0);
			data = null;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new bhkCompressedMeshShape();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out userData, s, info);
			Nif.NifStream(out radius, s, info);
			Nif.NifStream(out unknownFloat1, s, info);
			Nif.NifStream(out scale, s, info);
			Nif.NifStream(out radiusCopy, s, info);
			Nif.NifStream(out scaleCopy, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			WriteRef((NiObject)target, s, info, link_map, missing_link_stack);
			Nif.NifStream(userData, s, info);
			Nif.NifStream(radius, s, info);
			Nif.NifStream(unknownFloat1, s, info);
			Nif.NifStream(scale, s, info);
			Nif.NifStream(radiusCopy, s, info);
			Nif.NifStream(scaleCopy, s, info);
			WriteRef((NiObject)data, s, info, link_map, missing_link_stack);
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
			s.AppendLine($"      Target:  {target}");
			s.AppendLine($"      User Data:  {userData}");
			s.AppendLine($"      Radius:  {radius}");
			s.AppendLine($"      Unknown Float 1:  {unknownFloat1}");
			s.AppendLine($"      Scale:  {scale}");
			s.AppendLine($"      Radius Copy:  {radiusCopy}");
			s.AppendLine($"      Scale Copy:  {scaleCopy}");
			s.AppendLine($"      Data:  {data}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			target = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
			data = FixLink<bhkCompressedMeshShapeData>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (data != null)
				refs.Add((NiObject)data);
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
