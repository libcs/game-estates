﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class SSCRRecord : Record
    {
        public override string ToString() => $"SSCR: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public STRVField DATA; // Digits

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            if (format == GameFormat.TES3)
                switch (type)
                {
                    case "NAME": EDID = r.ReadSTRV(dataSize); return true;
                    case "DATA": DATA = r.ReadSTRV(dataSize); return true;
                    default: return false;
                }
            return false;
        }
    }
}
