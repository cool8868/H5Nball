using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// 开始拍卖输出类
    /// </summary>
    [DataContract]
    [Serializable]
    public class AuctionResponse : BaseResponse<AuctionEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class AuctionEntity
    {
        /// <summary>
        /// 剩余金条
        /// </summary>
        [DataMember]
        public int GoldBar { get; set; }
    
    }
}
