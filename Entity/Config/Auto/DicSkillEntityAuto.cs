
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_Skill 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicSkillEntity
	{
		
		public DicSkillEntity()
		{
		}

		public DicSkillEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.Int32 skilllevel
,				System.String skillname
,				System.Int32 buffsrctype
,				System.String reftype
,				System.String refkey
,				System.String refflag
,				System.Int32 skilltype
,				System.Int32 poolflag
,				System.Int32 liveflag
,				System.String memo
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillLevel = skilllevel;
			this.SkillName = skillname;
			this.BuffSrcType = buffsrctype;
			this.RefType = reftype;
			this.RefKey = refkey;
			this.RefFlag = refflag;
			this.SkillType = skilltype;
			this.PoolFlag = poolflag;
			this.LiveFlag = liveflag;
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
		///SkillLevel
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///SkillName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String SkillName {get ; set ;}

		///<summary>
		///BuffSrcType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 BuffSrcType {get ; set ;}

		///<summary>
		///RefType
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String RefType {get ; set ;}

		///<summary>
		///RefKey
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String RefKey {get ; set ;}

		///<summary>
		///RefFlag
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String RefFlag {get ; set ;}

		///<summary>
		///SkillType
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 SkillType {get ; set ;}

		///<summary>
		///PoolFlag
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 PoolFlag {get ; set ;}

		///<summary>
		///LiveFlag
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 LiveFlag {get ; set ;}

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
        public DicSkillEntity Clone()
        {
            DicSkillEntity entity = new DicSkillEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillLevel = this.SkillLevel;
			entity.SkillName = this.SkillName;
			entity.BuffSrcType = this.BuffSrcType;
			entity.RefType = this.RefType;
			entity.RefKey = this.RefKey;
			entity.RefFlag = this.RefFlag;
			entity.SkillType = this.SkillType;
			entity.PoolFlag = this.PoolFlag;
			entity.LiveFlag = this.LiveFlag;
			entity.Memo = this.Memo;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_Skill 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicSkillResponse : BaseResponse<DicSkillEntity>
    {

    }
}

