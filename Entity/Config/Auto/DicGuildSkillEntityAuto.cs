
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_GuildSkill 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicGuildskillEntity
	{
		
		public DicGuildskillEntity()
		{
		}

		public DicGuildskillEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.String skillname
,				System.Int32 maxlevel
,				System.Int32 reqguildlevel
,				System.String buffmemo
,				System.Int32 basevalue
,				System.Int32 plusvalue
,				System.String bufflastmemo
,				System.Int32 buycostactive
,				System.String icon
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillName = skillname;
			this.MaxLevel = maxlevel;
			this.ReqGuildLevel = reqguildlevel;
			this.BuffMemo = buffmemo;
			this.BaseValue = basevalue;
			this.PlusValue = plusvalue;
			this.BuffLastMemo = bufflastmemo;
			this.BuyCostActive = buycostactive;
			this.Icon = icon;
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
		///MaxLevel
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 MaxLevel {get ; set ;}

		///<summary>
		///ReqGuildLevel
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ReqGuildLevel {get ; set ;}

		///<summary>
		///BuffMemo
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///BaseValue
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 BaseValue {get ; set ;}

		///<summary>
		///PlusValue
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 PlusValue {get ; set ;}

		///<summary>
		///BuffLastMemo
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String BuffLastMemo {get ; set ;}

		///<summary>
		///BuyCostActive
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 BuyCostActive {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String Icon {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicGuildskillEntity Clone()
        {
            DicGuildskillEntity entity = new DicGuildskillEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillName = this.SkillName;
			entity.MaxLevel = this.MaxLevel;
			entity.ReqGuildLevel = this.ReqGuildLevel;
			entity.BuffMemo = this.BuffMemo;
			entity.BaseValue = this.BaseValue;
			entity.PlusValue = this.PlusValue;
			entity.BuffLastMemo = this.BuffLastMemo;
			entity.BuyCostActive = this.BuyCostActive;
			entity.Icon = this.Icon;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_GuildSkill 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicGuildskillResponse : BaseResponse<DicGuildskillEntity>
    {

    }
}

