
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossLadder_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrossladderManagerEntity
	{
		
		public CrossladderManagerEntity()
		{
		}

		public CrossladderManagerEntity(
		System.Guid managerid
,				System.Int32 domainid
,				System.String name
,				System.String logo
,				System.Int32 kpi
,				System.String siteid
,				System.String sitename
,				System.Int32 score
,				System.Int32 newlyscore
,				System.Int32 newlyhonor
,				System.Int32 honor
,				System.Int32 newlyladdercoin
,				System.Int32 laddercoin
,				System.Int32 maxscore
,				System.Int32 matchtime
,				System.DateTime lastexchagetime
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
,				System.Byte[] rowversion
,				System.Int32 dailymaxscore
,				System.Int32 dailymaxaddscore
,				System.Int32 stamina
,				System.Int32 staminabuy
		)
		{
			this.ManagerId = managerid;
			this.DomainId = domainid;
			this.Name = name;
			this.Logo = logo;
			this.Kpi = kpi;
			this.SiteId = siteid;
			this.SiteName = sitename;
			this.Score = score;
			this.NewlyScore = newlyscore;
			this.NewlyHonor = newlyhonor;
			this.Honor = honor;
			this.NewlyLadderCoin = newlyladdercoin;
			this.LadderCoin = laddercoin;
			this.MaxScore = maxscore;
			this.MatchTime = matchtime;
			this.LastExchageTime = lastexchagetime;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
			this.RowVersion = rowversion;
			this.DailyMaxScore = dailymaxscore;
			this.DailyMaxAddScore = dailymaxaddscore;
			this.Stamina = stamina;
			this.StaminaBuy = staminabuy;
		}
		
		#region Public Properties
		
		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///DomainId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Int32 DomainId {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String Name {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String Logo {get ; set ;}

		///<summary>
		///Kpi
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Kpi {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///SiteName
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.String SiteName {get ; set ;}

		///<summary>
		///天梯积分
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///最近增加积分
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 NewlyScore {get ; set ;}

		///<summary>
		///最近兑换荣誉数量
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 NewlyHonor {get ; set ;}

		///<summary>
		///荣誉数量
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 Honor {get ; set ;}

		///<summary>
		///NewlyLadderCoin
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 NewlyLadderCoin {get ; set ;}

		///<summary>
		///LadderCoin
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 LadderCoin {get ; set ;}

		///<summary>
		///最大积分
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 MaxScore {get ; set ;}

		///<summary>
		///今日比赛场次
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 MatchTime {get ; set ;}

		///<summary>
		///最近兑换时间
		///</summary>
        [DataMember]
        [ProtoMember(16)]
        [JsonIgnore]
		public System.DateTime LastExchageTime {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(18)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(19)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}

		///<summary>
		///RowVersion
		///</summary>
        [DataMember]
        [ProtoMember(20)]
        [JsonIgnore]
		public System.Byte[] RowVersion {get ; set ;}

		///<summary>
		///DailyMaxScore
		///</summary>
        [DataMember]
        [ProtoMember(21)]
		public System.Int32 DailyMaxScore {get ; set ;}

		///<summary>
		///DailyMaxAddScore
		///</summary>
        [DataMember]
        [ProtoMember(22)]
		public System.Int32 DailyMaxAddScore {get ; set ;}

		///<summary>
		///Stamina
		///</summary>
        [DataMember]
        [ProtoMember(23)]
		public System.Int32 Stamina {get ; set ;}

		///<summary>
		///StaminaBuy
		///</summary>
        [DataMember]
        [ProtoMember(24)]
		public System.Int32 StaminaBuy {get ; set ;}
		#endregion
        
        #region Clone
        public CrossladderManagerEntity Clone()
        {
            CrossladderManagerEntity entity = new CrossladderManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.DomainId = this.DomainId;
			entity.Name = this.Name;
			entity.Logo = this.Logo;
			entity.Kpi = this.Kpi;
			entity.SiteId = this.SiteId;
			entity.SiteName = this.SiteName;
			entity.Score = this.Score;
			entity.NewlyScore = this.NewlyScore;
			entity.NewlyHonor = this.NewlyHonor;
			entity.Honor = this.Honor;
			entity.NewlyLadderCoin = this.NewlyLadderCoin;
			entity.LadderCoin = this.LadderCoin;
			entity.MaxScore = this.MaxScore;
			entity.MatchTime = this.MatchTime;
			entity.LastExchageTime = this.LastExchageTime;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
			entity.RowVersion = this.RowVersion;
			entity.DailyMaxScore = this.DailyMaxScore;
			entity.DailyMaxAddScore = this.DailyMaxAddScore;
			entity.Stamina = this.Stamina;
			entity.StaminaBuy = this.StaminaBuy;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossLadder_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrossladderManagerResponse : BaseResponse<CrossladderManagerEntity>
    {

    }
}
