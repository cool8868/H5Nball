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
    public class WpfSummaryEntity
    {
        public WpfSummaryEntity()
        { }
        public WpfSummaryEntity(string name, string description)
        {
            Name = name;
            Description = description;
        }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }
    }
}
