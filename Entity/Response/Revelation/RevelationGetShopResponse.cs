using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Revelation
{
    /// <summary>
    ///球星启示录开始一个关卡输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class RevelationGetShopResponse : BaseResponse<RevelationGetShop>
    {

    }


    [Serializable]
    [DataContract]
    public class RevelationGetShop
    {
        /// <summary>
        /// 物品列表
        /// </summary>
        [DataMember]
        public List<RevelationShopItem> ItemList { get; set; }

        /// <summary>
        /// 我的勇气值
        /// </summary>
        [DataMember]
        public int MyCourage { get; set; }

        /// <summary>
        /// 刷新需要钻石
        /// </summary>
        [DataMember]
        public int RefreshPoint { get; set; }

        /// <summary>
        /// 需要更新的钻石
        /// </summary>
        [DataMember]
        public int Point { get; set; }
    }
    
    [Serializable]
    [DataContract]
    public class RevelationShopItem
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int Idx{get;set;}

        /// <summary>
        /// 物品code
        /// </summary>
        [DataMember]
        public int ItemCode{get;set;}

        /// <summary>
        /// 物品数量
        /// </summary>
        [DataMember]
        public int ItemCount{get;set;}

        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        public int Price{get;set;}

        /// <summary>
        /// 是否可以兑换
        /// </summary>
        [DataMember]
        public bool IsMayBuy { get; set; }
    }
}
