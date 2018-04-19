using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.League
{
    [Serializable]
    [DataContract]
    public class LeagueExchangeResponse : BaseResponse<LeagueExchangeEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class LeagueExchangeEntity
    {
        /// <summary>
        /// 兑换到的物品
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }
        /// <summary>
        /// 剩余积分
        /// </summary>
        [DataMember]
        public int CurScore { get; set; }
    }
}
