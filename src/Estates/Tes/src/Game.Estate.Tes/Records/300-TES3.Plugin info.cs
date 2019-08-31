using Game.Core;
using System.Collections.Generic;

namespace Game.Estate.Tes.Records
{
    public class TES3Record : Record
    {
        public struct HEDRField
        {
            public float Version;
            public uint FileType;
            public string CompanyName;
            public string FileDescription;
            public uint NumRecords;

            public HEDRField(BinaryFileReader r, int dataSize)
            {
                Version = r.ReadSingle();
                FileType = r.ReadUInt32();
                CompanyName = r.ReadASCIIString(32, ASCIIFormat.ZeroPadded);
                FileDescription = r.ReadASCIIString(256, ASCIIFormat.ZeroPadded);
                NumRecords = r.ReadUInt32();
            }
        }

        public HEDRField HEDR;
        public List<STRVField> MASTs;
        public List<INTVField> DATAs;

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "HEDR": HEDR = new HEDRField(r, dataSize); return true;
                case "MAST": if (MASTs == null) MASTs = new List<STRVField>(); MASTs.Add(r.ReadSTRV(dataSize)); return true;
                case "DATA": if (DATAs == null) DATAs = new List<INTVField>(); DATAs.Add(r.ReadINTV(dataSize)); return true;
                default: return false;
            }
        }
    }
}