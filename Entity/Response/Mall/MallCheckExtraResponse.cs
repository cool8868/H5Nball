using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Config.Custom;

namespace Games.NBall.Entity.Response
{
    /// <summary>
    /// MallCheckExtraResponse
    /// </summary>
    public class MallCheckExtraResponse : BaseResponse<MallCheckExtraEntity>
    {
    }

    /// <summary>
    /// 虚拟道具check
    /// </summary>
    public class MallCheckExtraEntity
    {
        /// <summary>
        /// 对应消息code
        /// </summary>
        public int MessageCode { get; set; }
        /// <summary>
        /// 花费点券数量
        /// </summary>
        public int CostPoint { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyCount { get; set; }
        /// <summary>
        /// 花费金币数量
        /// </summary>
        public int CostCoin { get; set; }

        /// <summary>
        /// 可购买总次数
        /// </summary>
        public int MaxNumber { get; set; }

        /// <summary>
        /// 已经购买次数
        /// </summary>
        public int HaveNumber { get; set; }
    }
}
