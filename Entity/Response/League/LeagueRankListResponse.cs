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
    public class LeagueRankListResponse : BaseResponse<LeagueRank>
    {
    }

    [Serializable]
    [DataContract]
    public class LeagueRank
    {
        /// <summary>
        /// 排名列表
        /// </summary>
        [DataMember]
        public List<LeagueRankRecord> RankList { get; set; }

        /// <summary>
        /// 我的排名
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }
        /// <summary>
        /// 我的积分
        /// </summary>
        [DataMember]
        public int MyScore { get; set; }

    }
}
