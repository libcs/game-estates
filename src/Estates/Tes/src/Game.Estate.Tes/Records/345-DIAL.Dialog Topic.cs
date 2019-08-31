﻿using Game.Core;
using System.Collections.Generic;

namespace Game.Estate.Tes.Records
{
    public class DIALRecord : Record
    {
        internal static DIALRecord LastRecord;

        public enum DIALType : byte { RegularTopic = 0, Voice, Greeting, Persuasion, Journal }

        public override string ToString() => $"DIAL: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public STRVField FULL; // Dialogue Name
        public BYTEField DATA; // Dialogue Type
        public List<FMIDField<QUSTRecord>> QSTIs; // Quests (optional)
        public List<INFORecord> INFOs = new List<INFORecord>(); // Info Records

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID":
                case "NAME": EDID = r.ReadSTRV(dataSize); LastRecord = this; return true;
                case "FULL": FULL = r.ReadSTRV(dataSize); return true;
                case "DATA": DATA = r.ReadT<BYTEField>(dataSize); return true;
                case "QSTI":
                case "QSTR": if (QSTIs == null) QSTIs = new List<FMIDField<QUSTRecord>>(); QSTIs.Add(new FMIDField<QUSTRecord>(r, dataSize)); return true;
                default: return false;
            }
        }
    }
}
