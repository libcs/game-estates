using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Gamer.Core.Debug;

namespace Gamer.Format.Cry.Core
{
    /// <summary>
    /// CryEngine cgf/cga/skin file handler
    /// 
    /// Structure:
    ///   HEADER        <- Provides information about the format of the file
    ///   CHUNKHEADER[] <- Provides information about locations of CHUNKs
    ///   CHUNK[]
    /// </summary>
    public class Model
    {
        /// <summary>
        /// The Root of the loaded object
        /// </summary>
        public ChunkNode RootNode { get; internal set; }

        /// <summary>
        /// Collection of all loaded Chunks
        /// </summary>
        public List<ChunkHeader> ChunkHeaders { get; internal set; }

        /// <summary>
        /// All the node chunks in this Model
        /// </summary>
        public List<ChunkNode> ChunkNodes { get; set; }

        /// <summary>
        /// Lookup Table for Chunks, indexed by ChunkID
        /// </summary>
        public Dictionary<int, Chunk> ChunkMap { get; internal set; }

        /// <summary>
        /// The name of the currently processed file
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// The File Signature - CryTek for 3.5 and lower. CrCh for 3.6 and higher
        /// </summary>
        public string FileSignature { get; internal set; }

        /// <summary>
        /// The type of file (geometry or animation)
        /// </summary>
        public FileTypeEnum FileType { get; internal set; }

        /// <summary>
        /// The version of the file
        /// </summary>
        public FileVersionEnum FileVersion { get; internal set; }

        /// <summary>
        /// Position of the Chunk Header table
        /// </summary>
        public int ChunkTableOffset { get; internal set; }

        /// <summary>
        /// Contains all the information about bones and skinning them.  This a reference to the Cryengine object, since multiple Models can exist for a single object).
        /// </summary>
        public SkinningInfo SkinningInfo { get; set; }

        /// <summary>
        /// The Bones in the model.  The CompiledBones chunk will have a unique RootBone.
        /// </summary>
        public ChunkCompiledBones Bones { get; internal set; }

        public uint NumChunks { get; internal set; }

        Dictionary<int, ChunkNode> _nodeMap { get; set; }
        /// <summary>
        /// Node map for this model only.
        /// </summary>
        public Dictionary<int, ChunkNode> NodeMap      // This isn't right.  Nodes can have duplicate names.
        {
            get
            {
                if (_nodeMap == null)
                {
                    _nodeMap = new Dictionary<int, ChunkNode>() { };
                    ChunkNode rootNode = null;
                    //Log("Mapping Model Nodes");
                    RootNode = rootNode = rootNode ?? RootNode;  // Each model will have it's own rootnode.
                    foreach (var node in ChunkMap.Values.Where(c => c.ChunkType == ChunkTypeEnum.Node).Select(c => c as ChunkNode))
                    {
                        // Preserve existing parents
                        if (_nodeMap.ContainsKey(node.ID))
                        {
                            var parentNode = _nodeMap[node.ID].ParentNode;
                            if (parentNode != null)
                                parentNode = _nodeMap[parentNode.ID];
                            node.ParentNode = parentNode;
                        }
                        _nodeMap[node.ID] = node;
                    }
                }
                return _nodeMap;
            }
        }
        // CALCULATED
        public List<ChunkHeader> _chunks = new List<ChunkHeader> { };
        public int NodeCount => ChunkMap.Values.Where(c => c.ChunkType == ChunkTypeEnum.Node).Count();
        public int BoneCount => ChunkMap.Values.Where(c => c.ChunkType == ChunkTypeEnum.CompiledBones).Count();

        public Model()
        {
            ChunkMap = new Dictionary<int, Chunk> { };
            ChunkHeaders = new List<ChunkHeader> { };
            SkinningInfo = new SkinningInfo();
        }

        /// <summary>
        /// Load the specified file as a Model, and return it
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Model FromFile(string fileName) { var r = new Model(); r.Load(fileName); return r; }

        /// <summary>
        /// Load a cgf/cga/skin file
        /// </summary>
        /// <param name="fileName"></param>
        public void Load(string fileName)
        {
            var inputFile = new FileInfo(fileName);
            Console.Title = $"Processing {inputFile.Name}...";
            FileName = inputFile.Name;
            if (!inputFile.Exists)
                throw new FileNotFoundException();
            // Open the file for reading.
            var r = new BinaryReader(File.Open(fileName, FileMode.Open));
            // Get the header.  This isn't essential for .cgam files, but we need this info to find the version and offset to the chunk table
            Read_FileHeader(r);
            Read_ChunkHeaders(r);
            Read_Chunks(r);
        }

