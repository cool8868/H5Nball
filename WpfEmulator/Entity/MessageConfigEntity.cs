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
    [XmlRoot(ElementName = "MessageDic")]
    [KnownType(typeof(MessageEnumEntity))]
    [XmlInclude(typeof(MessageEnumEntity))]
    public class MessageConfigEntity
    {

        public MessageConfigEntity()
        {
            Summary = new List<WpfSummaryEntity>();
            Summary.Add(new WpfSummaryEntity("EnumItemType", "物品类型枚举"));
            Summary.Add(new WpfSummaryEntity("EnumPlayerCardLevel", "球员卡颜色枚举"));
            Summary.Add(new WpfSummaryEntity("EnumEquipmentQuarity", "装备品质枚举"));
            Summary.Add(new WpfSummaryEntity("EnumBallsoulColor", "球魂颜色枚举"));
            Summary.Add(new WpfSummaryEntity("EnumPosition", "球员位置枚举"));
            Summary.Add(new WpfSummaryEntity("EnumProperty", "球员属性枚举"));
            Summary.Add(new WpfSummaryEntity("EnumTrainState", "球员训练状态枚举"));
            Summary.Add(new WpfSummaryEntity("EnumMallType", "商城道具类型枚举"));
            Summary.Add(new WpfSummaryEntity("EnumMallExtraType", "商城道具扩展类型枚举"));
            Summary.Add(new WpfSummaryEntity("EnumMallQuality", "商城道具品质枚举"));
            Summary.Add(new WpfSummaryEntity("EnumAttachmentType", "附件类型枚举"));
            Summary.Add(new WpfSummaryEntity("EnumPandoraResultType", "潘多拉处理结果枚举"));
            Summary.Add(new WpfSummaryEntity("EnumWinType", "比赛结果枚举"));
            Summary.Add(new WpfSummaryEntity("MessageCode", "通用消息枚举"));

        }

        [XmlArray, XmlArrayItem(ElementName = "summary")]
        public List<WpfSummaryEntity> Summary { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumItemType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumPlayerCardLevel { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumEquipmentQuarity { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumBallsoulColor { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumPosition { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumProperty { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumTrainState { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumMallType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumMallExtraType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumMallQuality { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumAttachmentType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumCurrencyType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumPandoraResultType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> EnumWinType { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "message")]
        public List<MessageEnumEntity> MessageCode { get; set; }


    }
    [Serializable]
    public class MessageEnumEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        [XmlAttribute(AttributeName = "Code")]
        public int Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [XmlAttribute(AttributeName = "Description")]
        public string Description { get; set; }
    }
}
