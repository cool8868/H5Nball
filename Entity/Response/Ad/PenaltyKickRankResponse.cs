using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ad
{
    /// <summary>
    /// 获取点球射门信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class PenaltyKickRankResponse : BaseResponse<PenaltyKickRank>
    {
    }
    [Serializable]
    [DataContract]
    public class PenaltyKickRank
    {
        /// <summary>
        /// 排名列表
        /// </summary>
        [DataMember]
        public List<PenaltyKickRankEntity> RankList { get; set; }

        /// <summary>
        /// 我的排名信息
        /// </summary>
        [DataMember]
        public PenaltyKickRankEntity MyData { get; set; }
    }

    public class PenaltyKickRankEntity
    {
        /// <summary>
        /// 经理ID
        /// </summary>
        [DataMember]
        public Guid ManagerId { get; set; }

        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        public int TotalScore { get; set; }

        /// <summary>
        /// 排名
        /// </summary>
        [DataMember]
        public int Rank { get; set; }

        /// <summary>
        /// 积分变化时间
        /// </summary>
        public DateTime ScoreChangeTime { get; set; }
    }
}
