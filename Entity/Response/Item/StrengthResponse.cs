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
    public class StrengthResponse : BaseResponse<StrengthEntity>
    {
    }

    /// <summary>
    /// 强化结果
    /// </summary>
    [Serializable]
    [DataContract]
    public class StrengthEntity
    {
        /// <summary>
        /// 强化成功后等级,为0表示强化失败了,失败类型见 FailType
        /// </summary>
        [DataMember]
        public int ResultStrength { get; set; }
        /// <summary>
        /// 失败类型：0,卡牌不降级；1，卡牌消失；2，球员卡强化等级分别降一级
        /// </summary>
        [DataMember]
        public int FailType { get; set; }
        /// <summary>
        /// 当前游戏币，-1表示无效，不用更新
        /// </summary>
        [DataMember]
        public int Coin { get; set; }
        /// <summary>
        /// 当前点券，-1表示无效，不用更新
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        [DataMember]
        public int Kpi { get; set; }

        /// <summary>
        /// 背包数据
        /// </summary>
        [DataMember]
        public ItemPackageData Package { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
