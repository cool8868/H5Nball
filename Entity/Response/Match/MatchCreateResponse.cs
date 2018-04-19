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
    public class MatchCreateResponse : BaseResponse<MatchCreateEntity>
    {
    }

    public class MatchCreateEntity
    {
        /// <summary>
        /// 比赛ID
        /// </summary>
        [DataMember]
        public Guid MatchId { get; set; }

        /// <summary>
        /// 体力
        /// </summary>
        [DataMember]
        public int Stamina { get; set; }

    }
}
