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
	/*! BSPackedAdditionalGeometryData */
	public class BSPackedAdditionalGeometryData : AbstractAdditionalGeometryData
	{
		// Definition of TYPE constant
		public static readonly Type_ TYPE = new Type_("BSPackedAdditionalGeometryData", AbstractAdditionalGeometryData.TYPE);

		/*! numVertices */
		internal ushort numVertices;
		/*! Information about additional data blocks */
		internal uint numBlockInfos;
		/*! Number of additional data blocks */
		internal IList<AdditionalDataInfo> blockInfos;
		/*! Number of additional data blocks. Usually there is exactly one block. */
		internal int numBlocks;
		/*! Number of additional data blocks */
		internal IList<BSPackedAdditionalDataBlock> blocks;
		public BSPackedAdditionalGeometryData()
		{
			numVertices = (ushort)0;
			numBlockInfos = (uint)0;
			numBlocks = (int)0;
		}

		/*! Used to determine the type of a particular instance of this object. \return The type constant for the actual type of the object. */
		public override Type_ GetType() => TYPE;

		/*!
		 * A factory function used during file reading to create an instance of this type of object.
		 * \return A pointer to a newly allocated instance of this type of object.
		 */
		public static NiObject Create() => new BSPackedAdditionalGeometryData();

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Read(IStream s, List<uint> link_stack, NifInfo info)
		{
			base.Read(s, link_stack, info);
			Nif.NifStream(out numVertices, s, info);
			Nif.NifStream(out numBlockInfos, s, info);
			blockInfos = new AdditionalDataInfo[numBlockInfos];
			for (var i3 = 0; i3 < blockInfos.Count; i3++)
			{
				Nif.NifStream(out blockInfos[i3].dataType, s, info);
				Nif.NifStream(out blockInfos[i3].numChannelBytesPerElement, s, info);
				Nif.NifStream(out blockInfos[i3].numChannelBytes, s, info);
				Nif.NifStream(out blockInfos[i3].numTotalBytesPerElement, s, info);
				Nif.NifStream(out blockInfos[i3].blockIndex, s, info);
				Nif.NifStream(out blockInfos[i3].channelOffset, s, info);
				Nif.NifStream(out blockInfos[i3].unknownByte1, s, info);
			}
			Nif.NifStream(out numBlocks, s, info);
			blocks = new BSPackedAdditionalDataBlock[numBlocks];
			for (var i3 = 0; i3 < blocks.Count; i3++)
			{
				Nif.NifStream(out blocks[i3].hasData, s, info);
				if (blocks[i3].hasData)
				{
					Nif.NifStream(out blocks[i3].numTotalBytes, s, info);
					Nif.NifStream(out blocks[i3].numBlocks, s, info);
					blocks[i3].blockOffsets = new int[blocks[i3].numBlocks];
					for (var i5 = 0; i5 < blocks[i3].blockOffsets.Count; i5++)
					{
						Nif.NifStream(out blocks[i3].blockOffsets[i5], s, info);
					}
					Nif.NifStream(out blocks[i3].numAtoms, s, info);
					blocks[i3].atomSizes = new int[blocks[i3].numAtoms];
					for (var i5 = 0; i5 < blocks[i3].atomSizes.Count; i5++)
					{
						Nif.NifStream(out blocks[i3].atomSizes[i5], s, info);
					}
					blocks[i3].data = new byte[blocks[i3].numTotalBytes];
					for (var i5 = 0; i5 < blocks[i3].data.Count; i5++)
					{
						Nif.NifStream(out blocks[i3].data[i5], s, info);
					}
				}
				Nif.NifStream(out blocks[i3].unknownInt1, s, info);
				Nif.NifStream(out blocks[i3].numTotalBytesPerElement, s, info);
			}
		}

		/*! NIFLIB_HIDDEN function.  For internal use only. */
		internal override void Write(OStream s, Dictionary<NiObject, uint> link_map, List<NiObject> missing_link_stack, NifInfo info)
		{
			base.Write(s, link_map, missing_link_stack, info);
			numBlocks = (int)blocks.Count;
			numBlockInfos = (uint)blockInfos.Count;
			Nif.NifStream(numVertices, s, info);
			Nif.NifStream(numBlockInfos, s, info);
			for (var i3 = 0; i3 < blockInfos.Count; i3++)
			{
				Nif.NifStream(blockInfos[i3].dataType, s, info);
				Nif.NifStream(blockInfos[i3].numChannelBytesPerElement, s, info);
				Nif.NifStream(blockInfos[i3].numChannelBytes, s, info);
				Nif.NifStream(blockInfos[i3].numTotalBytesPerElement, s, info);
				Nif.NifStream(blockInfos[i3].blockIndex, s, info);
				Nif.NifStream(blockInfos[i3].channelOffset, s, info);
				Nif.NifStream(blockInfos[i3].unknownByte1, s, info);
			}
			Nif.NifStream(numBlocks, s, info);
			for (var i3 = 0; i3 < blocks.Count; i3++)
			{
				blocks[i3].numAtoms = (int)blocks[i3].atomSizes.Count;
				blocks[i3].numBlocks = (int)blocks[i3].blockOffsets.Count;
				blocks[i3].numTotalBytes = (int)blocks[i3].data.Count;
				Nif.NifStream(blocks[i3].hasData, s, info);
				if (blocks[i3].hasData)
				{
					Nif.NifStream(blocks[i3].numTotalBytes, s, info);
					Nif.NifStream(blocks[i3].numBlocks, s, info);
					for (var i5 = 0; i5 < blocks[i3].blockOffsets.Count; i5++)
					{
						Nif.NifStream(blocks[i3].blockOffsets[i5], s, info);
					}
					Nif.NifStream(blocks[i3].numAtoms, s, info);
					for (var i5 = 0; i5 < blocks[i3].atomSizes.Count; i5++)
					{
						Nif.NifStream(blocks[i3].atomSizes[i5], s, info);
					}
					for (var i5 = 0; i5 < blocks[i3].data.Count; i5++)
					{
						Nif.NifStream(blocks[i3].data[i5], s, info);
					}
				}
				Nif.NifStream(blocks[i3].unknownInt1, s, info);
				Nif.NifStream(blocks[i3].numTotalBytesPerElement, s, info);
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
			numBlocks = (int)blocks.Count;
			numBlockInfos = (uint)blockInfos.Count;
			s.AppendLine($"      Num Vertices:  {numVertices}");
			s.AppendLine($"      Num Block Infos:  {numBlockInfos}");
			array_output_count = 0;
			for (var i3 = 0; i3 < blockInfos.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				s.AppendLine($"        Data Type:  {blockInfos[i3].dataType}");
				s.AppendLine($"        Num Channel Bytes Per Element:  {blockInfos[i3].numChannelBytesPerElement}");
				s.AppendLine($"        Num Channel Bytes:  {blockInfos[i3].numChannelBytes}");
				s.AppendLine($"        Num Total Bytes Per Element:  {blockInfos[i3].numTotalBytesPerElement}");
				s.AppendLine($"        Block Index:  {blockInfos[i3].blockIndex}");
				s.AppendLine($"        Channel Offset:  {blockInfos[i3].channelOffset}");
				s.AppendLine($"        Unknown Byte 1:  {blockInfos[i3].unknownByte1}");
			}
			s.AppendLine($"      Num Blocks:  {numBlocks}");
			array_output_count = 0;
			for (var i3 = 0; i3 < blocks.Count; i3++)
			{
				if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
				{
					s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
					break;
				}
				blocks[i3].numAtoms = (int)blocks[i3].atomSizes.Count;
				blocks[i3].numBlocks = (int)blocks[i3].blockOffsets.Count;
				blocks[i3].numTotalBytes = (int)blocks[i3].data.Count;
				s.AppendLine($"        Has Data:  {blocks[i3].hasData}");
				if (blocks[i3].hasData)
				{
					s.AppendLine($"          Num Total Bytes:  {blocks[i3].numTotalBytes}");
					s.AppendLine($"          Num Blocks:  {blocks[i3].numBlocks}");
					array_output_count = 0;
					for (var i5 = 0; i5 < blocks[i3].blockOffsets.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Block Offsets[{i5}]:  {blocks[i3].blockOffsets[i5]}");
						array_output_count++;
					}
					s.AppendLine($"          Num Atoms:  {blocks[i3].numAtoms}");
					array_output_count = 0;
					for (var i5 = 0; i5 < blocks[i3].atomSizes.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Atom Sizes[{i5}]:  {blocks[i3].atomSizes[i5]}");
						array_output_count++;
					}
					array_output_count = 0;
					for (var i5 = 0; i5 < blocks[i3].data.Count; i5++)
					{
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
						{
							s.AppendLine("<Data Truncated. Use verbose mode to see complete listing.>");
							break;
						}
						if (!verbose && (array_output_count > Nif.MAXARRAYDUMP))
							break;
						s.AppendLine($"            Data[{i5}]:  {blocks[i3].data[i5]}");
						array_output_count++;
					}
				}
				s.AppendLine($"        Unknown Int 1:  {blocks[i3].unknownInt1}");
				s.AppendLine($"        Num Total Bytes Per Element:  {blocks[i3].numTotalBytesPerElement}");
			}
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
