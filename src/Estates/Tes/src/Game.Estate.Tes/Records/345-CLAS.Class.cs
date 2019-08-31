﻿using Game.Core;

namespace Game.Estate.Tes.Records
{
    public class CLASRecord : Record
    {
        public struct DATAField
        {
            //wbArrayS('Primary Attributes', wbInteger('Primary Attribute', itS32, wbActorValueEnum), 2),
            //wbInteger('Specialization', itU32, wbSpecializationEnum),
            //wbArrayS('Major Skills', wbInteger('Major Skill', itS32, wbActorValueEnum), 7),
            //wbInteger('Flags', itU32, wbFlags(['Playable', 'Guard'])),
            //wbInteger('Buys/Sells and Services', itU32, wbServiceFlags),
            //wbInteger('Teaches', itS8, wbSkillEnum),
            //wbInteger('Maximum training level', itU8),
            //wbInteger('Unused', itU16)

            public DATAField(BinaryFileReader r, int dataSize) => r.Skip(dataSize);
        }

        public override string ToString() => $"CLAS: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public STRVField FULL; // Name
        public STRVField DESC; // Description
        public STRVField? ICON; // Icon (Optional)
        public DATAField DATA; // Data

        public override bool CreateField(BinaryFileReader r, GameFormat format, string type, int dataSize)
        {
            if (format == GameFormat.TES3)
                switch (type)
                {
                    case "NAME": EDID = r.ReadSTRV(dataSize); return true;
                    case "FNAM": FULL = r.ReadSTRV(dataSize); return true;
                    case "CLDT": r.Skip(dataSize); return true;
                    case "DESC": DESC = r.ReadSTRV(dataSize); return true;
                    default: return false;
                }
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "FULL": FULL = r.ReadSTRV(dataSize); return true;
                case "DESC": DESC = r.ReadSTRV(dataSize); return true;
                case "ICON": ICON = r.ReadSTRV(dataSize); return true;
                case "DATA": DATA = new DATAField(r, dataSize); return true;
                default: return false;
            }
        }
    }
}
