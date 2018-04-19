
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Config_CoachInfo 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ConfigCoachinfoEntity
	{
		
		public ConfigCoachinfoEntity()
		{
		}

		public ConfigCoachinfoEntity(
		System.Int32 idx
,				System.String name
,				System.Int32 offensive
,				System.Int32 organizational
,				System.Int32 defense
,				System.Int32 bodyattr
,				System.Int32 goalkeeping
,				System.Boolean isskill
,				System.Int32 skillid
,				System.Int32 debriscode
		)
		{
			this.Idx = idx;
			this.Name = name;
			this.Offensive = offensive;
			this.Organizational = organizational;
			this.Defense = defense;
			this.BodyAttr = bodyattr;
			this.Goalkeeping = goalkeeping;
			this.IsSkill = isskill;
			this.SkillId = skillid;
			this.DebrisCode = debriscode;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String Name {get ; set ;}

		///<summary>
		///Offensive
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 Offensive {get ; set ;}

		///<summary>
		///Organizational
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Organizational {get ; set ;}

		///<summary>
		///Defense
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Defense {get ; set ;}

		///<summary>
		///BodyAttr
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 BodyAttr {get ; set ;}

		///<summary>
		///Goalkeeping
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Goalkeeping {get ; set ;}

		///<summary>
		///IsSkill
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsSkill {get ; set ;}

		///<summary>
		///SkillId
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 SkillId {get ; set ;}

		///<summary>
		///DebrisCode
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 DebrisCode {get ; set ;}
		#endregion
        
        #region Clone
        public ConfigCoachinfoEntity Clone()
        {
            ConfigCoachinfoEntity entity = new ConfigCoachinfoEntity();
			entity.Idx = this.Idx;
			entity.Name = this.Name;
			entity.Offensive = this.Offensive;
			entity.Organizational = this.Organizational;
			entity.Defense = this.Defense;
			entity.BodyAttr = this.BodyAttr;
			entity.Goalkeeping = this.Goalkeeping;
			entity.IsSkill = this.IsSkill;
			entity.SkillId = this.SkillId;
			entity.DebrisCode = this.DebrisCode;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Config_CoachInfo 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ConfigCoachinfoResponse : BaseResponse<ConfigCoachinfoEntity>
    {

    }
}
