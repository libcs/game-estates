using Gamer.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace Gamer.Estate.Ultima.Resources
{
    public class TileMatrixDataPatch
    {
        public interface IMapDiffInfo
        {
            int MapCount { get; }
            int[] MapPatches { get; }
            int[] StaticPatches { get; }
        }

        static IMapDiffInfo EnabledDiffs;
        public static void EnableMapDiffs(IMapDiffInfo diffs) => EnabledDiffs = diffs;

        FileStream _landPatchStream;
        FileStream _staticPatchStream;

        Dictionary<uint, LandPatchData> _landPatchPtrs;
        Dictionary<uint, StaticPatchData> _staticPatchPtrs;

        class LandPatchData
        {
            public readonly uint Index;
            public readonly uint Pointer;
            public LandPatchData Next;

            public LandPatchData(uint index, uint ptr)
            {
                Index = index;
                Pointer = ptr;
            }
        }

        class StaticPatchData
        {
            public readonly uint Index;
            public readonly uint Pointer;
            public readonly int Length; // lengths can be negative; if they are, then they should be ignored.
            public StaticPatchData Next;

            public StaticPatchData(uint index, uint ptr, int length)
            {
                Index = index;
                Pointer = ptr;
                Length = length;
            }
        }

        public TileMatrixDataPatch(TileMatrixData matrix, uint idx)
        {
            LoadLandPatches(matrix, $"mapdif{idx}.mul", $"mapdifl{idx}.mul");
            LoadStaticPatches(matrix, $"stadif{idx}.mul", $"stadifl{idx}.mul", $"stadifi{idx}.mul");
        }

        uint MakeChunkKey(uint blockX, uint blockY) => ((blockY & 0x0000ffff) << 16) | (blockX & 0x0000ffff);

        public unsafe bool TryGetLandPatch(uint map, uint blockX, uint blockY, ref byte[] landData)
        {
            if (ClientVersion.InstallationIsUopFormat)
                return false;
            var key = MakeChunkKey(blockX, blockY);
            if (_landPatchPtrs.TryGetValue(key, out LandPatchData data))
            {
                if (EnabledDiffs != null && data.Index >= EnabledDiffs.MapPatches[map])
                    return false;
                while (data.Next != null)
                {
                    if (EnabledDiffs != null && data.Next.Index >= EnabledDiffs.MapPatches[map])
                        break;
                    data = data.Next;
                }
                _landPatchStream.Seek(data.Pointer, SeekOrigin.Begin);
                landData = new byte[192];
                _landPatchStream.ReadBuffer(landData, 192);
                return true;
            }
            return false;
        }

        unsafe int LoadLandPatches(TileMatrixData tileMatrix, string landPath, string indexPath)
        {
            _landPatchPtrs = new Dictionary<uint, LandPatchData>();
            if (ClientVersion.InstallationIsUopFormat)
                return 0;
            _landPatchStream = UltimaFileManager.GetFile(landPath);
            if (_landPatchStream == null)
                return 0;
            using (var fsIndex = UltimaFileManager.GetFile(indexPath))
            {
                var indexReader = new BinaryReader(fsIndex);
                var count = (int)(indexReader.BaseStream.Length / 4);
                var ptr = 0U;
                for (var i = 0U; i < count; ++i)
                {
                    var blockId = indexReader.ReadUInt32();
                    var x = blockId / tileMatrix.ChunkHeight;
                    var y = blockId % tileMatrix.ChunkHeight;
                    var key = MakeChunkKey(x, y);
                    ptr += 4;
                    if (_landPatchPtrs.ContainsKey(key))
                    {
                        var current = _landPatchPtrs[key];
                        while (current.Next != null)
                            current = current.Next;
                        current.Next = new LandPatchData(i, ptr);
                    }
                    else _landPatchPtrs.Add(key, new LandPatchData(i, ptr));
                    ptr += 192;
                }
                indexReader.Close();
                return count;
            }
        }

        public unsafe bool TryGetStaticChunk(uint map, uint blockX, uint blockY, ref byte[] staticData, out int length)
        {
            try
            {
                length = 0;
                if (ClientVersion.InstallationIsUopFormat)
                    return false;
                var key = MakeChunkKey(blockX, blockY);
                if (_staticPatchPtrs.TryGetValue(key, out StaticPatchData data))
                {
                    if (EnabledDiffs != null && data.Index >= EnabledDiffs.StaticPatches[map])
                        return false;
                    while (data.Next != null)
                    {
                        if (EnabledDiffs != null && data.Next.Index >= EnabledDiffs.StaticPatches[map])
                            break;
                        data = data.Next;
                    }
                    if (data.Pointer == 0 || data.Length <= 0)
                        return false;
                    length = data.Length;
                    _staticPatchStream.Seek(data.Pointer, SeekOrigin.Begin);
                    if (length > staticData.Length)
                        staticData = new byte[length];
                    _staticPatchStream.ReadBuffer(staticData, length);
                    return true;
                }
                length = 0;
                return false;
            }
            catch (EndOfStreamException) { throw new Exception("End of stream in static patch block!"); }
        }

        unsafe int LoadStaticPatches(TileMatrixData tileMatrix, string dataPath, string indexPath, string lookupPath)
        {
            _staticPatchPtrs = new Dictionary<uint, StaticPatchData>();
            _staticPatchStream = UltimaFileManager.GetFile(dataPath);
            if (_staticPatchStream == null)
                return 0;
            using (var fsIndex = UltimaFileManager.GetFile(indexPath))
            using (var fsLookup = UltimaFileManager.GetFile(lookupPath))
            {
                var indexReader = new BinaryReader(fsIndex);
                var lookupReader = new BinaryReader(fsLookup);
                var count = (int)(indexReader.BaseStream.Length / 4);
                for (var i = 0U; i < count; ++i)
                {
                    var blockID = indexReader.ReadUInt32();
                    var blockX = blockID / tileMatrix.ChunkHeight;
                    var blockY = blockID % tileMatrix.ChunkHeight;
                    var key = MakeChunkKey(blockX, blockY);
                    var offset = lookupReader.ReadUInt32();
                    var length = lookupReader.ReadInt32();
                    lookupReader.ReadInt32();
                    if (_staticPatchPtrs.ContainsKey(key))
                    {
                        var current = _staticPatchPtrs[key];
                        while (current.Next != null)
                            current = current.Next;
                        current.Next = new StaticPatchData(i, offset, length);
                    }
                    else _staticPatchPtrs.Add(key, new StaticPatchData(i, offset, length));
                }
                indexReader.Close();
                lookupReader.Close();
                return count;
            }
        }
    }
}