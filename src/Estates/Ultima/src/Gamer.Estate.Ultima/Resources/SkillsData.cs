using Gamer.Base.Core;
using Gamer.Estate.Ultima.Resources.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gamer.Estate.Ultima.Resources
{
    public class SkillsData
    {
        public static int DefaultLength = 55;
        public static AFileIndex FileIndex { get; } = FileManager.CreateFileIndex("Skills.idx", "Skills.mul", DefaultLength, -1);
        public static Skill[] List { get; } = new Skill[DefaultLength];
        static string[] _listNames;
        public static string[] ListNames
        {
            get
            {
                if (_listNames == null)
                {
                    _listNames = new string[List.Length];
                    for (var i = 0; i < List.Length; i++)
                        _listNames[i] = List[i].Name;
                }
                return _listNames;
            }
        }

        public static void Initialize()
        {
            for (var i = 0; i < DefaultLength; i++)
                GetSkill(i);
        }

        public static Skill GetSkill(int index)
        {
            if (List[index] != null)
                return List[index];
            var r = FileIndex.Seek(index, out int length, out int extra, out bool patched);
            if (r == null)
                return List[index] = new Skill(SkillVars.NullV);
            return List[index] = LoadSkill(index, r);
        }

        static unsafe Skill LoadSkill(int index, BinaryFileReader r)
        {
            var nameLength = FileIndex.Index[index].Length - 2;
            var extra = FileIndex.Index[index].Extra;
            var set1 = new byte[1];
            var set2 = new byte[nameLength];
            var set3 = new byte[1];
            set1 = r.ReadBytes(1);
            set2 = r.ReadBytes(nameLength);
            set3 = r.ReadBytes(1);
            var useBtn = ToBool(set1);
            var name = ToString(set2);
            return new Skill(new SkillVars(index, name, useBtn, extra, set3[0]));
        }

        public static string ToString(byte[] buffer)
        {
            var b = new StringBuilder(buffer.Length);
            for (var i = 0; i < buffer.Length; i++)
                b.Append(ToString(buffer[i]));
            return b.ToString();
        }

        public static bool ToBool(byte[] buffer) => BitConverter.ToBoolean(buffer, 0);

        public static string ToString(byte b) => ToString((char)b);

        public static string ToString(char c) => c.ToString();

        public static string ToHexidecimal(int input, int length) => string.Format("0x{0:X{1}}", input, length);
    }

    public class Skill
    {
        public SkillVars Data { get; private set; }
        public int Index { get; private set; } = -1;
        public bool UseButton { get; set; }
        public string Name { get; set; } = string.Empty;
        public SkillCategory Category { get; set; }
        public byte Unknown { get; private set; }

        public int Id => Index + 1;

        public Skill(SkillVars data)
        {
            Data = data;
            Index = Data.Index;
            UseButton = Data.UseButton;
            Name = Data.Name;
            Category = Data.Category;
            Unknown = Data.Unknown;
        }

        public void ResetFromData()
        {
            Index = Data.Index;
            UseButton = Data.UseButton;
            Name = Data.Name;
            Category = Data.Category;
            Unknown = Data.Unknown;
        }

        public void ResetFromData(SkillVars data)
        {
            Data = data;
            Index = Data.Index;
            UseButton = Data.UseButton;
            Name = Data.Name;
            Category = Data.Category;
            Unknown = Data.Unknown;
        }

        public override string ToString() => $"{Index} ({Index:X4}) {(UseButton ? "[x]" : "[ ]")} {Name}";
    }

    public class SkillVars
    {
        public static SkillVars NullV => new SkillVars(-1, "null", false, 0, 0x0);
        public int Index { get; } = -1;
        public string Name { get; } = string.Empty;
        public int Extra { get; }
        public bool UseButton { get; }
        public byte Unknown { get; }
        public SkillCategory Category { get; }

        public int NameLength => Name.Length;

        public SkillVars(int index, string name, bool useButton, int extra, byte unk)
        {
            Index = index;
            Name = name;
            UseButton = useButton;
            Extra = extra;
            Unknown = unk;
            Category = null;
        }
    }

    public class SkillCategories
    {
        public static SkillCategory[] List { get; private set; } = new SkillCategory[0];

        SkillCategories() { }

        public static SkillCategory GetCategory(int index)
        {
            if (List.Length > 0)
                if (index < List.Length)
                    return List[index];
            List = LoadCategories();
            return List.Length > 0 ? GetCategory(index) : new SkillCategory(SkillCategoryData.DefaultData);
        }

        static unsafe SkillCategory[] LoadCategories()
        {
            var list = new SkillCategory[0];
            var grpPath = FileManager.GetFilePath("skillgrp.mul");
            if (grpPath == null) return new SkillCategory[0];
            else
            {
                var toAdd = new List<SkillCategory>();
                using (var stream = new FileStream(grpPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var r = new BinaryReader(stream);
                    var start = new byte[4]; //File Start Offset
                    r.Read(start, 0, 4);
                    var index = 0;
                    long x = stream.Length, y = 0;
                    while (y < x) //Position < Length
                    {
                        var name = ParseName(stream);
                        var fileIndex = stream.Position - name.Length;
                        if (name.Length > 0)
                        {
                            toAdd.Add(new SkillCategory(new SkillCategoryData(fileIndex, index, name)));
                            y = stream.Position;
                            ++index;
                        }
                    }
                }
                if (toAdd.Count > 0)
                {
                    list = new SkillCategory[toAdd.Count];
                    for (var i = 0; i < toAdd.Count; i++)
                        list[i] = toAdd[i];
                    toAdd.Clear();
                }
            }
            return list;
        }

        static unsafe string ParseName(Stream stream)
        {
            var r = new BinaryReader(stream);
            var tempName = string.Empty;
            var esc = false;
            while (!esc && r.PeekChar() != -1)
            {
                var data = new byte[1];
                r.Read(data, 0, 1);
                var c = (char)data[0];
                if (char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c))
                {
                    tempName += SkillsData.ToString(c);
                    continue;
                }
                esc = true;
            }
            return tempName.Trim();
        }
    }

    public class SkillCategory
    {
        public SkillCategoryData Data { get; private set; }
        public int Index { get; private set; } = -1;
        public string Name { get; private set; } = string.Empty;

        public SkillCategory(SkillCategoryData data)
        {
            Data = data;
            Index = Data.Index;
            Name = Data.Name;
        }

        public void ResetFromData()
        {
            Index = Data.Index;
            Name = Data.Name;
        }

        public void ResetFromData(SkillCategoryData data)
        {
            Data = data;
            Index = Data.Index;
            Name = Data.Name;
        }
    }

    public class SkillCategoryData
    {
        public static SkillCategoryData DefaultData => new SkillCategoryData(0, -1, "null");
        public long FileIndex { get; } = -1;
        public int Index { get; } = -1;
        public string Name { get; } = string.Empty;

        public SkillCategoryData(long fileIndex, int index, string name)
        {
            FileIndex = fileIndex;
            Index = index;
            Name = name;
        }
    }
}