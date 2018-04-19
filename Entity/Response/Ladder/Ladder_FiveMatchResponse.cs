using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ladder
{
    [DataContract]
    [Serializable]
    public class LadderMatchEntityListResponse : BaseResponse<LadderMatchEntityList>
    {
    }

    [DataContract]
    [Serializable]
    public class LadderMatchEntityList
    {
        [DataMember]
        public List<LadderMatchEntityView> Matchs { get; set; }
    }

    [DataContract]
    [Serializable]
    public class LadderMatchEntityView
    {
        /// <summary>
        /// 比赛id
        /// </summary>
        [DataMember]
        public Guid MatchId { get; set; }
        /// <summary>
        /// 客队名
        /// </summary>
        [DataMember]
        public string AwayName { get; set; }
        /// <summary>
        /// WinType枚举 0：负; 1：胜;2：平
        /// </summary>
        [DataMember]
        public int WinType { get; set; }
        /// <summary>
        /// 比分显示，1:0
        /// </summary>
        [DataMember]
        public string ScoreView { get; set; }
        /// <summary>
        /// 获得天梯积分
        /// </summary>
        [DataMember]
        public int PrizeScore { get; set; }
    }
}
