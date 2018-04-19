using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Item
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class EquipmentUpgradeParamResponse : BaseResponse<UpgradeParamEntity>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [DataContract]
    public class UpgradeParamEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public ConfigEquipmentupgradeEntity UpgradeInfo { get; set; }
    }

}
