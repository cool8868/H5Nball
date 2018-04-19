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
    public class LadderExchangeResponse : BaseResponse<LadderExchangeEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class LadderExchangeEntity
    {
        /// <summary>
        /// 兑换到的物品
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }
        /// <summary>
        /// 剩余荣誉值
        /// </summary>
        [DataMember]
        public int CurHonor { get; set; }

        /// <summary>
        /// 剩余天梯币
        /// </summary>
        [DataMember]
        public int LadderCoin { get; set; }
    }
}
