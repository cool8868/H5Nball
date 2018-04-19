using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 获取联赛信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class LeagueFightResultResponse : BaseResponse<LeagueFightResult>
    {
    }

    [Serializable]
    [DataContract]
    public class LeagueFightResult
    {
        /// <summary>
        /// 比赛结果（0胜 1平 2负）
        /// </summary>
        [DataMember]
        public int Result { get; set; }

        /// <summary>
        /// 主队进球数 
        /// </summary>
        [DataMember]
        public int HomeGoals { get; set; }

        /// <summary>
        /// 客队进球数
        /// </summary>
        [DataMember]
        public int AwayGoals { get; set; }

        /// <summary>
        /// 当前体力
        /// </summary>
        [DataMember]
        public int Stamina { get; set; }

        /// <summary>
        /// 比赛奖励
        /// </summary>
        [DataMember]
        public List<LeaguePrizeEntity> PrizeList { get; set; }

        /// <summary>
        /// 比赛ID
        /// </summary>
        [DataMember]
        public Guid MatchId { get; set; }

        /// <summary>
        /// 这场得到了多少星星
        /// </summary>
        [DataMember]
        public int StarNumber { get; set; }

        /// <summary>
        /// VIP经验
        /// </summary>
        [DataMember]
        public int VipExp { get; set; }

    }
}
