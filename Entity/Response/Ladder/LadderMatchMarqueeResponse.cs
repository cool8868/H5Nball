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
    public class LadderMatchMarqueeResponse : BaseResponse<LadderMatchMarqueeData>
    {
    }

    [DataContract]
    [Serializable]
    public class LadderMatchMarqueeData
    {
        [DataMember]
        public List<LadderMatchMarqueeEntity> Matchs { get; set; }
    }

    [DataContract]
    [Serializable]
    public class LadderMatchMarqueeEntity
    {
        [DataMember]
        public Guid MatchId { get; set; }
        [DataMember]
        public string HomeName { get; set; }
        [DataMember]
        public int HomeScore { get; set; }
        [DataMember]
        public string AwayName { get; set; }
        [DataMember]
        public int AwayScore { get; set; }
        [DataMember]
        public string HomeLogo { get; set; }
        [DataMember]
        public string AwayLogo { get; set; }
    }
}
