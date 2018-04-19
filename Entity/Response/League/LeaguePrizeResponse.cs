using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取联赛奖励
    /// </summary>
    [Serializable]
    [DataContract]
    public class LeaguePrizeResponse : BaseResponse<LeaguePrizes>
    {
    }

    [Serializable]
    [DataContract]
    public class LeaguePrizes
    {
        /// <summary>
        /// 比赛奖励
        /// </summary>
        [DataMember]
        public List<LeaguePrizeEntity> PrizeList { get; set; }

        /// <summary>
        /// VIP经验
        /// </summary>
        [DataMember]
        public int VipExp { get; set; }
        
    }


    [Serializable]
    [DataContract]
    public class LeaguePrizeEntity
    {
        /// <summary>
        /// 奖励类型(1经验 2金币 3积分 4道具 5钻石)
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 物品id
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }

    }

    /// <summary>
    /// 获取所有联赛奖励情况
    /// </summary>
    [Serializable]
    [DataContract]
    public class LeagueAllPrizeInfoResponse : BaseResponse<LeagueAllPrizeInfoList>
    {
        
    }


    [Serializable]
    [DataContract]
    public class LeagueAllPrizeInfoList
    {
        /// <summary>
        /// 奖励列表
        /// </summary>
        [DataMember]
        public List<LeagueAllPrizeInfoEntity> PrizeInfoList { get; set; }
    }


    [Serializable]
    [DataContract]
    public class LeagueAllPrizeInfoEntity
    {
        /// <summary>
        /// 联赛类型
        /// </summary>
        [DataMember]
        public int LeagueId { get; set; }

        /// <summary>
        /// 未领取联赛记录id
        /// </summary>
        [DataMember]
        public Guid LeagueRecordId { get; set; }

        /// <summary>
        /// 初次通过
        /// </summary>
        [DataMember]
        public bool FirstPass { get; set; }

        /// <summary>
        /// 领取状态 0初始 1可领取 2已领取
        /// </summary>
        [DataMember]
        public int Status { get; set; }


    }

}
