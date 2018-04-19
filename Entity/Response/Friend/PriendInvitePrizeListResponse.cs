using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Games.NBall.Entity.Response.Friend
{
    /// <summary>
    /// 好友邀请活动奖励输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class PriendInvitePrizeListResponse:BaseResponse<PriendInvitePrizeList>
    {
    }

    [Serializable]
    [DataContract]
    public class PriendInvitePrizeList
    {
        /// <summary>
        /// 当前成功邀请人数
        /// </summary>
        [DataMember]
        public int InviteCount { get; set; }

        /// <summary>
        /// 再邀请多少个获得奖励
        /// </summary>
        [DataMember]
        public int NextInviteCount { get; set; }

        /// <summary>
        /// 奖励ID
        /// </summary>
        [DataMember]
        public int PrizeId { get; set; }

        /// <summary>
        /// 是否邀请完成
        /// </summary>
        [DataMember]
        public bool IsInviteAccomplish { get; set; }

        /// <summary>
        /// 物品集合
        /// </summary>
        [DataMember]
        public List<ItemInfo> ItemList { get; set; }

    }

    [Serializable]
    [DataContract]
    public class ItemInfo
    {
        /// <summary>
        /// 奖励类型 
        /// </summary>
        [DataMember]
        public int PrizeType { get; set; }

        /// <summary>
        /// 物品ID
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// 是否绑定
        /// </summary>
        [DataMember]
        public bool IsBinding { get; set; }
    }
}
