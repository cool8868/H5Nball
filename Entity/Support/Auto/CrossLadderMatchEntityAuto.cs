
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossLadder_Match 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossladderMatchEntity
	{
		
		public CrossladderMatchEntity()
		{
		}

		public CrossladderMatchEntity(
		System.Guid idx
,				System.Int32 domainid
,				System.Guid ladderid
,				System.Guid homeid
,				System.Guid awayid
,				System.String homename
,				System.String awayname
,				System.String homelogo
,				System.String awaylogo
,				System.String homesiteid
,				System.String awaysiteid
,				System.Int32 homeladderscore
,				System.Int32 awayladderscore
,				System.Int32 homescore
,				System.Int32 awayscore
,				System.Boolean homeisbot
,				System.Boolean awayisbot
,				System.Int32 groupindex
,				System.Int32 prizehomescore
,				System.Int32 prizeawayscore
,				System.Int32 status
,				System.DateTime rowtime
,				System.Int32 homecoin
,				System.Int32 homeexp
,				System.Int32 awaycoin
,				System.Int32 awayexp
		)
		{
			this.Idx = idx;
			this.DomainId = domainid;
			this.LadderId = ladderid;
			this.HomeId = homeid;
			this.AwayId = awayid;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.HomeLogo = homelogo;
			this.AwayLogo = awaylogo;
			this.HomeSiteId = homesiteid;
			this.AwaySiteId = awaysiteid;
			this.HomeLadderScore = homeladderscore;
			this.AwayLadderScore = awayladderscore;
			this.HomeScore = homescore;
			this.AwayScore = awayscore;
			this.HomeIsBot = homeisbot;
			this.AwayIsBot = awayisbot;
			this.GroupIndex = groupindex;
			this.PrizeHomeScore = prizehomescore;
			this.PrizeAwayScore = prizeawayscore;
			this.Status = status;
			this.RowTime = rowtime;
			this.HomeCoin = homecoin;
			this.HomeExp = homeexp;
			this.AwayCoin = awaycoin;
			this.AwayExp = awayexp;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid Idx {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///天梯id
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid LadderId {get ; set ;}

		///<summary>
		///主队经理
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid HomeId {get ; set ;}

		///<summary>
		///客队经理
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Guid AwayId {get ; set ;}

		///<summary>
		///主队名
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///客队名
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///HomeLogo
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.String HomeLogo {get ; set ;}

		///<summary>
		///AwayLogo
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String AwayLogo {get ; set ;}

		///<summary>
		///HomeSiteId
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.String HomeSiteId {get ; set ;}

		///<summary>
		///AwaySiteId
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String AwaySiteId {get ; set ;}

		///<summary>
		///主队天梯积分
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 HomeLadderScore {get ; set ;}

		///<summary>
		///客队天梯积分
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 AwayLadderScore {get ; set ;}

		///<summary>
		///主队比分
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 HomeScore {get ; set ;}

		///<summary>
		///客队比分
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 AwayScore {get ; set ;}

		///<summary>
		///HomeIsBot
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Boolean HomeIsBot {get ; set ;}

		///<summary>
		///AwayIsBot
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Boolean AwayIsBot {get ; set ;}

		///<summary>
		///所属分组
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 GroupIndex {get ; set ;}

		///<summary>
		///奖励主队积分
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 PrizeHomeScore {get ; set ;}

		///<summary>
		///奖励客队积分
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 PrizeAwayScore {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(22)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///HomeCoin
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 HomeCoin {get ; set ;}

		///<summary>
		///HomeExp
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 HomeExp {get ; set ;}

		///<summary>
		///AwayCoin
		///</summary>
        [DataMember]
        [ProtoMember(25)]
		public System.Int32 AwayCoin {get ; set ;}

		///<summary>
		///AwayExp
		///</summary>
        [DataMember]
        [ProtoMember(26)]
		public System.Int32 AwayExp {get ; set ;}
		#endregion
        
        #region Clone
        public CrossladderMatchEntity Clone()
        {
            CrossladderMatchEntity entity = new CrossladderMatchEntity();
			entity.Idx = this.Idx;
			entity.DomainId = this.DomainId;
			entity.LadderId = this.LadderId;
			entity.HomeId = this.HomeId;
			entity.AwayId = this.AwayId;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.HomeLogo = this.HomeLogo;
			entity.AwayLogo = this.AwayLogo;
			entity.HomeSiteId = this.HomeSiteId;
			entity.AwaySiteId = this.AwaySiteId;
			entity.HomeLadderScore = this.HomeLadderScore;
			entity.AwayLadderScore = this.AwayLadderScore;
			entity.HomeScore = this.HomeScore;
			entity.AwayScore = this.AwayScore;
			entity.HomeIsBot = this.HomeIsBot;
			entity.AwayIsBot = this.AwayIsBot;
			entity.GroupIndex = this.GroupIndex;
			entity.PrizeHomeScore = this.PrizeHomeScore;
			entity.PrizeAwayScore = this.PrizeAwayScore;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.HomeCoin = this.HomeCoin;
			entity.HomeExp = this.HomeExp;
			entity.AwayCoin = this.AwayCoin;
			entity.AwayExp = this.AwayExp;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossLadder_Match 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossladderMatchResponse : BaseResponse<CrossladderMatchEntity>
    {

    }
}
