﻿using Game.Core;
using System.Collections.Generic;

namespace Game.Estate.Tes.Records
{
    public class LVLIRecord : Record
    {
        public struct LVLOField
        {
            public short Level;
            public FormId<Record> ItemFormId;
            public int Count;

            public LVLOField(BinaryFileReader r, int dataSize)
            {
                Level = r.ReadInt16();
                r.Skip(2); // Unused
                ItemFormId = new FormId<Record>(r.ReadUInt32());
                if (dataSize == 12)
                {
                    Count = r.ReadInt16();
                    r.Skip(2); // Unused
                }
                else Count = 0;
            }
        }

        public override string ToString() => $"LVLI: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public BYTEField LVLD; // Chance
        public BYTEField LVLF; // Flags - 0x01 = Calculate from all levels <= player's level, 0x02 = Calculate for each item in count
        public BYTEField? DATA; // Data (optional)
        public List<LVLOField> LVLOs = new List<LVLOField>();

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "LVLD": LVLD = r.ReadT<BYTEField>(dataSize); return true;
                case "LVLF": LVLF = r.ReadT<BYTEField>(dataSize); return true;
                case "DATA": DATA = r.ReadT<BYTEField>(dataSize); return true;
                case "LVLO": LVLOs.Add(new LVLOField(r, dataSize)); return true;
                default: return false;
            }
        }
    }
}