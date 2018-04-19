
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ManagerTalent 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicManagertalentEntity
	{
		
		public DicManagertalentEntity()
		{
		}

		public DicManagertalentEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.String skillname
,				System.Int32 skilllevel
,				System.Int32 stepno
,				System.Int32 sectno
,				System.Int32 driveflag
,				System.Int32 reqmanagerlevel
,				System.Boolean denyflag
,				System.String icon
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillName = skillname;
			this.SkillLevel = skilllevel;
			this.StepNo = stepno;
			this.SectNo = sectno;
			this.DriveFlag = driveflag;
			this.ReqManagerLevel = reqmanagerlevel;
			this.DenyFlag = denyflag;
			this.Icon = icon;
			this.Memo = memo;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///SkillId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 SkillId {get ; set ;}

		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///SkillName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SkillName {get ; set ;}

		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///阶段号
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 StepNo {get ; set ;}

		///<summary>
		///分支号
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SectNo {get ; set ;}

		///<summary>
		///被动标记
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 DriveFlag {get ; set ;}

		///<summary>
		///要求经理等级
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ReqManagerLevel {get ; set ;}

		///<summary>
		///失效标记
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Boolean DenyFlag {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicManagertalentEntity Clone()
        {
            DicManagertalentEntity entity = new DicManagertalentEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.SkillLevel = this.SkillLevel;
			entity.StepNo = this.StepNo;
			entity.SectNo = this.SectNo;
			entity.DriveFlag = this.DriveFlag;
			entity.ReqManagerLevel = this.ReqManagerLevel;
			entity.DenyFlag = this.DenyFlag;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ManagerTalent 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicManagertalentResponse : BaseResponse<DicManagertalentEntity>
    {

    }
}

