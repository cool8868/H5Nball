using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using ProtoBuf;

namespace Games.NBall.Entity.NBall.Custom
{
    /// <summary>
    /// 球员卡属性
    /// </summary>
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class PlayerCardProperty : ItemProperty
    {
        public PlayerCardProperty()
        {

        }

        public PlayerCardProperty(int strength, Guid teammemberId, EquipmentUsedEntity equipment, bool isTrain, int exp,
            int theActualKpi, int level = 1, bool isMain = false, List<Potential> potential = null, int theStar = 0,
            int theStarExp = 0, int mainType = 0)
        {
            this.Strength = strength;
            this.TeammemberId = teammemberId;
            this.Equipment = equipment;
            this.Level = level;
            this.IsMain = isMain;
            this.IsTrain = isTrain;
            this.Exp = exp;
            this.TheActualKpi = theActualKpi;
            this.Potential = potential;
            if (this.Potential == null)
                this.Potential = new List<Potential>();
            this.TheStar = theStar;
            this.TheStarExp = theStarExp;
            this.MainType = mainType;
        }

        ///<summary>
        ///强化级别
        ///</summary>
        [DataMember]
        [ProtoMember(1)]
        public System.Int32 Strength { get; set; }

        /// <summary>
        /// 阵容ID
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public Guid TeammemberId { get; set; }

        /// <summary>
        /// 使用中的装备
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public EquipmentUsedEntity Equipment { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        [DataMember]
        [ProtoMember(4)]
        public int Level { get; set; }

        /// <summary>
        /// 是否为主力
        /// </summary>
        [DataMember]
        [ProtoMember(5)]
        public bool IsMain { get; set; }

        /// <summary>
        /// Kpi
        /// </summary>
        [DataMember]
        [ProtoMember(6)]
        public int Kpi { get; set; }

        /// <summary>
        /// 是否在训练
        /// </summary>
        [DataMember]
        [ProtoMember(7)]
        public bool IsTrain { get; set; }

        /// <summary>
        /// 实际KPI
        /// </summary>
        [DataMember]
        [ProtoMember(8)]
        public int TheActualKpi { get; set; }

        /// <summary>
        /// 经验
        /// </summary>
        [DataMember]
        [ProtoMember(9)]
        public int Exp { get; set; }

        /// <summary>
        /// 潜力列表
        /// </summary>
        [DataMember]
        [ProtoMember(10)]
        public List<Potential> Potential { get; set; }

        /// <summary>
        /// 球员星级
        /// </summary>
        [DataMember]
        [ProtoMember(11)]
        public int TheStar { get; set; }

        /// <summary>
        /// 星级经验
        /// </summary>
        [DataMember]
        [ProtoMember(12)]
        public int TheStarExp { get; set; }

        /// <summary>
        /// 主力类型  0=默认主力 1=	天空之城 2	重力感应 3	青春风暴 4	老兵不死 5	巨星闪耀 
        /// </summary>
        [DataMember]
        [ProtoMember(13)]
        public int MainType { get; set; }

        public override ItemProperty Clone()
        {
            var entity = new PlayerCardProperty();
            entity.Strength = this.Strength;
            entity.TeammemberId = this.TeammemberId;
            entity.Equipment = this.Equipment;
            entity.Level = this.Level;
            entity.IsMain = this.IsMain;
            entity.Kpi = this.Kpi;
            entity.Exp = this.Exp;
            entity.IsTrain = this.IsTrain;
            entity.TheActualKpi = this.TheActualKpi;
            entity.Potential = this.Potential;
            if (entity.Potential == null)
                entity.Potential = new List<Custom.Potential>();
            entity.TheStar = this.TheStar;
            entity.TheStarExp = this.TheStarExp;
            entity.MainType = this.MainType;
            return entity;
        }


    }

    [Serializable]
    [DataContract]
    [ProtoContract]
    public class Potential
    {
        /// <summary>
        /// 潜力ID
        /// </summary>
        [DataMember]
        [ProtoMember(1)]
        public int Idx { get; set; }

        /// <summary>
        /// 潜力等级 1低级 2中级 3高级
        /// </summary>
        [DataMember]
        [ProtoMember(2)]
        public int Level { get; set; }

        /// <summary>
        /// 潜力数值
        /// </summary>
        [DataMember]
        [ProtoMember(3)]
        public decimal Buff { get; set; }
    }
}
