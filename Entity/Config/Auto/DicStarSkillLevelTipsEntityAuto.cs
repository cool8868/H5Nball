
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_StarSkillLevelTips 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicStarskillleveltipsEntity
	{
		
		public DicStarskillleveltipsEntity()
		{
		}

		public DicStarskillleveltipsEntity(
		System.Int32 skillid
,				System.String starskillcode
,				System.String starskillname
,				System.Int32 acttype
,				System.String acttypememo
,				System.Int32 pid
,				System.Int32 reqstrength
,				System.String triggeraction
,				System.String triggerrate
,				System.String cd
,				System.String lasttime
,				System.String buffmemo
,				System.String icon
,				System.String memo
,				System.String plusskillcode
,				System.String plusskillname
,				System.String plusskillmemo
,				System.DateTime rowtime
,				System.Decimal origval
,				System.Decimal stepval
,				System.Decimal origval2
,				System.Decimal stepval2
,				System.Decimal origval3
,				System.Decimal stepval3
		)
		{
			this.SkillId = skillid;
			this.StarSkillCode = starskillcode;
			this.StarSkillName = starskillname;
			this.ActType = acttype;
			this.ActTypeMemo = acttypememo;
			this.Pid = pid;
			this.ReqStrength = reqstrength;
			this.TriggerAction = triggeraction;
			this.TriggerRate = triggerrate;
			this.CD = cd;
			this.LastTime = lasttime;
			this.BuffMemo = buffmemo;
			this.Icon = icon;
			this.Memo = memo;
			this.PlusSkillCode = plusskillcode;
			this.PlusSkillName = plusskillname;
			this.PlusSkillMemo = plusskillmemo;
			this.RowTime = rowtime;
			this.OrigVal = origval;
			this.StepVal = stepval;
			this.OrigVal2 = origval2;
			this.StepVal2 = stepval2;
			this.OrigVal3 = origval3;
			this.StepVal3 = stepval3;
		}
		
		#region Public Properties
		
		///<summary>
		///SkillId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 SkillId {get ; set ;}

		///<summary>
		///StarSkillCode
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String StarSkillCode {get ; set ;}

		///<summary>
		///StarSkillName
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String StarSkillName {get ; set ;}

		///<summary>
		///ActType
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActType {get ; set ;}

		///<summary>
		///ActTypeMemo
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ActTypeMemo {get ; set ;}

		///<summary>
		///Pid
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Pid {get ; set ;}

		///<summary>
		///ReqStrength
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ReqStrength {get ; set ;}

		///<summary>
		///TriggerAction
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String TriggerAction {get ; set ;}

		///<summary>
		///TriggerRate
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String TriggerRate {get ; set ;}

		///<summary>
		///CD
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String CD {get ; set ;}

		///<summary>
		///LastTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String LastTime {get ; set ;}

		///<summary>
		///BuffMemo
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String Memo {get ; set ;}

		///<summary>
		///PlusSkillCode
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.String PlusSkillCode {get ; set ;}

		///<summary>
		///PlusSkillName
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.String PlusSkillName {get ; set ;}

		///<summary>
		///PlusSkillMemo
		///</summary>
        [DataMember]
        [ProtoMember(17)]
        [JsonIgnore]
		public System.String PlusSkillMemo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///OrigVal
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Decimal OrigVal {get ; set ;}

		///<summary>
		///StepVal
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Decimal StepVal {get ; set ;}

		///<summary>
		///OrigVal2
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Decimal OrigVal2 {get ; set ;}

		///<summary>
		///StepVal2
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Decimal StepVal2 {get ; set ;}

		///<summary>
		///OrigVal3
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Decimal OrigVal3 {get ; set ;}

		///<summary>
		///StepVal3
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Decimal StepVal3 {get ; set ;}
		#endregion
        
        #region Clone
        public DicStarskillleveltipsEntity Clone()
        {
            DicStarskillleveltipsEntity entity = new DicStarskillleveltipsEntity();
			entity.SkillId = this.SkillId;
			entity.StarSkillCode = this.StarSkillCode;
			entity.StarSkillName = this.StarSkillName;
			entity.ActType = this.ActType;
			entity.ActTypeMemo = this.ActTypeMemo;
			entity.Pid = this.Pid;
			entity.ReqStrength = this.ReqStrength;
			entity.TriggerAction = this.TriggerAction;
			entity.TriggerRate = this.TriggerRate;
			entity.CD = this.CD;
			entity.LastTime = this.LastTime;
			entity.BuffMemo = this.BuffMemo;
			entity.Icon = this.Icon;
			entity.Memo = this.Memo;
			entity.PlusSkillCode = this.PlusSkillCode;
			entity.PlusSkillName = this.PlusSkillName;
			entity.PlusSkillMemo = this.PlusSkillMemo;
			entity.RowTime = this.RowTime;
			entity.OrigVal = this.OrigVal;
			entity.StepVal = this.StepVal;
			entity.OrigVal2 = this.OrigVal2;
			entity.StepVal2 = this.StepVal2;
			entity.OrigVal3 = this.OrigVal3;
			entity.StepVal3 = this.StepVal3;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_StarSkillLevelTips 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicStarskillleveltipsResponse : BaseResponse<DicStarskillleveltipsEntity>
    {

    }
}

