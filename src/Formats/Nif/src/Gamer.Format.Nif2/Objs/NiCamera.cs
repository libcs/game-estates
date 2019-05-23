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
	/*! Camera object. */
	public class NiCamera : NiAVObject
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiCamera", NiAVObject.TYPE);

		/*! Obsolete flags. */
		internal ushort cameraFlags;
		/*! Frustrum left. */
		internal float frustumLeft;
		/*! Frustrum right. */
		internal float frustumRight;
		/*! Frustrum top. */
		internal float frustumTop;
		/*! Frustrum bottom. */
		internal float frustumBottom;
		/*! Frustrum near. */
		internal float frustumNear;
		/*! Frustrum far. */
		internal float frustumFar;
		/*! Determines whether perspective is used.  Orthographic means no perspective. */
		internal bool useOrthographicProjection;
		/*! Viewport left. */
		internal float viewportLeft;
		/*! Viewport right. */
		internal float viewportRight;
		/*! Viewport top. */
		internal float viewportTop;
		/*! Viewport bottom. */
		internal float viewportBottom;
		/*! Level of detail adjust. */
		internal float lodAdjust;
		/*! scene */
		internal NiAVObject scene;
		/*! Deprecated. Array is always zero length on disk write. */
		internal uint numScreenPolygons;
		/*! Deprecated. Array is always zero length on disk write. */
		internal uint numScreenTextures;
		/*! Unknown. */
		internal uint unknownInt3;
		public NiCamera()
		{
			cameraFlags = (ushort)0;
			frustumLeft = 0.0f;
			frustumRight = 0.0f;
			frustumTop = 0.0f;
			frustumBottom = 0.0f;
			frustumNear = 0.0f;
			frustumFar = 0.0f;
			useOrthographicProjection = false;
			viewportLeft = 0.0f;
			viewportRight = 0.0f;
			viewportTop = 0.0f;
			viewportBottom = 0.0f;
			lodAdjust = 0.0f;
			scene = null;
			numScreenPolygons = (uint)0;
			numScreenTextures = (uint)0;
			unknownInt3 = (uint)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiCamera();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			uint block_num;
			base.Read(s, link_stack, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out cameraFlags, s, info);
			}
			Nif.NifStream(out frustumLeft, s, info);
			Nif.NifStream(out frustumRight, s, info);
			Nif.NifStream(out frustumTop, s, info);
			Nif.NifStream(out frustumBottom, s, info);
			Nif.NifStream(out frustumNear, s, info);
			Nif.NifStream(out frustumFar, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(out useOrthographicProjection, s, info);
			}
			Nif.NifStream(out viewportLeft, s, info);
			Nif.NifStream(out viewportRight, s, info);
			Nif.NifStream(out viewportTop, s, info);
			Nif.NifStream(out viewportBottom, s, info);
			Nif.NifStream(out lodAdjust, s, info);
			Nif.NifStream(out block_num, s, info);
			link_stack.Add(block_num);
			Nif.NifStream(out numScreenPolygons, s, info);
			if (info.version >= 0x04020100)
			{
				Nif.NifStream(out numScreenTextures, s, info);
			}
			if (info.version <= 0x03010000)
			{
				Nif.NifStream(out unknownInt3, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(cameraFlags, s, info);
			}
			Nif.NifStream(frustumLeft, s, info);
			Nif.NifStream(frustumRight, s, info);
			Nif.NifStream(frustumTop, s, info);
			Nif.NifStream(frustumBottom, s, info);
			Nif.NifStream(frustumNear, s, info);
			Nif.NifStream(frustumFar, s, info);
			if (info.version >= 0x0A010000)
			{
				Nif.NifStream(useOrthographicProjection, s, info);
			}
			Nif.NifStream(viewportLeft, s, info);
			Nif.NifStream(viewportRight, s, info);
			Nif.NifStream(viewportTop, s, info);
			Nif.NifStream(viewportBottom, s, info);
			Nif.NifStream(lodAdjust, s, info);
			WriteRef((NiObject)scene, s, info, link_map, missing_link_stack);
			Nif.NifStream(numScreenPolygons, s, info);
			if (info.version >= 0x04020100)
			{
				Nif.NifStream(numScreenTextures, s, info);
			}
			if (info.version <= 0x03010000)
			{
				Nif.NifStream(unknownInt3, s, info);
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
			s.AppendLine($"      Camera Flags:  {cameraFlags}");
			s.AppendLine($"      Frustum Left:  {frustumLeft}");
			s.AppendLine($"      Frustum Right:  {frustumRight}");
			s.AppendLine($"      Frustum Top:  {frustumTop}");
			s.AppendLine($"      Frustum Bottom:  {frustumBottom}");
			s.AppendLine($"      Frustum Near:  {frustumNear}");
			s.AppendLine($"      Frustum Far:  {frustumFar}");
			s.AppendLine($"      Use Orthographic Projection:  {useOrthographicProjection}");
			s.AppendLine($"      Viewport Left:  {viewportLeft}");
			s.AppendLine($"      Viewport Right:  {viewportRight}");
			s.AppendLine($"      Viewport Top:  {viewportTop}");
			s.AppendLine($"      Viewport Bottom:  {viewportBottom}");
			s.AppendLine($"      LOD Adjust:  {lodAdjust}");
			s.AppendLine($"      Scene:  {scene}");
			s.AppendLine($"      Num Screen Polygons:  {numScreenPolygons}");
			s.AppendLine($"      Num Screen Textures:  {numScreenTextures}");
			s.AppendLine($"      Unknown Int 3:  {unknownInt3}");
			return s.ToString();
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void FixLinks(Dictionary<uint, NiObject> objects, List<uint> link_stack, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.FixLinks(objects, link_stack, missing_link_stack, info);
			scene = FixLink<NiAVObject>(objects, link_stack, missing_link_stack, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override List<NiObject> GetRefs()
		{
			var refs = base.GetRefs();
			if (scene != null)
				refs.Add((NiObject)scene);
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
