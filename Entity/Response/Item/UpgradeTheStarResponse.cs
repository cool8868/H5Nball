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
    public class UpgradeTheStarResponse : BaseResponse<UpgradeTheStar>
    {
    }

    /// <summary>
    /// 强化结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class UpgradeTheStar
    {
        /// <summary>
        /// 需要更新的物品列表
        /// </summary>
        [DataMember]
        public List<ItemInfoEntity> UpdateItem { get; set; }

        /// <summary>
        /// 需要删除的物品列表
        /// </summary>
        [DataMember]
        public List<Guid> DeleteItem { get; set; }

        /// <summary>
        /// 剩余金币
        /// </summary>
        [DataMember]
        public int Coin { get; set; }

        /// <summary>
        /// KPI
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

    }
}
