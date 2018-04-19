
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ProtoBuf;
namespace Games.NBall.Entity
{	
    /// <summary>
	/// 对Table dbo.League_Rank 的数据映射.
	/// </summary>
	[DataContract]
	[Serializable]
    [ProtoContract]
	public partial class LeagueRankEntity
	{
		
		public LeagueRankEntity()
		{
		}

		public LeagueRankEntity(
		System.Int32 idx
,				System.Guid managerid
,				System.String managername
,				System.Guid leaguerecordid
,				System.Int32 leaguerank
,				System.Int32 score
,				System.Int32 goal
,				System.Int32 lose
,				System.Int32 wincount
,				System.Int32 drawcount
,				System.Int32 lostcount
,				System.Int32 status
,				System.DateTime rowtime
		)
		{
			this.Idx = idx;
			this.ManagerId = managerid;
			this.ManagerName = managername;
			this.LeagueRecordId = leaguerecordid;
			this.LeagueRank = leaguerank;
			this.Score = score;
			this.Goal = goal;
			this.Lose = lose;
			this.WinCount = wincount;
			this.DrawCount = drawcount;
			this.LostCount = lostcount;
			this.Status = status;
			this.Rowtime = rowtime;
		}
		
		#region Public Properties
		
		///<summary>
		///标识号
		///</summary>
        [DataMember]
        [ProtoMember(1)]
		public System.Int32 Idx {get ; set ;}

		///<summary>
		///经理ID
		///</summary>
        [DataMember]
        [ProtoMember(2)]
		public System.Guid ManagerId {get ; set ;}

		///<summary>
		///经理名
		///</summary>
        [DataMember]
        [ProtoMember(3)]
		public System.String ManagerName {get ; set ;}

		///<summary>
		///联赛记录id
		///</summary>
        [DataMember]
        [ProtoMember(4)]
		public System.Guid LeagueRecordId {get ; set ;}

		///<summary>
		///联赛排名
		///</summary>
        [DataMember]
        [ProtoMember(5)]
		public System.Int32 LeagueRank {get ; set ;}

		///<summary>
		///积分
		///</summary>
        [DataMember]
        [ProtoMember(6)]
		public System.Int32 Score {get ; set ;}

		///<summary>
		///进球数
		///</summary>
        [DataMember]
        [ProtoMember(7)]
		public System.Int32 Goal {get ; set ;}

		///<summary>
		///失球数
		///</summary>
        [DataMember]
        [ProtoMember(8)]
		public System.Int32 Lose {get ; set ;}

		///<summary>
		///胜场
		///</summary>
        [DataMember]
        [ProtoMember(9)]
		public System.Int32 WinCount {get ; set ;}

		///<summary>
		///平场
		///</summary>
        [DataMember]
        [ProtoMember(10)]
		public System.Int32 DrawCount {get ; set ;}

		///<summary>
		///输场
		///</summary>
        [DataMember]
        [ProtoMember(11)]
		public System.Int32 LostCount {get ; set ;}

		///<summary>
		///状态
		///</summary>
        [DataMember]
        [ProtoMember(12)]
		public System.Int32 Status {get ; set ;}

		///<summary>
		///创建时间
		///</summary>
        [DataMember]
        [ProtoMember(13)]
        [JsonIgnore]
		public System.DateTime Rowtime {get ; set ;}
		#endregion
        
        #region Clone
        public LeagueRankEntity Clone()
        {
            LeagueRankEntity entity = new LeagueRankEntity();
			entity.Idx = this.Idx;
			entity.ManagerId = this.ManagerId;
			entity.ManagerName = this.ManagerName;
			entity.LeagueRecordId = this.LeagueRecordId;
			entity.LeagueRank = this.LeagueRank;
			entity.Score = this.Score;
			entity.Goal = this.Goal;
			entity.Lose = this.Lose;
			entity.WinCount = this.WinCount;
			entity.DrawCount = this.DrawCount;
			entity.LostCount = this.LostCount;
			entity.Status = this.Status;
			entity.Rowtime = this.Rowtime;
            return entity;
        }
        #endregion
		
	}
	
	
    /// <summary>
	/// 对Table dbo.League_Rank 的输出映射.
	/// </summary>	
    [DataContract]
    [Serializable]
    public partial class LeagueRankResponse : BaseResponse<LeagueRankEntity>
    {

    }
}

