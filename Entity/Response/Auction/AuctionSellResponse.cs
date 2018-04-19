using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Auction
{
    [Serializable]
    [DataContract]
    public class AuctionSellResponse : BaseResponse<AuctionSellEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class AuctionSellEntity
    {
        /// <summary>
        /// 经理当前游戏币，-1不更新
        /// </summary>
        [DataMember]
        public int ManagerCoin { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }


    [Serializable]
    [DataContract]
    public class AuctionBuyResponse : BaseResponse<AuctionBuyEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class AuctionBuyEntity
    {
        /// <summary>
        /// 经理当前点券，-1不更新
        /// </summary>
        [DataMember]
        public int ManagerPoint { get; set; }
    }
}
