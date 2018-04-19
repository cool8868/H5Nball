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
    public class ArenaGetOpponentResponse : BaseResponse<ArenaOpponent>
    {
    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ArenaOpponent
    {
        /// <summary>
        /// 对手列表
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public List<OpponentEntity> OpponentList { get; set; }

        /// <summary>
        /// 打过比赛的对手ID
        /// </summary>
        [ProtoMember(2)]
        public List<Guid> MatchOpponent { get; set; }

        /// <summary>
        /// 体力对象
        /// </summary>
        [DataMember]
        public ArenaStamina StaminaEntity { get;set; }

    }

    /// <summary>
    /// 对手信息
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class OpponentEntity
    {
        /// <summary>
        /// 对手经理ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public Guid OpponentManagerId { get; set; }

        /// <summary>
        /// 对手区ID
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public string OpponentZoneName { get; set; }

        /// <summary>
        /// 对手名字
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public string OpponentName { get; set; }

        /// <summary>
        /// 对手实力
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int OpponentKpi { get; set; }

        /// <summary>
        /// 对手Logo
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public string OpponentLogo { get; set; }

        /// <summary>
        /// 对手积分
        /// </summary>
        [ProtoMember(6)]
        public int OpponentIntegral { get; set; }

        /// <summary>
        /// 对手段位
        /// </summary>
        [ProtoMember(7)]
        public int OpponentDanGrading { get; set; }

        /// <summary>
        /// 是否是Npc
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public bool IsNpc { get; set; }

        /// <summary>
        /// 可获得积分
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public int GetIntegral { get; set; }

    }


}
