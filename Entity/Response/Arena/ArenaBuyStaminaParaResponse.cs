using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class ArenaBuyStaminaParaResponse : BaseResponse<ArenaBuyStaminaPara>
    {
    }

    [Serializable]
    [DataContract]
    public class ArenaBuyStaminaPara
    {
        /// <summary>
        /// 消耗点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 体力对象
        /// </summary>
        [DataMember]
        public ArenaStamina StaminaEntity { get; set; }
    }

   
}
