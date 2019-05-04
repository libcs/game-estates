using System;
using System.Collections.Generic;
using System.IO;
using Gamer.Base.Core;
using UnityEngine;

namespace Gamer.Estate.Ultima.Resources
{
    public class ContainerData
    {
        static Dictionary<int, ContainerData> _table;

        static ContainerData()
        {
            _table = new Dictionary<int, ContainerData>();
            var path = @"data/containers.cfg";
            if (!File.Exists(path))
            {
                Default = new ContainerData(0x3C, new RectInt(44, 65, 142, 94), 0x48);
                return;
            }
            using (var r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Length == 0 || line.StartsWith("#"))
                        continue;
                    try
                    {
                        var split = line.Split('\t');
                        if (split.Length >= 3)
                        {
                            var gumpId = split[0].ToInt32();
                            var aRect = split[1].Split(' ');
                            if (aRect.Length < 4)
                                continue;
                            var x = aRect[0].ToInt32();
                            var y = aRect[1].ToInt32();
                            var width = aRect[2].ToInt32();
                            var height = aRect[3].ToInt32();
                            var bounds = new RectInt(x, y, width, height);
                            var dropSound = split[2].ToInt32();
                            var data = new ContainerData(gumpId, bounds, dropSound);
                            if (Default == null)
                                Default = data;
                            if (split.Length >= 4)
                            {
                                var aIds = split[3].Split(',');
                                for (var i = 0; i < aIds.Length; i++)
                                {
                                    var id = aIds[i].ToInt32();
                                    if (_table.ContainsKey(id)) Console.WriteLine(@"Warning: double ItemId entry in Data\containers.cfg");
                                    else _table[id] = data;
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            if (Default == null)
                Default = new ContainerData(0x3C, new RectInt(44, 65, 142, 94), 0x48);
        }

        public static ContainerData Default { get; set; }

        public static ContainerData Get(int itemId)
        {
            _table.TryGetValue(itemId, out var data);
            return data ?? Default;
        }

        public int GumpId { get; }
        public RectInt Bounds { get; }
        public int DropSound { get; }

        public ContainerData(int gumpId, RectInt bounds, int dropSound)
        {
            GumpId = gumpId;
            Bounds = bounds;
            DropSound = dropSound;
        }
    }
}
