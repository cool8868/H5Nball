
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.Dic_StarArousalSkills 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DicStararousalskillsEntity
	{
		
		public DicStararousalskillsEntity()
		{
		}

		public DicStararousalskillsEntity(
		System.Int32 idx
,				System.Int32 playerid
,				System.Int32 arousallv
,				System.Int32 arousalskillid
,				System.String arousalskillcode
,				System.String rawskillcode
,				System.String skillname
,				System.String description
		)
		{
			this.Idx = idx;
			this.PlayerId = playerid;
			this.ArousalLv = arousallv;
			this.ArousalSkillId = arousalskillid;
			this.ArousalSkillCode = arousalskillcode;
			this.RawSkillCode = rawskillcode;
			this.SkillName = skillname;
			this.Description = description;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///PlayerId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 PlayerId {get ; set ;}

		///<summary>
		///ArousalLv
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ArousalLv {get ; set ;}

		///<summary>
		///ArousalSkillId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ArousalSkillId {get ; set ;}

		///<summary>
		///ArousalSkillCode
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String ArousalSkillCode {get ; set ;}

		///<summary>
		///RawSkillCode
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String RawSkillCode {get ; set ;}

		///<summary>
		///SkillName
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String SkillName {get ; set ;}

		///<summary>
		///Description
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String Description {get ; set ;}
		#endregion
        
        #region Clone
        public DicStararousalskillsEntity Clone()
        {
            DicStararousalskillsEntity entity = new DicStararousalskillsEntity();
			entity.Idx = this.Idx;
			entity.PlayerId = this.PlayerId;
			entity.ArousalLv = this.ArousalLv;
			entity.ArousalSkillId = this.ArousalSkillId;
			entity.ArousalSkillCode = this.ArousalSkillCode;
			entity.RawSkillCode = this.RawSkillCode;
			entity.SkillName = this.SkillName;
			entity.Description = this.Description;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.Dic_StarArousalSkills 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DicStararousalskillsResponse : BaseResponse<DicStararousalskillsEntity>
    {

    }
}

