using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;

namespace Games.NBall.Entity.Response.Item
{
    public class SuitdrawingDecomposeResponse : BaseResponse<SuitdrawingDecomposeEntity>
    {

    }

    [Serializable]
    [DataContract]
    public class SuitdrawingDecomposeEntity
    {
        [DataMember]
        public int Coin { get; set; }

        [DataMember]
        public int ItemCode { get; set; }
        [DataMember]
        public bool IsBinding { get; set; }
        /// <summary>
        /// 装备属性
        /// </summary>
        [DataMember]
        public EquipmentProperty EquipmentProperty { get; set; }
        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }
    }
}
