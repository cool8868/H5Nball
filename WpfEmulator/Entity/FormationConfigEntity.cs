using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Games.NBall.WpfEmulator.Entity
{
    [Serializable]
    [XmlRoot(ElementName = "root")]
    [KnownType(typeof(FormationEntity))]
    [XmlInclude(typeof(FormationEntity))]
    [KnownType(typeof(FormationdetailEntity))]
    [XmlInclude(typeof(FormationdetailEntity))]
    public class FormationConfigEntity
    {
        [XmlArray, XmlArrayItem(ElementName = "formation")]
        public List<FormationEntity> FormationList { get; set; }
    }

    public class FormationEntity
    {
        [XmlAttribute(AttributeName = "idx")]
        public System.Int32 Idx { get; set; }

        [XmlAttribute(AttributeName = "code")]
        public System.String Formation { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public System.String Name { get; set; }

        [XmlAttribute(AttributeName = "buffperlevel")]
        public decimal BuffPerLevel { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public System.String Description { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "detail")]
        public List<FormationdetailEntity> DetailList { get; set; }
    }

    public class FormationdetailEntity
    {
        [XmlAttribute(AttributeName = "position")]
        public System.Int32 Position { get; set; }
        [XmlAttribute(AttributeName = "coordinate")]
        public System.String Coordinate { get; set; }
        [XmlAttribute(AttributeName = "specificpointdesc")]
        public System.String SpecificPointDesc { get; set; }
    }
}