        /// <summary>
        /// Output File Header to console for testing
        /// </summary>
        public void WriteFileHeader()
        {
            Log($"*** HEADER ***");
            Log($"    Header Filesignature: {FileSignature}");
            Log($"    FileType:            {FileType:X}");
            Log($"    ChunkVersion:        {FileVersion:X}");
            Log($"    ChunkTableOffset:    {ChunkTableOffset:X}");
            Log($"    NumChunks:           {NumChunks:X}");
            Log($"*** END HEADER ***");
            return;
        }

        /// <summary>
        /// Output Chunk Table to console for testing
        /// </summary>
        public void WriteChunkTable()
        {
            Log("*** Chunk Header Table***");
            Log("Chunk Type              Version   ID        Size      Offset    ");
            foreach (ChunkHeader chkHdr in _chunks)
                Log($"{chkHdr.ChunkType,-24:X}{chkHdr.Version,-10:X}{chkHdr.ID,-10:X}{chkHdr.Size,-10:X}{chkHdr.Offset,-10:X}");
            Console.WriteLine("*** Chunk Header Table***");
            Console.WriteLine("Chunk Type              Version   ID        Size      Offset    ");
            foreach (ChunkHeader chkHdr in _chunks)
                Console.WriteLine($"{chkHdr.ChunkType,-24:X}{chkHdr.Version,-10:X}{chkHdr.ID,-10:X}{chkHdr.Size,-10:X}{chkHdr.Offset,-10:X}");
        }

        /// <summary>
        /// Read FileHeader from stream
        /// </summary>
        /// <param name="r"></param>
        void Read_FileHeader(BinaryReader r)
        {
            // FILESIGNATURE V3.6+
            r.BaseStream.Seek(0, 0);
            FileSignature = r.ReadFString(4);
            if (FileSignature == "CrCh")
            {
                // Version 3.6 or later
                FileVersion = (FileVersionEnum)r.ReadUInt32();    // 0x746
                NumChunks = r.ReadUInt32();       // number of Chunks in the chunk table
                ChunkTableOffset = r.ReadInt32(); // location of the chunk table
                return;
            }
            // FILESIGNATURE V3.5-
            r.BaseStream.Seek(0, 0);
            FileSignature = r.ReadFString(8);
            if (FileSignature == "CryTek")
            {
                // Version 3.5 or earlier
                FileType = (FileTypeEnum)r.ReadUInt32();
                FileVersion = (FileVersionEnum)r.ReadUInt32();    // 0x744 0x745
                ChunkTableOffset = r.ReadInt32() + 4;
                NumChunks = r.ReadUInt32();       // number of Chunks in the chunk table
                return;
            }
            throw new NotSupportedException($"Unsupported FileSignature {FileSignature}");
        }

        /// <summary>
        /// Read HeaderTable from stream
        /// </summary>
        /// <typeparam name="TChunkHeader"></typeparam>
        /// <param name="r">BinaryReader of file being read</param>
        void Read_ChunkHeaders(BinaryReader r)
        {
            // need to seek to the start of the table here.  foffset points to the start of the table
            r.BaseStream.Seek(ChunkTableOffset, SeekOrigin.Begin);
            for (var i = 0; i < NumChunks; i++)
            {
                var header = Chunk.New<ChunkHeader>((UInt32)this.FileVersion);
                header.Read(r);
                _chunks.Add(header);
            }
            //WriteChunkTable();
        }

        /// <summary>
        /// Reads all the chunks in the Cryengine file.
        /// </summary>
        /// <param name="r">BinaryReader for the Cryengine file.</param>
        void Read_Chunks(BinaryReader r)
        {
            foreach (var chkHdr in _chunks)
            {
                ChunkMap[chkHdr.ID] = Chunk.New(chkHdr.ChunkType, chkHdr.Version);
                ChunkMap[chkHdr.ID].Load(this, chkHdr);
                ChunkMap[chkHdr.ID].Read(r);

                // Ensure we read to end of structure
                ChunkMap[chkHdr.ID].SkipBytes(r);

                // TODO: Change this to detect node with ~0 (0xFFFFFFFF) parent ID
                // Assume first node read in Model[0] is root node.  This may be bad if they aren't in order!
                if (chkHdr.ChunkType == ChunkTypeEnum.Node && RootNode == null)
                    RootNode = ChunkMap[chkHdr.ID] as ChunkNode;

                // Add Bones to the model.  We are assuming there is only one CompiledBones chunk per file.
                if (chkHdr.ChunkType == ChunkTypeEnum.CompiledBones || chkHdr.ChunkType == ChunkTypeEnum.CompiledBonesSC)
                {
                    Bones = ChunkMap[chkHdr.ID] as ChunkCompiledBones;
                    SkinningInfo.HasSkinningInfo = true;
                }
            }
        }
    }
}
