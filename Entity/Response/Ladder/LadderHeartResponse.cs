using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ladder
{
    [Serializable]
    [DataContract]
    public class CrossLadderHeartResponse : BaseResponse<CrossLadderHeartEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class CrossLadderHeartEntity
    {
        public CrossLadderHeartEntity() { }

        public CrossLadderHeartEntity(Guid matchId,  Guid awayId, string awaySiteId,bool isBot,int awayKpi)
        {
            MatchId = matchId;
            AwayId = awayId;
            AwaySiteId = awaySiteId;
            AwayIsBot = isBot ? 1 : 0;
            AwayKpi = awayKpi;
        }
        [DataMember]
        public int AvgWaitTime { get; set; }
        [DataMember]
        public Guid MatchId { get; set; }
        [DataMember]
        public Guid AwayId { get; set; }
        [DataMember]
        public string AwaySiteId { get; set; }
        [DataMember]
        public int AwayIsBot { get; set; }

        [DataMember]
        public int AwayKpi { get; set; }
    }
}
