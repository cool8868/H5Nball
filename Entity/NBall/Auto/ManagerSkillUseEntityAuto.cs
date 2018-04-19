
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ManagerSkill_Use 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ManagerskillUseEntity
	{
		
		public ManagerskillUseEntity()
		{
		}

		public ManagerskillUseEntity(
		System.Guid managerid
,				System.Int32 syncflag
,				System.String playerskills
,				System.String managerskills
,				System.String coachskill
,				System.String talents
,				System.String wills
,				System.String combs
,				System.String suits
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.ManagerId = managerid;
			this.SyncFlag = syncflag;
			this.PlayerSkills = playerskills;
			this.ManagerSkills = managerskills;
			this.CoachSkill = coachskill;
			this.Talents = talents;
			this.Wills = wills;
			this.Combs = combs;
			this.Suits = suits;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///SyncFlag
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 SyncFlag {get ; set ;}

		///<summary>
		///球员技能
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String PlayerSkills {get ; set ;}

		///<summary>
		///经理技能
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ManagerSkills {get ; set ;}

		///<summary>
		///教练技能
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String CoachSkill {get ; set ;}

		///<summary>
		///主动天赋
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Talents {get ; set ;}

		///<summary>
		///主动意志
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String Wills {get ; set ;}

		///<summary>
		///组合
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Combs {get ; set ;}

		///<summary>
		///套装
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String Suits {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public ManagerskillUseEntity Clone()
        {
            ManagerskillUseEntity entity = new ManagerskillUseEntity();
			entity.ManagerId = this.ManagerId;
			entity.SyncFlag = this.SyncFlag;
			entity.PlayerSkills = this.PlayerSkills;
			entity.ManagerSkills = this.ManagerSkills;
			entity.CoachSkill = this.CoachSkill;
			entity.Talents = this.Talents;
			entity.Wills = this.Wills;
			entity.Combs = this.Combs;
			entity.Suits = this.Suits;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ManagerSkill_Use 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ManagerskillUseResponse : BaseResponse<ManagerskillUseEntity>
    {

    }
}

