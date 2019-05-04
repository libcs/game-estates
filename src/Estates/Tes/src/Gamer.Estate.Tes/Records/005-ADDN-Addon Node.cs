using Gamer.Base.Core;

namespace Gamer.Estate.Tes.Records
{
    public class ADDNRecord : Record
    {
        public override string ToString() => $"ADDN: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public CREFField CNAME; // RGB color

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "CNAME": CNAME = r.ReadT<CREFField>(dataSize); return true;
                default: return false;
            }
        }
    }
}