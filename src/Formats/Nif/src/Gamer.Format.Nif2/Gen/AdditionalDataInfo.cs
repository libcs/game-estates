/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! AdditionalDataInfo */
	public class AdditionalDataInfo
	{
		/*! Type of data in this channel */
		internal int dataType;
		/*! Number of bytes per element of this channel */
		internal int numChannelBytesPerElement;
		/*! Total number of bytes of this channel (num vertices times num bytes per element) */
		internal int numChannelBytes;
		/*! Number of bytes per element in all channels together. Sum of num channel bytes per element over all block infos. */
		internal int numTotalBytesPerElement;
		/*! Unsure. The block in which this channel is stored? Usually there is only one block, and so this is zero. */
		internal int blockIndex;
		/*! Offset (in bytes) of this channel. Sum of all num channel bytes per element of all preceeding block infos. */
		internal int channelOffset;
		/*! Unknown, usually equal to 2. */
		internal byte unknownByte1;

		public AdditionalDataInfo()
		{
			unchecked
			{
				dataType = (int)0;
				numChannelBytesPerElement = (int)0;
				numChannelBytes = (int)0;
				numTotalBytesPerElement = (int)0;
				blockIndex = (int)0;
				channelOffset = (int)0;
				unknownByte1 = (byte)2;
			}
		}
	}
}
