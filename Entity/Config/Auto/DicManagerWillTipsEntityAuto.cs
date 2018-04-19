
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ManagerWillTips 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicManagerwilltipsEntity
	{
		
		public DicManagerwilltipsEntity()
		{
		}

		public DicManagerwilltipsEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.String skillname
,				System.Int32 willrank
,				System.String acttype
,				System.Int32 driveflag
,				System.String driveflagmemo
,				System.String buffmemo
,				System.Decimal buffarg
,				System.Decimal buffarg2
,				System.String icon
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillName = skillname;
			this.WillRank = willrank;
			this.ActType = acttype;
			this.DriveFlag = driveflag;
			this.DriveFlagMemo = driveflagmemo;
			this.BuffMemo = buffmemo;
			this.BuffArg = buffarg;
			this.BuffArg2 = buffarg2;
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
		///意志分级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 WillRank {get ; set ;}

		///<summary>
		///动作类型
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ActType {get ; set ;}

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
		///效果描述
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///BuffArg
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Decimal BuffArg {get ; set ;}

		///<summary>
		///BuffArg2
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Decimal BuffArg2 {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicManagerwilltipsEntity Clone()
        {
            DicManagerwilltipsEntity entity = new DicManagerwilltipsEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.WillRank = this.WillRank;
			entity.ActType = this.ActType;
			entity.DriveFlag = this.DriveFlag;
			entity.DriveFlagMemo = this.DriveFlagMemo;
			entity.BuffMemo = this.BuffMemo;
			entity.BuffArg = this.BuffArg;
			entity.BuffArg2 = this.BuffArg2;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ManagerWillTips 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicManagerwilltipsResponse : BaseResponse<DicManagerwilltipsEntity>
    {

    }
}

