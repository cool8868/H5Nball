using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [DataContract]
    [Serializable]
    public class ManagerStaminaResponse : BaseResponse<ManagerStaminaEntity>
    {
    }

    [DataContract]
    [Serializable]
    public class ManagerStaminaEntity
    {
        ///<summary>
        ///体力
        ///</summary>
        [DataMember]
        public System.Int32 Stamina { get; set; }

        ///<summary>
        ///最大体力
        ///</summary>
        [DataMember]
        public System.Int32 StaminaMax { get; set; }

        /// <summary>
        /// 恢复体力时间刻度
        /// </summary>
        [DataMember]
        public long ResumeStaminaTimeTick { get; set; }
    }
}
