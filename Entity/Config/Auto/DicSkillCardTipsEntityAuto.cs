
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_SkillCardTips 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSkillcardtipsEntity
	{
		
		public DicSkillcardtipsEntity()
		{
		}

		public DicSkillcardtipsEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.String skillname
,				System.Int32 acttype
,				System.String acttypememo
,				System.Int32 skillclass
,				System.String skillclassmemo
,				System.String skillroot
,				System.Int32 skilllevel
,				System.Int32 getlevel
,				System.Int32 maxexp
,				System.Decimal mixdiscount
,				System.String triggeraction
,				System.String triggerrate
,				System.String cd
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
			this.ActTypeMemo = acttypememo;
			this.SkillClass = skillclass;
			this.SkillClassMemo = skillclassmemo;
			this.SkillRoot = skillroot;
			this.SkillLevel = skilllevel;
			this.GetLevel = getlevel;
			this.MaxExp = maxexp;
			this.MixDiscount = mixdiscount;
			this.TriggerAction = triggeraction;
			this.TriggerRate = triggerrate;
			this.CD = cd;
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
		public System.Int32 ActType {get ; set ;}

		///<summary>
		///动作类型描述
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ActTypeMemo {get ; set ;}

		///<summary>
		///品质
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SkillClass {get ; set ;}

		///<summary>
		///品质描述
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String SkillClassMemo {get ; set ;}

		///<summary>
		///同名技能，用来判断合并时是否折扣经验
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String SkillRoot {get ; set ;}

		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///学习所需经理等级
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 GetLevel {get ; set ;}

		///<summary>
		///经验上限
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 MaxExp {get ; set ;}

		///<summary>
		///合并非同名的经验折扣
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Decimal MixDiscount {get ; set ;}

		///<summary>
		///触发动作
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.String TriggerAction {get ; set ;}

		///<summary>
		///触发率
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.String TriggerRate {get ; set ;}

		///<summary>
		///CD
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.String CD {get ; set ;}

		///<summary>
		///持续时间
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.String LastTime {get ; set ;}

		///<summary>
		///效果描述
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.String Icon {get ; set ;}

		///<summary>
		///Memo
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.String Memo {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicSkillcardtipsEntity Clone()
        {
            DicSkillcardtipsEntity entity = new DicSkillcardtipsEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.ActType = this.ActType;
			entity.ActTypeMemo = this.ActTypeMemo;
			entity.SkillClass = this.SkillClass;
			entity.SkillClassMemo = this.SkillClassMemo;
			entity.SkillRoot = this.SkillRoot;
			entity.SkillLevel = this.SkillLevel;
			entity.GetLevel = this.GetLevel;
			entity.MaxExp = this.MaxExp;
			entity.MixDiscount = this.MixDiscount;
			entity.TriggerAction = this.TriggerAction;
			entity.TriggerRate = this.TriggerRate;
			entity.CD = this.CD;
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
	/// 对Table dbo.Dic_SkillCardTips 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSkillcardtipsResponse : BaseResponse<DicSkillcardtipsEntity>
    {

    }
}

