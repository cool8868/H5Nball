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
    public class AuctionItemResponse : BaseResponse<AuctionItemEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class AuctionItemEntity
    {
        /// <summary>
        /// 我的拍卖列表
        /// </summary>
        [DataMember]
        public List<TransferMainEntity> MyTransferList { get; set; }

        /// <summary>
        /// 已经挂牌了多少个物品
        /// </summary>
        [DataMember]
        public int HaveTransferNumber { get; set; }

        /// <summary>
        /// 最多可以挂牌多少个物品
        /// </summary>
        [DataMember]
        public int MaxTransferNumber { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int TotalPageNumber { get; set; }

        /// <summary>
        /// 我的金条
        /// </summary>
        [DataMember]
        public int MyGoldBar { get; set; }
    
    }
}
