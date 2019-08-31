﻿using Game.Core;
using System.Collections.Generic;

namespace Game.Estate.Tes.Records
{
    public class IDLERecord : Record, IHaveEDID, IHaveMODL
    {
        public override string ToString() => $"IDLE: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public MODLGroup MODL { get; set; }
        public List<SCPTRecord.CTDAField> CTDAs = new List<SCPTRecord.CTDAField>(); // Conditions
        public BYTEField ANAM;
        public FMIDField<IDLERecord>[] DATAs;

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "MODL": MODL = new MODLGroup(r, dataSize); return true;
                case "MODB": MODL.MODBField(r, dataSize); return true;
                case "CTDA":
                case "CTDT": CTDAs.Add(new SCPTRecord.CTDAField(r, dataSize, format)); return true;
                case "ANAM": ANAM = r.ReadT<BYTEField>(dataSize); return true;
                case "DATA":
                    DATAs = new FMIDField<IDLERecord>[dataSize >> 2];
                    for (var i = 0; i < DATAs.Length; i++) DATAs[i] = new FMIDField<IDLERecord>(r, 4); return true;
                default: return false;
            }
        }
    }
}