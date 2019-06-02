using Gamer.Core;
using Gamer.Core.Records;
using Gamer.Estate.Tes.Records;
using Gamer.Proxy;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gamer.Core.Debug;

// TES3
//http://en.uesp.net/wiki/Tes3Mod:File_Format
//https://github.com/TES5Edit/TES5Edit/blob/dev/wbDefinitionsTES3.pas
//http://en.uesp.net/morrow/tech/mw_esm.txt
//https://github.com/mlox/mlox/blob/master/util/tes3cmd/tes3cmd
// TES4
//https://github.com/WrinklyNinja/esplugin/tree/master/src
//http://en.uesp.net/wiki/Tes4Mod:Mod_File_Format
//https://github.com/TES5Edit/TES5Edit/blob/dev/wbDefinitionsTES4.pas 
// TES5
//http://en.uesp.net/wiki/Tes5Mod:Mod_File_Format
//https://github.com/TES5Edit/TES5Edit/blob/dev/wbDefinitionsTES5.pas 

namespace Gamer.Estate.Tes.FilePack
{
    public class Header
    {
        [Flags]
        public enum HeaderFlags : uint
        {
            EsmFile = 0x00000001,               // ESM file. (TES4.HEDR record only.)
            Deleted = 0x00000020,               // Deleted
            R00 = 0x00000040,                   // Constant / (REFR) Hidden From Local Map (Needs Confirmation: Related to shields)
            R01 = 0x00000100,                   // Must Update Anims / (REFR) Inaccessible
            R02 = 0x00000200,                   // (REFR) Hidden from local map / (ACHR) Starts dead / (REFR) MotionBlurCastsShadows
            R03 = 0x00000400,                   // Quest item / Persistent reference / (LSCR) Displays in Main Menu
            InitiallyDisabled = 0x00000800,     // Initially disabled
            Ignored = 0x00001000,               // Ignored
            VisibleWhenDistant = 0x00008000,    // Visible when distant
            R04 = 0x00010000,                   // (ACTI) Random Animation Start
            R05 = 0x00020000,                   // (ACTI) Dangerous / Off limits (Interior cell) Dangerous Can't be set withough Ignore Object Interaction
            Compressed = 0x00040000,            // Data is compressed
            CantWait = 0x00080000,              // Can't wait
                                                // tes5
            R06 = 0x00100000,                   // (ACTI) Ignore Object Interaction Ignore Object Interaction Sets Dangerous Automatically
            IsMarker = 0x00800000,              // Is Marker
            R07 = 0x02000000,                   // (ACTI) Obstacle / (REFR) No AI Acquire
            NavMesh01 = 0x04000000,             // NavMesh Gen - Filter
            NavMesh02 = 0x08000000,             // NavMesh Gen - Bounding Box
            R08 = 0x10000000,                   // (FURN) Must Exit to Talk / (REFR) Reflected By Auto Water
            R09 = 0x20000000,                   // (FURN/IDLM) Child Can Use / (REFR) Don't Havok Settle
            R10 = 0x40000000,                   // NavMesh Gen - Ground / (REFR) NoRespawn
            R11 = 0x80000000,                   // (REFR) MultiBound
        }

        public enum HeaderGroupType : int
        {
            Top = 0,                    // Label: Record type
            WorldChildren,              // Label: Parent (WRLD)
            InteriorCellBlock,          // Label: Block number
            InteriorCellSubBlock,       // Label: Sub-block number
            ExteriorCellBlock,          // Label: Grid Y, X (Note the reverse order)
            ExteriorCellSubBlock,       // Label: Grid Y, X (Note the reverse order)
            CellChildren,               // Label: Parent (CELL)
            TopicChildren,              // Label: Parent (DIAL)
            CellPersistentChilden,      // Label: Parent (CELL)
            CellTemporaryChildren,      // Label: Parent (CELL)
            CellVisibleDistantChildren, // Label: Parent (CELL)
        }

