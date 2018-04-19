
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_RevelationTheDifficulty 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigRevelationthedifficultyEntity
	{
		
		public ConfigRevelationthedifficultyEntity()
		{
		}

		public ConfigRevelationthedifficultyEntity(
		System.Int32 mark
,				System.Int32 smallclearance
,				System.Int32 bonuses
,				System.Int32 strengthenlevel
,				System.Int32 playersllevel
,				System.Int32 formationlevel
,				System.Int32 skillnums
,				System.String skillthequalityof
,				System.Int32 skilllevel
,				System.Boolean theteamwill
,				System.Int32 combinationskilllevel
,				System.Int32 equipmentnums
,				System.String equipmentthequalityof
,				System.String equipmentset
,				System.String awarythecourageto
		)
		{
			this.Mark = mark;
			this.SmallClearance = smallclearance;
			this.Bonuses = bonuses;
			this.StrengthenLevel = strengthenlevel;
			this.PlayersLlevel = playersllevel;
			this.FormationLevel = formationlevel;
			this.SkillNums = skillnums;
			this.SkillTheQualityOf = skillthequalityof;
			this.SkillLevel = skilllevel;
			this.TheTeamWill = theteamwill;
			this.CombinationSkillLevel = combinationskilllevel;
			this.EquipmentNums = equipmentnums;
			this.EquipmentTheQualityOf = equipmentthequalityof;
			this.EquipmentSet = equipmentset;
			this.AwaryTheCourageTo = awarythecourageto;
		}
		
		#region Public Properties
		
		///<summary>
		///大关
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Mark {get ; set ;}

		///<summary>
		///小关
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SmallClearance {get ; set ;}

		///<summary>
		///全队属性加成百分比
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Bonuses {get ; set ;}

		///<summary>
		///强化等级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 StrengthenLevel {get ; set ;}

		///<summary>
		///球员等级
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PlayersLlevel {get ; set ;}

		///<summary>
		///阵形等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 FormationLevel {get ; set ;}

		///<summary>
		///技能数量
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 SkillNums {get ; set ;}

		///<summary>
		///技能品质
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String SkillTheQualityOf {get ; set ;}

		///<summary>
		///技能等级
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///球队意志
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Boolean TheTeamWill {get ; set ;}

		///<summary>
		///组合技等级
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 CombinationSkillLevel {get ; set ;}

		///<summary>
		///装备数量
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 EquipmentNums {get ; set ;}

		///<summary>
		///装备品质
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String EquipmentTheQualityOf {get ; set ;}

		///<summary>
		///装备套系 1 =散装 3 = 3件套
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String EquipmentSet {get ; set ;}

		///<summary>
		///奖励勇气值
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String AwaryTheCourageTo {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigRevelationthedifficultyEntity Clone()
        {
            ConfigRevelationthedifficultyEntity entity = new ConfigRevelationthedifficultyEntity();
			entity.Mark = this.Mark;
			entity.SmallClearance = this.SmallClearance;
			entity.Bonuses = this.Bonuses;
			entity.StrengthenLevel = this.StrengthenLevel;
			entity.PlayersLlevel = this.PlayersLlevel;
			entity.FormationLevel = this.FormationLevel;
			entity.SkillNums = this.SkillNums;
			entity.SkillTheQualityOf = this.SkillTheQualityOf;
			entity.SkillLevel = this.SkillLevel;
			entity.TheTeamWill = this.TheTeamWill;
			entity.CombinationSkillLevel = this.CombinationSkillLevel;
			entity.EquipmentNums = this.EquipmentNums;
			entity.EquipmentTheQualityOf = this.EquipmentTheQualityOf;
			entity.EquipmentSet = this.EquipmentSet;
			entity.AwaryTheCourageTo = this.AwaryTheCourageTo;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_RevelationTheDifficulty 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigRevelationthedifficultyResponse : BaseResponse<ConfigRevelationthedifficultyEntity>
    {

    }
}

