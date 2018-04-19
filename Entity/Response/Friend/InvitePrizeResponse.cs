using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Friend
{
    /// <summary>
    /// 领取好友邀请奖励输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class InvitePrizeResponse:BaseResponse<InvitePrize>
    {
    }

    [Serializable]
    [DataContract]
    public class InvitePrize
    {
        /// <summary>
        /// 获得的物品
        /// </summary>
        [DataMember]
        public List<ItemInfo> ItemList { get; set; }

        /// <summary>
        /// 剩余绑定点卷
        /// </summary>
        [DataMember]
        public int BindPoint { get; set; }
    }
}
