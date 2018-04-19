using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 跨服活动详情输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class CrossActivityPrizeResponse : BaseResponse<CrossActivityPrize>
    {
    }

    [Serializable]
    [DataContract]
    public class CrossActivityPrize
    {
        /// <summary>
        /// 剩余金条
        /// </summary>
        [DataMember]
        public int GoldBar { get; set; }

        /// <summary>
        /// 最大可抽奖次数
        /// </summary>
        [DataMember]
        public int MaxNumber { get; set; }

        /// <summary>
        /// 用掉了的次数
        /// </summary>
        [DataMember]
        public int UsedNumber { get; set; }

        /// <summary>
        /// 奖励ID
        /// </summary>
        [DataMember]
        public int PrizeId { get; set; }

        /// <summary>
        /// 物品code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Number { get; set; }
    }

}
