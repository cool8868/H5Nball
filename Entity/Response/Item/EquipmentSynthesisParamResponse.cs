using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Item
{
    /// <summary>
    /// 装备合成参数响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentSynthesisParamResponse : BaseResponse<EquipmentSynthesisParamEntity>
    {
    }

    /// <summary>
    /// 装备合成参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentSynthesisParamEntity
    {
        /// <summary>
        /// 配方对应的物品code串，逗号分隔,如20101,20102
        /// </summary>
        [DataMember]
        public string ItemString { get; set; }
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
    }
}
