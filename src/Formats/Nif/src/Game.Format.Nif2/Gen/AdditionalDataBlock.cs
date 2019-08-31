/* Copyright (c) 2006, NIF File Format Library and Tools */
// THIS FILE WAS AUTOMATICALLY GENERATED.  DO NOT EDIT! //
// To change this file, alter the generate_cs.py Python script.

using System;
using System.IO;
using System.Collections.Generic;

namespace Niflib
{
	/*! AdditionalDataBlock */
	public class AdditionalDataBlock
	{
		/*! Has data */
		internal bool hasData;
		/*! Size of Block */
		internal int blockSize;
		/*! numBlocks */
		internal int numBlocks;
		/*! blockOffsets */
		internal IList<int> blockOffsets;
		/*! numData */
		internal int numData;
		/*! dataSizes */
		internal IList<int> dataSizes;
		/*! data */
		internal IList<byte[]> data;

		public AdditionalDataBlock()
		{
			unchecked
			{
				hasData = false;
				blockSize = (int)0;
				numBlocks = (int)0;
				numData = (int)0;
			}
		}
	}
}
