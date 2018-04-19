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
    public class UpgradeTheStarParamResponse : BaseResponse<UpgradeTheStarParam>
    {
    }

    /// <summary>
    /// 强化结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class UpgradeTheStarParam
    {
        /// <summary>
        /// 消耗游戏币，
        /// </summary>
        [DataMember]
        public int CostCoin { get; set; }

        /// <summary>
        /// 是否提示强化过的球员卡
        /// </summary>
        [DataMember]
        public int IsPromptStrengthen { get; set; }

        /// <summary>
        /// 是否提示升星过的球员
        /// </summary>
        [DataMember]
        public int IsPromptTheStart { get; set; }
    }
}
