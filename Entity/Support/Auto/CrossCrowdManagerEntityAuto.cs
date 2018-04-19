
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_Manager 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdManagerEntity
	{
		
		public CrosscrowdManagerEntity()
		{
		}

		public CrosscrowdManagerEntity(
		System.Guid managerid
,				System.Int32 domainid
,				System.String siteid
,				System.String sitename
,				System.String name
,				System.String logo
,				System.Int32 crosscrowdid
,				System.Int32 morale
,				System.Int32 score
,				System.DateTime scoreupdatetime
,				System.Int32 killnumber
,				System.Int32 bykillnumber
,				System.DateTime nextmatchtime
,				System.Int32 clearcdcount
,				System.DateTime resurrectiontime
,				System.Int32 resurrectioncount
,				System.Int32 resurrectionauto
,				System.Int32 winningcount
,				System.Int32 kpi
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.ManagerId = managerid;
			this.DomainId = domainid;
			this.SiteId = siteid;
			this.SiteName = sitename;
			this.Name = name;
			this.Logo = logo;
			this.CrossCrowdId = crosscrowdid;
			this.Morale = morale;
			this.Score = score;
			this.ScoreUpdateTime = scoreupdatetime;
			this.KillNumber = killnumber;
			this.ByKillNumber = bykillnumber;
			this.NextMatchTime = nextmatchtime;
			this.ClearCdCount = clearcdcount;
			this.ResurrectionTime = resurrectiontime;
			this.ResurrectionCount = resurrectioncount;
			this.ResurrectionAuto = resurrectionauto;
			this.WinningCount = winningcount;
			this.Kpi = kpi;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
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
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///SiteName
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.String SiteName {get ; set ;}

		///<summary>
		///Name
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String Name {get ; set ;}

		///<summary>
		///Logo
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String Logo {get ; set ;}

		///<summary>
		///CrossCrowdId
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 CrossCrowdId {get ; set ;}

		///<summary>
		///Morale
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Morale {get ; set ;}

		///<summary>
		///Score
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///ScoreUpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime ScoreUpdateTime {get ; set ;}

		///<summary>
		///KillNumber
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 KillNumber {get ; set ;}

		///<summary>
		///ByKillNumber
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 ByKillNumber {get ; set ;}

		///<summary>
		///NextMatchTime
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime NextMatchTime {get ; set ;}

		///<summary>
		///ClearCdCount
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 ClearCdCount {get ; set ;}

		///<summary>
		///ResurrectionTime
		///</summary>
        [DataMember]
        [ProtoMember(15)]
        [JsonIgnore]
		public System.DateTime ResurrectionTime {get ; set ;}

		///<summary>
		///点券复活次数
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 ResurrectionCount {get ; set ;}

		///<summary>
		///到时间自动复活次数
		///</summary>
        [DataMember]
        [ProtoMember(17)]
		public System.Int32 ResurrectionAuto {get ; set ;}

		///<summary>
		///WinningCount
		///</summary>
        [DataMember]
        [ProtoMember(18)]
		public System.Int32 WinningCount {get ; set ;}

		///<summary>
		///Kpi
		///</summary>
        [DataMember]
        [ProtoMember(19)]
		public System.Int32 Kpi {get ; set ;}

		///<summary>
		///Status
		///</summary>
        [DataMember]
        [ProtoMember(20)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(21)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}

		///<summary>
		///UpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(22)]
        [JsonIgnore]
		public System.DateTime UpdateTime {get ; set ;}
		#endregion
        
        #region Clone
        public CrosscrowdManagerEntity Clone()
        {
            CrosscrowdManagerEntity entity = new CrosscrowdManagerEntity();
			entity.ManagerId = this.ManagerId;
			entity.DomainId = this.DomainId;
			entity.SiteId = this.SiteId;
			entity.SiteName = this.SiteName;
			entity.Name = this.Name;
			entity.Logo = this.Logo;
			entity.CrossCrowdId = this.CrossCrowdId;
			entity.Morale = this.Morale;
			entity.Score = this.Score;
			entity.ScoreUpdateTime = this.ScoreUpdateTime;
			entity.KillNumber = this.KillNumber;
			entity.ByKillNumber = this.ByKillNumber;
			entity.NextMatchTime = this.NextMatchTime;
			entity.ClearCdCount = this.ClearCdCount;
			entity.ResurrectionTime = this.ResurrectionTime;
			entity.ResurrectionCount = this.ResurrectionCount;
			entity.ResurrectionAuto = this.ResurrectionAuto;
			entity.WinningCount = this.WinningCount;
			entity.Kpi = this.Kpi;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_Manager 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdManagerResponse : BaseResponse<CrosscrowdManagerEntity>
    {

    }
}
