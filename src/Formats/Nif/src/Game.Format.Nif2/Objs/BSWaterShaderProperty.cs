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
	/*! Skyrim water shader property, different from "WaterShaderProperty" seen in Fallout. */
	public class BSWaterShaderProperty : BSShaderProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSWaterShaderProperty", BSShaderProperty.TYPE);

		/*! shaderFlags1 */
		internal SkyrimShaderPropertyFlags1 shaderFlags1;
		/*! shaderFlags2 */
		internal SkyrimShaderPropertyFlags2 shaderFlags2;
		/*! Offset UVs. Seems to be unused, but it fits with the other Skyrim shader properties. */
		internal TexCoord uvOffset;
		/*! Offset UV Scale to repeat tiling textures, see above. */
		internal TexCoord uvScale;
		/*! Defines attributes for the water shader (will use SkyrimWaterShaderFlags) */
		internal SkyrimWaterShaderFlags waterShaderFlags;
		/*! A bitflag, only the first/second bit controls water flow positive or negative along UVs. */
		internal byte waterDirection;
		/*! Unknown, flag? */
		internal ushort unknownShort3;
		public BSWaterShaderProperty()
		{
			shaderFlags1 = (SkyrimShaderPropertyFlags1)0;
			shaderFlags2 = (SkyrimShaderPropertyFlags2)0;
			uvScale = (1.0, 1.0);
			waterShaderFlags = (SkyrimWaterShaderFlags)0;
			waterDirection = (byte)3;
			unknownShort3 = (ushort)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSWaterShaderProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out shaderFlags1, s, info);
			Nif.NifStream(out shaderFlags2, s, info);
			Nif.NifStream(out uvOffset, s, info);
			Nif.NifStream(out uvScale, s, info);
			Nif.NifStream(out waterShaderFlags, s, info);
			Nif.NifStream(out waterDirection, s, info);
			Nif.NifStream(out unknownShort3, s, info);
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			Nif.NifStream(shaderFlags1, s, info);
			Nif.NifStream(shaderFlags2, s, info);
			Nif.NifStream(uvOffset, s, info);
			Nif.NifStream(uvScale, s, info);
			Nif.NifStream(waterShaderFlags, s, info);
			Nif.NifStream(waterDirection, s, info);
			Nif.NifStream(unknownShort3, s, info);
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
			s.AppendLine($"      Shader Flags 1:  {shaderFlags1}");
			s.AppendLine($"      Shader Flags 2:  {shaderFlags2}");
			s.AppendLine($"      UV Offset:  {uvOffset}");
			s.AppendLine($"      UV Scale:  {uvScale}");
			s.AppendLine($"      Water Shader Flags:  {waterShaderFlags}");
			s.AppendLine($"      Water Direction:  {waterDirection}");
			s.AppendLine($"      Unknown Short 3:  {unknownShort3}");
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
