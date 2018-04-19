using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Games.NBall.Common;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class TeammemberEntity
    { /// <summary>
        /// 能力值
        /// </summary>
        [DataMember]
        [ProtoMember(41)]
        public double Kpi { get; set; }

        /// <summary>
        /// 使用的球员卡
        /// </summary>
        [DataMember]
        [ProtoMember(42)]
        public PlayerCardUsedEntity PlayerCard { get; set; }

        /// <summary>
        /// 使用的装备
        /// </summary>
        [DataMember]
        [ProtoMember(43)]
        public EquipmentUsedEntity Equipment { get; set; }

        /// <summary>
        /// 未分配的属性点
        /// </summary>
        [DataMember]
        [ProtoMember(44)]
        public int PropertyPoint { get; set; }

        /// <summary>
        /// 加点最大值
        /// </summary>
        [DataMember]
        [ProtoMember(47)]
        public int PropertyMax { get; set; }

        /// <summary>
        /// 是否主力
        /// </summary>
        [DataMember]
        [ProtoMember(45)]
        public bool IsMain { get; set; }

        /// <summary>
        /// 加点的属性
        /// </summary>
        [DataMember]
        [ProtoMember(46)]
        public TeammemberPropertyEntity RawProperty { get; set; }

        /// <summary>
        /// 基础属性
        /// </summary>
        [DataMember]
        [ProtoMember(48)]
        public TeammemberPropertyEntity BaseProperty { get; set; }

        /// <summary>
        /// PopMsg
        /// </summary>
        [DataMember]
        public List<PopMessageEntity> PopMsg { get; set; }

        /// <summary>
        /// 是否为租借的球员
        /// </summary>
        [DataMember]
        [ProtoMember(52)]
        public bool IsHirePlayer { get; set; }

        private int _strength = -1;

        public string Name { get; set; }

        [DataMember]
        [ProtoMember(51)]
        public int GrowLevel { get; set; }

        /// <summary>
        /// 经理KPI
        /// </summary>
        [DataMember]
        [ProtoMember(53)]
	    public int TotalKpi { get; set; }

        /// <summary>
        /// 背包
        /// </summary>
        [DataMember]
        [ProtoMember(54)]
	    public ItemPackageData Package { get; set; }

        /// <summary>
        /// 阵型类型 0=默认  1-5 = 竞技场
        /// </summary>
        [DataMember]
        [ProtoMember(55)]
        public int MainType { get; set; }

	    /// <summary>
        /// 强化级别
        /// </summary>
        [DataMember]
        public int Strength
        {
            get
            {
                if (_strength == -1)
                    _strength = GetStrength();
                return _strength;
            }
            set { _strength = value; }
        }

        int GetStrength()
        {
            if (PlayerCard == null)
            {
                if (this.UsedPlayerCard == null)
                    return 1;
                PlayerCard = SerializationHelper.FromByte<PlayerCardUsedEntity>(this.UsedPlayerCard);
            }
            if (PlayerCard != null && PlayerCard.Property != null)
                return PlayerCard.Property.Strength;
            else
            {
                return 1;
            }
        }

        #region Spec4Hire
        public int Position
        {
            get;
            set;
        }
        public int CardLevel
        {
            get;
            set;
        }

	    public TeammemberEntity(System.Guid managerid, System.Int32 playerid, System.Int32 level,
	        System.Int32 strengthenlevel)
	    {
	        this.ManagerId = managerid;
	        this.PlayerId = playerid;
	        this.Level = level;
	        this.StrengthenLevel = strengthenlevel;
	    }

	    #endregion
    }


    /// <summary>
    /// 对Table dbo.Teammember 的输出映射.
    /// </summary>
    public partial class TeammemberResponse
    {

    }
    
    [Serializable]
    [DataContract]
    [ProtoContract]
    public class TeammemberPropertyEntity
    {
        public TeammemberPropertyEntity()
        {

        }

        public TeammemberPropertyEntity(DicPlayerEntity entity)
        {
            this.Speed = entity.Speed;
            this.Shoot = entity.Shoot;
            this.FreeKick = entity.FreeKick;
            this.Balance = entity.Balance;
            this.Physique = entity.Physique;
            this.Bounce = entity.Bounce;
            this.Aggression = entity.Aggression;
            this.Disturb = entity.Disturb;
            this.Interception = entity.Interception;
            this.Dribble = entity.Dribble;
            this.Pass = entity.Pass;
            this.Mentality = entity.Mentality;
            this.Response = entity.Response;
            this.Positioning = entity.Positioning;
            this.HandControl = entity.HandControl;
        }

        public TeammemberPropertyEntity(TeammemberEntity entity)
        {
            this.Speed = entity.Speed;
            this.Shoot = entity.Shoot;
            this.FreeKick = entity.FreeKick;
            this.Balance = entity.Balance;
            this.Physique = entity.Physique;
            this.Bounce = entity.Bounce;
            this.Aggression = entity.Aggression;
            this.Disturb = entity.Disturb;
            this.Interception = entity.Interception;
            this.Dribble = entity.Dribble;
            this.Pass = entity.Pass;
            this.Mentality = entity.Mentality;
            this.Response = entity.Response;
            this.Positioning = entity.Positioning;
            this.HandControl = entity.HandControl;
        }

        ///<summary>
        ///速度-加点
        ///</summary>
        [DataMember]
        [ProtoMember(1)]
        public System.Double Speed { get; set; }

        ///<summary>
        ///射门-加点
        ///</summary>
        [DataMember]
        [ProtoMember(2)]
        public System.Double Shoot { get; set; }

        ///<summary>
        ///任意球-加点
        ///</summary>
        [DataMember]
        [ProtoMember(3)]
        public System.Double FreeKick { get; set; }

        ///<summary>
        ///控制-加点
        ///</summary>
        [DataMember]
        [ProtoMember(4)]
        public System.Double Balance { get; set; }

        ///<summary>
        ///体质-加点
        ///</summary>
        [DataMember]
        [ProtoMember(5)]
        public System.Double Physique { get; set; }

        ///<summary>
        ///弹跳-加点
        ///</summary>
        [DataMember]
        [ProtoMember(6)]
        public System.Double Bounce { get; set; }

        ///<summary>
        ///侵略性-加点
        ///</summary>
        [DataMember]
        [ProtoMember(7)]
        public System.Double Aggression { get; set; }

        ///<summary>
        ///干扰-加点
        ///</summary>
        [DataMember]
        [ProtoMember(8)]
        public System.Double Disturb { get; set; }

        ///<summary>
        ///抢断-加点
        ///</summary>
        [DataMember]
        [ProtoMember(9)]
        public System.Double Interception { get; set; }

        ///<summary>
        ///控球-加点
        ///</summary>
        [DataMember]
        [ProtoMember(10)]
        public System.Double Dribble { get; set; }

        ///<summary>
        ///传球-加点
        ///</summary>
        [DataMember]
        [ProtoMember(11)]
        public System.Double Pass { get; set; }

        ///<summary>
        ///意识-加点
        ///</summary>
        [DataMember]
        [ProtoMember(12)]
        public System.Double Mentality { get; set; }

        ///<summary>
        ///反应-加点
        ///</summary>
        [DataMember]
        [ProtoMember(13)]
        public System.Double Response { get; set; }

        ///<summary>
        ///位置感-加点
        ///</summary>
        [DataMember]
        [ProtoMember(14)]
        public System.Double Positioning { get; set; }

        ///<summary>
        ///手控球-加点
        ///</summary>
        [DataMember]
        [ProtoMember(15)]
        public System.Double HandControl { get; set; }
    }
}


