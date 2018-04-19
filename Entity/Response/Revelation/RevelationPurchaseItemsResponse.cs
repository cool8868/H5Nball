using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示勇气商城购买物品输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationPurchaseItemsResponse : BaseResponse<RevelationPurchaseItems>
    {
    }

    [Serializable]
    [DataContract]
    public class RevelationPurchaseItems
    {
        /// <summary>
        /// 兑换物品
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 获取兑换数量
        /// </summary>
        [DataMember]
        public int ItemCount { get; set; }

        /// <summary>
        /// 剩余勇气值
        /// </summary>
        [DataMember]
        public int Courage { get; set; }
    }
}
