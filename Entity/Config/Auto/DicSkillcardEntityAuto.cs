
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_SkillCard 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSkillcardEntity
	{
		
		public DicSkillcardEntity()
		{
		}

		public DicSkillcardEntity(
		System.String skillcode
,				System.String itemname
,				System.String itemicon
,				System.Int32 skillclass
,				System.String skillroot
,				System.Int32 skilllevel
,				System.Int32 getlevel
,				System.Int32 acttype
,				System.Int32 mixexp
,				System.Decimal mixdiscount
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.SkillCode = skillcode;
			this.ItemName = itemname;
			this.ItemIcon = itemicon;
			this.SkillClass = skillclass;
			this.SkillRoot = skillroot;
			this.SkillLevel = skilllevel;
			this.GetLevel = getlevel;
			this.ActType = acttype;
			this.MixExp = mixexp;
			this.MixDiscount = mixdiscount;
			this.Memo = memo;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///名称
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String ItemName {get ; set ;}

		///<summary>
		///ItemIcon
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ItemIcon {get ; set ;}

		///<summary>
		///品质
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 SkillClass {get ; set ;}

		///<summary>
		///SkillRoot
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String SkillRoot {get ; set ;}

		///<summary>
		///等级
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///学习所需经理等级
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 GetLevel {get ; set ;}

		///<summary>
		///动作
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 ActType {get ; set ;}

		///<summary>
		///经验
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 MixExp {get ; set ;}

		///<summary>
		///合并非同名的经验折扣
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Decimal MixDiscount {get ; set ;}

		///<summary>
		///描述
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
        public DicSkillcardEntity Clone()
        {
            DicSkillcardEntity entity = new DicSkillcardEntity();
			entity.SkillCode = this.SkillCode;
			entity.ItemName = this.ItemName;
			entity.ItemIcon = this.ItemIcon;
			entity.SkillClass = this.SkillClass;
			entity.SkillRoot = this.SkillRoot;
			entity.SkillLevel = this.SkillLevel;
			entity.GetLevel = this.GetLevel;
			entity.ActType = this.ActType;
			entity.MixExp = this.MixExp;
			entity.MixDiscount = this.MixDiscount;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_SkillCard 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSkillcardResponse : BaseResponse<DicSkillcardEntity>
    {

    }
}