        public override string ToString() => $"{Type}:{GroupType}";
        public Header Parent;
        public string Type; // 4 bytes
        public uint DataSize;
        public HeaderFlags Flags;
        public bool Compressed => (Flags & HeaderFlags.Compressed) != 0;
        public uint FormId;
        public long Position;
        // group
        public string Label;
        public HeaderGroupType GroupType;

        public Header() { }
        public Header(BinaryFileReader r, GameFormat format, Header parent)
        {
            Parent = parent;
            Type = r.ReadASCIIString(4);
            if (Type == "GRUP")
            {
                DataSize = (uint)(r.ReadUInt32() - (format == GameFormat.TES4 ? 20 : 24));
                Label = RecordGroup.ToLabel(parent == null, r.ReadBytes(4));
                GroupType = (HeaderGroupType)r.ReadInt32();
                r.ReadUInt32(); // stamp | stamp + uknown
                if (format != GameFormat.TES4)
                    r.ReadUInt32(); // version + uknown
                Position = r.Position;
                return;
            }
            DataSize = r.ReadUInt32();
            if (format == GameFormat.TES3)
                r.ReadUInt32(); // Unknown
            Flags = (HeaderFlags)r.ReadUInt32();
            if (format == GameFormat.TES3)
            {
                Position = r.Position;
                return;
            }
            // tes4
            FormId = r.ReadUInt32();
            r.ReadUInt32();
            if (format == GameFormat.TES4)
            {
                Position = r.Position;
                return;
            }
            // tes5
            r.ReadUInt32();
            Position = r.Position;
        }

        public void Write(BinaryFileWriter w, GameFormat format)
        {
            w.WriteASCIIString(Type, 4);
            if (Type == "GRUP")
            {
                w.Write(DataSize + (format == GameFormat.TES4 ? 20 : 24));
                w.Write(RecordGroup.FromLabel(Parent == null, Label), 0, 4);
                w.Write((int)GroupType);
                w.Write(0U); // stamp | stamp + uknown
                if (format != GameFormat.TES4)
                    w.Write(0U); // version + uknown
                return;
            }
            throw new NotImplementedException();
        }

        struct RecordType
        {
            public Func<Record> F;
            public Func<int, bool> L;
        }

