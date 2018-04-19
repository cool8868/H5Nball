
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CoachSkill 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCoachskillEntity
	{
		
		public ConfigCoachskillEntity()
		{
		}

		public ConfigCoachskillEntity(
		System.Int32 coachid
,				System.String skillname
,				System.String triggercondition
,				System.Int32 cd
,				System.String timeofduration
,				System.Int32 triggerprobability
,				System.String description
,				System.String plusdescription
,				System.Decimal base0
,				System.Decimal base1
,				System.Decimal plus0
,				System.Decimal plus1
		)
		{
			this.CoachId = coachid;
			this.SkillName = skillname;
			this.TriggerCondition = triggercondition;
			this.CD = cd;
			this.TimeOfDuration = timeofduration;
			this.TriggerProbability = triggerprobability;
			this.Description = description;
			this.PlusDescription = plusdescription;
			this.Base0 = base0;
			this.Base1 = base1;
			this.Plus0 = plus0;
			this.Plus1 = plus1;
		}
		
		#region Public Properties
		
		///<summary>
		///CoachId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 CoachId {get ; set ;}

		///<summary>
		///SkillName
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SkillName {get ; set ;}

		///<summary>
		///TriggerCondition
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String TriggerCondition {get ; set ;}

		///<summary>
		///CD
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CD {get ; set ;}

		///<summary>
		///TimeOfDuration
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String TimeOfDuration {get ; set ;}

		///<summary>
		///TriggerProbability
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 TriggerProbability {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Description {get ; set ;}

		///<summary>
		///PlusDescription
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String PlusDescription {get ; set ;}

		///<summary>
		///Base0
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Decimal Base0 {get ; set ;}

		///<summary>
		///Base1
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Decimal Base1 {get ; set ;}

		///<summary>
		///Plus0
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Decimal Plus0 {get ; set ;}

		///<summary>
		///Plus1
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Decimal Plus1 {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCoachskillEntity Clone()
        {
            ConfigCoachskillEntity entity = new ConfigCoachskillEntity();
			entity.CoachId = this.CoachId;
			entity.SkillName = this.SkillName;
			entity.TriggerCondition = this.TriggerCondition;
			entity.CD = this.CD;
			entity.TimeOfDuration = this.TimeOfDuration;
			entity.TriggerProbability = this.TriggerProbability;
			entity.Description = this.Description;
			entity.PlusDescription = this.PlusDescription;
			entity.Base0 = this.Base0;
			entity.Base1 = this.Base1;
			entity.Plus0 = this.Plus0;
			entity.Plus1 = this.Plus1;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CoachSkill 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCoachskillResponse : BaseResponse<ConfigCoachskillEntity>
    {

    }
}
