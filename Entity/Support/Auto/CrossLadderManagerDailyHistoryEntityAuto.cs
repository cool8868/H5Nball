
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossLadder_ManagerDailyHistory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossladderManagerdailyhistoryEntity
	{
		
		public CrossladderManagerdailyhistoryEntity()
		{
		}

		public CrossladderManagerdailyhistoryEntity(
		System.Int32 idx
,				System.Int32 domainid
,				System.DateTime recorddate
,				System.Int32 season
,				System.Guid managerid
,				System.String siteid
,				System.Int32 rank
,				System.Int32 score
,				System.String prizeitems
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Int32 dailymaxscore
,				System.Int32 dailymaxaddscore
,				System.Int32 maxscore
		)
		{
			this.Idx = idx;
			this.DomainId = domainid;
			this.RecordDate = recorddate;
			this.Season = season;
			this.ManagerId = managerid;
			this.SiteId = siteid;
			this.Rank = rank;
			this.Score = score;
			this.PrizeItems = prizeitems;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.DailyMaxScore = dailymaxscore;
			this.DailyMaxAddScore = dailymaxaddscore;
			this.MaxScore = maxscore;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///RecordDate
		///</summary>
        [DataMember]
        [ProtoMember(3)]
        [JsonIgnore]
		public System.DateTime RecordDate {get ; set ;}

		///<summary>
		///Season
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 Season {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///Rank
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Rank {get ; set ;}

		///<summary>
		///Score
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///PrizeItems
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.String PrizeItems {get ; set ;}

		///<summary>
		///Status
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
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///DailyMaxScore
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 DailyMaxScore {get ; set ;}

		///<summary>
		///DailyMaxAddScore
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 DailyMaxAddScore {get ; set ;}

		///<summary>
		///MaxScore
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 MaxScore {get ; set ;}
		#endregion
        
        #region Clone
        public CrossladderManagerdailyhistoryEntity Clone()
        {
            CrossladderManagerdailyhistoryEntity entity = new CrossladderManagerdailyhistoryEntity();
			entity.Idx = this.Idx;
			entity.DomainId = this.DomainId;
			entity.RecordDate = this.RecordDate;
			entity.Season = this.Season;
			entity.ManagerId = this.ManagerId;
			entity.SiteId = this.SiteId;
			entity.Rank = this.Rank;
			entity.Score = this.Score;
			entity.PrizeItems = this.PrizeItems;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.DailyMaxScore = this.DailyMaxScore;
			entity.DailyMaxAddScore = this.DailyMaxAddScore;
			entity.MaxScore = this.MaxScore;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossLadder_ManagerDailyHistory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossladderManagerdailyhistoryResponse : BaseResponse<CrossladderManagerdailyhistoryEntity>
    {

    }
}
