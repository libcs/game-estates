using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Gamer.Format.Cry.Core
{
    /// <summary>
    /// Representation of a CryEngine .mtl file
    /// </summary>
    [XmlRoot(ElementName = "Material")]
    public class Material
    {
        /// <summary>
        /// Color used in XML serialization/deserialization
        /// </summary>
        public class Color
        {
            public double Red;
            public double Green;
            public double Blue;

            public static Color Deserialize(string value)
            {
                if (string.IsNullOrWhiteSpace(value))
                    return null;
                var parts = value.Split(',');
                if (parts.Length != 3)
                    return null;
                var r = new Color();
                if (!double.TryParse(parts[0], out r.Red)) return null;
                if (!double.TryParse(parts[1], out r.Green)) return null;
                if (!double.TryParse(parts[2], out r.Blue)) return null;
                return r;
            }
            public static string Serialize(Color input) => input == null ? null : $"{input.Red},{input.Green},{input.Blue}";
        }

        /// <summary>
        /// The texture object
        /// </summary>
        [XmlRoot(ElementName = "Texture")]
        public class Texture
        {
            public enum TypeEnum {[XmlEnum("0")] Default = 0, [XmlEnum("3")] Environment = 3, [XmlEnum("5")] Interface = 5, [XmlEnum("7")] CubeMap = 7 }
            public enum MapTypeEnum { Unknown = 0, Diffuse, Bumpmap, Specular, Environment, Decal, SubSurface, Custom, Opacity, Detail, Heightmap, BlendDetail }
            [XmlAttribute(AttributeName = "Map")] public string __Map { get => Enum.GetName(typeof(MapTypeEnum), Map); set { var r = MapTypeEnum.Unknown; Enum.TryParse(value, out r); Map = r; } }
            [XmlIgnore] public MapTypeEnum Map { get; set; }
            [XmlAttribute(AttributeName = "File")] public string File { get; set; }
            [XmlAttribute(AttributeName = "TexType"), DefaultValue(TypeEnum.Default)] public TypeEnum TexType;
            [XmlElement(ElementName = "TexMod")] public TextureModifier Modifier;
        }

        /// <summary>
        /// The texture modifier
        /// </summary>
        [XmlRoot(ElementName = "TexMod")]
        public class TextureModifier
        {
            [XmlAttribute(AttributeName = "TexMod_RotateType")] public int RotateType { get; set; }
            [XmlAttribute(AttributeName = "TexMod_TexGenType")] public int GenType { get; set; }
            [XmlAttribute(AttributeName = "TexMod_bTexGenProjected"), DefaultValue(1)] public int __Projected { get => Projected ? 1 : 0; set => Projected = value == 1; }
            [XmlIgnore] public bool Projected { get; set; }
            [XmlAttribute(AttributeName = "TileU"), DefaultValue(0)] public double TileU { get; set; }
            [XmlAttribute(AttributeName = "TileV"), DefaultValue(0)] public double TileV { get; set; }
            [XmlAttribute(AttributeName = "OffsetU"), DefaultValue(0)] public double OffsetU { get; set; }
        }

        /// <summary>
        /// After the textures General things that apply to the material
        /// Not really needed
        /// </summary>
        [XmlRoot(ElementName = "PublicParams")]
        public class PublicParameters
        {
            [XmlAttribute(AttributeName = "FresnelPower")] public string FresnelPower { get; set; }
            [XmlAttribute(AttributeName = "GlossFromDiffuseContrast")] public string GlossFromDiffuseContrast { get; set; }
            [XmlAttribute(AttributeName = "FresnelScale")] public string FresnelScale { get; set; }
            [XmlAttribute(AttributeName = "GlossFromDiffuseOffset")] public string GlossFromDiffuseOffset { get; set; }
            [XmlAttribute(AttributeName = "FresnelBias")] public string FresnelBias { get; set; }
            [XmlAttribute(AttributeName = "GlossFromDiffuseAmount")] public string GlossFromDiffuseAmount { get; set; }
            [XmlAttribute(AttributeName = "GlossFromDiffuseBrightness")] public string GlossFromDiffuseBrightness { get; set; }
            [XmlAttribute(AttributeName = "IndirectColor")] public string IndirectColor { get; set; }
            [XmlAttribute(AttributeName = "SpecMapChannelB")] public string SpecMapChannelB { get; set; }
            [XmlAttribute(AttributeName = "SpecMapChannelR")] public string SpecMapChannelR { get; set; }
            [XmlAttribute(AttributeName = "GlossMapChannelB")] public string GlossMapChannelB { get; set; }
            [XmlAttribute(AttributeName = "SpecMapChannelG")] public string SpecMapChannelG { get; set; }
            [XmlAttribute(AttributeName = "DirtTint")] public string DirtTint { get; set; }
            [XmlAttribute(AttributeName = "DirtGlossFactor")] public string DirtGlossFactor { get; set; }
            [XmlAttribute(AttributeName = "DirtTiling")] public string DirtTiling { get; set; }
            [XmlAttribute(AttributeName = "DirtStrength")] public string DirtStrength { get; set; }
            [XmlAttribute(AttributeName = "DirtMapAlphaInfluence")] public string DirtMapAlphaInfluence { get; set; }
            [XmlAttribute(AttributeName = "DetailBumpTillingU")] public string DetailBumpTillingU { get; set; }
            [XmlAttribute(AttributeName = "DetailDiffuseScale")] public string DetailDiffuseScale { get; set; }
            [XmlAttribute(AttributeName = "DetailBumpScale")] public string DetailBumpScale { get; set; }
            [XmlAttribute(AttributeName = "DetailGlossScale")] public string DetailGlossScale { get; set; }
        }

        [XmlAttribute(AttributeName = "Name"), DefaultValue("")] public string Name { get; set; }
        [XmlAttribute(AttributeName = "MtlFlags")] public int Flags { get; set; }
        [XmlAttribute(AttributeName = "MatTemplate")] public string Template { get; set; }
        [XmlAttribute(AttributeName = "MatSubTemplate")] public string SubTemplate { get; set; }
        [XmlAttribute(AttributeName = "vertModifType")] public short VertModifierType { get; set; }
        [XmlAttribute(AttributeName = "Shader"), DefaultValue("")] public string Shader { get; set; }
        [XmlAttribute(AttributeName = "GenMask"), DefaultValue("")] public string GenMask { get; set; }
        [XmlAttribute(AttributeName = "StringGenMask"), DefaultValue("")] public string StringGenMask { get; set; }
        [XmlAttribute(AttributeName = "SurfaceType"), DefaultValue(null)] public string SurfaceType { get; set; }
        [XmlAttribute(AttributeName = "Diffuse"), DefaultValue("")] public string __Diffuse { get => Color.Serialize(Diffuse); set => Diffuse = Color.Deserialize(value); }
        [XmlIgnore] public Color Diffuse { get; set; }
        [XmlAttribute(AttributeName = "Specular"), DefaultValue("")] public string __Specular { get => Color.Serialize(Specular); set => Specular = Color.Deserialize(value); }
        [XmlIgnore] public Color Specular { get; set; }
        [XmlAttribute(AttributeName = "Emissive"), DefaultValue("")] public string __Emissive { get => Color.Serialize(Emissive); set => Emissive = Color.Deserialize(value); }
        [XmlIgnore] public Color Emissive { get; set; }
        /// <summary>
        /// Value between 0 and 1 that controls opacity
        /// </summary>
        [XmlAttribute(AttributeName = "Opacity"), DefaultValue(1)] public double Opacity { get; set; }
        [XmlAttribute(AttributeName = "CloakAmount"), DefaultValue(1)] public double Cloak { get; set; }
        [XmlAttribute(AttributeName = "Shininess"), DefaultValue(0)] public double Shininess { get; set; }
        [XmlAttribute(AttributeName = "Glossiness"), DefaultValue(0)] public double Glossiness { get; set; }
        [XmlAttribute(AttributeName = "GlowAmount"), DefaultValue(0)] public double GlowAmount { get; set; }
        [XmlAttribute(AttributeName = "AlphaTest"), DefaultValue(0)] public double AlphaTest { get; set; }
        [XmlArray(ElementName = "SubMaterials"), XmlArrayItem(ElementName = "Material")] public Material[] SubMaterials { get; set; }
        [XmlElement(ElementName = "PublicParams")] public PublicParameters PublicParams { get; set; }
        // TODO: TimeOfDay Support
        [XmlArray(ElementName = "Textures"), XmlArrayItem(ElementName = "Texture")] public Texture[] Textures { get; set; }

        public static Material FromFile(FileInfo materialfile)
        {
            if (!materialfile.Exists)
                return null;
            try
            {
                using (var fileStream = materialfile.OpenRead())
                    return CryXmlSerializer.Deserialize<Material>(fileStream);
                //return CryXmlSerializer.Deserialize<Material>(materialfile.FullName);
            }
            catch (Exception ex) { Debug.WriteLine($"{materialfile} failed deserialize - {ex.Message}"); }
            return null;
        }
    }
}
