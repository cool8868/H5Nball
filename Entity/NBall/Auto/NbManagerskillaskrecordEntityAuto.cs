
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerSkillAskRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagerskillaskrecordEntity
	{
		
		public NbManagerskillaskrecordEntity()
		{
		}

		public NbManagerskillaskrecordEntity(
		System.Guid managerid
,				System.String skillmap
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.SkillMap = skillmap;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///技能背包
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SkillMap {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagerskillaskrecordEntity Clone()
        {
            NbManagerskillaskrecordEntity entity = new NbManagerskillaskrecordEntity();
			entity.ManagerId = this.ManagerId;
			entity.SkillMap = this.SkillMap;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerSkillAskRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagerskillaskrecordResponse : BaseResponse<NbManagerskillaskrecordEntity>
    {

    }
}