        const bool CreaturesEnabled = true;
        const bool NpcsEnabled = true;
        static Dictionary<string, RecordType> CreateMap = new Dictionary<string, RecordType>
        {
            { "TES3", new RecordType { F = ()=>new TES3Record(), L = x => true }},
            { "TES4", new RecordType { F = ()=>new TES4Record(), L = x => true }},
            // 0
            { "LTEX", new RecordType { F = ()=>new LTEXRecord(), L = x => x > 0 }},
            { "STAT", new RecordType { F = ()=>new STATRecord(), L = x => x > 0 }},
            { "CELL", new RecordType { F = ()=>new CELLRecord(), L = x => x > 0 }},
            { "LAND", new RecordType { F = ()=>new LANDRecord(), L = x => x > 0 }},
            // 1
            { "DOOR", new RecordType { F = ()=>new DOORRecord(), L = x => x > 1 }},
            { "MISC", new RecordType { F = ()=>new MISCRecord(), L = x => x > 1 }},
            { "WEAP", new RecordType { F = ()=>new WEAPRecord(), L = x => x > 1 }},
            { "CONT", new RecordType { F = ()=>new CONTRecord(), L = x => x > 1 }},
            { "LIGH", new RecordType { F = ()=>new LIGHRecord(), L = x => x > 1 }},
            { "ARMO", new RecordType { F = ()=>new ARMORecord(), L = x => x > 1 }},
            { "CLOT", new RecordType { F = ()=>new CLOTRecord(), L = x => x > 1 }},
            { "REPA", new RecordType { F = ()=>new REPARecord(), L = x => x > 1 }},
            { "ACTI", new RecordType { F = ()=>new ACTIRecord(), L = x => x > 1 }},
            { "APPA", new RecordType { F = ()=>new APPARecord(), L = x => x > 1 }},
            { "LOCK", new RecordType { F = ()=>new LOCKRecord(), L = x => x > 1 }},
            { "PROB", new RecordType { F = ()=>new PROBRecord(), L = x => x > 1 }},
            { "INGR", new RecordType { F = ()=>new INGRRecord(), L = x => x > 1 }},
            { "BOOK", new RecordType { F = ()=>new BOOKRecord(), L = x => x > 1 }},
            { "ALCH", new RecordType { F = ()=>new ALCHRecord(), L = x => x > 1 }},
            { "CREA", new RecordType { F = ()=>new CREARecord(), L = x => x > 1 && CreaturesEnabled }},
            { "NPC_", new RecordType { F = ()=>new NPC_Record(), L = x => x > 1 && NpcsEnabled }},
            // 2
            { "GMST", new RecordType { F = ()=>new GMSTRecord(), L = x => x > 2 }},
            { "GLOB", new RecordType { F = ()=>new GLOBRecord(), L = x => x > 2 }},
            { "SOUN", new RecordType { F = ()=>new SOUNRecord(), L = x => x > 2 }},
            { "REGN", new RecordType { F = ()=>new REGNRecord(), L = x => x > 2 }},
            // 3
            { "CLAS", new RecordType { F = ()=>new CLASRecord(), L = x => x > 3 }},
            { "SPEL", new RecordType { F = ()=>new SPELRecord(), L = x => x > 3 }},
            { "BODY", new RecordType { F = ()=>new BODYRecord(), L = x => x > 3 }},
            { "PGRD", new RecordType { F = ()=>new PGRDRecord(), L = x => x > 3 }},
            { "INFO", new RecordType { F = ()=>new INFORecord(), L = x => x > 3 }},
            { "DIAL", new RecordType { F = ()=>new DIALRecord(), L = x => x > 3 }},
            { "SNDG", new RecordType { F = ()=>new SNDGRecord(), L = x => x > 3 }},
            { "ENCH", new RecordType { F = ()=>new ENCHRecord(), L = x => x > 3 }},
            { "SCPT", new RecordType { F = ()=>new SCPTRecord(), L = x => x > 3 }},
            { "SKIL", new RecordType { F = ()=>new SKILRecord(), L = x => x > 3 }},
            { "RACE", new RecordType { F = ()=>new RACERecord(), L = x => x > 3 }},
            { "MGEF", new RecordType { F = ()=>new MGEFRecord(), L = x => x > 3 }},
            { "LEVI", new RecordType { F = ()=>new LEVIRecord(), L = x => x > 3 }},
            { "LEVC", new RecordType { F = ()=>new LEVCRecord(), L = x => x > 3 }},
            { "BSGN", new RecordType { F = ()=>new BSGNRecord(), L = x => x > 3 }},
            { "FACT", new RecordType { F = ()=>new FACTRecord(), L = x => x > 3 }},
            { "SSCR", new RecordType { F = ()=>new SSCRRecord(), L = x => x > 3 }},
            // 4 - Oblivion
            { "WRLD", new RecordType { F = ()=>new WRLDRecord(), L = x => x > 0 }},
            { "ACRE", new RecordType { F = ()=>new ACRERecord(), L = x => x > 1 }},
            { "ACHR", new RecordType { F = ()=>new ACHRRecord(), L = x => x > 1 }},
            { "REFR", new RecordType { F = ()=>new REFRRecord(), L = x => x > 1 }},
            //
            { "AMMO", new RecordType { F = ()=>new AMMORecord(), L = x => x > 4 }},
            { "ANIO", new RecordType { F = ()=>new ANIORecord(), L = x => x > 4 }},
            { "CLMT", new RecordType { F = ()=>new CLMTRecord(), L = x => x > 4 }},
            { "CSTY", new RecordType { F = ()=>new CSTYRecord(), L = x => x > 4 }},
            { "EFSH", new RecordType { F = ()=>new EFSHRecord(), L = x => x > 4 }},
            { "EYES", new RecordType { F = ()=>new EYESRecord(), L = x => x > 4 }},
            { "FLOR", new RecordType { F = ()=>new FLORRecord(), L = x => x > 4 }},
            { "FURN", new RecordType { F = ()=>new FURNRecord(), L = x => x > 4 }},
            { "GRAS", new RecordType { F = ()=>new GRASRecord(), L = x => x > 4 }},
            { "HAIR", new RecordType { F = ()=>new HAIRRecord(), L = x => x > 4 }},
            { "IDLE", new RecordType { F = ()=>new IDLERecord(), L = x => x > 4 }},
            { "KEYM", new RecordType { F = ()=>new KEYMRecord(), L = x => x > 4 }},
            { "LSCR", new RecordType { F = ()=>new LSCRRecord(), L = x => x > 4 }},
            { "LVLC", new RecordType { F = ()=>new LVLCRecord(), L = x => x > 4 }},
            { "LVLI", new RecordType { F = ()=>new LVLIRecord(), L = x => x > 4 }},
            { "LVSP", new RecordType { F = ()=>new LVSPRecord(), L = x => x > 4 }},
            { "PACK", new RecordType { F = ()=>new PACKRecord(), L = x => x > 4 }},
            { "QUST", new RecordType { F = ()=>new QUSTRecord(), L = x => x > 4 }},
            { "ROAD", new RecordType { F = ()=>new ROADRecord(), L = x => x > 4 }},
            { "SBSP", new RecordType { F = ()=>new SBSPRecord(), L = x => x > 4 }},
            { "SGST", new RecordType { F = ()=>new SGSTRecord(), L = x => x > 4 }},
            { "SLGM", new RecordType { F = ()=>new SLGMRecord(), L = x => x > 4 }},
            { "TREE", new RecordType { F = ()=>new TREERecord(), L = x => x > 4 }},
            { "WATR", new RecordType { F = ()=>new WATRRecord(), L = x => x > 4 }},
            { "WTHR", new RecordType { F = ()=>new WTHRRecord(), L = x => x > 4 }},
            // 5 - Skyrim
            { "AACT", new RecordType { F = ()=>new AACTRecord(), L = x => x > 5 }},
            { "ADDN", new RecordType { F = ()=>new ADDNRecord(), L = x => x > 5 }},
            { "ARMA", new RecordType { F = ()=>new ARMARecord(), L = x => x > 5 }},
            { "ARTO", new RecordType { F = ()=>new ARTORecord(), L = x => x > 5 }},
            { "ASPC", new RecordType { F = ()=>new ASPCRecord(), L = x => x > 5 }},
            { "ASTP", new RecordType { F = ()=>new ASTPRecord(), L = x => x > 5 }},
            { "AVIF", new RecordType { F = ()=>new AVIFRecord(), L = x => x > 5 }},
            { "DLBR", new RecordType { F = ()=>new DLBRRecord(), L = x => x > 5 }},
            { "DLVW", new RecordType { F = ()=>new DLVWRecord(), L = x => x > 5 }},
            { "SNDR", new RecordType { F = ()=>new SNDRRecord(), L = x => x > 5 }},
        };

