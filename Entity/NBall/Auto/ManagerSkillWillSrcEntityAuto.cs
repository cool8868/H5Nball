
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ManagerSkill_WillSrc 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ManagerskillWillsrcEntity
	{
		
		public ManagerskillWillsrcEntity()
		{
		}

		public ManagerskillWillsrcEntity(
		System.Int64 id
,				System.Guid managerid
,				System.String skillcode
,				System.String partmap
,				System.Int32 enableflag
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.Id = id;
			this.ManagerId = managerid;
			this.SkillCode = skillcode;
			this.PartMap = partmap;
			this.EnableFlag = enableflag;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///Id
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int64 Id {get ; set ;}

		///<summary>
		///经理Id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///SkillCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SkillCode {get ; set ;}

		///<summary>
		///球员组成
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String PartMap {get ; set ;}

		///<summary>
		///完成标记
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 EnableFlag {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public ManagerskillWillsrcEntity Clone()
        {
            ManagerskillWillsrcEntity entity = new ManagerskillWillsrcEntity();
			entity.Id = this.Id;
			entity.ManagerId = this.ManagerId;
			entity.SkillCode = this.SkillCode;
			entity.PartMap = this.PartMap;
			entity.EnableFlag = this.EnableFlag;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ManagerSkill_WillSrc 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ManagerskillWillsrcResponse : BaseResponse<ManagerskillWillsrcEntity>
    {

    }
}

