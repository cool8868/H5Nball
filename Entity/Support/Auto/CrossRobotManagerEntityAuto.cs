
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossRobot_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossrobotManagerEntity
	{
		
		public CrossrobotManagerEntity()
		{
		}

		public CrossrobotManagerEntity(
		System.Guid idx
,				System.String siteid
,				System.Boolean crosscrowd
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.SiteId = siteid;
			this.CrossCrowd = crosscrowd;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///CrossCrowd
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Boolean CrossCrowd {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrossrobotManagerEntity Clone()
        {
            CrossrobotManagerEntity entity = new CrossrobotManagerEntity();
			entity.Idx = this.Idx;
			entity.SiteId = this.SiteId;
			entity.CrossCrowd = this.CrossCrowd;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossRobot_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossrobotManagerResponse : BaseResponse<CrossrobotManagerEntity>
    {

    }
}
