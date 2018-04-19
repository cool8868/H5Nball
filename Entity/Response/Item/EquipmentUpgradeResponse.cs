using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Item
{
    [Serializable]
    [DataContract]
    public class EquipmentUpgradeResponse : BaseResponse<UpgradeInfoEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class UpgradeInfoEntity
    {
        /// <summary>
        /// 升阶后装备信息
        /// </summary>
        [DataMember]
        public ItemInfoEntity Item { get; set; }

        /// <summary>
        /// 返回剩余金币
        /// </summary>
        [DataMember]
        public int Coin { get; set; }

        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 进阶是否成功
        /// </summary>
        [DataMember]
        public int IsSuccess { get; set; }
    }
}
