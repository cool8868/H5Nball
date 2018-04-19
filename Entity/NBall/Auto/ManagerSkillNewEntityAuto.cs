
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.ManagerSkill_New 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class ManagerskillNewEntity
	{
		
		public ManagerskillNewEntity()
		{
		}

		public ManagerskillNewEntity(
		System.Guid managerid
,				System.String newskills
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.ManagerId = managerid;
			this.NewSkills = newskills;
			this.UpdateTime = updatetime;
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
		///NewSkills
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String NewSkills {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(4)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public ManagerskillNewEntity Clone()
        {
            ManagerskillNewEntity entity = new ManagerskillNewEntity();
			entity.ManagerId = this.ManagerId;
			entity.NewSkills = this.NewSkills;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.ManagerSkill_New 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class ManagerskillNewResponse : BaseResponse<ManagerskillNewEntity>
    {

    }
}

