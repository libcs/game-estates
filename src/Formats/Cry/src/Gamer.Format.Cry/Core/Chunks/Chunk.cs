using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    public abstract class Chunk : IBinaryChunk
    {
        private static Dictionary<Type, Dictionary<uint, Func<dynamic>>> _chunkFactoryCache = new Dictionary<Type, Dictionary<uint, Func<dynamic>>> { };

        public static Chunk New(ChunkTypeEnum chunkType, uint version)
        {
            switch (chunkType)
            {
                case ChunkTypeEnum.SourceInfo: return New<ChunkSourceInfo>(version);
                case ChunkTypeEnum.Timing: return New<ChunkTimingFormat>(version);
                case ChunkTypeEnum.ExportFlags: return New<ChunkExportFlags>(version);
                case ChunkTypeEnum.MtlName: return New<ChunkMtlName>(version);
                case ChunkTypeEnum.DataStream: return New<ChunkDataStream>(version);
                case ChunkTypeEnum.Mesh: return New<ChunkMesh>(version);
                case ChunkTypeEnum.MeshSubsets: return New<ChunkMeshSubsets>(version);
                case ChunkTypeEnum.Node: return New<ChunkNode>(version);
                case ChunkTypeEnum.Helper: return New<ChunkHelper>(version);
                case ChunkTypeEnum.Controller: return New<ChunkController>(version);
                case ChunkTypeEnum.SceneProps: return New<ChunkSceneProp>(version);
                case ChunkTypeEnum.MeshPhysicsData: return New<ChunkMeshPhysicsData>(version);
                case ChunkTypeEnum.BoneAnim: return New<ChunkBoneAnim>(version);
                // Compiled chunks
                case ChunkTypeEnum.CompiledBones: return New<ChunkCompiledBones>(version);
                case ChunkTypeEnum.CompiledPhysicalProxies: return New<ChunkCompiledPhysicalProxies>(version);
                case ChunkTypeEnum.CompiledPhysicalBones: return New<ChunkCompiledPhysicalBones>(version);
                case ChunkTypeEnum.CompiledIntSkinVertices: return New<ChunkCompiledIntSkinVertices>(version);
                case ChunkTypeEnum.CompiledMorphTargets: return New<ChunkCompiledMorphTargets>(version);
                case ChunkTypeEnum.CompiledExt2IntMap: return New<ChunkCompiledExtToIntMap>(version);
                case ChunkTypeEnum.CompiledIntFaces: return New<ChunkCompiledIntFaces>(version);
                // Star Citizen equivalents
                case ChunkTypeEnum.CompiledBonesSC: return New<ChunkCompiledBones>(version);
                case ChunkTypeEnum.CompiledPhysicalBonesSC: return New<ChunkCompiledPhysicalBones>(version);
                case ChunkTypeEnum.CompiledExt2IntMapSC: return New<ChunkCompiledExtToIntMap>(version);
                case ChunkTypeEnum.CompiledIntFacesSC: return New<ChunkCompiledIntFaces>(version);
                case ChunkTypeEnum.CompiledIntSkinVerticesSC: return New<ChunkCompiledIntSkinVertices>(version);
                case ChunkTypeEnum.CompiledMorphTargetsSC: return New<ChunkCompiledMorphTargets>(version);
                case ChunkTypeEnum.CompiledPhysicalProxiesSC: return New<ChunkCompiledPhysicalProxies>(version);
                // Old chunks
                case ChunkTypeEnum.BoneNameList: return New<ChunkBoneNameList>(version);
                case ChunkTypeEnum.MeshMorphTarget: return New<ChunkMeshMorphTargets>(version);
                case ChunkTypeEnum.Mtl: //Log("Mtl Chunk here");  // Obsolete.  Not used
                default: return new ChunkUnknown();
            }
        }

        public static T New<T>(uint version) where T : Chunk
        {
            if (!_chunkFactoryCache.TryGetValue(typeof(T), out var versionMap))
                _chunkFactoryCache[typeof(T)] = versionMap = new Dictionary<uint, Func<dynamic>> { };
            if (!versionMap.TryGetValue(version, out var factory))
            {
                var targetType = (from type in Assembly.GetExecutingAssembly().GetTypes()
                                  where !type.IsAbstract
                                  where type.IsClass
                                  where !type.IsGenericType
                                  where typeof(T).IsAssignableFrom(type)
                                  where type.Name == $"{typeof(T).Name}_{version:X}"
                                  select type).FirstOrDefault();
                if (targetType != null)
                    factory = () => Activator.CreateInstance(targetType) as T;
                _chunkFactoryCache[typeof(T)][version] = factory;
            }
            return (factory?.Invoke() as T) ?? throw new NotSupportedException($"Version {version:X} of {typeof(T).Name} is not supported"); ;
        }

        public void Load(Model model, ChunkHeader header)
        {
            _model = model;
            _header = header;
        }

        internal ChunkHeader _header;
        public Model _model;

        /// <summary>
        /// Position of the start of the chunk
        /// </summary>
        public uint Offset { get; internal set; }
        /// <summary>
        /// The Type of the Chunk
        /// </summary>
        public ChunkTypeEnum ChunkType { get; internal set; }
        /// <summary>
        /// The Version of this Chunk
        /// </summary>
        public uint Version;
        /// <summary>
        /// The ID of this Chunk
        /// </summary>
        public int ID;
        /// <summary>
        /// The Size of this Chunk (in Bytes)
        /// </summary>
        public uint Size;
        /// <summary>
        /// Size of the data in the chunk.  This is the chunk size, minus the header (if there is one)
        /// </summary>
        public uint DataSize { get; set; }

        public Dictionary<long, byte> SkippedBytes = new Dictionary<long, byte> { };

        public void SkipBytes(BinaryReader r, long? bytesToSkip = null)
        {
            if (r == null)
                return;
            if (r.BaseStream.Position > Offset + Size && Size > 0)
                Log($"Buffer Overflow in {GetType().Name} 0x{ID:X} ({r.BaseStream.Position - Offset - Size} bytes)");
            if (r.BaseStream.Length < Offset + Size)
                Log($"Corrupt Headers in {GetType().Name} 0x{ID:X}");
            if (!bytesToSkip.HasValue)
                bytesToSkip = Size - Math.Max(r.BaseStream.Position - Offset, 0);
            for (var i = 0L; i < bytesToSkip; i++)
            {
                SkippedBytes[r.BaseStream.Position - Offset] = r.ReadByte();
                // if (SkippedBytes[r.BaseStream.Position - Offset - 1] == 0)
                //     SkippedBytes.Remove(r.BaseStream.Position - Offset - 1);
            }
        }

        public virtual void Read(BinaryReader r)
        {
            ChunkType = _header.ChunkType;
            Version = _header.Version;
            Offset = _header.Offset;
            ID = _header.ID;
            Size = _header.Size;
            DataSize = Size; // For SC files, there is no header in chunks.  But need Datasize to calculate things.
            r.BaseStream.Seek(_header.Offset, 0);
            // Star Citizen files don't have the type, version, offset and ID at the start of a chunk, so don't read them.
            if (_model.FileVersion == FileVersionEnum.CryTek_3_4 || _model.FileVersion == FileVersionEnum.CryTek_3_5)
            {
                ChunkType = (ChunkTypeEnum)Enum.ToObject(typeof(ChunkTypeEnum), r.ReadUInt32());
                Version = r.ReadUInt32();
                Offset = r.ReadUInt32();
                ID = r.ReadInt32();
                DataSize = Size - 16;
            }
            if (Offset != _header.Offset || Size != _header.Size)
            {
                Log($"Conflict in chunk definition");
                Log($"{_header.Offset:X}+{_header.Size:X}");
                Log($"{Offset:X}+{Size:X}");
            }
        }

        /// <summary>
        /// Gets a link to the SkinningInfo model.
        /// </summary>
        /// <returns>Link to the SkinningInfo model.</returns>
        public SkinningInfo GetSkinningInfo()
        {
            if (_model.SkinningInfo == null)
                _model.SkinningInfo = new SkinningInfo();
            return _model.SkinningInfo;
        }

        public virtual void Write(BinaryWriter w) => throw new NotImplementedException();

        public virtual void WriteChunk()
        {
            Log($"*** CHUNK ***");
            Log($"    ChunkType: {ChunkType}");
            Log($"    ChunkVersion: {Version:X}");
            Log($"    Offset: {Offset:X}");
            Log($"    ID: {ID:X}");
            Log($"    Size: {Size:X}");
            Log($"*** END CHUNK ***");
        }
    }
}
