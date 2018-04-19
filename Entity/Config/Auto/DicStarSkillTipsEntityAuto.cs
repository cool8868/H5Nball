
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_StarSkillTips 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicStarskilltipsEntity
	{
		
		public DicStarskilltipsEntity()
		{
		}

		public DicStarskilltipsEntity(
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
		///动作类型
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ActType {get ; set ;}

		///<summary>
		///动作类型描述
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ActTypeMemo {get ; set ;}

		///<summary>
		///球员Id
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Pid {get ; set ;}

		///<summary>
		///需要强化等级
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 ReqStrength {get ; set ;}

		///<summary>
		///触发动作
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String TriggerAction {get ; set ;}

		///<summary>
		///触发率
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
		///持续时间
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String LastTime {get ; set ;}

		///<summary>
		///效果描述
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
		///球星封印Code
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String PlusSkillCode {get ; set ;}

		///<summary>
		///封印名称
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String PlusSkillName {get ; set ;}

		///<summary>
		///封印效果
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String PlusSkillMemo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicStarskilltipsEntity Clone()
        {
            DicStarskilltipsEntity entity = new DicStarskilltipsEntity();
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
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_StarSkillTips 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicStarskilltipsResponse : BaseResponse<DicStarskilltipsEntity>
    {

    }
}

