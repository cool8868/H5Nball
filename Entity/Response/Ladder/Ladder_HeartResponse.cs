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
    public class Ladder_HeartResponse : BaseResponse<Ladder_Heart>
    {
    }

    [DataContract]
    [Serializable]
    public class Ladder_Heart
    {
        /// <summary>
        /// 平均等待时间，单位秒
        /// Gets or sets the avg wait time.
        /// </summary>
        /// <value>The avg wait time.</value>
        [DataMember]
        public double AvgWaitTime
        { get; set; }

        /// <summary>
        /// Gets or sets the match id.
        /// </summary>
        /// <value>The match id.</value>
        [DataMember]
        public Guid MatchId
        { get; set; }
    }
}
