﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class BODYRecord : Record, IHaveEDID, IHaveMODL
    {
        public struct BYDTField
        {
            public byte Part;
            public byte Vampire;
            public byte Flags;
            public byte PartType;

            public BYDTField(BinaryFileReader r, int dataSize)
            {
                Part = r.ReadByte();
                Vampire = r.ReadByte();
                Flags = r.ReadByte();
                PartType = r.ReadByte();
            }
        }

        public override string ToString() => $"BODY: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public MODLGroup MODL { get; set; } // NIF Model
        public STRVField FNAM; // Body name
        public BYDTField BYDT;

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            if (format == GameFormat.TES3)
                switch (type)
                {
                    case "NAME": EDID = r.ReadSTRV(dataSize); return true;
                    case "MODL": MODL = new MODLGroup(r, dataSize); return true;
                    case "FNAM": FNAM = r.ReadSTRV(dataSize); return true;
                    case "BYDT": BYDT = new BYDTField(r, dataSize); return true;
                    default: return false;
                }
            return false;
        }
    }
}
