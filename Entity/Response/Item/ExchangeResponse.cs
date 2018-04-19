using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using ProtoBuf;

namespace Games.NBall.Entity.Response.Item
{
    [Serializable]
    [DataContract]
    public class ExchangeResponse : BaseResponse<ExchangeEntity>
    {

    }

    [Serializable]
    [DataContract]
    public class ExchangeEntity
    {
        /// <summary>
        /// 礼包类型：1，媒体礼包；2，新手礼包
        /// </summary>
        [DataMember]
        [ProtoMember(0)]
        public int ExchangeType { get; set; }
        /// <summary>
        /// 附件列表
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<ExchangePrizeEntity> PrizeList { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ExchangePrizeEntity
    {
        ///<summary>
        ///奖励类型，1:钻石：  2：金币；3：物品
        ///</summary>
        [DataMember]
        [ProtoMember(3)]
        public System.Int32 Type { get; set; }

        ///<summary>
        ///ItemCode
        ///</summary>
        [DataMember]
        [ProtoMember(4)]
        public System.Int32 ItemCode { get; set; }

        ///<summary>
        ///Strength
        ///</summary>
        [DataMember]
        [ProtoMember(5)]
        public System.Int32 Strength { get; set; }

        ///<summary>
        ///Count
        ///</summary>
        [DataMember]
        [ProtoMember(6)]
        public System.Int32 Count { get; set; }

        ///<summary>
        ///IsBinding
        ///</summary>
        [DataMember]
        [ProtoMember(7)]
        public System.Boolean IsBinding { get; set; }
    }
}
