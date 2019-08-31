﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class SLGMRecord : Record, IHaveEDID, IHaveMODL
    {
        public struct DATAField
        {
            public int Value;
            public float Weight;

            public DATAField(BinaryFileReader r, int dataSize)
            {
                Value = r.ReadInt32();
                Weight = r.ReadSingle();
            }
        }

        public override string ToString() => $"SLGM: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public MODLGroup MODL { get; set; } // Model
        public STRVField FULL; // Item Name
        public FMIDField<SCPTRecord> SCRI; // Script (optional)
        public DATAField DATA; // Type of soul contained in the gem
        public FILEField ICON; // Icon (optional)
        public BYTEField SOUL; // Type of soul contained in the gem
        public BYTEField SLCP; // Soul gem maximum capacity

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "MODL": MODL = new MODLGroup(r, dataSize); return true;
                case "MODB": MODL.MODBField(r, dataSize); return true;
                case "MODT": MODL.MODTField(r, dataSize); return true;
                case "FULL": FULL = r.ReadSTRV(dataSize); return true;
                case "SCRI": SCRI = new FMIDField<SCPTRecord>(r, dataSize); return true;
                case "DATA": DATA = new DATAField(r, dataSize); return true;
                case "ICON": ICON = r.ReadFILE(dataSize); return true;
                case "SOUL": SOUL = r.ReadT<BYTEField>(dataSize); return true;
                case "SLCP": SLCP = r.ReadT<BYTEField>(dataSize); return true;
                default: return false;
            }
        }
    }
}