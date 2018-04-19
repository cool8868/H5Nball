
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Template_Giants 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class TemplateGiantsEntity
	{
		
		public TemplateGiantsEntity()
		{
		}

		public TemplateGiantsEntity(
		System.Int32 markid
,				System.Int32 round
,				System.Int32 splay
,				System.Int32 eplay
,				System.Int32 strength
,				System.Int32 playlevel
,				System.Int32 formationlevel
,				System.Int32 skillcount
,				System.Int32 minskillclass
,				System.Int32 maxskillclass
,				System.Int32 skilllevel
,				System.Boolean iswill
,				System.Int32 equipcount
,				System.Int32 equipquality
,				System.Int32 suittype
,				System.Int32 talentlevel
		)
		{
			this.MarkId = markid;
			this.Round = round;
			this.SPlay = splay;
			this.Eplay = eplay;
			this.Strength = strength;
			this.playLevel = playlevel;
			this.FormationLevel = formationlevel;
			this.SkillCount = skillcount;
			this.MinSkillClass = minskillclass;
			this.MaxSkillClass = maxskillclass;
			this.SkillLevel = skilllevel;
			this.IsWill = iswill;
			this.EquipCount = equipcount;
			this.EquipQuality = equipquality;
			this.SuitType = suittype;
			this.TalentLevel = talentlevel;
		}
		
		#region Public Properties
		
		///<summary>
		///关卡ID
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 MarkId {get ; set ;}

		///<summary>
		///回合数
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Round {get ; set ;}

		///<summary>
		///球员能力
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SPlay {get ; set ;}

		///<summary>
		///球员能力
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Eplay {get ; set ;}

		///<summary>
		///强化等级
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Strength {get ; set ;}

		///<summary>
		///球员等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 playLevel {get ; set ;}

		///<summary>
		///阵形等级
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 FormationLevel {get ; set ;}

		///<summary>
		///技能数量
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 SkillCount {get ; set ;}

		///<summary>
		///技能等级
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 MinSkillClass {get ; set ;}

		///<summary>
		///技能等级
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 MaxSkillClass {get ; set ;}

		///<summary>
		///技能等级
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///是否有意志
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Boolean IsWill {get ; set ;}

		///<summary>
		///装备数量
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 EquipCount {get ; set ;}

		///<summary>
		///装备品质
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 EquipQuality {get ; set ;}

		///<summary>
		///套装类型
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 SuitType {get ; set ;}

		///<summary>
		///TalentLevel
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 TalentLevel {get ; set ;}
		#endregion
        
        #region Clone
        public TemplateGiantsEntity Clone()
        {
            TemplateGiantsEntity entity = new TemplateGiantsEntity();
			entity.MarkId = this.MarkId;
			entity.Round = this.Round;
			entity.SPlay = this.SPlay;
			entity.Eplay = this.Eplay;
			entity.Strength = this.Strength;
			entity.playLevel = this.playLevel;
			entity.FormationLevel = this.FormationLevel;
			entity.SkillCount = this.SkillCount;
			entity.MinSkillClass = this.MinSkillClass;
			entity.MaxSkillClass = this.MaxSkillClass;
			entity.SkillLevel = this.SkillLevel;
			entity.IsWill = this.IsWill;
			entity.EquipCount = this.EquipCount;
			entity.EquipQuality = this.EquipQuality;
			entity.SuitType = this.SuitType;
			entity.TalentLevel = this.TalentLevel;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Template_Giants 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class TemplateGiantsResponse : BaseResponse<TemplateGiantsEntity>
    {

    }
}

