using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取对阵记录
    /// </summary>
    [DataContract] 
    [Serializable]
    [ProtoContract]
    public class TurntableFrameEntity
    {
        /// <summary>
        /// 转盘详情
        /// </summary>
        [ProtoMember(1)]
        [DataMember]
        public Dictionary<int, TurntableList> TurntableInfo { get; set; }
    }

    /// <summary>
    /// 转盘集合
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class TurntableList
    {
        /// <summary>
        /// 集合
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<TurntableItem> ItemList { get; set; }

        /// <summary>
        /// 是否是首次
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public bool IsFirst { get; set; }
    }

    /// <summary>
    /// 转盘项
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class TurntableItem
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int Idx { get; set; }
        /// <summary>
        /// 概率
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int Rate { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public bool IsEffective { get; set; }

        /// <summary>
        /// 是否是转盘
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public bool IsTurntable { get; set; }

        /// <summary>
        /// 特殊物品
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public int SpecialItem { get; set; }
    }
}
