using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Friend
{ 
    /// <summary>
    /// 好友邀请活动成长奖励输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class InviteFriendGrowUpPrizeResponse:BaseResponse<InviteFriendGrowUpPrize>
    {
    }

    [Serializable]
    [DataContract]
    public class InviteFriendGrowUpPrize
    {
        /// <summary>
        /// 是否有奖励领取
        /// </summary>
        [DataMember]
        public bool IsHavePrize { get; set; }

        /// <summary>
        /// 已经领取的点卷
        /// </summary>
        [DataMember]
        public int AlreadyPoint { get; set; }

        /// <summary>
        /// 没有领取的点卷
        /// </summary>
        [DataMember]
        public int NotPoint { get; set; }

        /// <summary>
        /// 经理信息列表
        /// </summary>
        [DataMember]
        public List<FriendManagerInfo> ManagerList { get; set; }
    }

    public class FriendManagerInfo 
    {
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 当前等级
        /// </summary>
        [DataMember]
        public int Level { get; set; }

        /// <summary>
        /// 已经获得的点卷
        /// </summary>
        [DataMember]
        public int HavePoint { get; set; }
    }
}
