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
    public class CrossActivityInfoResponse : BaseResponse<CrossActivityInfo>
    {
    }

    [Serializable]
    [DataContract]
    public class CrossActivityInfo
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
        /// 奖励列表
        /// </summary>
        [DataMember]
        public List<ConfigCrossactivityprizeEntity> PrizeList { get; set; }

        /// <summary>
        /// 是否提示活动
        /// </summary>
        [DataMember]
        public bool IsPrompt { get; set; }

        /// <summary>
        /// 活动开始时间
        /// </summary>
        [DataMember]
        public long ActivityStart { get; set; }

        /// <summary>
        /// 活动结束时间
        /// </summary>
        [DataMember]
        public long ActivityEnd { get; set; }

        /// <summary>
        /// 活动是否开启
        /// </summary>
        [DataMember]
        public bool IsOpen { get; set; }
    }

}
