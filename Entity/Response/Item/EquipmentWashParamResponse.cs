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
    public class EquipmentWashParamResponse : BaseResponse<EquipmentWashParamEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class EquipmentWashParamEntity
    {
        /// <summary>
        /// 洗炼石点券,没有则为0
        /// </summary>
        [DataMember]
        public int StonePoint { get; set; }
        /// <summary>
        /// 融合剂点券
        /// </summary>
        [DataMember]
        public int FusogenPoint { get; set; }
        /// <summary>
        /// 锁定属性点券
        /// </summary>
        [DataMember]
        public int LockPropertyPoint { get; set; }
    }
}
