
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ManagerWill 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicManagerwillEntity
	{
		
		public DicManagerwillEntity()
		{
		}

		public DicManagerwillEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.String skillname
,				System.Int32 willrank
,				System.Int32 driveflag
,				System.String partmap
,				System.String combskillcode
,				System.Int32 maxcomblevel
,				System.String buffmemo
,				System.Decimal buffarg
,				System.Decimal buffarg2
,				System.Int32 sortno
,				System.Boolean denyflag
,				System.String icon
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillName = skillname;
			this.WillRank = willrank;
			this.DriveFlag = driveflag;
			this.PartMap = partmap;
			this.CombSkillCode = combskillcode;
			this.MaxCombLevel = maxcomblevel;
			this.BuffMemo = buffmemo;
			this.BuffArg = buffarg;
			this.BuffArg2 = buffarg2;
			this.SortNo = sortno;
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
		///意志分级
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 WillRank {get ; set ;}

		///<summary>
		///被动标记
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 DriveFlag {get ; set ;}

		///<summary>
		///球员组成
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PartMap {get ; set ;}

		///<summary>
		///组合技能
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String CombSkillCode {get ; set ;}

		///<summary>
		///组合等级
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 MaxCombLevel {get ; set ;}

		///<summary>
		///BuffMemo
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///BuffArg
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Decimal BuffArg {get ; set ;}

		///<summary>
		///BuffArg2
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Decimal BuffArg2 {get ; set ;}

		///<summary>
		///SortNo
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 SortNo {get ; set ;}

		///<summary>
		///失效标记
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Boolean DenyFlag {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicManagerwillEntity Clone()
        {
            DicManagerwillEntity entity = new DicManagerwillEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.WillRank = this.WillRank;
			entity.DriveFlag = this.DriveFlag;
			entity.PartMap = this.PartMap;
			entity.CombSkillCode = this.CombSkillCode;
			entity.MaxCombLevel = this.MaxCombLevel;
			entity.BuffMemo = this.BuffMemo;
			entity.BuffArg = this.BuffArg;
			entity.BuffArg2 = this.BuffArg2;
			entity.SortNo = this.SortNo;
			entity.DenyFlag = this.DenyFlag;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ManagerWill 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicManagerwillResponse : BaseResponse<DicManagerwillEntity>
    {

    }
}

