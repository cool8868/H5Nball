using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Games.NBall.Entity.Response.Coach
{
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class CoachFrameEntity
    {
        /// <summary>
        /// 教练ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int CoachId { get; set; }

        /// <summary>
        /// 教练等级
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int CoachLevel { get; set; }
        
        /// <summary>
        /// 已有教练经验
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public int CoachExp { get; set; }

        /// <summary>
        /// 升级需要经验
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int NeedCoachExp { get; set; }

        /// <summary>
        /// 是否达到最大等级
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public bool IsMaxLevel { get; set; }

        /// <summary>
        /// 教练星级
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public int CoachStar { get; set; }

        /// <summary>
        /// 已有星级经验
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public int StarExp { get; set; }

        /// <summary>
        /// 升星需要升星经验
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public int NeedStarExp { get; set; }

        /// <summary>
        /// 是否达到最高星级
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public bool IsMaxStar { get; set; }

        /// <summary>
        /// 技能等级
        /// </summary>
        [DataMember]
        [ProtoMember(10)]
        public int SkillLevel { get; set; }

        /// <summary>
        /// 是否得到最高技能等级
        /// </summary>
        [DataMember]
        [ProtoMember(11)]
        public bool IsMaxSkillLevel { get; set; }

        /// <summary>
        /// 进攻
        /// </summary>
        [DataMember]
        [ProtoMember(12)]
        public decimal Offensive { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public decimal Organizational { get; set; }

        /// <summary>
        /// 防守
        /// </summary>
        [DataMember]
        [ProtoMember(14)]
        public decimal Defense { get; set; }

        /// <summary>
        /// 身体
        /// </summary>
        [DataMember]
        [ProtoMember(15)]
        public decimal BodyAttr { get; set; }

        /// <summary>
        /// 守门
        /// </summary>
        [DataMember]
        [ProtoMember(16)]
        public decimal Goalkeeping { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="offensive"></param>
        /// <param name="organizational"></param>
        /// <param name="defense"></param>
        /// <param name="bodyattr"></param>
        /// <param name="goalkeeping"></param>
        /// <param name="needCoachExp"></param>
        /// <param name="needStarExp"></param>
        public CoachFrameEntity(int coachId,int offensive,int organizational,int defense,int bodyattr,int goalkeeping,int needCoachExp,int needStarExp)
        {
            this.CoachId = coachId;
            this.CoachExp = 0;
            this.CoachLevel = 1;
            this.CoachStar = 0;
            this.SkillLevel = 1;
            this.StarExp = 0;
            this.Offensive = offensive;
            this.Organizational = organizational;
            this.Defense = defense;
            this.BodyAttr = bodyattr;
            this.Goalkeeping = goalkeeping;
            this.IsMaxLevel = false;
            this.IsMaxSkillLevel = false;
            this.IsMaxStar = false;
            this.NeedCoachExp = needCoachExp;
            this.NeedStarExp = needStarExp;
        }

        public CoachFrameEntity()
        {

        }
    }
}
