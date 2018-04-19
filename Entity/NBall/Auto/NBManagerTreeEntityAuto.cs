
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.NB_ManagerTree 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class NbManagertreeEntity
	{
		
		public NbManagertreeEntity()
		{
		}

		public NbManagertreeEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String skillcode
,				System.Int32 points
,				System.Int32 status
,				System.DateTime updatetime
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.SkillCode = skillcode;
			this.Points = points;
			this.Status = status;
			this.UpdateTime = updatetime;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///ManagerId
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
		///Points
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Points {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public NbManagertreeEntity Clone()
        {
            NbManagertreeEntity entity = new NbManagertreeEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.SkillCode = this.SkillCode;
			entity.Points = this.Points;
			entity.Status = this.Status;
			entity.UpdateTime = this.UpdateTime;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.NB_ManagerTree 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class NbManagertreeResponse : BaseResponse<NbManagertreeEntity>
    {

    }
}

