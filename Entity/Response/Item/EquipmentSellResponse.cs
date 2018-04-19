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
    /// 装备出售
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentSellResponse : BaseResponse<EquipmentSellEntity>
    {
    }

    /// <summary>
    /// 球员卡分解
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentSellEntity
    {
        /// <summary>
        /// 出售获得金币数量
        /// </summary>
        [DataMember]
        public int Coin { get; set; }
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
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
