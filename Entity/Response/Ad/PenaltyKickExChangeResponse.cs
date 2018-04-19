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
    public class PenaltyKickExChangeResponse : BaseResponse<PenaltyKickExChange>
    {
    }
    [Serializable]
    [DataContract]
    public class PenaltyKickExChange
    {
        /// <summary>
        /// 可兑换的物品
        /// </summary>
        [DataMember]
        public List<PenaltyKickExChangeEntity> ExChangeList { get; set; }

        /// <summary>
        /// 兑换得到的物品
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 剩余可用积分
        /// </summary>
        [DataMember]
        public int AvailableScore { get; set; }
    }

    [Serializable]
    [DataContract]
    public class PenaltyKickExChangeEntity
    {
        /// <summary>
        /// 物品code
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [DataMember]
        public int Price { get; set; }

        /// <summary>
        /// 兑换状态 0=可兑换 1=已经兑换
        /// </summary>
        [DataMember]
        public int Status { get; set; }
    }
}
