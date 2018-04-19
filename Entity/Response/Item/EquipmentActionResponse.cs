using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Item
{
    [Serializable]
    [DataContract]
    public class EquipmentActionResponse : BaseResponse<EquipmentActionEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class EquipmentActionEntity
    {
        /// <summary>
        /// 装备信息
        /// </summary>
        [DataMember]
        public ItemInfoEntity ItemInfo { get; set; }
        /// <summary>
        /// 当前点券
        /// </summary>
        [DataMember]
        public int Point { get; set; }
        /// <summary>
        /// 球星技能附加
        /// </summary>
        [DataMember]
        public string StarskillPlus { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
