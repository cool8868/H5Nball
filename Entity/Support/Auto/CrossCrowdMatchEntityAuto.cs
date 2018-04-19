
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_Match 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdMatchEntity
	{
		
		public CrosscrowdMatchEntity()
		{
		}

		public CrosscrowdMatchEntity(
		System.Guid idx
,				System.Int32 crosscrowdid
,				System.Int32 pairindex
,				System.String homesiteid
,				System.String awaysiteid
,				System.Guid homeid
,				System.Guid awayid
,				System.String homename
,				System.String awayname
,				System.Int32 homescore
,				System.Int32 awayscore
,				System.Int32 homeprizecoin
,				System.Int32 homeprizehonor
,				System.Int32 homemorale
,				System.Int32 homecostmorale
,				System.Int32 homeprizescore
,				System.Int32 awayprizecoin
,				System.Int32 awayprizehonor
,				System.Int32 awaymorale
,				System.Int32 awaycostmorale
,				System.Int32 awayprizescore
,				System.Boolean iskill
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.CrossCrowdId = crosscrowdid;
			this.PairIndex = pairindex;
			this.HomeSiteId = homesiteid;
			this.AwaySiteId = awaysiteid;
			this.HomeId = homeid;
			this.AwayId = awayid;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.HomeScore = homescore;
			this.AwayScore = awayscore;
			this.HomePrizeCoin = homeprizecoin;
			this.HomePrizeHonor = homeprizehonor;
			this.HomeMorale = homemorale;
			this.HomeCostMorale = homecostmorale;
			this.HomePrizeScore = homeprizescore;
			this.AwayPrizeCoin = awayprizecoin;
			this.AwayPrizeHonor = awayprizehonor;
			this.AwayMorale = awaymorale;
			this.AwayCostMorale = awaycostmorale;
			this.AwayPrizeScore = awayprizescore;
			this.IsKill = iskill;
			this.Status = status;
			this.RowTime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///CrossCrowdId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 CrossCrowdId {get ; set ;}

		///<summary>
		///PairIndex
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 PairIndex {get ; set ;}

		///<summary>
		///HomeSiteId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String HomeSiteId {get ; set ;}

		///<summary>
		///AwaySiteId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String AwaySiteId {get ; set ;}

		///<summary>
		///HomeId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Guid HomeId {get ; set ;}

		///<summary>
		///AwayId
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Guid AwayId {get ; set ;}

		///<summary>
		///HomeName
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///AwayName
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///HomeScore
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 HomeScore {get ; set ;}

		///<summary>
		///AwayScore
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 AwayScore {get ; set ;}

		///<summary>
		///HomePrizeCoin
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 HomePrizeCoin {get ; set ;}

		///<summary>
		///HomePrizeHonor
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 HomePrizeHonor {get ; set ;}

		///<summary>
		///HomeMorale
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 HomeMorale {get ; set ;}

		///<summary>
		///HomeCostMorale
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 HomeCostMorale {get ; set ;}

		///<summary>
		///HomePrizeScore
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 HomePrizeScore {get ; set ;}

		///<summary>
		///AwayPrizeCoin
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 AwayPrizeCoin {get ; set ;}

		///<summary>
		///AwayPrizeHonor
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 AwayPrizeHonor {get ; set ;}

		///<summary>
		///AwayMorale
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 AwayMorale {get ; set ;}

		///<summary>
		///AwayCostMorale
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 AwayCostMorale {get ; set ;}

		///<summary>
		///AwayPrizeScore
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 AwayPrizeScore {get ; set ;}

		///<summary>
		///IsKill
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Boolean IsKill {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(24)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrosscrowdMatchEntity Clone()
        {
            CrosscrowdMatchEntity entity = new CrosscrowdMatchEntity();
			entity.Idx = this.Idx;
			entity.CrossCrowdId = this.CrossCrowdId;
			entity.PairIndex = this.PairIndex;
			entity.HomeSiteId = this.HomeSiteId;
			entity.AwaySiteId = this.AwaySiteId;
			entity.HomeId = this.HomeId;
			entity.AwayId = this.AwayId;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.HomeScore = this.HomeScore;
			entity.AwayScore = this.AwayScore;
			entity.HomePrizeCoin = this.HomePrizeCoin;
			entity.HomePrizeHonor = this.HomePrizeHonor;
			entity.HomeMorale = this.HomeMorale;
			entity.HomeCostMorale = this.HomeCostMorale;
			entity.HomePrizeScore = this.HomePrizeScore;
			entity.AwayPrizeCoin = this.AwayPrizeCoin;
			entity.AwayPrizeHonor = this.AwayPrizeHonor;
			entity.AwayMorale = this.AwayMorale;
			entity.AwayCostMorale = this.AwayCostMorale;
			entity.AwayPrizeScore = this.AwayPrizeScore;
			entity.IsKill = this.IsKill;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_Match 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdMatchResponse : BaseResponse<CrosscrowdMatchEntity>
    {

    }
}
