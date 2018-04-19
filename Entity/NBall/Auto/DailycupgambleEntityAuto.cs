
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.DailyCup_Gamble 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class DailycupGambleEntity
	{
		
		public DailycupGambleEntity()
		{
		}

		public DailycupGambleEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.Int32 gamblepoint
,				System.Guid matchid
,				System.String homename
,				System.String awayname
,				System.Int32 dailycupid
,				System.Int32 roundlevel
,				System.Int32 gambleresult
,				System.Guid gamblemanagerid
,				System.String gamblemanagername
,				System.Int32 resultpoint
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.GamblePoint = gamblepoint;
			this.MatchId = matchid;
			this.HomeName = homename;
			this.AwayName = awayname;
			this.DailyCupId = dailycupid;
			this.RoundLevel = roundlevel;
			this.GambleResult = gambleresult;
			this.GambleManagerId = gamblemanagerid;
			this.GambleManagerName = gamblemanagername;
			this.ResultPoint = resultpoint;
			this.Status = status;
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
		///经理id
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///押注砖石数量
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.Int32 GamblePoint {get ; set ;}

		///<summary>
		///下注的比赛
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid MatchId {get ; set ;}

		///<summary>
		///HomeName
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.String HomeName {get ; set ;}

		///<summary>
		///AwayName
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.String AwayName {get ; set ;}

		///<summary>
		///杯赛id
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 DailyCupId {get ; set ;}

		///<summary>
		///RoundLevel
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 RoundLevel {get ; set ;}

		///<summary>
		///押注的值--主队比赛结果（0：平，1：赢，2：败）
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 GambleResult {get ; set ;}

		///<summary>
		///GambleManagerId
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Guid GambleManagerId {get ; set ;}

		///<summary>
		///被押注经理名称
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.String GambleManagerName {get ; set ;}

		///<summary>
		///返回砖石数量
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 ResultPoint {get ; set ;}

		///<summary>
		///0为未处理，1为已开奖且竞猜成功，2为已开奖且竞猜失败
		///</summary>
        [DataMember]
        [ProtoMember(13)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///RowTime
		///</summary>
        [DataMember]
        [ProtoMember(14)]
        [JsonIgnore]
		public System.DateTime RowTime {get ; set ;}
		#endregion
        
        #region Clone
        public DailycupGambleEntity Clone()
        {
            DailycupGambleEntity entity = new DailycupGambleEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.GamblePoint = this.GamblePoint;
			entity.MatchId = this.MatchId;
			entity.HomeName = this.HomeName;
			entity.AwayName = this.AwayName;
			entity.DailyCupId = this.DailyCupId;
			entity.RoundLevel = this.RoundLevel;
			entity.GambleResult = this.GambleResult;
			entity.GambleManagerId = this.GambleManagerId;
			entity.GambleManagerName = this.GambleManagerName;
			entity.ResultPoint = this.ResultPoint;
			entity.Status = this.Status;
			entity.RowTime = this.RowTime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.DailyCup_Gamble 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class DailycupGambleResponse : BaseResponse<DailycupGambleEntity>
    {

    }
}

