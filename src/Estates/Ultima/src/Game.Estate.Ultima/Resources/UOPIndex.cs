﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Game.Estate.Ultima.Resources
{
    public class UOPIndex
    {
        readonly UOPEntry[] _entries;
        readonly int _length;
        readonly BinaryReader _reader;

        class OffsetComparer : IComparer<UOPEntry>
        {
            public static readonly IComparer<UOPEntry> Instance = new OffsetComparer();

            public int Compare(UOPEntry x, UOPEntry y) => x.Offset.CompareTo(y.Offset);
        }

        class UOPEntry : IComparable<UOPEntry>
        {
            public readonly int Length;
            public int Offset;
            public int Order;

            public UOPEntry(int offset, int length)
            {
                Offset = offset;
                Length = length;
                Order = 0;
            }

            public int CompareTo(UOPEntry other) => Order.CompareTo(other.Order);
        }

        public UOPIndex(FileStream stream)
        {
            _reader = new BinaryReader(stream);
            _length = (int)stream.Length;
            if (_reader.ReadInt32() != 0x50594D)
                throw new ArgumentException("Invalid UOP file.");
            Version = _reader.ReadInt32();
            _reader.ReadInt32();
            var nextTable = _reader.ReadInt32();
            var entries = new List<UOPEntry>();
            do
            {
                stream.Seek(nextTable, SeekOrigin.Begin);
                var count = _reader.ReadInt32();
                nextTable = _reader.ReadInt32();
                _reader.ReadInt32();
                for (var i = 0; i < count; ++i)
                {
                    var offset = _reader.ReadInt32();
                    if (offset == 0)
                    {
                        stream.Seek(30, SeekOrigin.Current);
                        continue;
                    }
                    _reader.ReadInt64();
                    var length = _reader.ReadInt32();
                    entries.Add(new UOPEntry(offset, length));
                    stream.Seek(18, SeekOrigin.Current);
                }
            } while (nextTable != 0 && nextTable < _length);
            entries.Sort(OffsetComparer.Instance);
            for (var i = 0; i < entries.Count; ++i)
            {
                stream.Seek(entries[i].Offset + 2, SeekOrigin.Begin);
                var dataOffset = _reader.ReadInt16();
                entries[i].Offset += 4 + dataOffset;
                stream.Seek(dataOffset, SeekOrigin.Current);
                entries[i].Order = _reader.ReadInt32();
            }
            entries.Sort();
            _entries = entries.ToArray();
        }

        public int Version { get; }

        public int Lookup(int offset)
        {
            var total = 0;
            for (var i = 0; i < _entries.Length; ++i)
            {
                var newTotal = total + _entries[i].Length;
                if (offset < newTotal)
                    return _entries[i].Offset + (offset - total);
                total = newTotal;
            }
            return _length;
        }

        public void Close() => _reader.Close();
    }
}
