
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.CrossCrowd_ManagerHistory 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class CrosscrowdManagerhistoryEntity
	{
		
		public CrosscrowdManagerhistoryEntity()
		{
		}

		public CrosscrowdManagerhistoryEntity(
		System.Int32 idx
,				System.String siteid
,				System.Guid managerid
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
,				System.Int32 rank
,				System.Int32 status
,				System.DateTime rowtime
,				System.DateTime updatetime
		)
		{
			this.Idx = idx;
			this.SiteId = siteid;
			this.ManagerId = managerid;
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
			this.Rank = rank;
			this.Status = status;
			this.RowTime = rowtime;
			this.UpdateTime = updatetime;
		}
		
		#region Public Properties
		
		///<summary>
		///Idx
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///SiteId
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.String SiteId {get ; set ;}

		///<summary>
		///ManagerId
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///CrossCrowdId
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Int32 CrossCrowdId {get ; set ;}

		///<summary>
		///Morale
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 Morale {get ; set ;}

		///<summary>
		///Score
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///ScoreUpdateTime
		///</summary>
        [DataMember]
        [ProtoMember(7)]
        [JsonIgnore]
		public System.DateTime ScoreUpdateTime {get ; set ;}

		///<summary>
		///KillNumber
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 KillNumber {get ; set ;}

		///<summary>
		///ByKillNumber
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 ByKillNumber {get ; set ;}

		///<summary>
		///NextMatchTime
		///</summary>
        [DataMember]
        [ProtoMember(10)]
        [JsonIgnore]
		public System.DateTime NextMatchTime {get ; set ;}

		///<summary>
		///ClearCdCount
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 ClearCdCount {get ; set ;}

		///<summary>
		///ResurrectionTime
		///</summary>
        [DataMember]
        [ProtoMember(12)]
        [JsonIgnore]
		public System.DateTime ResurrectionTime {get ; set ;}

		///<summary>
		///ResurrectionCount
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 ResurrectionCount {get ; set ;}

		///<summary>
		///ResurrectionAuto
		///</summary>
        [DataMember]
        [ProtoMember(14)]
		public System.Int32 ResurrectionAuto {get ; set ;}

		///<summary>
		///WinningCount
		///</summary>
        [DataMember]
        [ProtoMember(15)]
		public System.Int32 WinningCount {get ; set ;}

		///<summary>
		///Rank
		///</summary>
        [DataMember]
        [ProtoMember(16)]
		public System.Int32 Rank {get ; set ;}

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
		#endregion
        
        #region Clone
        public CrosscrowdManagerhistoryEntity Clone()
        {
            CrosscrowdManagerhistoryEntity entity = new CrosscrowdManagerhistoryEntity();
			entity.Idx = this.Idx;
			entity.SiteId = this.SiteId;
			entity.ManagerId = this.ManagerId;
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
			entity.Rank = this.Rank;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
			entity.UpdateTime = this.UpdateTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.CrossCrowd_ManagerHistory 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class CrosscrowdManagerhistoryResponse : BaseResponse<CrosscrowdManagerhistoryEntity>
    {

    }
}
