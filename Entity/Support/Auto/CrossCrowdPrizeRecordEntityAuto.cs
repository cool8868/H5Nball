
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_PrizeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdPrizerecordEntity
	{
		
		public CrosscrowdPrizerecordEntity()
		{
		}

		public CrosscrowdPrizerecordEntity(
		System.Int32 idx
,				System.Int32 category
,				System.String siteid
,				System.Guid managerid
,				System.String source
,				System.String prizeitems
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.Category = category;
			this.SiteId = siteid;
			this.ManagerId = managerid;
			this.Source = source;
			this.PrizeItems = prizeitems;
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
		///Category
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 Category {get ; set ;}

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
		///Source
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Source {get ; set ;}

		///<summary>
		///PrizeItems
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String PrizeItems {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrosscrowdPrizerecordEntity Clone()
        {
            CrosscrowdPrizerecordEntity entity = new CrosscrowdPrizerecordEntity();
			entity.Idx = this.Idx;
			entity.Category = this.Category;
			entity.SiteId = this.SiteId;
			entity.ManagerId = this.ManagerId;
			entity.Source = this.Source;
			entity.PrizeItems = this.PrizeItems;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_PrizeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdPrizerecordResponse : BaseResponse<CrosscrowdPrizerecordEntity>
    {

    }
}
