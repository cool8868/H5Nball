using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Crowd
{
    [Serializable]
    [DataContract]
    public class CrowdHeartResponse : BaseResponse<CrowdHeartEntity>
    {

    }

    [Serializable]
    [DataContract]
    public class CrowdHeartEntity
    {
        public CrowdHeartEntity(){}

        public CrowdHeartEntity(Guid matchId, string awayName,string awayLogo, int awayMorale)
        {
            MatchId = matchId;
            AwayName = awayName;
            AwayMorale = awayMorale;
            AwayLogo = awayLogo;
        }

        public CrowdHeartEntity(Guid matchId, string awayName, string awayLogo, int awayMorale,Guid awayId,string awaySiteId)
            :this(matchId,awayName,awayLogo,awayMorale)
        {
            AwayId = awayId;
            AwaySiteId = awaySiteId;
        }

        [DataMember]
        public Guid MatchId { get; set; }
        [DataMember]
        public string AwayName { get; set; }
        [DataMember]
        public string AwayLogo { get; set; }
        [DataMember]
        public int AwayMorale { get; set; }
        [DataMember]
        public Guid AwayId { get; set; }
        [DataMember]
        public string AwaySiteId { get; set; }
    }
}
