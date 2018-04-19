using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Match
{
    [DataContract]
    [Serializable]
    public class Match_FightinfoResponse : BaseResponse<Match_Fightinfo>
    {
    }

    [DataContract]
    [Serializable]
    public class Match_Fightinfo
    {
        [DataMember]
        public Match_FightManagerinfo Home { get; set; }
        [DataMember]
        public Match_FightManagerinfo Away { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Match_FightManagerinfo
    {
        [DataMember]
        public Guid ManagerId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public int FormationId { get; set; }
        [DataMember]
        public int CoachId { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public double Kpi { get; set; }
        [DataMember]
        public double KpiReady { get; set; }
        [DataMember]
        public List<NBSolutionTeammember> Teammembers { get; set; }
    }
}
