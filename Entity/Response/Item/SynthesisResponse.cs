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
    /// 球员卡合成响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class SynthesisResponse : BaseResponse<SynthesisEntity>
    {
    }

    /// <summary>
    /// 球员卡合成
    /// </summary>
    [Serializable]
    [DataContract]
    public class SynthesisEntity
    {
        /// <summary>
        /// 合成出的卡牌code,为0表示合成失败
        /// </summary>
        [DataMember]
        public int ItemCode { get; set; }
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

        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public EquipmentProperty EquipmentProperty { get; set; }
        [DataMember]
        public bool IsBinding { get; set; }
    }
}
