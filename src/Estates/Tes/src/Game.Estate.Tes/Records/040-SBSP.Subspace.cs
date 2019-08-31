﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class SBSPRecord : Record
    {
        public struct DNAMField
        {
            public float X; // X dimension
            public float Y; // Y dimension
            public float Z; // Z dimension

            public DNAMField(BinaryFileReader r, int dataSize)
            {
                X = r.ReadSingle();
                Y = r.ReadSingle();
                Z = r.ReadSingle();
            }
        }

        public override string ToString() => $"SBSP: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public DNAMField DNAM;

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "DNAM": DNAM = new DNAMField(r, dataSize); return true;
                default: return false;
            }
        }
    }
}