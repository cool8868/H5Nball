using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Item
{
    /// <summary>
    /// 强化参数响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class StrengthParamResponse : BaseResponse<StrengthParamEntity>
    {
    }

    /// <summary>
    /// 强化参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class StrengthParamEntity
    {
        /// <summary>
        /// 强化成功后等级
        /// </summary>
        [DataMember]
        public int ResultStrength { get; set; }
        /// <summary>
        /// 失败类型：0,卡牌不降级；1，卡牌消失；2，球员卡强化等级分别降一级
        /// </summary>
        [DataMember]
        public int FailType { get; set; }
        /// <summary>
        /// 使用强化保护后的失败类型
        /// </summary>
        [DataMember]
        public int ProtectFailType { get; set; }
        /// <summary>
        /// 成功率
        /// </summary>
        [DataMember]
        public double Rate { get; set; }
        /// <summary>
        /// 消耗游戏币，强化保护不消耗游戏币
        /// </summary>
        [DataMember]
        public int CostCoin { get; set; }
        /// <summary>
        /// 消耗点券，只有强化保护消耗
        /// </summary>
        [DataMember]
        public int CostPoint { get; set; }
        /// <summary>
        /// 球员卡颜色等级
        /// </summary>
        public int CardLevel { get; set; }
        /// <summary>
        /// 幸运符增加成功率
        /// </summary>
        public int LuckyRate { get; set; }

        public bool IsProtect { get; set; }

        public double RealRate { get; set; }
        /// <summary>
        /// 再升一级成功率
        /// </summary>
        [DataMember]
        public double UpgradeRate { get; set; }
    }

    /// <summary>
    /// 强化参数响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class StrengthenClubClothesParamResponse : BaseResponse<StrengthenClubClothesParamEntity>
    {
    }

    /// <summary>
    /// 强化参数
    /// </summary>
    [Serializable]
    [DataContract]
    public class StrengthenClubClothesParamEntity
    {
        /// <summary>
        /// 强化成功后等级
        /// </summary>
        [DataMember]
        public int ResultStrength { get; set; }
        /// <summary>
        /// 成功率
        /// </summary>
        [DataMember]
        public double Rate { get; set; }
        /// <summary>
        /// 消耗游戏币，强化保护不消耗游戏币
        /// </summary>
        [DataMember]
        public int CostCoin { get; set; }
        /// <summary>
        /// 球员卡颜色等级
        /// </summary>
        public int CardLevel { get; set; }
       
    }

}
