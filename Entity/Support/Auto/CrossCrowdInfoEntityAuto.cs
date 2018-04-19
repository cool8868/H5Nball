
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_Info 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdInfoEntity
	{
		
		public CrosscrowdInfoEntity()
		{
		}

		public CrosscrowdInfoEntity(
		System.Int32 idx
,				System.DateTime starttime
,				System.DateTime endtime
,				System.Int32 domainid
,				System.Int32 playercount
,				System.Int32 paircount
,				System.Int32 prizepoint
,				System.Boolean issendkillprize
,				System.Boolean issendrankprize
,				System.Int32 status
,				System.DateTime rowtime
,				System.Int32 prizelegendcount
		)
		{
			this.Idx = idx;
			this.StartTime = starttime;
			this.EndTime = endtime;
			this.DomainId = domainid;
			this.PlayerCount = playercount;
			this.PairCount = paircount;
			this.PrizePoint = prizepoint;
			this.IsSendKillPrize = issendkillprize;
			this.IsSendRankPrize = issendrankprize;
			this.Status = status;
			this.RowTime = rowtime;
			this.PrizeLegendCount = prizelegendcount;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///StartTime
		///</summary>
        [DataMember]
        [ProtoMember(2)]
        [JsonIgnore]
		public System.DateTime StartTime {get ; set ;}

		///<summary>
		///EndTime
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime EndTime {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///参与人数
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 PlayerCount {get ; set ;}

		///<summary>
		///配对次数
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 PairCount {get ; set ;}

		///<summary>
		///PrizePoint
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 PrizePoint {get ; set ;}

		///<summary>
		///IsSendKillPrize
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Boolean IsSendKillPrize {get ; set ;}

		///<summary>
		///IsSendRankPrize
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Boolean IsSendRankPrize {get ; set ;}

		///<summary>
		///状态：0，初始；1，已开始；2，已结束
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(11)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///PrizeLegendCount
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 PrizeLegendCount {get ; set ;}
		#endregion
        
        #region Clone
        public CrosscrowdInfoEntity Clone()
        {
            CrosscrowdInfoEntity entity = new CrosscrowdInfoEntity();
			entity.Idx = this.Idx;
			entity.StartTime = this.StartTime;
			entity.EndTime = this.EndTime;
			entity.DomainId = this.DomainId;
			entity.PlayerCount = this.PlayerCount;
			entity.PairCount = this.PairCount;
			entity.PrizePoint = this.PrizePoint;
			entity.IsSendKillPrize = this.IsSendKillPrize;
			entity.IsSendRankPrize = this.IsSendRankPrize;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.PrizeLegendCount = this.PrizeLegendCount;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_Info 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdInfoResponse : BaseResponse<CrosscrowdInfoEntity>
    {

    }
}
