using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Friend
{
    /// <summary>
    /// 领取好友成长奖励输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class GrowUpPrizeResponse:BaseResponse<GrowUpPrize>
    {
    }

    [Serializable]
    [DataContract]
    public class GrowUpPrize
    {
        /// <summary>
        /// 获得的点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 总点卷
        /// </summary>
        [DataMember]
        public int SumPoint { get; set; }
    }
}
