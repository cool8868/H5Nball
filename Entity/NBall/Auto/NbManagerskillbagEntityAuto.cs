
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerSkillBag 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerskillbagEntity
	{
		
		public NbManagerskillbagEntity()
		{
		}

		public NbManagerskillbagEntity(
		System.Guid managerid
,				System.String setskills
,				System.String setmap
,				System.DateTime rowtime
,				System.Byte[] rowversion
		)
		{
			this.ManagerId = managerid;
			this.SetSkills = setskills;
			this.SetMap = setmap;
			this.RowTime = rowtime;
			this.RowVersion = rowversion;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///已设置的技能
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SetSkills {get ; set ;}

		///<summary>
		///已学习的技能
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SetMap {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerskillbagEntity Clone()
        {
            NbManagerskillbagEntity entity = new NbManagerskillbagEntity();
			entity.ManagerId = this.ManagerId;
			entity.SetSkills = this.SetSkills;
			entity.SetMap = this.SetMap;
			entity.RowTime = this.RowTime;
			entity.RowVersion = this.RowVersion;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerSkillBag 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerskillbagResponse : BaseResponse<NbManagerskillbagEntity>
    {

    }
}

