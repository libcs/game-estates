﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class GLOBRecord : Record, IHaveEDID
    {
        public override string ToString() => $"GLOB: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public BYTEField? FNAM; // Type of global (s, l, f)
        public FLTVField? FLTV; // Float data

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID":
                case "NAME": EDID = r.ReadSTRV(dataSize); return true;
                case "FNAM": FNAM = r.ReadT<BYTEField>(dataSize); return true;
                case "FLTV": FLTV = r.ReadT<FLTVField>(dataSize); return true;
                default: return false;
            }
        }
    }
}