        public Record CreateRecord(long position, int recordLevel)
        {
            if (!CreateMap.TryGetValue(Type, out var recordType))
            {
                Log($"Unsupported ESM record type: {Type}");
                return null;
            }
            if (!recordType.L(recordLevel))
                return null;
            var record = recordType.F();
            record.Header = this;
            return record;
        }
    }

    public partial class RecordGroup
    {
        public RecordGroup Next;
        public string Label => Headers.First.Value.Label;
        public override string ToString() => Headers.First.Value.ToString();
        public LinkedList<Header> Headers = new LinkedList<Header>();
        public List<Record> Records = new List<Record>();
        public Dictionary<string, RecordGroup> GroupByLabel;
        protected readonly ProxySink _proxySink;
        protected BinaryFileReader _r;
        public readonly string FilePath;
        public readonly GameFormat Format;
        public readonly int RecordLevel;
        int _headerSkip;

        public RecordGroup(ProxySink proxySink, BinaryFileReader r, string filePath, GameFormat format, int recordLevel)
        {
            _proxySink = proxySink;
            _r = r;
            FilePath = filePath;
            Format = format;
            RecordLevel = recordLevel;
        }

        public void Close()
        {
            _r?.Close();
            _r = null;
        }

        public void AddHeader(Header header, ProxySink.DataInfo info)
        {
            //Log($"Read: {header.Label}");
            Headers.AddLast(header);
            if (header.GroupType == Header.HeaderGroupType.Top)
                switch (header.Label)
                {
                    case "CELL":
                    //case "DIAL":
                    case "WRLD": Load(info: info); break;
                }
        }

