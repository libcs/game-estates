using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Diagnostics.Debug;

namespace Gamer.Asset.Nif.Format
{
    public class NifParserGenerator
    {
        public const uint MORROWIND_NIF_VERSION = 0x04000002;
        const int SPACES_PER_INDENT = 4;
        uint _nifVersion;
        XDocument _nifXmlDoc;
        StringBuilder _b;
        int _indentLevel;

        public void GenerateParser(string nifXmlFilePath, uint nifVersion, string generatedParserFilePath)
        {
            _nifXmlDoc = XDocument.Load(nifXmlFilePath);
            _nifVersion = nifVersion;
            _b = new StringBuilder();
            _indentLevel = 0;
            GenerateEnums();
            File.WriteAllBytes(generatedParserFilePath, Encoding.UTF8.GetBytes(_b.ToString()));
        }

        string GetConvertedOptionName(XElement enumElement, XElement optionElement)
        {
            var optionNamePrefix = enumElement.Attribute("prefix")?.Value;
            optionNamePrefix = optionNamePrefix != null ? optionNamePrefix + '_' : string.Empty;
            var optionNameTail = optionElement.Attribute("name").Value.Replace(' ', '_').ToUpper();
            return optionNamePrefix + optionNameTail;
        }

        string ConvertTypeName(string typeName)
        {
            switch (typeName)
            {
                case "uint": return "uint";
                case "ushort": return "ushort";
                case "byte": return "byte";
                default: throw new NotSupportedException($"Unsupported type: \"{typeName}\".");
            }
        }

        string CleanDescription(string description) => description == null ? description : Regex.Replace(description.Trim(), "\r?\n", "");

        uint VersionStringToInt(string versionString)
        {
            Assert(versionString != null);
            var versionNumberStrings = versionString.Split('.');
            Assert(versionNumberStrings.Length >= 1 && versionNumberStrings.Length <= 4);
            var versionInt = 0U;
            for (var i = 0; i < versionNumberStrings.Length; i++)
                versionInt |= uint.Parse(versionNumberStrings[i]) << (32 - (8 * (i + 1)));
            return versionInt;
        }

        bool IsElementInVersion(XElement element)
        {
            var minVersionStr = element.Attribute("ver1")?.Value;
            var minVersion = minVersionStr != null ? VersionStringToInt(minVersionStr) : 0;
            var maxVersionStr = element.Attribute("ver2")?.Value;
            var maxVersion = maxVersionStr != null ? VersionStringToInt(maxVersionStr) : uint.MaxValue;
            return _nifVersion >= minVersion && _nifVersion <= maxVersion;
        }

        void Generate(char c) => _b.Append(c);
        void Generate(string s) => _b.Append(s);

        void GenerateLine(string line, int deltaIndentLevel = 0)
        {
            _b.Append(line);
            EndLine(deltaIndentLevel);
        }

        void EndLine(int deltaIndentLevel = 0)
        {
            _indentLevel += deltaIndentLevel;
            _b.AppendLine();
            _b.Append(' ', SPACES_PER_INDENT * _indentLevel);
        }

        void GenerateEnums()
        {
            foreach (var enumElement in _nifXmlDoc.Descendants("enum").Where(IsElementInVersion))
            {
                var enumName = enumElement.Attribute("name").Value;
                var enumElementType = ConvertTypeName(enumElement.Attribute("storage").Value);
                var enumDescription = CleanDescription(enumElement.Nodes().OfType<XText>().FirstOrDefault()?.Value);
                if (!string.IsNullOrWhiteSpace(enumDescription))
                    GenerateLine($"// {enumDescription}");
                GenerateLine($"public enum {enumName} : {enumElementType}");
                GenerateLine("{", 1);
                GenerateEnumValues(enumElement);
                GenerateLine("}");
                GenerateLine("");
            }
        }

        void GenerateEnumValues(XElement enumElement)
        {
            var optionElements = enumElement.Descendants("option").Where(IsElementInVersion).ToArray();
            var lastOptionElementIndex = optionElements.Length - 1;
            for (var i = 0; i < optionElements.Length; i++)
            {
                var optionElement = optionElements[i];
                var optionName = GetConvertedOptionName(enumElement, optionElement);
                var optionValue = optionElement.Attribute("value").Value;
                var optionDescription = CleanDescription(optionElement.Nodes().OfType<XText>().FirstOrDefault()?.Value);
                Generate($"{optionName} = {optionValue}");
                if (i < lastOptionElementIndex) Generate(',');
                if (!string.IsNullOrWhiteSpace(optionDescription)) Generate($" // {optionDescription}");
                EndLine(i < lastOptionElementIndex ? 0 : -1);
            }
        }
    }
}