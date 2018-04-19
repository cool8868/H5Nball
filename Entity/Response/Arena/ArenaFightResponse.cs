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
    public class ArenaFightResponse : BaseResponse<ArenaFight>
    {
    }

    [Serializable]
    [DataContract]
    public class ArenaFight
    {
        /// <summary>
        /// 胜利类型
        /// </summary>
        [DataMember]
        public int WinType { get; set; }

        /// <summary>
        /// 获得积分
        /// </summary>
        [DataMember]
        public int Integral { get; set; }

        /// <summary>
        /// 对手列表
        /// </summary>
        [DataMember]
        public List<OpponentEntity> OpponentList { get; set; }

        /// <summary>
        /// 体力对象
        /// </summary>
        [DataMember]
        public ArenaStamina StaminEntity { get; set; }

        /// <summary>
        /// 我的积分
        /// </summary>
        [DataMember]
        public int MyIntegral { get; set; }

        /// <summary>
        /// 我的段位
        /// </summary>
        [DataMember]
        public int DanGrading { get; set; }

        /// <summary>
        /// 升级积分
        /// </summary>
        [DataMember]
        public int UpIntegral { get; set; }
        /// <summary>
        /// 是否达到最高段位
        /// </summary>
        [DataMember]
        public bool IsMaxDanGrading { get; set; }

        /// <summary>
        /// 我的排名
        /// </summary>
        [DataMember]
        public int MyRank { get; set; }

        /// <summary>
        /// 比赛ID
        /// </summary>
        [DataMember]
        public Guid MatchId { get; set; }
    }
}
