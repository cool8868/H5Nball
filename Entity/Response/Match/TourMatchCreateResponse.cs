using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Match
{
    [Serializable]
    [DataContract]
    public class TourMatchCreateResponse : BaseResponse<TourMatchCreateEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class TourMatchCreateEntity
    {
        /// <summary>
        /// 有未抽卡的比赛
        /// </summary>
        [DataMember]
        public bool HasLottery { get; set; }

        /// <summary>
        /// 未抽卡的比赛id
        /// </summary>
        [DataMember]
        public Guid LotteryMatchId { get; set; }

        /// <summary>
        /// 比赛id 
        /// </summary>
        [DataMember]
        public Guid MatchId { get; set; }
    }
}
