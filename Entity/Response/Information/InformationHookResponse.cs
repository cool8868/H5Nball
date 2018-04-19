using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Information
{
    [Serializable]
    [DataContract]
    public class InformationHookResponse : BaseResponse<InformationHookEntity>
    {
    }

    [Serializable]
    [DataContract]
    public class InformationHookEntity
    {
        /// <summary>
        /// 挂机类型，0:无；1：巡回赛；2：世界挑战赛
        /// </summary>
        [DataMember]
        public int HookType { get; set; }
        /// <summary>
        /// 巡回赛关卡，或世界挑战赛关卡
        /// </summary>
        [DataMember]
        public int StageId { get; set; }

        ///<summary>
        ///巡回赛挂机时有效，总次数/挂机次数上限
        ///</summary>
        [DataMember]
        public System.Int32 TotalFightTimes { get; set; }

        ///<summary>
        ///巡回赛挂机时有效，已战斗次数
        ///</summary>
        [DataMember]
        public System.Int32 FightTimes { get; set; }

        ///<summary>
        ///下一次比赛开始时间
        ///</summary>
        [DataMember]
        public int NextMatchTick { get; set; }

    }
}
