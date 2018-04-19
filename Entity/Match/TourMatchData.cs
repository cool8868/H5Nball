using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity.Match
{
    /// <summary>
    /// 巡回赛比赛数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class TourMatchData:BaseMatchData
    {
        public TourMatchData(Guid matchId, Guid homeId, Guid npcId,int stageId)
            : base(EnumMatchType.Tour, matchId, homeId, npcId)
        {
            this.StageId = stageId;
            this.Away.IsNpc = true;
        }

        /// <summary>
        /// 关卡id
        /// </summary>
        public int StageId { get; set; }

        public int GuideTaskRecordId { get; set; }
    }
}
