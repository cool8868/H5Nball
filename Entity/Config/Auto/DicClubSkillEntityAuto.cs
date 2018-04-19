
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_ClubSkill 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicClubskillEntity
	{
		
		public DicClubskillEntity()
		{
		}

		public DicClubskillEntity(
		System.Int32 skillid
,				System.String skillcode
,				System.Int32 skilllevel
,				System.String skillname
,				System.Int32 clubtype
,				System.String skillkey
,				System.Int32 skillvalue
,				System.String buffmemo
,				System.String icon
,				System.DateTime rowtime
		)
		{
			this.SkillId = skillid;
			this.SkillCode = skillcode;
			this.SkillLevel = skilllevel;
			this.SkillName = skillname;
			this.ClubType = clubtype;
			this.SkillKey = skillkey;
			this.SkillValue = skillvalue;
			this.BuffMemo = buffmemo;
			this.Icon = icon;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///SkillId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
        [JsonIgnore]
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
        [JsonIgnore]
		public System.Int32 SkillLevel {get ; set ;}

		///<summary>
		///SkillName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String SkillName {get ; set ;}

		///<summary>
		///ClubType
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 ClubType {get ; set ;}

		///<summary>
		///SkillKey
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String SkillKey {get ; set ;}

		///<summary>
		///SkillValue
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.Int32 SkillValue {get ; set ;}

		///<summary>
		///BuffMemo
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String BuffMemo {get ; set ;}

		///<summary>
		///Icon
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String Icon {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DicClubskillEntity Clone()
        {
            DicClubskillEntity entity = new DicClubskillEntity();
			entity.SkillId = this.SkillId;
			entity.SkillCode = this.SkillCode;
			entity.SkillLevel = this.SkillLevel;
			entity.SkillName = this.SkillName;
			entity.ClubType = this.ClubType;
			entity.SkillKey = this.SkillKey;
			entity.SkillValue = this.SkillValue;
			entity.BuffMemo = this.BuffMemo;
			entity.Icon = this.Icon;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_ClubSkill 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicClubskillResponse : BaseResponse<DicClubskillEntity>
    {

    }
}

