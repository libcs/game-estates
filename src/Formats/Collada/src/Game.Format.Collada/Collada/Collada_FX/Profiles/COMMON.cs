using System;
using System.Xml;
using System.Xml.Serialization;

namespace grendgine_collada
{
    [Serializable, XmlType(AnonymousType = true), XmlRoot(ElementName = "technique", Namespace = "http://www.collada.org/2005/11/COLLADASchema", IsNullable = true)]
    public partial class Grendgine_Collada_Effect_Technique_COMMON : Grendgine_Collada_Effect_Technique
    {
        [XmlElement(ElementName = "blinn")] public Grendgine_Collada_Blinn Blinn;
        [XmlElement(ElementName = "constant")] public Grendgine_Collada_Constant Constant;
        [XmlElement(ElementName = "lambert")] public Grendgine_Collada_Lambert Lambert;
        [XmlElement(ElementName = "phong")] public Grendgine_Collada_Phong Phong;
    }

    [Serializable, XmlType(AnonymousType = true), XmlRoot(ElementName = "profile_COMMON", Namespace = "http://www.collada.org/2005/11/COLLADASchema", IsNullable = true)]
    public partial class Grendgine_Collada_Profile_COMMON : Grendgine_Collada_Profile
    {
        [XmlElement(ElementName = "newparam")] public Grendgine_Collada_New_Param[] New_Param;
        [XmlElement(ElementName = "technique")] public Grendgine_Collada_Effect_Technique_COMMON Technique;
    }
}

