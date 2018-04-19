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
    public class EquipmentPrecisionCastingResponse : BaseResponse<EquipmentPrecisionCastinEntity>
    {

    }

    [Serializable]
    [DataContract]
    public class EquipmentPrecisionCastinEntity
    {
        [DataMember]
        public ItemInfoEntity Equipment { get; set; }
        
        [DataMember]
        public int Point { get; set; }

        [DataMember]
        public int Coin { get; set; }
    }
}
