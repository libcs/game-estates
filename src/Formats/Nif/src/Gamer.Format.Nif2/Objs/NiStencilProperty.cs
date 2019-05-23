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
	/*! Allows control of stencil testing. */
	public class NiStencilProperty : NiProperty
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("NiStencilProperty", NiProperty.TYPE);

		/*! Property flags. */
		internal ushort flags;
		/*! Enables or disables the stencil test. */
		internal byte stencilEnabled;
		/*! Selects the compare mode function (see: glStencilFunc). */
		internal StencilCompareMode stencilFunction;
		/*! stencilRef */
		internal uint stencilRef;
		/*! A bit mask. The default is 0xffffffff. */
		internal uint stencilMask;
		/*! failAction */
		internal StencilAction failAction;
		/*! zFailAction */
		internal StencilAction zFailAction;
		/*! passAction */
		internal StencilAction passAction;
		/*! Used to enabled double sided faces. Default is 3 (DRAW_BOTH). */
		internal StencilDrawMode drawMode;
		public NiStencilProperty()
		{
			flags = (ushort)0;
			stencilEnabled = (byte)0;
			stencilFunction = (StencilCompareMode)0;
			stencilRef = (uint)0;
			stencilMask = (uint)4294967295;
			failAction = (StencilAction)0;
			zFailAction = (StencilAction)0;
			passAction = (StencilAction)0;
			drawMode = StencilDrawMode.DRAW_BOTH;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new NiStencilProperty();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(out flags, s, info);
			}
			if (info.version <= 0x14000005)
			{
				Nif.NifStream(out stencilEnabled, s, info);
				Nif.NifStream(out stencilFunction, s, info);
				Nif.NifStream(out stencilRef, s, info);
				Nif.NifStream(out stencilMask, s, info);
				Nif.NifStream(out failAction, s, info);
				Nif.NifStream(out zFailAction, s, info);
				Nif.NifStream(out passAction, s, info);
				Nif.NifStream(out drawMode, s, info);
			}
			if (info.version >= 0x14010003)
			{
				Nif.NifStream(out (ushort)flags, s, info);
				Nif.NifStream(out (uint)stencilRef, s, info);
				Nif.NifStream(out (uint)stencilMask, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			if (info.version <= 0x0A000102)
			{
				Nif.NifStream(flags, s, info);
			}
			if (info.version <= 0x14000005)
			{
				Nif.NifStream(stencilEnabled, s, info);
				Nif.NifStream(stencilFunction, s, info);
				Nif.NifStream(stencilRef, s, info);
				Nif.NifStream(stencilMask, s, info);
				Nif.NifStream(failAction, s, info);
				Nif.NifStream(zFailAction, s, info);
				Nif.NifStream(passAction, s, info);
				Nif.NifStream(drawMode, s, info);
			}
			if (info.version >= 0x14010003)
			{
				Nif.NifStream((ushort)flags, s, info);
				Nif.NifStream((uint)stencilRef, s, info);
				Nif.NifStream((uint)stencilMask, s, info);
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
			s.AppendLine($"      Flags:  {flags}");
			s.AppendLine($"      Stencil Enabled:  {stencilEnabled}");
			s.AppendLine($"      Stencil Function:  {stencilFunction}");
			s.AppendLine($"      Stencil Ref:  {stencilRef}");
			s.AppendLine($"      Stencil Mask:  {stencilMask}");
			s.AppendLine($"      Fail Action:  {failAction}");
			s.AppendLine($"      Z Fail Action:  {zFailAction}");
			s.AppendLine($"      Pass Action:  {passAction}");
			s.AppendLine($"      Draw Mode:  {drawMode}");
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
