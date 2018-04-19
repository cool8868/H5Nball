using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Item
{
    /// <summary>
    /// 强化响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResetPotentialResponse : BaseResponse<ResetPotential>
    {
    }

    /// <summary>
    /// 强化结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResetPotential
    {
        /// <summary>
        /// 需要更新的物品列表
        /// </summary>
        [DataMember]
        public ItemInfoEntity UpdateItem { get; set; }

        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// KPI
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }
    }
}
