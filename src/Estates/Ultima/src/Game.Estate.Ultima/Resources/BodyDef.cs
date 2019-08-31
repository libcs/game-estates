﻿using Game.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Estate.Ultima.Resources
{
    public static class BodyDef
    {
        static Dictionary<int, BodyTableEntry> _entries;

        static BodyDef()
        {
            _entries = new Dictionary<int, BodyTableEntry>();
            var filePath = UltimaFileManager.GetFilePath("body.def");
            if (filePath == null)
                return;
            var def = new StreamReader(filePath);
            string line;
            var totalDataRead = 0;
            while ((line = def.ReadLine()) != null)
            {
                totalDataRead += line.Length;
                if ((line = line.Trim()).Length == 0 || line.StartsWith("#"))
                    continue;
                try
                {
                    var index1 = line.IndexOf("{");
                    var index2 = line.IndexOf("}");
                    var origBody = line.Substring(0, index1);
                    var newBody = line.Substring(index1 + 1, index2 - index1 - 1);
                    var newHue = line.Substring(index2 + 1);
                    var indexOf = newBody.IndexOf(',');
                    if (indexOf > -1)
                        newBody = newBody.Substring(0, indexOf).Trim();
                    var iParam1 = Convert.ToInt32(origBody);
                    var iParam2 = Convert.ToInt32(newBody);
                    var iParam3 = Convert.ToInt32(newHue);
                    _entries[iParam1] = new BodyTableEntry(iParam1, iParam2, iParam3);
                }
                catch { }
            }
            Metrics.ReportDataRead(totalDataRead);
        }

        public static void TranslateBodyAndHue(ref int body, ref int hue)
        {
            if (_entries.TryGetValue(body, out var bte))
            {
                body = bte.NewBody;
                if (hue == 0)
                    hue = bte.NewHue;
            }
        }

        class BodyTableEntry
        {
            public readonly int OriginalBody;
            public readonly int NewBody;
            public readonly int NewHue;

            public BodyTableEntry(int oldId, int newId, int newHue)
            {
                OriginalBody = oldId;
                NewBody = newId;
                NewHue = newHue;
            }

            public override string ToString() => $"{OriginalBody} {NewBody} {NewHue}";
        }
    }
}
