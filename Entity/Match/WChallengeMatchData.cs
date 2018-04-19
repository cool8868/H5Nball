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
    /// 世界挑战赛比赛数据
    /// </summary>
    [Serializable]
    [DataContract]
    public class WChallengeMatchData : BaseMatchData
    {
        public WChallengeMatchData(Guid matchId, Guid homeId, Guid npcId)
            : base(EnumMatchType.WorldChallenge, matchId, homeId, npcId)
        {
            this.Away.IsNpc = true;
        }
        
       // public WorldchallengeRecordEntity WorldchallengeRecord { get; set; }
    }
}
