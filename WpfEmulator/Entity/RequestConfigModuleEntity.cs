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
    [XmlRoot(ElementName = "requests")]
    [KnownType(typeof(RequestConfigModuleEntity))]
    [XmlInclude(typeof(RequestConfigModuleEntity))]
    [KnownType(typeof(RequestConfigActionEntity))]
    [XmlInclude(typeof(RequestConfigActionEntity))]
    [KnownType(typeof(RequestConfigParameterEntity))]
    [XmlInclude(typeof(RequestConfigParameterEntity))]
    public class RequestConfigEntity
    {
        [XmlArray, XmlArrayItem(ElementName = "module")]
        public List<RequestConfigModuleEntity> Modules { get; set; }
    }

    [Serializable]
    public class RequestConfigModuleEntity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// status:0,设计;1,开发中;2,完成
        /// </summary>
        [XmlAttribute(AttributeName = "status")]
        public int Status { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "action")]
        public List<RequestConfigActionEntity> Actions { get; set; }
    }

    [Serializable]
    public class RequestConfigActionEntity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "memo")]
        public string Memo { get; set; }

        /// <summary>
        /// status:0,设计;1,开发中;2,完成
        /// </summary>
        [XmlAttribute(AttributeName = "status")]
        public int Status { get; set; }

        [XmlAttribute(AttributeName = "response")]
        public string Response { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "parameter")]
        public List<RequestConfigParameterEntity> Parameters { get; set; }

        [XmlElement(ElementName = "eg")]
        public string Eg { get; set; }

        [XmlElement(ElementName = "egmemo")]
        public string EgMemo { get; set; }
    }

    [Serializable]
    public class RequestConfigParameterEntity
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }

        [XmlAttribute(AttributeName = "eg")]
        public string Eg { get; set; }
    }
}