        public Task<byte[]> LoadDataLabelAsync(string label) => _proxySink.LoadDataLabelAsync(label, () =>
        {
            return Task.FromResult(new byte[] { 1, 2, 3 });
        });

        public List<Record> Load(bool loadAll = false, ProxySink.DataInfo info = null)
        {
            if (_r == null)
                _r = new BinaryFileReader(new MemoryStream(LoadDataLabelAsync(FilePath).Result()));
            if (_headerSkip == Headers.Count) return Records;
            lock (_r ?? throw new InvalidOperationException("Should not reach here"))
            {
                if (_headerSkip == Headers.Count) return Records;
                foreach (var header in Headers.Skip(_headerSkip))
                    ReadGroup(header, loadAll, info);
                _headerSkip = Headers.Count;
                return Records;
            }
        }

        static int _cellsLoaded = 0;
        void ReadGroup(Header parentHeader, bool loadAll, ProxySink.DataInfo info)
        {
            //if (_r == null)
            //    _r = new BinaryFileReader(_i)
            _r.Position = parentHeader.Position;
            var endPosition = parentHeader.Position + parentHeader.DataSize;
            while (_r.Position < endPosition)
            {
                var header = new Header(_r, Format, parentHeader);
                if (header.Type == "GRUP")
                {
                    info?.EnterGroup();
                    ReadGRUP(parentHeader, header, loadAll, info);
                    info?.LeaveGroup();
                    continue;
                }
                //if (info != null)
                //{
                //    var bytes = _r.ReadBytes((int)recordHeader.DataSize);
                //    info.Data(bytes);
                //}
                // HACK to limit cells loading
                if (header.Type == "CELL" && _cellsLoaded > int.MaxValue)
                {
                    _r.Position += header.DataSize;
                    continue;
                }
                var record = header.CreateRecord(_r.Position, RecordLevel);
                if (record == null)
                {
                    _r.Position += header.DataSize;
                    continue;
                }
                ReadRecord(record, header.Compressed);
                Records.Add(record);
                if (header.Type == "CELL") _cellsLoaded++;
            }
        }

        protected RecordGroup ReadGRUP(Header parentHeader, Header header, bool loadAll, ProxySink.DataInfo info)
        {
            var nextPosition = _r.Position + header.DataSize;
            var label = header.Label;
            var headerData = new byte[0];
            info?.AddGroup(label, header.Position, headerData);
            if (GroupByLabel == null)
                GroupByLabel = new Dictionary<string, RecordGroup>();
            if (!GroupByLabel.TryGetValue(label, out var group))
                GroupByLabel.Add(label, group = new RecordGroup(_proxySink, _r, FilePath, Format, RecordLevel));
            else group = new RecordGroup(_proxySink, _r, FilePath, Format, RecordLevel) { Next = group };
            group.AddHeader(header, info);
            _r.Position = nextPosition;
            // print header path
            //Log($"Grup: {string.Join("/", GetHeaderPath(new List<string>(), parentHeader).ToArray())} {parentHeader.GroupType}");
            if (loadAll || info?.Level <= int.MaxValue)
                group.Load(loadAll, info);
            return group;
        }

