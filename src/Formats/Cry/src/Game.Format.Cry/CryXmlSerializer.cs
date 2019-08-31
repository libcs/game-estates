using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Game.Format.Cry
{
    public class CryXmlNode
    {
        public int NodeID { get; set; }
        public int NodeNameOffset { get; set; }
        public int ItemType { get; set; }
        public short AttributeCount { get; set; }
        public short ChildCount { get; set; }
        public int ParentNodeID { get; set; }
        public int FirstAttributeIndex { get; set; }
        public int FirstChildIndex { get; set; }
        public int Reserved { get; set; }
    }

    public class CryXmlReference
    {
        public int NameOffset { get; set; }
        public int ValueOffset { get; set; }
    }

    public class CryXmlValue
    {
        public int Offset { get; set; }
        public string Value { get; set; }
    }

    public static class CryXmlSerializer
    {
        public static XmlDocument ReadFile(string inFile, bool writeLog = false) => ReadStream(File.OpenRead(inFile), writeLog);
        public static XmlDocument ReadBytes(byte[] bytes, bool writeLog = false) => ReadStream(new MemoryStream(bytes), writeLog);

        public static XmlDocument ReadStream(Stream inStream, bool writeLog = false)
        {
            var startOffset = (int)inStream.Position;
            using (var r = new BinaryReader(inStream))
            {
                var peek = r.PeekChar();
                if (peek == '<') { var xml = new XmlDocument(); xml.Load(inStream); return xml; } // File is already XML, so return the XML.
                else if (peek != 'C') throw new Exception("Unknown File Format"); // Unknown file format

                var header = r.ReadCString();
                if (header != "CryXmlB")
                    throw new Exception("Unknown File Format");

                var headerLength = r.BaseStream.Position;
                var fileLength = r.ReadInt32();

                var nodeTableOffset = r.ReadInt32() + startOffset;
                var nodeTableCount = r.ReadInt32();
                var nodeTableSize = 28;

                var referenceTableOffset = r.ReadInt32() + startOffset;
                var referenceTableCount = r.ReadInt32();
                var referenceTableSize = 8;

                var offset3 = r.ReadInt32();
                var count3 = r.ReadInt32();
                var length3 = 4;

                var contentOffset = r.ReadInt32() + startOffset;
                var contentLength = r.ReadInt32();

                // NODE TABLE
                if (writeLog)
                {
                    // var byteFormatter = new Regex("([0-9A-F]{8})");
                    Console.WriteLine("Header");
                    Console.WriteLine("0x{0:X6}: {1}", 0x00, header);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x00, fileLength);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x04, nodeTableOffset);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x08, nodeTableCount);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x12, referenceTableOffset);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x16, referenceTableCount);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x20, offset3);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x24, count3);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x28, contentOffset);
                    Console.WriteLine("0x{0:X6}: {1:X8} (Dec: {1:D8})", headerLength + 0x32, contentLength);
                    Console.WriteLine("\nNode Table");
                }
                var nodeTable = new List<CryXmlNode> { };
                r.BaseStream.Seek(nodeTableOffset, SeekOrigin.Begin);
                var nodeID = 0;
                while (r.BaseStream.Position < nodeTableOffset + nodeTableCount * nodeTableSize)
                {
                    var position = r.BaseStream.Position;
                    var value = new CryXmlNode
                    {
                        NodeID = nodeID++,
                        NodeNameOffset = r.ReadInt32(),
                        ItemType = r.ReadInt32(),
                        AttributeCount = r.ReadInt16(),
                        ChildCount = r.ReadInt16(),
                        ParentNodeID = r.ReadInt32(),
                        FirstAttributeIndex = r.ReadInt32(),
                        FirstChildIndex = r.ReadInt32(),
                        Reserved = r.ReadInt32(),
                    };
                    nodeTable.Add(value);
                    if (writeLog)
                        Console.WriteLine(
                            "0x{0:X6}: {1:X8} {2:X8} {3:X4} {4:X4} {5:X8} {6:X8} {7:X8} {8:X8}",
                            position,
                            value.NodeNameOffset,
                            value.ItemType,
                            value.AttributeCount,
                            value.ChildCount,
                            value.ParentNodeID,
                            value.FirstAttributeIndex,
                            value.FirstChildIndex,
                            value.Reserved);
                }

                // REFERENCE TABLE
                if (writeLog) Console.WriteLine("\nReference Table");
                var attributeTable = new List<CryXmlReference> { };
                r.BaseStream.Seek(referenceTableOffset, SeekOrigin.Begin);
                while (r.BaseStream.Position < referenceTableOffset + referenceTableCount * referenceTableSize)
                {
                    var position = r.BaseStream.Position;
                    var value = new CryXmlReference
                    {
                        NameOffset = r.ReadInt32(),
                        ValueOffset = r.ReadInt32()
                    };
                    attributeTable.Add(value);
                    if (writeLog)
                        Console.WriteLine("0x{0:X6}: {1:X8} {2:X8}", position, value.NameOffset, value.ValueOffset);
                }

                // ORDER TABLE
                if (writeLog) Console.WriteLine("\nOrder Table");
                var table3 = new List<int> { };
                r.BaseStream.Seek(offset3, SeekOrigin.Begin);
                while (r.BaseStream.Position < offset3 + count3 * length3)
                {
                    var position = r.BaseStream.Position;
                    var value = r.ReadInt32();
                    table3.Add(value);
                    if (writeLog)
                        Console.WriteLine("0x{0:X6}: {1:X8}", position, value);
                }

                // DYNAMIC DICTIONARY
                if (writeLog) Console.WriteLine("\nDynamic Dictionary");
                var dataTable = new List<CryXmlValue> { };
                r.BaseStream.Seek(contentOffset, SeekOrigin.Begin);
                while (r.BaseStream.Position < r.BaseStream.Length)
                {
                    var position = r.BaseStream.Position;
                    var value = new CryXmlValue
                    {
                        Offset = (int)position - contentOffset,
                        Value = r.ReadCString(),
                    };
                    dataTable.Add(value);
                    if (writeLog)
                        Console.WriteLine("0x{0:X6}: {1:X8} {2}", position, value.Offset, value.Value);
                }

                var dataMap = dataTable.ToDictionary(k => k.Offset, v => v.Value);
                var attributeIndex = 0;
                var xmlDoc = new XmlDocument();
                var bugged = false;

                // DOCUMENT
                var xmlMap = new Dictionary<int, XmlElement> { };
                foreach (var node in nodeTable)
                {
                    var element = xmlDoc.CreateElement(dataMap[node.NodeNameOffset]);
                    for (int i = 0, j = node.AttributeCount; i < j; i++)
                    {
                        if (dataMap.ContainsKey(attributeTable[attributeIndex].ValueOffset))
                            element.SetAttribute(dataMap[attributeTable[attributeIndex].NameOffset], dataMap[attributeTable[attributeIndex].ValueOffset]);
                        else
                        {
                            bugged = true;
                            element.SetAttribute(dataMap[attributeTable[attributeIndex].NameOffset], "BUGGED");
                        }
                        attributeIndex++;
                    }
                    xmlMap[node.NodeID] = element;
                    if (xmlMap.ContainsKey(node.ParentNodeID))
                        xmlMap[node.ParentNodeID].AppendChild(element);
                    else
                        xmlDoc.AppendChild(element);
                }
                return xmlDoc;
            }
        }

        public static TObject Deserialize<TObject>(Stream inStream) where TObject : class
        {
            using (var ms = new MemoryStream())
            {
                var xs = new XmlSerializer(typeof(TObject));
                var xmlDoc = ReadStream(inStream);
                xmlDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return xs.Deserialize(ms) as TObject;
            }
        }

        public static TObject Deserialize<TObject>(string inFile) where TObject : class
        {
            using (var ms = new MemoryStream())
            {
                var xmlDoc = ReadFile(inFile);
                xmlDoc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var xs = new XmlSerializer(typeof(TObject));
                return xs.Deserialize(ms) as TObject;
            }
        }
    }
}
