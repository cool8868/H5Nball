
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_ExchangeRecord 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueExchangerecordEntity
	{
		
		public LeagueExchangerecordEntity()
		{
		}

		public LeagueExchangerecordEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 itemcode
,				System.Int32 costscore
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ItemCode = itemcode;
			this.CostScore = costscore;
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
		///ItemCode
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 ItemCode {get ; set ;}

		///<summary>
		///CostScore
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CostScore {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(5)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueExchangerecordEntity Clone()
        {
            LeagueExchangerecordEntity entity = new LeagueExchangerecordEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ItemCode = this.ItemCode;
			entity.CostScore = this.CostScore;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_ExchangeRecord 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueExchangerecordResponse : BaseResponse<LeagueExchangerecordEntity>
    {

    }
}

