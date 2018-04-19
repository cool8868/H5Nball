using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Ad
{
    /// <summary>
    /// 获取点球射门信息输出类
    /// </summary>
    [Serializable]
    [DataContract]
    public class PenaltyKickShootResponse : BaseResponse<PenaltyKickShoot>
    {
    }
    [Serializable]
    [DataContract]
    public class PenaltyKickShoot
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        [DataMember]
        public GetPenaltyKickInfo Info { get; set; }

        /// <summary>
        /// 射门结果
        /// </summary>
        [DataMember]
        public ShootItem ShootResult { get; set; }

        /// <summary>
        /// 获得的物品
        /// </summary>
        [DataMember]
        public List<PrizeEntity> ItemList { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ShootItem
    {
        /// <summary>
        /// 是否进球
        /// </summary>
        [DataMember]
        public bool IsGoals { get; set; }

        /// <summary>
        /// 射门类型 0-射正;1-门框;2-射飞
        /// </summary>
        [DataMember]
        public int ShootPos { get; set; }

        /// <summary>
        /// 扑救是否一致
        /// </summary>
        [DataMember]
        public bool DiveDir { get; set; }
    }
}
