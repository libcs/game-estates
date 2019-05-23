/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*!
	 * In a .kf file, this links to a controllable object, via its name (or for version 10.2.0.0 and up, a link and offset to a NiStringPalette that contains the
	 * name), and a sequence of interpolators that apply to this controllable object, via links.
	 *         For Controller ID, NiInterpController::GetCtlrID() virtual function returns a string formatted specifically for the derived type.
	 *         For Interpolator ID, NiInterpController::GetInterpolatorID() virtual function returns a string formatted specifically for the derived type.
	 *         The string formats are documented on the relevant niobject blocks.
	 */
	public class ControlledBlock
	{
		/*! Name of a controllable object in another NIF file. */
		internal IndexString targetName;
		/*! interpolator */
		internal NiInterpolator interpolator;
		/*! controller */
		internal NiTimeController controller;
		/*! blendInterpolator */
		internal NiBlendInterpolator blendInterpolator;
		/*! blendIndex */
		internal ushort blendIndex;
		/*! Idle animations tend to have low values for this, and high values tend to correspond with the important parts of the animations. */
		internal byte priority;
		/*! The name of the animated NiAVObject. */
		internal IndexString nodeName;
		/*! The RTTI type of the NiProperty the controller is attached to, if applicable. */
		internal IndexString propertyType;
		/*! The RTTI type of the NiTimeController. */
		internal IndexString controllerType;
		/*! An ID that can uniquely identify the controller among others of the same type on the same NiObjectNET. */
		internal IndexString controllerId;
		/*! An ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET. */
		internal IndexString interpolatorId;
		/*! Refers to the NiStringPalette which contains the name of the controlled NIF object. */
		internal NiStringPalette stringPalette;
		/*! Offset in NiStringPalette to the name of the animated NiAVObject. */
		internal uint nodeNameOffset;
		/*! Offset in NiStringPalette to the RTTI type of the NiProperty the controller is attached to, if applicable. */
		internal uint propertyTypeOffset;
		/*! Offset in NiStringPalette to the RTTI type of the NiTimeController. */
		internal uint controllerTypeOffset;
		/*! Offset in NiStringPalette to an ID that can uniquely identify the controller among others of the same type on the same NiObjectNET. */
		internal uint controllerIdOffset;
		/*! Offset in NiStringPalette to an ID that can uniquely identify the interpolator among others of the same type on the same NiObjectNET. */
		internal uint interpolatorIdOffset;

		public ControlledBlock()
		{
			unchecked
			{
				interpolator = null;
				controller = null;
				blendInterpolator = null;
				blendIndex = (ushort)0;
				priority = (byte)0;
				stringPalette = null;
				nodeNameOffset = (uint)-1;
				propertyTypeOffset = (uint)-1;
				controllerTypeOffset = (uint)-1;
				controllerIdOffset = (uint)-1;
				interpolatorIdOffset = (uint)-1;
			}
		}
	}
}
