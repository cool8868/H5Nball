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
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentPrecisionCastingParamResponse : BaseResponse<EquipmentPrecisionCastingParamEntity>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentPrecisionCastingParamEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LockPropertyPoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int Coin { get; set; }


    }
}
