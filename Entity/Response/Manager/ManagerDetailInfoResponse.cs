using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    [Serializable]
    [DataContract]
    public class ManagerDetailInfoResponse : BaseResponse<ManagerDetailInfoEntity>
    {
    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class ManagerDetailInfoEntity
    {
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public string Name { get; set; }
        [DataMember]
        [ProtoMember(2)]
        public string Logo { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int Level { get; set; }
        /// <summary>
        /// 综合实力
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int Kpi { get; set; }
        /// <summary>
        /// 公会名
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public string GuildName { get; set; }
        /// <summary>
        /// 天梯排名
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public int LadderRank { get; set; }
        /// <summary>
        /// 竞技场排名
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public int ArenaRank { get; set; }
        /// <summary>
        /// 冠军杯积分
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public int Score { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public NBSolutionInfo SolutionInfo { get; set; }
        [DataMember]
        [ProtoMember(10)]
        public string SolutionTalents { get; set; }

        /// <summary>
        /// 获取荣誉
        /// </summary>
        [DataMember]
        [ProtoMember(15)]
        public List<NbManagerhonorEntity> Honors { get; set; }
        /// <summary>
        /// 比赛统计
        /// </summary>
        [DataMember]
        [ProtoMember(16)]
        public List<NbMatchstatEntity> Matchstats { get; set; }
        [DataMember]
        [ProtoMember(17)]
        public int VipLevel { get; set; }
    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class TeammemberDetailEntity
    {
        /// <summary>
        /// pid
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int PlayerId { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int Level { get; set; }

        /// <summary>
        /// 能力值
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public double Kpi { get; set; }

        /// <summary>
        /// 强化等级
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int Strength { get; set; }

        /// <summary>
        /// 进攻
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public double PropertyAttack { get; set; }
        /// <summary>
        /// 身体
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public double PropertyBody { get; set; }
        /// <summary>
        /// 防守
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public double PropertyDefense { get; set; }
        /// <summary>
        /// 组织
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public double PropertyOrganize { get; set; }
        /// <summary>
        /// 守门
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public double PropertyGoalkeep { get; set; }

        ///// <summary>
        ///// 使用的装备
        ///// </summary>
        //[DataMember]
        //[ProtoMember(10)]
        //public EquipmentUsedEntity Equipment { get; set; }
    }
}
