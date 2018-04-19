
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ManagerTalentTips 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicManagertalenttipsEntity
	{
		
		public DicManagertalenttipsEntity()
		{
		}

		public DicManagertalenttipsEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.String skillname
,				System.String acttype
,				System.Int32 reqmanagerlevel
,				System.Int32 driveflag
,				System.String driveflagmemo
,				System.String lasttime
,				System.String buffmemo
,				System.String icon
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillName = skillname;
			this.ActType = acttype;
			this.ReqManagerLevel = reqmanagerlevel;
			this.DriveFlag = driveflag;
			this.DriveFlagMemo = driveflagmemo;
			this.LastTime = lasttime;
			this.BuffMemo = buffmemo;
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
		///动作类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String ActType {get ; set ;}

		///<summary>
		///要求经理等级
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ReqManagerLevel {get ; set ;}

		///<summary>
		///被动标记
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 DriveFlag {get ; set ;}

		///<summary>
		///被动标记描述
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String DriveFlagMemo {get ; set ;}

		///<summary>
		///持续时间
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String LastTime {get ; set ;}

		///<summary>
		///效果描述
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String BuffMemo {get ; set ;}

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
        public DicManagertalenttipsEntity Clone()
        {
            DicManagertalenttipsEntity entity = new DicManagertalenttipsEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.ActType = this.ActType;
			entity.ReqManagerLevel = this.ReqManagerLevel;
			entity.DriveFlag = this.DriveFlag;
			entity.DriveFlagMemo = this.DriveFlagMemo;
			entity.LastTime = this.LastTime;
			entity.BuffMemo = this.BuffMemo;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ManagerTalentTips 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicManagertalenttipsResponse : BaseResponse<DicManagertalenttipsEntity>
    {

    }
}

