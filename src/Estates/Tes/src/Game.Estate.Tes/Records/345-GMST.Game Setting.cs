﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class GMSTRecord : Record, IHaveEDID
    {
        public override string ToString() => $"GMST: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public DATVField DATA; // Data

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            if (format == GameFormat.TES3)
                switch (type)
                {
                    case "NAME": EDID = r.ReadSTRV(dataSize); return true;
                    case "STRV": DATA = r.ReadDATV(dataSize, 's'); return true;
                    case "INTV": DATA = r.ReadDATV(dataSize, 'i'); return true;
                    case "FLTV": DATA = r.ReadDATV(dataSize, 'f'); return true;
                    default: return false;
                }
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "DATA": DATA = r.ReadDATV(dataSize, EDID.Value[0]); return true;
                default: return false;
            }
        }
    }
}