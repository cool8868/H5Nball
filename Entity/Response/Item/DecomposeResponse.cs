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
    /// 球员卡分解响应
    /// </summary>
    [Serializable]
    [DataContract]
    public class DecomposeResponse : BaseResponse<DecomposeEntity>
    {
    }

    /// <summary>
    /// 球员卡分解
    /// </summary>
    [Serializable]
    [DataContract]
    public class DecomposeEntity
    {
        /// <summary>
        /// 是否暴击
        /// </summary>
        [DataMember]
        public bool IsCrit { get; set; }
        /// <summary>
        /// 分解获得金币数量
        /// </summary>
        [DataMember]
        public int Coin { get; set; }

        /// <summary>
        /// 分解获得装备Id
        /// </summary>
        [DataMember]
        public string EquipmentId { get; set; }

        /// <summary>
        /// 经理当前金币数量
        /// </summary>
        [DataMember]
        public int ManagerCoin { get; set; }

        /// <summary>
        /// 背包数据
        /// </summary>
        [DataMember]
        public ItemPackageData Package { get; set; }

        /// <summary>
        /// 奥运金牌ID
        /// </summary>
        [DataMember]
        public int OlympicTheGoldMedalId { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
