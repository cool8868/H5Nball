
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_MaxKillerRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdMaxkillerrecordEntity
	{
		
		public CrosscrowdMaxkillerrecordEntity()
		{
		}

		public CrosscrowdMaxkillerrecordEntity(
		System.Int32 idx
,				System.Int32 crosscrowdid
,				System.String siteid
,				System.Guid managerid
,				System.String prizeitems
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.CrossCrowdId = crosscrowdid;
			this.SiteId = siteid;
			this.ManagerId = managerid;
			this.PrizeItems = prizeitems;
			this.Status = status;
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
		///CrossCrowdId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CrossCrowdId {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///PrizeItems
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String PrizeItems {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrosscrowdMaxkillerrecordEntity Clone()
        {
            CrosscrowdMaxkillerrecordEntity entity = new CrosscrowdMaxkillerrecordEntity();
			entity.Idx = this.Idx;
			entity.CrossCrowdId = this.CrossCrowdId;
			entity.SiteId = this.SiteId;
			entity.ManagerId = this.ManagerId;
			entity.PrizeItems = this.PrizeItems;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_MaxKillerRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdMaxkillerrecordResponse : BaseResponse<CrosscrowdMaxkillerrecordEntity>
    {

    }
}
