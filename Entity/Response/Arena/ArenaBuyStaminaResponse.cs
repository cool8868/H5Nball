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
    public class ArenaBuyStaminaResponse : BaseResponse<ArenaBuyStamina>
    {
    }

    [Serializable]
    [DataContract]
    public class ArenaBuyStamina
    {
        /// <summary>
        /// 剩余点卷
        /// </summary>
        [DataMember]
        public int Point { get; set; }

        /// <summary>
        /// 体力对象
        /// </summary>
        [DataMember]
        public ArenaStamina StaminaEntity { get; set; }
    }

    [Serializable]
    [DataContract]
    public class ArenaStamina
    {
        /// <summary>
        /// 体力
        /// </summary>
        [DataMember]
        public int Stamina { get; set; }

        /// <summary>
        /// 最大体力
        /// </summary>
        [DataMember]
        public int MaxStamina { get; set; }

        /// <summary>
        /// 下次体力恢复时间
        /// </summary>
        [DataMember]
        public long NextRestoreStaminaTick { get; set; }

        /// <summary>
        /// 多少秒恢复1点
        /// </summary>
        [DataMember]
        public int RestoreTimes { get; set; }

        /// <summary>
        /// 是否可以恢复体力
        /// </summary>
        [DataMember]
        public bool IsRestoreStamina { get; set; }
    }
}