        protected RecordGroup ReadGRUP(ProxySink.DataInfo info)
        {
            var stack = new Stack<RecordGroup>();
            RecordGroup group = this;
            var groupByLabel = group.GroupByLabel = new Dictionary<string, RecordGroup>();
            var r = new BinaryFileReader(new MemoryStream());
            info.Decoder(
                group: (label, position, headerData) =>
                {
                    if (!groupByLabel.TryGetValue(label, out group))
                        groupByLabel.Add(label, group = new RecordGroup(_proxySink, null, FilePath, Format, RecordLevel));
                    else group = new RecordGroup(_proxySink, null, FilePath, Format, 0) { Next = group };
                    r.Position = 0;
                    r.BaseStream.Write(headerData, 0, headerData.Length);
                    group.AddHeader(new Header(r, Format, null), null);
                },
                enterGroup: () => { stack.Push(group); groupByLabel = group.GroupByLabel = new Dictionary<string, RecordGroup>(); },
                leaveGroup: () => { group = stack.Pop(); groupByLabel = group.GroupByLabel; }
            );
            return group;
        }

        void ReadRecord(Record record, bool compressed)
        {
            //Log($"Recd: {record.Header.Type}");
            if (!compressed)
            {
                record.Read(_r, FilePath, Format);
                return;
            }
            var newDataSize = _r.ReadUInt32();
            var data = _r.ReadBytes((int)record.Header.DataSize - 4);
            var newData = new byte[newDataSize];
            using (var s = new MemoryStream(data))
            using (var gs = new InflaterInputStream(s))
                gs.Read(newData, 0, newData.Length);
            // read record
            record.Header.Position = 0;
            record.Header.DataSize = newDataSize;
            using (var s = new MemoryStream(newData))
            using (var r = new BinaryFileReader(s))
                record.Read(r, FilePath, Format);
        }

        internal static string ToLabel(bool top, byte[] label) => top ? Encoding.ASCII.GetString(label) : Utils.ToB64String(label);
        internal static string ToLabel(bool top, uint label) => ToLabel(top, BitConverter.GetBytes(label));
        internal static byte[] FromLabel(bool top, string label) => top ? Encoding.ASCII.GetBytes(label) : Utils.FromB64String(label);

        internal static GameFormat ToFormat(TesGame game)
        {
            switch (game)
            {
                // tes
                case TesGame.Morrowind: return GameFormat.TES3;
                case TesGame.Oblivion: return GameFormat.TES4;
                case TesGame.Skyrim:
                case TesGame.SkyrimSE:
                case TesGame.SkyrimVR: return GameFormat.TES5;
                // fallout
                case TesGame.Fallout3:
                case TesGame.FalloutNV: return GameFormat.TES4;
                case TesGame.Fallout4:
                case TesGame.Fallout4VR: return GameFormat.TES5;
                default: throw new InvalidOperationException();
            }
        }
        //static List<string> GetHeaderPath(List<string> b, Header header)
        //{
        //    if (header.Parent != null) GetHeaderPath(b, header.Parent);
        //    b.Add(header.Label);
        //    return b;
        //}
    }

    public class FieldHeader
    {
        public override string ToString() => Type;
        public string Type; // 4 bytes
        public int DataSize;

        public FieldHeader(BinaryFileReader r, GameFormat format)
        {
            Type = r.ReadASCIIString(4);
            DataSize = (int)(format == GameFormat.TES3 ? r.ReadUInt32() : r.ReadUInt16());
        }
    }
}
