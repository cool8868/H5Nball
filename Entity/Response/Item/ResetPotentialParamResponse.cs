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
    public class ResetPotentialParamResponse : BaseResponse<ResetPotentialParam>
    {
    }

    /// <summary>
    /// 强化结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class ResetPotentialParam
    {
        /// <summary>
        /// 剩余免费重置次数
        /// </summary>
        [DataMember]
        public int FreeResetNumber { get; set; }

        /// <summary>
        /// 锁定的数量
        /// </summary>
        [DataMember]
        public int LockNumber { get; set; }

        /// <summary>
        /// 需要消耗点卷
        /// </summary>
        [DataMember]
        public int CostPoint { get; set; }

    }
}
