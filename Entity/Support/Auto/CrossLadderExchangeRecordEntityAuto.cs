
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossLadder_ExchangeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossladderExchangerecordEntity
	{
		
		public CrossladderExchangerecordEntity()
		{
		}

		public CrossladderExchangerecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String siteid
,				System.Int32 itemcode
,				System.Int32 costhonor
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.SiteId = siteid;
			this.ItemCode = itemcode;
			this.CostHonor = costhonor;
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
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///CostHonor
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 CostHonor {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(6)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrossladderExchangerecordEntity Clone()
        {
            CrossladderExchangerecordEntity entity = new CrossladderExchangerecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.SiteId = this.SiteId;
			entity.ItemCode = this.ItemCode;
			entity.CostHonor = this.CostHonor;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossLadder_ExchangeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossladderExchangerecordResponse : BaseResponse<CrossladderExchangerecordEntity>
    {

    }
}
