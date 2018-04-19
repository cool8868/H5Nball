using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ad
{
    /// <summary>
    /// 获取点球信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class GetPenaltyKickInfoResponse : BaseResponse<GetPenaltyKickInfo>
    {
    }
    [Serializable]
    [DataContract]
    public class GetPenaltyKickInfo
    {
        /// <summary>
        /// 剩余免费次数
        /// </summary>
        [DataMember]
        public int FreeNumber { get; set; }

        /// <summary>
        /// 剩余游戏币数量
        /// </summary>
        [DataMember]
        public int GameCurrency { get; set; }

        /// <summary>
        /// 踢球球员射门属性
        /// </summary>
        [DataMember]
        public int ShooterAttribute { get; set; }

        /// <summary>
        /// 踢球记录
        /// </summary>
        [DataMember]
        public string ShootLog { get; set; }

        /// <summary>
        /// 连续进球次数
        /// </summary>
        [DataMember]
        public int CombGoals { get; set; }

        /// <summary>
        /// 本局最大连续进球次数
        /// </summary>
        [DataMember]
        public int MaxCombGoals { get; set; }

        /// <summary>
        /// 游戏状态 0=初始 1=游戏已经开始 2=游戏结束
        /// </summary>
        [DataMember]
        public int Status { get; set; }

        /// <summary>
        /// 总积分
        /// </summary>
        [DataMember]
        public int TotalScore { get; set; }

        /// <summary>
        /// 可用于兑换积分
        /// </summary>
        [DataMember]
        public int AvailableScore { get; set; }

        /// <summary>
        /// 可兑换的物品
        /// </summary>
        [DataMember]
        public List<PenaltyKickExChangeEntity> ExChangeList { get; set; }
    }

    /// <summary>
    /// 点球奖励输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class PrizeEntity
    {
        /// <summary>
        /// 奖励类型 1=金币 2=点卷 3=物品 5积分
        /// </summary>
        [DataMember]
        public int ItemType { get; set; }

        /// <summary>
        /// 奖励code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 奖励数量
        /// </summary>
        [DataMember]
        public int ItemCount { get; set; }
    }
}
