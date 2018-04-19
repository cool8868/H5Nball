using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Item
{
    /// <summary>
    /// 合成参数响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class SynthesisParamResponse : BaseResponse<SynthesisParamEntity>
    {

    }

    /// <summary>
    /// 合成参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class SynthesisParamEntity
    {
        /// <summary>
        /// 成功率
        /// </summary>
        [DataMember]
        public double Rate { get; set; }
        /// <summary>
        /// 消耗游戏币
        /// </summary>
        [DataMember]
        public int CostCoin { get; set; }
        /// <summary>
        /// 消耗点券
        /// </summary>
        [DataMember]
        public int CostPoint { get; set; }
        /// <summary>
        /// 可能合成的球员卡
        /// </summary>
        [DataMember]
        public List<int> PlayCardList { get; set; }
        /// <summary>
        /// 显示随机金卡
        /// </summary>
        [DataMember]
        public bool ShowRandomGoldCard { get; set; }

        /// <summary>
        /// 显示的概率
        /// </summary>
        [DataMember]
        public int ShowRate { get; set; }
    }
}
